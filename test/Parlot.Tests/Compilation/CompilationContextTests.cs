using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "CompilationContext"/> class focusing on the CreateCompilationResult methods.
/// </summary>
// public class CompilationContextTests [Error] (543-2)CS1038 #endregion directive expected
// {
//     /// <summary>
//     /// Tests the generic CreateCompilationResult method with default parameters.
//     /// This test verifies that when no values are provided for defaultSuccess and defaultValue,
//     /// the resulting CompilationResult has a Success of false and a null Value.
//     /// </summary>
//     [Fact] [Error] (27-22)CS1503 Argument 1: cannot convert from 'System.Linq.Expressions.ParameterExpression' to 'bool'
//     public void CreateCompilationResult_GenericMethod_WithDefaultParameters_ReturnsCompilationResultWithDefaults()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         // Act
//         CompilationResult result = context.CreateCompilationResult<int>();
//         // Assert
//         Assert.NotNull(result);
//         Assert.False(result.Success);
//         Assert.Null(result.Value);
//     }
// 
//     /// <summary>
//     /// Tests the generic CreateCompilationResult method when specific default values are provided.
//     /// This test verifies that the created CompilationResult reflects the provided defaultSuccess and defaultValue.
//     /// </summary>
//     [Fact] [Error] (46-22)CS1503 Argument 1: cannot convert from 'bool' to 'string?' [Error] (46-39)CS1503 Argument 2: cannot convert from 'System.Linq.Expressions.ParameterExpression' to 'string?'
//     public void CreateCompilationResult_GenericMethod_WithSpecifiedDefaults_ReturnsCompilationResultWithProvidedDefaults()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         Expression expectedExpression = Expression.Constant(42);
//         bool expectedSuccess = true;
//         // Act
//         CompilationResult result = context.CreateCompilationResult<int>(expectedSuccess, expectedExpression);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(expectedSuccess, result.Success);
//         Assert.Equal(expectedExpression, result.Value);
//     }
// 
//     /// <summary>
//     /// Tests the typed CreateCompilationResult method with default parameters.
//     /// This test verifies that when no values are provided for defaultSuccess and defaultValue,
//     /// the resulting CompilationResult has a Success of false and a null Value.
//     /// </summary>
//     [Fact] [Error] (64-22)CS1503 Argument 1: cannot convert from 'System.Linq.Expressions.ParameterExpression' to 'bool'
//     public void CreateCompilationResult_TypedMethod_WithDefaultParameters_ReturnsCompilationResultWithDefaults()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         // Act
//         CompilationResult result = context.CreateCompilationResult(typeof(string));
//         // Assert
//         Assert.NotNull(result);
//         Assert.False(result.Success);
//         Assert.Null(result.Value);
//     }
// 
//     /// <summary>
//     /// Tests the typed CreateCompilationResult method when specific default values are provided.
//     /// This test verifies that the created CompilationResult correctly reflects the provided defaultSuccess and defaultValue.
//     /// </summary>
//     [Fact] [Error] (83-22)CS1503 Argument 1: cannot convert from 'bool' to 'string?' [Error] (83-39)CS1503 Argument 2: cannot convert from 'System.Linq.Expressions.ParameterExpression' to 'string?'
//     public void CreateCompilationResult_TypedMethod_WithSpecifiedDefaults_ReturnsCompilationResultWithProvidedDefaults()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         Expression expectedExpression = Expression.Constant("test");
//         bool expectedSuccess = true;
//         // Act
//         CompilationResult result = context.CreateCompilationResult(typeof(string), expectedSuccess, expectedExpression);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(expectedSuccess, result.Success);
//         Assert.Equal(expectedExpression, result.Value);
//     }
// 
// #region Non Generic Overload Tests
//     /// <summary>
//     /// Tests the non-generic CreateCompilationResult method when no default value is provided.
//     /// Expected outcome: The returned CompilationResult should contain two variables (success and value) and one assignment in the body (for success).
//     /// </summary>
//     [Fact]
//     public void CreateCompilationResult_NonGeneric_NoDefaultValue_ReturnsCompilationResultWithOnlySuccessAssignment()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         var valueType = typeof(int);
//         bool defaultSuccess = true;
//         Expression defaultValue = null;
//         // Act
//         var result = context.CreateCompilationResult(valueType, defaultSuccess, defaultValue);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotNull(result.Success);
//         Assert.NotNull(result.Value);
//         // Verify that two variables (success and value) are created.
//         Assert.NotNull(result.Variables);
//         Assert.Equal(2, result.Variables.Count);
//         Assert.Equal(typeof(bool), result.Variables[0].Type);
//         Assert.Contains("success", result.Variables[0].Name, StringComparison.OrdinalIgnoreCase);
//         Assert.Equal(valueType, result.Variables[1].Type);
//         Assert.Contains("value", result.Variables[1].Name, StringComparison.OrdinalIgnoreCase);
//         // Verify that the body has exactly one assignment (for success variable).
//         Assert.NotNull(result.Body);
//         Assert.Single(result.Body);
//         var successAssign = result.Body[0] as BinaryExpression;
//         Assert.NotNull(successAssign);
//         // Check left side of assignment is the success variable.
//         Assert.Equal(result.Success, successAssign.Left);
//         // Check right side is a constant expression with the defaultSuccess value.
//         var constantExpr = successAssign.Right as ConstantExpression;
//         Assert.NotNull(constantExpr);
//         Assert.Equal(defaultSuccess, constantExpr.Value);
//     }
// 
//     /// <summary>
//     /// Tests the non-generic CreateCompilationResult method when a default value is provided.
//     /// Expected outcome: The returned CompilationResult should contain two variables (success and value) and two assignments in the body 
//     /// (one for success and one for the value variable with the provided default value).
//     /// </summary>
//     [Fact]
//     public void CreateCompilationResult_NonGeneric_WithDefaultValue_ReturnsCompilationResultWithValueAssignment()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         var valueType = typeof(int);
//         bool defaultSuccess = false;
//         var defaultConst = 42;
//         Expression defaultValue = Expression.Constant(defaultConst, valueType);
//         // Act
//         var result = context.CreateCompilationResult(valueType, defaultSuccess, defaultValue);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotNull(result.Success);
//         Assert.NotNull(result.Value);
//         // Verify variables count and their types.
//         Assert.NotNull(result.Variables);
//         Assert.Equal(2, result.Variables.Count);
//         Assert.Equal(typeof(bool), result.Variables[0].Type);
//         Assert.Contains("success", result.Variables[0].Name, StringComparison.OrdinalIgnoreCase);
//         Assert.Equal(valueType, result.Variables[1].Type);
//         Assert.Contains("value", result.Variables[1].Name, StringComparison.OrdinalIgnoreCase);
//         // Verify that the body has exactly two assignments: one for the success variable and one for the value variable.
//         Assert.NotNull(result.Body);
//         Assert.Equal(2, result.Body.Count);
//         // Verify assignment for success variable.
//         var successAssign = result.Body[0] as BinaryExpression;
//         Assert.NotNull(successAssign);
//         Assert.Equal(result.Success, successAssign.Left);
//         var successConst = successAssign.Right as ConstantExpression;
//         Assert.NotNull(successConst);
//         Assert.Equal(defaultSuccess, successConst.Value);
//         // Verify assignment for value variable.
//         var valueAssign = result.Body[1] as BinaryExpression;
//         Assert.NotNull(valueAssign);
//         Assert.Equal(result.Value, valueAssign.Left);
//         // The right-hand side should match the provided defaultValue expression.
//         if (defaultValue is ConstantExpression defaultConstExpr)
//         {
//             var valueRightConst = valueAssign.Right as ConstantExpression;
//             Assert.NotNull(valueRightConst);
//             Assert.Equal(defaultConstExpr.Value, valueRightConst.Value);
//         }
//         else
//         {
//             Assert.True(false, "Default value expression is not a ConstantExpression as expected.");
//         }
//     }
// 
// #endregion
// #region Generic Overload Tests
//     /// <summary>
//     /// Tests the generic CreateCompilationResult method when no default value is provided.
//     /// Expected outcome: The returned CompilationResult should contain two variables (success and value) and one assignment in the body (for success).
//     /// The value variable should be of the specified generic type.
//     /// </summary>
//     [Fact]
//     public void CreateCompilationResult_Generic_NoDefaultValue_ReturnsCompilationResultWithOnlySuccessAssignment()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         bool defaultSuccess = true;
//         Expression defaultValue = null;
//         // Act
//         var result = context.CreateCompilationResult<int>(defaultSuccess, defaultValue);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotNull(result.Success);
//         Assert.NotNull(result.Value);
//         // Validate that the value variable is of type int.
//         Assert.Equal(typeof(int), result.Value.Type);
//         // Verify that two variables (success and value) are created.
//         Assert.NotNull(result.Variables);
//         Assert.Equal(2, result.Variables.Count);
//         Assert.Equal(typeof(bool), result.Variables[0].Type);
//         Assert.Contains("success", result.Variables[0].Name, StringComparison.OrdinalIgnoreCase);
//         Assert.Equal(typeof(int), result.Variables[1].Type);
//         Assert.Contains("value", result.Variables[1].Name, StringComparison.OrdinalIgnoreCase);
//         // Verify that the body has exactly one assignment (for the success variable).
//         Assert.NotNull(result.Body);
//         Assert.Single(result.Body);
//         var successAssign = result.Body[0] as BinaryExpression;
//         Assert.NotNull(successAssign);
//         Assert.Equal(result.Success, successAssign.Left);
//         var constantExpr = successAssign.Right as ConstantExpression;
//         Assert.NotNull(constantExpr);
//         Assert.Equal(defaultSuccess, constantExpr.Value);
//     }
// 
//     /// <summary>
//     /// Tests the generic CreateCompilationResult method when a default value is provided.
//     /// Expected outcome: The returned CompilationResult should contain two variables (success and value) and two assignments in the body 
//     /// (one for success and one for the value variable with the provided default value). The value variable should be of the specified generic type.
//     /// </summary>
//     [Fact]
//     public void CreateCompilationResult_Generic_WithDefaultValue_ReturnsCompilationResultWithValueAssignment()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         bool defaultSuccess = false;
//         int defaultConst = 100;
//         Expression defaultValue = Expression.Constant(defaultConst, typeof(int));
//         // Act
//         var result = context.CreateCompilationResult<int>(defaultSuccess, defaultValue);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotNull(result.Success);
//         Assert.NotNull(result.Value);
//         // Validate that the value variable is of type int.
//         Assert.Equal(typeof(int), result.Value.Type);
//         // Verify variables and their properties.
//         Assert.NotNull(result.Variables);
//         Assert.Equal(2, result.Variables.Count);
//         Assert.Equal(typeof(bool), result.Variables[0].Type);
//         Assert.Contains("success", result.Variables[0].Name, StringComparison.OrdinalIgnoreCase);
//         Assert.Equal(typeof(int), result.Variables[1].Type);
//         Assert.Contains("value", result.Variables[1].Name, StringComparison.OrdinalIgnoreCase);
//         // Verify that the body has exactly two assignments.
//         Assert.NotNull(result.Body);
//         Assert.Equal(2, result.Body.Count);
//         // Check assignment for success variable.
//         var successAssign = result.Body[0] as BinaryExpression;
//         Assert.NotNull(successAssign);
//         Assert.Equal(result.Success, successAssign.Left);
//         var successConst = successAssign.Right as ConstantExpression;
//         Assert.NotNull(successConst);
//         Assert.Equal(defaultSuccess, successConst.Value);
//         // Check assignment for value variable.
//         var valueAssign = result.Body[1] as BinaryExpression;
//         Assert.NotNull(valueAssign);
//         Assert.Equal(result.Value, valueAssign.Left);
//         if (defaultValue is ConstantExpression defaultConstExpr)
//         {
//             var valueRightConst = valueAssign.Right as ConstantExpression;
//             Assert.NotNull(valueRightConst);
//             Assert.Equal(defaultConstExpr.Value, valueRightConst.Value);
//         }
//         else
//         {
//             Assert.True(false, "Default value expression is not a ConstantExpression as expected.");
//         }
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "CompilationContext"/>'s constructor to ensure that all properties are initialized with the expected default values.
//     /// This includes initializing ParseContext (as a ParameterExpression of type ParseContext),
//     /// setting NextNumber to 0, initializing GlobalVariables, GlobalExpressions and Lambdas as empty lists,
//     /// and ensuring DiscardResult is set to its default value (false).
//     /// </summary>
//     [Fact]
//     public void Constructor_WhenCalled_InitializesPropertiesCorrectly()
//     {
//         // Arrange & Act
//         var context = new CompilationContext();
//         // Assert
//         // Assert that the ParseContext is not null and is a ParameterExpression of type "ParseContext".
//         Assert.NotNull(context.ParseContext);
//         Assert.IsType<ParameterExpression>(context.ParseContext);
//         Assert.Equal("ParseContext", context.ParseContext.Type.Name);
//         // Assert that NextNumber defaults to 0.
//         Assert.Equal(0, context.NextNumber);
//         // Assert that GlobalVariables is initialized as an empty list.
//         Assert.NotNull(context.GlobalVariables);
//         Assert.Empty(context.GlobalVariables);
//         // Assert that GlobalExpressions is initialized as an empty list.
//         Assert.NotNull(context.GlobalExpressions);
//         Assert.Empty(context.GlobalExpressions);
//         // Assert that Lambdas is initialized as an empty list.
//         Assert.NotNull(context.Lambdas);
//         Assert.Empty(context.Lambdas);
//         // Assert that DiscardResult defaults to false.
//         Assert.False(context.DiscardResult);
//     }
// 
//     /// <summary>
//     /// Tests that the ParseContext property returns a non-null instance of ParameterExpression.
//     /// </summary>
//     [Fact]
//     public void ParseContext_WhenAccessed_ReturnsNonNullParameterExpression()
//     {
//         // Arrange
//         var compilationContext = new CompilationContext();
//         // Act
//         var parseContext = compilationContext.ParseContext;
//         // Assert
//         Assert.NotNull(parseContext);
//         Assert.IsAssignableFrom<ParameterExpression>(parseContext);
//     }
// 
//     /// <summary>
//     /// Tests that the ParseContext property returns a ParameterExpression with the correct type.
//     /// </summary>
//     [Fact]
//     public void ParseContext_WhenAccessed_ReturnsParameterExpressionOfCorrectType()
//     {
//         // Arrange
//         var compilationContext = new CompilationContext();
//         // Act
//         var parseContext = compilationContext.ParseContext;
//         // Assert
//         Assert.Equal(typeof(ParseContext), parseContext.Type);
//     }
// 
//     /// <summary>
//     /// Tests that the initial call to the NextNumber property returns 0.
//     /// </summary>
//     [Fact]
//     public void NextNumber_WhenCalledOnce_ReturnsZero()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         // Act
//         int firstValue = context.NextNumber;
//         // Assert
//         Assert.Equal(0, firstValue);
//     }
// 
//     /// <summary>
//     /// Tests that multiple sequential calls to the NextNumber property return an incrementing sequence.
//     /// </summary>
//     [Fact]
//     public void NextNumber_WhenCalledMultipleTimes_ReturnsIncrementalSequence()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         // Act
//         int firstValue = context.NextNumber;
//         int secondValue = context.NextNumber;
//         int thirdValue = context.NextNumber;
//         // Assert
//         Assert.Equal(0, firstValue);
//         Assert.Equal(1, secondValue);
//         Assert.Equal(2, thirdValue);
//     }
// 
//     /// <summary>
//     /// Tests that many sequential calls to the NextNumber property produce unique, incremented values.
//     /// </summary>
//     [Fact]
//     public void NextNumber_WhenCalledRepeatedly_ProducesUniqueIncrementedValues()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         const int iterations = 100;
//         int[] numbers = new int[iterations];
//         // Act
//         for (int i = 0; i < iterations; i++)
//         {
//             numbers[i] = context.NextNumber;
//         }
// 
//         // Assert
//         for (int i = 0; i < iterations; i++)
//         {
//             Assert.Equal(i, numbers[i]);
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the GlobalVariables property is not null when a new CompilationContext is instantiated.
//     /// </summary>
//     [Fact]
//     public void GlobalVariables_WhenInstanceCreated_IsNotNull()
//     {
//         // Arrange & Act
//         var context = new CompilationContext();
//         // Assert
//         Assert.NotNull(context.GlobalVariables);
//     }
// 
//     /// <summary>
//     /// Tests that the GlobalVariables property is empty upon instance creation.
//     /// </summary>
//     [Fact]
//     public void GlobalVariables_WhenInstanceCreated_IsEmpty()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         // Act
//         var globalVariables = context.GlobalVariables;
//         // Assert
//         Assert.Empty(globalVariables);
//     }
// 
//     /// <summary>
//     /// Tests that items can be added to the GlobalVariables list and that the same instance is returned on multiple accesses.
//     /// </summary>
//     [Fact]
//     public void GlobalVariables_CanAddParameterExpression_AndRetainsSameInstance()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         var parameterExpr = Expression.Parameter(typeof(int), "testParam");
//         // Act
//         context.GlobalVariables.Add(parameterExpr);
//         var retrievedList = context.GlobalVariables;
//         // Assert
//         Assert.Single(retrievedList);
//         Assert.Contains(parameterExpr, retrievedList);
//         // Ensure same instance is returned on multiple calls
//         Assert.Same(retrievedList, context.GlobalVariables);
//     }
// 
//     /// <summary>
//     /// Tests that the GlobalExpressions property returns a non-null and empty list when a new context is instantiated.
//     /// </summary>
//     [Fact]
//     public void GlobalExpressions_GetterInitialState_ReturnsEmptyList()
//     {
//         // Arrange
//         var compilationContext = new CompilationContext();
//         // Act
//         List<Expression> globalExpressions = compilationContext.GlobalExpressions;
//         // Assert
//         Assert.NotNull(globalExpressions);
//         Assert.Empty(globalExpressions);
//     }
// 
//     /// <summary>
//     /// Tests that an expression can be successfully added to the GlobalExpressions list.
//     /// </summary>
//     [Fact]
//     public void GlobalExpressions_AddExpression_ListReflectsAddition()
//     {
//         // Arrange
//         var compilationContext = new CompilationContext();
//         Expression expectedExpression = Expression.Constant(42);
//         // Act
//         compilationContext.GlobalExpressions.Add(expectedExpression);
//         // Assert
//         Assert.Single(compilationContext.GlobalExpressions);
//         Assert.Contains(expectedExpression, compilationContext.GlobalExpressions);
//     }
// 
//     /// <summary>
//     /// Tests that modifications to the GlobalExpressions list in one instance of CompilationContext do not affect another instance.
//     /// </summary>
//     [Fact]
//     public void GlobalExpressions_MultipleInstances_IndependentLists()
//     {
//         // Arrange
//         var context1 = new CompilationContext();
//         var context2 = new CompilationContext();
//         Expression expressionForContext1 = Expression.Constant("Test");
//         // Act
//         context1.GlobalExpressions.Add(expressionForContext1);
//         // Assert
//         Assert.Single(context1.GlobalExpressions);
//         Assert.Empty(context2.GlobalExpressions);
//     }
// 
//     /// <summary>
//     /// Tests that the Lambdas property returns a non-null empty list upon initialization.
//     /// </summary>
//     [Fact]
//     public void Lambdas_InitialState_ReturnsNonNullEmptyList()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         // Act
//         List<Expression> lambdasList = context.Lambdas;
//         // Assert
//         Assert.NotNull(lambdasList);
//         Assert.Empty(lambdasList);
//     }
// 
//     /// <summary>
//     /// Tests that the Lambdas property list is modifiable and reflects added expressions.
//     /// </summary>
//     [Fact]
//     public void Lambdas_WhenExpressionAdded_ListReflectsAddedExpression()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         Expression testExpression = Expression.Constant(42);
//         // Act
//         context.Lambdas.Add(testExpression);
//         // Assert
//         Assert.Single(context.Lambdas);
//         Assert.Contains(testExpression, context.Lambdas);
//     }
// 
//     /// <summary>
//     /// Verifies that the default value of the DiscardResult property is false.
//     /// </summary>
//     [Fact]
//     public void DiscardResult_DefaultValue_ReturnsFalse()
//     {
//         // Arrange & Act
//         var context = new CompilationContext();
//         // Assert
//         Assert.False(context.DiscardResult, "Expected default value of DiscardResult to be false.");
//     }
// 
//     /// <summary>
//     /// Verifies that setting the DiscardResult property to true or false returns the expected values.
//     /// </summary>
//     [Fact]
//     public void DiscardResult_SetValue_ReturnsExpectedValue()
//     {
//         // Arrange
//         var context = new CompilationContext();
//         // Act - Set DiscardResult to true and verify
//         context.DiscardResult = true;
//         bool valueAfterTrue = context.DiscardResult;
//         // Act - Set DiscardResult to false and verify
//         context.DiscardResult = false;
//         bool valueAfterFalse = context.DiscardResult;
//         // Assert
//         Assert.True(valueAfterTrue, "Expected DiscardResult to be true after setting it to true.");
//         Assert.False(valueAfterFalse, "Expected DiscardResult to be false after setting it to false.");
//     }
// }