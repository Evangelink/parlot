using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Parlot.Fluent;
using Parlot.Compilation;
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
        /// Tests that the default constructor initializes all relevant properties correctly.
        /// </summary>
        [Fact]
        public void Constructor_InitializesProperties_Correctly()
        {
            // Assert: Verify default initialization of properties.
            Assert.NotNull(_compilationContext.ParseContext);
            Assert.IsType<ParameterExpression>(_compilationContext.ParseContext);

            Assert.NotNull(_compilationContext.GlobalVariables);
            Assert.Empty(_compilationContext.GlobalVariables);

            Assert.NotNull(_compilationContext.GlobalExpressions);
            Assert.Empty(_compilationContext.GlobalExpressions);

            Assert.NotNull(_compilationContext.Lambdas);
            Assert.Empty(_compilationContext.Lambdas);
        }

        /// <summary>
        /// Tests that the NextNumber property returns incremental numbers on each access.
        /// </summary>
        [Fact]
        public void NextNumber_MultipleCalls_ReturnsIncrementalNumbers()
        {
            // Act
            int firstCall = _compilationContext.NextNumber;
            int secondCall = _compilationContext.NextNumber;
            int thirdCall = _compilationContext.NextNumber;

            // Assert: Since the counter starts at 0, each successive call should increment.
            Assert.Equal(0, firstCall);
            Assert.Equal(1, secondCall);
            Assert.Equal(2, thirdCall);
        }

        /// <summary>
        /// Tests that the DiscardResult property can be set and retrieved as expected.
        /// </summary>
        [Fact]
        public void DiscardResult_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            _compilationContext.DiscardResult = true;
            
            // Act & Assert
            Assert.True(_compilationContext.DiscardResult);

            // Act
            _compilationContext.DiscardResult = false;

            // Assert
            Assert.False(_compilationContext.DiscardResult);
        }

        /// <summary>
        /// Tests the generic CreateCompilationResult method without providing a default value.
        /// Ensures that only the success variable is assigned.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_GenericWithoutDefaultValue_ReturnsExpectedCompilationResult()
        {
            // Arrange
            bool defaultSuccess = true;

            // Act
            var result = _compilationContext.CreateCompilationResult<int>(defaultSuccess);

            // Assert: Validate that the result and its components are initialized correctly.
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.NotNull(result.Value);
            Assert.Contains("success", result.Success.Name);
            Assert.Contains("value", result.Value.Name);
            Assert.Contains(result.Success, result.Variables);
            Assert.Contains(result.Value, result.Variables);

            // There should be one assignment in the body for the success variable.
            Assert.Single(result.Body);
            var assignSuccess = result.Body[0] as BinaryExpression;
            Assert.NotNull(assignSuccess);
            Assert.Equal(result.Success, assignSuccess.Left);
            var constantExpr = assignSuccess.Right as ConstantExpression;
            Assert.NotNull(constantExpr);
            Assert.Equal(defaultSuccess, constantExpr.Value);
        }

        /// <summary>
        /// Tests the generic CreateCompilationResult method when a default value is provided.
        /// Ensures that assignments for both success and value variables are included.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_GenericWithDefaultValue_ReturnsExpectedCompilationResult()
        {
            // Arrange
            bool defaultSuccess = false;
            Expression defaultValue = Expression.Constant(42);

            // Act
            var result = _compilationContext.CreateCompilationResult<int>(defaultSuccess, defaultValue);

            // Assert: Validate result components.
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.NotNull(result.Value);
            Assert.Contains("success", result.Success.Name);
            Assert.Contains("value", result.Value.Name);
            Assert.Contains(result.Success, result.Variables);
            Assert.Contains(result.Value, result.Variables);

            // Expect two assignments: one for success and one for the provided default value.
            Assert.Equal(2, result.Body.Count);

            // Validate the assignment for success.
            var assignSuccess = result.Body[0] as BinaryExpression;
            Assert.NotNull(assignSuccess);
            Assert.Equal(result.Success, assignSuccess.Left);
            var constantSuccess = assignSuccess.Right as ConstantExpression;
            Assert.NotNull(constantSuccess);
            Assert.Equal(defaultSuccess, constantSuccess.Value);

            // Validate the assignment for value with the provided defaultValue.
            var assignValue = result.Body[1] as BinaryExpression;
            Assert.NotNull(assignValue);
            Assert.Equal(result.Value, assignValue.Left);
            Assert.Equal(defaultValue, assignValue.Right);
        }

        /// <summary>
        /// Tests the non-generic CreateCompilationResult method without providing a default value.
        /// Verifies correct handling when a type is specified.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_NonGenericWithoutDefaultValue_ReturnsExpectedCompilationResult()
        {
            // Arrange
            bool defaultSuccess = true;
            Type valueType = typeof(string);

            // Act
            var result = _compilationContext.CreateCompilationResult(valueType, defaultSuccess);

            // Assert: Validate the properties of the result.
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.NotNull(result.Value);
            Assert.Equal(valueType, result.Value.Type);
            Assert.Contains("success", result.Success.Name);
            Assert.Contains("value", result.Value.Name);
            Assert.Contains(result.Success, result.Variables);
            Assert.Contains(result.Value, result.Variables);

            // With no default value provided, only the success variable should be assigned.
            Assert.Single(result.Body);
            var assignSuccess = result.Body[0] as BinaryExpression;
            Assert.NotNull(assignSuccess);
            Assert.Equal(result.Success, assignSuccess.Left);
            var constantExpr = assignSuccess.Right as ConstantExpression;
            Assert.NotNull(constantExpr);
            Assert.Equal(defaultSuccess, constantExpr.Value);
        }

        /// <summary>
        /// Tests the non-generic CreateCompilationResult method with a provided default value.
        /// Ensures that both success and value variables are assigned appropriately.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_NonGenericWithDefaultValue_ReturnsExpectedCompilationResult()
        {
            // Arrange
            bool defaultSuccess = false;
            Type valueType = typeof(double);
            Expression defaultValue = Expression.Constant(3.14);

            // Act
            var result = _compilationContext.CreateCompilationResult(valueType, defaultSuccess, defaultValue);

            // Assert: Validate that the result is properly initialized.
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.NotNull(result.Value);
            Assert.Equal(valueType, result.Value.Type);
            Assert.Contains("success", result.Success.Name);
            Assert.Contains("value", result.Value.Name);
            Assert.Contains(result.Success, result.Variables);
            Assert.Contains(result.Value, result.Variables);

            // Expect two assignments: one for success and one for assigning the provided default value.
            Assert.Equal(2, result.Body.Count);

            var assignSuccess = result.Body[0] as BinaryExpression;
            Assert.NotNull(assignSuccess);
            Assert.Equal(result.Success, assignSuccess.Left);
            var constantSuccess = assignSuccess.Right as ConstantExpression;
            Assert.NotNull(constantSuccess);
            Assert.Equal(defaultSuccess, constantSuccess.Value);

            var assignValue = result.Body[1] as BinaryExpression;
            Assert.NotNull(assignValue);
            Assert.Equal(result.Value, assignValue.Left);
            Assert.Equal(defaultValue, assignValue.Right);
        }
    }
}
