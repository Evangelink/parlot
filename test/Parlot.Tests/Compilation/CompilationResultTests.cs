using System.Collections.Generic;
using System.Linq.Expressions;
using Parlot.Compilation;
using Xunit;

namespace Parlot.Compilation.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CompilationResult"/> class.
    /// </summary>
    public class CompilationResultTests
    {
        private readonly CompilationResult _compilationResult;

        public CompilationResultTests()
        {
            _compilationResult = new CompilationResult();
        }

        /// <summary>
        /// Tests that the generic DeclareVariable method with a provided defaultValue updates the Variables and Body collections correctly.
        /// </summary>
        [Fact]
        public void DeclareVariableGeneric_WithDefaultValueProvided_UpdatesCollections()
        {
            // Arrange
            string variableName = "var1";
            var defaultValue = Expression.Constant(42);

            // Act
            var variable = _compilationResult.DeclareVariable<int>(variableName, defaultValue);

            // Assert
            Assert.NotNull(variable);
            Assert.Equal(typeof(int), variable.Type);
            Assert.Equal(variableName, variable.Name);
            Assert.Contains(variable, _compilationResult.Variables);
            Assert.NotEmpty(_compilationResult.Body);

            var assignmentExpression = Assert.IsType<BinaryExpression>(_compilationResult.Body[^1]);
            Assert.Equal(ExpressionType.Assign, assignmentExpression.NodeType);
            Assert.Equal(variable, assignmentExpression.Left);
            Assert.Equal(defaultValue, assignmentExpression.Right);
        }

        /// <summary>
        /// Tests that the generic DeclareVariable method with a null defaultValue creates a default assignment expression.
        /// </summary>
        [Fact]
        public void DeclareVariableGeneric_WithNullDefaultValue_CreatesDefaultAssignment()
        {
            // Arrange
            string variableName = "var2";

            // Act
            var variable = _compilationResult.DeclareVariable<int>(variableName);

            // Assert
            Assert.NotNull(variable);
            Assert.Equal(typeof(int), variable.Type);
            Assert.Equal(variableName, variable.Name);
            Assert.Contains(variable, _compilationResult.Variables);
            Assert.NotEmpty(_compilationResult.Body);

            var assignmentExpression = Assert.IsType<BinaryExpression>(_compilationResult.Body[^1]);
            Assert.Equal(ExpressionType.Assign, assignmentExpression.NodeType);
            Assert.Equal(variable, assignmentExpression.Left);

            var defaultExpression = Assert.IsType<DefaultExpression>(assignmentExpression.Right);
            Assert.Equal(typeof(int), defaultExpression.Type);
        }

        /// <summary>
        /// Tests that the non-generic DeclareVariable method with a provided defaultValue updates the Variables and Body collections correctly.
        /// </summary>
        [Fact]
        public void DeclareVariableNonGeneric_WithProvidedDefaultValue_UpdatesCollections()
        {
            // Arrange
            string variableName = "var3";
            Type variableType = typeof(string);
            var defaultValue = Expression.Constant("test");

            // Act
            var variable = _compilationResult.DeclareVariable(variableName, variableType, defaultValue);

            // Assert
            Assert.NotNull(variable);
            Assert.Equal(variableType, variable.Type);
            Assert.Equal(variableName, variable.Name);
            Assert.Contains(variable, _compilationResult.Variables);
            Assert.NotEmpty(_compilationResult.Body);

            var assignmentExpression = Assert.IsType<BinaryExpression>(_compilationResult.Body[^1]);
            Assert.Equal(ExpressionType.Assign, assignmentExpression.NodeType);
            Assert.Equal(variable, assignmentExpression.Left);
            Assert.Equal(defaultValue, assignmentExpression.Right);
        }

        /// <summary>
        /// Tests that the non-generic DeclareVariable method with a null defaultValue creates a default assignment expression.
        /// </summary>
        [Fact]
        public void DeclareVariableNonGeneric_WithNullDefaultValue_CreatesDefaultAssignment()
        {
            // Arrange
            string variableName = "var4";
            Type variableType = typeof(double);

            // Act
            var variable = _compilationResult.DeclareVariable(variableName, variableType);

            // Assert
            Assert.NotNull(variable);
            Assert.Equal(variableType, variable.Type);
            Assert.Equal(variableName, variable.Name);
            Assert.Contains(variable, _compilationResult.Variables);
            Assert.NotEmpty(_compilationResult.Body);

            var assignmentExpression = Assert.IsType<BinaryExpression>(_compilationResult.Body[^1]);
            Assert.Equal(ExpressionType.Assign, assignmentExpression.NodeType);
            Assert.Equal(variable, assignmentExpression.Left);

            var defaultExpression = Assert.IsType<DefaultExpression>(assignmentExpression.Right);
            Assert.Equal(variableType, defaultExpression.Type);
        }

        /// <summary>
        /// Tests that multiple calls to DeclareVariable methods update the Variables and Body collections correctly.
        /// </summary>
        [Fact]
        public void DeclareVariable_MultipleCalls_UpdatesCollectionsCorrectly()
        {
            // Arrange
            int initialVariableCount = _compilationResult.Variables.Count;
            int initialBodyCount = _compilationResult.Body.Count;
            
            // Act
            var intVar = _compilationResult.DeclareVariable<int>("intVar", Expression.Constant(100));
            var stringVar = _compilationResult.DeclareVariable("stringVar", typeof(string), Expression.Constant("hello"));

            // Assert
            Assert.Equal(initialVariableCount + 2, _compilationResult.Variables.Count);
            Assert.Equal(initialBodyCount + 2, _compilationResult.Body.Count);
            Assert.Contains(intVar, _compilationResult.Variables);
            Assert.Contains(stringVar, _compilationResult.Variables);
        }

        /// <summary>
        /// Tests that passing a null type to the non-generic DeclareVariable method throws an ArgumentNullException.
        /// </summary>
        [Fact]
        public void DeclareVariableNonGeneric_WithNullType_ThrowsArgumentNullException()
        {
            // Arrange
            string variableName = "varNull";
            Type nullType = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _compilationResult.DeclareVariable(variableName, nullType));
        }

        /// <summary>
        /// Tests that the Success and Value properties can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void SetAndGet_SuccessAndValueProperties_AreSetCorrectly()
        {
            // Arrange
            var successParam = Expression.Parameter(typeof(bool), "success");
            var valueParam = Expression.Parameter(typeof(bool), "value");

            // Act
            _compilationResult.Success = successParam;
            _compilationResult.Value = valueParam;

            // Assert
            Assert.Equal(successParam, _compilationResult.Success);
            Assert.Equal(valueParam, _compilationResult.Value);
        }
    }
}
