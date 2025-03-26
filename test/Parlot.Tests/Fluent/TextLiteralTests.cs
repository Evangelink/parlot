using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="TextLiteral"/> class.
    /// </summary>
    public class TextLiteralTests
    {
        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when a null text is provided.
        /// </summary>
        [Fact]
        public void Constructor_NullText_ThrowsArgumentNullException()
        {
            // Arrange
            string text = null!;
            StringComparison comparisonType = StringComparison.Ordinal;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new TextLiteral(text, comparisonType));
            Assert.Equal("text", exception.ParamName, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Tests that the constructor with a non-empty text and StringComparison.Ordinal sets properties correctly.
        /// ExpectedChars should contain a single character.
        /// </summary>
        [Fact]
        public void Constructor_ValidTextOrdinal_SetsPropertiesCorrectly()
        {
            // Arrange
            string text = "aBc";
            StringComparison comparisonType = StringComparison.Ordinal;
            
            // Act
            var textLiteral = new TextLiteral(text, comparisonType);

            // Assert
            Assert.Equal(text, textLiteral.Text);
            Assert.True(textLiteral.CanSeek);
            // For Ordinal, ignoreCase is false so ExpectedChars should contain only the first character.
            Assert.Single(textLiteral.ExpectedChars);
            Assert.Equal(text[0], textLiteral.ExpectedChars[0]);
        }

        /// <summary>
        /// Tests that the constructor with a non-empty text and StringComparison.OrdinalIgnoreCase sets properties correctly.
        /// ExpectedChars should contain two characters with different cases.
        /// </summary>
        [Fact]
        public void Constructor_ValidTextOrdinalIgnoreCase_SetsPropertiesCorrectly()
        {
            // Arrange
            string text = "aBc";
            StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
            
            // Act
            var textLiteral = new TextLiteral(text, comparisonType);

            // Assert
            Assert.Equal(text, textLiteral.Text);
            Assert.True(textLiteral.CanSeek);
            // For ignore case comparisons, ExpectedChars should include both upper and lower case versions.
            Assert.Equal(2, textLiteral.ExpectedChars.Length);
            char expectedUpper = char.ToUpper(text[0], CultureInfo.CurrentCulture);
            char expectedLower = char.ToLower(text[0], CultureInfo.CurrentCulture);
            Assert.Contains(expectedUpper, textLiteral.ExpectedChars);
            Assert.Contains(expectedLower, textLiteral.ExpectedChars);
        }

        /// <summary>
        /// Tests that the constructor with an empty text sets CanSeek to false and ExpectedChars remains empty.
        /// </summary>
        [Fact]
        public void Constructor_EmptyText_SetsCanSeekFalseAndNoExpectedChars()
        {
            // Arrange
            string text = "";
            StringComparison comparisonType = StringComparison.Ordinal;

            // Act
            var textLiteral = new TextLiteral(text, comparisonType);

            // Assert
            Assert.Equal(text, textLiteral.Text);
            Assert.False(textLiteral.CanSeek);
            Assert.Empty(textLiteral.ExpectedChars);
        }

        /// <summary>
        /// Tests that the Parse method returns true when the input text matches exactly.
        /// </summary>
        [Fact]
        public void Parse_WhenTextMatches_ReturnsTrueAndSetsResult()
        {
            // Arrange
            string literalText = "abc";
            var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);
            var input = "abcdef";
            var fakeCursor = new FakeCursor(input);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<string>();

            // Act
            bool parseResult = textLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(0, result.Start);
            Assert.Equal(literalText.Length, result.End);
            Assert.Equal(literalText, result.Value);
        }

        /// <summary>
        /// Tests that the Parse method returns false when the input text does not match.
        /// </summary>
        [Fact]
        public void Parse_WhenTextDoesNotMatch_ReturnsFalse()
        {
            // Arrange
            string literalText = "xyz";
            var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);
            var input = "abcdef";
            var fakeCursor = new FakeCursor(input);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<string>();

            // Act
            bool parseResult = textLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            // Ensure result remains unchanged (start and end not set, value is null)
            Assert.Equal(0, result.Start);
            Assert.Equal(0, result.End);
            Assert.Null(result.Value);
        }

        /// <summary>
        /// Tests that the Compile method returns a valid compilation result with a non-empty body when DiscardResult is false.
        /// </summary>
        [Fact]
        public void Compile_WhenDiscardResultFalse_ReturnsCompilationResultWithAssignment()
        {
            // Arrange
            string literalText = "test";
            var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);
            var fakeContext = new FakeCompilationContext { DiscardResult = false, ParseContext = Expression.Constant(new object()) };

            // Act
            var compilationResult = textLiteral.Compile(fakeContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
        }

        /// <summary>
        /// Tests that the Compile method returns a valid compilation result with a non-empty body when DiscardResult is true.
        /// </summary>
        [Fact]
        public void Compile_WhenDiscardResultTrue_ReturnsCompilationResultWithNoValueAssignment()
        {
            // Arrange
            string literalText = "test";
            var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);
            var fakeContext = new FakeCompilationContext { DiscardResult = true, ParseContext = Expression.Constant(new object()) };

            // Act
            var compilationResult = textLiteral.Compile(fakeContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
        }

        /// <summary>
        /// Tests that the ToString method returns a string in the expected format.
        /// </summary>
        [Fact]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            string literalText = "sample";
            var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);

            // Act
            string toStringResult = textLiteral.ToString();

            // Assert
            Assert.Equal($"Text(\"{literalText}\")", toStringResult);
        }
    }

    #region Fake Classes for Parsing

    /// <summary>
    /// A fake implementation of a cursor used for parsing.
    /// </summary>
    internal class FakeCursor
    {
        private readonly string _input;
        public int Offset { get; private set; }

        public FakeCursor(string input)
        {
            _input = input;
            Offset = 0;
        }

        /// <summary>
        /// Simulates matching the provided span with the input starting at the current offset.
        /// </summary>
        public bool Match(ReadOnlySpan<char> text, StringComparison comparisonType)
        {
            if ((_input.Length - Offset) < text.Length)
            {
                return false;
            }
            string substring = _input.Substring(Offset, text.Length);
            return string.Equals(substring, text.ToString(), comparisonType);
        }

        /// <summary>
        /// Advances the cursor by the specified count.
        /// </summary>
        public void Advance(int count)
        {
            Offset += count;
        }

        /// <summary>
        /// Advances the cursor by the specified count without crossing new line characters.
        /// For simplicity, this implementation is the same as Advance.
        /// </summary>
        public void AdvanceNoNewLines(int count)
        {
            Advance(count);
        }
    }

    /// <summary>
    /// A fake scanner that holds a fake cursor.
    /// </summary>
    internal class FakeScanner
    {
        public FakeCursor Cursor { get; }

        public FakeScanner(FakeCursor cursor)
        {
            Cursor = cursor;
        }
    }

    /// <summary>
    /// A fake parse context for testing the Parse method.
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
            // No operation for fake context.
        }

        public void ExitParser(object parser)
        {
            // No operation for fake context.
        }
    }

    /// <summary>
    /// A fake implementation of ParseResult to capture parsing outcomes.
    /// </summary>
    /// <typeparam name="T">The type of the parsed result.</typeparam>
    internal class FakeParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T? Value { get; private set; }

        /// <summary>
        /// Simulates setting the result of a parse operation.
        /// </summary>
        public void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    #endregion

    #region Fake Classes for Compilation

    /// <summary>
    /// A fake compilation result to simulate the result of the Compile method.
    /// </summary>
    internal class FakeCompilationResult<T>
    {
        public List<Expression> Body { get; } = new List<Expression>();
        public MemberExpression Success { get; } = Expression.Variable(typeof(bool), "success");
        public Expression Value { get; } = Expression.Variable(typeof(T), "value");
    }

    /// <summary>
    /// A fake compilation context to simulate a CompilationContext required by the Compile method.
    /// </summary>
    internal class FakeCompilationContext
    {
        /// <summary>
        /// Flag indicating whether to discard the result.
        /// </summary>
        public bool DiscardResult { get; set; }

        /// <summary>
        /// Simulated ParseContext used in compilation.
        /// </summary>
        public Expression ParseContext { get; set; } = Expression.Constant(new object());

        /// <summary>
        /// Creates a fake compilation result.
        /// </summary>
        public FakeCompilationResult<T> CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }
    }

    #endregion
}
