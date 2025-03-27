using System;
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
        /// Tests that the constructor initializes the fields correctly when using an integer value.
        /// </summary>
        [Fact]
        public void Constructor_WithIntegerParameters_InitializesFieldsCorrectly()
        {
            // Arrange
            int expectedStart = 0;
            int expectedEnd = 10;
            int expectedValue = 42;

            // Act
            var parseResult = new ParseResult<int>(expectedStart, expectedEnd, expectedValue);

            // Assert
            Assert.Equal(expectedStart, parseResult.Start);
            Assert.Equal(expectedEnd, parseResult.End);
            Assert.Equal(expectedValue, parseResult.Value);
        }

        /// <summary>
        /// Tests that the constructor initializes the fields correctly when using a string value.
        /// </summary>
        [Fact]
        public void Constructor_WithStringParameters_InitializesFieldsCorrectly()
        {
            // Arrange
            int expectedStart = -5;
            int expectedEnd = 5;
            string expectedValue = "test";

            // Act
            var parseResult = new ParseResult<string>(expectedStart, expectedEnd, expectedValue);

            // Assert
            Assert.Equal(expectedStart, parseResult.Start);
            Assert.Equal(expectedEnd, parseResult.End);
            Assert.Equal(expectedValue, parseResult.Value);
        }

        /// <summary>
        /// Tests that the Set method correctly updates the fields when provided with new integer values.
        /// </summary>
        [Fact]
        public void Set_WithIntegerParameters_UpdatesFieldsCorrectly()
        {
            // Arrange
            var parseResult = new ParseResult<int>(0, 0, 0);
            int newStart = 5;
            int newEnd = 15;
            int newValue = 100;

            // Act
            parseResult.Set(newStart, newEnd, newValue);

            // Assert
            Assert.Equal(newStart, parseResult.Start);
            Assert.Equal(newEnd, parseResult.End);
            Assert.Equal(newValue, parseResult.Value);
        }

        /// <summary>
        /// Tests that the Set method correctly updates the fields when provided with new string values.
        /// </summary>
        [Fact]
        public void Set_WithStringParameters_UpdatesFieldsCorrectly()
        {
            // Arrange
            var parseResult = new ParseResult<string>(0, 0, "initial");
            int newStart = -10;
            int newEnd = 0;
            string newValue = "updated";

            // Act
            parseResult.Set(newStart, newEnd, newValue);

            // Assert
            Assert.Equal(newStart, parseResult.Start);
            Assert.Equal(newEnd, parseResult.End);
            Assert.Equal(newValue, parseResult.Value);
        }
    }
}
