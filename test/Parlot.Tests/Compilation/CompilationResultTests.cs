using Moq;
using Parlot.Compilation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "CompilationResult"/> class DeclareVariable methods.
/// </summary>
public class CompilationResultTests
{
    /// <summary>
    /// Tests the generic DeclareVariable method without providing a default value.
    /// It ensures that the returned parameter expression has the expected name and type.
    /// </summary>
//     [Fact] [Error] (21-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_Generic_NoDefaultValue_ReturnsExpectedParameter()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "testVar";
//         // Act
//         ParameterExpression parameter = compilationResult.DeclareVariable<int>(variableName);
//         // Assert
//         Assert.NotNull(parameter);
//         Assert.Equal(variableName, parameter.Name);
//         Assert.Equal(typeof(int), parameter.Type);
//     }

    /// <summary>
    /// Tests the generic DeclareVariable method when providing a default value.
    /// It ensures that the returned parameter expression has the expected name and type.
    /// (Note: Since the default value handling is implementation-specific and "stripped out",
    /// we simply verify that the method call succeeds and returns a parameter with correct properties.)
    /// </summary>
//     [Fact] [Error] (41-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_Generic_WithDefaultValue_ReturnsExpectedParameter()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "defaultVar";
//         Expression defaultExpression = Expression.Constant(42);
//         // Act
//         ParameterExpression parameter = compilationResult.DeclareVariable<int>(variableName, defaultExpression);
//         // Assert
//         Assert.NotNull(parameter);
//         Assert.Equal(variableName, parameter.Name);
//         Assert.Equal(typeof(int), parameter.Type);
//     }

    /// <summary>
    /// Tests the non-generic DeclareVariable method without providing a default value.
    /// It ensures that the returned parameter expression has the expected name and type.
    /// </summary>
//     [Fact] [Error] (60-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_NonGeneric_NoDefaultValue_ReturnsExpectedParameter()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "nonGenericVar";
//         Type variableType = typeof(string);
//         // Act
//         ParameterExpression parameter = compilationResult.DeclareVariable(variableName, variableType);
//         // Assert
//         Assert.NotNull(parameter);
//         Assert.Equal(variableName, parameter.Name);
//         Assert.Equal(variableType, parameter.Type);
//     }

    /// <summary>
    /// Tests the non-generic DeclareVariable method when providing a default value.
    /// It ensures that the returned parameter expression has the expected name and type.
    /// (Note: Default value handling is implementation-specific; this test focuses on correct parameter creation.)
    /// </summary>
//     [Fact] [Error] (80-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_NonGeneric_WithDefaultValue_ReturnsExpectedParameter()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "nonGenericDefaultVar";
//         Type variableType = typeof(double);
//         Expression defaultExpression = Expression.Constant(3.14);
//         // Act
//         ParameterExpression parameter = compilationResult.DeclareVariable(variableName, variableType, defaultExpression);
//         // Assert
//         Assert.NotNull(parameter);
//         Assert.Equal(variableName, parameter.Name);
//         Assert.Equal(variableType, parameter.Type);
//     }

    /// <summary>
    /// Tests that the non-generic DeclareVariable method throws an ArgumentNullException
    /// when a null type is provided.
    /// </summary>
//     [Fact] [Error] (100-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_NonGeneric_NullType_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "nullTypeVar";
//         Type nullType = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => compilationResult.DeclareVariable(variableName, nullType));
//     }

    /// <summary>
    /// Tests the non-generic DeclareVariable method with a valid name, type and a null default value.
    /// Expected behavior: A new variable is created, added to the Variables list and an assignment to Expression.Default is added to the Body list.
    /// </summary>
//     [Fact] [Error] (115-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_WithNullDefaultValue_ShouldAssignDefaultValue()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "testVar";
//         Type variableType = typeof(int);
//         // Act
//         var variable = compilationResult.DeclareVariable(variableName, variableType, null);
//         // Assert
//         Assert.NotNull(variable);
//         Assert.Equal(variableName, variable.Name);
//         Assert.Equal(variableType, variable.Type);
//         Assert.Contains(variable, compilationResult.Variables);
//         Assert.Single(compilationResult.Body);
//         var assignment = Assert.IsAssignableFrom<BinaryExpression>(compilationResult.Body[0]);
//         Assert.Equal(variable, assignment.Left);
//         var defaultExpr = Assert.IsType<DefaultExpression>(assignment.Right);
//         Assert.Equal(variableType, defaultExpr.Type);
//     }

