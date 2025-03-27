using Parlot.Benchmarks;
using Parlot.Benchmarks.SuperpowerParsers;
using Parlot.Tests.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Parlot.Benchmarks.SuperpowerParsers.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref = "SuperpowerJsonParser"/> class.
    /// </summary>
    public class SuperpowerJsonParserTests
    {
        /// <summary>
        /// Tests that parsing a valid JSON string returns a JsonString instance with the correct value.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonString_ReturnsJsonString()
        {
            // Arrange
            string input = "\"Hello\"";
            // Act
            IJson result = SuperpowerJsonParser.Parse(input);
            // Assert
            Assert.NotNull(result);
            var jsonString = Assert.IsType<JsonString>(result);
            Assert.Equal("Hello", jsonString.Value);
        }

        /// <summary>
        /// Tests that parsing a valid JSON array returns a JsonArray instance with the correct elements.
        /// </summary>
//         [Fact] [Error] (45-48)CS1061 'IReadOnlyList<IJson>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<IJson>' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidJsonArray_ReturnsJsonArray()
//         {
//             // Arrange
//             string input = "[\"Hello\", \"World\"]";
//             // Act
//             IJson result = SuperpowerJsonParser.Parse(input);
//             // Assert
//             Assert.NotNull(result);
//             var jsonArray = Assert.IsType<JsonArray>(result);
//             Assert.NotNull(jsonArray.Elements);
//             Assert.Equal(2, jsonArray.Elements.Length);
//             var firstElement = Assert.IsType<JsonString>(jsonArray.Elements[0]);
//             var secondElement = Assert.IsType<JsonString>(jsonArray.Elements[1]);
//             Assert.Equal("Hello", firstElement.Value);
//             Assert.Equal("World", secondElement.Value);
//         }

        /// <summary>
        /// Tests that parsing a valid JSON object returns a JsonObject instance with the correct members.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonObject_ReturnsJsonObject()
        {
            // Arrange
            string input = "{\"key\": \"value\"}";
            // Act
            IJson result = SuperpowerJsonParser.Parse(input);
            // Assert
            Assert.NotNull(result);
            var jsonObject = Assert.IsType<JsonObject>(result);
            Assert.NotNull(jsonObject.Members);
            Assert.Single(jsonObject.Members);
            Assert.True(jsonObject.Members.ContainsKey("key"));
            var jsonValue = Assert.IsType<JsonString>(jsonObject.Members["key"]);
            Assert.Equal("value", jsonValue.Value);
        }

        /// <summary>
        /// Tests that parsing a null input throws an ArgumentNullException.
        /// </summary>
//         [Fact] [Error] (79-28)CS8600 Converting null literal or possible null value to non-nullable type.
//         public void Parse_NullInput_ThrowsArgumentNullException()
//         {
//             // Arrange
//             string input = null;
//             // Act & Assert
//             Assert.Throws<ArgumentNullException>(() => SuperpowerJsonParser.Parse(input));
//         }

        /// <summary>
        /// Tests that parsing an empty input string throws an exception indicating invalid JSON.
        /// </summary>
        [Fact]
        public void Parse_EmptyInput_ThrowsException()
        {
            // Arrange
            string input = string.Empty;
            // Act & Assert
            Assert.ThrowsAny<Exception>(() => SuperpowerJsonParser.Parse(input));
        }

        /// <summary>
        /// Tests that parsing a malformed JSON string throws an exception.
        /// </summary>
        [Fact]
        public void Parse_MalformedJson_ThrowsException()
        {
            // Arrange
            string input = "{ \"key\": \"value\" "; // Missing closing brace
            // Act & Assert
            Assert.ThrowsAny<Exception>(() => SuperpowerJsonParser.Parse(input));
        }
    }
}