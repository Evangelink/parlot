using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// A fake parser implementation for testing purposes.
    /// </summary>
    /// <typeparam name="T">The type parameter for the parser.</typeparam>
    public class FakeParser<T> : Parser<T>
    {
        public string Identifier { get; }

        public FakeParser(string identifier)
        {
            Identifier = identifier;
        }

        public override string ToString() => $"FakeParser({Identifier})";
    }

    /// <summary>
    /// A fake parser implementation of type A for testing the generic overload.
    /// </summary>
    public class FakeParserA : Parser<string>
    {
        public string Name { get; }

        public FakeParserA(string name)
        {
            Name = name;
        }

        public override string ToString() => $"FakeParserA({Name})";
    }

    /// <summary>
    /// A fake parser implementation of type B for testing the generic overload.
    /// </summary>
    public class FakeParserB : Parser<string>
    {
        public string Name { get; }

        public FakeParserB(string name)
        {
            Name = name;
        }

        public override string ToString() => $"FakeParserB({Name})";
    }

    /// <summary>
    /// Unit tests for the Parsers class.
    /// </summary>
    public class ParsersTests
    {
        /// <summary>
        /// Tests that the Or extension method creates a new OneOf instance containing both parsers 
        /// when the original parser is not already a OneOf.
        /// </summary>
        [Fact]
        public void Or_WhenParserIsNotOneOf_CreatesNewOneOfWithBothParsers()
        {
            // Arrange
            var parser1 = new FakeParser<string>("P1");
            var parser2 = new FakeParser<string>("P2");

            // Act
            var result = parser1.Or(parser2);

            // Assert
            var oneOf = Assert.IsType<OneOf<string>>(result);
            Assert.NotNull(oneOf.OriginalParsers);

            var parsersList = oneOf.OriginalParsers.ToList();
            Assert.Equal(2, parsersList.Count);
            Assert.Same(parser1, parsersList[0]);
            Assert.Same(parser2, parsersList[1]);
        }

        /// <summary>
        /// Tests that the Or extension method appends a new parser to an existing OneOf instance.
        /// </summary>
        [Fact]
        public void Or_WhenParserIsOneOf_AppendsNewParserToExistingOneOf()
        {
            // Arrange
            var fake1 = new FakeParser<string>("P1");
            var fake2 = new FakeParser<string>("P2");
            var fake3 = new FakeParser<string>("P3");

            // Create an initial OneOf instance using the OneOf method.
            var oneOfInitial = Parsers.OneOf(new Parser<string>[] { fake1, fake2 });

            // Act
            var result = oneOfInitial.Or(fake3);

            // Assert
            var oneOfResult = Assert.IsType<OneOf<string>>(result);
            Assert.NotNull(oneOfResult.OriginalParsers);

            var parsersList = oneOfResult.OriginalParsers.ToList();
            Assert.Equal(3, parsersList.Count);
            Assert.Same(fake1, parsersList[0]);
            Assert.Same(fake2, parsersList[1]);
            Assert.Same(fake3, parsersList[2]);
        }

        /// <summary>
        /// Tests the generic Or<A, B, T> extension method to ensure it creates a OneOf instance 
        /// with the provided different parser types.
        /// </summary>
        [Fact]
        public void Or_GenericVersion_CreatesOneOfOfTwoDifferentParserTypes()
        {
            // Arrange
            var parserA = new FakeParserA("A");
            var parserB = new FakeParserB("B");

            // Act
            // Explicitly specifying type arguments to ensure the generic overload is called.
            var result = Parsers.Or<FakeParserA, FakeParserB, string>(parserA, parserB);

            // Assert
            var oneOfGeneric = Assert.IsType<OneOf<FakeParserA, FakeParserB, string>>(result);
            // Since internal structure is not exposed, we rely on ToString containing identifiers.
            var resultString = oneOfGeneric.ToString();
            Assert.Contains("A", resultString);
            Assert.Contains("B", resultString);
        }

        /// <summary>
        /// Tests that the OneOf method wraps multiple parsers into a OneOf instance correctly.
        /// </summary>
        [Fact]
        public void OneOf_WithMultipleParsers_CreatesOneOfContainingAllParsers()
        {
            // Arrange
            var parser1 = new FakeParser<string>("P1");
            var parser2 = new FakeParser<string>("P2");
            var parser3 = new FakeParser<string>("P3");

            var parsers = new Parser<string>[] { parser1, parser2, parser3 };

            // Act
            var result = Parsers.OneOf(parsers);

            // Assert
            var oneOf = Assert.IsType<OneOf<string>>(result);
            Assert.NotNull(oneOf.OriginalParsers);

            var parsersList = oneOf.OriginalParsers.ToList();
            Assert.Equal(3, parsersList.Count);
            Assert.Same(parser1, parsersList[0]);
            Assert.Same(parser2, parsersList[1]);
            Assert.Same(parser3, parsersList[2]);
        }
    }
}
