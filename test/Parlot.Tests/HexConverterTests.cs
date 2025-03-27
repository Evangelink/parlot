using System;
using Xunit;

namespace HexConverter.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="HexConverter"/> class.
    /// </summary>
    public class HexConverterTests
    {
        /// <summary>
        /// Tests the IsHexChar method with valid hex characters and invalid non-hex characters.
        /// Expected outcome: returns true for valid hex digits ('0'-'9', 'A'-'F', 'a'-'f') and false for invalid ones.
        /// </summary>
        /// <param name="input">The character code to test.</param>
        /// <param name="expected">The expected boolean result.</param>
//         [Theory] [Error] (33-27)CS0234 The type or namespace name 'IsHexChar' does not exist in the namespace 'HexConverter' (are you missing an assembly reference?)
//         [InlineData('0', true)]
//         [InlineData('1', true)]
//         [InlineData('9', true)]
//         [InlineData('A', true)]
//         [InlineData('B', true)]
//         [InlineData('F', true)]
//         [InlineData('a', true)]
//         [InlineData('b', true)]
//         [InlineData('f', true)]
//         [InlineData('G', false)]
//         [InlineData('$', false)]
//         [InlineData(' ', false)]
//         public void IsHexChar_ValidAndInvalidCharacters_ReturnsExpectedOutcome(int input, bool expected)
//         {
//             // Act
//             bool result = HexConverter.IsHexChar(input);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }

        /// <summary>
        /// Tests the IsHexChar method with a negative value.
        /// For 64-bit systems, it is expected to handle the negative input and return false.
        /// For systems that are not 64-bit, the fallback implementation calls FromChar which will throw an IndexOutOfRangeException.
        /// </summary>
//         [Fact] [Error] (53-31)CS0234 The type or namespace name 'IsHexChar' does not exist in the namespace 'HexConverter' (are you missing an assembly reference?) [Error] (58-63)CS0234 The type or namespace name 'IsHexChar' does not exist in the namespace 'HexConverter' (are you missing an assembly reference?)
//         public void IsHexChar_NegativeValue_BehavesAsExpected()
//         {
//             // Arrange
//             int negativeValue = -1;
// 
//             // Act & Assert
//             if (IntPtr.Size == 8)
//             {
//                 bool result = HexConverter.IsHexChar(negativeValue);
//                 Assert.False(result);
//             }
//             else
//             {
//                 Assert.Throws<IndexOutOfRangeException>(() => HexConverter.IsHexChar(negativeValue));
//             }
//         }

        /// <summary>
        /// Tests the FromChar method with valid hex characters.
        /// Expected outcome: returns the corresponding hex value for valid hex digits.
        /// </summary>
        /// <param name="input">The character code to test.</param>
        /// <param name="expected">The expected hex value.</param>
//         [Theory] [Error] (81-26)CS0234 The type or namespace name 'FromChar' does not exist in the namespace 'HexConverter' (are you missing an assembly reference?)
//         [InlineData('0', 0)]
//         [InlineData('1', 1)]
//         [InlineData('9', 9)]
//         [InlineData('A', 10)]
//         [InlineData('B', 11)]
//         [InlineData('F', 15)]
//         [InlineData('a', 10)]
//         [InlineData('b', 11)]
//         [InlineData('f', 15)]
//         public void FromChar_ValidHexCharacters_ReturnsHexValue(int input, int expected)
//         {
//             // Act
//             int result = HexConverter.FromChar(input);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }

        /// <summary>
        /// Tests the FromChar method with invalid hex characters.
        /// Expected outcome: returns 0xFF indicating an invalid hex digit.
        /// </summary>
        /// <param name="input">The character code to test.</param>
//         [Theory] [Error] (99-26)CS0234 The type or namespace name 'FromChar' does not exist in the namespace 'HexConverter' (are you missing an assembly reference?)
//         [InlineData('G')]
//         [InlineData('$')]
//         [InlineData(' ')]
//         public void FromChar_InvalidHexCharacters_ReturnsFF(int input)
//         {
//             // Act
//             int result = HexConverter.FromChar(input);
// 
//             // Assert
//             Assert.Equal(0xFF, result);
//         }

        /// <summary>
        /// Tests the FromChar method with an out-of-range value (>=256).
        /// Expected outcome: returns 0xFF indicating an invalid hex digit.
        /// </summary>
//         [Fact] [Error] (116-26)CS0234 The type or namespace name 'FromChar' does not exist in the namespace 'HexConverter' (are you missing an assembly reference?)
//         public void FromChar_OutOfRangeValue_ReturnsFF()
//         {
//             // Arrange
//             int outOfRangeValue = 256;
// 
//             // Act
//             int result = HexConverter.FromChar(outOfRangeValue);
// 
//             // Assert
//             Assert.Equal(0xFF, result);
//         }

        /// <summary>
        /// Tests the FromChar method with a negative input.
        /// Expected outcome: throws an IndexOutOfRangeException.
        /// </summary>
//         [Fact] [Error] (133-59)CS0234 The type or namespace name 'FromChar' does not exist in the namespace 'HexConverter' (are you missing an assembly reference?)
//         public void FromChar_NegativeValue_ThrowsIndexOutOfRangeException()
//         {
//             // Arrange
//             int negativeValue = -1;
// 
//             // Act & Assert
//             Assert.Throws<IndexOutOfRangeException>(() => HexConverter.FromChar(negativeValue));
//         }

        /// <summary>
        /// Tests that the CharToHexLookup property returns a lookup span of length 256.
        /// Expected outcome: The span length should be exactly 256.
        /// </summary>
//         [Fact] [Error] (144-24)CS0234 The type or namespace name 'CharToHexLookup' does not exist in the namespace 'HexConverter' (are you missing an assembly reference?)
//         public void CharToHexLookup_ReturnsSpanOfLength256()
//         {
//             // Act
//             var span = HexConverter.CharToHexLookup;
// 
//             // Assert
//             Assert.Equal(256, span.Length);
//         }
    }
}
