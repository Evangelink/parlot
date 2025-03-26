using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Tests that the parameterless constructor initializes ExpectedChars as an empty array.
        /// </summary>
        [Fact]
        public void Constructor_NoArguments_ExpectedCharsIsEmpty()
        {
            // Arrange & Act
            var charMap = new CharMap<string>();

            // Assert
            Assert.Empty(charMap.ExpectedChars);
        }

        /// <summary>
        /// Tests that the constructor with an enumerable initializes ExpectedChars with unique, sorted characters.
        /// </summary>
        [Fact]
        public void Constructor_WithMap_InitializesExpectedCharsSortedAndStoresValues()
        {
            // Arrange
            var mapEntries = new List<KeyValuePair<char, string>>
            {
                new KeyValuePair<char, string>('z', "zValue"),
                new KeyValuePair<char, string>('a', "aValue"),
                new KeyValuePair<char, string>('m', "mValue")
            };

            // Act
            var charMap = new CharMap<string>(mapEntries);

            // Assert
            var expectedChars = new char[] { 'a', 'm', 'z' };
            Assert.Equal(expectedChars, charMap.ExpectedChars);

            // Verify that the indexer returns the correct values for ascii characters.
            Assert.Equal("aValue", charMap[(uint)'a']);
            Assert.Equal("mValue", charMap[(uint)'m']);
            Assert.Equal("zValue", charMap[(uint)'z']);
        }

        /// <summary>
        /// Tests that the constructor with duplicate keys only stores the first assigned value.
        /// </summary>
        [Fact]
        public void Constructor_WithDuplicateKeys_StoresFirstValueOnly()
        {
            // Arrange
            var mapEntries = new List<KeyValuePair<char, string>>
            {
                new KeyValuePair<char, string>('a', "first"),
                new KeyValuePair<char, string>('a', "second")
            };

            // Act
            var charMap = new CharMap<string>(mapEntries);

            // Assert
            Assert.Single(charMap.ExpectedChars);
            Assert.Equal('a', charMap.ExpectedChars.First());
            // The indexer should return the first assigned value.
            Assert.Equal("first", charMap[(uint)'a']);
        }

        /// <summary>
        /// Tests that the Set method correctly inserts a new ascii character and updates ExpectedChars.
        /// </summary>
        [Fact]
        public void Set_WithAsciiCharacter_AddsValueAndUpdatesExpectedChars()
        {
            // Arrange
            var charMap = new CharMap<string>();
            char asciiChar = 'C';
            string value = "AsciiValue";

            // Act
            charMap.Set(asciiChar, value);

            // Assert
            Assert.Contains(asciiChar, charMap.ExpectedChars);
            Assert.Equal("AsciiValue", charMap[(uint)asciiChar]);
        }

        /// <summary>
        /// Tests that the Set method correctly inserts a new non-ascii character and updates ExpectedChars.
        /// </summary>
        [Fact]
        public void Set_WithNonAsciiCharacter_AddsValueAndUpdatesExpectedChars()
        {
            // Arrange
            var charMap = new CharMap<string>();
            char nonAsciiChar = 'é'; // Unicode value 233, non-ascii.
            string value = "NonAsciiValue";

            // Act
            charMap.Set(nonAsciiChar, value);

            // Assert
            Assert.Contains(nonAsciiChar, charMap.ExpectedChars);
            Assert.Equal("NonAsciiValue", charMap[(uint)nonAsciiChar]);
        }

        /// <summary>
        /// Tests that the indexer returns null for an unset ascii and non-ascii key.
        /// </summary>
        [Fact]
        public void Indexer_ForUnsetKey_ReturnsNull()
        {
            // Arrange
            var charMap = new CharMap<string>();

            // Act & Assert
            // For an ascii character which has not been set.
            Assert.Null(charMap[(uint)'X']);

            // For a non-ascii character which has not been set.
            Assert.Null(charMap[(uint)'Ω']); // Greek capital Omega.
        }

        /// <summary>
        /// Tests that calling Set multiple times with the same ascii character does not override the original value.
        /// </summary>
        [Fact]
        public void Set_MultipleCallsWithSameAsciiCharacter_DoesNotOverrideInitialValue()
        {
            // Arrange
            var charMap = new CharMap<string>();
            char asciiChar = 'A';
            string initialValue = "Initial";
            string newValue = "NewValue";

            // Act
            charMap.Set(asciiChar, initialValue);
            charMap.Set(asciiChar, newValue);

            // Assert
            Assert.Equal("Initial", charMap[(uint)asciiChar]);
            // ExpectedChars should contain only one instance of the character.
            Assert.Single(charMap.ExpectedChars.Where(c => c == asciiChar));
        }

        /// <summary>
        /// Tests that calling Set multiple times with the same non-ascii character does not override the original value.
        /// </summary>
        [Fact]
        public void Set_MultipleCallsWithSameNonAsciiCharacter_DoesNotOverrideInitialValue()
        {
            // Arrange
            var charMap = new CharMap<string>();
            char nonAsciiChar = 'ü'; // Unicode value 252.
            string initialValue = "Initial";
            string newValue = "NewValue";

            // Act
            charMap.Set(nonAsciiChar, initialValue);
            charMap.Set(nonAsciiChar, newValue);

            // Assert
            Assert.Equal("Initial", charMap[(uint)nonAsciiChar]);
            // ExpectedChars should contain only one instance of the character.
            Assert.Single(charMap.ExpectedChars.Where(c => c == nonAsciiChar));
        }

        /// <summary>
        /// Tests that the indexer returns the correct value for a character set via constructor and then further updated via Set.
        /// </summary>
        [Fact]
        public void Indexer_ReturnsCorrectValues_ForCharactersSetInConstructorAndUpdatedBySet()
        {
            // Arrange
            var mapEntries = new List<KeyValuePair<char, string>>
            {
                new KeyValuePair<char, string>('a', "fromConstructor"),
                new KeyValuePair<char, string>('Ω', "nonAsciiCtor")
            };

            var charMap = new CharMap<string>(mapEntries);

            // Act & Assert
            // Verify initial values set by constructor.
            Assert.Equal("fromConstructor", charMap[(uint)'a']);
            Assert.Equal("nonAsciiCtor", charMap[(uint)'Ω']);

            // Update with Set method for a new ascii character.
            charMap.Set('b', "fromSet");
            Assert.Equal("fromSet", charMap[(uint)'b']);

            // Update with Set method for a new non-ascii character.
            charMap.Set('ñ', "fromSetNonAscii");
            Assert.Equal("fromSetNonAscii", charMap[(uint)'ñ']);
        }
    }
}
