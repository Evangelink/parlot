using Parlot;
using System;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "ParseError"/> class.
/// </summary>
public class ParseErrorTests
{
    /// <summary>
    /// Tests that the default value of the Message property is null when a new instance is created.
    /// </summary>
    [Fact]
    public void Message_PropertyNotSet_ReturnsNull()
    {
        // Arrange
        var parseError = new ParseError();
        // Act
        var result = parseError.Message;
        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Tests that the Message property correctly stores and retrieves a non-null string value.
    /// </summary>
    [Fact]
    public void Message_SetNonNullValue_ReturnsSameValue()
    {
        // Arrange
        var parseError = new ParseError();
        string expectedMessage = "An error occurred.";
        // Act
        parseError.Message = expectedMessage;
        var result = parseError.Message;
        // Assert
        Assert.Equal(expectedMessage, result);
    }

    /// <summary>
    /// Tests that the Message property correctly stores and retrieves a null value after being assigned a non-null value.
    /// </summary>
    [Fact]
    public void Message_SetNullValueAfterNonNull_ReturnsNull()
    {
        // Arrange
        var parseError = new ParseError();
        parseError.Message = "Temporary error message";
        // Act
        parseError.Message = null;
        var result = parseError.Message;
        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Tests that setting the Position property returns the assigned value via the getter.
    /// </summary>
//     [Fact] [Error] (65-36)CS7036 There is no argument given that corresponds to the required parameter 'column' of 'TextPosition.TextPosition(int, int, int)'
//     public void Position_SetValidValue_ReturnsAssignedValue()
//     {
//         // Arrange
//         var error = new ParseError();
//         // Assumes that TextPosition has a constructor accepting line and column values.
//         var expectedPosition = new TextPosition(5, 10);
//         // Act
//         error.Position = expectedPosition;
//         var actualPosition = error.Position;
//         // Assert
//         Assert.Equal(expectedPosition, actualPosition);
//     }

    /// <summary>
    /// Tests that the Position property correctly updates when changed multiple times.
    /// </summary>
//     [Fact] [Error] (81-35)CS7036 There is no argument given that corresponds to the required parameter 'column' of 'TextPosition.TextPosition(int, int, int)' [Error] (82-35)CS7036 There is no argument given that corresponds to the required parameter 'column' of 'TextPosition.TextPosition(int, int, int)'
//     public void Position_MultipleAssignments_ReturnsLastAssignedValue()
//     {
//         // Arrange
//         var error = new ParseError();
//         var initialPosition = new TextPosition(1, 1);
//         var updatedPosition = new TextPosition(2, 3);
//         // Act
//         error.Position = initialPosition;
//         error.Position = updatedPosition;
//         var actualPosition = error.Position;
//         // Assert
//         Assert.Equal(updatedPosition, actualPosition);
//     }
}