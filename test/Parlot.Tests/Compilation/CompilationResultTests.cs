using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilationResultTests"/> class.
        /// Sets required properties on the <see cref="CompilationResult"/> instance.
        /// </summary>
        public CompilationResultTests()
        {
            _compilationResult = new CompilationResult();
            _compilationResult.Success = Expression.Parameter(typeof(bool), "success");
            _compilationResult.Value = Expression.Parameter(typeof(bool), "value");
        }

        /// <summary>
        /// Tests the generic DeclareVariable method with a provided default value.
        /// Verifies that the variable is created with the correct name, type, and assigned the provided default value.
        /// </summary>
        [Fact]
        public void DeclareVariable_Generic_WithDefaultValue_ReturnsExpectedVariable()
        {
            // Arrange
            string variableName = "intVar";
            Expression defaultValue = Expression.Constant(10);
            int initialVariableCount = _compilationResult.Variables.Count;
            int initialBodyCount = _compilationResult.Body.Count;

            // Act
            ParameterExpression variable = _compilationResult.DeclareVariable<int>(variableName, defaultValue);

            // Assert
            Assert.NotNull(variable);
            Assert.Equal(variableName, variable.Name);
            Assert.Equal(typeof(int), variable.Type);
            Assert.Equal(initialVariableCount + 1, _compilationResult.Variables.Count);
            Assert.Equal(initialBodyCount + 1, _compilationResult.Body.Count);

            BinaryExpression assignment = Assert.IsType<BinaryExpression>(_compilationResult.Body.Last());
            Assert.Equal(variable, assignment.Left);
            Assert.Equal(defaultValue, assignment.Right);
        }

        /// <summary>
        /// Tests the generic DeclareVariable method without providing a default value.
        /// Verifies that the variable is created with the correct name and type, and that the default expression is assigned.
        /// </summary>
        [Fact]
        public void DeclareVariable_Generic_WithoutDefaultValue_UsesDefaultExpression()
        {
            // Arrange
            string variableName = "intVar2";
            int initialVariableCount = _compilationResult.Variables.Count;
            int initialBodyCount = _compilationResult.Body.Count;

            // Act
            ParameterExpression variable = _compilationResult.DeclareVariable<int>(variableName, null);

            // Assert
            Assert.NotNull(variable);
            Assert.Equal(variableName, variable.Name);
            Assert.Equal(typeof(int), variable.Type);
            Assert.Equal(initialVariableCount + 1, _compilationResult.Variables.Count);
            Assert.Equal(initialBodyCount + 1, _compilationResult.Body.Count);

            BinaryExpression assignment = Assert.IsType<BinaryExpression>(_compilationResult.Body.Last());
            Assert.Equal(variable, assignment.Left);

            DefaultExpression defaultExpr = Assert.IsType<DefaultExpression>(assignment.Right);
            Assert.Equal(typeof(int), defaultExpr.Type);
        }

        /// <summary>
        /// Tests the non-generic DeclareVariable method with a provided default value.
        /// Verifies that the variable is created with the correct name, type, and assigned the provided default value.
        /// </summary>
        [Fact]
        public void DeclareVariable_WithType_WithDefaultValue_ReturnsExpectedVariable()
        {
            // Arrange
            string variableName = "stringVar";
            Type type = typeof(string);
            Expression defaultValue = Expression.Constant("hello");
            int initialVariableCount = _compilationResult.Variables.Count;
            int initialBodyCount = _compilationResult.Body.Count;

            // Act
            ParameterExpression variable = _compilationResult.DeclareVariable(variableName, type, defaultValue);

            // Assert
            Assert.NotNull(variable);
            Assert.Equal(variableName, variable.Name);
            Assert.Equal(type, variable.Type);
            Assert.Equal(initialVariableCount + 1, _compilationResult.Variables.Count);
            Assert.Equal(initialBodyCount + 1, _compilationResult.Body.Count);

            BinaryExpression assignment = Assert.IsType<BinaryExpression>(_compilationResult.Body.Last());
            Assert.Equal(variable, assignment.Left);
            Assert.Equal(defaultValue, assignment.Right);
        }

        /// <summary>
        /// Tests the non-generic DeclareVariable method without providing a default value.
        /// Verifies that the variable is created with the correct name and type, and that the default expression is assigned.
        /// </summary>
        [Fact]
        public void DeclareVariable_WithType_WithoutDefaultValue_UsesDefaultExpression()
        {
            // Arrange
            string variableName = "stringVar2";
            Type type = typeof(string);
            int initialVariableCount = _compilationResult.Variables.Count;
            int initialBodyCount = _compilationResult.Body.Count;

            // Act
            ParameterExpression variable = _compilationResult.DeclareVariable(variableName, type, null);

            // Assert
            Assert.NotNull(variable);
            Assert.Equal(variableName, variable.Name);
            Assert.Equal(type, variable.Type);
            Assert.Equal(initialVariableCount + 1, _compilationResult.Variables.Count);
            Assert.Equal(initialBodyCount + 1, _compilationResult.Body.Count);

            BinaryExpression assignment = Assert.IsType<BinaryExpression>(_compilationResult.Body.Last());
            Assert.Equal(variable, assignment.Left);

            DefaultExpression defaultExpr = Assert.IsType<DefaultExpression>(assignment.Right);
            Assert.Equal(type, defaultExpr.Type);
        }

        /// <summary>
        /// Tests multiple calls to DeclareVariable methods to ensure that variables and their assignments are added correctly.
        /// Verifies that each variable is distinct and reflected in the Variables and Body lists.
        /// </summary>
        [Fact]
        public void DeclareVariable_MultipleVariables_AddedCorrectly()
        {
            // Arrange
            int initialVariableCount = _compilationResult.Variables.Count;
            int initialBodyCount = _compilationResult.Body.Count;

            // Act
            ParameterExpression var1 = _compilationResult.DeclareVariable<int>("var1", Expression.Constant(5));
            ParameterExpression var2 = _compilationResult.DeclareVariable("var2", typeof(string), Expression.Constant("test"));

            // Assert
            Assert.Equal(initialVariableCount + 2, _compilationResult.Variables.Count);
            Assert.Equal(initialBodyCount + 2, _compilationResult.Body.Count);
            Assert.Contains(var1, _compilationResult.Variables);
            Assert.Contains(var2, _compilationResult.Variables);

            BinaryExpression assignment1 = Assert.IsType<BinaryExpression>(_compilationResult.Body[initialBodyCount]);
            BinaryExpression assignment2 = Assert.IsType<BinaryExpression>(_compilationResult.Body[initialBodyCount + 1]);

            Assert.Equal(var1, assignment1.Left);
            Assert.Equal(Expression.Constant(5).ToString(), assignment1.Right.ToString());

            Assert.Equal(var2, assignment2.Left);
            Assert.Equal(Expression.Constant("test").ToString(), assignment2.Right.ToString());
        }
    }
}
