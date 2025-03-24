using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParseError"/> class.
    /// </summary>
    public class ParseErrorTests
    {
        /// <summary>
        /// Tests that a newly constructed ParseError has a null Message and a default Position.
        /// </summary>
        [Fact]
        public void Constructor_DefaultValues_ShouldBeExpected()
        {
            // Arrange & Act
            var parseError = new ParseError();

            // Assert
            Assert.Null(parseError.Message);
            // Using default comparison for Position; assuming TextPosition is a struct or properly implements equality.
            Assert.Equal(default, parseError.Position);
        }

        /// <summary>
        /// Tests that the Message property of ParseError can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void MessageProperty_SetAndGet_ReturnsSameValue()
        {
            // Arrange
            var expectedMessage = "An error occurred during parsing.";
            var parseError = new ParseError();

            // Act
            parseError.Message = expectedMessage;
            var actualMessage = parseError.Message;

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        /// <summary>
        /// Tests that the Position property of ParseError can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void PositionProperty_SetAndGet_ReturnsSameValue()
        {
            // Arrange
            // Assuming that TextPosition is a value type (or a reference type with proper equality) that can be instantiated.
            // Here we use the default value as a baseline; if TextPosition has a constructor, replace with an appropriate instance.
            var expectedPosition = new TextPosition();
            var parseError = new ParseError();

            // Act
            parseError.Position = expectedPosition;
            var actualPosition = parseError.Position;

            // Assert
            Assert.Equal(expectedPosition, actualPosition);
        }
    }
}
