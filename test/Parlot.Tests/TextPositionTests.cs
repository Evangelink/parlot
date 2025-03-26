using Parlot;
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
        /// Tests the constructor of <see cref="TextPosition"/> to ensure it assigns the fields correctly.
        /// </summary>
        /// <param name="offset">The offset value.</param>
        /// <param name="line">The line number.</param>
        /// <param name="column">The column number.</param>
        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(0, 1, 1)]
        [InlineData(-1, 0, 0)]
        public void Constructor_WhenCalled_AssignsFieldsCorrectly(int offset, int line, int column)
        {
            // Arrange & Act
            var position = new TextPosition(offset, line, column);

            // Assert
            Assert.Equal(offset, position.Offset);
            Assert.Equal(line, position.Line);
            Assert.Equal(column, position.Column);
        }

        /// <summary>
        /// Tests that the subtraction operator returns the difference between the offsets of two TextPositions.
        /// </summary>
        /// <param name="leftOffset">The left TextPosition offset.</param>
        /// <param name="rightOffset">The right TextPosition offset.</param>
        /// <param name="expectedDifference">The expected difference in offsets.</param>
        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(5, 10, -5)]
        [InlineData(0, 0, 0)]
        public void SubtractionOperator_WhenCalled_ReturnsDifferenceOfOffsets(int leftOffset, int rightOffset, int expectedDifference)
        {
            // Arrange
            var left = new TextPosition(leftOffset, 1, 1);
            var right = new TextPosition(rightOffset, 1, 1);

            // Act
            int result = left - right;

            // Assert
            Assert.Equal(expectedDifference, result);
        }

        /// <summary>
        /// Tests that the ToString method returns a formatted string in the "(Line:Column)" format.
        /// </summary>
        /// <param name="offset">The offset value (not used in the string formatting).</param>
        /// <param name="line">The line number.</param>
        /// <param name="column">The column number.</param>
        /// <param name="expectedString">The expected string representation.</param>
        [Theory]
        [InlineData(0, 1, 1, "(1:1)")]
        [InlineData(15, 4, 5, "(4:5)")]
        [InlineData(-3, 0, 0, "(0:0)")]
        public void ToString_WhenCalled_ReturnsFormattedString(int offset, int line, int column, string expectedString)
        {
            // Arrange
            var position = new TextPosition(offset, line, column);

            // Act
            string result = position.ToString();

            // Assert
            Assert.Equal(expectedString, result);
        }

        /// <summary>
        /// Tests that the static <see cref="TextPosition.Start"/> field holds the correct initial position.
        /// </summary>
        [Fact]
        public void StartField_WhenAccessed_ReturnsInitialPosition()
        {
            // Arrange & Act
            var start = TextPosition.Start;

            // Assert
            Assert.Equal(0, start.Offset);
            Assert.Equal(1, start.Line);
            Assert.Equal(1, start.Column);
        }
    }
}
