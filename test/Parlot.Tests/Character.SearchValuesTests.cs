using System;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Character"/> class.
    /// </summary>
//     public class CharacterTests [Error] (10-18)CS0101 The namespace 'Parlot.UnitTests' already contains a definition for 'CharacterTests'
//     {
//         /// <summary>
//         /// Tests the <see cref="Character.IsDecimalDigit(char)"/> method with various characters to verify it returns the expected result.
//         /// </summary>
//         /// <param name="input">The character to test.</param>
//         /// <param name="expected">The expected outcome indicating if the character is a decimal digit.</param>
//         [Theory]
//         [InlineData('0', true)]
//         [InlineData('5', true)]
//         [InlineData('9', true)]
//         [InlineData('a', false)]
//         [InlineData('-', false)]
//         [InlineData((char)0, false)]
//         [InlineData(char.MaxValue, false)]
//         public void IsDecimalDigit_InputVariousCharacters_ReturnsExpected(char input, bool expected)
//         {
//             // Act
//             bool result = Character.IsDecimalDigit(input);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
// 
//         /// <summary>
//         /// Tests the <see cref="Character.IsHexDigit(char)"/> method with various characters to verify it returns the expected result.
//         /// </summary>
//         /// <param name="input">The character to test.</param>
//         /// <param name="expected">The expected outcome indicating if the character is a hexadecimal digit.</param>
//         [Theory]
//         [InlineData('0', true)]
//         [InlineData('9', true)]
//         [InlineData('a', true)]
//         [InlineData('f', true)]
//         [InlineData('A', true)]
//         [InlineData('F', true)]
//         [InlineData('g', false)]
//         [InlineData('z', false)]
//         [InlineData('/', false)]
//         [InlineData((char)0, false)]
//         [InlineData(char.MaxValue, false)]
//         public void IsHexDigit_InputVariousCharacters_ReturnsExpected(char input, bool expected)
//         {
//             // Act
//             bool result = Character.IsHexDigit(input);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
// 
//         /// <summary>
//         /// Tests the <see cref="Character.IsIdentifierStart(char)"/> method with various characters to verify it returns the expected result.
//         /// </summary>
//         /// <param name="input">The character to test.</param>
//         /// <param name="expected">The expected outcome indicating if the character can start an identifier.</param>
//         [Theory]
//         [InlineData('a', true)]
//         [InlineData('A', true)]
//         [InlineData('_', true)]
//         [InlineData('z', true)]
//         [InlineData('Z', true)]
//         [InlineData('0', false)]
//         [InlineData('9', false)]
//         [InlineData('-', false)]
//         [InlineData((char)0, false)]
//         [InlineData(char.MaxValue, false)]
//         public void IsIdentifierStart_InputVariousCharacters_ReturnsExpected(char input, bool expected)
//         {
//             // Act
//             bool result = Character.IsIdentifierStart(input);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
// 
//         /// <summary>
//         /// Tests the <see cref="Character.IsIdentifierPart(char)"/> method with various characters to verify it returns the expected result.
//         /// </summary>
//         /// <param name="input">The character to test.</param>
//         /// <param name="expected">The expected outcome indicating if the character can be a part of an identifier.</param>
//         [Theory]
//         [InlineData('a', true)]
//         [InlineData('A', true)]
//         [InlineData('_', true)]
//         [InlineData('z', true)]
//         [InlineData('Z', true)]
//         [InlineData('0', true)]   // Typically digits are allowed in identifier parts.
//         [InlineData('9', true)]
//         [InlineData('-', false)]
//         [InlineData((char)0, false)]
//         [InlineData(char.MaxValue, false)]
//         public void IsIdentifierPart_InputVariousCharacters_ReturnsExpected(char input, bool expected)
//         {
//             // Act
//             bool result = Character.IsIdentifierPart(input);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
//     }
}
