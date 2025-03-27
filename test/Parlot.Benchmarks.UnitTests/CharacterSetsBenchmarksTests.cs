using System;
using Parlot.Benchmarks;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CharacterSetsBenchmarks"/> class.
    /// </summary>
    public class CharacterSetsBenchmarksTests
    {
        private readonly CharacterSetsBenchmarks _benchmarks;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSetsBenchmarksTests"/> class.
        /// </summary>
        public CharacterSetsBenchmarksTests()
        {
            // Arrange: Creating a new instance of CharacterSetsBenchmarks.
            // The constructor validates that all benchmark methods return expected results.
            _benchmarks = new CharacterSetsBenchmarks();
        }

        /// <summary>
        /// Tests that the Character_IsIdentifierPart_True method returns true.
        /// </summary>
        [Fact]
        public void Character_IsIdentifierPart_True_ReturnsTrue()
        {
            // Act
            bool result = _benchmarks.Character_IsIdentifierPart_True();

            // Assert
            Assert.True(result, "Expected Character_IsIdentifierPart_True to return true when provided with a valid identifier character.");
        }

        /// <summary>
        /// Tests that the Character_IsIdentifierPart_False method returns false.
        /// </summary>
        [Fact]
        public void Character_IsIdentifierPart_False_ReturnsFalse()
        {
            // Act
            bool result = _benchmarks.Character_IsIdentifierPart_False();

            // Assert
            Assert.False(result, "Expected Character_IsIdentifierPart_False to return false when provided with an invalid identifier character.");
        }

        /// <summary>
        /// Tests that the SearchValuesIndexOfAny_IsIdentifierPart_True method returns true.
        /// </summary>
        [Fact]
        public void SearchValuesIndexOfAny_IsIdentifierPart_True_ReturnsTrue()
        {
            // Act
            bool result = _benchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True();

            // Assert
            Assert.True(result, "Expected SearchValuesIndexOfAny_IsIdentifierPart_True to return true when the first character is a valid identifier character.");
        }

        /// <summary>
        /// Tests that the SearchValuesIndexOfAny_IsIdentifierPart_False method returns false.
        /// </summary>
        [Fact]
        public void SearchValuesIndexOfAny_IsIdentifierPart_False_ReturnsFalse()
        {
            // Act
            bool result = _benchmarks.SearchValuesIndexOfAny_IsIdentifierPart_False();

            // Assert
            Assert.False(result, "Expected SearchValuesIndexOfAny_IsIdentifierPart_False to return false when the first character is not a valid identifier character.");
        }

        /// <summary>
        /// Tests that the SearchValuesContains_IsIdentifierPart_True method returns true.
        /// </summary>
        [Fact]
        public void SearchValuesContains_IsIdentifierPart_True_ReturnsTrue()
        {
            // Act
            bool result = _benchmarks.SearchValuesContains_IsIdentifierPart_True();

            // Assert
            Assert.True(result, "Expected SearchValuesContains_IsIdentifierPart_True to return true when the identifier contains a valid character.");
        }

        /// <summary>
        /// Tests that the SearchValuesContains_IsIdentifierPart_False method returns false.
        /// </summary>
        [Fact]
        public void SearchValuesContains_IsIdentifierPart_False_ReturnsFalse()
        {
            // Act
            bool result = _benchmarks.SearchValuesContains_IsIdentifierPart_False();

            // Assert
            Assert.False(result, "Expected SearchValuesContains_IsIdentifierPart_False to return false when the identifier does not contain a valid character.");
        }

        /// <summary>
        /// Tests that instantiating the CharacterSetsBenchmarks class does not throw an exception.
        /// </summary>
        [Fact]
        public void Constructor_WithValidBenchmarkMethods_DoesNotThrowException()
        {
            // Act
            var exception = Record.Exception(() => new CharacterSetsBenchmarks());

            // Assert
            Assert.Null(exception);
        }
    }
}
