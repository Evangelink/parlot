using Xunit;

namespace HexConverter.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="HexConverter"/> internal static class.
    /// </summary>
    public class HexConverterTests
    {
        /// <summary>
        /// Tests that IsHexChar returns true for valid hexadecimal characters.
        /// </summary>
        /// <param name="input">The integer representation of a hexadecimal character.</param>
        [Theory]
        [InlineData((int)'0')]
        [InlineData((int)'1')]
        [InlineData((int)'9')]
        [InlineData((int)'A')]
        [InlineData((int)'F')]
        [InlineData((int)'a')]
        [InlineData((int)'f')]
        public void IsHexChar_ValidHexCharacter_ReturnsTrue(int input)
        {
            // Act
            bool result = HexConverter.IsHexChar(input);

            // Assert
            Assert.True(result, $"Expected IsHexChar to return true for valid hex character '{(char)input}' (int value {input}).");
        }

        /// <summary>
        /// Tests that IsHexChar returns false for non-hexadecimal characters.
        /// </summary>
        /// <param name="input">The integer representation of a non-hexadecimal character.</param>
        [Theory]
        [InlineData((int)'G')]
        [InlineData((int)'g')]
        [InlineData((int)' ')]
        [InlineData((int)'/')]
        [InlineData(256)]
        public void IsHexChar_NonHexCharacter_ReturnsFalse(int input)
        {
            // Act
            bool result = HexConverter.IsHexChar(input);

            // Assert
            Assert.False(result, $"Expected IsHexChar to return false for non-hex character with int value {input}.");
        }

        /// <summary>
        /// Tests that IsHexChar behaves as expected when provided with a negative input.
        /// On 64-bit systems, a negative input should return false, while on non-64-bit systems, it may throw an exception.
        /// </summary>
        [Fact]
        public void IsHexChar_NegativeInput_BehaviorDependsOnArchitecture()
        {
            int negativeInput = -1;

            if (IntPtr.Size == 8)
            {
                // Act
                bool result = HexConverter.IsHexChar(negativeInput);

                // Assert
                Assert.False(result, "Expected IsHexChar to return false for negative input on 64-bit architecture.");
            }
            else
            {
                // Act & Assert
                Assert.Throws<IndexOutOfRangeException>(() => HexConverter.IsHexChar(negativeInput));
            }
        }

        /// <summary>
        /// Tests that FromChar returns the correct hex value for valid hexadecimal characters.
        /// </summary>
        /// <param name="input">The integer representation of a hexadecimal character.</param>
        /// <param name="expected">The expected hexadecimal value.</param>
        [Theory]
        [InlineData((int)'0', 0)]
        [InlineData((int)'1', 1)]
        [InlineData((int)'9', 9)]
        [InlineData((int)'A', 10)]
        [InlineData((int)'F', 15)]
        [InlineData((int)'a', 10)]
        [InlineData((int)'f', 15)]
        public void FromChar_ValidHexCharacter_ReturnsCorrectValue(int input, int expected)
        {
            // Act
            int actual = HexConverter.FromChar(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Tests that FromChar returns 0xFF for characters that are not valid hexadecimal characters.
        /// </summary>
        /// <param name="input">The integer representation of a non-hexadecimal character.</param>
        [Theory]
        [InlineData((int)'G')]
        [InlineData((int)'g')]
        [InlineData((int)' ')]
        [InlineData((int)'/')]
        [InlineData(256)]
        public void FromChar_NonHexCharacter_Returns0xFF(int input)
        {
            // Act
            int actual = HexConverter.FromChar(input);

            // Assert
            Assert.Equal(0xFF, actual);
        }

        /// <summary>
        /// Tests that FromChar throws an IndexOutOfRangeException when a negative input is provided,
        /// since negative indices are invalid for the lookup table.
        /// </summary>
        [Fact]
        public void FromChar_NegativeInput_ThrowsIndexOutOfRangeException()
        {
            int negativeInput = -1;

            // Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => HexConverter.FromChar(negativeInput));
        }
    }
}
