using Parlot.Benchmarks;
using Parlot.Benchmarks.SpracheParsers;
using Parlot.Tests.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Parlot.Benchmarks.SpracheParsers.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref = "SpracheJsonParser"/> class.
    /// </summary>
    public class SpracheJsonParserTests
    {
        /// <summary>
        /// Tests that Parse returns a successful result containing a JsonString when a valid JSON string is provided.
        /// The test arranges a valid JSON string, invokes the Parse method, and asserts that the returned value is a JsonString with the expected content.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonString_ReturnsJsonString()
        {
            // Arrange
            string input = "\"hello\"";
            // Act
            var result = SpracheJsonParser.Parse(input);
            // Assert
            Assert.True(result.WasSuccessful, "Expected the parsing to be successful for a valid JSON string input.");
            var jsonString = Assert.IsType<JsonString>(result.Value);
            Assert.Equal("hello", jsonString.Value);
        }

        /// <summary>
        /// Tests that Parse returns a successful result containing a JsonArray when a valid JSON array input is provided.
        /// The test arranges a JSON array input, invokes Parse, and asserts that the result is a JsonArray with the correct elements.
        /// </summary>
//         [Fact] [Error] (47-48)CS1061 'IReadOnlyList<IJson>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<IJson>' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidJsonArray_ReturnsJsonArray()
//         {
//             // Arrange
//             string input = "[\"hello\", \"world\"]";
//             // Act
//             var result = SpracheJsonParser.Parse(input);
//             // Assert
//             Assert.True(result.WasSuccessful, "Expected the parsing to be successful for a valid JSON array input.");
//             var jsonArray = Assert.IsType<JsonArray>(result.Value);
//             Assert.NotNull(jsonArray.Elements);
//             Assert.Equal(2, jsonArray.Elements.Length);
//             var firstElement = Assert.IsType<JsonString>(jsonArray.Elements[0]);
//             var secondElement = Assert.IsType<JsonString>(jsonArray.Elements[1]);
//             Assert.Equal("hello", firstElement.Value);
//             Assert.Equal("world", secondElement.Value);
//         }

        /// <summary>
        /// Tests that Parse returns a successful result containing a JsonObject when a valid JSON object input is provided.
        /// The test arranges a JSON object input, invokes Parse, and asserts that the result is a JsonObject with the expected key-value pair.
        /// </summary>
//         [Fact] [Error] (68-39)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (69-38)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (70-36)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (71-70)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidJsonObject_ReturnsJsonObject()
//         {
//             // Arrange
//             string input = "{\"key\":\"value\"}";
//             // Act
//             var result = SpracheJsonParser.Parse(input);
//             // Assert
//             Assert.True(result.WasSuccessful, "Expected the parsing to be successful for a valid JSON object input.");
//             var jsonObject = Assert.IsType<JsonObject>(result.Value);
//             Assert.NotNull(jsonObject.Properties);
//             Assert.Single(jsonObject.Properties);
//             Assert.True(jsonObject.Properties.ContainsKey("key"));
//             var valueProperty = Assert.IsType<JsonString>(jsonObject.Properties["key"]);
//             Assert.Equal("value", valueProperty.Value);
//         }

        /// <summary>
        /// Tests that Parse returns an unsuccessful result when an invalid JSON input is provided.
        /// The test arranges an input that does not conform to any valid JSON structure and asserts that the parsing fails.
        /// </summary>
        [Fact]
        public void Parse_InvalidJson_ReturnsUnsuccessfulResult()
        {
            // Arrange
            string input = "invalid";
            // Act
            var result = SpracheJsonParser.Parse(input);
            // Assert
            Assert.False(result.WasSuccessful, "Expected the parsing to fail for an invalid JSON input.");
        }

        /// <summary>
        /// Tests that Parse returns an unsuccessful result when an empty string is provided as input.
        /// The test arranges an empty input string, invokes Parse, and asserts that the parsing does not succeed.
        /// </summary>
        [Fact]
        public void Parse_EmptyInput_ReturnsUnsuccessfulResult()
        {
            // Arrange
            string input = string.Empty;
            // Act
            var result = SpracheJsonParser.Parse(input);
            // Assert
            Assert.False(result.WasSuccessful, "Expected the parsing to fail for an empty input string.");
        }

        /// <summary>
        /// Tests that Parse throws an ArgumentNullException when null is passed as input.
        /// The test arranges a null input value, invokes Parse, and asserts that an ArgumentNullException is thrown.
        /// </summary>
//         [Fact] [Error] (113-28)CS8600 Converting null literal or possible null value to non-nullable type.
//         public void Parse_NullInput_ThrowsArgumentNullException()
//         {
//             // Arrange
//             string input = null;
//             // Act & Assert
//             Assert.Throws<ArgumentNullException>(() => SpracheJsonParser.Parse(input));
//         }

        /// <summary>
        /// Tests that Parse correctly handles a nested JSON structure containing both an array and an object.
        /// The test arranges a nested JSON string, invokes Parse, and asserts that the returned structure matches the expected nested elements.
        /// </summary>
//         [Fact] [Error] (132-39)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (133-40)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (135-36)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (136-65)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (137-48)CS1061 'IReadOnlyList<IJson>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<IJson>' could be found (are you missing a using directive or an assembly reference?) [Error] (143-36)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (144-69)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (145-41)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (146-40)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (147-38)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?) [Error] (148-70)CS1061 'JsonObject' does not contain a definition for 'Properties' and no accessible extension method 'Properties' accepting a first argument of type 'JsonObject' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_NestedJson_ReturnsCorrectNestedStructure()
//         {
//             // Arrange
//             string input = "{\"array\":[\"a\",\"b\"],\"object\":{\"nested\":\"c\"}}";
//             // Act
//             var result = SpracheJsonParser.Parse(input);
//             // Assert
//             Assert.True(result.WasSuccessful, "Expected the parsing to be successful for a valid nested JSON input.");
//             var jsonObject = Assert.IsType<JsonObject>(result.Value);
//             Assert.NotNull(jsonObject.Properties);
//             Assert.Equal(2, jsonObject.Properties.Count);
//             // Validate the 'array' property
//             Assert.True(jsonObject.Properties.ContainsKey("array"));
//             var jsonArray = Assert.IsType<JsonArray>(jsonObject.Properties["array"]);
//             Assert.Equal(2, jsonArray.Elements.Length);
//             var firstElement = Assert.IsType<JsonString>(jsonArray.Elements[0]);
//             var secondElement = Assert.IsType<JsonString>(jsonArray.Elements[1]);
//             Assert.Equal("a", firstElement.Value);
//             Assert.Equal("b", secondElement.Value);
//             // Validate the 'object' property
//             Assert.True(jsonObject.Properties.ContainsKey("object"));
//             var nestedObject = Assert.IsType<JsonObject>(jsonObject.Properties["object"]);
//             Assert.NotNull(nestedObject.Properties);
//             Assert.Single(nestedObject.Properties);
//             Assert.True(nestedObject.Properties.ContainsKey("nested"));
//             var nestedValue = Assert.IsType<JsonString>(nestedObject.Properties["nested"]);
//             Assert.Equal("c", nestedValue.Value);
//         }
    }
}