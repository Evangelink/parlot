using Parlot.Benchmarks;
using System;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="IndexOfBenchmarks"/> class.
    /// </summary>
    public class IndexOfBenchmarksTests
    {
        private readonly IndexOfBenchmarks _benchmarks;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexOfBenchmarksTests"/> class.
        /// </summary>
        public IndexOfBenchmarksTests()
        {
            _benchmarks = new IndexOfBenchmarks();
        }

        /// <summary>
        /// Tests the RosIndexOfChar method to ensure it returns -1 when the backslash character is not found.
        /// </summary>
        [Fact]
        public void RosIndexOfChar_WhenBackslashNotPresent_ReturnsMinusOne()
        {
            // Act
            int result = _benchmarks.RosIndexOfChar();
            
            // Assert
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests the RosIndexOfString method to ensure it returns -1 when the backslash string is not found.
        /// </summary>
        [Fact]
        public void RosIndexOfString_WhenBackslashNotPresent_ReturnsMinusOne()
        {
            // Act
            int result = _benchmarks.RosIndexOfString();
            
            // Assert
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests the RosIndexOfStringOrdinal method to ensure it returns -1 when the backslash string is not found using ordinal comparison.
        /// </summary>
        [Fact]
        public void RosIndexOfStringOrdinal_WhenBackslashNotPresent_ReturnsMinusOne()
        {
            // Act
            int result = _benchmarks.RosIndexOfStringOrdinal();
            
            // Assert
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests the StringIndexOfChar method to ensure it returns -1 when the backslash character is not found.
        /// </summary>
        [Fact]
        public void StringIndexOfChar_WhenBackslashNotPresent_ReturnsMinusOne()
        {
            // Act
            int result = _benchmarks.StringIndexOfChar();
            
            // Assert
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests the StringIndexOfString method to ensure it returns -1 when the backslash string is not found.
        /// </summary>
        [Fact]
        public void StringIndexOfString_WhenBackslashNotPresent_ReturnsMinusOne()
        {
            // Act
            int result = _benchmarks.StringIndexOfString();
            
            // Assert
            Assert.Equal(-1, result);
        }

        /// <summary>
        /// Tests the StringIndexOfStringOrdinal method to ensure it returns -1 when the backslash string is not found using ordinal comparison.
        /// </summary>
        [Fact]
        public void StringIndexOfStringOrdinal_WhenBackslashNotPresent_ReturnsMinusOne()
        {
            // Act
            int result = _benchmarks.StringIndexOfStringOrdinal();
            
            // Assert
            Assert.Equal(-1, result);
        }
    }
}
