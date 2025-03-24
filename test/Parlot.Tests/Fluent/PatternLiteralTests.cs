using System.Collections.Generic;
using System.Linq.Expressions;
using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="PatternLiteral"/> class.
    /// </summary>
    public class PatternLiteralTests
    {
        private readonly Func<char, bool> _alwaysTruePredicate = c => true;
        private readonly Func<char, bool> _isA_Predicate = c => c == 'a';

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when a null predicate is provided.
        /// </summary>
        [Fact]
        public void Constructor_NullPredicate_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PatternLiteral(null, 1, 0));
        }

        /// <summary>
        /// Tests that Parse returns false when the input is empty (EOF) or the first character does not match the predicate.
        /// </summary>
        [Fact]
        public void Parse_InputDoesNotMatch_ReturnsFalse()
        {
            // Arrange
            var parser = new PatternLiteral(_isA_Predicate, 1, 0);
            var input = "bcd";
            var cursor = new DummyCursor(input);
            var scanner = new DummyScanner(input, cursor);
            var context = new DummyParseContext(scanner);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.False(result.Success);
            Assert.Equal(0, cursor.Position.Offset);
        }

        /// <summary>
        /// Tests that Parse returns true and sets the correct TextSpan when input characters match the predicate.
        /// </summary>
        [Fact]
        public void Parse_InputMatches_ReturnsTrueWithTextSpan()
        {
            // Arrange
            var parser = new PatternLiteral(_isA_Predicate, 1, 0);
            var input = "aaab";
            var cursor = new DummyCursor(input);
            var scanner = new DummyScanner(input, cursor);
            var context = new DummyParseContext(scanner);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.Success);
            // Expected matched span is "aaa"
            Assert.Equal("aaa", result.Value.Content);
            // Cursor should have advanced to the first non-matching character ('b')
            Assert.Equal(3, cursor.Position.Offset);
        }

        /// <summary>
        /// Tests that Parse returns false and resets the cursor when the number of matched characters is less than the minimum required.
        /// </summary>
        [Fact]
        public void Parse_InputTooShortForMinSize_ReturnsFalseAndResetsCursor()
        {
            // Arrange
            var parser = new PatternLiteral(_isA_Predicate, 3, 0);
            var input = "aa"; // Only 2 characters, but minimum required is 3.
            var cursor = new DummyCursor(input);
            var scanner = new DummyScanner(input, cursor);
            var context = new DummyParseContext(scanner);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.False(result.Success);
            // Cursor should be reset to initial position.
            Assert.Equal(0, cursor.Position.Offset);
        }

        /// <summary>
        /// Tests that Parse respects the maxSize constraint and stops parsing after reaching maxSize.
        /// </summary>
        [Fact]
        public void Parse_WithMaxSizeStopsAfterMaxReached()
        {
            // Arrange
            var parser = new PatternLiteral(_isA_Predicate, 1, 2);
            var input = "aaaa";
            var cursor = new DummyCursor(input);
            var scanner = new DummyScanner(input, cursor);
            var context = new DummyParseContext(scanner);
            var result = new DummyParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.Success);
            // Expected matched span is only 2 characters due to maxSize constraint.
            Assert.Equal("aa", result.Value.Content);
            Assert.Equal(2, cursor.Position.Offset);
        }

        /// <summary>
        /// Tests that Compile returns a valid compilation result containing expected variables and expression body.
        /// </summary>
        [Fact]
        public void Compile_ReturnsValidCompilationResult()
        {
            // Arrange
            var parser = new PatternLiteral(_isA_Predicate, 1, 3);
            var dummyContext = new DummyCompilationContext();

            // Act
            var compilationResult = parser.Compile(dummyContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Variables);
            Assert.NotEmpty(compilationResult.Body);
            // Verify that the compilation body contains assignment to result.Success.
            bool hasSuccessAssignment = false;
            foreach (var expr in compilationResult.Body)
            {
                if (expr.ToString().Contains("result.Success"))
                {
                    hasSuccessAssignment = true;
                    break;
                }
            }
            Assert.True(hasSuccessAssignment);
        }

        #region Dummy Classes for Parse Testing

        /// <summary>
        /// Minimal implementation of TextPosition.
        /// </summary>
        public class TextPosition
        {
            public int Offset { get; set; }
            public TextPosition(int offset)
            {
                Offset = offset;
            }
        }

        /// <summary>
        /// Minimal implementation of TextSpan.
        /// </summary>
        public class TextSpan
        {
            public string Buffer { get; }
            public int Start { get; }
            public int Length { get; }
            public string Content => Buffer.Substring(Start, Length);

            public TextSpan(string buffer, int start, int length)
            {
                Buffer = buffer;
                Start = start;
                Length = length;
            }
        }

        /// <summary>
        /// Minimal implementation of a cursor for scanning input.
        /// </summary>
        public class DummyCursor
        {
            private readonly string _input;
            private int _position;

            public DummyCursor(string input)
            {
                _input = input;
                _position = 0;
            }

            public char Current => _position < _input.Length ? _input[_position] : '\0';

            public bool Eof => _position >= _input.Length;

            public int Offset => _position;

            public TextPosition Position => new TextPosition(_position);

            public void Advance()
            {
                if (!Eof)
                {
                    _position++;
                }
            }

            public void ResetPosition(TextPosition position)
            {
                _position = position.Offset;
            }
        }

        /// <summary>
        /// Minimal implementation of a scanner wrapping the input buffer and cursor.
        /// </summary>
        public class DummyScanner
        {
            public string Buffer { get; }
            public DummyCursor Cursor { get; }
            public DummyScanner(string buffer, DummyCursor cursor)
            {
                Buffer = buffer;
                Cursor = cursor;
            }
        }

        /// <summary>
        /// Minimal implementation of a parse context.
        /// </summary>
        public class DummyParseContext
        {
            public DummyScanner Scanner { get; }
            public DummyParseContext(DummyScanner scanner)
            {
                Scanner = scanner;
            }
            public void EnterParser(object parser) { }
            public void ExitParser(object parser) { }
        }

        /// <summary>
        /// Minimal implementation of a parse result.
        /// </summary>
        public class DummyParseResult<T>
        {
            public bool Success { get; private set; }
            public T Value { get; private set; }
            public void Set(int start, int end, T value)
            {
                Success = true;
                Value = value;
            }
        }

        #endregion

        #region Dummy Classes for Compilation Testing

        /// <summary>
        /// Minimal implementation of a compilation result.
        /// </summary>
        public class DummyCompilationResult<T>
        {
            public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
            public List<Expression> Body { get; } = new List<Expression>();
            public Expression Value { get; set; }
            public Expression Success { get; set; }
        }

        /// <summary>
        /// Dummy implementation of a compilation context to support the Compile method.
        /// </summary>
        public class DummyCompilationContext
        {
            private int _counter = 1;
            public int NextNumber => _counter++;

            public bool DiscardResult { get; set; } = false;

            public DummyCompilationResult<T> CreateCompilationResult<T>()
            {
                return new DummyCompilationResult<T>();
            }

            public Expression Position()
            {
                // Returns a dummy TextPosition instance.
                return Expression.Constant(new TextPosition(0));
            }

            public Expression Eof()
            {
                return Expression.Constant(false);
            }

            public Expression Current()
            {
                return Expression.Constant('a');
            }

            public Expression Advance()
            {
                return Expression.Empty();
            }

            public Expression ResetPosition(ParameterExpression start)
            {
                return Expression.Empty();
            }

            public Expression Buffer()
            {
                return Expression.Constant("dummy buffer");
            }

            public Expression Offset()
            {
                return Expression.Constant(1);
            }

            public Expression NewTextSpan(Expression buffer, Expression startOffset, Expression length)
            {
                // Returns a dummy TextSpan instance.
                return Expression.Constant(new TextSpan("dummy buffer", 0, 1));
            }
        }

        #endregion
    }
}