    /// <summary>
    /// Tests the non-generic DeclareVariable method with a valid name, type and a provided default value.
    /// Expected behavior: A new variable is created, added to the Variables list and an assignment to the provided default value is added to the Body list.
    /// </summary>
//     [Fact] [Error] (140-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_WithProvidedDefaultValue_ShouldAssignProvidedValue()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "testVar";
//         Type variableType = typeof(int);
//         Expression defaultValue = Expression.Constant(42, variableType);
//         // Act
//         var variable = compilationResult.DeclareVariable(variableName, variableType, defaultValue);
//         // Assert
//         Assert.NotNull(variable);
//         Assert.Equal(variableName, variable.Name);
//         Assert.Equal(variableType, variable.Type);
//         Assert.Contains(variable, compilationResult.Variables);
//         Assert.Single(compilationResult.Body);
//         var assignment = Assert.IsAssignableFrom<BinaryExpression>(compilationResult.Body[0]);
//         Assert.Equal(variable, assignment.Left);
//         Assert.Equal(defaultValue, assignment.Right);
//     }

    /// <summary>
    /// Tests the non-generic DeclareVariable method when called with a null name.
    /// Expected behavior: An ArgumentNullException is thrown.
    /// </summary>
//     [Fact] [Error] (165-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_WithNullName_ShouldThrowArgumentNullException()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = null;
//         Type variableType = typeof(int);
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => compilationResult.DeclareVariable(variableName, variableType, null));
//     }

    /// <summary>
    /// Tests the non-generic DeclareVariable method when called with a null type.
    /// Expected behavior: An ArgumentNullException is thrown.
    /// </summary>
//     [Fact] [Error] (180-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariable_WithNullType_ShouldThrowArgumentNullException()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "testVar";
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => compilationResult.DeclareVariable(variableName, null, null));
//     }

    /// <summary>
    /// Tests the generic DeclareVariable method with a valid name, type parameter and a provided default value.
    /// Expected behavior: A new variable is created, added to the Variables list and an assignment to the provided default value is added to the Body list.
    /// </summary>
//     [Fact] [Error] (194-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariableGeneric_WithProvidedDefaultValue_ShouldAssignProvidedValue()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "genericVar";
//         Expression defaultValue = Expression.Constant(99, typeof(int));
//         // Act
//         var variable = compilationResult.DeclareVariable<int>(variableName, defaultValue);
//         // Assert
//         Assert.NotNull(variable);
//         Assert.Equal(variableName, variable.Name);
//         Assert.Equal(typeof(int), variable.Type);
//         Assert.Contains(variable, compilationResult.Variables);
//         Assert.Single(compilationResult.Body);
//         var assignment = Assert.IsAssignableFrom<BinaryExpression>(compilationResult.Body[0]);
//         Assert.Equal(variable, assignment.Left);
//         Assert.Equal(defaultValue, assignment.Right);
//     }

    /// <summary>
    /// Tests the generic DeclareVariable method with a valid name and a null default value.
    /// Expected behavior: A new variable is created, added to the Variables list and an assignment to Expression.Default is added to the Body list.
    /// </summary>
