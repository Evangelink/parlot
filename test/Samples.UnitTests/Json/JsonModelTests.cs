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
        /// Tests that the ToString method returns the correct string representation for a JsonArray with valid elements.
        /// </summary>
        [Fact]
        public void ToString_WithValidElements_ReturnsCorrectRepresentation()
        {
            // Arrange
            var elements = new List<IJson>
            {
                new JsonString("Hello"),
                new JsonString("World")
            };
            var jsonArray = new JsonArray(elements);

            // Act
            string result = jsonArray.ToString();

            // Assert
            string expected = "[\"Hello\",\"World\"]";
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that the ToString method throws a NullReferenceException when JsonArray is constructed with null elements.
        /// </summary>
        [Fact]
        public void ToString_WithNullElements_ThrowsNullReferenceException()
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
        /// Tests that the ToString method returns the correct string representation for a JsonObject with valid members.
        /// </summary>
        [Fact]
        public void ToString_WithValidMembers_ReturnsCorrectRepresentation()
        {
            // Arrange
            var members = new Dictionary<string, IJson>
            {
                { "a", new JsonString("hello") },
                { "b", new JsonString("world") }
            };
            var jsonObject = new JsonObject(members);

            // Act
            string result = jsonObject.ToString();

            // Assert
            // Dictionary preserves insertion order in .NET Core so the expected output matches the insertion order.
            string expected = "{\"a\":\"hello\",\"b\":\"world\"}";
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that the ToString method throws a NullReferenceException when JsonObject is constructed with null members.
        /// </summary>
        [Fact]
        public void ToString_WithNullMembers_ThrowsNullReferenceException()
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
        /// Tests that the ToString method returns the correct string representation for a simple JsonString.
        /// </summary>
        [Fact]
        public void ToString_WithRegularString_ReturnsCorrectRepresentation()
        {
            // Arrange
            var jsonString = new JsonString("test");

            // Act
            string result = jsonString.ToString();

            // Assert
            string expected = "\"test\"";
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that the ToString method returns the correct string representation when the value contains quotes.
        /// </summary>
        [Fact]
        public void ToString_WithQuotesInString_ReturnsCorrectRepresentation()
        {
            // Arrange
            var value = "He said, \"Hello\"";
            var jsonString = new JsonString(value);

            // Act
            string result = jsonString.ToString();

            // Assert
            string expected = $"\"{value}\"";
            Assert.Equal(expected, result);
        }
    }
}
