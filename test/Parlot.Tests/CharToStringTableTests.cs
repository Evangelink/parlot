using System;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CharToStringTable"/> class.
    /// </summary>
    public class CharToStringTableTests
    {
        /// <summary>
        /// Tests the GetString method for characters within the cached range [0, 255].
        /// Expects the method to return a string representation of the character from the prepopulated cache.
        /// </summary>
        /// <param name="input">The character within the range [0, 255].</param>
        [Theory]
        [InlineData((char)0)]
        [InlineData('A')]
        [InlineData((char)255)]
        public void GetString_InputWithinCacheRange_ReturnsCachedString(char input)
        {
            // Arrange
            string expected = input.ToString();

            // Act
            string actual = CharToStringTable.GetString(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Tests the GetString method for characters outside the cached range (>= 256).
        /// Expects the method to return the string representation of the character computed via ToString().
        /// </summary>
        /// <param name="input">The character outside the cached range (>=256).</param>
        [Theory]
        [InlineData((char)256)]
        [InlineData((char)300)]
        [InlineData((char)65535)]
        public void GetString_InputOutsideCacheRange_ReturnsComputedString(char input)
        {
            // Arrange
            string expected = input.ToString();

            // Act
            string actual = CharToStringTable.GetString(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
