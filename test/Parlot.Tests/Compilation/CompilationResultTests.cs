using System;
using System.Linq.Expressions;
using Parlot.Compilation;
using Xunit;

namespace Parlot.Compilation.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CompilationResult"/> class.
    /// </summary>
//     public class CompilationResultTests [Error] (17-38)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     {
//         private readonly CompilationResult _compilationResult;
// 
//         public CompilationResultTests()
//         {
//             _compilationResult = new CompilationResult();
//         }
// 
//         /// <summary>
//         /// Tests that a newly created CompilationResult initializes empty Variables and Body collections.
//         /// </summary>
//         [Fact]
//         public void Constructor_InitializesEmptyCollections()
//         {
//             // Arrange & Act done in constructor
// 
//             // Assert
//             Assert.Empty(_compilationResult.Variables);
//             Assert.Empty(_compilationResult.Body);
//         }
// 
//         /// <summary>
//         /// Tests that the Success and Value properties can be set and retrieved correctly.
//         /// </summary>
//         [Fact]
//         public void Properties_SuccessAndValue_SetAndGetWorkCorrectly()
//         {
//             // Arrange
//             ParameterExpression successParam = Expression.Parameter(typeof(bool), "success");
//             ParameterExpression valueParam = Expression.Parameter(typeof(object), "value");
// 
//             // Act
//             _compilationResult.Success = successParam;
//             _compilationResult.Value = valueParam;
// 
//             // Assert
//             Assert.Equal(successParam, _compilationResult.Success);
//             Assert.Equal(valueParam, _compilationResult.Value);
//         }
// 
//         /// <summary>
//         /// Tests the DeclareVariable&lt;T&gt; method without providing a custom default value; expects the variable to be assigned the Expression.Default of type T.
//         /// </summary>
//         [Fact]
//         public void DeclareVariableT_WithoutCustomDefault_AddsVariableAndAssignsDefaultExpression()
//         {
//             // Arrange
//             string variableName = "testInt";
// 
//             // Act
//             ParameterExpression variable = _compilationResult.DeclareVariable<int>(variableName);
// 
//             // Assert
//             Assert.Contains(variable, _compilationResult.Variables);
//             Assert.NotEmpty(_compilationResult.Body);
//             var assignExpression = Assert.IsType<BinaryExpression>(_compilationResult.Body[^1]);
//             Assert.Equal(ExpressionType.Assign, assignExpression.NodeType);
//             Assert.Equal(variable, assignExpression.Left);
// 
//             // Verify that the right side is a DefaultExpression with the correct type.
//             var defaultExpr = Assert.IsType<DefaultExpression>(assignExpression.Right);
//             Assert.Equal(typeof(int), defaultExpr.Type);
//         }
// 
//         /// <summary>
//         /// Tests the DeclareVariable&lt;T&gt; method with a custom default value; expects the variable to be assigned the provided default expression.
//         /// </summary>
//         [Fact]
//         public void DeclareVariableT_WithCustomDefault_AddsVariableAndAssignsProvidedDefaultExpression()
//         {
//             // Arrange
//             string variableName = "testInt";
//             Expression customDefault = Expression.Constant(42);
// 
//             // Act
//             ParameterExpression variable = _compilationResult.DeclareVariable<int>(variableName, customDefault);
// 
//             // Assert
//             Assert.Contains(variable, _compilationResult.Variables);
//             Assert.NotEmpty(_compilationResult.Body);
//             var assignExpression = Assert.IsType<BinaryExpression>(_compilationResult.Body[^1]);
//             Assert.Equal(ExpressionType.Assign, assignExpression.NodeType);
//             Assert.Equal(variable, assignExpression.Left);
// 
//             // Verify that the right side is the same custom default provided.
//             var constantExpr = Assert.IsType<ConstantExpression>(assignExpression.Right);
//             Assert.Equal(42, constantExpr.Value);
//             Assert.Equal(typeof(int), constantExpr.Type);
//         }
// 
//         /// <summary>
//         /// Tests the non-generic DeclareVariable method when a null type is provided; expects an ArgumentNullException.
//         /// </summary>
//         [Fact]
//         public void DeclareVariable_NullType_ThrowsArgumentNullException()
//         {
//             // Arrange
//             string variableName = "nullTypeVar";
//             Type nullType = null;
// 
//             // Act & Assert
//             Assert.Throws<ArgumentNullException>(() => _compilationResult.DeclareVariable(variableName, nullType));
//         }
// 
//         /// <summary>
//         /// Tests the non-generic DeclareVariable method with a custom default expression that is incompatible with the specified type; expects an ArgumentException.
//         /// </summary>
//         [Fact]
//         public void DeclareVariable_IncompatibleCustomDefault_ThrowsArgumentException()
//         {
//             // Arrange
//             string variableName = "incompatibleVar";
//             // Trying to assign a string constant to an int variable, which is incompatible.
//             Expression incompatibleDefault = Expression.Constant("not an int");
// 
//             // Act & Assert
//             Assert.Throws<ArgumentException>(() => _compilationResult.DeclareVariable(variableName, typeof(int), incompatibleDefault));
//         }
//     }
}
