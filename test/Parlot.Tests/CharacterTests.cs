using System;
using System.Buffers;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Character"/> class.
    /// </summary>
    public class CharacterTests
    {
        /// <summary>
        /// Tests the IsInRange method using characters that are within and outside the specified range.
        /// Expected outcome: Returns true for characters within range and false otherwise.
        /// </summary>
        /// <param name="ch">The character to check.</param>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        /// <param name="expected">The expected result.</param>
        [Theory]
        [InlineData('b', 'a', 'c', true)]
        [InlineData('a', 'a', 'c', true)]
        [InlineData('c', 'a', 'c', true)]
        [InlineData('d', 'a', 'c', false)]
        public void IsInRange_ValidInput_ReturnsExpectedResult(char ch, char min, char max, bool expected)
        {
            // Act
            bool actual = Character.IsInRange(ch, min, max);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Tests the ScanHexEscape(ReadOnlySpan&lt;char&gt;) method with a valid hexadecimal escape sequence.
        /// Expected outcome: Returns the correct character and sets the length to the number of processed hex digits.
        /// </summary>
        [Fact]
        public void ScanHexEscape_ReadOnlySpan_ValidHexEscape_ReturnsDecodedCharAndLength()
        {
            // Arrange
            // The input simulates a valid hexadecimal escape sequence (excluding the escape indicator) "u0041"
            // where '0', '0', '4', '1' should be parsed to give 'A'.
            ReadOnlySpan<char> span = "u0041".AsSpan();

            // Act
            int length;
            char result = Character.ScanHexEscape(span, out length);

            // Assert
            Assert.Equal('A', result);
            Assert.Equal(4, length);
        }

        /// <summary>
        /// Tests the ScanHexEscape(ReadOnlySpan&lt;char&gt;) method with an invalid hexadecimal escape sequence.
        /// Expected outcome: Returns a null character and length remains zero when non-hex digit is encountered.
        /// </summary>
        [Fact]
        public void ScanHexEscape_ReadOnlySpan_InvalidHexEscape_ReturnsDefaultCharAndZeroLength()
        {
            // Arrange
            // The sequence "uZ123" is invalid because 'Z' is not a valid hex digit.
            ReadOnlySpan<char> span = "uZ123".AsSpan();

            // Act
            int length;
            char result = Character.ScanHexEscape(span, out length);

            // Assert
            Assert.Equal('\0', result);
            Assert.Equal(0, length);
        }

        /// <summary>
        /// Tests the ScanHexEscape(string, int, out int) overload with valid input.
        /// Expected outcome: Returns the correctly decoded character and sets the length to the count of processed hex digits.
        /// </summary>
        [Fact]
        public void ScanHexEscape_StringOverload_ValidHexEscape_ReturnsDecodedCharAndLength()
        {
            // Arrange
            // Provide a string that starts with a valid escape sequence.
            string text = "u0042"; // expected result is 'B'
            int index = 0;

            // Act
            char result = Character.ScanHexEscape(text, index, out int length);

            // Assert
            Assert.Equal('B', result);
            Assert.Equal(4, length);
        }

        /// <summary>
        /// Tests the DecodeString(ReadOnlySpan&lt;char&gt;) method when the input contains no escape characters.
        /// Expected outcome: Returns the input span unchanged.
        /// </summary>
        [Fact]
        public void DecodeString_ReadOnlySpan_NoEscapeCharacters_ReturnsOriginalSpan()
        {
            // Arrange
            string input = "No escapes here!";
            ReadOnlySpan<char> span = input.AsSpan();

            // Act
            ReadOnlySpan<char> result = Character.DecodeString(span);

            // Assert
            Assert.Equal(input, result.ToString());
        }

        /// <summary>
        /// Tests the DecodeString(ReadOnlySpan&lt;char&gt;) method with a string containing various escape sequences.
        /// Expected outcome: Correctly decodes known escape sequences, including newline, tab, and unicode/x escapes.
        /// </summary>
        [Fact]
        public void DecodeString_ReadOnlySpan_WithEscapeCharacters_ReturnsDecodedString()
        {
            // Arrange
            // The encoded string contains various escapes:
            // \\n should become a newline, \\t a tab, and \\u0041 a unicode 'A'
            string input = "Line1\\nLine2\\tTabbed\\u0041";
            ReadOnlySpan<char> span = input.AsSpan();
            string expected = "Line1\nLine2\tTabbedA";

            // Act
            ReadOnlySpan<char> result = Character.DecodeString(span);

            // Assert
            Assert.Equal(expected, result.ToString());
        }

        /// <summary>
        /// Tests the DecodeString(string) method when the input has no escape sequences.
        /// Expected outcome: Returns a TextSpan representing the original string.
        /// </summary>
        [Fact]
        public void DecodeString_String_NoEscapeCharacters_ReturnsSameTextSpan()
        {
            // Arrange
            string input = "Simple string with no escapes";

            // Act
            TextSpan result = Character.DecodeString(input);

            // Assert
            Assert.Equal(input, result.ToString());
        }

        /// <summary>
        /// Tests the DecodeString(TextSpan) method with a TextSpan containing escape sequences.
        /// Expected outcome: Returns a TextSpan with the string correctly decoded.
        /// </summary>
        [Fact]
        public void DecodeString_TextSpan_WithEscapeCharacters_ReturnsDecodedTextSpan()
        {
            // Arrange
            // The encoded string contains an escape for newline, tab and a hex escape.
            string original = "Test\\nNewLine\\tTab\\x43";
            TextSpan inputSpan = new TextSpan(original);
            string expected = "Test\nNewLine\tTabC";

            // Act
            TextSpan result = Character.DecodeString(inputSpan);

            // Assert
            Assert.Equal(expected, result.ToString());
        }

        /// <summary>
        /// Tests the IsWhiteSpace method with various characters.
        /// Expected outcome: Returns true for characters marked as white space and false otherwise.
        /// </summary>
        /// <param name="ch">The character being tested.</param>
        /// <param name="expected">The expected boolean result.</param>
        [Theory]
        [InlineData(' ', true)]
        [InlineData('\t', true)]
        // Depending on implementation, newline may not be flagged as white space by IsWhiteSpace.
        [InlineData('\n', false)]
        [InlineData('A', false)]
        public void IsWhiteSpace_VariousCharacters_ReturnsExpectedResult(char ch, bool expected)
        {
            // Act
            bool result = Character.IsWhiteSpace(ch);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the IsWhiteSpaceOrNewLine method with various characters.
        /// Expected outcome: Returns true for both white space and new line characters.
        /// </summary>
        /// <param name="ch">The character being tested.</param>
        /// <param name="expected">The expected boolean outcome.</param>
        [Theory]
        [InlineData(' ', true)]
        [InlineData('\t', true)]
        [InlineData('\n', true)]
        [InlineData('\r', true)]
        [InlineData('A', false)]
        public void IsWhiteSpaceOrNewLine_VariousCharacters_ReturnsExpectedResult(char ch, bool expected)
        {
            // Act
            bool result = Character.IsWhiteSpaceOrNewLine(ch);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the IsNewLine method with newline characters and non-newline characters.
        /// Expected outcome: Returns true only for newline characters ('\n', '\r', '\v').
        /// </summary>
        /// <param name="ch">The character being tested.</param>
        /// <param name="expected">The expected boolean outcome.</param>
        [Theory]
        [InlineData('\n', true)]
        [InlineData('\r', true)]
        [InlineData('\v', true)]
        [InlineData(' ', false)]
        [InlineData('A', false)]
        public void IsNewLine_VariousCharacters_ReturnsExpectedResult(char ch, bool expected)
        {
            // Act
            bool result = Character.IsNewLine(ch);

            // Assert
            Assert.Equal(expected, result);
        }
    }

    /// <summary>
    /// Minimal implementation of the TextSpan struct for testing purposes.
    /// This implementation assumes a simple wrapper around a string.
    /// </summary>
    public readonly struct TextSpan
    {
        public TextSpan(string buffer) : this(buffer, 0, buffer?.Length ?? 0)
        {
        }

        public TextSpan(string buffer, int offset, int length)
        {
            Buffer = buffer;
            Offset = offset;
            Length = length;
        }

        public string Buffer { get; }
        public int Offset { get; }
        public int Length { get; }

        public ReadOnlySpan<char> Span => string.IsNullOrEmpty(Buffer) ? ReadOnlySpan<char>.Empty : Buffer.AsSpan(Offset, Length);

        public override string ToString()
        {
            return Buffer?.Substring(Offset, Length) ?? string.Empty;
        }
    }
}
