using Moq;
using System;
using Xunit;
using Xunit.Sdk;

/// <summary>
/// Unit tests for the <see cref = "HexConverter"/> class, specifically testing the IsHexChar method.
/// </summary>
public class HexConverterTests
{
    /// <summary>
    /// Tests that IsHexChar returns true for valid hexadecimal characters.
    /// This test covers the happy path where the provided character is one of:
    /// '0'-'9', 'A'-'F', 'a'-'f'. The test uses a series of inline data to validate that each
    /// valid hex character returns true.
    /// </summary>
    /// <param name = "input">The ASCII integer value representing a hex character.</param>
//     [Theory] [Error] (44-23)CS0122 'HexConverter' is inaccessible due to its protection level
//     [InlineData((int)'0')]
//     [InlineData((int)'1')]
//     [InlineData((int)'9')]
//     [InlineData((int)'A')]
//     [InlineData((int)'B')]
//     [InlineData((int)'C')]
//     [InlineData((int)'D')]
//     [InlineData((int)'E')]
//     [InlineData((int)'F')]
//     [InlineData((int)'a')]
//     [InlineData((int)'b')]
//     [InlineData((int)'c')]
//     [InlineData((int)'d')]
//     [InlineData((int)'e')]
//     [InlineData((int)'f')]
//     public void IsHexChar_ValidHexCharacter_ReturnsTrue(int input)
//     {
//         // Arrange
//         if (!Environment.Is64BitProcess)
//         {
//             // Skip test on non 64-bit environments as the behavior depends on IntPtr.Size.
//             throw new SkipException("Test is designed for 64-bit environments.");
//         }
// 
//         // Act
//         bool result = HexConverter.IsHexChar(input);
//         // Assert
//         Assert.True(result, $"Expected IsHexChar({(char)input}) to return true.");
//     }

    /// <summary>
    /// Tests that IsHexChar returns false for characters that are not valid hexadecimal digits.
    /// This test covers edge cases such as characters outside the valid hex range, negative values,
    /// and values that are not standard ASCII letters or digits.
    /// </summary>
    /// <param name = "input">The ASCII integer value representing a non-hex character.</param>
//     [Theory] [Error] (74-23)CS0122 'HexConverter' is inaccessible due to its protection level
//     [InlineData((int)'G')]
//     [InlineData((int)'g')]
//     [InlineData((int)' ')]
//     [InlineData((int)'!')]
//     [InlineData(-1)]
//     [InlineData(128)]
//     [InlineData((int)'z')]
//     [InlineData((int)'@')]
//     public void IsHexChar_InvalidHexCharacter_ReturnsFalse(int input)
//     {
//         // Arrange
//         if (!Environment.Is64BitProcess)
//         {
//             // Skip test on non 64-bit environments as the behavior depends on IntPtr.Size.
//             throw new SkipException("Test is designed for 64-bit environments.");
//         }
// 
//         // Act
//         bool result = HexConverter.IsHexChar(input);
//         // Assert
//         Assert.False(result, $"Expected IsHexChar({(char)input}) to return false.");
//     }

    /// <summary>
    /// Tests the <see cref = "HexConverter.FromChar(int)"/> method when invoked with a valid index (within the range of the lookup table).
    /// Expects to return the corresponding value from the lookup table.
    /// </summary>
//     [Fact] [Error] (88-30)CS0122 'HexConverter' is inaccessible due to its protection level [Error] (90-27)CS0122 'HexConverter' is inaccessible due to its protection level
//     public void FromChar_InputWithinLookupRange_ReturnsLookupValue()
//     {
//         // Arrange
//         int validIndex = 0;
//         byte expectedValue = HexConverter.CharToHexLookup[validIndex];
//         // Act
//         int actualValue = HexConverter.FromChar(validIndex);
//         // Assert
//         Assert.Equal(expectedValue, actualValue);
//     }

