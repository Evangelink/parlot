using System;
using Moq;
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
        /// Tests the IsDecimalDigit method with characters in the range '0' to '9' should return true.
        /// </summary>
        /// <param name="digit">A character that represents a decimal digit.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('5')]
        [InlineData('9')]
        public void IsDecimalDigit_WithDigitCharacters_ReturnsTrue(char digit)
        {
            // Act
            bool result = Character.IsDecimalDigit(digit);
            // Assert
            Assert.True(result, $"Expected '{digit}' to be recognized as a decimal digit.");
        }

        /// <summary>
        /// Tests the IsDecimalDigit method with characters outside the range '0' to '9' should return false.
        /// </summary>
        /// <param name="nonDigit">A character that does not represent a decimal digit.</param>
        [Theory]
        [InlineData('a')]
        [InlineData('#')]
        [InlineData(' ')]
        public void IsDecimalDigit_WithNonDigitCharacters_ReturnsFalse(char nonDigit)
        {
            // Act
            bool result = Character.IsDecimalDigit(nonDigit);
            // Assert
            Assert.False(result, $"Expected '{nonDigit}' to not be recognized as a decimal digit.");
        }

        /// <summary>
        /// Tests the IsIdentifierStart method with characters that are expected to be valid as identifier start characters.
        /// </summary>
        /// <param name="ch">A character that is presumed valid for starting an identifier.</param>
        [Theory]
        [InlineData('a')]
        [InlineData('Z')]
        [InlineData('_')]
        public void IsIdentifierStart_WithValidCharacters_ReturnsTrue(char ch)
        {
            // Act
            bool result = Character.IsIdentifierStart(ch);
            // Assert
            Assert.True(result, $"Expected '{ch}' to be recognized as a valid identifier start.");
        }

        /// <summary>
        /// Tests the IsIdentifierStart method with characters that are expected to be invalid as identifier start characters.
        /// </summary>
        /// <param name="ch">A character that is presumed invalid for starting an identifier.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('@')]
        [InlineData(' ')]
        public void IsIdentifierStart_WithInvalidCharacters_ReturnsFalse(char ch)
        {
            // Act
            bool result = Character.IsIdentifierStart(ch);
            // Assert
            Assert.False(result, $"Expected '{ch}' to not be recognized as a valid identifier start.");
        }

        /// <summary>
        /// Tests the IsIdentifierPart method with characters that are expected to be valid as part of an identifier.
        /// Typically, letters, digits and underscore are allowed.
        /// </summary>
        /// <param name="ch">A character that is presumed valid for an identifier part.</param>
        [Theory]
        [InlineData('a')]
        [InlineData('Z')]
        [InlineData('_')]
        [InlineData('0')] // Commonly, digits are allowed as identifier parts.
        public void IsIdentifierPart_WithValidCharacters_ReturnsTrue(char ch)
        {
            // Act
            bool result = Character.IsIdentifierPart(ch);
            // Assert
            Assert.True(result, $"Expected '{ch}' to be recognized as a valid identifier part.");
        }

        /// <summary>
        /// Tests the IsIdentifierPart method with characters that are expected to be invalid as part of an identifier.
        /// </summary>
        /// <param name="ch">A character that is presumed invalid for an identifier part.</param>
        [Theory]
        [InlineData('@')]
        [InlineData('#')]
        [InlineData(' ')]
        public void IsIdentifierPart_WithInvalidCharacters_ReturnsFalse(char ch)
        {
            // Act
            bool result = Character.IsIdentifierPart(ch);
            // Assert
            Assert.False(result, $"Expected '{ch}' to not be recognized as a valid identifier part.");
        }

        /// <summary>
        /// Tests the IsHexDigit method with valid hexadecimal characters.
        /// </summary>
        /// <param name="hexChar">A character that is a valid hexadecimal digit.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('9')]
        [InlineData('a')]
        [InlineData('f')]
        [InlineData('A')]
        [InlineData('F')]
        public void IsHexDigit_WithValidHexCharacters_ReturnsTrue(char hexChar)
        {
            // Act
            bool result = Character.IsHexDigit(hexChar);
            // Assert
            Assert.True(result, $"Expected '{hexChar}' to be recognized as a hex digit.");
        }

        /// <summary>
        /// Tests the IsHexDigit method with invalid hexadecimal characters.
        /// </summary>
        /// <param name="nonHexChar">A character that is not a valid hexadecimal digit.</param>
        [Theory]
        [InlineData('g')]
        [InlineData('G')]
        [InlineData('!')]
        [InlineData(' ')]
        public void IsHexDigit_WithInvalidHexCharacters_ReturnsFalse(char nonHexChar)
        {
            // Act
            bool result = Character.IsHexDigit(nonHexChar);
            // Assert
            Assert.False(result, $"Expected '{nonHexChar}' to not be recognized as a hex digit.");
        }

        /// <summary>
        /// Tests the identifier methods with boundary characters to ensure no exceptions are thrown.
        /// This checks for both char.MinValue and char.MaxValue.
        /// </summary>
        [Fact]
        public void IdentifierMethods_WithBoundaryCharacters_DoNotThrowException()
        {
            // Act
            Exception exMinStart = Record.Exception(() => Character.IsIdentifierStart(char.MinValue));
            Exception exMinPart = Record.Exception(() => Character.IsIdentifierPart(char.MinValue));
            Exception exMaxStart = Record.Exception(() => Character.IsIdentifierStart(char.MaxValue));
            Exception exMaxPart = Record.Exception(() => Character.IsIdentifierPart(char.MaxValue));

            // Assert
            Assert.Null(exMinStart);
            Assert.Null(exMinPart);
            Assert.Null(exMaxStart);
            Assert.Null(exMaxPart);
        }
    }
}
