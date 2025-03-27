using Parlot;
using System;
using System.Collections.Generic;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CharMap{T}"/> class.
    /// </summary>
    public class CharMapTests
    {
        /// <summary>
        /// Tests that the default constructor initializes ExpectedChars as an empty array.
        /// </summary>
//         [Fact] [Error] (20-31)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (23-36)CS0122 'CharMap<T>.ExpectedChars' is inaccessible due to its protection level [Error] (24-34)CS0122 'CharMap<T>.ExpectedChars' is inaccessible due to its protection level
//         public void Constructor_Default_InitializesEmptyExpectedChars()
//         {
//             // Arrange & Act
//             var charMap = new CharMap<string>();
// 
//             // Assert
//             Assert.NotNull(charMap.ExpectedChars);
//             Assert.Empty(charMap.ExpectedChars);
//         }

        /// <summary>
        /// Tests that the constructor with an ascii map initializes ExpectedChars and the indexer correctly for ascii characters.
        /// </summary>
//         [Fact] [Error] (42-31)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (46-49)CS0122 'CharMap<T>.ExpectedChars' is inaccessible due to its protection level
//         public void Constructor_WithAsciiMap_InitializesExpectedCharsAndAsciiIndexer()
//         {
//             // Arrange
//             var input = new List<KeyValuePair<char, string>>
//             {
//                 new KeyValuePair<char, string>('a', "alpha"),
//                 new KeyValuePair<char, string>('z', "zeta"),
//                 new KeyValuePair<char, string>('m', "middle")
//             };
// 
//             // Act
//             var charMap = new CharMap<string>(input);
// 
//             // Assert
//             var expectedChars = new char[] { 'a', 'm', 'z' };
//             Assert.Equal(expectedChars, charMap.ExpectedChars);
//             Assert.Equal("alpha", charMap['a']);
//             Assert.Equal("middle", charMap['m']);
//             Assert.Equal("zeta", charMap['z']);
//         }

        /// <summary>
        /// Tests that the constructor with a mixed ascii and non-ascii map initializes ExpectedChars and indexers correctly.
        /// </summary>
//         [Fact] [Error] (68-31)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (73-49)CS0122 'CharMap<T>.ExpectedChars' is inaccessible due to its protection level
//         public void Constructor_WithMixedMap_InitializesExpectedCharsAndIndexers()
//         {
//             // Arrange
//             char asciiChar = 'A';
//             char nonAsciiChar = 'é';
//             var input = new List<KeyValuePair<char, string>>
//             {
//                 new KeyValuePair<char, string>(asciiChar, "letter A"),
//                 new KeyValuePair<char, string>(nonAsciiChar, "accented e")
//             };
// 
//             // Act
//             var charMap = new CharMap<string>(input);
// 
//             // Assert
//             var expectedChars = new char[] { asciiChar, nonAsciiChar };
//             Array.Sort(expectedChars);
//             Assert.Equal(expectedChars, charMap.ExpectedChars);
//             Assert.Equal("letter A", charMap[asciiChar]);
//             Assert.Equal("accented e", charMap[nonAsciiChar]);
//         }

        /// <summary>
        /// Tests that the Set method adds a new ascii character and updates ExpectedChars accordingly.
        /// </summary>
//         [Fact] [Error] (85-31)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (90-21)CS0122 'CharMap<T>.Set(char, T)' is inaccessible due to its protection level [Error] (93-47)CS0122 'CharMap<T>.ExpectedChars' is inaccessible due to its protection level
//         public void Set_WithAsciiChar_AddsValueAndUpdatesExpectedChars()
//         {
//             // Arrange
//             var charMap = new CharMap<string>();
//             char testChar = 'c';
//             string testValue = "char c";
// 
//             // Act
//             charMap.Set(testChar, testValue);
// 
//             // Assert
//             Assert.Contains(testChar, charMap.ExpectedChars);
//             Assert.Equal(testValue, charMap[testChar]);
//         }

        /// <summary>
        /// Tests that the Set method adds a new non-ascii character and updates ExpectedChars accordingly.
        /// </summary>
//         [Fact] [Error] (104-31)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (109-21)CS0122 'CharMap<T>.Set(char, T)' is inaccessible due to its protection level [Error] (112-47)CS0122 'CharMap<T>.ExpectedChars' is inaccessible due to its protection level
//         public void Set_WithNonAsciiChar_AddsValueAndUpdatesExpectedChars()
//         {
//             // Arrange
//             var charMap = new CharMap<string>();
//             char testChar = 'ñ';
//             string testValue = "enye";
// 
//             // Act
//             charMap.Set(testChar, testValue);
// 
//             // Assert
//             Assert.Contains(testChar, charMap.ExpectedChars);
//             Assert.Equal(testValue, charMap[testChar]);
//         }

        /// <summary>
        /// Tests that calling Set with a duplicate ascii character does not override the existing value.
        /// </summary>
//         [Fact] [Error] (123-31)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (129-21)CS0122 'CharMap<T>.Set(char, T)' is inaccessible due to its protection level [Error] (130-21)CS0122 'CharMap<T>.Set(char, T)' is inaccessible due to its protection level
//         public void Set_WithDuplicateAsciiChar_DoesNotOverrideExistingValue()
//         {
//             // Arrange
//             var charMap = new CharMap<string>();
//             char testChar = 'd';
//             string initialValue = "delta";
//             string duplicateValue = "changed";
// 
//             // Act
//             charMap.Set(testChar, initialValue);
//             charMap.Set(testChar, duplicateValue);
// 
//             // Assert
//             Assert.Equal(initialValue, charMap[testChar]);
//         }

        /// <summary>
        /// Tests that calling Set with a duplicate non-ascii character does not override the existing value.
        /// </summary>
//         [Fact] [Error] (143-31)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (149-21)CS0122 'CharMap<T>.Set(char, T)' is inaccessible due to its protection level [Error] (150-21)CS0122 'CharMap<T>.Set(char, T)' is inaccessible due to its protection level
//         public void Set_WithDuplicateNonAsciiChar_DoesNotOverrideExistingValue()
//         {
//             // Arrange
//             var charMap = new CharMap<string>();
//             char testChar = 'ø';
//             string initialValue = "initial";
//             string duplicateValue = "duplicate";
// 
//             // Act
//             charMap.Set(testChar, initialValue);
//             charMap.Set(testChar, duplicateValue);
// 
//             // Assert
//             Assert.Equal(initialValue, charMap[testChar]);
//         }

        /// <summary>
        /// Tests that the indexer returns null when an ascii character has not been set.
        /// </summary>
//         [Fact] [Error] (163-31)CS0122 'CharMap<T>' is inaccessible due to its protection level
//         public void Indexer_ForUnsetAsciiChar_ReturnsNull()
//         {
//             // Arrange
//             var charMap = new CharMap<string>();
// 
//             // Act & Assert
//             Assert.Null(charMap['x']);
//         }

        /// <summary>
        /// Tests that the indexer returns null when a non-ascii character has not been set.
        /// </summary>
//         [Fact] [Error] (176-31)CS0122 'CharMap<T>' is inaccessible due to its protection level
//         public void Indexer_ForUnsetNonAsciiChar_ReturnsNull()
//         {
//             // Arrange
//             var charMap = new CharMap<string>();
//             char nonExistingChar = 'ü';
// 
//             // Act & Assert
//             Assert.Null(charMap[nonExistingChar]);
//         }

        /// <summary>
        /// Tests that the static field IndexerMethodInfo is correctly assigned to the get_Item method.
        /// </summary>
//         [Fact] [Error] (190-45)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (193-28)CS0122 'CharMap<T>' is inaccessible due to its protection level [Error] (194-46)CS0122 'CharMap<T>' is inaccessible due to its protection level
//         public void IndexerMethodInfo_StaticField_IsCorrectlyAssigned()
//         {
//             // Arrange
//             var expectedMethodInfo = typeof(CharMap<string>).GetMethod("get_Item");
// 
//             // Act & Assert
//             Assert.NotNull(CharMap<string>.IndexerMethodInfo);
//             Assert.Equal(expectedMethodInfo, CharMap<string>.IndexerMethodInfo);
//         }
    }
}
