using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Compilation
{
    /// <summary>
    /// Fake implementation of CompilationResult for testing purposes.
    /// </summary>
    public class CompilationResult
    {
        public Expression Value { get; set; }
        public ParameterExpression Success { get; set; }
        public List<Expression> Body { get; set; } = new List<Expression>();
        public List<ParameterExpression> Variables { get; set; } = new List<ParameterExpression>();
    }

    /// <summary>
    /// Fake implementation of CompilationContext for testing purposes.
    /// </summary>
    public class CompilationContext
    {
        public bool DiscardResult { get; set; }

        public CompilationResult CreateCompilationResult(Type type, bool flag, Expression expr)
        {
            // Creates a new CompilationResult with the provided initial expression and a dummy Success variable.
            var result = new CompilationResult
            {
                Value = expr,
                Success = Expression.Parameter(typeof(bool), "success")
            };
            return result;
        }

        public Expression DeclarePositionVariable(CompilationResult result)
        {
            // Declares a fake position variable.
            return Expression.Parameter(typeof(int), "start");
        }

        public Expression ResetPosition(Expression start)
        {
            // Returns an expression representing resetting the position.
            return Expression.Empty();
        }
    }

    /// <summary>
    /// Fake implementation of SkippableCompilationResult for testing purposes.
    /// </summary>
    public class SkippableCompilationResult
    {
        public bool Skip { get; set; }
        public CompilationResult CompilationResult { get; set; }
    }
}

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Contains unit tests for the SequenceCompileHelper class.
    /// </summary>
    public class SequenceCompileHelperTests
    {
        /// <summary>
        /// Tests CreateSequenceCompileResult with two non-skippable results and DiscardResult set to false.
        /// Verifies that the returned CompilationResult has its Value assigned using a ValueTuple constructor.
        /// </summary>
        [Fact]
        public void CreateSequenceCompileResult_WithTwoNonSkippableAndDiscardResultFalse_ReturnsResultWithValueAssigned()
        {
            // Arrange
            var context = new CompilationContext { DiscardResult = false };

            var firstResult = new SkippableCompilationResult
            {
                Skip = false,
                CompilationResult = new CompilationResult
                {
                    Value = Expression.Constant(1)
                }
            };

            var secondResult = new SkippableCompilationResult
            {
                Skip = false,
                CompilationResult = new CompilationResult
                {
                    Value = Expression.Constant("test")
                }
            };

            var parserResults = new[] { firstResult, secondResult };

            // Act
            var result = SequenceCompileHelper.CreateSequenceCompileResult(parserResults, context);

            // Assert
            Assert.NotNull(result);
            // Check that the Value property is a NewExpression with the correct ValueTuple type (ValueTuple<int, string>)
            var newExpr = result.Value as NewExpression;
            Assert.NotNull(newExpr);
            var expectedTupleType = typeof(ValueTuple<int, string>);
            Assert.Equal(expectedTupleType, newExpr.Type);
        }

        /// <summary>
        /// Tests CreateSequenceCompileResult with two non-skippable results and DiscardResult set to true.
        /// Verifies that the returned CompilationResult is created with the correct ValueTuple type and does not assign a value.
        /// </summary>
        [Fact]
        public void CreateSequenceCompileResult_WithTwoNonSkippableAndDiscardResultTrue_ReturnsResultWithoutValueAssignment()
        {
            // Arrange
            var context = new CompilationContext { DiscardResult = true };

            var firstResult = new SkippableCompilationResult
            {
                Skip = false,
                CompilationResult = new CompilationResult
                {
                    Value = Expression.Constant(42)
                }
            };

            var secondResult = new SkippableCompilationResult
            {
                Skip = false,
                CompilationResult = new CompilationResult
                {
                    Value = Expression.Constant(3.14)
                }
            };

            var parserResults = new[] { firstResult, secondResult };

            // Act
            var result = SequenceCompileHelper.CreateSequenceCompileResult(parserResults, context);

            // Assert
            Assert.NotNull(result);
            var newExpr = result.Value as NewExpression;
            Assert.NotNull(newExpr);
            var expectedTupleType = typeof(ValueTuple<int, double>);
            Assert.Equal(expectedTupleType, newExpr.Type);
        }

        /// <summary>
        /// Tests CreateSequenceCompileResult with an insufficient number of non-skippable results (only one non-skippable result).
        /// Expects a NotSupportedException to be thrown with the appropriate message.
        /// </summary>
        [Fact]
        public void CreateSequenceCompileResult_WithInsufficientNonSkippableResults_ThrowsNotSupportedException()
        {
            // Arrange
            var context = new CompilationContext { DiscardResult = false };

            // Only one non-skippable result is provided; the other is skipped.
            var firstResult = new SkippableCompilationResult
            {
                Skip = false,
                CompilationResult = new CompilationResult
                {
                    Value = Expression.Constant(99)
                }
            };

            var secondResult = new SkippableCompilationResult
            {
                Skip = true,
                CompilationResult = new CompilationResult
                {
                    Value = Expression.Constant("ignored")
                }
            };

            var parserResults = new[] { firstResult, secondResult };

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                SequenceCompileHelper.CreateSequenceCompileResult(parserResults, context));
            Assert.Equal("Unsupported number of type arguments", exception.Message);
        }
    }
}
