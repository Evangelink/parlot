using System.Collections.Generic;
using Parlot.Fluent;
using Parlot.Rewriting;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="OneOf{T}"/> class.
    /// </summary>
    public class OneOfTests
    {
        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when passing a null array.
        /// </summary>
        [Fact]
        public void Constructor_NullParsers_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new OneOf<int>(null));
        }

        /// <summary>
        /// Tests that when no parser succeeds, the Parse method returns false.
        /// </summary>
        [Fact]
        public void Parse_NoParserSucceeds_ReturnsFalse()
        {
            // Arrange
            var parsers = new Parser<int>[]
            {
                new FakeParser<int>(shouldSucceed: false, returnValue: 0),
                new FakeParser<int>(shouldSucceed: false, returnValue: 0)
            };
            var oneOf = new OneOf<int>(parsers);
            var context = new FakeParseContext('a');
            var result = new ParseResult<int>();

            // Act
            bool parseResult = oneOf.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.False(result.Success);
        }

        /// <summary>
        /// Tests that when the first parser succeeds, Parse returns true and sets the correct result.
        /// </summary>
        [Fact]
        public void Parse_FirstParserSucceeds_ReturnsTrue()
        {
            // Arrange
            var expectedValue = 10;
            var parsers = new Parser<int>[]
            {
                new FakeParser<int>(shouldSucceed: true, returnValue: expectedValue),
                new FakeParser<int>(shouldSucceed: false, returnValue: 0)
            };
            var oneOf = new OneOf<int>(parsers);
            var context = new FakeParseContext('b');
            var result = new ParseResult<int>();

            // Act
            bool parseResult = oneOf.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.Success);
            Assert.Equal(expectedValue, result.Value);
        }

        /// <summary>
        /// Tests that if the first parser fails and the second succeeds, Parse returns true with the second parser's result.
        /// </summary>
        [Fact]
        public void Parse_SecondParserSucceeds_ReturnsTrue()
        {
            // Arrange
            var expectedValue = 20;
            var parsers = new Parser<int>[]
            {
                new FakeParser<int>(shouldSucceed: false, returnValue: 0),
                new FakeParser<int>(shouldSucceed: true, returnValue: expectedValue)
            };
            var oneOf = new OneOf<int>(parsers);
            var context = new FakeParseContext('c');
            var result = new ParseResult<int>();

            // Act
            bool parseResult = oneOf.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.Success);
            Assert.Equal(expectedValue, result.Value);
        }

        /// <summary>
        /// Tests that the ToString method returns a string containing the parsers and expected characters.
        /// </summary>
        [Fact]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var parser1 = new FakeParser<int>(shouldSucceed: false, returnValue: 0);
            var parser2 = new FakeParser<int>(shouldSucceed: false, returnValue: 0);
            var parsers = new Parser<int>[] { parser1, parser2 };
            var oneOf = new OneOf<int>(parsers);
            string expected = $"{string.Join(" | ", oneOf.Parsers)}) on [{string.Join(" ", oneOf.ExpectedChars)}]";

            // Act
            string toStringResult = oneOf.ToString();

            // Assert
            Assert.Equal(expected, toStringResult);
        }

        /// <summary>
        /// Tests that the OriginalParsers and Parsers properties are set correctly in the constructor.
        /// </summary>
        [Fact]
        public void Properties_AreInitializedCorrectly()
        {
            // Arrange
            var fakeParser = new FakeParser<int>(shouldSucceed: false, returnValue: 0);
            var parsers = new Parser<int>[] { fakeParser };
            var oneOf = new OneOf<int>(parsers);

            // Act & Assert
            Assert.Equal(parsers, oneOf.OriginalParsers);
            Assert.Equal(parsers, oneOf.Parsers);
        }

        /// <summary>
        /// Tests that the Parse method correctly uses seekable parsers based on the current cursor character.
        /// </summary>
        [Fact]
        public void Parse_SeekableParser_SucceedsBasedOnExpectedChar()
        {
            // Arrange
            var expectedValue = 99;
            // First seekable parser fails, second succeeds.
            var seekableParser1 = new FakeSeekableParser<int>(shouldSucceed: false, returnValue: 0, expectedChars: new char[] { 'x' });
            var seekableParser2 = new FakeSeekableParser<int>(shouldSucceed: true, returnValue: expectedValue, expectedChars: new char[] { 'x' });
            var parsers = new Parser<int>[] { seekableParser1, seekableParser2 };
            var oneOf = new OneOf<int>(parsers);
            var context = new FakeParseContext('x');
            var result = new ParseResult<int>();

            // Act
            bool parseResult = oneOf.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.Success);
            Assert.Equal(expectedValue, result.Value);
        }

        /// <summary>
        /// Tests that when whitespace is skipped and no parser succeeds, the cursor position is reset.
        /// </summary>
        [Fact]
        public void Parse_NoParserSucceedsWithSkipWhitespace_ResetsCursorPosition()
        {
            // Arrange
            // Use seekable parsers with SkipWhitespace = true to force the lookup table path.
            var seekableParser1 = new FakeSeekableParser<int>(shouldSucceed: false, returnValue: 0, expectedChars: new char[] { ' ' }, skipWhitespace: true);
            var seekableParser2 = new FakeSeekableParser<int>(shouldSucceed: false, returnValue: 0, expectedChars: new char[] { ' ' }, skipWhitespace: true);
            var parsers = new Parser<int>[] { seekableParser1, seekableParser2 };
            var oneOf = new OneOf<int>(parsers);
            var context = new FakeParseContext(' ');
            var initialPosition = context.Scanner.Cursor.Position;
            var result = new ParseResult<int>();

            // Act
            bool parseResult = oneOf.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            // Since SkipWhitespace is true, the cursor position should be reset to the initial value.
            Assert.Equal(initialPosition, context.Scanner.Cursor.Position);
        }
    }

    #region Fake Implementations for Testing

    /// <summary>
    /// A fake implementation of ParseResult for testing purposes.
    /// </summary>
    public class ParseResult<T>
    {
        public bool Success { get; set; }
        public T Value { get; set; }
    }

    /// <summary>
    /// A fake implementation of a parser for testing.
    /// </summary>
    public class FakeParser<T> : Parser<T>
    {
        private readonly bool _shouldSucceed;
        private readonly T _returnValue;

        public FakeParser(bool shouldSucceed, T returnValue)
        {
            _shouldSucceed = shouldSucceed;
            _returnValue = returnValue;
        }

        public override bool Parse(ParseContext context, ref ParseResult<T> result)
        {
            if (_shouldSucceed)
            {
                result.Success = true;
                result.Value = _returnValue;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "FakeParser";
        }
    }

    /// <summary>
    /// A fake seekable parser for testing which implements ISeekable.
    /// </summary>
    public class FakeSeekableParser<T> : FakeParser<T>, ISeekable
    {
        public bool CanSeek => true;
        public char[] ExpectedChars { get; }
        public bool SkipWhitespace { get; }

        public FakeSeekableParser(bool shouldSucceed, T returnValue, char[] expectedChars, bool skipWhitespace = false)
            : base(shouldSucceed, returnValue)
        {
            ExpectedChars = expectedChars;
            SkipWhitespace = skipWhitespace;
        }

        public override string ToString()
        {
            return "FakeSeekableParser";
        }
    }

    /// <summary>
    /// A fake implementation of ParseContext for testing.
    /// </summary>
    public class FakeParseContext : ParseContext
    {
        public FakeParseContext(char currentChar)
        {
            Scanner = new FakeScanner(currentChar);
        }

        public override Scanner Scanner { get; } = null!;

        public override void EnterParser<T>(Parser<T> parser)
        {
            // No-op for testing.
        }

        public override void ExitParser<T>(Parser<T> parser)
        {
            // No-op for testing.
        }

        public override void SkipWhiteSpace()
        {
            // No-op for testing.
        }
    }

    /// <summary>
    /// A fake implementation of Scanner for testing.
    /// </summary>
    public class FakeScanner : Scanner
    {
        public FakeScanner(char currentChar)
        {
            Cursor = new FakeCursor(currentChar);
        }

        public override Cursor Cursor { get; }
    }

    /// <summary>
    /// A fake implementation of Cursor for testing.
    /// </summary>
    public class FakeCursor : Cursor
    {
        public FakeCursor(char currentChar)
        {
            Current = currentChar;
            Position = 0;
        }

        public override char Current { get; set; }

        public override TextPosition Position { get; set; } = new TextPosition(0, 0, 0);

        public override void ResetPosition(TextPosition position)
        {
            Position = position;
        }
    }

    #endregion

    #region Minimal Abstract Classes (Stubs)

    // These minimal abstract classes are provided to allow the fake implementations to compile.
    // In production, the actual implementations would come from the Parlot library.

    public abstract class Parser<T>
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
    }

    public abstract class ParseContext
    {
        public abstract Scanner Scanner { get; }
        public abstract void EnterParser<T>(Parser<T> parser);
        public abstract void ExitParser<T>(Parser<T> parser);
        public abstract void SkipWhiteSpace();
    }

    public abstract class Scanner
    {
        public abstract Cursor Cursor { get; }
    }

    public abstract class Cursor
    {
        public abstract char Current { get; set; }
        public abstract TextPosition Position { get; set; }
        public abstract void ResetPosition(TextPosition position);
    }

    public readonly struct TextPosition : IEquatable<TextPosition>
    {
        public int Absolute { get; }
        public int Line { get; }
        public int Column { get; }

        public TextPosition(int absolute, int line, int column)
        {
            Absolute = absolute;
            Line = line;
            Column = column;
        }

        public bool Equals(TextPosition other)
        {
            return Absolute == other.Absolute && Line == other.Line && Column == other.Column;
        }

        public override bool Equals(object? obj)
        {
            return obj is TextPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Absolute, Line, Column);
        }

        public override string ToString()
        {
            return $"Abs:{Absolute}, L:{Line}, C:{Column}";
        }
    }

    #endregion
}
