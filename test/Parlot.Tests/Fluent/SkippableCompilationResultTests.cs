using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "SkippableCompilationResult"/> class.
/// </summary>
public class SkippableCompilationResultTests
{
    /// <summary>
    /// Tests that the constructor of <see cref = "SkippableCompilationResult"/> correctly assigns the properties when given a non-null CompilationResult instance and a true skip value.
    /// </summary>
//     [Fact] [Error] (19-51)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void Constructor_WithValidNonNullCompilationResultAndTrueSkip_SetsPropertiesCorrectly()
//     {
//         // Arrange
//         CompilationResult compilationResult = new CompilationResult();
//         bool skipValue = true;
//         // Act
//         var result = new SkippableCompilationResult(compilationResult, skipValue);
//         // Assert
//         Assert.Equal(compilationResult, result.CompilationResult);
//         Assert.True(result.Skip);
//     }

    /// <summary>
    /// Tests that the constructor of <see cref = "SkippableCompilationResult"/> correctly assigns the properties when given a non-null CompilationResult instance and a false skip value.
    /// </summary>
//     [Fact] [Error] (35-51)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     public void Constructor_WithValidNonNullCompilationResultAndFalseSkip_SetsPropertiesCorrectly()
//     {
//         // Arrange
//         CompilationResult compilationResult = new CompilationResult();
//         bool skipValue = false;
//         // Act
//         var result = new SkippableCompilationResult(compilationResult, skipValue);
//         // Assert
//         Assert.Equal(compilationResult, result.CompilationResult);
//         Assert.False(result.Skip);
//     }

    /// <summary>
    /// Tests that the constructor of <see cref = "SkippableCompilationResult"/> correctly assigns the properties even when a null CompilationResult is provided.
    /// </summary>
    [Fact]
    public void Constructor_WithNullCompilationResult_SetsPropertiesCorrectly()
    {
        // Arrange
        CompilationResult compilationResult = null;
        bool skipValue = true;
        // Act
        var result = new SkippableCompilationResult(compilationResult, skipValue);
        // Assert
        Assert.Null(result.CompilationResult);
        Assert.True(result.Skip);
    }

    /// <summary>
    /// Tests that the CompilationResult property returns the value provided via the constructor.
    /// </summary>
    [Fact]
    public void CompilationResult_GetAfterConstruction_ReturnsProvidedValue()
    {
        // Arrange
        var expectedCompilationResult = new Mock<CompilationResult>().Object;
        var skipFlag = true;
        var skippableCompilationResult = new SkippableCompilationResult(expectedCompilationResult, skipFlag);
        // Act
        CompilationResult actualResult = skippableCompilationResult.CompilationResult;
        // Assert
        Assert.Equal(expectedCompilationResult, actualResult);
    }

    /// <summary>
    /// Tests that setting a new value to the CompilationResult property updates the property accordingly.
    /// </summary>
    [Fact]
    public void CompilationResult_SetToNewValue_UpdatesValue()
    {
        // Arrange
        var initialCompilationResult = new Mock<CompilationResult>().Object;
        var skippableCompilationResult = new SkippableCompilationResult(initialCompilationResult, false);
        var newCompilationResult = new Mock<CompilationResult>().Object;
        // Act
        skippableCompilationResult.CompilationResult = newCompilationResult;
        // Assert
        Assert.Equal(newCompilationResult, skippableCompilationResult.CompilationResult);
    }

    /// <summary>
    /// Tests that setting the CompilationResult property to null is allowed and returns null.
    /// </summary>
    [Fact]
    public void CompilationResult_SetToNull_AllowsNullValue()
    {
        // Arrange
        var initialCompilationResult = new Mock<CompilationResult>().Object;
        var skippableCompilationResult = new SkippableCompilationResult(initialCompilationResult, false);
        // Act
        skippableCompilationResult.CompilationResult = null;
        // Assert
        Assert.Null(skippableCompilationResult.CompilationResult);
    }

    /// <summary>
    /// Tests that the constructor correctly assigns the Skip property when provided a true value.
    /// </summary>
    [Fact]
    public void Constructor_WhenCalledWithTrueSkip_SetsSkipPropertyToTrue()
    {
        // Arrange
        // Create a dummy CompilationResult. Using null for simplicity, assuming it's acceptable.
        CompilationResult dummyCompilationResult = null;
        bool expectedSkip = true;
        // Act
        var instance = new SkippableCompilationResult(dummyCompilationResult, expectedSkip);
        // Assert
        Assert.True(instance.Skip, "The Skip property should be true when passed true to the constructor.");
    }

    /// <summary>
    /// Tests that the constructor correctly assigns the Skip property when provided a false value.
    /// </summary>
    [Fact]
    public void Constructor_WhenCalledWithFalseSkip_SetsSkipPropertyToFalse()
    {
        // Arrange
        CompilationResult dummyCompilationResult = null;
        bool expectedSkip = false;
        // Act
        var instance = new SkippableCompilationResult(dummyCompilationResult, expectedSkip);
        // Assert
        Assert.False(instance.Skip, "The Skip property should be false when passed false to the constructor.");
    }

    /// <summary>
    /// Tests that the Skip property can be updated after object instantiation.
    /// </summary>
    [Fact]
    public void SkipProperty_WhenSetToNewValue_ReflectsUpdatedValue()
    {
        // Arrange
        CompilationResult dummyCompilationResult = null;
        var instance = new SkippableCompilationResult(dummyCompilationResult, false);
        // Act
        instance.Skip = true;
        // Assert
        Assert.True(instance.Skip, "The Skip property should reflect the updated value of true.");
    }
}