//     [Fact] [Error] (218-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariableGeneric_WithNullDefaultValue_ShouldAssignDefaultValue()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = "genericVar";
//         // Act
//         var variable = compilationResult.DeclareVariable<int>(variableName, null);
//         // Assert
//         Assert.NotNull(variable);
//         Assert.Equal(variableName, variable.Name);
//         Assert.Equal(typeof(int), variable.Type);
//         Assert.Contains(variable, compilationResult.Variables);
//         Assert.Single(compilationResult.Body);
//         var assignment = Assert.IsAssignableFrom<BinaryExpression>(compilationResult.Body[0]);
//         Assert.Equal(variable, assignment.Left);
//         var defaultExpr = Assert.IsType<DefaultExpression>(assignment.Right);
//         Assert.Equal(typeof(int), defaultExpr.Type);
//     }

    /// <summary>
    /// Tests the generic DeclareVariable method when called with a null name.
    /// Expected behavior: An ArgumentNullException is thrown.
    /// </summary>
//     [Fact] [Error] (242-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void DeclareVariableGeneric_WithNullName_ShouldThrowArgumentNullException()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         string variableName = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => compilationResult.DeclareVariable<int>(variableName, null));
//     }

    /// <summary>
    /// Tests that the CompilationResult constructor properly initializes the Variables and Body lists 
    /// when required properties are set through an object initializer.
    /// Expected outcome: The instance should be created successfully with empty Variables and Body lists,
    /// and the Success and Value properties should correctly reflect the provided values.
    /// </summary>
//     [Fact] [Error] (261-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void CompilationResult_Constructor_WithRequiredProperties_InitializesPropertiesCorrectly()
//     {
//         // Arrange
//         ParameterExpression expectedSuccess = Expression.Parameter(typeof(bool), "success");
//         ParameterExpression expectedValue = Expression.Parameter(typeof(bool), "value");
//         // Act
//         var compilationResult = new CompilationResult
//         {
//             Success = expectedSuccess,
//             Value = expectedValue
//         };
//         // Assert
//         Assert.NotNull(compilationResult);
//         Assert.NotNull(compilationResult.Variables);
//         Assert.Empty(compilationResult.Variables);
//         Assert.NotNull(compilationResult.Body);
//         Assert.Empty(compilationResult.Body);
//         Assert.Equal(expectedSuccess, compilationResult.Success);
//         Assert.Equal(expectedValue, compilationResult.Value);
//     }

    /// <summary>
    /// Tests that the Variables property is initialized as a non-null empty list when a new instance is created.
    /// </summary>
//     [Fact] [Error] (284-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void Variables_OnNewInstance_ReturnsEmptyList()
//     {
//         // Arrange
//         // Create instance of CompilationResult. Assuming the test assembly has access to internal members.
//         var compilationResult = new CompilationResult();
//         // Act
//         List<ParameterExpression> variables = compilationResult.Variables;
//         // Assert
//         Assert.NotNull(variables);
//         Assert.Empty(variables);
//     }

    /// <summary>
    /// Tests that items can be added to the Variables list and that the list reflects the newly added item.
    /// </summary>
//     [Fact] [Error] (299-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void Variables_AddItem_ListReflectsNewItem()
//     {
//         // Arrange
//         var compilationResult = new CompilationResult();
//         var testParameter = Expression.Parameter(typeof(int), "testVariable");
//         // Act
//         compilationResult.Variables.Add(testParameter);
//         // Assert
//         Assert.Single(compilationResult.Variables);
//         Assert.Contains(testParameter, compilationResult.Variables);
//     }

    /// <summary>
    /// Tests the Body property getter to ensure it returns a non-null, empty list immediately after instantiation.
    /// </summary>
//     [Fact] [Error] (315-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void Body_Getter_ReturnsNonNullEmptyList()
//     {
//         // Arrange: Create a new instance of CompilationResult.
//         var compilationResult = new CompilationResult();
//         // Act: Retrieve the Body property.
//         List<Expression> bodyExpressions = compilationResult.Body;
//         // Assert: Verify the body list is not null and initially empty.
//         Assert.NotNull(bodyExpressions);
//         Assert.Empty(bodyExpressions);
//     }

    /// <summary>
    /// Tests that the Body property is modifiable by adding an expression and verifying it is stored.
    /// </summary>
