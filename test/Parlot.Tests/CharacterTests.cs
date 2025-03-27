using System;
using Moq;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Character"/> class.
    /// </summary>
//     public class CharacterTests [Error] (11-18)CS0101 The namespace 'Parlot.UnitTests' already contains a definition for 'CharacterTests'
//     {
//         /// <summary>
//         /// Tests the IsInRange method to ensure it correctly identifies if a character is within a given range.
//         /// </summary>
//         /// <param name="ch">The character to check.</param>
//         /// <param name="min">The minimum character of the range.</param>
//         /// <param name="max">The maximum character of the range.</param>
//         /// <param name="expected">Expected boolean result.</param>
//         [Theory]
//         [InlineData('b', 'a', 'c', true)]
//         [InlineData('a', 'a', 'c', true)]
//         [InlineData('c', 'a', 'c', true)]
//         [InlineData('d', 'a', 'c', false)]
//         [InlineData('Z', 'A', 'Z', true)]
//         [InlineData('a', 'A', 'Z', false)]
//         public void IsInRange_ValidInputs_ReturnsExpected(char ch, char min, char max, bool expected)
//         {
//             // Act
//             bool result = Character.IsInRange(ch, min, max);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
// 
//         /// <summary>
//         /// Tests the ScanHexEscape method (ReadOnlySpan overload) with a valid hex sequence.
//         /// </summary>
//         [Fact]
//         public void ScanHexEscape_ReadOnlySpan_ValidHexSequence_ReturnsCorrectCharAndLength()
//         {
//             // Arrange
//             ReadOnlySpan<char> input = "u0041".AsSpan(); // Expected to decode to 'A'
//             int length;
// 
//             // Act
//             char result = Character.ScanHexEscape(input, out length);
// 
//             // Assert
//             Assert.Equal('A', result);
//             Assert.Equal(4, length);
//         }
// 
//         /// <summary>
//         /// Tests the ScanHexEscape method (ReadOnlySpan overload) with an invalid hex digit causing early termination.
//         /// </summary>
//         [Fact]
//         public void ScanHexEscape_ReadOnlySpan_InvalidHexDigit_ProcessesUntilInvalid_ReturnsZeroCharAndPartialLength()
//         {
//             // Arrange
//             // "u0G41" -> '0' is processed, 'G' is not a hex digit so processing stops.
//             ReadOnlySpan<char> input = "u0G41".AsSpan();
//             int length;
// 
//             // Act
//             char result = Character.ScanHexEscape(input, out length);
// 
//             // Assert
//             // '0' converts to 0 so result is '\0'
//             Assert.Equal('\0', result);
//             Assert.Equal(1, length);
//         }
// 
//         /// <summary>
//         /// Tests the ScanHexEscape method (ReadOnlySpan overload) when no hex digits are available.
//         /// </summary>
//         [Fact]
//         public void ScanHexEscape_ReadOnlySpan_NoHexDigits_ReturnsZeroCharAndZeroLength()
//         {
//             // Arrange
//             ReadOnlySpan<char> input = "u".AsSpan();
//             int length;
// 
//             // Act
//             char result = Character.ScanHexEscape(input, out length);
// 
//             // Assert
//             Assert.Equal('\0', result);
//             Assert.Equal(0, length);
//         }
// 
//         /// <summary>
//         /// Tests the ScanHexEscape method (string overload) with a valid hex sequence.
//         /// </summary>
//         [Fact]
//         public void ScanHexEscape_String_ValidHexSequence_ReturnsCorrectCharAndLength()
//         {
//             // Arrange
//             string input = "u0041";
//             int length;
// 
//             // Act
//             char result = Character.ScanHexEscape(input, 0, out length);
// 
//             // Assert
//             Assert.Equal('A', result);
//             Assert.Equal(4, length);
//         }
// 
//         /// <summary>
//         /// Tests the DecodeString method (ReadOnlySpan overload) with input that has no escape sequences.
//         /// </summary>
//         /// <param name="input">The input string with no escapes.</param>
//         [Theory]
//         [InlineData("")]
//         [InlineData("Simple text without escapes")]
//         public void DecodeString_ReadOnlySpan_NoEscapes_ReturnsOriginalSpan(string input)
//         {
//             // Arrange
//             ReadOnlySpan<char> span = input.AsSpan();
// 
//             // Act
//             ReadOnlySpan<char> result = Character.DecodeString(span);
// 
//             // Assert
//             Assert.Equal(span.ToString(), result.ToString());
//         }
// 
//         /// <summary>
//         /// Tests the DecodeString method (ReadOnlySpan overload) with various escape sequences.
//         /// </summary>
//         [Fact]
//         public void DecodeString_ReadOnlySpan_WithEscapes_ReturnsCorrectDecodedString()
//         {
//             // Arrange
//             // Testing common escapes: \n, \t, \\, and a hex escape \u0041 which decodes to 'A'
//             string input = "Hello\\nWorld\\tTest\\\\Escape\\u0041";
//             string expected = "Hello\nWorld\tTest\\EscapeA";
//             ReadOnlySpan<char> span = input.AsSpan();
// 
//             // Act
//             ReadOnlySpan<char> result = Character.DecodeString(span);
// 
//             // Assert
//             Assert.Equal(expected, result.ToString());
//         }
// 
//         /// <summary>
//         /// Tests the DecodeString method (ReadOnlySpan overload) with an incomplete escape sequence,
//         /// expecting an IndexOutOfRangeException.
//         /// </summary>
//         [Fact] [Error] (161-82)CS8175 Cannot use ref local 'span' inside an anonymous method, lambda expression, or query expression
//         public void DecodeString_ReadOnlySpan_IncompleteEscape_ThrowsIndexOutOfRangeException()
//         {
//             // Arrange
//             // The trailing '\' will cause the method to try to access an index that doesn't exist.
//             string input = "Incomplete\\";
//             ReadOnlySpan<char> span = input.AsSpan();
// 
//             // Act & Assert
//             Assert.Throws<IndexOutOfRangeException>(() => Character.DecodeString(span).ToString());
//         }
// 
//         /// <summary>
//         /// Tests the DecodeString method (string overload) with input that has no escape sequences.
//         /// </summary>
//         /// <param name="input">The input string without escapes.</param>
//         [Theory] [Error] (174-31)CS0029 Cannot implicitly convert type 'Parlot.TextSpan' to 'Parlot.UnitTests.TextSpan'
//         [InlineData("")]
//         [InlineData("No escapes here")]
//         public void DecodeString_String_NoEscapes_ReturnsOriginalTextSpan(string input)
//         {
//             // Act
//             TextSpan result = Character.DecodeString(input);
// 
//             // Assert
//             Assert.Equal(input, result.ToString());
//         }
// 
//         /// <summary>
//         /// Tests the DecodeString method (string overload) with escape sequences.
//         /// </summary>
//         [Fact] [Error] (191-31)CS0029 Cannot implicitly convert type 'Parlot.TextSpan' to 'Parlot.UnitTests.TextSpan'
//         public void DecodeString_String_WithEscapes_ReturnsCorrectDecodedTextSpan()
//         {
//             // Arrange
//             string input = "Line1\\nLine2\\tTabbed\\u0021"; // \u0021 is '!'
//             string expected = "Line1\nLine2\tTabbed!";
//             
//             // Act
//             TextSpan result = Character.DecodeString(input);
// 
//             // Assert
//             Assert.Equal(expected, result.ToString());
//         }
// 
//         /// <summary>
//         /// Tests the DecodeString method (TextSpan overload) with input that has no escape sequences.
//         /// </summary>
//         /// <param name="input">The input string without escapes.</param>
//         [Theory] [Error] (210-54)CS1503 Argument 1: cannot convert from 'Parlot.UnitTests.TextSpan' to 'System.ReadOnlySpan<char>'
//         [InlineData("")]
//         [InlineData("PlainText")]
//         public void DecodeString_TextSpan_NoEscapes_ReturnsSameTextSpan(string input)
//         {
//             // Arrange
//             TextSpan textSpan = new TextSpan(input);
// 
//             // Act
//             TextSpan result = Character.DecodeString(textSpan);
// 
//             // Assert
//             Assert.Equal(input, result.ToString());
//         }
// 
//         /// <summary>
//         /// Tests the DecodeString method (TextSpan overload) with escape sequences.
//         /// </summary>
//         [Fact] [Error] (228-54)CS1503 Argument 1: cannot convert from 'Parlot.UnitTests.TextSpan' to 'System.ReadOnlySpan<char>'
//         public void DecodeString_TextSpan_WithEscapes_ReturnsCorrectDecodedTextSpan()
//         {
//             // Arrange
//             string input = "Start\\nMiddle\\u0020End"; // \u0020 is space
//             string expected = "Start\nMiddle End";
//             TextSpan textSpan = new TextSpan(input);
// 
//             // Act
//             TextSpan result = Character.DecodeString(textSpan);
// 
//             // Assert
//             Assert.Equal(expected, result.ToString());
//         }
// 
//         /// <summary>
//         /// Tests the IsWhiteSpace method for characters that are and are not considered as whitespace.
//         /// </summary>
//         /// <param name="ch">The character to evaluate.</param>
//         /// <param name="expected">Expected boolean result.</param>
//         [Theory]
//         [InlineData(' ', true)]
//         [InlineData('\t', true)]
//         [InlineData('a', false)]
//         public void IsWhiteSpace_GivenCharacter_ReturnsCorrectResult(char ch, bool expected)
//         {
//             // Act
//             bool result = Character.IsWhiteSpace(ch);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
// 
//         /// <summary>
//         /// Tests the IsWhiteSpaceOrNewLine method for characters that are whitespace or newline.
//         /// </summary>
//         /// <param name="ch">The character to evaluate.</param>
//         /// <param name="expected">Expected boolean result.</param>
//         [Theory]
//         [InlineData(' ', true)]
//         [InlineData('\n', true)]
//         [InlineData('\r', true)]
//         [InlineData('a', false)]
//         public void IsWhiteSpaceOrNewLine_GivenCharacter_ReturnsCorrectResult(char ch, bool expected)
//         {
//             // Act
//             bool result = Character.IsWhiteSpaceOrNewLine(ch);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
// 
//         /// <summary>
//         /// Tests the IsNewLine method for newline characters.
//         /// </summary>
//         /// <param name="ch">The character to evaluate.</param>
//         /// <param name="expected">Expected boolean result.</param>
//         [Theory]
//         [InlineData('\n', true)]
//         [InlineData('\r', true)]
//         [InlineData('\v', true)]
//         [InlineData(' ', false)]
//         [InlineData('A', false)]
//         public void IsNewLine_GivenCharacter_ReturnsCorrectResult(char ch, bool expected)
//         {
//             // Act
//             bool result = Character.IsNewLine(ch);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
//     }

    /// <summary>
    /// Minimal implementation of TextSpan for testing purposes.
    /// </summary>
    public class TextSpan
    {
        /// <summary>
        /// Gets the underlying string buffer.
        /// </summary>
        public string Buffer { get; }

        /// <summary>
        /// Gets the offset of the span.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the length of the span.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the value of the span as a ReadOnlySpan of char.
        /// </summary>
        public ReadOnlySpan<char> Span => Buffer.AsSpan(Offset, Length);

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSpan"/> class.
        /// </summary>
        /// <param name="text">The underlying text.</param>
        public TextSpan(string text)
        {
            Buffer = text ?? string.Empty;
            Offset = 0;
            Length = Buffer.Length;
        }

        /// <summary>
        /// Returns the text in the span.
        /// </summary>
        /// <returns>The string represented by the span.</returns>
        public override string ToString() => Buffer.Substring(Offset, Length);
    }
}
