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
        /// Verifies that the TextPosition constructor correctly initializes its fields.
        /// Arrange: Provide specific offset, line, and column values.
        /// Act: Create a new instance of TextPosition.
        /// Assert: The Offset, Line, and Column properties should match the provided values.
        /// </summary>
        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(10, 5, 3)]
        [InlineData(-5, 0, 0)]
        public void Constructor_ValidInputs_InitializesFieldsCorrectly(int offset, int line, int column)
        {
            // Act
            var textPosition = new TextPosition(offset, line, column);

            // Assert
            Assert.Equal(offset, textPosition.Offset);
            Assert.Equal(line, textPosition.Line);
            Assert.Equal(column, textPosition.Column);
        }

        /// <summary>
        /// Verifies that the static Start field returns a TextPosition with offset 0, line 1 and column 1.
        /// Arrange: None.
        /// Act: Retrieve TextPosition.Start.
        /// Assert: The Offset, Line, and Column properties should match the expected start values.
        /// </summary>
        [Fact]
        public void Start_StaticField_HasCorrectValues()
        {
            // Act
            TextPosition start = TextPosition.Start;

            // Assert
            Assert.Equal(0, start.Offset);
            Assert.Equal(1, start.Line);
            Assert.Equal(1, start.Column);
        }

        /// <summary>
        /// Verifies that the subtraction operator returns the correct difference between TextPositions' offsets.
        /// Arrange: Create two TextPosition instances with specific offsets.
        /// Act: Use the operator - to subtract the offsets.
        /// Assert: The result should equal the difference of the offsets.
        /// </summary>
        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(5, 10, -5)]
        [InlineData(0, 0, 0)]
        public void OperatorMinus_ValidTextPositions_ReturnsCorrectDifference(int leftOffset, int rightOffset, int expectedDifference)
        {
            // Arrange
            var leftPosition = new TextPosition(leftOffset, 1, 1);
            var rightPosition = new TextPosition(rightOffset, 1, 1);

            // Act
            int result = leftPosition - rightPosition;

            // Assert
            Assert.Equal(expectedDifference, result);
        }

        /// <summary>
        /// Verifies that the ToString method returns a formatted string representing the line and column.
        /// Arrange: Create a TextPosition with specific line and column values.
        /// Act: Call the ToString method.
        /// Assert: The returned string should be in the format "(Line:Column)".
        /// </summary>
        [Theory]
        [InlineData(1, 1, "(1:1)")]
        [InlineData(3, 5, "(3:5)")]
        [InlineData(10, 20, "(10:20)")]
        public void ToString_ReturnsCorrectFormattedString(int line, int column, string expectedString)
        {
            // Arrange
            var position = new TextPosition(0, line, column);

            // Act
            string result = position.ToString();

            // Assert
            Assert.Equal(expectedString, result);
        }
    }
}