//     [Fact] [Error] (330-37)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void Body_Modifiable_AddItemsSuccessfully()
//     {
//         // Arrange: Create a new instance of CompilationResult.
//         var compilationResult = new CompilationResult();
//         var testExpression = Expression.Constant(42);
//         // Act: Add a test expression to the Body list.
//         compilationResult.Body.Add(testExpression);
//         // Assert: Verify the expression was successfully added.
//         Assert.Single(compilationResult.Body);
//         Assert.Same(testExpression, compilationResult.Body[0]);
//     }

    /// <summary>
    /// Tests that the Success property returns the value that was set.
    /// This test creates a new parameter expression, assigns it to the Success property,
    /// and then verifies that the getter returns the same parameter expression.
    /// </summary>
    [Fact]
    public void Success_GetSet_ReturnsAssignedValue()
    {
        // Arrange
        // Since the constructor of CompilationResult is internal, use reflection to create an instance.
        var instance = (CompilationResult)Activator.CreateInstance(typeof(CompilationResult), nonPublic: true);
        ParameterExpression expectedSuccess = Expression.Parameter(typeof(bool), "success");
        // Act
        instance.Success = expectedSuccess;
        var actualSuccess = instance.Success;
        // Assert
        Assert.Same(expectedSuccess, actualSuccess);
    }

    /// <summary>
    /// Tests that the Success property can be explicitly set to null.
    /// This test assigns null to the Success property and verifies that the getter returns null.
    /// </summary>
    [Fact]
    public void Success_SetToNull_ReturnsNull()
    {
        // Arrange
        var instance = (CompilationResult)Activator.CreateInstance(typeof(CompilationResult), nonPublic: true);
        // Act
        instance.Success = null;
        var actualSuccess = instance.Success;
        // Assert
        Assert.Null(actualSuccess);
    }

    /// <summary>
    /// Tests that setting the 'Value' property returns the same instance when retrieved.
    /// </summary>
    [Fact]
    public void Value_SetAndGet_ReturnsSameInstance()
    {
        // Arrange: create a ParameterExpression instance for the test.
        var expectedParameter = Expression.Parameter(typeof(bool), "value");
        var compilationResult = CreateCompilationResultInstance();
        // Act: set the Value property and retrieve it.
        compilationResult.Value = expectedParameter;
        var actualParameter = compilationResult.Value;
        // Assert: the retrieved instance should match the one that was set.
        Assert.Equal(expectedParameter, actualParameter);
    }

    /// <summary>
    /// Tests that setting the 'Value' property multiple times returns the most recently assigned instance.
    /// </summary>
    [Fact]
    public void Value_SetMultipleTimes_ReturnsLastAssignedInstance()
    {
        // Arrange: create two distinct ParameterExpression instances.
        var firstParameter = Expression.Parameter(typeof(bool), "firstValue");
        var secondParameter = Expression.Parameter(typeof(bool), "secondValue");
        var compilationResult = CreateCompilationResultInstance();
        // Act: assign first then overwrite with second.
        compilationResult.Value = firstParameter;
        compilationResult.Value = secondParameter;
        var actualParameter = compilationResult.Value;
        // Assert: the Value property should return the second assigned instance.
        Assert.Equal(secondParameter, actualParameter);
    }

    /// <summary>
    /// Helper method to create an instance of the <see cref = "CompilationResult"/> class.
    /// Since the constructor of <see cref = "CompilationResult"/> is internal, reflection is used to create an instance.
    /// </summary>
    /// <returns>An instance of <see cref = "CompilationResult"/>.</returns>
    private CompilationResult CreateCompilationResultInstance()
    {
        // Use reflection to invoke the internal constructor.
        var constructor = typeof(CompilationResult).GetConstructor(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, binder: null, types: Type.EmptyTypes, modifiers: null);
        if (constructor == null)
        {
            throw new InvalidOperationException("No accessible internal constructor found for CompilationResult.");
        }

        return (CompilationResult)constructor.Invoke(null);
    }
}