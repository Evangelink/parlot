using System;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CharToStringTable"/> class.
    /// </summary>
    public class CharToStringTableTests
    {
        /// <summary>
        /// Tests that GetString returns a cached string instance for characters within the table range.
        /// When the same character is queried multiple times, the same instance is returned.
        /// </summary>
        [Fact]
        public void GetString_CharacterWithinCache_ReturnsSameCachedInstance()
        {
            // Arrange
            char testChar = 'A';
            
            // Act
            string result1 = CharToStringTable.GetString(testChar);
            string result2 = CharToStringTable.GetString(testChar);

            // Assert
            Assert.Same(result1, result2);
        }

        /// <summary>
        /// Tests that GetString returns the correct string representation for characters within the cache.
        /// This ensures that the cached string matches the output of char.ToString().
        /// </summary>
        /// <param name="testChar">A character within the cache range.</param>
        [Theory]
        [InlineData('A')]
        [InlineData('z')]
        [InlineData((char)0)]
        [InlineData((char)127)]
        [InlineData((char)255)]
        public void GetString_CharacterWithinCache_ReturnsExpectedString(char testChar)
        {
            // Arrange
            string expected = testChar.ToString();

            // Act
            string actual = CharToStringTable.GetString(testChar);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Tests that GetString correctly returns a string representation for characters outside of the cache range.
        /// Characters with an integer value greater than or equal to the cache size (256) should not be cached.
        /// </summary>
        /// <param name="charValue">The integer value of the character outside the cache range.</param>
        [Theory]
        [InlineData(256)]
        [InlineData(300)]
        [InlineData(1000)]
        [InlineData(ushort.MaxValue)]
        public void GetString_CharacterOutsideCache_ReturnsExpectedString(int charValue)
        {
            // Arrange
            char testChar = (char)charValue;
            string expected = testChar.ToString();

            // Act
            string actual = CharToStringTable.GetString(testChar);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
