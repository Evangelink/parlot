using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="SkippableCompilationResult"/> class.
    /// </summary>
    public class SkippableCompilationResultTests
    {
        private readonly CompilationResult _dummyCompilationResult1;
        private readonly CompilationResult _dummyCompilationResult2;

        /// <summary>
        /// Initializes readonly dummy instances used in the tests.
        /// </summary>
        public SkippableCompilationResultTests()
        {
            // Using Moq to create dummy instances of CompilationResult.
            _dummyCompilationResult1 = new Mock<CompilationResult>().Object;
            _dummyCompilationResult2 = new Mock<CompilationResult>().Object;
        }

        /// <summary>
        /// Tests the constructor with valid parameters to ensure properties are correctly assigned.
        /// </summary>
        [Fact]
        public void Constructor_ValidParameters_PropertiesAreSet()
        {
            // Arrange
            bool skipValue = true;

            // Act
            var result = new SkippableCompilationResult(_dummyCompilationResult1, skipValue);

            // Assert
            Assert.Equal(_dummyCompilationResult1, result.CompilationResult);
            Assert.Equal(skipValue, result.Skip);
        }

        /// <summary>
        /// Tests the constructor when a null CompilationResult is provided, ensuring that the properties are set accordingly.
        /// </summary>
        [Fact]
        public void Constructor_NullCompilationResult_PropertiesAreSetToNull()
        {
            // Arrange
            CompilationResult nullCompilationResult = null;
            bool skipValue = false;

            // Act
            var result = new SkippableCompilationResult(nullCompilationResult, skipValue);

            // Assert
            Assert.Null(result.CompilationResult);
            Assert.Equal(skipValue, result.Skip);
        }

        /// <summary>
        /// Tests that the CompilationResult property getter and setter work correctly by updating its value.
        /// </summary>
        [Fact]
        public void CompilationResultProperty_SetValue_StoresCorrectly()
        {
            // Arrange
            var result = new SkippableCompilationResult(_dummyCompilationResult1, false);

            // Act
            result.CompilationResult = _dummyCompilationResult2;

            // Assert
            Assert.Equal(_dummyCompilationResult2, result.CompilationResult);
        }

        /// <summary>
        /// Tests that the Skip property getter and setter work correctly by updating its value.
        /// </summary>
        [Fact]
        public void SkipProperty_SetValue_StoresCorrectly()
        {
            // Arrange
            var result = new SkippableCompilationResult(_dummyCompilationResult1, false);

            // Act
            result.Skip = true;

            // Assert
            Assert.True(result.Skip);
        }
    }
}
