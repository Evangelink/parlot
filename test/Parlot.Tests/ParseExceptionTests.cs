using Parlot;
using System;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "ParseException"/> class.
/// </summary>
// public class ParseExceptionTests [Error] (72-12)CS1520 Method must have a return type
// {
//     /// <summary>
//     /// Tests that the ParseException constructor correctly assigns the Message and Position properties when provided with valid arguments.
//     /// </summary>
//     [Fact]
//     public void ParseExceptionConstructor_WithValidArguments_SetsPropertiesCorrectly()
//     {
//         // Arrange
//         string expectedMessage = "An error has occurred.";
//         TextPosition expectedPosition = CreateDummyTextPosition(3, 15);
//         // Act
//         var exception = new ParseException(expectedMessage, expectedPosition);
//         // Assert
//         Assert.Equal(expectedMessage, exception.Message);
//         Assert.Equal(expectedPosition, exception.Position);
//     }
// 
//     /// <summary>
//     /// Tests that the ParseException constructor correctly assigns the Message and Position properties when an empty message is provided.
//     /// </summary>
//     [Fact]
//     public void ParseExceptionConstructor_WithEmptyMessage_SetsPropertiesCorrectly()
//     {
//         // Arrange
//         string expectedMessage = string.Empty;
//         TextPosition expectedPosition = CreateDummyTextPosition(0, 0);
//         // Act
//         var exception = new ParseException(expectedMessage, expectedPosition);
//         // Assert
//         Assert.Equal(expectedMessage, exception.Message);
//         Assert.Equal(expectedPosition, exception.Position);
//     }
// 
//     /// <summary>
//     /// Tests that the ParseException constructor correctly assigns a null Message and the Position property when a null message is provided.
//     /// </summary>
//     [Fact]
//     public void ParseExceptionConstructor_WithNullMessage_SetsPropertiesCorrectly()
//     {
//         // Arrange
//         string expectedMessage = null;
//         TextPosition expectedPosition = CreateDummyTextPosition(10, 20);
//         // Act
//         var exception = new ParseException(expectedMessage, expectedPosition);
//         // Assert
//         Assert.Null(exception.Message);
//         Assert.Equal(expectedPosition, exception.Position);
//     }
// 
//     /// <summary>
//     /// Helper method to create a dummy TextPosition instance for testing.
//     /// </summary>
//     /// <param name = "line">The line number to assign.</param>
//     /// <param name = "column">The column number to assign.</param>
//     /// <returns>A dummy TextPosition instance with the specified values.</returns>
//     private static TextPosition CreateDummyTextPosition(int line, int column) [Error] (66-20)CS0246 The type or namespace name 'DummyTextPosition' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         return new DummyTextPosition(line, column);
//     }
// 
//     public int Line { get; }
//     public int Column { get; }
// 
//     public DummyTextPosition(int line, int column)
//     {
//         Line = line;
//         Column = column;
//     }
// 
//     public override bool Equals(object obj) => Equals(obj as DummyTextPosition); [Error] (78-62)CS0246 The type or namespace name 'DummyTextPosition' could not be found (are you missing a using directive or an assembly reference?)
//     public bool Equals(DummyTextPosition other) [Error] (79-24)CS0246 The type or namespace name 'DummyTextPosition' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         if (other is null)
//         {
//             return false;
//         }
// 
//         return Line == other.Line && Column == other.Column;
//     }
// 
//     public override int GetHashCode() => HashCode.Combine(Line, Column);
// }