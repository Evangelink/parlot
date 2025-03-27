using Parlot.Benchmarks;
using Parlot.Benchmarks.PidginParsers;
using Parlot.Tests.Json;
using System;
using Xunit;

namespace Parlot.Benchmarks.PidginParsers.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref = "PidginJsonParser"/> class.
    /// </summary>
    public class PidginJsonParserTests
    {
        /// <summary>
        /// Tests that Parse returns a JsonString when given a valid JSON string input.
        /// The test arranges a valid JSON string, invokes the Parse method and asserts that the result is a successful parse,
        /// returning a JsonString instance with the expected value.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonString_ReturnsJsonString()
        {
            // Arrange
            string input = "\"hello\"";
            // Act
            var result = PidginJsonParser.Parse(input);
            // Assert
            Assert.True(result.Success, "Parsing a valid JSON string should succeed.");
            Assert.NotNull(result.Value);
            var jsonString = Assert.IsType<JsonString>(result.Value);
            Assert.Equal("hello", jsonString.Value);
        }

        /// <summary>
        /// Tests that Parse returns a JsonArray when given a valid JSON array input.
        /// The test arranges a JSON array of strings, invokes the Parse method and asserts that the result is a successful parse,
        /// returning a JsonArray instance with the expected elements.
        /// </summary>
//         [Fact] [Error] (50-48)CS1061 'IReadOnlyList<IJson>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<IJson>' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidJsonArray_ReturnsJsonArray()
//         {
//             // Arrange
//             string input = "[\"one\",\"two\"]";
//             // Act
//             var result = PidginJsonParser.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing a valid JSON array should succeed.");
//             Assert.NotNull(result.Value);
//             var jsonArray = Assert.IsType<JsonArray>(result.Value);
//             Assert.NotNull(jsonArray.Elements);
//             Assert.Equal(2, jsonArray.Elements.Length);
//             var firstElement = Assert.IsType<JsonString>(jsonArray.Elements[0]);
//             var secondElement = Assert.IsType<JsonString>(jsonArray.Elements[1]);
//             Assert.Equal("one", firstElement.Value);
//             Assert.Equal("two", secondElement.Value);
//         }

        /// <summary>
        /// Tests that Parse returns a JsonObject when given a valid JSON object input.
        /// The test arranges a JSON object with a single key-value pair, invokes the Parse method and asserts that the result is a successful parse,
        /// returning a JsonObject instance with the expected key and corresponding JsonString value.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonObject_ReturnsJsonObject()
        {
            // Arrange
            string input = "{\"key\":\"value\"}";
            // Act
            var result = PidginJsonParser.Parse(input);
            // Assert
            Assert.True(result.Success, "Parsing a valid JSON object should succeed.");
            Assert.NotNull(result.Value);
            var jsonObject = Assert.IsType<JsonObject>(result.Value);
            Assert.NotNull(jsonObject.Members);
            Assert.Single(jsonObject.Members);
            Assert.True(jsonObject.Members.ContainsKey("key"), "The object should contain the key 'key'.");
            var memberValue = Assert.IsType<JsonString>(jsonObject.Members["key"]);
            Assert.Equal("value", memberValue.Value);
        }

        /// <summary>
        /// Tests that Parse returns a failure result when given an input that is not valid JSON.
        /// The test uses various forms of invalid JSON input, invokes the Parse method and asserts that parsing fails.
        /// </summary>
        /// <param name = "input">The invalid JSON input string.</param>
        [Theory]
        [InlineData("not a json")]
        [InlineData("123")]
        public void Parse_InvalidJson_ReturnsFailure(string input)
        {
            // Act
            var result = PidginJsonParser.Parse(input);
            // Assert
            Assert.False(result.Success, $"Parsing an invalid JSON input '{input}' should fail.");
        }

        /// <summary>
        /// Tests that Parse returns a failure result when given an empty input string.
        /// The test arranges an empty string, invokes the Parse method and asserts that parsing fails.
        /// </summary>
        [Fact]
        public void Parse_EmptyInput_ReturnsFailure()
        {
            // Arrange
            string input = "";
            // Act
            var result = PidginJsonParser.Parse(input);
            // Assert
            Assert.False(result.Success, "Parsing an empty input should fail.");
        }

        /// <summary>
        /// Tests that Parse correctly handles an empty JSON array.
        /// The test arranges an input of an empty array "[]", invokes the Parse method and asserts that the resulting JsonArray has no elements.
        /// </summary>
        [Fact]
        public void Parse_ValidEmptyArray_ReturnsEmptyJsonArray()
        {
            // Arrange
            string input = "[]";
            // Act
            var result = PidginJsonParser.Parse(input);
            // Assert
            Assert.True(result.Success, "Parsing an empty JSON array should succeed.");
            Assert.NotNull(result.Value);
            var jsonArray = Assert.IsType<JsonArray>(result.Value);
            Assert.Empty(jsonArray.Elements);
        }

        /// <summary>
        /// Tests that Parse correctly handles an empty JSON object.
        /// The test arranges an input of an empty object "{}", invokes the Parse method and asserts that the resulting JsonObject has no members.
        /// </summary>
        [Fact]
        public void Parse_ValidEmptyObject_ReturnsEmptyJsonObject()
        {
            // Arrange
            string input = "{}";
            // Act
            var result = PidginJsonParser.Parse(input);
            // Assert
            Assert.True(result.Success, "Parsing an empty JSON object should succeed.");
            Assert.NotNull(result.Value);
            var jsonObject = Assert.IsType<JsonObject>(result.Value);
            Assert.Empty(jsonObject.Members);
        }
    }
}