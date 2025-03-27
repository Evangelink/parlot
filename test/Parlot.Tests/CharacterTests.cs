using Moq;
using Parlot;
using System;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Character"/> class.
/// </summary>
// public class CharacterTests [Error] (1114-2)CS1038 #endregion directive expected [Error] (10-14)CS0101 The namespace '<global namespace>' already contains a definition for 'CharacterTests'
// {
//     /// <summary>
//     /// Tests that IsInRange returns true when the character is equal to the minimum bound.
//     /// </summary>
//     /// <param name = "ch">The character to test.</param>
//     /// <param name = "min">The lower bound of the range.</param>
//     /// <param name = "max">The upper bound of the range.</param>
//     [Theory]
//     [InlineData('a', 'a', 'z')]
//     [InlineData('0', '0', '9')]
//     [InlineData('\0', '\0', 'A')]
//     public void IsInRange_CharEqualToMin_ReturnsTrue(char ch, char min, char max)
//     {
//         // Act
//         bool result = Character.IsInRange(ch, min, max);
//         // Assert
//         Assert.True(result, $"Expected '{ch}' to be in range [{min}, {max}], since it equals the minimum.");
//     }
// 
//     /// <summary>
//     /// Tests that IsInRange returns true when the character is equal to the maximum bound.
//     /// </summary>
//     /// <param name = "ch">The character to test.</param>
//     /// <param name = "min">The lower bound of the range.</param>
//     /// <param name = "max">The upper bound of the range.</param>
//     [Theory]
//     [InlineData('z', 'a', 'z')]
//     [InlineData('9', '0', '9')]
//     [InlineData('A', '\0', 'A')]
//     public void IsInRange_CharEqualToMax_ReturnsTrue(char ch, char min, char max)
//     {
//         // Act
//         bool result = Character.IsInRange(ch, min, max);
//         // Assert
//         Assert.True(result, $"Expected '{ch}' to be in range [{min}, {max}], since it equals the maximum.");
//     }
// 
//     /// <summary>
//     /// Tests that IsInRange returns true when the character is strictly between the minimum and maximum bounds.
//     /// </summary>
//     /// <param name = "ch">The character to test.</param>
//     /// <param name = "min">The lower bound of the range.</param>
//     /// <param name = "max">The upper bound of the range.</param>
//     [Theory]
//     [InlineData('m', 'a', 'z')]
//     [InlineData('5', '0', '9')]
//     [InlineData('B', 'A', 'C')]
//     public void IsInRange_CharWithinRange_ReturnsTrue(char ch, char min, char max)
//     {
//         // Act
//         bool result = Character.IsInRange(ch, min, max);
//         // Assert
//         Assert.True(result, $"Expected '{ch}' to be within range [{min}, {max}].");
//     }
// 
//     /// <summary>
//     /// Tests that IsInRange returns false when the character is below the minimum bound.
//     /// </summary>
//     /// <param name = "ch">The character to test.</param>
//     /// <param name = "min">The lower bound of the range.</param>
//     /// <param name = "max">The upper bound of the range.</param>
//     [Theory]
//     [InlineData('`', 'a', 'z')]
//     [InlineData('/', '0', '9')]
//     [InlineData('@', 'A', 'Z')]
//     public void IsInRange_CharBeforeMin_ReturnsFalse(char ch, char min, char max)
//     {
//         // Act
//         bool result = Character.IsInRange(ch, min, max);
//         // Assert
//         Assert.False(result, $"Expected '{ch}' to be out of range [{min}, {max}] because it is below the minimum.");
//     }
// 
//     /// <summary>
//     /// Tests that IsInRange returns false when the character is above the maximum bound.
//     /// </summary>
//     /// <param name = "ch">The character to test.</param>
//     /// <param name = "min">The lower bound of the range.</param>
//     /// <param name = "max">The upper bound of the range.</param>
//     [Theory]
//     [InlineData('{', 'a', 'z')]
//     [InlineData(':', '0', '9')]
//     [InlineData('[', 'A', 'Z')]
//     public void IsInRange_CharAfterMax_ReturnsFalse(char ch, char min, char max)
//     {
//         // Act
//         bool result = Character.IsInRange(ch, min, max);
//         // Assert
//         Assert.False(result, $"Expected '{ch}' to be out of range [{min}, {max}] because it is above the maximum.");
//     }
// 
//     /// <summary>
//     /// Tests that IsInRange returns the expected result when the minimum and maximum bounds are the same.
//     /// Only the character equal to the bound should return true.
//     /// </summary>
//     /// <param name = "ch">The character to test.</param>
//     /// <param name = "min">The single value representing both the minimum and maximum.</param>
//     /// <param name = "max">The single value representing both the minimum and maximum.</param>
//     /// <param name = "expected">The expected result.</param>
//     [Theory]
//     [InlineData('x', 'x', 'x', true)]
//     [InlineData('y', 'x', 'x', false)]
//     public void IsInRange_SameMinAndMax_ReturnsExpectedResult(char ch, char min, char max, bool expected)
//     {
//         // Act
//         bool result = Character.IsInRange(ch, min, max);
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests that IsInRange handles cases where the minimum is greater than the maximum by using unsigned arithmetic.
//     /// The expected outcome is computed based on the underlying arithmetic.
//     /// </summary>
//     /// <param name = "ch">The character to test.</param>
//     /// <param name = "min">The lower bound (greater than max in these cases).</param>
//     /// <param name = "max">The upper bound (less than min in these cases).</param>
//     [Theory]
//     [InlineData('a', 'z', 'a')]
//     [InlineData('b', 'z', 'a')]
//     [InlineData('z', 'z', 'a')]
//     public void IsInRange_MinGreaterThanMax_ReturnsExpectedResult(char ch, char min, char max)
//     {
//         // Act
//         bool result = Character.IsInRange(ch, min, max);
//         // The underlying logic uses unsigned arithmetic:
//         // (ch - (uint)min) <= (max - (uint)min)
//         uint diffCh = (uint)ch - (uint)min;
//         uint diffBoundary = (uint)max - (uint)min;
//         bool expected = diffCh <= diffBoundary;
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests the ScanHexEscape(string, int, out int) method with valid hexadecimal input.
//     /// The test ensures that when a valid hex sequence is provided at the given index, the correct decoded character 
//     /// is returned and the appropriate number of characters consumed is set via the out parameter.
//     /// </summary>
//     /// <param name = "input">The input string containing the hexadecimal escape sequence.</param>
//     /// <param name = "index">The starting index where the hex sequence appears.</param>
//     /// <param name = "expected">The expected decoded character.</param>
//     /// <param name = "expectedLength">The expected number of characters that represent the escape.</param>
//     [Theory]
//     [InlineData("0041XYZ", 0, 'A', 4)]
//     [InlineData("0062Extra", 0, 'b', 4)]
//     [InlineData("41", 0, (char)0x41, 2)] // Assumes that two hex digits are acceptable.
//     public void ScanHexEscape_StringOverload_ValidInput_ReturnsDecodedChar(string input, int index, char expected, int expectedLength)
//     {
//         // Arrange & Act
//         char result = Character.ScanHexEscape(input, index, out int actualLength);
//         // Assert
//         Assert.Equal(expected, result);
//         Assert.Equal(expectedLength, actualLength);
//     }
// 
//     /// <summary>
//     /// Tests the ScanHexEscape(ReadOnlySpan&lt;char&gt;, out int) method with valid hexadecimal input.
//     /// The test ensures that when a valid hex sequence is provided in a span, the method decodes it correctly
//     /// and outputs the correct length of the processed escape sequence.
//     /// </summary>
//     /// <param name = "input">The input string (converted to a span) containing the hexadecimal escape sequence.</param>
//     /// <param name = "expected">The expected decoded character.</param>
//     /// <param name = "expectedLength">The expected number of characters that represent the escape.</param>
//     [Theory]
//     [InlineData("0042", 'B', 4)]
//     [InlineData("007A", 'z', 4)]
//     [InlineData("2A", (char)0x2A, 2)] // Assumes that two hex digits can be processed.
//     public void ScanHexEscape_SpanOverload_ValidInput_ReturnsDecodedChar(string input, char expected, int expectedLength)
//     {
//         // Arrange
//         ReadOnlySpan<char> span = input.AsSpan();
//         // Act
//         char result = Character.ScanHexEscape(span, out int actualLength);
//         // Assert
//         Assert.Equal(expected, result);
//         Assert.Equal(expectedLength, actualLength);
//     }
// 
//     /// <summary>
//     /// Tests the ScanHexEscape(string, int, out int) method with an index that is out of range.
//     /// The test verifies that an exception is thrown when the provided index is not valid for the input string.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_StringOverload_IndexOutOfRange_ThrowsException()
//     {
//         // Arrange
//         string input = "1234";
//         int invalidIndex = input.Length; // Index equal to the length is out of range.
//         // Act & Assert
//         Assert.ThrowsAny<Exception>(() => Character.ScanHexEscape(input, invalidIndex, out int _));
//     }
// 
//     /// <summary>
//     /// Tests the ScanHexEscape(ReadOnlySpan&lt;char&gt;, out int) method with insufficient hex digits.
//     /// The test expects that if the span does not contain enough characters to form a valid hexadecimal escape,
//     /// an exception is thrown.
//     /// </summary>
//     [Fact] [Error] (215-67)CS8175 Cannot use ref local 'span' inside an anonymous method, lambda expression, or query expression
//     public void ScanHexEscape_SpanOverload_InsufficientLength_ThrowsException()
//     {
//         // Arrange
//         ReadOnlySpan<char> span = "0A".AsSpan(); // Assuming a valid escape requires more than 2 hex digits.
//         // Act & Assert
//         Assert.ThrowsAny<Exception>(() => Character.ScanHexEscape(span, out int _));
//     }
// 
//     /// <summary>
//     /// Tests the ScanHexEscape(string, int, out int) method with a null string input.
//     /// The test ensures that providing a null string causes an ArgumentNullException to be thrown.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_StringOverload_NullInput_ThrowsArgumentNullException()
//     {
//         // Arrange
//         string input = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Character.ScanHexEscape(input, 0, out int _));
//     }
// 
// #region Tests for ScanHexEscape(ReadOnlySpan<char>, out int length)
//     /// <summary>
//     /// Tests that ScanHexEscape(ReadOnlySpan<char>, out int) returns the correct character value and length
//     /// when the input contains valid hex digits.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_ReadOnlySpan_ValidHexDigits_ReturnsCorrectEscape()
//     {
//         // Arrange
//         // Input string with a backslash followed by two valid hex digits "1" and "A" (0x1A). 
//         var input = "\\1A".AsSpan();
//         int expectedLength = 2;
//         int outLength;
//         // Act
//         char result = Character.ScanHexEscape(input, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal((char)0x1A, result);
//     }
// 
//     /// <summary>
//     /// Tests that ScanHexEscape(ReadOnlySpan<char>, out int) returns a zero value and zero length
//     /// when there are no hex digits following the escape identifier.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_ReadOnlySpan_NoHexDigits_ReturnsZeroAndLengthZero()
//     {
//         // Arrange
//         // Input string with a backslash followed by a non-hex character.
//         var input = "\\G".AsSpan();
//         int expectedLength = 0;
//         int outLength;
//         // Act
//         char result = Character.ScanHexEscape(input, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal((char)0, result);
//     }
// 
//     /// <summary>
//     /// Tests that ScanHexEscape(ReadOnlySpan<char>, out int) returns a zero value and zero length
//     /// when the input span contains only the escape indicator with no following characters.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_ReadOnlySpan_InsufficientLength_ReturnsZeroAndLengthZero()
//     {
//         // Arrange
//         // Input span with only one character.
//         var input = "\\".AsSpan();
//         int expectedLength = 0;
//         int outLength;
//         // Act
//         char result = Character.ScanHexEscape(input, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal((char)0, result);
//     }
// 
//     /// <summary>
//     /// Tests that ScanHexEscape(ReadOnlySpan<char>, out int) processes only valid consecutive hex digits
//     /// and stops scanning when an invalid digit is encountered.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_ReadOnlySpan_StopsAtInvalidHexDigit_ReturnsPartialValueAndLength()
//     {
//         // Arrange
//         // Input example: "\1Z3" should only process "1" as hex digit.
//         var input = "\\1Z3".AsSpan();
//         int expectedLength = 1;
//         int outLength;
//         // '1' in hex equals 1.
//         char expectedChar = (char)1;
//         // Act
//         char result = Character.ScanHexEscape(input, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal(expectedChar, result);
//     }
// 
//     /// <summary>
//     /// Tests that ScanHexEscape(ReadOnlySpan<char>, out int) processes a maximum of four hex digits,
//     /// even if more digits are available.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_ReadOnlySpan_MaxDigitsProcessed_ReturnsValueFromFourDigits()
//     {
//         // Arrange
//         // Input with more than four hex digits: "\12345" should only consider "1234".
//         var input = "\\12345".AsSpan();
//         int expectedLength = 4;
//         int outLength;
//         // hex "1234" equals 0x1234 = 4660 decimal.
//         char expectedChar = (char)0x1234;
//         // Act
//         char result = Character.ScanHexEscape(input, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal(expectedChar, result);
//     }
// 
// #endregion
// #region Tests for ScanHexEscape(string, int, out int length)
//     /// <summary>
//     /// Tests that ScanHexEscape(string, int, out int) returns the correct escape value and length
//     /// when provided with a valid hex sequence starting at the specified index.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_String_ValidHexDigits_ReturnsCorrectEscape()
//     {
//         // Arrange
//         // Input string where the escape sequence starts at index 0.
//         string input = "\\1A";
//         int index = 0;
//         int expectedLength = 2;
//         int outLength;
//         // Act
//         char result = Character.ScanHexEscape(input, index, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal((char)0x1A, result);
//     }
// 
//     /// <summary>
//     /// Tests that ScanHexEscape(string, int, out int) returns a zero value and zero length
//     /// when the character following the escape indicator is not a hex digit.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_String_NoHexDigits_ReturnsZeroAndLengthZero()
//     {
//         // Arrange
//         // Input string with an invalid hex sequence beginning at index 0.
//         string input = "\\G";
//         int index = 0;
//         int expectedLength = 0;
//         int outLength;
//         // Act
//         char result = Character.ScanHexEscape(input, index, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal((char)0, result);
//     }
// 
//     /// <summary>
//     /// Tests that ScanHexEscape(string, int, out int) correctly processes the escape sequence
//     /// when it does not start at index 0.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_String_NonZeroIndex_ReturnsCorrectEscape()
//     {
//         // Arrange
//         // Input string where the escape sequence starts at index 3.
//         // "abc\\FFxyz" - the backslash is at index 3; following hex digits "F" and "F" should be processed.
//         string input = "abc\\FFxyz";
//         int index = 3;
//         int expectedLength = 2;
//         int outLength;
//         char expectedChar = (char)0xFF; // Hex FF = 255
//         // Act
//         char result = Character.ScanHexEscape(input, index, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal(expectedChar, result);
//     }
// 
//     /// <summary>
//     /// Tests that ScanHexEscape(string, int, out int) returns zero value and zero length
//     /// when the provided index points to the last character (i.e. no hex digits available).
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_String_IndexAtEnd_ReturnsZeroAndLengthZero()
//     {
//         // Arrange
//         // Input string with only the escape indicator at the end.
//         string input = "test\\";
//         int index = input.Length - 1; // points to the backslash
//         int expectedLength = 0;
//         int outLength;
//         // Act
//         char result = Character.ScanHexEscape(input, index, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal((char)0, result);
//     }
// 
//     /// <summary>
//     /// Tests that ScanHexEscape(string, int, out int) only processes up to four hex digits,
//     /// even if additional valid hex digits follow.
//     /// </summary>
//     [Fact]
//     public void ScanHexEscape_String_MoreThanFourHexDigits_ReturnsValueFromFourDigits()
//     {
//         // Arrange
//         // Input string with a backslash followed by five hex digits. Only the first four should be processed.
//         string input = "\\12345";
//         int index = 0;
//         int expectedLength = 4;
//         int outLength;
//         char expectedChar = (char)0x1234; // Only "1234" is processed
//         // Act
//         char result = Character.ScanHexEscape(input, index, out outLength);
//         // Assert
//         Assert.Equal(expectedLength, outLength);
//         Assert.Equal(expectedChar, result);
//     }
// 
// #region DecodeString(ReadOnlySpan<char>) Tests
//     /// <summary>
//     /// Tests that DecodeString(ReadOnlySpan<char>) returns the same span when there are no escape characters.
//     /// </summary>
//     [Fact]
//     public void DecodeString_ReadOnlySpan_NoEscape_ReturnsOriginalSpan()
//     {
//         // Arrange
//         string input = "Hello World";
//         ReadOnlySpan<char> spanInput = input.AsSpan();
//         // Act
//         ReadOnlySpan<char> result = Character.DecodeString(spanInput);
//         // Assert
//         Assert.Equal(input, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that DecodeString(ReadOnlySpan<char>) decodes common escape sequences correctly.
//     /// </summary>
//     [Fact]
//     public void DecodeString_ReadOnlySpan_WithStandardEscapes_ReturnsDecodedSpan()
//     {
//         // Arrange
//         string input = "Line1\\nLine2";
//         // Expected: \n converted to newline character.
//         string expected = "Line1\nLine2";
//         ReadOnlySpan<char> spanInput = input.AsSpan();
//         // Act
//         ReadOnlySpan<char> result = Character.DecodeString(spanInput);
//         // Assert
//         Assert.Equal(expected, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that DecodeString(ReadOnlySpan<char>) correctly decodes a string containing several escape sequences.
//     /// </summary>
//     [Fact]
//     public void DecodeString_ReadOnlySpan_AllEscapes_ReturnsDecodedSpan()
//     {
//         // Arrange
//         // Input contains escapes: \' -> ', \" -> ", \\ -> \, \0, \a, \b, \f, \n, \r, \t, \v.
//         string input = "\\'\\\"\\\\\\0\\a\\b\\f\\n\\r\\t\\v";
//         string expected = "\'\"\\\0\a\b\f\n\r\t\v";
//         ReadOnlySpan<char> spanInput = input.AsSpan();
//         // Act
//         ReadOnlySpan<char> result = Character.DecodeString(spanInput);
//         // Assert
//         Assert.Equal(expected, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that DecodeString(ReadOnlySpan<char>) throws an exception when the input ends with a single backslash.
//     /// This tests the boundary condition where escape sequence is incomplete.
//     /// </summary>
//     [Fact] [Error] (497-81)CS8175 Cannot use ref local 'spanInput' inside an anonymous method, lambda expression, or query expression
//     public void DecodeString_ReadOnlySpan_TrailingBackslash_ThrowsException()
//     {
//         // Arrange
//         string input = "Test\\";
//         ReadOnlySpan<char> spanInput = input.AsSpan();
//         // Act & Assert
//         Assert.ThrowsAny<IndexOutOfRangeException>(() => Character.DecodeString(spanInput));
//     }
// 
//     /// <summary>
//     /// Tests that DecodeString(ReadOnlySpan<char>) correctly decodes a long input (length > 128) without escapes,
//     /// ensuring that the rented buffer path works as expected.
//     /// </summary>
//     [Fact]
//     public void DecodeString_ReadOnlySpan_LongInput_NoEscape_ReturnsOriginalSpan()
//     {
//         // Arrange
//         string input = new string ('a', 130);
//         ReadOnlySpan<char> spanInput = input.AsSpan();
//         // Act
//         ReadOnlySpan<char> result = Character.DecodeString(spanInput);
//         // Assert
//         Assert.Equal(input, result.ToString());
//     }
// 
// #endregion
// #region DecodeString(string) Tests
//     /// <summary>
//     /// Tests that DecodeString(string) returns the expected decoded TextSpan when the input has no escape characters.
//     /// </summary>
//     [Fact]
//     public void DecodeString_String_NoEscape_ReturnsOriginalTextSpan()
//     {
//         // Arrange
//         string input = "Simple string without escapes";
//         // Act
//         TextSpan result = Character.DecodeString(input);
//         // Assert
//         // Assuming TextSpan's ToString returns the underlying string.
//         Assert.Equal(input, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that DecodeString(string) decodes escape sequences correctly.
//     /// </summary>
//     [Fact]
//     public void DecodeString_String_WithEscape_ReturnsDecodedTextSpan()
//     {
//         // Arrange
//         string input = "Test\\nString";
//         string expected = "Test\nString";
//         // Act
//         TextSpan result = Character.DecodeString(input);
//         // Assert
//         Assert.Equal(expected, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that DecodeString(string) handles an empty string correctly.
//     /// </summary>
//     [Fact]
//     public void DecodeString_String_Empty_ReturnsEmptyTextSpan()
//     {
//         // Arrange
//         string input = string.Empty;
//         // Act
//         TextSpan result = Character.DecodeString(input);
//         // Assert
//         Assert.Equal(input, result.ToString());
//     }
// 
// #endregion
// #region DecodeString(TextSpan) Tests
//     /// <summary>
//     /// Tests that DecodeString(TextSpan) returns the original TextSpan when there are no escape characters.
//     /// </summary>
//     [Fact]
//     public void DecodeString_TextSpan_NoEscape_ReturnsOriginalTextSpan()
//     {
//         // Arrange
//         // Assuming TextSpan has a constructor that takes a string.
//         TextSpan inputSpan = new TextSpan("No escapes here");
//         // Act
//         TextSpan result = Character.DecodeString(inputSpan);
//         // Assert
//         Assert.Equal(inputSpan.ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that DecodeString(TextSpan) decodes escape sequences correctly.
//     /// </summary>
//     [Fact]
//     public void DecodeString_TextSpan_WithEscape_ReturnsDecodedTextSpan()
//     {
//         // Arrange
//         TextSpan inputSpan = new TextSpan("Line1\\nLine2");
//         string expected = "Line1\nLine2";
//         // Act
//         TextSpan result = Character.DecodeString(inputSpan);
//         // Assert
//         Assert.Equal(expected, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the DecodeString(ReadOnlySpan<char>) method with input that contains no escape sequences.
//     /// The expected result is identical to the input.
//     /// </summary>
//     /// <param name = "input">A string with no escape sequences.</param>
//     [Theory]
//     [InlineData("")]
//     [InlineData("Hello World")]
//     public void DecodeString_ReadOnlySpan_NoEscape_ReturnsSameText(string input)
//     {
//         // Arrange
//         ReadOnlySpan<char> span = input.AsSpan();
//         // Act
//         ReadOnlySpan<char> resultSpan = Character.DecodeString(span);
//         string result = new string (resultSpan);
//         // Assert
//         Assert.Equal(input, result);
//     }
// 
//     /// <summary>
//     /// Tests the DecodeString(ReadOnlySpan<char>) method with input containing escape sequences.
//     /// It verifies that common escapes such as newline, tab, and hex escapes are decoded correctly.
//     /// </summary>
//     [Fact]
//     public void DecodeString_ReadOnlySpan_WithEscapes_ReturnsDecodedText()
//     {
//         // Arrange
//         // The input string contains escape sequences:
//         // \n should be decoded to newline, \t to tab and \x21 to '!'
//         string input = "Line1\\nLine2\\tTest\\x21";
//         string expected = "Line1\nLine2\tTest!";
//         // Act
//         ReadOnlySpan<char> span = input.AsSpan();
//         ReadOnlySpan<char> resultSpan = Character.DecodeString(span);
//         string result = new string (resultSpan);
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests the DecodeString(TextSpan) method with input that contains no escape sequences.
//     /// The expected result is that the returned TextSpan converts back to the original text.
//     /// </summary>
//     /// <param name = "input">A string with no escape sequences.</param>
//     [Theory]
//     [InlineData("")]
//     [InlineData("Hello World")]
//     public void DecodeString_TextSpan_NoEscape_ReturnsSameText(string input)
//     {
//         // Arrange
//         var textSpan = new TextSpan(input);
//         // Act
//         TextSpan resultTextSpan = Character.DecodeString(textSpan);
//         // Assert
//         Assert.Equal(input, resultTextSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the DecodeString(TextSpan) method with input containing escape sequences.
//     /// It verifies that the method decodes the escape sequences correctly.
//     /// </summary>
//     [Fact]
//     public void DecodeString_TextSpan_WithEscapes_ReturnsDecodedText()
//     {
//         // Arrange
//         string input = "Line1\\nLine2\\tTest\\x21";
//         string expected = "Line1\nLine2\tTest!";
//         var textSpan = new TextSpan(input);
//         // Act
//         TextSpan resultTextSpan = Character.DecodeString(textSpan);
//         // Assert
//         Assert.Equal(expected, resultTextSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the DecodeString(string) method with input that contains no escape sequences.
//     /// The expected result is a TextSpan that represents the original text.
//     /// </summary>
//     /// <param name = "input">A string with no escape sequences.</param>
//     [Theory]
//     [InlineData("")]
//     [InlineData("Hello World")]
//     public void DecodeString_String_NoEscape_ReturnsCorrectText(string input)
//     {
//         // Arrange & Act
//         TextSpan resultTextSpan = Character.DecodeString(input);
//         // Assert
//         Assert.Equal(input, resultTextSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the DecodeString(string) method with input containing escape sequences.
//     /// It verifies that the method decodes the escape sequences correctly within the returned TextSpan.
//     /// </summary>
//     [Fact]
//     public void DecodeString_String_WithEscapes_ReturnsCorrectText()
//     {
//         // Arrange
//         string input = "Line1\\nLine2\\tTest\\x21";
//         string expected = "Line1\nLine2\tTest!";
//         // Act
//         TextSpan resultTextSpan = Character.DecodeString(input);
//         // Assert
//         Assert.Equal(expected, resultTextSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the DecodeString(string) method when provided a null input.
//     /// It expects the method to throw an ArgumentNullException.
//     /// </summary>
//     [Fact]
//     public void DecodeString_String_NullInput_ThrowsArgumentNullException()
//     {
//         // Arrange
//         string input = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Character.DecodeString(input));
//     }
// 
//     /// <summary>
//     /// Tests DecodeString(ReadOnlySpan<char>) when the input contains no escape characters.
//     /// Expected outcome: the output span should be identical to the input.
//     /// </summary>
//     [Fact]
//     public void DecodeString_ReadOnlySpan_NoEscape_ReturnsSameContent()
//     {
//         // Arrange
//         string input = "Hello World";
//         ReadOnlySpan<char> spanInput = input.AsSpan();
//         // Act
//         ReadOnlySpan<char> result = Character.DecodeString(spanInput);
//         // Assert
//         Assert.Equal(input, new string (result));
//     }
// 
//     /// <summary>
//     /// Tests DecodeString(ReadOnlySpan<char>) when the input contains an escape sequence.
//     /// Assumes that a "\\n" sequence is converted into a newline character.
//     /// Expected outcome: the output span should have the escape sequence processed.
//     /// </summary>
//     [Fact]
//     public void DecodeString_ReadOnlySpan_WithEscape_ReturnsDecodedContent()
//     {
//         // Arrange
//         string input = "Hello\\nWorld";
//         ReadOnlySpan<char> spanInput = input.AsSpan();
//         string expected = "Hello\nWorld";
//         // Act
//         ReadOnlySpan<char> result = Character.DecodeString(spanInput);
//         // Assert
//         Assert.Equal(expected, new string (result));
//     }
// 
//     /// <summary>
//     /// Tests DecodeString(string) when the input string has no escape characters.
//     /// Expected outcome: the returned TextSpan represents the original string unchanged.
//     /// </summary>
//     [Fact]
//     public void DecodeString_String_NoEscape_ReturnsSameTextSpan()
//     {
//         // Arrange
//         string input = "NoEscapeHere";
//         // Act
//         TextSpan result = Character.DecodeString(input);
//         // Assert
//         Assert.Equal(input, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests DecodeString(string) when the input string contains an escape sequence.
//     /// Assumes that "\\t" sequence is converted into a tab character.
//     /// Expected outcome: the returned TextSpan contains the decoded string.
//     /// </summary>
//     [Fact] [Error] (768-17)CS0111 Type 'CharacterTests' already defines a member called 'DecodeString_String_WithEscape_ReturnsDecodedTextSpan' with the same parameter types
//     public void DecodeString_String_WithEscape_ReturnsDecodedTextSpan()
//     {
//         // Arrange
//         string input = "Column1\\tColumn2";
//         string expected = "Column1\tColumn2";
//         // Act
//         TextSpan result = Character.DecodeString(input);
//         // Assert
//         Assert.Equal(expected, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests DecodeString(TextSpan) when the TextSpan's buffer is empty.
//     /// Expected outcome: the returned TextSpan is equivalent to the input.
//     /// </summary>
//     [Fact]
//     public void DecodeString_TextSpan_EmptyBuffer_ReturnsSameTextSpan()
//     {
//         // Arrange
//         TextSpan textSpan = new TextSpan(string.Empty);
//         // Act
//         TextSpan result = Character.DecodeString(textSpan);
//         // Assert
//         Assert.Equal(textSpan.ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests DecodeString(TextSpan) when the input does not contain any escape characters.
//     /// Expected outcome: the returned TextSpan is identical to the input.
//     /// </summary>
//     [Fact]
//     public void DecodeString_TextSpan_NoEscape_ReturnsSameTextSpan()
//     {
//         // Arrange
//         TextSpan textSpan = new TextSpan("SampleText");
//         // Act
//         TextSpan result = Character.DecodeString(textSpan);
//         // Assert
//         Assert.Equal(textSpan.ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests DecodeString(TextSpan) when the input contains an escape sequence.
//     /// Assumes that a "\\n" sequence is converted into a newline character.
//     /// Expected outcome: the returned TextSpan contains the decoded string.
//     /// </summary>
//     [Fact] [Error] (815-17)CS0111 Type 'CharacterTests' already defines a member called 'DecodeString_TextSpan_WithEscape_ReturnsDecodedTextSpan' with the same parameter types
//     public void DecodeString_TextSpan_WithEscape_ReturnsDecodedTextSpan()
//     {
//         // Arrange
//         TextSpan textSpan = new TextSpan("Line1\\nLine2");
//         string expected = "Line1\nLine2";
//         // Act
//         TextSpan result = Character.DecodeString(textSpan);
//         // Assert
//         Assert.Equal(expected, result.ToString());
//     }
// 
//     /// <summary>
//     /// Retrieves the private static 'HexValue' method from the Character class via reflection.
//     /// </summary>
//     /// <returns>The MethodInfo representing the 'HexValue' method.</returns>
//     private static MethodInfo GetHexValueMethod()
//     {
//         var method = typeof(Character).GetMethod("HexValue", BindingFlags.NonPublic | BindingFlags.Static);
//         if (method == null)
//         {
//             throw new InvalidOperationException("The method 'HexValue' was not found in Character.");
//         }
// 
//         return method;
//     }
// 
//     /// <summary>
//     /// Tests that HexValue correctly converts the character '0' to the integer 0.
//     /// </summary>
//     [Fact]
//     public void HexValue_WithDigit0_Returns0()
//     {
//         // Arrange
//         var method = GetHexValueMethod();
//         char input = '0';
//         int expected = 0;
//         // Act
//         object result = method.Invoke(null, new object[] { input });
//         int actual = (int)result;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that HexValue correctly converts the character '9' to the integer 9.
//     /// </summary>
//     [Fact]
//     public void HexValue_WithDigit9_Returns9()
//     {
//         // Arrange
//         var method = GetHexValueMethod();
//         char input = '9';
//         int expected = 9;
//         // Act
//         object result = method.Invoke(null, new object[] { input });
//         int actual = (int)result;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that HexValue correctly converts the uppercase hexadecimal digit 'A' to the integer 10.
//     /// </summary>
//     [Fact]
//     public void HexValue_WithUppercaseA_Returns10()
//     {
//         // Arrange
//         var method = GetHexValueMethod();
//         char input = 'A';
//         int expected = 10;
//         // Act
//         object result = method.Invoke(null, new object[] { input });
//         int actual = (int)result;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that HexValue correctly converts the uppercase hexadecimal digit 'F' to the integer 15.
//     /// </summary>
//     [Fact]
//     public void HexValue_WithUppercaseF_Returns15()
//     {
//         // Arrange
//         var method = GetHexValueMethod();
//         char input = 'F';
//         int expected = 15;
//         // Act
//         object result = method.Invoke(null, new object[] { input });
//         int actual = (int)result;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that HexValue correctly converts the lowercase hexadecimal digit 'a' to the integer 10.
//     /// </summary>
//     [Fact]
//     public void HexValue_WithLowercaseA_Returns10()
//     {
//         // Arrange
//         var method = GetHexValueMethod();
//         char input = 'a';
//         int expected = 10;
//         // Act
//         object result = method.Invoke(null, new object[] { input });
//         int actual = (int)result;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that HexValue correctly converts the lowercase hexadecimal digit 'f' to the integer 15.
//     /// </summary>
//     [Fact]
//     public void HexValue_WithLowercaseF_Returns15()
//     {
//         // Arrange
//         var method = GetHexValueMethod();
//         char input = 'f';
//         int expected = 15;
//         // Act
//         object result = method.Invoke(null, new object[] { input });
//         int actual = (int)result;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that HexValue throws a FormatException when an invalid hexadecimal character (e.g., 'G') is provided.
//     /// </summary>
//     [Fact]
//     public void HexValue_WithInvalidCharacter_ThrowsFormatException()
//     {
//         // Arrange
//         var method = GetHexValueMethod();
//         char input = 'G'; // 'G' is not a valid hexadecimal character
//         // Act & Assert
//         var exception = Assert.Throws<TargetInvocationException>(() => method.Invoke(null, new object[] { input }));
//         Assert.IsType<FormatException>(exception.InnerException);
//     }
// 
//     /// <summary>
//     /// Tests that HexValue throws a FormatException when a non-alphanumeric character (e.g., '!') is provided.
//     /// </summary>
//     [Fact]
//     public void HexValue_WithNonAlphanumericCharacter_ThrowsFormatException()
//     {
//         // Arrange
//         var method = GetHexValueMethod();
//         char input = '!';
//         // Act & Assert
//         var exception = Assert.Throws<TargetInvocationException>(() => method.Invoke(null, new object[] { input }));
//         Assert.IsType<FormatException>(exception.InnerException);
//     }
// 
//     /// <summary>
//     /// Tests that the IsWhiteSpace method returns true for typical white space characters.
//     /// This test uses characters such as space and tab which are expected to be marked as white space
//     /// by the underlying implementation.
//     /// </summary>
//     /// <param name = "input">The white space character to test.</param>
//     [Theory]
//     [InlineData(' ')]
//     [InlineData('\t')]
//     public void IsWhiteSpace_WithWhiteSpaceCharacter_ReturnsTrue(char input)
//     {
//         // Act
//         bool result = Character.IsWhiteSpace(input);
//         // Assert
//         Assert.True(result, $"Expected Character.IsWhiteSpace to return true for white space character '{input}', but it returned false.");
//     }
// 
//     /// <summary>
//     /// Tests that the IsWhiteSpace method returns false for characters that are not flagged as white space.
//     /// This test covers various non-white space characters including alphabetical, numeral, punctuation, and control characters.
//     /// </summary>
//     /// <param name = "input">The non-white space character to test.</param>
//     [Theory]
//     [InlineData('A')]
//     [InlineData('0')]
//     [InlineData('-')]
//     [InlineData('\n')]
//     [InlineData('\r')]
//     [InlineData('\v')]
//     [InlineData('\u0000')]
//     [InlineData('\u00A0')]
//     public void IsWhiteSpace_WithNonWhiteSpaceCharacter_ReturnsFalse(char input)
//     {
//         // Act
//         bool result = Character.IsWhiteSpace(input);
//         // Assert
//         Assert.False(result, $"Expected Character.IsWhiteSpace to return false for non-white space character '{input}', but it returned true.");
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "Character.IsWhiteSpaceOrNewLine(char)"/> method with various character inputs.
//     /// This test verifies that the method returns true for known whitespace or new line characters,
//     /// and false for characters that are not considered whitespace or new line.
//     /// </summary>
//     /// <param name = "input">The character input to test.</param>
//     /// <param name = "expected">The expected boolean result.</param>
//     [Theory]
//     [InlineData(' ', true)]
//     [InlineData('\n', true)]
//     [InlineData('\r', true)]
//     [InlineData('\t', true)]
//     [InlineData('A', false)]
//     [InlineData('\0', false)]
//     [InlineData('\uffff', false)]
//     public void IsWhiteSpaceOrNewLine_WithVariousCharacters_ReturnsExpectedResult(char input, bool expected)
//     {
//         // Act
//         bool actual = Character.IsWhiteSpaceOrNewLine(input);
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "Character.IsWhiteSpaceOrNewLine(char)"/> method for a boundary case.
//     /// This test uses the minimum possible char value and ensures the method does not throw and returns false.
//     /// </summary>
//     [Fact]
//     public void IsWhiteSpaceOrNewLine_WithCharMinValue_ReturnsFalse()
//     {
//         // Arrange
//         char input = char.MinValue;
//         // Act
//         bool actual = Character.IsWhiteSpaceOrNewLine(input);
//         // Assert
//         Assert.False(actual);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "Character.IsWhiteSpaceOrNewLine(char)"/> method for a boundary case.
//     /// This test uses the maximum possible char value and ensures the method does not throw and returns false.
//     /// </summary>
//     [Fact]
//     public void IsWhiteSpaceOrNewLine_WithCharMaxValue_ReturnsFalse()
//     {
//         // Arrange
//         char input = char.MaxValue;
//         // Act
//         bool actual = Character.IsWhiteSpaceOrNewLine(input);
//         // Assert
//         Assert.False(actual);
//     }
// 
//     /// <summary>
//     /// Tests that the IsNewLine method returns true for valid new line characters.
//     /// </summary>
//     /// <param name = "input">The character to test.</param>
//     [Theory]
//     [InlineData('\n')]
//     [InlineData('\r')]
//     [InlineData('\v')]
//     public void IsNewLine_WhenInputIsNewLineCharacter_ReturnsTrue(char input)
//     {
//         // Act
//         bool result = Character.IsNewLine(input);
//         // Assert
//         Assert.True(result, $"Expected true for new line character '{input}', but got false.");
//     }
// 
//     /// <summary>
//     /// Tests that the IsNewLine method returns false for characters that are not considered new lines.
//     /// </summary>
//     /// <param name = "input">The character to test.</param>
//     [Theory]
//     [InlineData('a')]
//     [InlineData(' ')]
//     [InlineData('0')]
//     [InlineData('\t')]
//     [InlineData('X')]
//     public void IsNewLine_WhenInputIsNotNewLineCharacter_ReturnsFalse(char input)
//     {
//         // Act
//         bool result = Character.IsNewLine(input);
//         // Assert
//         Assert.False(result, $"Expected false for non new line character '{input}', but got true.");
//     }
// 
//     /// <summary>
//     /// Tests that the IsNewLine method correctly handles boundary characters.
//     /// Validates that char.MinValue and char.MaxValue are not treated as new line characters.
//     /// </summary>
//     [Fact]
//     public void IsNewLine_WhenInputIsBoundaryCharacter_ReturnsFalse()
//     {
//         // Arrange
//         char minChar = char.MinValue;
//         char maxChar = char.MaxValue;
//         // Act
//         bool resultMin = Character.IsNewLine(minChar);
//         bool resultMax = Character.IsNewLine(maxChar);
//         // Assert
//         Assert.False(resultMin, $"Expected false for char.MinValue, but got true.");
//         Assert.False(resultMax, $"Expected false for char.MaxValue, but got true.");
//     }
// }