using System;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilationContextTests"/> class.
        /// </summary>
        public CompilationContextTests()
        {
            _compilationContext = new CompilationContext();
        }

        /// <summary>
        /// Tests that the ParseContext property is initialized correctly with a ParameterExpression of type ParseContext.
        /// </summary>
        [Fact]
        public void ParseContext_WhenAccessed_ReturnsParameterExpressionWithCorrectType()
        {
            // Act
            ParameterExpression parseContext = _compilationContext.ParseContext;

            // Assert
            Assert.NotNull(parseContext);
            Assert.Equal(typeof(ParseContext), parseContext.Type);
        }

        /// <summary>
        /// Tests that calling NextNumber multiple times returns incrementing values.
        /// </summary>
        [Fact]
        public void NextNumber_MultipleCalls_ReturnsIncrementingValues()
        {
            // Act
            int firstNumber = _compilationContext.NextNumber;
            int secondNumber = _compilationContext.NextNumber;
            int thirdNumber = _compilationContext.NextNumber;

            // Assert
            Assert.Equal(firstNumber + 1, secondNumber);
            Assert.Equal(secondNumber + 1, thirdNumber);
        }

        /// <summary>
        /// Tests that the GlobalVariables property returns an empty list upon initialization.
        /// </summary>
        [Fact]
        public void GlobalVariables_OnInitialization_IsEmpty()
        {
            // Act
            List<ParameterExpression> globalVariables = _compilationContext.GlobalVariables;

            // Assert
            Assert.NotNull(globalVariables);
            Assert.Empty(globalVariables);
        }

        /// <summary>
        /// Tests that the GlobalExpressions property returns an empty list upon initialization.
        /// </summary>
        [Fact]
        public void GlobalExpressions_OnInitialization_IsEmpty()
        {
            // Act
            List<Expression> globalExpressions = _compilationContext.GlobalExpressions;

            // Assert
            Assert.NotNull(globalExpressions);
            Assert.Empty(globalExpressions);
        }

        /// <summary>
        /// Tests that the Lambdas property returns an empty list upon initialization.
        /// </summary>
        [Fact]
        public void Lambdas_OnInitialization_IsEmpty()
        {
            // Act
            List<Expression> lambdas = _compilationContext.Lambdas;

            // Assert
            Assert.NotNull(lambdas);
            Assert.Empty(lambdas);
        }

        /// <summary>
        /// Tests that the DiscardResult property can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void DiscardResult_SetAndGet_WorksCorrectly()
        {
            // Act
            _compilationContext.DiscardResult = true;
            bool valueAfterSettingTrue = _compilationContext.DiscardResult;

            _compilationContext.DiscardResult = false;
            bool valueAfterSettingFalse = _compilationContext.DiscardResult;

            // Assert
            Assert.True(valueAfterSettingTrue);
            Assert.False(valueAfterSettingFalse);
        }

        /// <summary>
        /// Tests the CreateCompilationResult&lt;TValue&gt; method without providing a default value.
        /// Verifies that the compilation result contains the correct variables and a single assignment in the body.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_GenericWithoutDefaultValue_ReturnsResultWithOneBodyAssignment()
        {
            // Arrange
            bool defaultSuccess = true;

            // Act
            CompilationResult result = _compilationContext.CreateCompilationResult<int>(defaultSuccess);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.NotNull(result.Value);
            Assert.Contains(result.Success, result.Variables);
            Assert.Contains(result.Value, result.Variables);
            Assert.Single(result.Body);

            // Verify that the first body assignment assigns the defaultSuccess value to the success variable.
            BinaryExpression successAssignment = Assert.IsAssignableFrom<BinaryExpression>(result.Body[0]);
            Assert.Equal(result.Success, successAssignment.Left);
            ConstantExpression successConst = Assert.IsAssignableFrom<ConstantExpression>(successAssignment.Right);
            Assert.Equal(defaultSuccess, successConst.Value);
        }

        /// <summary>
        /// Tests the CreateCompilationResult&lt;TValue&gt; method when a default value is provided.
        /// Verifies that the compilation result contains the correct variables and two assignments in the body.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_GenericWithDefaultValue_ReturnsResultWithTwoBodyAssignments()
        {
            // Arrange
            bool defaultSuccess = false;
            Expression defaultValue = Expression.Constant(42);

            // Act
            CompilationResult result = _compilationContext.CreateCompilationResult<int>(defaultSuccess, defaultValue);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Body.Count);

            // First body assignment should assign defaultSuccess to the success variable.
            BinaryExpression successAssignment = Assert.IsAssignableFrom<BinaryExpression>(result.Body[0]);
            Assert.Equal(result.Success, successAssignment.Left);
            ConstantExpression successConst = Assert.IsAssignableFrom<ConstantExpression>(successAssignment.Right);
            Assert.Equal(defaultSuccess, successConst.Value);

            // Second body assignment should assign the provided default value to the value variable.
            BinaryExpression valueAssignment = Assert.IsAssignableFrom<BinaryExpression>(result.Body[1]);
            Assert.Equal(result.Value, valueAssignment.Left);
            ConstantExpression valueConst = Assert.IsAssignableFrom<ConstantExpression>(valueAssignment.Right);
            Assert.Equal(42, valueConst.Value);
        }

        /// <summary>
        /// Tests the CreateCompilationResult method with a Type parameter.
        /// Verifies that the returned compilation result has a Value variable of the specified type and both assignments in its body.
        /// </summary>
        [Fact]
        public void CreateCompilationResult_WithTypeParameter_ReturnsResultWithCorrectValueType()
        {
            // Arrange
            bool defaultSuccess = true;
            Expression defaultValue = Expression.Constant("default");

            // Act
            CompilationResult result = _compilationContext.CreateCompilationResult(typeof(string), defaultSuccess, defaultValue);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Success);
            Assert.NotNull(result.Value);
            Assert.Equal(typeof(string), result.Value.Type);
            Assert.Equal(2, result.Body.Count);

            BinaryExpression successAssignment = Assert.IsAssignableFrom<BinaryExpression>(result.Body[0]);
            ConstantExpression successConst = Assert.IsAssignableFrom<ConstantExpression>(successAssignment.Right);
            Assert.Equal(defaultSuccess, successConst.Value);

            BinaryExpression valueAssignment = Assert.IsAssignableFrom<BinaryExpression>(result.Body[1]);
            ConstantExpression valueConst = Assert.IsAssignableFrom<ConstantExpression>(valueAssignment.Right);
            Assert.Equal("default", valueConst.Value);
        }
    }
}
