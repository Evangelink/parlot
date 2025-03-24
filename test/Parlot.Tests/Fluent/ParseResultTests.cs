using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParseResult{T}"/> struct.
    /// </summary>
    public class ParseResultTests
    {
        /// <summary>
        /// Tests the constructor of <see cref="ParseResult{T}"/> with an integer type to ensure it correctly assigns the provided values.
        /// </summary>
        [Theory]
        [InlineData(0, 10, 42)]
        [InlineData(-5, 5, 0)]
        [InlineData(100, 200, 999)]
        public void Constructor_WithIntValues_SetsPropertiesCorrectly(int start, int end, int value)
        {
            // Arrange & Act
            var result = new ParseResult<int>(start, end, value);

            // Assert
            Assert.Equal(start, result.Start);
            Assert.Equal(end, result.End);
            Assert.Equal(value, result.Value);
        }

        /// <summary>
        /// Tests the constructor of <see cref="ParseResult{T}"/> with a string type to ensure it correctly assigns the provided values.
        /// </summary>
        [Theory]
        [InlineData(0, 5, "test")]
        [InlineData(10, 20, "")]
        [InlineData(-3, 3, "boundary")]
        public void Constructor_WithStringValue_SetsPropertiesCorrectly(int start, int end, string value)
        {
            // Arrange & Act
            var result = new ParseResult<string>(start, end, value);

            // Assert
            Assert.Equal(start, result.Start);
            Assert.Equal(end, result.End);
            Assert.Equal(value, result.Value);
        }

        /// <summary>
        /// Tests the Set method of <see cref="ParseResult{T}"/> with an integer type to ensure it correctly updates the values.
        /// </summary>
        [Theory]
        [InlineData(0, 10, 42, 5, 15, 100)]
        [InlineData(-5, 5, 0, -10, 0, -1)]
        public void SetMethod_WithIntValues_UpdatesPropertiesCorrectly(int initialStart, int initialEnd, int initialValue,
                                                                         int newStart, int newEnd, int newValue)
        {
            // Arrange
            var result = new ParseResult<int>(initialStart, initialEnd, initialValue);

            // Act
            result.Set(newStart, newEnd, newValue);

            // Assert
            Assert.Equal(newStart, result.Start);
            Assert.Equal(newEnd, result.End);
            Assert.Equal(newValue, result.Value);
        }

        /// <summary>
        /// Tests the Set method of <see cref="ParseResult{T}"/> with a string type to ensure it correctly updates the values.
        /// </summary>
        [Theory]
        [InlineData(0, 10, "initial", 3, 13, "updated")]
        [InlineData(-5, 5, "start", 0, 10, "finish")]
        public void SetMethod_WithStringValue_UpdatesPropertiesCorrectly(int initialStart, int initialEnd, string initialValue,
                                                                           int newStart, int newEnd, string newValue)
        {
            // Arrange
            var result = new ParseResult<string>(initialStart, initialEnd, initialValue);

            // Act
            result.Set(newStart, newEnd, newValue);

            // Assert
            Assert.Equal(newStart, result.Start);
            Assert.Equal(newEnd, result.End);
            Assert.Equal(newValue, result.Value);
        }
    }
}