    /// <summary>
    /// Tests the <see cref = "HexConverter.FromChar(int)"/> method when invoked with the highest valid index.
    /// Expects to return the corresponding value from the lookup table.
    /// </summary>
//     [Fact] [Error] (103-26)CS0122 'HexConverter' is inaccessible due to its protection level [Error] (104-30)CS0122 'HexConverter' is inaccessible due to its protection level [Error] (106-27)CS0122 'HexConverter' is inaccessible due to its protection level
//     public void FromChar_InputAtUpperBoundOfLookup_ReturnsLookupValue()
//     {
//         // Arrange
//         int validIndex = HexConverter.CharToHexLookup.Length - 1;
//         byte expectedValue = HexConverter.CharToHexLookup[validIndex];
//         // Act
//         int actualValue = HexConverter.FromChar(validIndex);
//         // Assert
//         Assert.Equal(expectedValue, actualValue);
//     }

    /// <summary>
    /// Tests the <see cref = "HexConverter.FromChar(int)"/> method when invoked with an input that is equal to the length of the lookup table.
    /// Expects to return 0xFF, indicating an invalid hex character.
    /// </summary>
//     [Fact] [Error] (119-28)CS0122 'HexConverter' is inaccessible due to its protection level [Error] (122-27)CS0122 'HexConverter' is inaccessible due to its protection level
//     public void FromChar_InputEqualToLookupLength_ReturnsFF()
//     {
//         // Arrange
//         int invalidIndex = HexConverter.CharToHexLookup.Length;
//         int expectedValue = 0xFF;
//         // Act
//         int actualValue = HexConverter.FromChar(invalidIndex);
//         // Assert
//         Assert.Equal(expectedValue, actualValue);
//     }

    /// <summary>
    /// Tests the <see cref = "HexConverter.FromChar(int)"/> method when invoked with an input greater than the lookup table length.
    /// Expects to return 0xFF, indicating an invalid hex character.
    /// </summary>
//     [Fact] [Error] (135-28)CS0122 'HexConverter' is inaccessible due to its protection level [Error] (138-27)CS0122 'HexConverter' is inaccessible due to its protection level
//     public void FromChar_InputGreaterThanLookupLength_ReturnsFF()
//     {
//         // Arrange
//         int invalidIndex = HexConverter.CharToHexLookup.Length + 10;
//         int expectedValue = 0xFF;
//         // Act
//         int actualValue = HexConverter.FromChar(invalidIndex);
//         // Assert
//         Assert.Equal(expectedValue, actualValue);
//     }

    /// <summary>
    /// Tests the <see cref = "HexConverter.FromChar(int)"/> method when invoked with a negative input.
    /// Expects an <see cref = "IndexOutOfRangeException"/> due to invalid indexing into the lookup table.
    /// </summary>
//     [Fact] [Error] (153-55)CS0122 'HexConverter' is inaccessible due to its protection level
//     public void FromChar_InputNegative_ThrowsIndexOutOfRangeException()
//     {
//         // Arrange
//         int negativeInput = -1;
//         // Act & Assert
//         Assert.Throws<IndexOutOfRangeException>(() => HexConverter.FromChar(negativeInput));
//     }

    /// <summary>
    /// Tests that the CharToHexLookup property returns a span of length 256.
    /// </summary>
//     [Fact] [Error] (163-37)CS0122 'HexConverter' is inaccessible due to its protection level
//     public void CharToHexLookup_WhenAccessed_ReturnsSpanOfLength256()
//     {
//         // Act
//         ReadOnlySpan<byte> lookup = HexConverter.CharToHexLookup;
//         // Assert
//         Assert.Equal(256, lookup.Length);
//     }

