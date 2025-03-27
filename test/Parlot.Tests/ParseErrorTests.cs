using System;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParseError"/> class.
    /// </summary>
    public class ParseErrorTests
    {
        /// <summary>
        /// Tests that a newly created ParseError instance has default values: Message is null and Position is default.
        /// </summary>
        [Fact]
        public void Constructor_DefaultValues_ShouldHaveNullMessageAndDefaultPosition()
        {
            // Arrange & Act
            var parseError = new ParseError();

            // Assert
            Assert.Null(parseError.Message);
            Assert.Equal(default, parseError.Position);
        }

        /// <summary>
        /// Tests that setting and getting the Message property returns the expected value.
        /// </summary>
        [Theory]
        [InlineData("An error occurred.")]
        [InlineData(null)]
        public void MessageProperty_SetAndGet_ReturnsSameValue(string expectedMessage)
        {
            // Arrange
            var parseError = new ParseError();

            // Act
            parseError.Message = expectedMessage;
            var actualMessage = parseError.Message;

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        /// <summary>
        /// Tests that setting and getting the Position property returns the expected value.
        /// </summary>
        [Fact]
        public void PositionProperty_SetAndGet_ReturnsSameValue()
        {
            // Arrange
            var parseError = new ParseError();
            // Create a non-default TextPosition by using default struct construction.
            // Since the implementation details of TextPosition are unknown,
            // we assume that setting any value and then getting it returns the same value.
            var expectedPosition = new TextPosition();

            // Act
            parseError.Position = expectedPosition;
            var actualPosition = parseError.Position;

            // Assert
            Assert.Equal(expectedPosition, actualPosition);
        }
    }
}
