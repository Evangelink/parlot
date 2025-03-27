using Moq;
using Parlot.Benchmarks;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="IdentifiersBenchmarks"/> class.
    /// </summary>
    public class IdentifiersBenchmarksTests
    {
        private const int ExpectedIndex = 18;
        private readonly IdentifiersBenchmarks _benchmarks;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiersBenchmarksTests"/> class.
        /// </summary>
        public IdentifiersBenchmarksTests()
        {
            _benchmarks = new IdentifiersBenchmarks();
        }

        /// <summary>
        /// Tests the <see cref="IdentifiersBenchmarks.NaiveIdentifierMatch"/> method in the happy path scenario.
        /// The expected outcome is that the method returns the correct index marking the end of the valid identifier.
        /// </summary>
        [Fact]
        public void NaiveIdentifierMatch_HappyPath_ReturnsExpectedIndex()
        {
            // Act
            int result = _benchmarks.NaiveIdentifierMatch();

            // Assert
            Assert.Equal(ExpectedIndex, result);
        }

        /// <summary>
        /// Tests the <see cref="IdentifiersBenchmarks.SearchValuesIdentifierMatch"/> method in the happy path scenario.
        /// The expected outcome is that the method returns the correct index marking the end of the valid identifier.
        /// </summary>
        [Fact]
        public void SearchValuesIdentifierMatch_HappyPath_ReturnsExpectedIndex()
        {
            // Act
            int result = _benchmarks.SearchValuesIdentifierMatch();

            // Assert
            Assert.Equal(ExpectedIndex, result);
        }

        /// <summary>
        /// Tests the <see cref="IdentifiersBenchmarks.NaiveIdentifierAndContainsMatch"/> method in the happy path scenario.
        /// The expected outcome is that the method returns the correct index marking the end of the valid identifier.
        /// </summary>
        [Fact]
        public void NaiveIdentifierAndContainsMatch_HappyPath_ReturnsExpectedIndex()
        {
            // Act
            int result = _benchmarks.NaiveIdentifierAndContainsMatch();

            // Assert
            Assert.Equal(ExpectedIndex, result);
        }
    }
}
