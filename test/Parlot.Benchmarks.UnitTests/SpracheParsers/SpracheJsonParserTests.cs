using System;
using Parlot.Benchmarks.SpracheParsers;
using Parlot.Tests.Json;
using Sprache;
using Xunit;

namespace Parlot.Benchmarks.SpracheParsers.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="SpracheJsonParser"/> class.
    /// </summary>
    public class SpracheJsonParserTests
    {
        /// <summary>
        /// Tests that the Parse method returns a successful result when a valid JSON object is provided.
        /// Arrange: A valid JSON object string "{}" is used.
        /// Act: The Parse method is called.
        /// Assert: The returned result indicates success and the value is not null.
        /// </summary>
        [Theory]
        [InlineData("{}")]
        [InlineData("[]")]
        [InlineData("\"string\"")]
        public void Parse_ValidJson_ReturnsSuccessfulResult(string validJson)
        {
            // Act
            IResult<IJson> result = SpracheJsonParser.Parse(validJson);
            
            // Assert
            Assert.NotNull(result);
            Assert.True(result.WasSuccessful, $"Expected parsing '{validJson}' to be successful.");
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests that the Parse method returns a failure result when an invalid JSON string is provided.
        /// Arrange: An invalid JSON string is used.
        /// Act: The Parse method is called.
        /// Assert: The returned result indicates failure.
        /// </summary>
        [Fact]
        public void Parse_InvalidJson_ReturnsFailureResult()
        {
            // Arrange
            string invalidJson = "invalid json";

            // Act
            IResult<IJson> result = SpracheJsonParser.Parse(invalidJson);
            
            // Assert
            Assert.NotNull(result);
            Assert.False(result.WasSuccessful, "Expected parsing an invalid JSON string to fail.");
        }

        /// <summary>
        /// Tests that the Parse method throws an ArgumentNullException when null is passed as input.
        /// Arrange: A null input string is used.
        /// Act & Assert: Calling Parse with null should throw an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Parse_NullInput_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => SpracheJsonParser.Parse(null));
        }
    }
}
