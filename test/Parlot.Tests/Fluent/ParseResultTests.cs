using Parlot;
using System;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "ParseResult{T}"/> struct's Set method.
/// </summary>
public class ParseResultTests
{
    /// <summary>
    /// Tests that the Set method correctly assigns values for an int type instance.
    /// This test covers the happy path with typical positive integer values.
    /// </summary>
    [Fact]
    public void Set_Int_HappyPath_AssignsCorrectValues()
    {
        // Arrange
        int expectedStart = 5;
        int expectedEnd = 10;
        int expectedValue = 100;
        var parseResult = new ParseResult<int>(0, 0, 0);
        // Act
        parseResult.Set(expectedStart, expectedEnd, expectedValue);
        // Assert
        Assert.Equal(expectedStart, parseResult.Start);
        Assert.Equal(expectedEnd, parseResult.End);
        Assert.Equal(expectedValue, parseResult.Value);
    }

    /// <summary>
    /// Tests that the Set method correctly assigns negative index values for an int type instance.
    /// This test ensures that the method does not enforce any restrictions on the numerical range.
    /// </summary>
    [Fact]
    public void Set_Int_NegativeIndices_AssignsCorrectValues()
    {
        // Arrange
        int expectedStart = -3;
        int expectedEnd = -1;
        int expectedValue = 42;
        var parseResult = new ParseResult<int>(0, 0, 0);
        // Act
        parseResult.Set(expectedStart, expectedEnd, expectedValue);
        // Assert
        Assert.Equal(expectedStart, parseResult.Start);
        Assert.Equal(expectedEnd, parseResult.End);
        Assert.Equal(expectedValue, parseResult.Value);
    }

    /// <summary>
    /// Tests that the Set method correctly assigns values for a string type instance.
    /// This test covers the happy path where a valid non-null string is provided.
    /// </summary>
    [Fact]
    public void Set_String_HappyPath_AssignsCorrectValues()
    {
        // Arrange
        int expectedStart = 0;
        int expectedEnd = 10;
        string expectedValue = "test";
        var parseResult = new ParseResult<string>(-1, -1, string.Empty);
        // Act
        parseResult.Set(expectedStart, expectedEnd, expectedValue);
        // Assert
        Assert.Equal(expectedStart, parseResult.Start);
        Assert.Equal(expectedEnd, parseResult.End);
        Assert.Equal(expectedValue, parseResult.Value);
    }

    /// <summary>
    /// Tests that the Set method correctly assigns a null value for a string type instance.
    /// This test verifies that the method can handle null as a valid input.
    /// </summary>
    [Fact]
    public void Set_String_NullValue_AssignsNullValue()
    {
        // Arrange
        int expectedStart = 0;
        int expectedEnd = 5;
        string expectedValue = null;
        var parseResult = new ParseResult<string>(100, 200, "initial");
        // Act
        parseResult.Set(expectedStart, expectedEnd, expectedValue);
        // Assert
        Assert.Equal(expectedStart, parseResult.Start);
        Assert.Equal(expectedEnd, parseResult.End);
        Assert.Null(parseResult.Value);
    }

    /// <summary>
    /// Tests that the ParseResult constructor initializes fields correctly with typical positive integer values.
    /// Arrange: Provide positive integers for start, end and a valid integer value.
    /// Act: Create a new instance of ParseResult using the constructor.
    /// Assert: Verify that the Start, End, and Value fields are correctly assigned.
    /// </summary>
    [Fact]
    public void ParseResult_Constructor_ValidInputs_SetsFieldsCorrectly()
    {
        // Arrange
        int expectedStart = 0;
        int expectedEnd = 10;
        int expectedValue = 123;
        // Act
        var result = new ParseResult<int>(expectedStart, expectedEnd, expectedValue);
        // Assert
        Assert.Equal(expectedStart, result.Start);
        Assert.Equal(expectedEnd, result.End);
        Assert.Equal(expectedValue, result.Value);
    }

    /// <summary>
    /// Tests that the ParseResult constructor initializes fields correctly when using negative integer values.
    /// Arrange: Provide negative integers for start, end and a negative integer value.
    /// Act: Create a new instance of ParseResult using the constructor.
    /// Assert: Verify that the Start, End, and Value fields are correctly assigned even when values are negative.
    /// </summary>
    [Fact]
    public void ParseResult_Constructor_NegativeValues_SetsFieldsCorrectly()
    {
        // Arrange
        int expectedStart = -5;
        int expectedEnd = -1;
        int expectedValue = -10;
        // Act
        var result = new ParseResult<int>(expectedStart, expectedEnd, expectedValue);
        // Assert
        Assert.Equal(expectedStart, result.Start);
        Assert.Equal(expectedEnd, result.End);
        Assert.Equal(expectedValue, result.Value);
    }

    /// <summary>
    /// Tests that the ParseResult constructor initializes fields correctly when passing a null value for a reference type.
    /// Arrange: Provide valid integers for start and end, and null for a string value.
    /// Act: Create a new instance of ParseResult using the constructor with a null value.
    /// Assert: Verify that the Start and End fields are set correctly and that the Value field is null.
    /// </summary>
    [Fact]
    public void ParseResult_Constructor_NullReferenceValue_SetsFieldsCorrectly()
    {
        // Arrange
        int expectedStart = 5;
        int expectedEnd = 15;
        string expectedValue = null;
        // Act
        var result = new ParseResult<string>(expectedStart, expectedEnd, expectedValue);
        // Assert
        Assert.Equal(expectedStart, result.Start);
        Assert.Equal(expectedEnd, result.End);
        Assert.Null(result.Value);
    }

    /// <summary>
    /// Tests that the ParseResult constructor initializes fields correctly when using boundary integer values.
    /// Arrange: Use int.MinValue and int.MaxValue for start and end respectively, and zero for the value.
    /// Act: Create a new instance of ParseResult using these boundary values.
    /// Assert: Verify that the Start, End, and Value fields are correctly assigned to their boundary values.
    /// </summary>
    [Fact]
    public void ParseResult_Constructor_BoundaryValues_SetsFieldsCorrectly()
    {
        // Arrange
        int expectedStart = int.MinValue;
        int expectedEnd = int.MaxValue;
        int expectedValue = 0;
        // Act
        var result = new ParseResult<int>(expectedStart, expectedEnd, expectedValue);
        // Assert
        Assert.Equal(expectedStart, result.Start);
        Assert.Equal(expectedEnd, result.End);
        Assert.Equal(expectedValue, result.Value);
    }
}