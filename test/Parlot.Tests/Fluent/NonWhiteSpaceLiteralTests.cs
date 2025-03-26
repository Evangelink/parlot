using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="NonWhiteSpaceLiteral"/> class.
    /// </summary>
    public class NonWhiteSpaceLiteralTests
    {
        private readonly string _sampleText = "abc def";
        private readonly string _sampleTextWithNewLine = "\nabc def";
        private readonly string _whitespaceText = "   ";

        #region Parse Tests

        /// <summary>
        /// Tests that Parse returns false when the scanner is at EOF.
        /// </summary>
        [Fact]
        public void Parse_WhenScannerAtEof_ReturnsFalse()
        {
            // Arrange
            var context = new FakeParseContext(string.Empty);
            var parser = new NonWhiteSpaceLiteral();
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
        }

        /// <summary>
        /// Tests that Parse returns false when no non-whitespace characters are present (for includeNewLines = false).
        /// </summary>
        [Fact]
        public void Parse_WhenNoNonWhiteSpaceRead_ReturnsFalse_ForIncludeNewLinesFalse()
        {
            // Arrange
            var context = new FakeParseContext(_whitespaceText);
            var parser = new NonWhiteSpaceLiteral(includeNewLines: false);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
        }

        /// <summary>
        /// Tests that Parse successfully reads a literal when non-whitespace characters are present (includeNewLines = true).
        /// </summary>
        [Fact]
        public void Parse_WhenNonWhiteSpaceRead_ReturnsTrue_ForIncludeNewLinesTrue()
        {
            // Arrange
            var context = new FakeParseContext(_sampleText);
            var parser = new NonWhiteSpaceLiteral(includeNewLines: true);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            // Expect that "abc" is read until the space.
            Assert.Equal(0, result.Start);
            Assert.Equal(3, result.End);
            Assert.Equal("abc", result.Value.ToString());
        }

        /// <summary>
        /// Tests that Parse successfully reads a literal including newline characters when includeNewLines is true.
        /// </summary>
        [Fact]
        public void Parse_WhenNewLineIsPresent_IncludesNewLine_ForIncludeNewLinesTrue()
        {
            // Arrange
            var context = new FakeParseContext(_sampleTextWithNewLine);
            var parser = new NonWhiteSpaceLiteral(includeNewLines: true);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            // In our fake scanner, ReadNonWhiteSpaceOrNewLine consumes all characters
            // until encountering a whitespace that is not a newline.
            // For input "\nabc def", it will consume "\nabc" because space is encountered at index 4.
            Assert.True(parseResult);
            Assert.Equal(0, result.Start);
            Assert.Equal(4, result.End);
            Assert.Equal("\nabc", result.Value.ToString());
        }

        /// <summary>
        /// Tests that Parse returns false when no literal can be read (includeNewLines = false with whitespace at start).
        /// </summary>
        [Fact]
        public void Parse_WhenStartingWithWhitespace_ReturnsFalse_ForIncludeNewLinesFalse()
        {
            // Arrange
            var context = new FakeParseContext(" def");
            var parser = new NonWhiteSpaceLiteral(includeNewLines: false);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
        }
        #endregion

        #region Compile Tests

        /// <summary>
        /// Tests that Compile returns a CompilationResult with a non-empty body expression for includeNewLines = true.
        /// </summary>
        [Fact]
        public void Compile_ForIncludeNewLinesTrue_ReturnsCompilationResult_WithExpectedBody()
        {
            // Arrange
            var parser = new NonWhiteSpaceLiteral(includeNewLines: true);
            var context = new FakeCompilationContext { DiscardResult = false };

            // Act
            var compilationResult = parser.Compile(context);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotNull(compilationResult.Body);
            Assert.NotEmpty(compilationResult.Body);
            // Check that there is an IfThen expression in the body.
            bool containsIfThen = false;
            foreach (var expr in compilationResult.Body)
            {
                if (expr.NodeType == ExpressionType.IfThen)
                {
                    containsIfThen = true;
                    break;
                }
            }
            Assert.True(containsIfThen, "CompilationResult.Body should contain an IfThen expression.");
        }

        /// <summary>
        /// Tests that Compile returns a CompilationResult with a non-empty body expression for includeNewLines = false.
        /// </summary>
        [Fact]
        public void Compile_ForIncludeNewLinesFalse_ReturnsCompilationResult_WithExpectedBody()
        {
            // Arrange
            var parser = new NonWhiteSpaceLiteral(includeNewLines: false);
            var context = new FakeCompilationContext { DiscardResult = true };

            // Act
            var compilationResult = parser.Compile(context);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotNull(compilationResult.Body);
            Assert.NotEmpty(compilationResult.Body);
            // Check that the Success and Value parameters are set in the result.
            Assert.NotNull(compilationResult.Success);
            Assert.NotNull(compilationResult.Value);
        }
        #endregion

        #region Fake Implementations for Parse Tests

        /// <summary>
        /// A fake implementation of a parse context for testing purposes.
        /// </summary>
        private class FakeParseContext
        {
            public FakeScanner Scanner { get; }

            public FakeParseContext(string buffer)
            {
                Scanner = new FakeScanner(buffer);
            }

            public void EnterParser(object parser)
            {
                // No-op for testing.
            }

            public void ExitParser(object parser)
            {
                // No-op for testing.
            }
        }

        /// <summary>
        /// A fake implementation of a scanner for testing the Parse method.
        /// </summary>
        private class FakeScanner
        {
            public string Buffer { get; }
            public int Offset { get; set; }

            public FakeScanner(string buffer)
            {
                Buffer = buffer;
                Offset = 0;
            }

            public FakeCursor Cursor => new FakeCursor(Buffer.Length, Offset);

            public void ReadNonWhiteSpace()
            {
                while (Offset < Buffer.Length && !char.IsWhiteSpace(Buffer[Offset]))
                {
                    Offset++;
                }
            }

            public void ReadNonWhiteSpaceOrNewLine()
            {
                while (Offset < Buffer.Length)
                {
                    char c = Buffer[Offset];
                    // If the character is white space but not a newline, break.
                    if (char.IsWhiteSpace(c) && c != '\n' && c != '\r')
                    {
                        break;
                    }
                    Offset++;
                }
            }
        }

        /// <summary>
        /// A fake implementation of a cursor providing position information.
        /// </summary>
        private class FakeCursor
        {
            public int Offset { get; }
            public int BufferLength { get; }

            public FakeCursor(int bufferLength, int offset)
            {
                BufferLength = bufferLength;
                Offset = offset;
            }

            public bool Eof => Offset >= BufferLength;
        }

        /// <summary>
        /// A fake implementation of ParseResult for testing purposes.
        /// </summary>
        /// <typeparam name="T">The type of the parsed value.</typeparam>
        private class FakeParseResult<T>
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
        #endregion

        #region Fake Implementations for Compile Tests

        /// <summary>
        /// A fake implementation of CompilationContext for testing the Compile method.
        /// </summary>
        private class FakeCompilationContext
        {
            public bool DiscardResult { get; set; }

            public Expression Eof()
            {
                return Expression.Constant(false);
            }

            public Expression Offset()
            {
                return Expression.Constant(0);
            }

            public Expression ReadNonWhiteSpace()
            {
                return Expression.Empty();
            }

            public Expression ReadNonWhiteSpaceOrNewLine()
            {
                return Expression.Empty();
            }

            public Expression Buffer()
            {
                return Expression.Constant("test buffer");
            }

            public Expression NewTextSpan(Expression buffer, Expression start, Expression length)
            {
                var method = typeof(FakeCompilationContext).GetMethod(nameof(FakeNewTextSpan));
                return Expression.Call(method, buffer, start, length);
            }

            public static TextSpan FakeNewTextSpan(string buffer, int start, int length)
            {
                return new TextSpan(buffer, start, length);
            }

            public FakeCompilationResult<T> CreateCompilationResult<T>()
            {
                return new FakeCompilationResult<T>();
            }
        }

        /// <summary>
        /// A fake implementation of CompilationResult for testing purposes.
        /// </summary>
        /// <typeparam name="T">The type of the compiled value.</typeparam>
        private class FakeCompilationResult<T>
        {
            public List<Expression> Body { get; } = new List<Expression>();
            public ParameterExpression Success { get; } = Expression.Parameter(typeof(bool), "success");
            public ParameterExpression Value { get; } = Expression.Parameter(typeof(TextSpan), "value");
        }
        #endregion
    }

    /// <summary>
    /// A minimal implementation of TextSpan for testing purposes.
    /// </summary>
    public class TextSpan
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

        public override string ToString()
        {
            if (Buffer == null)
            {
                return string.Empty;
            }
            if (Start < 0 || Start > Buffer.Length)
            {
                return string.Empty;
            }
            if (Start + Length > Buffer.Length)
            {
                return Buffer.Substring(Start);
            }
            return Buffer.Substring(Start, Length);
        }
    }
}
