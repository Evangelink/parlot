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
        private readonly string _dummyBuffer = "dummy buffer";

        /// <summary>
        /// Tests that Parse returns true and properly sets the result when the sub-parser succeeds.
        /// </summary>
        [Fact]
        public void Parse_WhenSubParserSucceeds_SetsResultAndReturnsTrue()
        {
            // Arrange
            // Create a fake sub-parser that always returns true and simulates advancing the scanner offset.
            var fakeSubParser = new Mock<Parser<int>>(MockBehavior.Strict);
            // Setup the Parse method to update the context's cursor offset.
            fakeSubParser
                .Setup(p => p.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny))
                .Callback((ParseContext ctx, ref ParseResult<int> res) =>
                {
                    // Simulate sub-parser consumption by advancing the cursor's offset.
                    ctx.Scanner.Cursor.Offset = ctx.Scanner.Cursor.Position.Offset + 10;
                })
                .Returns(true);

            var captureParser = new Capture<int>(fakeSubParser.Object);

            // Create a fake parse context with an initial cursor position at offset 0.
            var fakeContext = new FakeParseContext(_dummyBuffer, startOffset: 0);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = captureParser.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            // The start offset should be 0 and the end offset should be 10 as simulated.
            Assert.Equal(0, result.Start);
            Assert.Equal(10, result.End);
            Assert.NotNull(result.Value);
            Assert.Equal(_dummyBuffer, result.Value.Buffer);
            Assert.Equal(0, result.Value.Offset);
            Assert.Equal(10, result.Value.Length);
        }

        /// <summary>
        /// Tests that Parse returns false and does not set the result when the sub-parser fails.
        /// </summary>
        [Fact]
        public void Parse_WhenSubParserFails_ReturnsFalse()
        {
            // Arrange
            var fakeSubParser = new Mock<Parser<int>>(MockBehavior.Strict);
            // Setup the Parse method to return false without advancing the offset.
            fakeSubParser
                .Setup(p => p.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny))
                .Returns(false);

            var captureParser = new Capture<int>(fakeSubParser.Object);

            var fakeContext = new FakeParseContext(_dummyBuffer, startOffset: 0);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = captureParser.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            // When parse fails, the result should not have been set.
            Assert.Equal(0, result.Start);
            Assert.Equal(0, result.End);
            Assert.Null(result.Value);
        }

        /// <summary>
        /// Tests that ToString returns the sub-parser's ToString with the Capture suffix.
        /// </summary>
        [Fact]
        public void ToString_ReturnsSubParserToStringWithCaptureSuffix()
        {
            // Arrange
            var fakeSubParser = new Mock<Parser<int>>(MockBehavior.Strict);
            fakeSubParser.Setup(p => p.ToString()).Returns("FakeParser");
            var captureParser = new Capture<int>(fakeSubParser.Object);

            // Act
            string description = captureParser.ToString();

            // Assert
            Assert.Equal("FakeParser (Capture)", description);
        }

        /// <summary>
        /// Tests that Compile method returns a compilation result with an expected body expression.
        /// </summary>
        [Fact]
        public void Compile_WhenCalled_ReturnsCompilationResultWithConditionalBlock()
        {
            // Arrange
            // Create a fake parser compile result to be returned by the sub-parser's Build method.
            var fakeParserCompileResult = new FakeParserCompileResult
            {
                Variables = new List<ParameterExpression>(),
                Body = Expression.Block(Expression.Constant(1)),
                Success = Expression.Constant(true)
            };

            // Set up mock for the sub-parser Build method.
            var fakeSubParser = new Mock<Parser<int>>(MockBehavior.Strict);
            fakeSubParser
                .Setup(p => p.Build(It.IsAny<CompilationContext>()))
                .Returns(fakeParserCompileResult);

            var captureParser = new Capture<int>(fakeSubParser.Object);

            // Create a fake compilation context.
            var fakeCompilationContext = new FakeCompilationContext(_dummyBuffer);

            // Act
            CompilationResult compileResult = captureParser.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(compileResult);
            Assert.NotNull(compileResult.Body);
            // Check that there is at least one IfThen block in the expression tree.
            bool containsIfThen = false;
            foreach (var expr in compileResult.Body)
            {
                if (ContainsIfThen(expr))
                {
                    containsIfThen = true;
                    break;
                }
            }
            Assert.True(containsIfThen, "Compilation result body does not contain the expected conditional block.");
        }

        /// <summary>
        /// Recursively checks if an expression contains an IfThen expression.
        /// </summary>
        /// <param name="expr">The expression to inspect.</param>
        /// <returns>True if an IfThen expression is found; otherwise false.</returns>
        private bool ContainsIfThen(Expression expr)
        {
            if (expr is ConditionalExpression)
            {
                return true;
            }
            if (expr is BlockExpression block)
            {
                foreach (var inner in block.Expressions)
                {
                    if (ContainsIfThen(inner))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    #region Fake and Helper Classes for Parse

    // Minimal fake implementation for ParseContext and its related components.
    internal class FakeParseContext : ParseContext
    {
        public FakeParseContext(string buffer, int startOffset)
        {
            Scanner = new FakeScanner(buffer, startOffset);
        }

        public override void EnterParser(object parser)
        {
            // No-op for fake context.
        }

        public override void ExitParser(object parser)
        {
            // No-op for fake context.
        }
    }

    internal class FakeScanner
    {
        public FakeScanner(string buffer, int startOffset)
        {
            Buffer = buffer;
            Cursor = new FakeCursor(startOffset);
        }

        public string Buffer { get; }
        public FakeCursor Cursor { get; }
    }

    internal class FakeCursor
    {
        public FakeCursor(int offset)
        {
            Position = new FakeTextPosition(offset);
            Offset = offset;
        }

        public FakeTextPosition Position { get; set; }
        public int Offset { get; set; }
    }

    internal class FakeTextPosition
    {
        public FakeTextPosition(int offset)
        {
            Offset = offset;
        }

        public int Offset { get; set; }
    }

    internal class FakeParseResult<T> : ParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public new T Value { get; private set; }

        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    // Minimal implementation for TextSpan to verify captured values.
    internal class TextSpan
    {
        public TextSpan(string buffer, int offset, int length)
        {
            Buffer = buffer;
            Offset = offset;
            Length = length;
        }

        public string Buffer { get; }
        public int Offset { get; }
        public int Length { get; }
    }

    #endregion

    #region Fake and Helper Classes for Compilation

    // Minimal fake implementation for CompilationContext.
    internal class FakeCompilationContext : CompilationContext
    {
        private int _nextNumber = 1;

        public FakeCompilationContext(string buffer)
        {
            _buffer = buffer;
        }

        private readonly string _buffer;
        public override CompilationResult CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }

        public override Expression Buffer()
        {
            return Expression.Constant(_buffer);
        }

        public override Expression Offset()
        {
            // For simplicity, we return a constant offset.
            return Expression.Constant(10);
        }

        public override Expression Offset(Expression positionExpression)
        {
            // The offset difference: for our fake, just return constant 10.
            return Expression.Constant(10);
        }

        public override Expression NewTextSpan(Expression buffer, Expression startOffset, Expression length)
        {
            var constructor = typeof(TextSpan).GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });
            return Expression.New(constructor, buffer, startOffset, length);
        }

        public override Expression DeclarePositionVariable(CompilationResult result)
        {
            // Return a new parameter expression representing the starting position.
            return Expression.Parameter(typeof(int), "startPos" + _nextNumber++);
        }
    }

    // Minimal fake implementation for CompilationResult.
    internal class FakeCompilationResult<T> : CompilationResult
    {
        public FakeCompilationResult()
        {
            Value = Expression.Parameter(typeof(T), "value");
            Success = Expression.Parameter(typeof(bool), "success");
            Variables = new List<ParameterExpression>();
            Body = new List<Expression>();
        }

        public override ParameterExpression DeclareVariable<U>(string name, Expression initializer)
        {
            var variable = Expression.Parameter(typeof(U), name);
            Variables.Add(variable);
            return variable;
        }
    }

    // Minimal fake implementation for Parser compile result.
    internal class FakeParserCompileResult
    {
        public ICollection<ParameterExpression> Variables { get; set; }
        public Expression Body { get; set; }
        public Expression Success { get; set; }
    }

    #endregion
}
