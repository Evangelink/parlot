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
        /// Tests the <see cref="Character.IsDecimalDigit(char)"/> method for valid digit characters.
        /// Expected to return true when the input character is between '0' and '9'.
        /// </summary>
        /// <param name="digit">A character digit between '0' and '9'.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('3')]
        [InlineData('9')]
        public void IsDecimalDigit_WithDigit_ReturnsTrue(char digit)
        {
            // Act
            bool result = Character.IsDecimalDigit(digit);

            // Assert
            Assert.True(result, $"Expected '{digit}' to be identified as a decimal digit.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsDecimalDigit(char)"/> method for non-digit characters.
        /// Expected to return false when the input character is not between '0' and '9'.
        /// </summary>
        /// <param name="nonDigit">A character that is not a digit.</param>
        [Theory]
        [InlineData('a')]
        [InlineData(' ')]
        [InlineData('@')]
        [InlineData('-')]
        public void IsDecimalDigit_WithNonDigit_ReturnsFalse(char nonDigit)
        {
            // Act
            bool result = Character.IsDecimalDigit(nonDigit);

            // Assert
            Assert.False(result, $"Expected '{nonDigit}' not to be identified as a decimal digit.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierStart(char)"/> method with characters
        /// that are expected to be valid as the start of an identifier.
        /// Here, we assume typical identifier-start characters such as letters.
        /// </summary>
        /// <param name="ch">A character expected to be a valid identifier start.</param>
        [Theory]
        [InlineData('a')]
        [InlineData('Z')]
        [InlineData('_')]
        public void IsIdentifierStart_WithValidStartCharacters_ReturnsTrue(char ch)
        {
            // Act
            bool result = Character.IsIdentifierStart(ch);

            // Assert
            Assert.True(result, $"Expected '{ch}' to be recognized as a valid identifier starting character.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierStart(char)"/> method with characters
        /// that are expected not to be valid as the start of an identifier.
        /// For instance, digits and whitespace characters.
        /// </summary>
        /// <param name="ch">A character not valid as an identifier start.</param>
        [Theory]
        [InlineData('1')]
        [InlineData(' ')]
        [InlineData('\t')]
        [InlineData('0')]
        public void IsIdentifierStart_WithInvalidStartCharacters_ReturnsFalse(char ch)
        {
            // Act
            bool result = Character.IsIdentifierStart(ch);

            // Assert
            Assert.False(result, $"Expected '{ch}' not to be recognized as a valid identifier starting character.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierPart(char)"/> method with characters
        /// that are expected to be valid as part of an identifier.
        /// Typically, letters, digits and underscore are used in identifier parts.
        /// </summary>
        /// <param name="ch">A character expected to be a valid part of an identifier.</param>
        [Theory]
        [InlineData('a')]
        [InlineData('Z')]
        [InlineData('_')]
        [InlineData('0')] // Assuming digits are allowed as part of an identifier.
        [InlineData('9')]
        public void IsIdentifierPart_WithValidPartCharacters_ReturnsTrue(char ch)
        {
            // Act
            bool result = Character.IsIdentifierPart(ch);

            // Assert
            Assert.True(result, $"Expected '{ch}' to be recognized as a valid identifier part character.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierPart(char)"/> method with characters
        /// that are expected not to be valid as part of an identifier.
        /// For example, whitespace and punctuation.
        /// </summary>
        /// <param name="ch">A character not valid as part of an identifier.</param>
        [Theory]
        [InlineData(' ')]
        [InlineData('\n')]
        [InlineData('\t')]
        [InlineData('+')]
        [InlineData('-')]
        public void IsIdentifierPart_WithInvalidPartCharacters_ReturnsFalse(char ch)
        {
            // Act
            bool result = Character.IsIdentifierPart(ch);

            // Assert
            Assert.False(result, $"Expected '{ch}' not to be recognized as a valid identifier part character.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsHexDigit(char)"/> method for valid hexadecimal digit characters.
        /// Expected to return true for characters '0'-'9', 'A'-'F' and 'a'-'f'.
        /// </summary>
        /// <param name="hexChar">A hexadecimal digit character.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('5')]
        [InlineData('9')]
        [InlineData('A')]
        [InlineData('C')]
        [InlineData('F')]
        [InlineData('a')]
        [InlineData('d')]
        [InlineData('f')]
        public void IsHexDigit_WithValidHexCharacters_ReturnsTrue(char hexChar)
        {
            // Act
            bool result = Character.IsHexDigit(hexChar);

            // Assert
            Assert.True(result, $"Expected '{hexChar}' to be recognized as a valid hex digit.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsHexDigit(char)"/> method for invalid hexadecimal digit characters.
        /// Expected to return false for characters outside the ranges '0'-'9', 'A'-'F' and 'a'-'f'.
        /// </summary>
        /// <param name="nonHexChar">A character not valid in hexadecimal representation.</param>
        [Theory]
        [InlineData('G')]
        [InlineData('g')]
        [InlineData(' ')]
        [InlineData('z')]
        [InlineData('!')]
        public void IsHexDigit_WithInvalidHexCharacters_ReturnsFalse(char nonHexChar)
        {
            // Act
            bool result = Character.IsHexDigit(nonHexChar);

            // Assert
            Assert.False(result, $"Expected '{nonHexChar}' not to be recognized as a valid hex digit.");
        }
    }
}
