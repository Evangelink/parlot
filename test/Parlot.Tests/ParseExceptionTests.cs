using Parlot;
using System;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParseException"/> class.
    /// </summary>
    public class ParseExceptionTests
    {
        private readonly TextPosition _defaultPosition;
        private readonly string _defaultMessage;

        /// <summary>
        /// Initializes test data for <see cref="ParseExceptionTests"/>.
        /// </summary>
        public ParseExceptionTests()
        {
            // Assuming TextPosition has a constructor accepting two integer parameters (e.g., line and column).
            _defaultPosition = new TextPosition(1, 1);
            _defaultMessage = "Test error message";
        }

        /// <summary>
        /// Tests that the <see cref="ParseException"/> constructor properly initializes the exception message and position.
        /// </summary>
        [Fact]
        public void Constructor_WithValidParameters_SetsMessageAndPosition()
        {
            // Arrange
            // (Using default test data initialized in the constructor)

            // Act
            var exception = new ParseException(_defaultMessage, _defaultPosition);

            // Assert
            Assert.Equal(_defaultMessage, exception.Message);
            Assert.Equal(_defaultPosition, exception.Position);
        }

        /// <summary>
        /// Tests that the <see cref="ParseException.Position"/> property getter and setter work as expected.
        /// </summary>
        [Fact]
        public void PositionProperty_SetValue_UpdatesPositionCorrectly()
        {
            // Arrange
            var exception = new ParseException(_defaultMessage, _defaultPosition);
            var newPosition = new TextPosition(5, 10);

            // Act
            exception.Position = newPosition;

            // Assert
            Assert.Equal(newPosition, exception.Position);
        }
    }
}
