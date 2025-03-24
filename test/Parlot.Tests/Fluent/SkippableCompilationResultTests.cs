using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="SkippableCompilationResult"/> class.
    /// </summary>
    public class SkippableCompilationResultTests
    {
        /// <summary>
        /// Tests that the constructor initializes the properties correctly when provided with a non-null CompilationResult.
        /// </summary>
        [Fact]
        public void Constructor_WithNonNullCompilationResult_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var dummyCompilationResult = new DummyCompilationResult();
            bool skipFlag = true;

            // Act
            var result = new SkippableCompilationResult(dummyCompilationResult, skipFlag);

            // Assert
            Assert.Equal(dummyCompilationResult, result.CompilationResult);
            Assert.True(result.Skip);
        }

        /// <summary>
        /// Tests that the constructor initializes the properties correctly when provided with a null CompilationResult.
        /// </summary>
        [Fact]
        public void Constructor_WithNullCompilationResult_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            CompilationResult? dummyCompilationResult = null;
            bool skipFlag = false;

            // Act
            var result = new SkippableCompilationResult(dummyCompilationResult, skipFlag);

            // Assert
            Assert.Null(result.CompilationResult);
            Assert.False(result.Skip);
        }

        /// <summary>
        /// Tests that the CompilationResult property can be updated and retrieved successfully.
        /// </summary>
        [Fact]
        public void CompilationResultProperty_GetAndSet_ShouldWorkCorrectly()
        {
            // Arrange
            var initialDummy = new DummyCompilationResult();
            var updatedDummy = new DummyCompilationResult();
            var result = new SkippableCompilationResult(initialDummy, false);

            // Act
            result.CompilationResult = updatedDummy;

            // Assert
            Assert.Equal(updatedDummy, result.CompilationResult);
        }

        /// <summary>
        /// Tests that the Skip property can be updated and retrieved successfully.
        /// </summary>
        /// <param name="skipValue">The value to set for the Skip property.</param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SkipProperty_GetAndSet_ShouldWorkCorrectly(bool skipValue)
        {
            // Arrange
            var dummyCompilationResult = new DummyCompilationResult();
            var result = new SkippableCompilationResult(dummyCompilationResult, !skipValue);

            // Act
            result.Skip = skipValue;

            // Assert
            Assert.Equal(skipValue, result.Skip);
        }
    }

    /// <summary>
    /// A dummy implementation of CompilationResult for testing purposes.
    /// </summary>
    public class DummyCompilationResult : CompilationResult
    {
        // Dummy implementation details are omitted, as this class is intended solely for testing.
    }
}
