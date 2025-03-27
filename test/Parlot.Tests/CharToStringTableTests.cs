using Parlot;
using System;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CharToStringTable"/> class.
    /// </summary>
    public class CharToStringTableTests
    {
        /// <summary>
        /// Tests that the GetString method returns the cached string for characters in the range [0, 255].
        /// </summary>
        /// <param name="input">A character value within the caching range.</param>
//         [Theory] [Error] (27-29)CS0122 'CharToStringTable' is inaccessible due to its protection level
//         [InlineData('A')]
//         [InlineData('0')]
//         [InlineData((char)0)]
//         [InlineData((char)255)]
//         public void GetString_WithinCacheRange_ReturnsCachedString(char input)
//         {
//             // Arrange
//             string expected = input.ToString();
// 
//             // Act
//             string actual = CharToStringTable.GetString(input);
// 
//             // Assert
//             Assert.Equal(expected, actual);
//         }

        /// <summary>
        /// Tests that the GetString method returns the result of ToString for characters outside the caching range.
        /// </summary>
        /// <param name="input">A character value that is not cached (>= 256).</param>
//         [Theory] [Error] (47-29)CS0122 'CharToStringTable' is inaccessible due to its protection level
//         [InlineData((char)256)]
//         [InlineData((char)300)]
//         [InlineData((char)500)]
//         public void GetString_OutOfCacheRange_ReturnsToStringResult(char input)
//         {
//             // Arrange
//             string expected = input.ToString();
// 
//             // Act
//             string actual = CharToStringTable.GetString(input);
// 
//             // Assert
//             Assert.Equal(expected, actual);
//         }
    }
}
