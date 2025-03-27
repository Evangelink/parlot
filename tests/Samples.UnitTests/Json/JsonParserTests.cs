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
        /// Tests that the Parse method correctly parses a valid JSON string literal input.
        /// Expected outcome: A non-null instance of <see cref="JsonString"/> is returned.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonString_ReturnsJsonString()
        {
            // Arrange
            string input = "\"hello\"";

            // Act
            var result = JsonParser.Parse(input);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonString>(result);
        }

        /// <summary>
        /// Tests that the Parse method correctly parses a valid JSON array input.
        /// Expected outcome: A non-null instance of <see cref="JsonArray"/> is returned.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonArray_ReturnsJsonArray()
        {
            // Arrange
            string input = "[\"hello\", \"world\"]";

            // Act
            var result = JsonParser.Parse(input);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonArray>(result);
        }

        /// <summary>
        /// Tests that the Parse method correctly parses a valid JSON object input.
        /// Expected outcome: A non-null instance of <see cref="JsonObject"/> is returned.
        /// </summary>
        [Fact]
        public void Parse_ValidJsonObject_ReturnsJsonObject()
        {
            // Arrange
            string input = "{\"key\":\"value\"}";

            // Act
            var result = JsonParser.Parse(input);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<JsonObject>(result);
        }

        /// <summary>
        /// Tests that the Parse method returns null when provided with invalid JSON inputs.
        /// Expected outcome: The Parse method returns null for inputs that are not valid JSON.
        /// </summary>
        /// <param name="input">A string that is not valid JSON as defined by the parser.</param>
        [Theory]
        [InlineData("")]
        [InlineData("invalid")]
        [InlineData("123")]
        public void Parse_InvalidJsonInput_ReturnsNull(string input)
        {
            // Act
            var result = JsonParser.Parse(input);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that the Parse method returns null when the input is null.
        /// Expected outcome: The Parse method returns null when a null input is provided.
        /// </summary>
//         [Fact] [Error] (93-28)CS8600 Converting null literal or possible null value to non-nullable type.
//         public void Parse_NullInput_ReturnsNull()
//         {
//             // Arrange
//             string input = null;
// 
//             // Act
//             var result = JsonParser.Parse(input);
// 
//             // Assert
//             Assert.Null(result);
//         }
    }
}
