using System;
using System.Collections.Generic;
using System.Linq;
using Parlot.Tests.Json;
using Xunit;

namespace Parlot.Tests.Json.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="JsonArray"/> class.
    /// </summary>
    public class JsonArrayTests
    {
        /// <summary>
        /// Tests that the constructor assigns the Elements property properly.
        /// </summary>
        [Fact]
        public void Constructor_AssignsElements_WhenListProvided()
        {
            // Arrange
            IReadOnlyList<IJson> expectedElements = new List<IJson>
            {
                new JsonString("value1"),
                new JsonString("value2")
            };

            // Act
            var jsonArray = new JsonArray(expectedElements);

            // Assert
            Assert.Equal(expectedElements, jsonArray.Elements);
        }

        /// <summary>
        /// Tests the ToString method with an empty list of elements.
        /// </summary>
        [Fact]
        public void ToString_EmptyElements_ReturnsEmptyArrayRepresentation()
        {
            // Arrange
            var jsonArray = new JsonArray(new List<IJson>());

            // Act
            string result = jsonArray.ToString();

            // Assert
            Assert.Equal("[]", result);
        }

        /// <summary>
        /// Tests the ToString method with a single element in the array.
        /// </summary>
        [Fact]
        public void ToString_SingleElement_ReturnsCorrectRepresentation()
        {
            // Arrange
            var jsonElement = new JsonString("test");
            var jsonArray = new JsonArray(new List<IJson> { jsonElement });
            string expected = $"[{jsonElement.ToString()}]";

            // Act
            string result = jsonArray.ToString();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the ToString method when the Elements property is null, expecting a NullReferenceException.
        /// </summary>
        [Fact]
        public void ToString_NullElements_ThrowsNullReferenceException()
        {
            // Arrange
            var jsonArray = new JsonArray(null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => jsonArray.ToString());
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="JsonObject"/> class.
    /// </summary>
    public class JsonObjectTests
    {
        /// <summary>
        /// Tests that the constructor assigns the Members property correctly.
        /// </summary>
        [Fact]
        public void Constructor_AssignsMembers_WhenDictionaryProvided()
        {
            // Arrange
            IDictionary<string, IJson> expectedMembers = new Dictionary<string, IJson>
            {
                { "key1", new JsonString("value1") },
                { "key2", new JsonString("value2") }
            };

            // Act
            var jsonObject = new JsonObject(expectedMembers);

            // Assert
            Assert.Equal(expectedMembers, jsonObject.Members);
        }

        /// <summary>
        /// Tests the ToString method with an empty dictionary of members.
        /// </summary>
        [Fact]
        public void ToString_EmptyMembers_ReturnsEmptyObjectRepresentation()
        {
            // Arrange
            var jsonObject = new JsonObject(new Dictionary<string, IJson>());

            // Act
            string result = jsonObject.ToString();

            // Assert
            Assert.Equal("{}", result);
        }

        /// <summary>
        /// Tests the ToString method with a single member in the object.
        /// </summary>
        [Fact]
        public void ToString_SingleMember_ReturnsCorrectRepresentation()
        {
            // Arrange
            var memberValue = new JsonString("value");
            var members = new Dictionary<string, IJson>
            {
                { "key", memberValue }
            };
            var jsonObject = new JsonObject(members);
            string expected = $"{{\"key\":{memberValue.ToString()}}}";

            // Act
            string result = jsonObject.ToString();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the ToString method when the Members property is null, expecting a NullReferenceException.
        /// </summary>
        [Fact]
        public void ToString_NullMembers_ThrowsNullReferenceException()
        {
            // Arrange
            var jsonObject = new JsonObject(null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => jsonObject.ToString());
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="JsonString"/> class.
    /// </summary>
    public class JsonStringTests
    {
        /// <summary>
        /// Tests that the constructor assigns the Value property correctly.
        /// </summary>
        [Fact]
        public void Constructor_AssignsValue_WhenValueProvided()
        {
            // Arrange
            string expectedValue = "test";

            // Act
            var jsonString = new JsonString(expectedValue);

            // Assert
            Assert.Equal(expectedValue, jsonString.Value);
        }

        /// <summary>
        /// Tests the ToString method with a normal string value.
        /// </summary>
        [Fact]
        public void ToString_WithValue_ReturnsQuotedString()
        {
            // Arrange
            string value = "hello";
            var jsonString = new JsonString(value);
            string expected = $"\"{value}\"";

            // Act
            string result = jsonString.ToString();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the ToString method with an empty string value.
        /// </summary>
        [Fact]
        public void ToString_EmptyValue_ReturnsEmptyQuotes()
        {
            // Arrange
            var jsonString = new JsonString(string.Empty);
            string expected = "\"\"";

            // Act
            string result = jsonString.ToString();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the ToString method with a null value, expecting it to return empty quotes.
        /// </summary>
        [Fact]
        public void ToString_NullValue_ReturnsQuotedEmptyString()
        {
            // Arrange
            var jsonString = new JsonString(null);
            string expected = "\"\"";

            // Act
            string result = jsonString.ToString();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
