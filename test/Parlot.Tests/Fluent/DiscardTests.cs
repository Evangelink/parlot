using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Discard{T, U}"/> class.
    /// </summary>
    public class DiscardTests
    {
        private readonly int _discardValue = 42;

        /// <summary>
        /// Tests that the Parse method returns true and sets the result correctly when the inner parser succeeds.
        /// </summary>
        [Fact]
        public void Parse_WhenInnerParserSucceeds_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var fakeParser = new FakeParserSuccess();
            var discard = new Discard<string, int>(fakeParser, _discardValue);
            var context = new FakeParseContext();
            var result = new FakeParseResult<int>();

            // Act
            bool success = discard.Parse(context, ref result);

            // Assert
            Assert.True(success);
            Assert.Equal(10, result.Start);
            Assert.Equal(20, result.End);
            Assert.Equal(_discardValue, result.Value);
            // Verify that the context received both EnterParser and ExitParser calls.
            Assert.Contains(discard, context.EnteredParsers);
            Assert.Contains(discard, context.ExitedParsers);
        }

        /// <summary>
        /// Tests that the Parse method returns false and leaves the result unchanged when the inner parser fails.
        /// </summary>
        [Fact]
        public void Parse_WhenInnerParserFails_ReturnsFalseAndLeavesResultUnchanged()
        {
            // Arrange
            var fakeParser = new FakeParserFailure();
            var discard = new Discard<string, int>(fakeParser, _discardValue);
            var context = new FakeParseContext();
            var result = new FakeParseResult<int>(); // Defaults: Start = 0, End = 0, Value = 0

            // Act
            bool success = discard.Parse(context, ref result);

            // Assert
            Assert.False(success);
            Assert.Equal(0, result.Start);
            Assert.Equal(0, result.End);
            Assert.Equal(0, result.Value);
            Assert.Contains(discard, context.EnteredParsers);
            Assert.Contains(discard, context.ExitedParsers);
        }

        /// <summary>
        /// Tests that the Compile method returns a valid CompilationResult containing the discard value and the inner parser's compilation instructions.
        /// </summary>
        [Fact]
        public void Compile_WhenCalled_ReturnsCompilationResultContainingDiscardValue()
        {
            // Arrange
            var fakeParser = new FakeParserForCompile();
            var discard = new Discard<string, int>(fakeParser, _discardValue);
            var context = new FakeCompilationContext();

            // Act
            var compilationResult = discard.Compile(context);

            // Assert
            Assert.NotNull(compilationResult);
            // Verify that the constant expression holds the discard value.
            var constantExpr = compilationResult.ConstantExpression as ConstantExpression;
            Assert.NotNull(constantExpr);
            Assert.Equal(_discardValue, constantExpr.Value);

            // Verify that the compilation body includes the inner parser's compilation instructions.
            Assert.Single(compilationResult.Body);
            var blockExpr = compilationResult.Body[0] as BlockExpression;
            Assert.NotNull(blockExpr);
            // The last expression in the block should be an assignment (from Expression.Assign).
            var lastExpr = blockExpr.Expressions.Last();
            Assert.IsType<BinaryExpression>(lastExpr);
        }

        /// <summary>
        /// Tests that the ToString method returns the inner parser's string representation followed by " (Discard)".
        /// </summary>
        [Fact]
        public void ToString_ReturnsInnerParserToStringWithDiscardSuffix()
        {
            // Arrange
            var fakeParser = new FakeParserSuccess("FakeParser");
            var discard = new Discard<string, int>(fakeParser, _discardValue);

            // Act
            string result = discard.ToString();

            // Assert
            Assert.Equal("FakeParser (Discard)", result);
        }

        #region Fake Classes for Testing

        /// <summary>
        /// Fake implementation of ParseContext for testing purposes.
        /// </summary>
        private class FakeParseContext : ParseContext
        {
            public List<object> EnteredParsers { get; } = new List<object>();
            public List<object> ExitedParsers { get; } = new List<object>();

            public override void EnterParser(object parser)
            {
                EnteredParsers.Add(parser);
            }

            public override void ExitParser(object parser)
            {
                ExitedParsers.Add(parser);
            }
        }

        /// <summary>
        /// Fake implementation of ParseResult for testing purposes.
        /// </summary>
        private class FakeParseResult<T> : ParseResult<T>
        {
            public int Start { get; set; }
            public int End { get; set; }
            public T Value { get; set; }

            public override void Set(int start, int end, T value)
            {
                Start = start;
                End = end;
                Value = value;
            }
        }

        /// <summary>
        /// A fake parser that always succeeds and simulates setting fixed start and end positions.
        /// </summary>
        private class FakeParserSuccess : Parser<string>
        {
            private readonly string _name;

            public FakeParserSuccess(string name = "FakeParser")
            {
                _name = name;
            }

            public override bool Parse(ParseContext context, ref ParseResult<string> result)
            {
                if (result is FakeParseResult<string> fakeResult)
                {
                    // Set predetermined positions.
                    fakeResult.Set(10, 20, "Success");
                }
                return true;
            }

            public override CompilationResult Build(CompilationContext context)
            {
                // Return a fake compilation result.
                var fakeResult = new FakeCompilationResult<string>(Expression.Constant("dummy", typeof(string)));
                // Simulate a success indicator.
                fakeResult.Success = Expression.Parameter(typeof(bool), "parserSuccess");
                return fakeResult;
            }

            public override string ToString() => _name;
        }

        /// <summary>
        /// A fake parser that always fails.
        /// </summary>
        private class FakeParserFailure : Parser<string>
        {
            public override bool Parse(ParseContext context, ref ParseResult<string> result)
            {
                return false;
            }

            public override CompilationResult Build(CompilationContext context)
            {
                var fakeResult = new FakeCompilationResult<string>(Expression.Constant("dummy", typeof(string)));
                fakeResult.Success = Expression.Parameter(typeof(bool), "parserSuccess");
                return fakeResult;
            }
        }

        /// <summary>
        /// A fake parser used to test the Compile method.
        /// </summary>
        private class FakeParserForCompile : Parser<string>
        {
            public override bool Parse(ParseContext context, ref ParseResult<string> result)
            {
                // Not used in compile tests.
                return true;
            }

            public override CompilationResult Build(CompilationContext context)
            {
                // Return a fake compilation result with dummy variables, body, and success.
                var fakeResult = new FakeCompilationResult<string>(Expression.Constant("dummy", typeof(string)));
                fakeResult.Variables.Add(Expression.Parameter(typeof(int), "dummyVar"));
                fakeResult.Body.Add(Expression.Constant("innerBody"));
                fakeResult.Success = Expression.Constant(true);
                return fakeResult;
            }
        }

        /// <summary>
        /// Fake implementation of CompilationContext for testing purposes.
        /// </summary>
        private class FakeCompilationContext : CompilationContext
        {
            public override CompilationResult CreateCompilationResult<T>(bool hasValue, Expression constantExpression)
            {
                return new FakeCompilationResult<T>(constantExpression);
            }
        }

        /// <summary>
        /// Fake implementation of CompilationResult for testing purposes.
        /// </summary>
        private class FakeCompilationResult<T> : CompilationResult
        {
            public FakeCompilationResult(Expression constantExpression)
            {
                ConstantExpression = constantExpression;
                Variables = new List<ParameterExpression>();
                Body = new List<Expression>();
                Success = Expression.Parameter(typeof(bool), "success");
            }

            public override Expression ConstantExpression { get; }

            public override List<ParameterExpression> Variables { get; }

            public override List<Expression> Body { get; }

            public override Expression Success { get; set; }
        }

        #endregion
    }
}
