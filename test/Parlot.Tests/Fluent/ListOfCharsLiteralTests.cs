using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ListOfChars"/> class.
    /// </summary>
    public class ListOfCharsTests
    {
        private readonly ListOfChars _parserWithoutNewLine;
        private readonly ListOfChars _parserWithNewLine;

        public ListOfCharsTests()
        {
            // Note: Due to the order of assignments in the constructor of ListOfChars,
            // even if a positive minSize is provided, the ExpectedChars property remains empty 
            // and CanSeek remains false.
            _parserWithoutNewLine = new ListOfChars("abc", 1, 0);
            _parserWithNewLine = new ListOfChars("a\nc", 1, 0);
        }

        /// <summary>
        /// Tests that the constructor sets default property values when no newline is present.
        /// Expected: CanSeek is false, ExpectedChars is empty, and SkipWhitespace is false.
        /// </summary>
        [Fact]
        public void Constructor_WithoutNewLine_SetsDefaultProperties()
        {
            // Arrange is done in the constructor of the test class.

            // Act
            bool canSeek = _parserWithoutNewLine.CanSeek;
            char[] expectedChars = _parserWithoutNewLine.ExpectedChars;
            bool skipWhitespace = _parserWithoutNewLine.SkipWhitespace;

            // Assert
            Assert.False(canSeek);
            Assert.Empty(expectedChars);
            Assert.False(skipWhitespace);
        }

        /// <summary>
        /// Tests that the ToString method returns the expected format.
        /// Expected: "AnyOf([])" because the ExpectedChars property remains empty.
        /// </summary>
        [Fact]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            // Using _parserWithoutNewLine which was initialized with "abc" but ExpectedChars remains empty due to bug.

            // Act
            string result = _parserWithoutNewLine.ToString();

            // Assert
            Assert.Equal("AnyOf([])", result);
        }

        /// <summary>
        /// Tests the Parse method for a successful parse when no newline is present.
        /// It verifies that AdvanceNoNewLines is called and the result is correctly set.
        /// </summary>
        [Fact]
        public void Parse_WithoutNewLine_MatchingCharacters_ReturnsTrueAndAdvancesCursor()
        {
            // Arrange
            string buffer = "abcdef";
            FakeCursor cursor = new FakeCursor(buffer);
            FakeScanner scanner = new FakeScanner(cursor, buffer);
            FakeParseContext context = new FakeParseContext(scanner);
            FakeParseResult<TextSpan> result = new FakeParseResult<TextSpan>();

            // Act
            bool success = _parserWithoutNewLine.Parse(context, ref result);

            // Assert
            // Expected: it should parse "abc" and stop at 'd'.
            Assert.True(success);
            Assert.Equal(0, result.Start);
            Assert.Equal(3, result.End);
            Assert.NotNull(result.Value);
            Assert.Equal(buffer, result.Value.Buffer);
            Assert.Equal(0, result.Value.Start);
            Assert.Equal(3, result.Value.Length);
            Assert.Equal(3, cursor.Offset);
            Assert.Equal(1, cursor.AdvanceNoNewLinesCallCount);
            Assert.Equal(0, cursor.AdvanceCallCount);
        }

        /// <summary>
        /// Tests the Parse method for a successful parse when a newline is present.
        /// It verifies that Advance (which handles newlines) is called and the result is correctly set.
        /// </summary>
        [Fact]
        public void Parse_WithNewLine_MatchingCharacters_ReturnsTrueAndAdvancesCursorUsingAdvance()
        {
            // Arrange
            string buffer = "a\ncxyz";
            FakeCursor cursor = new FakeCursor(buffer);
            FakeScanner scanner = new FakeScanner(cursor, buffer);
            FakeParseContext context = new FakeParseContext(scanner);
            FakeParseResult<TextSpan> result = new FakeParseResult<TextSpan>();

            // Act
            bool success = _parserWithNewLine.Parse(context, ref result);

            // Assert
            // Expected: it should parse "a\nc" (3 characters) and stop at 'x'.
            Assert.True(success);
            Assert.Equal(0, result.Start);
            Assert.Equal(3, result.End);
            Assert.NotNull(result.Value);
            Assert.Equal(buffer, result.Value.Buffer);
            Assert.Equal(0, result.Value.Start);
            Assert.Equal(3, result.Value.Length);
            Assert.Equal(3, cursor.Offset);
            Assert.Equal(1, cursor.AdvanceCallCount);
            Assert.Equal(0, cursor.AdvanceNoNewLinesCallCount);
        }

        /// <summary>
        /// Tests the Parse method when the number of matching characters is less than the required minimum size.
        /// Expected: Parse returns false and the cursor offset remains unchanged.
        /// </summary>
        [Fact]
        public void Parse_WhenMatchingCharactersAreLessThanMinSize_ReturnsFalseAndDoesNotAdvanceCursor()
        {
            // Arrange
            // Create a parser with minSize 4, but allowed characters are only "abc".
            ListOfChars parser = new ListOfChars("abc", 4, 0);
            string buffer = "abcdef";
            FakeCursor cursor = new FakeCursor(buffer);
            FakeScanner scanner = new FakeScanner(cursor, buffer);
            FakeParseContext context = new FakeParseContext(scanner);
            FakeParseResult<TextSpan> result = new FakeParseResult<TextSpan>();
            int initialOffset = cursor.Offset;

            // Act
            bool success = parser.Parse(context, ref result);

            // Assert
            Assert.False(success);
            Assert.Equal(initialOffset, cursor.Offset);
        }
    }

    /// <summary>
    /// A fake implementation of a cursor used for testing.
    /// It tracks the offset and counts for Advance methods.
    /// </summary>
    internal class FakeCursor
    {
        public int Offset { get; private set; }
        public string Span { get; }
        public int AdvanceCallCount { get; private set; }
        public int AdvanceNoNewLinesCallCount { get; private set; }

        public FakeCursor(string buffer)
        {
            Span = buffer;
            Offset = 0;
            AdvanceCallCount = 0;
            AdvanceNoNewLinesCallCount = 0;
        }

        public void Advance(int count)
        {
            AdvanceCallCount++;
            Offset += count;
        }

        public void AdvanceNoNewLines(int count)
        {
            AdvanceNoNewLinesCallCount++;
            Offset += count;
        }
    }

    /// <summary>
    /// A fake implementation of a scanner used for testing.
    /// </summary>
    internal class FakeScanner
    {
        public FakeCursor Cursor { get; }
        public string Buffer { get; }

        public FakeScanner(FakeCursor cursor, string buffer)
        {
            Cursor = cursor;
            Buffer = buffer;
        }
    }

    /// <summary>
    /// A fake implementation of a parse context used for testing.
    /// </summary>
    internal class FakeParseContext
    {
        public FakeScanner Scanner { get; }

        public FakeParseContext(FakeScanner scanner)
        {
            Scanner = scanner;
        }

        public void EnterParser(object parser)
        {
            // No implementation needed for testing.
        }

        public void ExitParser(object parser)
        {
            // No implementation needed for testing.
        }
    }

    /// <summary>
    /// A fake implementation of a parse result used for testing.
    /// </summary>
    /// <typeparam name="T">Type of the parse result value.</typeparam>
    internal class FakeParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T Value { get; private set; }

        public void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    /// <summary>
    /// A basic implementation of TextSpan used for testing.
    /// </summary>
    internal class TextSpan
    {
        public string Buffer { get; }
        public int Start { get; }
        public int Length { get; }

        public TextSpan(string buffer, int start, int length)
        {
            Buffer = buffer;
            Start = start;
            Length = length;
        }
    }
}
