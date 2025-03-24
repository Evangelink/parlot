using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Contains unit tests for the <see cref="SequenceCompileHelper"/> class.
    /// </summary>
    public class SequenceCompileHelperTests
    {
        /// <summary>
        /// Tests that CreateSequenceCompileResult returns a valid compilation result when provided with a mixture of skippable and non-skippable parser results.
        /// This test uses two non-skippable results (of type int) and one skippable result, expecting the resulting compilation result type to be ValueTuple&lt;int, int&gt;.
        /// </summary>
        [Fact]
        public void CreateSequenceCompileResult_WithTwoNonSkippableAndOneSkippable_ReturnsResultWithValueTupleOfTwoInts()
        {
            // Arrange
            var fakeContext = new FakeCompilationContext
            {
                DiscardResult = false
            };

            // Create two non-skippable results (type int) and one skippable result.
            var nonSkippable1 = FakeSkippableCompilationResult.Create(fakeContext, skip: false, typeof(int));
            var skippable = FakeSkippableCompilationResult.Create(fakeContext, skip: true, typeof(string));
            var nonSkippable2 = FakeSkippableCompilationResult.Create(fakeContext, skip: false, typeof(int));

            var parserCompileResults = new[] { nonSkippable1, skippable, nonSkippable2 };

            // Act
            var result = SequenceCompileHelper.CreateSequenceCompileResult(parserCompileResults, fakeContext);

            // Assert
            var expectedResultType = typeof(ValueTuple<int, int>);
            Assert.Equal(expectedResultType, result.ResultType);

            // Validate that two expressions were added to the body (one for the inner block and one for the reset position).
            Assert.Equal(2, result.Body.Count);

            // Validate that the second expression is an IfThen expression which resets the position when success is false.
            var ifThenExpr = result.Body[1] as ConditionalExpression;
            Assert.NotNull(ifThenExpr);
            Assert.IsType<UnaryExpression>(ifThenExpr.Test);
        }

        /// <summary>
        /// Tests that CreateSequenceCompileResult throws a NotSupportedException when there is exactly one non-skippable parser, as the value tuple cannot be constructed.
        /// </summary>
        [Fact]
        public void CreateSequenceCompileResult_WithSingleNonSkippableParser_ThrowsNotSupportedException()
        {
            // Arrange
            var fakeContext = new FakeCompilationContext();
            var nonSkippable = FakeSkippableCompilationResult.Create(fakeContext, skip: false, typeof(int));
            var parserCompileResults = new[] { nonSkippable };

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                SequenceCompileHelper.CreateSequenceCompileResult(parserCompileResults, fakeContext));

            Assert.Equal("Unsupported number of type arguments", exception.Message);
        }

        /// <summary>
        /// Tests that CreateSequenceCompileResult throws a NotSupportedException when there are no non-skippable parsers.
        /// </summary>
        [Fact]
        public void CreateSequenceCompileResult_WithNoNonSkippableParser_ThrowsNotSupportedException()
        {
            // Arrange
            var fakeContext = new FakeCompilationContext();
            var skippable1 = FakeSkippableCompilationResult.Create(fakeContext, skip: true, typeof(int));
            var skippable2 = FakeSkippableCompilationResult.Create(fakeContext, skip: true, typeof(int));
            var parserCompileResults = new[] { skippable1, skippable2 };

            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() =>
                SequenceCompileHelper.CreateSequenceCompileResult(parserCompileResults, fakeContext));

            Assert.Equal("Unsupported number of type arguments", exception.Message);
        }
    }

    /// <summary>
    /// A fake implementation of CompilationContext to support unit testing of SequenceCompileHelper.
    /// </summary>
    internal class FakeCompilationContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether the compilation result should be discarded.
        /// </summary>
        public bool DiscardResult { get; set; }

        /// <summary>
        /// Simulates the creation of a CompilationResult.
        /// </summary>
        /// <param name="resultType">The type of the result value.</param>
        /// <param name="flag">A flag parameter (not used in the fake implementation).</param>
        /// <param name="creationExpression">The expression to create a new instance of the result type.</param>
        /// <returns>A new instance of FakeCompilationResult.</returns>
        public FakeCompilationResult CreateCompilationResult(Type resultType, bool flag, Expression creationExpression)
        {
            return new FakeCompilationResult(resultType)
            {
                CreationExpression = creationExpression,
                Success = Expression.Parameter(typeof(bool), "success"),
                Value = Expression.Parameter(resultType, "value")
            };
        }

        /// <summary>
        /// Simulates declaration of a position variable in the compilation context.
        /// </summary>
        /// <param name="result">The compilation result for which the position is declared.</param>
        /// <returns>A new ParameterExpression representing the position variable.</returns>
        public ParameterExpression DeclarePositionVariable(FakeCompilationResult result)
        {
            return Expression.Variable(typeof(int), "position");
        }

        /// <summary>
        /// Simulates the generation of an expression to reset the scanner position.
        /// </summary>
        /// <param name="position">The position variable to reset.</param>
        /// <returns>An Expression representing the reset operation.</returns>
        public Expression ResetPosition(ParameterExpression position)
        {
            // For testing purposes, simply return an empty expression.
            return Expression.Empty();
        }
    }

    /// <summary>
    /// A fake implementation of CompilationResult to support unit testing.
    /// </summary>
    internal class FakeCompilationResult
    {
        /// <summary>
        /// Gets the result type.
        /// </summary>
        public Type ResultType { get; }

        /// <summary>
        /// Gets or sets the expression representing the success flag.
        /// </summary>
        public Expression Success { get; set; }

        /// <summary>
        /// Gets or sets the expression representing the value.
        /// </summary>
        public Expression Value { get; set; }

        /// <summary>
        /// Gets the list of variable expressions used in the compilation.
        /// </summary>
        public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();

        /// <summary>
        /// Gets the list of expressions constituting the body of the compiled parser.
        /// </summary>
        public List<Expression> Body { get; } = new List<Expression>();

        /// <summary>
        /// Gets or sets the creation expression for the result.
        /// </summary>
        public Expression CreationExpression { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeCompilationResult"/> class.
        /// </summary>
        /// <param name="resultType">The type of the result.</param>
        public FakeCompilationResult(Type resultType)
        {
            ResultType = resultType;
        }
    }

    /// <summary>
    /// A fake representation of SkippableCompilationResult to support unit testing.
    /// </summary>
    internal class FakeSkippableCompilationResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this parser result is to be skipped.
        /// </summary>
        public bool Skip { get; set; }

        /// <summary>
        /// Gets or sets the compilation result associated with this parser.
        /// </summary>
        public FakeCompilationResult CompilationResult { get; set; }

        /// <summary>
        /// Creates a new fake skippable compilation result.
        /// </summary>
        /// <param name="context">The fake compilation context to use for creating the compilation result.</param>
        /// <param name="skip">Indicates whether to mark this result as skippable.</param>
        /// <param name="valueType">The type of the value produced by this parser.</param>
        /// <returns>A new instance of FakeSkippableCompilationResult.</returns>
        public static FakeSkippableCompilationResult Create(FakeCompilationContext context, bool skip, Type valueType)
        {
            // Use a dummy creation expression for the compilation result.
            var creationExpr = Expression.New(valueType.GetConstructor(Type.EmptyTypes) ?? typeof(object).GetConstructor(Type.EmptyTypes));
            var compResult = context.CreateCompilationResult(valueType, false, creationExpr);

            // For testing purposes, assign the Value property to a constant expression of the provided type.
            compResult.Value = Expression.Constant(Activator.CreateInstance(valueType), valueType);

            return new FakeSkippableCompilationResult
            {
                Skip = skip,
                CompilationResult = compResult
            };
        }
    }
}
