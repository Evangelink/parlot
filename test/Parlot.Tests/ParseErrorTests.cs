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
        /// Tests that a newly created <see cref="ParseError"/> instance has its properties set to the expected default values.
        /// Expected outcome: Message is null and Position has its default value.
        /// </summary>
        [Fact]
        public void Constructor_DefaultValues_ShouldBeExpected()
        {
            // Arrange & Act
            var parseError = new ParseError();
            
            // Assert
            Assert.Null(parseError.Message);
            Assert.Equal(default, parseError.Position);
        }

        /// <summary>
        /// Tests that the Message property correctly stores and retrieves a value.
        /// Expected outcome: The value set is the same as the value retrieved.
        /// </summary>
        [Fact]
        public void MessageProperty_GetSet_ReturnsSameMessage()
        {
            // Arrange
            var parseError = new ParseError();
            const string expectedMessage = "An error occurred during parsing.";

            // Act
            parseError.Message = expectedMessage;
            string? actualMessage = parseError.Message;

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        /// <summary>
        /// Tests that the Position property correctly stores and retrieves a value.
        /// Expected outcome: The value set is the same as the value retrieved.
        /// </summary>
        [Fact]
        public void PositionProperty_GetSet_ReturnsSamePosition()
        {
            // Arrange
            var parseError = new ParseError();
            // Assuming TextPosition has a parameterless constructor.
            var expectedPosition = new TextPosition();

            // Act
            parseError.Position = expectedPosition;
            TextPosition actualPosition = parseError.Position;

            // Assert
            Assert.Equal(expectedPosition, actualPosition);
        }
    }
}
