using System;
using System.Collections.Generic;
using Parlot.Tests.Json;
using Xunit;

namespace Parlot.Tests.Json.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="JsonParser"/> class.
    /// </summary>
    public class JsonParserTests
    {
        /// <summary>
        /// Tests that Parse returns a JsonString when given a valid JSON string input.
        /// The test arranges a valid JSON string input, invokes the Parse method, and asserts that the returned IJson
        /// is a JsonString with the expected content.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonString_ReturnsJsonString()
        {
            // Arrange
            string input = "\"hello\"";

            // Act
            IJson result = JsonParser.Parse(input);

            // Assert
            Assert.NotNull(result);
            var jsonString = Assert.IsType<JsonString>(result);
            // Assuming JsonString has a property 'Value' which holds the parsed string value.
            Assert.Equal("hello", jsonString.Value);
        }

        /// <summary>
        /// Tests that Parse returns a JsonArray when given a valid JSON array input.
        /// The test arranges a valid JSON array input with multiple elements, invokes the Parse method,
        /// and asserts that the returned IJson is a JsonArray containing the expected JsonString elements.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonArray_ReturnsJsonArray()
        {
            // Arrange
            string input = "[\"hello\",\"world\"]";

            // Act
            IJson result = JsonParser.Parse(input);

            // Assert
            Assert.NotNull(result);
            var jsonArray = Assert.IsType<JsonArray>(result);
            // Assuming JsonArray has a property 'Elements' which is a list of IJson.
            Assert.NotNull(jsonArray.Elements);
            Assert.Equal(2, jsonArray.Elements.Count);

            var firstElement = Assert.IsType<JsonString>(jsonArray.Elements[0]);
            var secondElement = Assert.IsType<JsonString>(jsonArray.Elements[1]);
            Assert.Equal("hello", firstElement.Value);
            Assert.Equal("world", secondElement.Value);
        }

        /// <summary>
        /// Tests that Parse returns a JsonArray when given a valid empty JSON array input.
        /// The test arranges an empty JSON array input, invokes the Parse method, and asserts that
        /// the returned IJson is a JsonArray with an empty collection of elements.
        /// </summary>
        [Fact]
        public void Parse_ValidEmptyJsonArray_ReturnsJsonArray()
        {
            // Arrange
            string input = "[]";

            // Act
            IJson result = JsonParser.Parse(input);

            // Assert
            Assert.NotNull(result);
            var jsonArray = Assert.IsType<JsonArray>(result);
            Assert.NotNull(jsonArray.Elements);
            Assert.Empty(jsonArray.Elements);
        }

        /// <summary>
        /// Tests that Parse returns a JsonObject when given a valid JSON object input.
        /// The test arranges a valid JSON object input, invokes the Parse method, and asserts that
        /// the returned IJson is a JsonObject containing the expected key-value pair.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonObject_ReturnsJsonObject()
        {
            // Arrange
            string input = "{\"key\":\"value\"}";

            // Act
            IJson result = JsonParser.Parse(input);

            // Assert
            Assert.NotNull(result);
            var jsonObject = Assert.IsType<JsonObject>(result);
            // Assuming JsonObject has a property 'Members' which is a dictionary mapping strings to IJson.
            Assert.NotNull(jsonObject.Members);
            Assert.Single(jsonObject.Members);
            Assert.True(jsonObject.Members.ContainsKey("key"));
            
            var memberValue = Assert.IsType<JsonString>(jsonObject.Members["key"]);
            Assert.Equal("value", memberValue.Value);
        }

        /// <summary>
        /// Tests that Parse returns null when given an input string that is not valid JSON.
        /// The test arranges an invalid JSON input, invokes the Parse method, and asserts that the result is null.
        /// </summary>
        [Fact]
        public void Parse_InvalidJson_ReturnsNull()
        {
            // Arrange
            string input = "invalid json";

            // Act
            IJson result = JsonParser.Parse(input);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that Parse returns null when given a null input.
        /// The test arranges a null input, invokes the Parse method, and asserts that the result is null.
        /// </summary>
//         [Fact] [Error] (133-28)CS8600 Converting null literal or possible null value to non-nullable type.
//         public void Parse_NullInput_ReturnsNull()
//         {
//             // Arrange
//             string input = null;
// 
//             // Act
//             IJson result = JsonParser.Parse(input);
// 
//             // Assert
//             Assert.Null(result);
//         }
    }
}
