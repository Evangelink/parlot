using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="PatternLiteral"/> class.
    /// </summary>
    public class PatternLiteralTests
    {
        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when a null predicate is provided.
        /// </summary>
        [Fact]
        public void Constructor_NullPredicate_ThrowsArgumentNullException()
        {
            // Arrange
            Func<char, bool> nullPredicate = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new PatternLiteral(nullPredicate));
            Assert.Equal("predicate", exception.ParamName);
        }

        /// <summary>
        /// Tests that Parse returns false when the scanner is at end-of-file.
        /// </summary>
        [Fact]
        public void Parse_WhenScannerAtEof_ReturnsFalse()
        {
            // Arrange
            var predicate = new Func<char, bool>(_ => true);
            var patternLiteral = new PatternLiteral(predicate);
            var fakeCursor = new FakeCursor(string.Empty);
            var fakeScanner = new FakeScanner(fakeCursor, string.Empty);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = patternLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.False(result.Success);
            Assert.Equal(0, fakeCursor.Offset); // Ensure cursor has not advanced.
        }

        /// <summary>
        /// Tests that Parse returns false when the first character does not satisfy the predicate.
        /// </summary>
        [Fact]
        public void Parse_WhenFirstCharDoesNotMatch_ReturnsFalse()
        {
            // Arrange
            var predicate = new Func<char, bool>(ch => ch == 'x');
            var patternLiteral = new PatternLiteral(predicate);
            var fakeCursor = new FakeCursor("abc");
            var fakeScanner = new FakeScanner(fakeCursor, "abc");
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = patternLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.False(result.Success);
            Assert.Equal(0, fakeCursor.Offset); // No advancement expected
        }

        /// <summary>
        /// Tests that Parse returns true and consumes input when input matches the predicate and meets minimum size.
        /// </summary>
        [Fact]
        public void Parse_WhenInputMatchesAndMeetsMinSize_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var predicate = new Func<char, bool>(ch => ch == 'a');
            // Set minSize = 2 to require at least two 'a's
            var patternLiteral = new PatternLiteral(predicate, minSize: 2);
            var fakeCursor = new FakeCursor("aaab");
            var fakeScanner = new FakeScanner(fakeCursor, "aaab");
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = patternLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.Success);
            // Should consume all consecutive 'a's until a non-'a' is encountered.
            Assert.Equal(3, result.End - result.Start);
            Assert.Equal("aaa", result.Value.ToString());
            Assert.Equal(3, fakeCursor.Offset);
        }

        /// <summary>
        /// Tests that Parse resets the cursor when the input does not meet the minimum size requirement.
        /// </summary>
        [Fact]
        public void Parse_WhenInputDoesNotMeetMinSize_ResetsCursorAndReturnsFalse()
        {
            // Arrange
            var predicate = new Func<char, bool>(ch => ch == 'b');
            // Set minSize = 2 but input only has one character.
            var patternLiteral = new PatternLiteral(predicate, minSize: 2);
            var fakeCursor = new FakeCursor("b");
            var fakeScanner = new FakeScanner(fakeCursor, "b");
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = patternLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.False(result.Success);
            // Cursor should be reset to starting position.
            Assert.Equal(0, fakeCursor.Offset);
        }

        /// <summary>
        /// Tests that Parse respects the maximum size limit when set.
        /// </summary>
        [Fact]
        public void Parse_WithMaxSizeLimit_ConsumesOnlyUpToMaxSize()
        {
            // Arrange
            var predicate = new Func<char, bool>(ch => ch == 'a');
            // Set maxSize = 3 so that no more than 3 characters are consumed.
            var patternLiteral = new PatternLiteral(predicate, minSize: 1, maxSize: 3);
            var fakeCursor = new FakeCursor("aaaaa");
            var fakeScanner = new FakeScanner(fakeCursor, "aaaaa");
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = patternLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.Success);
            Assert.Equal(3, result.End - result.Start);
            Assert.Equal("aaa", result.Value.ToString());
            Assert.Equal(3, fakeCursor.Offset);
        }

        /// <summary>
        /// Tests that Compile returns a valid CompilationResult with non-empty Variables and Body.
        /// </summary>
        [Fact]
        public void Compile_ReturnsCompilationResult_WithNonEmptyBodyAndVariables()
        {
            // Arrange
            var predicate = new Func<char, bool>(ch => char.IsDigit(ch));
            var patternLiteral = new PatternLiteral(predicate, minSize: 1);
            var fakeContext = new FakeCompilationContext();

            // Act
            CompilationResult compileResult = patternLiteral.Compile(fakeContext);

            // Assert
            Assert.NotNull(compileResult);
            Assert.NotEmpty(compileResult.Variables);
            Assert.NotEmpty(compileResult.Body);
        }
    }

    #region Fakes for Parse

    /// <summary>
    /// Fake implementation of a cursor used in parsing.
    /// </summary>
    internal class FakeCursor
    {
        private int _index;
        public string Buffer { get; }

        public FakeCursor(string buffer)
        {
            Buffer = buffer;
            _index = 0;
        }

        public bool Eof => _index >= Buffer.Length;

        public char Current => !Eof ? Buffer[_index] : '\0';

        public int Offset => _index;

        public TextPosition Position => new TextPosition { Offset = _index };

        public void Advance()
        {
            if (!Eof)
            {
                _index++;
            }
        }

        public void ResetPosition(TextPosition pos)
        {
            _index = pos.Offset;
        }
    }

    /// <summary>
    /// Fake implementation of a scanner that holds a cursor and a buffer.
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
    /// Fake implementation of a parse context.
    /// </summary>
    internal class FakeParseContext : ParseContext
    {
        public FakeParseContext(FakeScanner scanner)
        {
            Scanner = scanner;
        }

        public override void EnterParser(object parser)
        {
            // No operation needed for fake.
        }

        public override void ExitParser(object parser)
        {
            // No operation needed for fake.
        }
    }

    /// <summary>
    /// Fake implementation of a parse result.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FakeParseResult<T> : ParseResult<T>
    {
        public FakeParseResult()
        {
            Success = false;
        }

        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
            Success = true;
        }
    }

    /// <summary>
    /// Minimal implementation of TextPosition for testing.
    /// </summary>
    public class TextPosition
    {
        public int Offset { get; set; }
    }

    /// <summary>
    /// Minimal implementation of TextSpan for testing.
    /// </summary>
    public class TextSpan
    {
        private readonly string _buffer;
        private readonly int _start;
        private readonly int _length;

        public TextSpan(string buffer, int start, int length)
        {
            _buffer = buffer;
            _start = start;
            _length = length;
        }

        public override string ToString() => _buffer.Substring(_start, _length);
    }

    #endregion

    #region Fakes for Compilation

    /// <summary>
    /// Fake implementation of a compilation context.
    /// </summary>
    internal class FakeCompilationContext : CompilationContext
    {
        private int _nextNumber = 1;
        public override int NextNumber => _nextNumber++;

        public override bool DiscardResult { get; set; } = false;

        public override CompilationResult CreateCompilationResult<T>()
        {
            return new FakeCompilationResult();
        }

        public override Expression Position()
        {
            // Return a dummy TextPosition with Offset = 0.
            return Expression.Constant(new TextPosition { Offset = 0 });
        }

        public override Expression Eof()
        {
            return Expression.Constant(false);
        }

        public override Expression Current()
        {
            return Expression.Constant('0');
        }

        public override Expression Advance()
        {
            return Expression.Empty();
        }

        public override Expression Buffer()
        {
            return Expression.Constant("dummyBuffer");
        }

        public override Expression Offset()
        {
            return Expression.Constant(10);
        }

        public override Expression ResetPosition(ParameterExpression start)
        {
            return Expression.Empty();
        }

        public override Expression NewTextSpan(Expression buffer, Expression startOffset, Expression length)
        {
            var constructor = typeof(TextSpan).GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });
            return Expression.New(constructor, buffer, startOffset, length);
        }
    }

    /// <summary>
    /// Fake implementation of a compilation result.
    /// </summary>
    internal class FakeCompilationResult : CompilationResult
    {
        public FakeCompilationResult()
        {
            Variables = new List<ParameterExpression>();
            Body = new List<Expression>();
        }
    }

    #endregion
}
