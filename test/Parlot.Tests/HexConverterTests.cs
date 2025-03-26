using System;
using Xunit;

namespace HexConverter.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="HexConverter"/> class.
    /// </summary>
    public class HexConverterTests
    {
        /// <summary>
        /// Tests the FromChar method for valid hex characters.
        /// Arrange: Provide valid hex character codes (digits, uppercase and lowercase hex letters).
        /// Act: Call FromChar and obtain conversion result.
        /// Assert: The conversion matches expected hex numeric values.
        /// </summary>
        /// <param name="input">The ASCII code of the character.</param>
        /// <param name="expected">The expected hex value.</param>
        [Theory]
        [InlineData('0', 0)]
        [InlineData('1', 1)]
        [InlineData('9', 9)]
        [InlineData('A', 10)]
        [InlineData('F', 15)]
        [InlineData('a', 10)]
        [InlineData('f', 15)]
        public void FromChar_ValidHexCharacters_ReturnsCorrectValue(int input, int expected)
        {
            // Act
            int result = HexConverter.FromChar(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the FromChar method for invalid characters.
        /// Arrange: Provide characters and integer values that are not valid hex digits.
        /// Act: Call FromChar and capture the returned value.
        /// Assert: The method returns 0xFF indicating an invalid hex digit.
        /// </summary>
        /// <param name="input">The ASCII code of the invalid character.</param>
        [Theory]
        [InlineData('G')]
        [InlineData('z')]
        [InlineData(256)]
        [InlineData(-1)]
        public void FromChar_InvalidCharacters_ReturnsFF(int input)
        {
            // Act
            int result = HexConverter.FromChar(input);

            // Assert
            Assert.Equal(0xFF, result);
        }

        /// <summary>
        /// Tests the IsHexChar method for valid hex characters.
        /// Arrange: Provide valid hex character codes.
        /// Act: Call IsHexChar and get the boolean indicator.
        /// Assert: The method returns true for valid hex characters.
        /// </summary>
        /// <param name="input">The ASCII code of the character.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('1')]
        [InlineData('9')]
        [InlineData('A')]
        [InlineData('F')]
        [InlineData('a')]
        [InlineData('f')]
        public void IsHexChar_ValidHexCharacters_ReturnsTrue(int input)
        {
            // Act
            bool result = HexConverter.IsHexChar(input);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Tests the IsHexChar method for invalid hex characters.
        /// Arrange: Provide invalid hex character codes.
        /// Act: Call IsHexChar to verify the outcome.
        /// Assert: The method returns false for characters that are not valid hex digits.
        /// </summary>
        /// <param name="input">The ASCII code of the character.</param>
        [Theory]
        [InlineData('G')]
        [InlineData('z')]
        [InlineData(256)]
        [InlineData(-10)]
        public void IsHexChar_InvalidHexCharacters_ReturnsFalse(int input)
        {
            // Act
            bool result = HexConverter.IsHexChar(input);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Tests the CharToHexLookup property to verify it returns a lookup table of correct length.
        /// Arrange: None.
        /// Act: Retrieve the lookup table from CharToHexLookup.
        /// Assert: The table length equals 256.
        /// </summary>
        [Fact]
        public void CharToHexLookup_ValidLength_Returns256()
        {
            // Act
            ReadOnlySpan<byte> lookupTable = HexConverter.CharToHexLookup;

            // Assert
            Assert.Equal(256, lookupTable.Length);
        }
    }
}
