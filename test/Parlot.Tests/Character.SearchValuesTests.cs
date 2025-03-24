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
        /// Tests the <see cref="Character.IsDecimalDigit(char)"/> method for valid decimal digit inputs.
        /// Expected outcome: It returns true for characters '0' through '9'.
        /// </summary>
        /// <param name="input">A character representing a valid decimal digit.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('5')]
        [InlineData('9')]
        public void IsDecimalDigit_WithDigit_ReturnsTrue(char input)
        {
            // Act
            bool result = Character.IsDecimalDigit(input);

            // Assert
            Assert.True(result, $"Expected '{input}' to be recognized as a decimal digit.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsDecimalDigit(char)"/> method for non-decimal digit inputs.
        /// Expected outcome: It returns false for characters not between '0' and '9'.
        /// </summary>
        /// <param name="input">A character that is not a decimal digit.</param>
        [Theory]
        [InlineData('a')]
        [InlineData(' ')]
        [InlineData('%')]
        public void IsDecimalDigit_WithNonDigit_ReturnsFalse(char input)
        {
            // Act
            bool result = Character.IsDecimalDigit(input);

            // Assert
            Assert.False(result, $"Expected '{input}' to not be recognized as a decimal digit.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierStart(char)"/> method for valid identifier start characters.
        /// Expected outcome: It returns true for letters and underscore that typically start an identifier.
        /// </summary>
        /// <param name="input">A character valid as the start of an identifier.</param>
        [Theory]
        [InlineData('a')]
        [InlineData('Z')]
        [InlineData('_')]
        public void IsIdentifierStart_WithValidStartCharacter_ReturnsTrue(char input)
        {
            // Act
            bool result = Character.IsIdentifierStart(input);

            // Assert
            Assert.True(result, $"Expected '{input}' to be recognized as a valid identifier start character.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierStart(char)"/> method for invalid identifier start characters.
        /// Expected outcome: It returns false for characters generally not allowed to start an identifier.
        /// </summary>
        /// <param name="input">A character that is not valid as the start of an identifier.</param>
        [Theory]
        [InlineData('1')]
        [InlineData('-')]
        [InlineData('+')]
        public void IsIdentifierStart_WithInvalidStartCharacter_ReturnsFalse(char input)
        {
            // Act
            bool result = Character.IsIdentifierStart(input);

            // Assert
            Assert.False(result, $"Expected '{input}' to not be recognized as a valid identifier start character.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierPart(char)"/> method for valid identifier part characters.
        /// Expected outcome: It returns true for characters that can be part of an identifier, 
        /// including letters, digits, and underscore.
        /// </summary>
        /// <param name="input">A character valid as a part of an identifier.</param>
        [Theory]
        [InlineData('a')]
        [InlineData('Z')]
        [InlineData('_')]
        [InlineData('0')]
        [InlineData('9')]
        public void IsIdentifierPart_WithValidPartCharacter_ReturnsTrue(char input)
        {
            // Act
            bool result = Character.IsIdentifierPart(input);

            // Assert
            Assert.True(result, $"Expected '{input}' to be recognized as a valid identifier part character.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierPart(char)"/> method for invalid identifier part characters.
        /// Expected outcome: It returns false for characters that are not allowed in an identifier.
        /// </summary>
        /// <param name="input">A character not valid as part of an identifier.</param>
        [Theory]
        [InlineData('-')]
        [InlineData('+')]
        [InlineData(' ')]
        [InlineData('%')]
        public void IsIdentifierPart_WithInvalidPartCharacter_ReturnsFalse(char input)
        {
            // Act
            bool result = Character.IsIdentifierPart(input);

            // Assert
            Assert.False(result, $"Expected '{input}' to not be recognized as a valid identifier part character.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsHexDigit(char)"/> method for valid hexadecimal digits.
        /// Expected outcome: It returns true for characters representing hexadecimal digits (0-9, A-F, a-f).
        /// </summary>
        /// <param name="input">A character that is a valid hexadecimal digit.</param>
        [Theory]
        [InlineData('0')]
        [InlineData('9')]
        [InlineData('A')]
        [InlineData('F')]
        [InlineData('a')]
        [InlineData('f')]
        public void IsHexDigit_WithHexDigit_ReturnsTrue(char input)
        {
            // Act
            bool result = Character.IsHexDigit(input);

            // Assert
            Assert.True(result, $"Expected '{input}' to be recognized as a valid hexadecimal digit.");
        }

        /// <summary>
        /// Tests the <see cref="Character.IsHexDigit(char)"/> method for non-hexadecimal digit inputs.
        /// Expected outcome: It returns false for characters not representing hexadecimal digits.
        /// </summary>
        /// <param name="input">A character that is not a valid hexadecimal digit.</param>
        [Theory]
        [InlineData('G')]
        [InlineData('g')]
        [InlineData('z')]
        [InlineData(' ')]
        public void IsHexDigit_WithNonHexDigit_ReturnsFalse(char input)
        {
            // Act
            bool result = Character.IsHexDigit(input);

            // Assert
            Assert.False(result, $"Expected '{input}' to not be recognized as a valid hexadecimal digit.");
        }
    }
}
