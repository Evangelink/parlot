using System;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Character"/> class.
    /// </summary>
    public class CharacterTests
    {
        /// <summary>
        /// Tests that IsDecimalDigit returns true for valid decimal digit characters.
        /// </summary>
        /// <param name="ch">The character to test.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('1')]
        [InlineData('5')]
        [InlineData('9')]
        public void IsDecimalDigit_ValidDigit_ReturnsTrue(char ch)
        {
            // Act
            bool result = Character.IsDecimalDigit(ch);

            // Assert
            Assert.True(result, $"Expected '{ch}' to be recognized as a decimal digit.");
        }

        /// <summary>
        /// Tests that IsDecimalDigit returns false for characters that are not decimal digits.
        /// </summary>
        /// <param name="ch">The character to test.</param>
        [Theory]
        [InlineData('a')]
        [InlineData(' ')]
        [InlineData('%')]
        [InlineData('A')]
        public void IsDecimalDigit_InvalidDigit_ReturnsFalse(char ch)
        {
            // Act
            bool result = Character.IsDecimalDigit(ch);

            // Assert
            Assert.False(result, $"Expected '{ch}' to not be recognized as a decimal digit.");
        }

        /// <summary>
        /// Tests that IsHexDigit returns true for valid hexadecimal digit characters.
        /// </summary>
        /// <param name="ch">The character to test.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('9')]
        [InlineData('A')]
        [InlineData('F')]
        [InlineData('a')]
        [InlineData('f')]
        public void IsHexDigit_ValidHex_ReturnsTrue(char ch)
        {
            // Act
            bool result = Character.IsHexDigit(ch);

            // Assert
            Assert.True(result, $"Expected '{ch}' to be recognized as a hexadecimal digit.");
        }

        /// <summary>
        /// Tests that IsHexDigit returns false for characters that are not valid hexadecimal digits.
        /// </summary>
        /// <param name="ch">The character to test.</param>
        [Theory]
        [InlineData('G')]
        [InlineData('g')]
        [InlineData('z')]
        [InlineData(' ')]
        public void IsHexDigit_InvalidHex_ReturnsFalse(char ch)
        {
            // Act
            bool result = Character.IsHexDigit(ch);

            // Assert
            Assert.False(result, $"Expected '{ch}' to not be recognized as a hexadecimal digit.");
        }

        /// <summary>
        /// Tests that IsIdentifierStart returns true for valid identifier start characters.
        /// Assumes letters and underscore are valid starting characters.
        /// </summary>
        /// <param name="ch">The character to test.</param>
        [Theory]
        [InlineData('A')]
        [InlineData('Z')]
        [InlineData('a')]
        [InlineData('z')]
        [InlineData('_')]
        public void IsIdentifierStart_ValidStart_ReturnsTrue(char ch)
        {
            // Act
            bool result = Character.IsIdentifierStart(ch);

            // Assert
            Assert.True(result, $"Expected '{ch}' to be a valid identifier start character.");
        }

        /// <summary>
        /// Tests that IsIdentifierStart returns false for characters that are not valid identifier start characters.
        /// Assumes digits and special symbols are invalid.
        /// </summary>
        /// <param name="ch">The character to test.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('5')]
        [InlineData('-')]
        [InlineData('+')]
        [InlineData(' ')]
        public void IsIdentifierStart_InvalidStart_ReturnsFalse(char ch)
        {
            // Act
            bool result = Character.IsIdentifierStart(ch);

            // Assert
            Assert.False(result, $"Expected '{ch}' to not be a valid identifier start character.");
        }

        /// <summary>
        /// Tests that IsIdentifierPart returns true for valid identifier part characters.
        /// Assumes letters, digits, and underscore are valid parts of an identifier.
        /// </summary>
        /// <param name="ch">The character to test.</param>
        [Theory]
        [InlineData('A')]
        [InlineData('z')]
        [InlineData('0')]
        [InlineData('9')]
        [InlineData('_')]
        public void IsIdentifierPart_ValidPart_ReturnsTrue(char ch)
        {
            // Act
            bool result = Character.IsIdentifierPart(ch);

            // Assert
            Assert.True(result, $"Expected '{ch}' to be a valid identifier part character.");
        }

        /// <summary>
        /// Tests that IsIdentifierPart returns false for characters that are not valid identifier parts.
        /// </summary>
        /// <param name="ch">The character to test.</param>
        [Theory]
        [InlineData('@')]
        [InlineData('-')]
        [InlineData(' ')]
        [InlineData('%')]
        public void IsIdentifierPart_InvalidPart_ReturnsFalse(char ch)
        {
            // Act
            bool result = Character.IsIdentifierPart(ch);

            // Assert
            Assert.False(result, $"Expected '{ch}' to not be a valid identifier part character.");
        }
    }
}
