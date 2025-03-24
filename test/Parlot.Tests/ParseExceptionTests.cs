using Moq;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParseException"/> class.
    /// </summary>
    public class ParseExceptionTests
    {
        /// <summary>
        /// Tests that the constructor correctly initializes the Exception.Message and Position properties with valid input.
        /// </summary>
        [Fact]
        public void Constructor_WithValidParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            string expectedMessage = "Test parse error";
            var expectedPosition = new TextPosition(1, 2);
            
            // Act
            var exception = new ParseException(expectedMessage, expectedPosition);
            
            // Assert
            Assert.Equal(expectedMessage, exception.Message);
            Assert.Equal(expectedPosition, exception.Position);
        }

        /// <summary>
        /// Tests that the Position property getter and setter work as expected.
        /// It verifies that the Position can be retrieved and updated correctly.
        /// </summary>
        [Fact]
        public void PositionProperty_GetAndSet_WorksAsExpected()
        {
            // Arrange
            string message = "Another error occurred";
            var initialPosition = new TextPosition(3, 4);
            var newPosition = new TextPosition(5, 6);
            var exception = new ParseException(message, initialPosition);
            
            // Act & Assert - initial value check
            Assert.Equal(initialPosition, exception.Position);
            
            // Act - update the Position property
            exception.Position = newPosition;
            
            // Assert - new value check
            Assert.Equal(newPosition, exception.Position);
        }

        /// <summary>
        /// Tests the constructor when a null message is provided.
        /// It verifies that the Exception.Message property returns a system-supplied message and that the Position is set correctly.
        /// </summary>
        [Fact]
        public void Constructor_WithNullMessage_SetsDefaultMessageAndPosition()
        {
            // Arrange
            string nullMessage = null;
            var expectedPosition = new TextPosition(0, 0);
            
            // Act
            var exception = new ParseException(nullMessage, expectedPosition);
            
            // Assert
            // Even if null is passed, Exception.Message should not be null because of the default behavior.
            Assert.NotNull(exception.Message);
            Assert.Equal(expectedPosition, exception.Position);
        }
    }
}
