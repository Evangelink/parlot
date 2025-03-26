using Parlot;
using System;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParseResult{T}"/> struct.
    /// </summary>
    public class ParseResultTests
    {
        /// <summary>
        /// Tests that the constructor properly sets the fields for ParseResult with int as the type parameter.
        /// </summary>
        /// <param name="start">The start index passed to the constructor.</param>
        /// <param name="end">The end index passed to the constructor.</param>
        /// <param name="value">The integer value passed to the constructor.</param>
        [Theory]
        [InlineData(0, 10, 5)]
        [InlineData(-5, -1, 100)]
        public void Constructor_WithInt_ValidInput_FieldsSet(int start, int end, int value)
        {
            // Act
            var result = new ParseResult<int>(start, end, value);

            // Assert
            Assert.Equal(start, result.Start);
            Assert.Equal(end, result.End);
            Assert.Equal(value, result.Value);
        }

        /// <summary>
        /// Tests that the constructor properly sets the fields for ParseResult with string as the type parameter.
        /// </summary>
        /// <param name="start">The start index passed to the constructor.</param>
        /// <param name="end">The end index passed to the constructor.</param>
        /// <param name="value">The string value passed to the constructor.</param>
        [Theory]
        [InlineData(0, 10, "Hello")]
        [InlineData(5, 15, "")]
        [InlineData(100, 200, null)]
        public void Constructor_WithString_ValidInput_FieldsSet(int start, int end, string value)
        {
            // Act
            var result = new ParseResult<string>(start, end, value);

            // Assert
            Assert.Equal(start, result.Start);
            Assert.Equal(end, result.End);
            Assert.Equal(value, result.Value);
        }

        /// <summary>
        /// Tests that the Set method updates the fields correctly for ParseResult with int as the type parameter.
        /// </summary>
        /// <param name="initStart">The initial start index.</param>
        /// <param name="initEnd">The initial end index.</param>
        /// <param name="initValue">The initial integer value.</param>
        /// <param name="newStart">The new start index to set.</param>
        /// <param name="newEnd">The new end index to set.</param>
        /// <param name="newValue">The new integer value to set.</param>
        [Theory]
        [InlineData(10, 20, 30, 40, 50, 60)]
        [InlineData(-1, -2, 0, -3, -4, 5)]
        public void Set_WithInt_ValidInput_FieldsUpdated(int initStart, int initEnd, int initValue, int newStart, int newEnd, int newValue)
        {
            // Arrange
            var result = new ParseResult<int>(initStart, initEnd, initValue);

            // Act
            result.Set(newStart, newEnd, newValue);

            // Assert
            Assert.Equal(newStart, result.Start);
            Assert.Equal(newEnd, result.End);
            Assert.Equal(newValue, result.Value);
        }

        /// <summary>
        /// Tests that the Set method updates the fields correctly for ParseResult with string as the type parameter.
        /// </summary>
        /// <param name="initStart">The initial start index.</param>
        /// <param name="initEnd">The initial end index.</param>
        /// <param name="initValue">The initial string value.</param>
        /// <param name="newStart">The new start index to set.</param>
        /// <param name="newEnd">The new end index to set.</param>
        /// <param name="newValue">The new string value to set.</param>
        [Theory]
        [InlineData(0, 0, "Initial", 1, 1, "Updated")]
        [InlineData(5, 10, null, 15, 20, "NonNull")]
        [InlineData(10, 20, "NonNull", 30, 40, null)]
        public void Set_WithString_ValidInput_FieldsUpdated(int initStart, int initEnd, string initValue, int newStart, int newEnd, string newValue)
        {
            // Arrange
            var result = new ParseResult<string>(initStart, initEnd, initValue);

            // Act
            result.Set(newStart, newEnd, newValue);

            // Assert
            Assert.Equal(newStart, result.Start);
            Assert.Equal(newEnd, result.End);
            Assert.Equal(newValue, result.Value);
        }
    }
}
