using Parlot;
using System;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "TextPosition"/> struct.
/// </summary>
public class TextPositionTests
{
    /// <summary>
    /// Tests the ToString method for a TextPosition with typical positive line and column values.
    /// Expected outcome: The method returns a correctly formatted string "(Line:Column)".
    /// </summary>
    [Fact]
    public void ToString_WhenCalledWithPositiveValues_ReturnsExpectedString()
    {
        // Arrange
        int offset = 100;
        int line = 5;
        int column = 10;
        var textPosition = new TextPosition(offset, line, column);
        string expected = $"({line}:{column})";
        // Act
        string result = textPosition.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToString method on the default TextPosition instance.
    /// Expected outcome: The method returns "(0:0)" since the default values for int are 0.
    /// </summary>
    [Fact]
    public void ToString_WhenCalledOnDefaultInstance_ReturnsZeroRepresentation()
    {
        // Arrange
        var textPosition = default(TextPosition);
        string expected = "(0:0)";
        // Act
        string result = textPosition.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToString method for a TextPosition with negative line and column values.
    /// Expected outcome: The method returns a correctly formatted string with negative values.
    /// </summary>
    [Fact]
    public void ToString_WhenCalledWithNegativeValues_ReturnsExpectedString()
    {
        // Arrange
        int offset = -50;
        int line = -3;
        int column = -7;
        var textPosition = new TextPosition(offset, line, column);
        string expected = $"({line}:{column})";
        // Act
        string result = textPosition.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToString method for a TextPosition with large line and column values.
    /// Expected outcome: The method returns a correctly formatted string with large numerical values.
    /// </summary>
    [Fact]
    public void ToString_WhenCalledWithLargeValues_ReturnsExpectedString()
    {
        // Arrange
        int offset = int.MaxValue;
        int line = int.MaxValue;
        int column = int.MaxValue;
        var textPosition = new TextPosition(offset, line, column);
        string expected = $"({line}:{column})";
        // Act
        string result = textPosition.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that the <see cref = "TextPosition"/> constructor initializes the fields correctly with various input values.
    /// This test uses multiple sets of data, including typical values, boundary conditions, and extreme values,
    /// and then asserts that Offset, Line, and Column are set to the values passed to the constructor.
    /// </summary>
    /// <param name = "offset">The offset value for the text position.</param>
    /// <param name = "line">The line number for the text position.</param>
    /// <param name = "column">The column number for the text position.</param>
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(10, 1, 5)]
    [InlineData(-1, -2, -3)]
    [InlineData(int.MaxValue, int.MaxValue, int.MaxValue)]
    [InlineData(int.MinValue, int.MinValue, int.MinValue)]
    public void Constructor_WithVariousValues_AssignsFieldsCorrectly(int offset, int line, int column)
    {
        // Act
        var textPosition = new TextPosition(offset, line, column);
        // Assert
        Assert.Equal(offset, textPosition.Offset);
        Assert.Equal(line, textPosition.Line);
        Assert.Equal(column, textPosition.Column);
    }
}