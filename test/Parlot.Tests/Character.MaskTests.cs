using Moq;
using Parlot;
using System;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Character"/> class.
    /// </summary>
    public class CharacterTests
    {
        /// <summary>
        /// Tests the <see cref="Character.IsDecimalDigit(char)"/> method with various characters.
        /// This test verifies that IsDecimalDigit correctly identifies characters between '0' and '9'
        /// and rejects characters outside that range.
        /// </summary>
        /// <param name="input">The character to test.</param>
        /// <param name="expected">The expected result (true if a decimal digit, false otherwise).</param>
        [Theory]
        [InlineData('0', true)]
        [InlineData('5', true)]
        [InlineData('9', true)]
        [InlineData('a', false)]
        [InlineData('/', false)]
        [InlineData(':', false)]
        public void IsDecimalDigit_GivenVariousCharacters_ReturnsExpectedResult(char input, bool expected)
        {
            // Act
            bool result = Character.IsDecimalDigit(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the <see cref="Character.IsHexDigit(char)"/> method with various characters.
        /// This test verifies that IsHexDigit correctly identifies valid hexadecimal characters 
        /// (0-9, A-F, a-f) and rejects invalid characters.
        /// </summary>
        /// <param name="input">The character to test.</param>
        /// <param name="expected">The expected result (true if a hex digit, false otherwise).</param>
        [Theory]
        [InlineData('0', true)]
        [InlineData('9', true)]
        [InlineData('A', true)]
        [InlineData('F', true)]
        [InlineData('a', true)]
        [InlineData('f', true)]
        [InlineData('G', false)]
        [InlineData('g', false)]
        [InlineData('Z', false)]
        [InlineData(' ', false)]
        public void IsHexDigit_GivenVariousCharacters_ReturnsExpectedResult(char input, bool expected)
        {
            // Act
            bool result = Character.IsHexDigit(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierStart(char)"/> method with characters.
        /// This test verifies that IsIdentifierStart returns true for characters that are expected
        /// to be valid as the start of identifiers (e.g., letters and underscore) and false for others.
        /// </summary>
        /// <param name="input">The character to test.</param>
        /// <param name="expected">The expected result (true if valid identifier start, false otherwise).</param>
        [Theory]
        [InlineData('A', true)]
        [InlineData('a', true)]
        [InlineData('_', true)]
        [InlineData('1', false)]
        [InlineData(' ', false)]
        [InlineData('\0', false)]
        public void IsIdentifierStart_GivenVariousCharacters_ReturnsExpectedResult(char input, bool expected)
        {
            // Act
            bool result = Character.IsIdentifierStart(input);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the <see cref="Character.IsIdentifierPart(char)"/> method with characters.
        /// This test verifies that IsIdentifierPart returns true for characters that are valid as
        /// part of an identifier (e.g., letters, underscore, and digits) and false for invalid ones.
        /// </summary>
        /// <param name="input">The character to test.</param>
        /// <param name="expected">The expected result (true if valid identifier part, false otherwise).</param>
        [Theory]
        [InlineData('A', true)]
        [InlineData('a', true)]
        [InlineData('_', true)]
        [InlineData('1', true)]
        [InlineData(' ', false)]
        [InlineData('@', false)]
        [InlineData('\0', false)]
        public void IsIdentifierPart_GivenVariousCharacters_ReturnsExpectedResult(char input, bool expected)
        {
            // Act
            bool result = Character.IsIdentifierPart(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
