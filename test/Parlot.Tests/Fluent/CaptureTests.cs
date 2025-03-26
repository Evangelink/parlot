using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Capture{T}"/> class.
    /// </summary>
    public class CaptureTests
    {
        private readonly Mock<Parser<int>> _mockParser;

        public CaptureTests()
        {
            _mockParser = new Mock<Parser<int>>(MockBehavior.Strict);
        }

        /// <summary>
        /// Tests the constructor and ToString method to ensure that the internal parser is correctly assigned.
        /// </summary>
        [Fact]
        public void ToString_WhenCalled_ReturnsParserToStringWithCaptureSuffix()
        {
            // Arrange
            string mockParserToString = "MockParser";
            _mockParser.Setup(x => x.ToString()).Returns(mockParserToString);
            var capture = new Capture<int>(_mockParser.Object);

            // Act
            var result = capture.ToString();

            // Assert
            Assert.Equal($"{mockParserToString} (Capture)", result);
            _mockParser.Verify(x => x.ToString(), Times.Once);
        }

        /// <summary>
        /// Tests the Parse method when the underlying parser succeeds.
        /// Verifies that the method returns true, sets the ParseResult correctly, and calls EnterParser/ExitParser.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserSucceeds_ReturnsTrueAndSetsTextSpan()
        {
            // Arrange
            // Set up fake scanner and parse context with predetermined cursor positions.
            int startOffset = 5;
            int endOffset = 10;
            string buffer = "abcdefghij";
            var fakeContext = new FakeParseContext(buffer, startOffset, endOffset);
            var parseResult = new FakeParseResult<TextSpan>();
            // Setup the underlying parser's Parse to return true.
            _mockParser.Setup(x => x.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny))
                       .Callback<ParseContext, ParseResult<int>>((ctx, refResult) =>
                       {
                           // Simulate successful parse by doing nothing; value is not used.
                       })
                       .Returns(true);

            var capture = new Capture<int>(_mockParser.Object);

            // Act
            bool result = capture.Parse(fakeContext, ref parseResult);

            // Assert
            Assert.True(result);
            // Expected: result.Set is called with start.Offset and end offset.
            Assert.Equal(startOffset, parseResult.Start);
            Assert.Equal(endOffset, parseResult.End);
            // TextSpan should have been constructed with the buffer, starting at startOffset, with length = endOffset - startOffset.
            Assert.NotNull(parseResult.Value);
            Assert.Equal(buffer, parseResult.Value.Buffer);
            Assert.Equal(startOffset, parseResult.Value.Offset);
            Assert.Equal(endOffset - startOffset, parseResult.Value.Length);
            // Verify that EnterParser and ExitParser were called.
            Assert.Contains(capture, fakeContext.EnteredParsers);
            Assert.Contains(capture, fakeContext.ExitedParsers);
            _mockParser.Verify(x => x.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny), Times.Once);
        }

        /// <summary>
        /// Tests the Parse method when the underlying parser fails.
        /// Verifies that the method returns false and calls EnterParser/ExitParser without modifying the parse result.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserFails_ReturnsFalse()
        {
            // Arrange
            int startOffset = 0;
            int endOffset = 0;
            string buffer = "test";
            var fakeContext = new FakeParseContext(buffer, startOffset, endOffset);
            var parseResult = new FakeParseResult<TextSpan>();
            // Setup the underlying parser's Parse to return false.
            _mockParser.Setup(x => x.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny))
                       .Returns(false);

            var capture = new Capture<int>(_mockParser.Object);

            // Act
            bool result = capture.Parse(fakeContext, ref parseResult);

            // Assert
            Assert.False(result);
            // Expect that the result was not set.
            Assert.Equal(0, parseResult.Start);
            Assert.Equal(0, parseResult.End);
            Assert.Null(parseResult.Value);
            // Verify that EnterParser and ExitParser were called.
            Assert.Contains(capture, fakeContext.EnteredParsers);
            Assert.Contains(capture, fakeContext.ExitedParsers);
            _mockParser.Verify(x => x.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny), Times.Once);
        }

        /// <summary>
        /// Tests the Compile method to ensure that it returns a valid CompilationResult with a non-empty Body.
        /// </summary>
        [Fact]
        public void Compile_WhenCalled_ReturnsCompilationResultWithBodyExpressions()
        {
            // Arrange
            var fakeCompilationContext = new FakeCompilationContext();
            // Setup a fake parser compile result.
            var fakeParserCompileResult = new FakeParserCompileResult
            {
                Variables = new List<ParameterExpression>(),
                Body = Expression.Empty(),
                Success = Expression.Constant(true)
            };
            _mockParser.Setup(x => x.Build(It.IsAny<CompilationContext>())).Returns(fakeParserCompileResult);
            var capture = new Capture<int>(_mockParser.Object);

            // Act
            CompilationResult result = capture.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(result);
            // Since the body is added to the CompilationResult, verify that the Body collection is not empty.
            var fakeResult = result as FakeCompilationResult<TextSpan>;
            Assert.NotNull(fakeResult);
            Assert.NotEmpty(fakeResult.Body);
            _mockParser.Verify(x => x.Build(It.IsAny<CompilationContext>()), Times.Once);
        }
    }

    #region Fake Classes for Parsing

    // Fake implementation of ParseContext for testing purposes.
    internal class FakeParseContext : ParseContext
    {
        public FakeScanner Scanner { get; set; }
        public List<object> EnteredParsers { get; } = new List<object>();
        public List<object> ExitedParsers { get; } = new List<object>();

        public FakeParseContext(string buffer, int startOffset, int currentOffset)
        {
            Scanner = new FakeScanner
            {
                Buffer = buffer,
                Cursor = new FakeCursor
                {
                    Position = new FakePosition { Offset = startOffset },
                    Offset = currentOffset
                }
            };
        }

        public override void EnterParser(object parser)
        {
            EnteredParsers.Add(parser);
        }

        public override void ExitParser(object parser)
        {
            ExitedParsers.Add(parser);
        }
    }

    // Fake implementation of ParseResult<T> for testing purposes.
    internal class FakeParseResult<T> : ParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T Value { get; private set; }

        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    // Fake implementations for Scanner, Cursor, and Position.
    internal class FakeScanner
    {
        public FakeCursor Cursor { get; set; }
        public string Buffer { get; set; }
    }

    internal class FakeCursor
    {
        public FakePosition Position { get; set; }
        public int Offset { get; set; }
    }

    internal class FakePosition
    {
        public int Offset { get; set; }
    }

    #endregion

    #region Fake Classes for Compilation

    // Fake implementation of CompilationContext for testing purposes.
    internal class FakeCompilationContext : CompilationContext
    {
        public override bool DiscardResult { get; set; }
        public override int NextNumber { get; set; } = 1;

        public override CompilationResult CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }

        public override Expression DeclarePositionVariable(CompilationResult result)
        {
            return Expression.Parameter(typeof(object), "start");
        }

        public override Expression Offset(Expression expr)
        {
            return Expression.Constant(0);
        }

        public override Expression Buffer()
        {
            return Expression.Constant("buffer");
        }

        public override Expression NewTextSpan(Expression buffer, Expression startOffset, Expression length)
        {
            // Return a constant dummy TextSpan for testing purposes.
            return Expression.Constant(new DummyTextSpan("buffer", 0, 0));
        }
    }

    // Fake implementation of CompilationResult for testing purposes.
    internal class FakeCompilationResult<T> : CompilationResult
    {
        public List<Expression> _body = new List<Expression>();
        public override IList<Expression> Body => _body;
        public override Expression Value { get; set; }
        public override Expression Success { get; set; }

        public override ParameterExpression DeclareVariable<U>(string name, Expression value)
        {
            return Expression.Parameter(typeof(U), name);
        }
    }

    // Dummy TextSpan that calls base constructor.
    internal class DummyTextSpan : TextSpan
    {
        public DummyTextSpan(string buffer, int offset, int length)
            : base(buffer, offset, length)
        {
        }
    }

    // Fake representation of the parser compile result returned by Build.
    internal class FakeParserCompileResult
    {
        public IEnumerable<ParameterExpression> Variables { get; set; }
        public Expression Body { get; set; }
        public Expression Success { get; set; }
    }

    #endregion
}