    /// <summary>
    /// Tests that the lookup correctly maps the ASCII digits '0' to '9' to their hex values (0-9).
    /// </summary>
    /// <param name = "ascii">The ASCII value of the digit.</param>
    /// <param name = "expected">The expected hex value.</param>
//     [Theory] [Error] (187-37)CS0122 'HexConverter' is inaccessible due to its protection level
//     [InlineData(48, 0)] // '0'
//     [InlineData(49, 1)] // '1'
//     [InlineData(50, 2)] // '2'
//     [InlineData(51, 3)] // '3'
//     [InlineData(52, 4)] // '4'
//     [InlineData(53, 5)] // '5'
//     [InlineData(54, 6)] // '6'
//     [InlineData(55, 7)] // '7'
//     [InlineData(56, 8)] // '8'
//     [InlineData(57, 9)] // '9'
//     public void CharToHexLookup_DigitCharacters_ReturnsExpectedHexValue(int ascii, byte expected)
//     {
//         // Arrange
//         ReadOnlySpan<byte> lookup = HexConverter.CharToHexLookup;
//         // Act
//         byte actual = lookup[ascii];
//         // Assert
//         Assert.Equal(expected, actual);
//     }

    /// <summary>
    /// Tests that the lookup correctly maps uppercase hexadecimal characters ('A' to 'F') to their expected hex values (10-15).
    /// </summary>
    /// <param name = "ascii">The ASCII value of the uppercase character.</param>
    /// <param name = "expected">The expected hex value.</param>
//     [Theory] [Error] (209-37)CS0122 'HexConverter' is inaccessible due to its protection level
//     [InlineData(65, 10)] // 'A'
//     [InlineData(66, 11)] // 'B'
//     [InlineData(67, 12)] // 'C'
//     [InlineData(68, 13)] // 'D'
//     [InlineData(69, 14)] // 'E'
//     [InlineData(70, 15)] // 'F'
//     public void CharToHexLookup_UppercaseHexCharacters_ReturnsExpectedHexValue(int ascii, byte expected)
//     {
//         // Arrange
//         ReadOnlySpan<byte> lookup = HexConverter.CharToHexLookup;
//         // Act
//         byte actual = lookup[ascii];
//         // Assert
//         Assert.Equal(expected, actual);
//     }

    /// <summary>
    /// Tests that the lookup correctly maps lowercase hexadecimal characters ('a' to 'f') to their expected hex values (10-15).
    /// </summary>
    /// <param name = "ascii">The ASCII value of the lowercase character.</param>
    /// <param name = "expected">The expected hex value.</param>
//     [Theory] [Error] (231-37)CS0122 'HexConverter' is inaccessible due to its protection level
//     [InlineData(97, 10)] // 'a'
//     [InlineData(98, 11)] // 'b'
//     [InlineData(99, 12)] // 'c'
//     [InlineData(100, 13)] // 'd'
//     [InlineData(101, 14)] // 'e'
//     [InlineData(102, 15)] // 'f'
//     public void CharToHexLookup_LowercaseHexCharacters_ReturnsExpectedHexValue(int ascii, byte expected)
//     {
//         // Arrange
//         ReadOnlySpan<byte> lookup = HexConverter.CharToHexLookup;
//         // Act
//         byte actual = lookup[ascii];
//         // Assert
//         Assert.Equal(expected, actual);
//     }

    /// <summary>
    /// Tests that the lookup returns 0xFF for characters that are not valid hexadecimal digits.
    /// </summary>
    /// <param name = "ascii">The ASCII value of the non-hex character.</param>
//     [Theory] [Error] (254-37)CS0122 'HexConverter' is inaccessible due to its protection level
//     [InlineData(32)] // space
//     [InlineData(47)] // '/'
//     [InlineData(58)] // ':'
//     [InlineData(64)] // '@'
//     [InlineData(71)] // 'G'
//     [InlineData(96)] // '`'
//     [InlineData(103)] // 'g'
//     [InlineData(127)] // DEL
//     public void CharToHexLookup_NonHexCharacters_Returns0xFF(int ascii)
//     {
//         // Arrange
//         ReadOnlySpan<byte> lookup = HexConverter.CharToHexLookup;
//         // Act
//         byte actual = lookup[ascii];
//         // Assert
//         Assert.Equal(0xFF, actual);
//     }
}