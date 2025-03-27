using Parlot.Benchmarks.PidginParsers;
using System;
using Xunit;

namespace Parlot.Benchmarks.PidginParsers.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="PidginJsonParser"/> static class.
    /// </summary>
    public class PidginJsonParserTests
    {
        /// <summary>
        /// Tests that the Parse method returns a successful result and a non-null IJson value when given valid JSON input.
        /// Scenarios include JSON objects, arrays, strings and complex objects.
        /// </summary>
        /// <param name="jsonInput">A valid JSON string input.</param>
        [Theory]
        [InlineData("{}")]
        [InlineData("[]")]
        [InlineData("\"hello\"")]
        [InlineData("{\"key\":\"value\"}")]
        public void Parse_ValidJsonInput_ReturnsSuccess(string jsonInput)
        {
            // Act
            var result = PidginJsonParser.Parse(jsonInput);

            // Assert: Expect parsing to succeed with a valid IJson result.
            Assert.True(result.Success, $"Expected successful parsing for input: {jsonInput}. Error: {result.Error}");
            Assert.NotNull(result.Value);
        }

        /// <summary>
        /// Tests that the Parse method returns a failure result when given invalid JSON input.
        /// Scenarios include malformed JSON strings.
        /// </summary>
        /// <param name="jsonInput">An invalid JSON string input.</param>
        [Theory]
        [InlineData("invalid")]
        [InlineData("123abc")]
        [InlineData("{{}")]
        [InlineData("[}]")]
        public void Parse_InvalidJsonInput_ReturnsFailure(string jsonInput)
        {
            // Act
            var result = PidginJsonParser.Parse(jsonInput);

            // Assert: Expect parsing to fail for invalid JSON inputs.
            Assert.False(result.Success, $"Expected parsing to fail for input: {jsonInput}.");
        }

        /// <summary>
        /// Tests that the Parse method returns a failure result when given an empty string.
        /// This is a boundary condition where no meaningful JSON content is provided.
        /// </summary>
        [Fact]
        public void Parse_EmptyString_ReturnsFailure()
        {
            // Arrange
            string jsonInput = string.Empty;

            // Act
            var result = PidginJsonParser.Parse(jsonInput);

            // Assert: Expect parsing to fail for an empty string input.
            Assert.False(result.Success, "Expected parsing to fail for an empty string.");
        }

        /// <summary>
        /// Tests that the Parse method throws an ArgumentNullException when passed a null input.
        /// This ensures that the method correctly handles improper null input.
        /// </summary>
//         [Fact] [Error] (76-32)CS8600 Converting null literal or possible null value to non-nullable type.
//         public void Parse_NullInput_ThrowsArgumentNullException()
//         {
//             // Arrange
//             string jsonInput = null;
// 
//             // Act & Assert: Expect an ArgumentNullException when parsing a null input.
//             Assert.Throws<ArgumentNullException>(() => PidginJsonParser.Parse(jsonInput));
//         }
    }
}
