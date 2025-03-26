using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Xunit;
using Parlot.Compilation;
using Parlot.Rewriting;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ZeroOrOne{T}"/> class.
    /// </summary>
    public class ZeroOrOneTests
    {
        private readonly int _testDefaultValue = 100;

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when a null parser is provided.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ZeroOrOne<int>(null, _testDefaultValue));
            Assert.Equal("parser", exception.ParamName);
        }

        /// <summary>
        /// Tests that Parse returns the underlying parser's parsed value when it succeeds.
        /// </summary>
        [Fact]
        public void Parse_WhenParserSucceeds_ReturnsParsedValue()
        {
            // Arrange
            int expectedValue = 42;
            var fakeParser = new FakeParserSuccess(expectedValue, start: 5, end: 10);
            var zeroOrOne = new ZeroOrOne<int>(fakeParser, _testDefaultValue);
            var context = new FakeParseContext();
            var result = new ParseResult<int>();

            // Act
            bool returned = zeroOrOne.Parse(context, ref result);

            // Assert
            Assert.True(returned);
            Assert.Equal(5, result.Start);
            Assert.Equal(10, result.End);
            Assert.Equal(expectedValue, result.Value);
        }

        /// <summary>
        /// Tests that Parse returns the default value when the underlying parser fails.
        /// </summary>
        [Fact]
        public void Parse_WhenParserFails_ReturnsDefaultValue()
        {
            // Arrange
            var fakeParser = new FakeParserFailure(start: 0, end: 0);
            var zeroOrOne = new ZeroOrOne<int>(fakeParser, _testDefaultValue);
            var context = new FakeParseContext();
            var result = new ParseResult<int>();

            // Act
            bool returned = zeroOrOne.Parse(context, ref result);

            // Assert
            Assert.True(returned);
            // Even if the underlying parser fails, ZeroOrOne always succeeds and assigns the default value.
            Assert.Equal(_testDefaultValue, result.Value);
        }

        /// <summary>
        /// Tests that ToString returns the underlying parser's ToString with a trailing '?'.
        /// </summary>
        [Fact]
        public void ToString_ReturnsUnderlyingParserToStringWithQuestionMark()
        {
            // Arrange
            var fakeParser = new FakeParserForToString("FakeParser");
            var zeroOrOne = new ZeroOrOne<int>(fakeParser, _testDefaultValue);

            // Act
            string description = zeroOrOne.ToString();

            // Assert
            Assert.Equal("FakeParser?", description);
        }

        /// <summary>
        /// Tests that Compile returns a CompilationResult that, when inspected, includes an assignment with the default value in the failure branch.
        /// </summary>
        [Fact]
        public void Compile_WhenParserCompilationFails_AssignsDefaultValue()
        {
            // Arrange
            var fakeCompilationParser = new FakeCompilationParser();
            var zeroOrOne = new ZeroOrOne<int>(fakeCompilationParser, _testDefaultValue);
            var compilationContext = new FakeCompilationContext(discardResult: false);

            // Act
            var compilationResult = zeroOrOne.Compile(compilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);

            // Search for the Conditional (IfThenElse) expression inside the block.
            var blockExpression = compilationResult.Body.First() as BlockExpression;
            Assert.NotNull(blockExpression);

            // Look for a ConditionalExpression inside the block that assigns to compilationResult.Value.
            var conditionalExpression = blockExpression.Expressions
                .OfType<ConditionalExpression>()
                .FirstOrDefault();
            Assert.NotNull(conditionalExpression);

            // In the false branch, the assignment should set the result's value to the default value.
            // The false branch is: Expression.Assign(result.Value, Expression.Constant(_defaultValue, typeof(int)))
            var falseBranch = conditionalExpression.IfFalse as BinaryExpression;
            Assert.NotNull(falseBranch);
            var constantExpr = falseBranch.Right as ConstantExpression;
            Assert.NotNull(constantExpr);
            Assert.Equal(_testDefaultValue, constantExpr.Value);
        }

        #region Fake Classes for Testing

        // Fake implementation of ParseContext.
        private class FakeParseContext : ParseContext
        {
            public override void EnterParser(Parser parser) { }
            public override void ExitParser(Parser parser) { }
        }

        // Fake implementation of ParseResult.
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

        // Fake parser that always succeeds.
        private class FakeParserSuccess : Parser<int>
        {
            private readonly int _value;
            private readonly int _start;
            private readonly int _end;

            public FakeParserSuccess(int value, int start, int end)
            {
                _value = value;
                _start = start;
                _end = end;
            }

            public override bool Parse(ParseContext context, ref ParseResult<int> result)
            {
                // Simulate a successful parse.
                result.Set(_start, _end, _value);
                return true;
            }

            // Not used in these tests.
            public override string ToString() => "FakeParserSuccess";
        }

        // Fake parser that always fails.
        private class FakeParserFailure : Parser<int>
        {
            private readonly int _start;
            private readonly int _end;

            public FakeParserFailure(int start, int end)
            {
                _start = start;
                _end = end;
            }

            public override bool Parse(ParseContext context, ref ParseResult<int> result)
            {
                // Simulate a failed parse, do not set a value.
                result.Set(_start, _end, default);
                return false;
            }

            public override string ToString() => "FakeParserFailure";
        }

        // Fake parser for ToString testing.
        private class FakeParserForToString : Parser<int>
        {
            private readonly string _description;

            public FakeParserForToString(string description)
            {
                _description = description;
            }

            public override bool Parse(ParseContext context, ref ParseResult<int> result)
            {
                throw new NotImplementedException();
            }

            public override string ToString() => _description;
        }

        // Fake parser that implements ICompilable to simulate compilation behavior.
        private class FakeCompilationParser : Parser<int>, ICompilable
        {
            public override bool Parse(ParseContext context, ref ParseResult<int> result)
            {
                throw new NotImplementedException();
            }

            public CompilationResult Compile(CompilationContext context)
            {
                // Return a fake compilation result that simulates a failed parse.
                var fakeResult = new FakeCompilationResult<int>(Expression.Constant(_testDefaultValue, typeof(int)));
                fakeResult.Variables = new List<ParameterExpression>();
                fakeResult.Body = new List<Expression>
                {
                    // Dummy body expression.
                    Expression.Constant("Fake Body")
                };
                // Set Success to false so that the default value branch is used.
                fakeResult.Success = Expression.Constant(false);
                fakeResult.Value = Expression.Constant(999); // This value should be ignored due to success==false.
                return fakeResult;
            }

            public override string ToString() => "FakeCompilationParser";
        }

        // Minimal fake implementation of CompilationContext.
        private class FakeCompilationContext : CompilationContext
        {
            public override bool DiscardResult { get; }

            public FakeCompilationContext(bool discardResult)
            {
                DiscardResult = discardResult;
            }

            public override CompilationResult<T> CreateCompilationResult<T>(bool isTerminal, Expression defaultExpression)
            {
                return new FakeCompilationResult<T>(defaultExpression);
            }
        }

        // Minimal fake implementation of CompilationResult.
        private class FakeCompilationResult<T> : CompilationResult<T>
        {
            public override List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
            public override List<Expression> Body { get; } = new List<Expression>();
            public override Expression Value { get; set; }

            public Expression DefaultExpression { get; }

            public FakeCompilationResult(Expression defaultExpression)
            {
                DefaultExpression = defaultExpression;
                Value = defaultExpression;
            }
        }

        #endregion
    }

    #region Minimal Abstract Base Classes from Parlot

    // The following minimal abstract classes are provided to support the tests.
    // In a real scenario, these would come from the Parlot libraries.

    public abstract class Parser<T>
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
        public abstract override string ToString();

        // Virtual Build method to support ICompilable interface in parsers.
        public virtual CompilationResult Build(CompilationContext context)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class ParseContext
    {
        public abstract void EnterParser(Parser parser);
        public abstract void ExitParser(Parser parser);
    }

    public abstract class CompilationContext
    {
        public abstract bool DiscardResult { get; }
        public abstract CompilationResult<T> CreateCompilationResult<T>(bool isTerminal, Expression defaultExpression);
    }

    public abstract class CompilationResult
    {
    }

    public abstract class CompilationResult<T> : CompilationResult
    {
        public abstract List<ParameterExpression> Variables { get; }
        public abstract List<Expression> Body { get; }
        public abstract Expression Value { get; set; }
    }

    #endregion
}
