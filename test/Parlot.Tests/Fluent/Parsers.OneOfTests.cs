using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// A simple fake implementation of Parser{T} for testing purposes.
    /// </summary>
    /// <typeparam name="T">The parser result type.</typeparam>
    public class FakeParser<T> : Parser<T>
    {
    }

    /// <summary>
    /// Unit tests for the extension methods in the Parsers class.
    /// </summary>
    public class ParsersTests
    {
        private readonly Parser<string> _fakeParser1;
        private readonly Parser<string> _fakeParser2;
        private readonly Parser<string> _fakeParser3;

        /// <summary>
        /// Initializes the test class with dummy parser instances.
        /// </summary>
        public ParsersTests()
        {
            // Arrange: using FakeParser as simple dummy implementations.
            _fakeParser1 = new FakeParser<string>();
            _fakeParser2 = new FakeParser<string>();
            _fakeParser3 = new FakeParser<string>();
        }

        /// <summary>
        /// Helper method to retrieve the 'OriginalParsers' property via reflection from a OneOf parser.
        /// </summary>
        /// <typeparam name="T">The parser result type.</typeparam>
        /// <param name="oneOfParser">The OneOf parser instance.</param>
        /// <returns>A read-only list of original parsers.</returns>
        private static IReadOnlyList<Parser<T>> GetOriginalParsers<T>(object oneOfParser)
        {
            var property = oneOfParser.GetType().GetProperty("OriginalParsers", BindingFlags.Instance | BindingFlags.Public);
            if (property == null)
            {
                throw new InvalidOperationException("The property 'OriginalParsers' was not found on the OneOf parser instance.");
            }
            return (IReadOnlyList<Parser<T>>)property.GetValue(oneOfParser);
        }

        /// <summary>
        /// Tests the Or{T}(this Parser{T}, Parser{T}) method when the original parser is not a OneOf.
        /// Verifies that a new OneOf parser is created containing both the original and the additional parser.
        /// </summary>
        [Fact]
        public void Or_WhenNotOneOf_ReturnsNewOneOfContainingBothParsers()
        {
            // Arrange
            var parser1 = _fakeParser1;
            var parser2 = _fakeParser2;

            // Act
            var result = parser1.Or(parser2);

            // Assert
            Assert.NotNull(result);
            // Verify that the returned instance is of type OneOf<T> by checking its type name.
            Assert.Equal("OneOf`1", result.GetType().Name);
            
            var originalParsers = GetOriginalParsers<string>(result);
            Assert.Equal(2, originalParsers.Count);
            Assert.Same(parser1, originalParsers[0]);
            Assert.Same(parser2, originalParsers[1]);
        }

        /// <summary>
        /// Tests the Or{T}(this Parser{T}, Parser{T}) method when the original parser is already a OneOf.
        /// Verifies that a new OneOf parser is created with the additional parser appended to the existing collection.
        /// </summary>
        [Fact]
        public void Or_WhenParserIsOneOf_ReturnsNewOneOfWithAppendedParser()
        {
            // Arrange
            var parser1 = _fakeParser1;
            var parser2 = _fakeParser2;
            var parser3 = _fakeParser3;
            // Create an initial OneOf parser from two parsers.
            var initialOneOf = Parsers.OneOf(parser1, parser2);

            // Act
            var result = initialOneOf.Or(parser3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("OneOf`1", result.GetType().Name);
            var originalParsers = GetOriginalParsers<string>(result);
            Assert.Equal(3, originalParsers.Count);
            Assert.Same(parser1, originalParsers[0]);
            Assert.Same(parser2, originalParsers[1]);
            Assert.Same(parser3, originalParsers[2]);
        }

        /// <summary>
        /// Tests the generic Or{A,B,T}(this Parser{A}, Parser{B}) method.
        /// Verifies that the method returns a OneOf parser combining both parser instances.
        /// </summary>
        [Fact]
        public void Or_Generic_ReturnsOneOfContainingBothParsers()
        {
            // Arrange
            var mockParserA = new Mock<Parser<string>>();
            var mockParserB = new Mock<Parser<string>>();
            var parserA = mockParserA.Object;
            var parserB = mockParserB.Object;

            // Act
            var result = parserA.Or(parserB);

            // Assert
            Assert.NotNull(result);
            // Check that the result's type name indicates it's a OneOf parser.
            Assert.Contains("OneOf", result.GetType().Name);
            var originalParsers = GetOriginalParsers<string>(result);
            Assert.Equal(2, originalParsers.Count);
            Assert.Same(parserA, originalParsers[0]);
            Assert.Same(parserB, originalParsers[1]);
        }

        /// <summary>
        /// Tests the OneOf{T}(params Parser{T}[]) method with multiple parsers.
        /// Verifies that a new OneOf parser is created containing all provided parsers in order.
        /// </summary>
        [Fact]
        public void OneOf_WithMultipleParsers_ReturnsOneOfContainingAllParsers()
        {
            // Arrange
            var parsers = new Parser<string>[] { _fakeParser1, _fakeParser2, _fakeParser3 };

            // Act
            var result = Parsers.OneOf(parsers);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("OneOf`1", result.GetType().Name);
            var originalParsers = GetOriginalParsers<string>(result);
            Assert.Equal(parsers.Length, originalParsers.Count);
            Assert.Same(_fakeParser1, originalParsers[0]);
            Assert.Same(_fakeParser2, originalParsers[1]);
            Assert.Same(_fakeParser3, originalParsers[2]);
        }

        /// <summary>
        /// Tests the OneOf{T}(params Parser{T}[]) method with an empty array of parsers.
        /// Verifies that a new OneOf parser is created with an empty collection of original parsers.
        /// </summary>
        [Fact]
        public void OneOf_WithEmptyArray_ReturnsOneOfWithNoParsers()
        {
            // Arrange
            var parsers = Array.Empty<Parser<string>>();

            // Act
            var result = Parsers.OneOf(parsers);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("OneOf`1", result.GetType().Name);
            var originalParsers = GetOriginalParsers<string>(result);
            Assert.Empty(originalParsers);
        }
    }
}
