using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="SearchValuesCharLiteral"/> class.
    /// </summary>
    public class SearchValuesCharLiteralTests
    {
        private readonly string _sampleBuffer = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Tests that the constructor with SearchValues parameter throws ArgumentNullException when a null searchValues is passed.
        /// </summary>
        [Fact]
        public void Ctor_WithNullSearchValues_ThrowsArgumentNullException()
        {
            // Arrange & Act
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new SearchValuesCharLiteral(null as SearchValues<char>, 1, 0));

            // Assert
            Assert.Equal("searchValues", exception.ParamName);
        }

        /// <summary>
        /// Tests that the constructor with ReadOnlySpan parameter initializes CanSeek to true and sets ExpectedChars correctly when minSize > 0.
        /// </summary>
        [Fact]
        public void Ctor_WithReadOnlySpanSearchValues_SetsCanSeekAndExpectedChars()
        {
            // Arrange
            ReadOnlySpan<char> span = "abc";
            
            // Act
            var parser = new SearchValuesCharLiteral(span, minSize: 1, maxSize: 0);

            // Assert
            Assert.True(parser.CanSeek);
            Assert.Equal("abc".ToCharArray(), parser.ExpectedChars);
        }

        /// <summary>
        /// Tests that Parse returns false if the input length is less than the minimum size required.
        /// </summary>
        [Fact]
        public void Parse_InputShorterThanMinSize_ReturnsFalseAndDoesNotAdvanceCursor()
        {
            // Arrange
            // Using constructor with SearchValues
            var searchValues = new SearchValues<char>(new List<char> { 'a' });
            var parser = new SearchValuesCharLiteral(searchValues, minSize: 5, maxSize: 0);

            // Input length is less than minSize (4 < 5)
            string input = "aaaa";
            var context = new DummyParseContext(input);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.Equal(0, context.Scanner.Cursor.Offset);
        }

        /// <summary>
        /// Tests that Parse returns false when the first non-matching character occurs before reaching minimum required size.
        /// </summary>
        [Fact]
        public void Parse_FirstNonMatchingBeforeMinSize_ReturnsFalseAndDoesNotAdvanceCursor()
        {
            // Arrange
            // searchValues: only 'a' is acceptable.
            var searchValues = new SearchValues<char>(new List<char> { 'a' });
            // set minSize to 3 so that at least 3 matching characters are required.
            var parser = new SearchValuesCharLiteral(searchValues, minSize: 3, maxSize: 0);

            // Input where first character is not matching: "baaa"
            string input = "baaa";
            var context = new DummyParseContext(input);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.Equal(0, context.Scanner.Cursor.Offset);
        }

        /// <summary>
        /// Tests that Parse returns true and consumes the entire input when the entire input matches the search values.
        /// </summary>
        [Fact]
        public void Parse_EntireInputMatches_ReturnsTrueAndConsumesWholeInput()
        {
            // Arrange
            var searchValues = new SearchValues<char>(new List<char> { 'a' });
            var parser = new SearchValuesCharLiteral(searchValues, minSize: 1, maxSize: 0);

            string input = "aaaaa";
            var context = new DummyParseContext(input);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            // Since entire input matches, the cursor should have advanced by input.Length.
            Assert.Equal(input.Length, context.Scanner.Cursor.Offset);
            Assert.Equal(0, result.Start);
            Assert.Equal(input.Length, result.End);
            Assert.Equal(input, result.Value.Content);
        }

        /// <summary>
        /// Tests that Parse returns true and correctly limits the consumed characters to maxSize when size exceeds maxSize.
        /// </summary>
        [Fact]
        public void Parse_MatchExceedsMaxSize_ConsumesOnlyMaxSizeCharacters()
        {
            // Arrange
            // searchValues: only 'a' is acceptable.
            var searchValues = new SearchValues<char>(new List<char> { 'a' });
            // Set maxSize to 3; even though there may be more matching characters.
            var parser = new SearchValuesCharLiteral(searchValues, minSize: 1, maxSize: 3);

            // Input: 5 'a's followed by a non-matching character.
            string input = "aaaaX";
            var context = new DummyParseContext(input);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            // Should only consume maxSize (3) characters.
            Assert.Equal(3, context.Scanner.Cursor.Offset);
            Assert.Equal(0, result.Start);
            Assert.Equal(3, result.End);
            Assert.Equal("aaa", result.Value.Content);
        }

        /// <summary>
        /// Tests that Parse returns true and consumes the correct number of characters when a non-matching character is encountered after minimum size.
        /// </summary>
        [Fact]
        public void Parse_NonMatchingCharacterAfterMinSize_ConsumesUntilNonMatchingCharacter()
        {
            // Arrange
            // searchValues: only 'a' is acceptable.
            var searchValues = new SearchValues<char>(new List<char> { 'a' });
            var parser = new SearchValuesCharLiteral(searchValues, minSize: 1, maxSize: 0);

            // Input: 3 'a's then a 'b' then more characters.
            string input = "aaabccc";
            var context = new DummyParseContext(input);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            // Should consume characters until the first non-matching char, which is at index 3.
            Assert.Equal(3, context.Scanner.Cursor.Offset);
            Assert.Equal(0, result.Start);
            Assert.Equal(3, result.End);
            Assert.Equal("aaa", result.Value.Content);
        }

        /// <summary>
        /// Tests that ToString returns the expected string representation.
        /// </summary>
        [Fact]
        public void ToString_ReturnsExpectedString()
        {
            // Arrange
            ReadOnlySpan<char> span = "xyz";
            var parser = new SearchValuesCharLiteral(span, minSize: 1, maxSize: 0);

            // Act
            string toStringResult = parser.ToString();

            // Assert
            Assert.Contains("AnyOf", toStringResult);
            // Since our SearchValues.ToString returns "xyz", ensure it's contained.
            Assert.Contains("xyz", toStringResult);
        }
    }

    #region Dummy and Helper Classes

    /// <summary>
    /// Dummy implementation of TextSpan to simulate parsed text.
    /// </summary>
    public class TextSpan
    {
        /// <summary>
        /// Gets the underlying buffer.
        /// </summary>
        public string Buffer { get; }

        /// <summary>
        /// Gets the start index of the span.
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// Gets the length of the span.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the content of the span.
        /// </summary>
        public string Content => Buffer.Substring(Start, Length);

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSpan"/> class.
        /// </summary>
        public TextSpan(string buffer, int start, int length)
        {
            Buffer = buffer;
            Start = start;
            Length = length;
        }
    }

    /// <summary>
    /// Dummy implementation of a parse result.
    /// </summary>
    /// <typeparam name="T">Type of the parsed value.</typeparam>
    public class DummyParseResult<T>
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
    /// Dummy implementation of a parse context.
    /// </summary>
    public class DummyParseContext
    {
        public DummyScanner Scanner { get; }
        
        public DummyParseContext(string input)
        {
            Scanner = new DummyScanner(input);
        }

        public void EnterParser(object parser)
        {
            // No-op for dummy context.
        }

        public void ExitParser(object parser)
        {
            // No-op for dummy context.
        }
    }

    /// <summary>
    /// Dummy implementation of a scanner.
    /// </summary>
    public class DummyScanner
    {
        public DummyCursor Cursor { get; }
        public string Buffer { get; }

        public DummyScanner(string buffer)
        {
            Buffer = buffer;
            Cursor = new DummyCursor(buffer);
        }
    }

    /// <summary>
    /// Dummy implementation of a cursor.
    /// </summary>
    public class DummyCursor
    {
        private readonly string _buffer;
        public int Position { get; private set; }

        public DummyCursor(string buffer)
        {
            _buffer = buffer;
            Position = 0;
        }

        public ReadOnlySpan<char> Span => _buffer.AsSpan(Position);
        public int Offset => Position;

        public void Advance(int size)
        {
            Position += size;
        }
    }

    /// <summary>
    /// Minimal implementation of SearchValues for char.
    /// </summary>
    /// <typeparam name="T">Type parameter.</typeparam>
    public class SearchValues<T>
    {
        private readonly List<T> _values;
        public SearchValues(IEnumerable<T> values)
        {
            _values = values.ToList();
        }

        /// <summary>
        /// Determines whether the specified value is contained in the search values.
        /// </summary>
        public bool Contains(T value)
        {
            return _values.Contains(value);
        }

        /// <summary>
        /// Creates a SearchValues instance from a ReadOnlySpan.
        /// </summary>
        public static SearchValues<char> Create(ReadOnlySpan<char> values)
        {
            return new SearchValues<char>(values.ToArray());
        }

        public override string ToString()
        {
            return new string(_values.Cast<char>().ToArray());
        }
    }

    /// <summary>
    /// Extensions to simulate the IndexOfAnyExcept functionality.
    /// </summary>
    public static class SpanExtensions
    {
        /// <summary>
        /// Returns the index of the first element in the span that is not contained in the search values.
        /// Returns -1 if all elements are contained.
        /// </summary>
        public static int IndexOfAnyExcept(this ReadOnlySpan<char> span, SearchValues<char> searchValues)
        {
            for (int i = 0; i < span.Length; i++)
            {
                if (!searchValues.Contains(span[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }

    #endregion
}
