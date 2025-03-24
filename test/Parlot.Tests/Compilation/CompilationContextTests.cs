using System.Collections.Generic;
using System.Linq.Expressions;
using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Compilation.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CompilationContext"/> class.
    /// </summary>
    public class CompilationContextTests
    {
        private readonly CompilationContext _compilationContext;

        public CompilationContextTests()
        {
            _compilationContext = new CompilationContext();
        }

        /// <summary>
        /// Tests that the constructor initializes the ParseContext property and the collections.
        /// </summary>
        [Fact]
        public void Constructor_InitializesPropertiesProperly()
        {
            // Assert
            Assert.NotNull(_compilationContext.ParseContext);
            Assert.Equal(typeof(ParseContext), _compilationContext.ParseContext.Type);

            Assert.NotNull(_compilationContext.GlobalVariables);
            Assert.Empty(_compilationContext.GlobalVariables);

            Assert.NotNull(_compilationContext.GlobalExpressions);
            Assert.Empty(_compilationContext.GlobalExpressions);

            Assert.NotNull(_compilationContext.Lambdas);
            Assert.Empty(_compilationContext.Lambdas);
        }

        /// <summary>
        /// Tests that the NextNumber property returns incrementing unique values.
        /// </summary>
        [Fact]
        public void NextNumber_WhenCalledMultipleTimes_ReturnsIncrementingValues()
        {
            // Arrange
            int first = _compilationContext.NextNumber;
            int second = _compilationContext.NextNumber;
            int third = _compilationContext.NextNumber;

            // Assert
            Assert.Equal(first + 1, second);
            Assert.Equal(second + 1, third);
        }

        /// <summary>
        /// Tests the getter and setter of the DiscardResult property.
        /// </summary>
        [Fact]
        public void DiscardResult_GetAndSet_WorksAsExpected()
        {
            // Default value should be false
            Assert.False(_compilationContext.DiscardResult);

            // Act
            _compilationContext.DiscardResult = true;

            // Assert
            Assert.True(_compilationContext.DiscardResult);
        }

        /// <summary>
        /// Tests the generic CreateCompilationResult method when defaultValue is null.
        /// Verifies that the generated CompilationResult contains the correct success assignment and only one body expression.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_Generic_WithNullDefaultValue_CreatesResultWithSingleAssignment()
        {
            // Arrange
            bool defaultSuccess = false;

            // Act
            var result = _compilationContext.CreateCompilationResult<int>(defaultSuccess, defaultValue: null);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.Equal(typeof(bool), result.Success.Type);
            Assert.NotNull(result.Value);
            Assert.Equal(typeof(int), result.Value.Type);

            // Variables list should contain two variables.
            Assert.NotNull(result.Variables);
            Assert.Equal(2, result.Variables.Count);
            Assert.Contains(result.Variables, v => v == result.Success);
            Assert.Contains(result.Variables, v => v == result.Value);

            // Body should have one assignment (for success variable)
            Assert.NotNull(result.Body);
            Assert.Single(result.Body);

            var assignExpr = result.Body[0] as BinaryExpression;
            Assert.NotNull(assignExpr);
            Assert.Equal(ExpressionType.Assign, assignExpr.NodeType);

            // Verify that the left-hand side is the success variable and assigned constant false.
            Assert.Equal(result.Success, assignExpr.Left);
            var constantExpr = assignExpr.Right as ConstantExpression;
            Assert.NotNull(constantExpr);
            Assert.Equal(defaultSuccess, constantExpr.Value);
        }

        /// <summary>
        /// Tests the generic CreateCompilationResult method when a defaultValue expression is provided.
        /// Verifies that two assignment expressions are generated: one for success and one for value.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_Generic_WithNonNullDefaultValue_CreatesResultWithTwoAssignments()
        {
            // Arrange
            bool defaultSuccess = true;
            Expression defaultValueExpr = Expression.Constant(42);

            // Act
            var result = _compilationContext.CreateCompilationResult<int>(defaultSuccess, defaultValueExpr);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.Equal(typeof(bool), result.Success.Type);
            Assert.NotNull(result.Value);
            Assert.Equal(typeof(int), result.Value.Type);

            // Variables list should contain two variables.
            Assert.NotNull(result.Variables);
            Assert.Equal(2, result.Variables.Count);

            // Body should have two assignments: one for success and one for value.
            Assert.NotNull(result.Body);
            Assert.Equal(2, result.Body.Count);

            // Verify first assignment (success variable)
            var successAssign = result.Body[0] as BinaryExpression;
            Assert.NotNull(successAssign);
            Assert.Equal(ExpressionType.Assign, successAssign.NodeType);
            Assert.Equal(result.Success, successAssign.Left);
            var successConst = successAssign.Right as ConstantExpression;
            Assert.NotNull(successConst);
            Assert.Equal(defaultSuccess, successConst.Value);

            // Verify second assignment (value variable)
            var valueAssign = result.Body[1] as BinaryExpression;
            Assert.NotNull(valueAssign);
            Assert.Equal(ExpressionType.Assign, valueAssign.NodeType);
            Assert.Equal(result.Value, valueAssign.Left);
            // Check that the right side is the provided default expression (here, constant 42)
            var valueConst = valueAssign.Right as ConstantExpression;
            Assert.NotNull(valueConst);
            Assert.Equal(42, valueConst.Value);
        }

        /// <summary>
        /// Tests the non-generic CreateCompilationResult method when defaultValue is null.
        /// Verifies that the generated CompilationResult contains the correct variable types and a single assignment.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_NonGeneric_WithNullDefaultValue_CreatesResultWithSingleAssignment()
        {
            // Arrange
            bool defaultSuccess = false;
            Type valueType = typeof(string);

            // Act
            var result = _compilationContext.CreateCompilationResult(valueType, defaultSuccess, defaultValue: null);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.Equal(typeof(bool), result.Success.Type);
            Assert.NotNull(result.Value);
            Assert.Equal(valueType, result.Value.Type);

            // Variables list should contain two variables.
            Assert.NotNull(result.Variables);
            Assert.Equal(2, result.Variables.Count);

            // Body should have one assignment (for success variable)
            Assert.NotNull(result.Body);
            Assert.Single(result.Body);

            var assignExpr = result.Body[0] as BinaryExpression;
            Assert.NotNull(assignExpr);
            Assert.Equal(ExpressionType.Assign, assignExpr.NodeType);
            Assert.Equal(result.Success, assignExpr.Left);
            var constantExpr = assignExpr.Right as ConstantExpression;
            Assert.NotNull(constantExpr);
            Assert.Equal(defaultSuccess, constantExpr.Value);
        }

        /// <summary>
        /// Tests the non-generic CreateCompilationResult method when a defaultValue expression is provided.
        /// Verifies that two assignment expressions are generated: one for success and one for value.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_NonGeneric_WithNonNullDefaultValue_CreatesResultWithTwoAssignments()
        {
            // Arrange
            bool defaultSuccess = true;
            Type valueType = typeof(string);
            Expression defaultValueExpr = Expression.Constant("default");

            // Act
            var result = _compilationContext.CreateCompilationResult(valueType, defaultSuccess, defaultValueExpr);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.Equal(typeof(bool), result.Success.Type);
            Assert.NotNull(result.Value);
            Assert.Equal(valueType, result.Value.Type);

            // Variables list should contain two variables.
            Assert.NotNull(result.Variables);
            Assert.Equal(2, result.Variables.Count);

            // Body should have two assignment expressions.
            Assert.NotNull(result.Body);
            Assert.Equal(2, result.Body.Count);

            // Verify first assignment (success variable)
            var successAssign = result.Body[0] as BinaryExpression;
            Assert.NotNull(successAssign);
            Assert.Equal(ExpressionType.Assign, successAssign.NodeType);
            Assert.Equal(result.Success, successAssign.Left);
            var successConst = successAssign.Right as ConstantExpression;
            Assert.NotNull(successConst);
            Assert.Equal(defaultSuccess, successConst.Value);

            // Verify second assignment (value variable)
            var valueAssign = result.Body[1] as BinaryExpression;
            Assert.NotNull(valueAssign);
            Assert.Equal(ExpressionType.Assign, valueAssign.NodeType);
            Assert.Equal(result.Value, valueAssign.Left);
            var valueConst = valueAssign.Right as ConstantExpression;
            Assert.NotNull(valueConst);
            Assert.Equal("default", valueConst.Value);
        }
    }
}
