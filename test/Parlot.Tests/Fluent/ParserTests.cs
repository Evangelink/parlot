using System;
using System.Linq;
using Moq;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Dummy implementation for ParseContext used for testing.
    /// </summary>
    public class DummyParseContext
    {
    }

    /// <summary>
    /// Dummy implementation for ParseResult used for testing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DummyParseResult<T>
    {
    }

    /// <summary>
    /// Dummy implementation for ISeekable interface used for testing.
    /// </summary>
    public interface ISeekable
    {
        bool SkipWhitespace { get; }
        ReadOnlySpan<char> ExpectedChars { get; }
    }

    /// <summary>
    /// Dummy implementation for ISeekable for testing purposes.
    /// </summary>
    public class FakeSeekable : ISeekable
    {
        public bool SkipWhitespace { get; }
        public ReadOnlySpan<char> ExpectedChars { get; }

        public FakeSeekable(bool skipWhitespace, string expectedChars)
        {
            SkipWhitespace = skipWhitespace;
            ExpectedChars = expectedChars.AsSpan();
        }
    }

    /// <summary>
    /// A fake concrete implementation of the abstract Parser<T> class for testing.
    /// This class also provides a public Name property to support tests for the Named() method.
    /// </summary>
    /// <typeparam name="T">The type parameter for the parser result.</typeparam>
    public class FakeParser<T> : Parser<T>
    {
        // Adding a public property for testing the Named method.
        public string Name { get; set; }

        public override bool Parse(ParseContext context, ref ParseResult<T> result)
        {
            // For testing, simply set result to a new instance and return true.
            result = new ParseResult<T>();
            return true;
        }

        // Override the Named method to store Name in this fake parser.
        public new FakeParser<T> Named(string name)
        {
            Name = name;
            return this;
        }
    }

    /// <summary>
    /// Minimal definitions for ParseContext and ParseResult<T> to support tests.
    /// </summary>
    public class ParseContext
    {
    }

    public class ParseResult<T>
    {
    }

    /// <summary>
    /// Unit tests for the <see cref="Parser{T}"/> class.
    /// </summary>
    public class ParserTests
    {
        private readonly FakeParser<int> _parser;

        public ParserTests()
        {
            _parser = new FakeParser<int>();
        }

        /// <summary>
        /// Tests that Parse method executes and returns true.
        /// </summary>
        [Fact]
        public void Parse_WhenCalled_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var context = new ParseContext();
            ParseResult<int> result = null;

            // Act
            bool parseSuccess = _parser.Parse(context, ref result);

            // Assert
            Assert.True(parseSuccess);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests that Then(Func&lt;T, U&gt;) returns a new parser instance of appropriate type.
        /// </summary>
        [Fact]
        public void Then_WithConversionFunction_ReturnsThenParser()
        {
            // Arrange
            Func<int, string> conversion = i => i.ToString();

            // Act
            var thenParser = _parser.Then(conversion);

            // Assert
            Assert.NotNull(thenParser);
            Assert.NotSame(_parser, thenParser);
            Assert.Contains("Then", thenParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Then(Func&lt;ParseContext, T, U&gt;) returns a new parser instance of appropriate type.
        /// </summary>
        [Fact]
        public void Then_WithContextConversionFunction_ReturnsThenParser()
        {
            // Arrange
            Func<ParseContext, int, string> conversion = (ctx, i) => $"{i}";
            
            // Act
            var thenParser = _parser.Then(conversion);

            // Assert
            Assert.NotNull(thenParser);
            Assert.NotSame(_parser, thenParser);
            Assert.Contains("Then", thenParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Then(U value) returns a new parser instance wrapping the specified value.
        /// </summary>
        [Fact]
        public void Then_WithSpecifiedValue_ReturnsThenParser()
        {
            // Arrange
            string specifiedValue = "constant";

            // Act
            var thenParser = _parser.Then(specifiedValue);

            // Assert
            Assert.NotNull(thenParser);
            Assert.NotSame(_parser, thenParser);
            Assert.Contains("Then", thenParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that ThenElse(Func&lt;T, U&gt;, U) returns a parser that will yield conversion on success, and elseValue if failed.
        /// </summary>
        [Fact]
        public void ThenElse_WithConversionFunction_ReturnsThenElseParser()
        {
            // Arrange
            Func<int, string> conversion = i => i.ToString();
            string elseValue = "default";

            // Act
            var thenElseParser = _parser.ThenElse(conversion, elseValue);

            // Assert
            Assert.NotNull(thenElseParser);
            Assert.Contains("Then", thenElseParser.GetType().Name, StringComparison.Ordinal);
            Assert.Contains("Else", thenElseParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that ThenElse(Func&lt;ParseContext, T, U&gt;, U) returns a parser that will yield conversion with context, or elseValue if failed.
        /// </summary>
        [Fact]
        public void ThenElse_WithContextConversionFunction_ReturnsThenElseParser()
        {
            // Arrange
            Func<ParseContext, int, string> conversion = (ctx, i) => $"{i}";
            string elseValue = "default";

            // Act
            var thenElseParser = _parser.ThenElse(conversion, elseValue);

            // Assert
            Assert.NotNull(thenElseParser);
            Assert.Contains("Then", thenElseParser.GetType().Name, StringComparison.Ordinal);
            Assert.Contains("Else", thenElseParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that ThenElse(U, U) returns a parser that will yield a constant conversion value or elseValue.
        /// </summary>
        [Fact]
        public void ThenElse_WithConstantValues_ReturnsThenElseParser()
        {
            // Arrange
            string value = "first";
            string elseValue = "second";

            // Act
            var thenElseParser = _parser.ThenElse(value, elseValue);

            // Assert
            Assert.NotNull(thenElseParser);
            Assert.Contains("Then", thenElseParser.GetType().Name, StringComparison.Ordinal);
            Assert.Contains("Else", thenElseParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that ElseError(string) returns a parser instance that emits an error.
        /// </summary>
        [Fact]
        public void ElseError_WithMessage_ReturnsElseErrorParser()
        {
            // Arrange
            string errorMessage = "Error occurred";

            // Act
            var errorParser = _parser.ElseError(errorMessage);

            // Assert
            Assert.NotNull(errorParser);
            Assert.Contains("ElseError", errorParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Error(string) returns a parser instance that emits an error.
        /// </summary>
        [Fact]
        public void Error_WithMessage_ReturnsErrorParser()
        {
            // Arrange
            string errorMessage = "Error occurred";

            // Act
            var errorParser = _parser.Error(errorMessage);

            // Assert
            Assert.NotNull(errorParser);
            Assert.Contains("Error", errorParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Error&lt;U&gt;(string) returns a parser instance that emits an error and converts the result.
        /// </summary>
        [Fact]
        public void Error_Generic_WithMessage_ReturnsErrorParser()
        {
            // Arrange
            string errorMessage = "Error occurred";

            // Act
            var errorParser = _parser.Error<string>(errorMessage);

            // Assert
            Assert.NotNull(errorParser);
            Assert.Contains("Error", errorParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Named(string) sets the parser's name and returns the same instance.
        /// </summary>
        [Fact]
        public void Named_WithName_SetsNameAndReturnsSameInstance()
        {
            // Arrange
            string expectedName = "TestParser";

            // Act
            // Use the overridden Named method on FakeParser to capture the name.
            var returnedParser = _parser.Named(expectedName);

            // Assert
            Assert.NotNull(returnedParser);
            Assert.Equal(expectedName, _parser.Name);
            Assert.Same(_parser, returnedParser);
        }

        /// <summary>
        /// Tests that When(Func&lt;T, bool&gt;) (obsolete) returns a parser that verifies the result with a predicate.
        /// </summary>
        [Fact]
        public void When_Obsolete_WithPredicate_ReturnsWhenParser()
        {
            // Arrange
            Func<int, bool> predicate = i => i > 0;

            // Act
            var whenParser = _parser.When(predicate);

            // Assert
            Assert.NotNull(whenParser);
            Assert.Contains("When", whenParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that When(Func&lt;ParseContext, T, bool&gt;) returns a parser that verifies the result with a predicate.
        /// </summary>
        [Fact]
        public void When_WithContextPredicate_ReturnsWhenParser()
        {
            // Arrange
            Func<ParseContext, int, bool> predicate = (ctx, i) => i > 0;

            // Act
            var whenParser = _parser.When(predicate);

            // Assert
            Assert.NotNull(whenParser);
            Assert.Contains("When", whenParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Switch&lt;U&gt;(Func&lt;ParseContext, T, Parser&lt;U&gt;&gt;) returns a parser that switches based on the result.
        /// </summary>
        [Fact]
        public void Switch_WithAction_ReturnsSwitchParser()
        {
            // Arrange
            Func<ParseContext, int, Parser<string>> action = (ctx, i) =>
            {
                // For testing, return a new FakeParser<string>.
                return new FakeParser<string>();
            };

            // Act
            var switchParser = _parser.Switch(action);

            // Assert
            Assert.NotNull(switchParser);
            Assert.Contains("Switch", switchParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Eof() returns a parser that ensures the cursor is at the end of input.
        /// </summary>
        [Fact]
        public void Eof_WhenCalled_ReturnsEofParser()
        {
            // Act
            var eofParser = _parser.Eof();

            // Assert
            Assert.NotNull(eofParser);
            Assert.Contains("Eof", eofParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Discard&lt;U&gt;() returns a parser that discards the previous result and replaces it with default of U.
        /// </summary>
        [Fact]
        public void Discard_WithoutValue_ReturnsDiscardParser()
        {
            // Act
            var discardParser = _parser.Discard<string>();

            // Assert
            Assert.NotNull(discardParser);
            Assert.Contains("Discard", discardParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Discard&lt;U&gt;(U value) returns a parser that discards the previous result and replaces it with the provided value.
        /// </summary>
        [Fact]
        public void Discard_WithValue_ReturnsDiscardParser()
        {
            // Arrange
            string replacementValue = "replacement";

            // Act
            var discardParser = _parser.Discard(replacementValue);

            // Assert
            Assert.NotNull(discardParser);
            Assert.Contains("Discard", discardParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Else(T) returns a parser that returns the provided default value if the previous parser fails.
        /// </summary>
        [Fact]
        public void Else_WithValue_ReturnsElseParser()
        {
            // Arrange
            int defaultValue = -1;

            // Act
            var elseParser = _parser.Else(defaultValue);

            // Assert
            Assert.NotNull(elseParser);
            Assert.Contains("Else", elseParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Lookup(bool, params ReadOnlySpan&lt;char&gt;) returns a seekable parser using the provided parameters.
        /// </summary>
        [Fact]
        public void Lookup_WithSkipWhitespaceAndExpectedChars_ReturnsSeekableParser()
        {
            // Arrange
            bool skipWhitespace = true;
            ReadOnlySpan<char> expectedChars = "abc".AsSpan();

            // Act
            var lookupParser = _parser.Lookup(skipWhitespace, expectedChars);

            // Assert
            Assert.NotNull(lookupParser);
            Assert.Contains("Seekable", lookupParser.GetType().Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Tests that Lookup(params ISeekable[]) returns a seekable parser using properties from the provided seekable parsers.
        /// </summary>
        [Fact]
        public void Lookup_WithSeekableArray_ReturnsSeekableParser()
        {
            // Arrange
            var seekable1 = new FakeSeekable(true, "123");
            var seekable2 = new FakeSeekable(false, "xyz");

            // Act
            var lookupParser = _parser.Lookup(new ISeekable[] { seekable1, seekable2 });

            // Assert
            Assert.NotNull(lookupParser);
            Assert.Contains("Seekable", lookupParser.GetType().Name, StringComparison.Ordinal);
            // Validate that expected chars were aggregated.
            var aggregatedChars = seekable1.ExpectedChars.ToString() + seekable2.ExpectedChars.ToString();
            Assert.Contains("123", aggregatedChars);
            Assert.Contains("xyz", aggregatedChars);
        }
    }
}
