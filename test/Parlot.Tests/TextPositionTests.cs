using System;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="TextPosition"/> struct.
    /// </summary>
    public class TextPositionTests
    {
        /// <summary>
        /// Verifies that the constructor initializes the TextPosition fields correctly.
        /// </summary>
        [Fact]
        public void Constructor_ValidInputs_InitializesFieldsCorrectly()
        {
            // Arrange
            int expectedOffset = 10;
            int expectedLine = 5;
            int expectedColumn = 7;

            // Act
            var position = new TextPosition(expectedOffset, expectedLine, expectedColumn);

            // Assert
            Assert.Equal(expectedOffset, position.Offset);
            Assert.Equal(expectedLine, position.Line);
            Assert.Equal(expectedColumn, position.Column);
        }

        /// <summary>
        /// Verifies that the subtraction operator returns the correct difference between the offsets.
        /// </summary>
        /// <param name="leftOffset">The offset value of the left TextPosition.</param>
        /// <param name="rightOffset">The offset value of the right TextPosition.</param>
        /// <param name="expectedDifference">The expected difference of the offsets.</param>
        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(5, 10, -5)]
        [InlineData(0, 0, 0)]
        public void OperatorMinus_ValidInputs_ReturnsCorrectDifference(int leftOffset, int rightOffset, int expectedDifference)
        {
            // Arrange
            var leftPosition = new TextPosition(leftOffset, 1, 1);
            var rightPosition = new TextPosition(rightOffset, 1, 1);

            // Act
            int actualDifference = leftPosition - rightPosition;

            // Assert
            Assert.Equal(expectedDifference, actualDifference);
        }

        /// <summary>
        /// Verifies that ToString returns a formatted string in the form "(Line:Column)".
        /// </summary>
        /// <param name="offset">The offset value (not used in formatting).</param>
        /// <param name="line">The line number to be displayed.</param>
        /// <param name="column">The column number to be displayed.</param>
        /// <param name="expectedString">The expected formatted string output.</param>
        [Theory]
        [InlineData(0, 1, 1, "(1:1)")]
        [InlineData(100, 10, 20, "(10:20)")]
        [InlineData(50, 5, 10, "(5:10)")]
        public void ToString_ValidTextPosition_ReturnsFormattedString(int offset, int line, int column, string expectedString)
        {
            // Arrange
            var position = new TextPosition(offset, line, column);

            // Act
            string actualString = position.ToString();

            // Assert
            Assert.Equal(expectedString, actualString);
        }

        /// <summary>
        /// Verifies that the static Start field returns a TextPosition representing the beginning of the text buffer.
        /// </summary>
        [Fact]
        public void StartField_ReturnsExpectedStartPosition()
        {
            // Act
            var startPosition = TextPosition.Start;

            // Assert
            Assert.Equal(0, startPosition.Offset);
            Assert.Equal(1, startPosition.Line);
            Assert.Equal(1, startPosition.Column);
            Assert.Equal("(1:1)", startPosition.ToString());
        }
    }
}
