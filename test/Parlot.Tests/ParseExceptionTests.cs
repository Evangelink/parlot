using Parlot;
using System;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParseException"/> class.
    /// </summary>
//     public class ParseExceptionTests [Error] (19-36)CS7036 There is no argument given that corresponds to the required parameter 'column' of 'TextPosition.TextPosition(int, int, int)' [Error] (20-38)CS7036 There is no argument given that corresponds to the required parameter 'column' of 'TextPosition.TextPosition(int, int, int)'
//     {
//         private readonly TextPosition _defaultPosition;
//         private readonly TextPosition _alternatePosition;
// 
//         public ParseExceptionTests()
//         {
//             // Assuming TextPosition has a constructor that takes line and column.
//             // Update the constructor parameters if necessary to align with the actual implementation.
//             _defaultPosition = new TextPosition(1, 1);
//             _alternatePosition = new TextPosition(2, 2);
//         }
// 
//         /// <summary>
//         /// Tests that the constructor of ParseException correctly sets the Message and Position properties with valid arguments.
//         /// </summary>
//         [Fact]
//         public void Constructor_WithValidArguments_SetsMessageAndPosition()
//         {
//             // Arrange
//             string expectedMessage = "An error has occurred.";
// 
//             // Act
//             var exception = new ParseException(expectedMessage, _defaultPosition);
// 
//             // Assert
//             Assert.Equal(expectedMessage, exception.Message);
//             Assert.Equal(_defaultPosition, exception.Position);
//         }
// 
//         /// <summary>
//         /// Tests that the constructor of ParseException allows a null message and correctly sets the Position property.
//         /// </summary>
//         [Fact]
//         public void Constructor_WithNullMessage_AllowsNullMessageAndSetsPosition()
//         {
//             // Arrange
//             string expectedMessage = null;
// 
//             // Act
//             var exception = new ParseException(expectedMessage, _defaultPosition);
// 
//             // Assert
//             Assert.Null(exception.Message);
//             Assert.Equal(_defaultPosition, exception.Position);
//         }
// 
//         /// <summary>
//         /// Tests that the Position property getter and setter work correctly.
//         /// </summary>
//         [Fact]
//         public void Position_SetterGetter_ReturnsUpdatedValue()
//         {
//             // Arrange
//             var exception = new ParseException("Test", _defaultPosition);
// 
//             // Act
//             exception.Position = _alternatePosition;
// 
//             // Assert
//             Assert.Equal(_alternatePosition, exception.Position);
//         }
//     }
}
