using System;
using System.Collections.Generic;
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
        private readonly string _testValue;
        
        public DiscardTests()
        {
            _testValue = "TestValue";
        }

        /// <summary>
        /// Tests that the constructor correctly initializes an instance.
        /// </summary>
        [Fact]
        public void Constructor_ValidParameters_InitializesInstance()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                Succeed = true
            };

            // Act
            var discardParser = new Discard<int, string>(fakeParser, _testValue);

            // Assert
            Assert.NotNull(discardParser);
            Assert.Contains("(Discard)", discardParser.ToString());
        }

        /// <summary>
        /// Tests the Parse method when the underlying parser succeeds.
        /// Expected: Returns true and sets the result's value to the provided discard value.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserSucceeds_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                Succeed = true,
                StartVal = 5,
                EndVal = 10,
                DummyParseValue = 123 // dummy value not used directly in Discard
            };
            var discardParser = new Discard<int, string>(fakeParser, _testValue);
            var context = new FakeParseContext();
            var result = new ParseResult<string>();

            // Act
            bool parseResult = discardParser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(5, result.Start);
            Assert.Equal(10, result.End);
            Assert.Equal(_testValue, result.Value);
        }

        /// <summary>
        /// Tests the Parse method when the underlying parser fails.
        /// Expected: Returns false and does not modify the result.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserFails_ReturnsFalse()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                Succeed = false
            };
            var discardParser = new Discard<int, string>(fakeParser, _testValue);
            var context = new FakeParseContext();
            var result = new ParseResult<string>();

            // Act
            bool parseResult = discardParser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            // Since underlying parser failed, result is not set.
            Assert.Equal(0, result.Start);
            Assert.Equal(0, result.End);
            Assert.Null(result.Value);
        }

        /// <summary>
        /// Tests the Compile method to ensure it returns a compilation result containing a constant expression with the discard value.
        /// </summary>
        [Fact]
        public void Compile_WhenCalled_ReturnsCompilationResultWithExpectedExpressions()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                Succeed = true
            };
            var discardParser = new Discard<int, string>(fakeParser, _testValue);
            var context = new FakeCompilationContext();

            // Act
            var compilationResult = discardParser.Compile(context);

            // Assert
            Assert.NotNull(compilationResult);
            // Check that the constant expression in the result is of type string and equals _testValue.
            var constExpression = compilationResult.InitialValue as ConstantExpression;
            Assert.NotNull(constExpression);
            Assert.Equal(_testValue, constExpression.Value);
            Assert.Equal(typeof(string), constExpression.Type);
            // Check that the Body was appended to compilationResult.
            Assert.NotEmpty(compilationResult.Body);
        }

        #region Fake Implementations for Testing

        /// <summary>
        /// A fake implementation of Parser<T> used for testing.
        /// </summary>
        /// <typeparam name="T">The type parsed.</typeparam>
        private class FakeParser<T> : Parser<T>
        {
            public bool Succeed { get; set; }
            public int StartVal { get; set; } = 0;
            public int EndVal { get; set; } = 0;
            public T DummyParseValue { get; set; }

            public bool BuildCalled { get; private set; } = false;

            public override bool Parse(ParseContext context, ref ParseResult<T> result)
            {
                if (Succeed)
                {
                    result.Set(StartVal, EndVal, DummyParseValue);
                    return true;
                }
                return false;
            }

            public override CompilationResult Build(CompilationContext context)
            {
                BuildCalled = true;
                // Create a fake compilation result for T.
                var constExp = Expression.Constant(DummyParseValue, typeof(T));
                var compilationResult = context.CreateCompilationResult<T>(false, constExp);
                // For testing, simulate that the parser produces an expression that represents success.
                compilationResult.Success = Expression.Constant(true);
                // Simulate empty body and variables.
                compilationResult.Body = new List<Expression>();
                compilationResult.Variables = new List<ParameterExpression>();
                return compilationResult;
            }
        }

        /// <summary>
        /// A fake implementation of ParseContext used for testing.
        /// </summary>
        private class FakeParseContext : ParseContext
        {
            public override void EnterParser(object parser)
            {
                // No-op for testing.
            }

            public override void ExitParser(object parser)
            {
                // No-op for testing.
            }
        }

        /// <summary>
        /// A fake implementation of CompilationContext used for testing.
        /// </summary>
        private class FakeCompilationContext : CompilationContext
        {
            public override CompilationResult CreateCompilationResult<T>(bool success, Expression expression)
            {
                return new FakeCompilationResult
                {
                    InitialValue = expression,
                    Success = expression,
                    Body = new List<Expression>(),
                    Variables = new List<ParameterExpression>()
                };
            }
        }

        /// <summary>
        /// A fake implementation of CompilationResult used for testing.
        /// </summary>
        private class FakeCompilationResult : CompilationResult
        {
            public override Expression Success { get; set; }
            public override List<Expression> Body { get; set; }
            public override List<ParameterExpression> Variables { get; set; }
            public Expression InitialValue { get; set; }
        }

        #endregion
    }

    #region Minimal Stubs Mimicking Production Types

    // The following minimal stubs are provided to enable compilation of tests.
    // In the real test environment, these would be replaced by the actual production types.

    /// <summary>
    /// Represents the result of a parse operation.
    /// </summary>
    public class ParseResult<T>
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
    /// Represents the context in which parsing occurs.
    /// </summary>
    public abstract class ParseContext
    {
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
    }

    /// <summary>
    /// Base parser class.
    /// </summary>
    public abstract class Parser<T>
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
        public abstract CompilationResult Build(CompilationContext context);
    }

    /// <summary>
    /// Represents the context in which compilation occurs.
    /// </summary>
    public abstract class CompilationContext
    {
        public abstract CompilationResult CreateCompilationResult<T>(bool success, Expression expression);
    }

    /// <summary>
    /// Represents the result of a compilation.
    /// </summary>
    public abstract class CompilationResult
    {
        public abstract Expression Success { get; set; }
        public abstract List<Expression> Body { get; set; }
        public abstract List<ParameterExpression> Variables { get; set; }
    }

    #endregion
}
