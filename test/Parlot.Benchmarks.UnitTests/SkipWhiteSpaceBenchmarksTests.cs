using Parlot.Benchmarks;
using System;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="SkipWhiteSpaceBenchmarks"/> class.
    /// </summary>
    public class SkipWhiteSpaceBenchmarksTests
    {
        /// <summary>
        /// Tests the SkipWhiteSpace_Default method returns false when the source has no leading white space,
        /// and returns true when the source has leading white spaces.
        /// </summary>
        /// <param name="length">The number of leading spaces to include in the source string.</param>
        /// <param name="expected">The expected boolean result from the method.</param>
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void SkipWhiteSpace_Default_WithVariousLengths_ReturnsExpectedOutcome(int length, bool expected)
        {
            // Arrange
            var benchmarks = new SkipWhiteSpaceBenchmarks
            {
                Length = length
            };
            benchmarks.Setup();

            // Act
            bool result = benchmarks.SkipWhiteSpace_Default();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the SkipWhiteSpaceOrNewLine_Default method returns false when the source has no leading white space or new line,
        /// and returns true when the source has leading white spaces.
        /// </summary>
        /// <param name="length">The number of leading spaces to include in the source string.</param>
        /// <param name="expected">The expected boolean result from the method.</param>
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void SkipWhiteSpaceOrNewLine_Default_WithVariousLengths_ReturnsExpectedOutcome(int length, bool expected)
        {
            // Arrange
            var benchmarks = new SkipWhiteSpaceBenchmarks
            {
                Length = length
            };
            benchmarks.Setup();

            // Act
            bool result = benchmarks.SkipWhiteSpaceOrNewLine_Default();

            // Assert
            Assert.Equal(expected, result);
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Tests the SkipWhiteSpace_Vectorized method returns false when the source starts with a non-white space character,
        /// and returns true when it skips one or more white space characters.
        /// </summary>
        /// <param name="length">The number of leading spaces to include in the source string.</param>
        /// <param name="expected">The expected boolean result from the method.</param>
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void SkipWhiteSpace_Vectorized_WithVariousLengths_ReturnsExpectedOutcome(int length, bool expected)
        {
            // Arrange
            var benchmarks = new SkipWhiteSpaceBenchmarks
            {
                Length = length
            };
            benchmarks.Setup();

            // Act
            bool result = benchmarks.SkipWhiteSpace_Vectorized();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the SkipWhiteSpaceOrNewLines_Vectorized method returns false when the source starts with a non-white space character,
        /// and returns true when it skips one or more white space or newline characters.
        /// </summary>
        /// <param name="length">The number of leading spaces to include in the source string.</param>
        /// <param name="expected">The expected boolean result from the method.</param>
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void SkipWhiteSpaceOrNewLines_Vectorized_WithVariousLengths_ReturnsExpectedOutcome(int length, bool expected)
        {
            // Arrange
            var benchmarks = new SkipWhiteSpaceBenchmarks
            {
                Length = length
            };
            benchmarks.Setup();

            // Act
            bool result = benchmarks.SkipWhiteSpaceOrNewLines_Vectorized();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the SkipWhiteSpace_PeekSearchValue method returns false when the source starts with a non-white space character,
        /// and returns true when it skips one or more white space characters using a peek search approach.
        /// </summary>
        /// <param name="length">The number of leading spaces to include in the source string.</param>
        /// <param name="expected">The expected boolean result from the method.</param>
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void SkipWhiteSpace_PeekSearchValue_WithVariousLengths_ReturnsExpectedOutcome(int length, bool expected)
        {
            // Arrange
            var benchmarks = new SkipWhiteSpaceBenchmarks
            {
                Length = length
            };
            benchmarks.Setup();

            // Act
            bool result = benchmarks.SkipWhiteSpace_PeekSearchValue();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the SkipWhiteSpaceOrNewLines_PeekSearchValue method returns false when the source starts with a non-white space character,
        /// and returns true when it skips one or more white space or newline characters using a peek search approach.
        /// </summary>
        /// <param name="length">The number of leading spaces to include in the source string.</param>
        /// <param name="expected">The expected boolean result from the method.</param>
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void SkipWhiteSpaceOrNewLines_PeekSearchValue_WithVariousLengths_ReturnsExpectedOutcome(int length, bool expected)
        {
            // Arrange
            var benchmarks = new SkipWhiteSpaceBenchmarks
            {
                Length = length
            };
            benchmarks.Setup();

            // Act
            bool result = benchmarks.SkipWhiteSpaceOrNewLines_PeekSearchValue();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the SkipWhiteSpace_PeekCharacter method returns false when the source starts with a non-white space character,
        /// and returns true when it skips one or more white space characters by peeking each character.
        /// </summary>
        /// <param name="length">The number of leading spaces to include in the source string.</param>
        /// <param name="expected">The expected boolean result from the method.</param>
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void SkipWhiteSpace_PeekCharacter_WithVariousLengths_ReturnsExpectedOutcome(int length, bool expected)
        {
            // Arrange
            var benchmarks = new SkipWhiteSpaceBenchmarks
            {
                Length = length
            };
            benchmarks.Setup();

            // Act
            bool result = benchmarks.SkipWhiteSpace_PeekCharacter();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the SkipWhiteSpaceOrNewLines_PeekCharacter method returns false when the source starts with a non-white space character,
        /// and returns true when it skips one or more white space or newline characters by peeking each character.
        /// </summary>
        /// <param name="length">The number of leading spaces to include in the source string.</param>
        /// <param name="expected">The expected boolean result from the method.</param>
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(10, true)]
        public void SkipWhiteSpaceOrNewLines_PeekCharacter_WithVariousLengths_ReturnsExpectedOutcome(int length, bool expected)
        {
            // Arrange
            var benchmarks = new SkipWhiteSpaceBenchmarks
            {
                Length = length
            };
            benchmarks.Setup();

            // Act
            bool result = benchmarks.SkipWhiteSpaceOrNewLines_PeekCharacter();

            // Assert
            Assert.Equal(expected, result);
        }
#endif
    }
}
