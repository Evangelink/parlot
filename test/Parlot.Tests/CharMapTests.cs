using System.Collections.Generic;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CharMap{T}"/> class.
    /// </summary>
    public class CharMapTests
    {
        /// <summary>
        /// Verifies that the parameterless constructor initializes ExpectedChars as an empty array.
        /// </summary>
        [Fact]
        public void Constructor_Parameterless_ExpectedCharsEmpty()
        {
            // Arrange & Act
            var charMap = new CharMap<string>();

            // Assert
            Assert.NotNull(charMap.ExpectedChars);
            Assert.Empty(charMap.ExpectedChars);
        }

        /// <summary>
        /// Verifies that the indexer returns null for keys that have not been set.
        /// </summary>
        [Theory]
        [InlineData(65)]   // 'A'
        [InlineData(200)]  // Non-ascii key
        public void Indexer_WhenKeyNotSet_ReturnsNull(uint key)
        {
            // Arrange
            var charMap = new CharMap<string>();

            // Act
            var result = charMap[key];

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Verifies that the constructor with a map initializes ExpectedChars in sorted order and assigns values correctly for both ascii and non-ascii keys.
        /// </summary>
        [Fact]
        public void Constructor_WithMap_AssignsValuesAndSortsExpectedChars()
        {
            // Arrange
            var map = new List<KeyValuePair<char, string>>
            {
                new KeyValuePair<char, string>('z', "zValue"),
                new KeyValuePair<char, string>('a', "aValue"),
                new KeyValuePair<char, string>('\u0100', "nonAsciiValue") // non-ascii
            };

            // Act
            var charMap = new CharMap<string>(map);

            // Assert: ExpectedChars should contain all unique keys sorted.
            var expectedKeys = new char[] { 'a', 'z', '\u0100' };
            Assert.Equal(expectedKeys, charMap.ExpectedChars);

            // Assert: Indexer returns correct values.
            Assert.Equal("aValue", charMap['a']);
            Assert.Equal("zValue", charMap['z']);
            Assert.Equal("nonAsciiValue", charMap[(uint)'\u0100']);
        }

        /// <summary>
        /// Verifies that when duplicate keys are provided to the constructor, the first encountered value is retained.
        /// </summary>
        [Fact]
        public void Constructor_WithMap_DuplicateKeys_UsesFirstValue()
        {
            // Arrange
            var map = new List<KeyValuePair<char, string>>
            {
                new KeyValuePair<char, string>('a', "firstValue"),
                new KeyValuePair<char, string>('a', "secondValue")
            };

            // Act
            var charMap = new CharMap<string>(map);

            // Assert: ExpectedChars should contain only one instance of 'a'
            Assert.Single(charMap.ExpectedChars);
            Assert.Equal('a', charMap.ExpectedChars[0]);

            // Assert: Indexer returns the first value and not the second.
            Assert.Equal("firstValue", charMap['a']);
        }

        /// <summary>
        /// Verifies that the Set method assigns a value for an ascii character and updates ExpectedChars accordingly, without overriding an existing value.
        /// </summary>
        [Fact]
        public void Set_ForAsciiCharacter_SetsValueAndUpdatesExpectedChars()
        {
            // Arrange
            var charMap = new CharMap<string>();

            // Act: Set an ascii character.
            charMap.Set('b', "valueB");

            // Assert: ExpectedChars should update and indexer returns the correct value.
            Assert.Contains('b', charMap.ExpectedChars);
            Assert.Equal("valueB", charMap['b']);

            // Act: Attempt to set again for the same ascii character.
            charMap.Set('b', "newValueB");

            // Assert: Value remains unchanged due to null-coalescing assignment.
            Assert.Equal("valueB", charMap['b']);
        }

        /// <summary>
        /// Verifies that the Set method assigns a value for a non-ascii character and updates ExpectedChars accordingly, without overriding an existing value.
        /// </summary>
        [Fact]
        public void Set_ForNonAsciiCharacter_SetsValueAndUpdatesExpectedChars()
        {
            // Arrange
            var nonAsciiChar = '\u0101'; // non-ascii character
            var charMap = new CharMap<string>();

            // Act: Set a non-ascii character.
            charMap.Set(nonAsciiChar, "nonAsciiValue");

            // Assert: ExpectedChars should contain the non-ascii character and indexer returns correct value.
            Assert.Contains(nonAsciiChar, charMap.ExpectedChars);
            Assert.Equal("nonAsciiValue", charMap[(uint)nonAsciiChar]);

            // Act: Attempt to set again for the same non-ascii character.
            charMap.Set(nonAsciiChar, "newNonAsciiValue");

            // Assert: Value remains unchanged.
            Assert.Equal("nonAsciiValue", charMap[(uint)nonAsciiChar]);
        }

        /// <summary>
        /// Verifies that the indexer retrieves correct values for both ascii and non-ascii keys, and returns null for keys that are not set.
        /// </summary>
        [Fact]
        public void Indexer_RetrievesCorrectValues_ForAsciiAndNonAscii()
        {
            // Arrange
            var charMap = new CharMap<string>();
            charMap.Set('x', "asciiValue");
            charMap.Set('\u0102', "nonAsciiValue");

            // Act & Assert: For ascii key
            Assert.Equal("asciiValue", charMap['x']);

            // Act & Assert: For non-ascii key
            Assert.Equal("nonAsciiValue", charMap[(uint)'\u0102']);

            // Act & Assert: For keys that are not set.
            Assert.Null(charMap['y']);
            Assert.Null(charMap[(uint)'\u0103']);
        }

        /// <summary>
        /// Verifies the behavior of the indexer for boundary ascii keys, ensuring that unset keys return null.
        /// </summary>
        [Theory]
        [InlineData(0)]    // First ascii index
        [InlineData(127)]  // Last ascii index
        public void Indexer_ForBoundaryAsciiKeys_ReturnsNullWhenUnset(uint key)
        {
            // Arrange
            var charMap = new CharMap<string>();

            // Act
            var result = charMap[key];

            // Assert
            Assert.Null(result);
        }
    }
}
