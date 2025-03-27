using Moq;
using Parlot.Tests.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "JsonArray"/> class.
/// </summary>
public class JsonArrayTests
{
    /// <summary>
    /// Tests that the ToString method returns "[]" when the JsonArray contains no elements.
    /// </summary>
    [Fact]
    public void ToString_WithEmptyElements_ReturnsEmptyArrayRepresentation()
    {
        // Arrange
        var emptyElements = new List<IJson>();
        var jsonArray = new JsonArray(emptyElements);
        // Act
        string result = jsonArray.ToString();
        // Assert
        Assert.Equal("[]", result);
    }

    /// <summary>
    /// Tests that the ToString method returns a correctly formatted string when the JsonArray contains multiple elements.
    /// </summary>
    [Fact]
    public void ToString_WithMultipleElements_ReturnsCommaSeparatedElements()
    {
        // Arrange
        IJson element1 = new JsonString("one");
        IJson element2 = new JsonString("two");
        var elements = new List<IJson>
        {
            element1,
            element2
        };
        var jsonArray = new JsonArray(elements);
        string expected = $"[{element1.ToString()},{element2.ToString()}]";
        // Act
        string result = jsonArray.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that the ToString method throws a NullReferenceException when one of the elements is null.
    /// </summary>
//     [Fact] [Error] (61-13)CS8625 Cannot convert null literal to non-nullable reference type.
//     public void ToString_WithNullElementInElements_ThrowsNullReferenceException()
//     {
//         // Arrange
//         IJson validElement = new JsonString("value");
//         var elements = new List<IJson>
//         {
//             validElement,
//             null
//         };
//         var jsonArray = new JsonArray(elements);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => jsonArray.ToString());
//     }

    /// <summary>
    /// Tests the ToString method of JsonArray when the elements list is empty.
    /// Expected outcome: "[]" is returned.
    /// </summary>
    [Fact]
    public void ToString_EmptyElements_ReturnsEmptyJsonArray()
    {
        // Arrange
        var elements = new List<IJson>();
        var jsonArray = new JsonArray(elements);
        // Act
        string result = jsonArray.ToString();
        // Assert
        Assert.Equal("[]", result);
    }

    /// <summary>
    /// Tests the ToString method of JsonArray when the elements list contains a single JsonString.
    /// Expected outcome: JsonArray string representation with one element is returned.
    /// </summary>
    [Fact]
    public void ToString_SingleElement_ReturnsJsonArrayWithOneElement()
    {
        // Arrange
        var jsonString = new JsonString("test");
        var elements = new List<IJson>
        {
            jsonString
        };
        var jsonArray = new JsonArray(elements);
        string expected = $"[{jsonString.ToString()}]";
        // Act
        string result = jsonArray.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the ToString method of JsonArray when the elements list contains multiple Json elements.
    /// Expected outcome: JsonArray string representation with all elements is returned in order.
    /// </summary>
    [Fact]
    public void ToString_MultipleElements_ReturnsJsonArrayWithAllElements()
    {
        // Arrange
        var jsonString1 = new JsonString("first");
        var jsonString2 = new JsonString("second");
        var elements = new List<IJson>
        {
            jsonString1,
            jsonString2
        };
        var jsonArray = new JsonArray(elements);
        string expected = $"[{jsonString1.ToString()},{jsonString2.ToString()}]";
        // Act
        string result = jsonArray.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that ToString returns "[]" when the JsonArray is instantiated with an empty list.
    /// </summary>
    [Fact]
    public void ToString_EmptyArray_ReturnsEmptyBrackets()
    {
        // Arrange
        var elements = new List<IJson>();
        var jsonArray = new JsonArray(elements);
        // Act
        string result = jsonArray.ToString();
        // Assert
        Assert.Equal("[]", result);
    }

    /// <summary>
    /// Tests that ToString returns a correctly formatted string when the JsonArray contains a single JsonString element.
    /// </summary>
    [Fact]
    public void ToString_SingleElement_ReturnsFormattedArray()
    {
        // Arrange
        var element = new JsonString("test");
        var elements = new List<IJson>
        {
            element
        };
        var jsonArray = new JsonArray(elements);
        string expected = $"[{element.ToString()}]";
        // Act
        string result = jsonArray.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests that ToString returns a correctly formatted string when the JsonArray contains multiple elements.
    /// </summary>
    [Fact]
    public void ToString_MultipleElements_ReturnsFormattedArray()
    {
        // Arrange
        var element1 = new JsonString("one");
        var element2 = new JsonString("two");
        var elements = new List<IJson>
        {
            element1,
            element2
        };
        var jsonArray = new JsonArray(elements);
        string expected = $"[{element1.ToString()},{element2.ToString()}]";
        // Act
        string result = jsonArray.ToString();
        // Assert
        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Tests the JsonArray constructor with an empty list to ensure that the Elements property is set to an empty list.
    /// </summary>
    [Fact]
    public void JsonArray_WithEmptyList_SetsElementsPropertyToEmptyList()
    {
        // Arrange
        IReadOnlyList<IJson> elements = new List<IJson>();
        // Act
        var jsonArray = new JsonArray(elements);
        // Assert
        Assert.NotNull(jsonArray.Elements);
        Assert.Empty(jsonArray.Elements);
    }

    /// <summary>
    /// Tests the JsonArray constructor with a valid non-empty list to verify that the Elements property is assigned correctly.
    /// </summary>
    [Fact]
    public void JsonArray_WithValidList_SetsElementsPropertyCorrectly()
    {
        // Arrange
        IJson jsonString = new JsonString("test");
        IReadOnlyList<IJson> elements = new List<IJson>
        {
            jsonString
        };
        // Act
        var jsonArray = new JsonArray(elements);
        // Assert
        Assert.NotNull(jsonArray.Elements);
        Assert.Single(jsonArray.Elements);
        Assert.Same(jsonString, jsonArray.Elements[0]);
    }

    /// <summary>
    /// Tests the JsonArray constructor with a null list argument.
    /// This test verifies that the Elements property is assigned null and that calling ToString() results in a NullReferenceException.
    /// </summary>
//     [Fact] [Error] (228-41)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void JsonArray_WithNullElements_AssignsNullAndToStringThrowsNullReferenceException()
//     {
//         // Arrange
//         IReadOnlyList<IJson> elements = null;
//         // Act
//         var jsonArray = new JsonArray(elements);
//         // Assert
//         Assert.Null(jsonArray.Elements);
//         Assert.Throws<NullReferenceException>(() => jsonArray.ToString());
//     }

    /// <summary>
    /// Tests that the constructor of JsonObject properly initializes the Members property when provided with a non-empty dictionary.
    /// Arrange: A dictionary with valid entries is created.
    /// Act: A JsonObject is instantiated using the dictionary.
    /// Assert: The Members property of the JsonObject matches the provided dictionary.
    /// </summary>
    [Fact]
    public void Constructor_WithNonEmptyMembers_SetsMembersProperty()
    {
        // Arrange
        var expectedMembers = new Dictionary<string, IJson>
        {
            {
                "key1",
                new JsonString("value1")
            },
            {
                "key2",
                new JsonString("value2")
            }
        };
        // Act
        var jsonObject = new JsonObject(expectedMembers);
        // Assert
        Assert.Equal(expectedMembers, jsonObject.Members);
    }

    /// <summary>
    /// Tests that the constructor of JsonObject properly initializes the Members property when provided with an empty dictionary.
    /// Arrange: An empty dictionary is created.
    /// Act: A JsonObject is instantiated using the empty dictionary.
    /// Assert: The Members property of the JsonObject is an empty dictionary.
    /// </summary>
    [Fact]
    public void Constructor_WithEmptyMembers_SetsMembersPropertyToEmptyDictionary()
    {
        // Arrange
        var expectedMembers = new Dictionary<string, IJson>();
        // Act
        var jsonObject = new JsonObject(expectedMembers);
        // Assert
        Assert.NotNull(jsonObject.Members);
        Assert.Empty(jsonObject.Members);
    }

    /// <summary>
    /// Tests that the constructor of JsonObject assigns a null Members property when a null dictionary is provided.
    /// Arrange: A null dictionary is used.
    /// Act: A JsonObject is instantiated using the null dictionary.
    /// Assert: The Members property of the JsonObject is null.
    /// </summary>
//     [Fact] [Error] (291-54)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Constructor_WithNullMembers_SetsMembersPropertyToNull()
//     {
//         // Arrange
//         IDictionary<string, IJson> expectedMembers = null;
//         // Act
//         var jsonObject = new JsonObject(expectedMembers);
//         // Assert
//         Assert.Null(jsonObject.Members);
//     }

    /// <summary>
    /// Tests the JsonString constructor with a valid non-empty string.
    /// Verifies that the Value property is correctly initialized and ToString returns the expected formatted string.
    /// </summary>
    [Fact]
    public void JsonString_ValidNonEmptyString_InitializesValueProperly()
    {
        // Arrange
        string expectedValue = "Hello, World!";
        string expectedToString = $"\"{expectedValue}\"";
        // Act
        var jsonString = new JsonString(expectedValue);
        // Assert
        Assert.Equal(expectedValue, jsonString.Value);
        Assert.Equal(expectedToString, jsonString.ToString());
    }

    /// <summary>
    /// Tests the JsonString constructor with an empty string.
    /// Verifies that the Value property is set to an empty string and that ToString returns the proper formatted representation.
    /// </summary>
    [Fact]
    public void JsonString_EmptyString_InitializesValueProperly()
    {
        // Arrange
        string expectedValue = string.Empty;
        string expectedToString = $"\"{expectedValue}\"";
        // Act
        var jsonString = new JsonString(expectedValue);
        // Assert
        Assert.Equal(expectedValue, jsonString.Value);
        Assert.Equal(expectedToString, jsonString.ToString());
    }

    /// <summary>
    /// Tests the JsonString constructor with a null value.
    /// Verifies that the Value property is null and that ToString returns the correct formatted output, handling null appropriately.
    /// </summary>
//     [Fact] [Error] (340-32)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void JsonString_NullValue_InitializesValueProperly()
//     {
//         // Arrange
//         string expectedValue = null;
//         // When Value is null, string interpolation in ToString yields an empty string within quotes.
//         string expectedToString = "\"\"";
//         // Act
//         var jsonString = new JsonString(expectedValue);
//         // Assert
//         Assert.Null(jsonString.Value);
//         Assert.Equal(expectedToString, jsonString.ToString());
//     }

    /// <summary>
    /// Tests that the Elements property returns the exact list that was provided to the constructor.
    /// This verifies the happy path where a non-empty list is used.
    /// </summary>
    [Fact]
    public void Elements_Getter_WithNonEmptyList_ReturnsSameList()
    {
        // Arrange
        var jsonString = new JsonString("test");
        var elements = new List<IJson>
        {
            jsonString
        };
        // Act
        var jsonArray = new JsonArray(elements);
        // Assert
        Assert.NotNull(jsonArray.Elements);
        Assert.Equal(elements, jsonArray.Elements);
    }

    /// <summary>
    /// Tests that the Elements property returns an empty list when an empty list is provided.
    /// This verifies the edge case of initialization with an empty list.
    /// </summary>
    [Fact]
    public void Elements_Getter_WithEmptyList_ReturnsEmptyList()
    {
        // Arrange
        var emptyList = new List<IJson>();
        // Act
        var jsonArray = new JsonArray(emptyList);
        // Assert
        Assert.NotNull(jsonArray.Elements);
        Assert.Empty(jsonArray.Elements);
    }

    /// <summary>
    /// Tests that constructing a JsonArray with a null list for Elements throws an ArgumentNullException.
    /// This verifies the exceptional scenario of invalid input.
    /// </summary>
//     [Fact] [Error] (394-32)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Constructor_NullElements_ThrowsArgumentNullException()
//     {
//         // Arrange
//         List<IJson> nullList = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new JsonArray(nullList));
//     }

    /// <summary>
    /// Tests that the Members property returns the dictionary provided to the constructor.
    /// This happy path test ensures that the Members property correctly reflects the input data.
    /// </summary>
    [Fact]
    public void Members_Property_WithValidDictionary_ReturnsSameDictionary()
    {
        // Arrange
        var expectedMembers = new Dictionary<string, IJson>
        {
            {
                "key1",
                new JsonString("value1")
            },
            {
                "key2",
                new JsonString("value2")
            }
        };
        var jsonObject = new JsonObject(expectedMembers);
        // Act
        var actualMembers = jsonObject.Members;
        // Assert
        Assert.Same(expectedMembers, actualMembers);
    }

    /// <summary>
    /// Tests that the Members property returns an empty dictionary when an empty dictionary is provided.
    /// This test covers the edge case where no members are present.
    /// </summary>
    [Fact]
    public void Members_Property_WithEmptyDictionary_ReturnsEmptyDictionary()
    {
        // Arrange
        var expectedMembers = new Dictionary<string, IJson>();
        var jsonObject = new JsonObject(expectedMembers);
        // Act
        var actualMembers = jsonObject.Members;
        // Assert
        Assert.Empty(actualMembers);
    }

    /// <summary>
    /// Tests that the constructor of JsonObject throws an ArgumentNullException when null is passed as the members parameter.
    /// This test ensures the class properly guards against invalid input for the Members property.
    /// </summary>
    [Fact]
    public void Constructor_WithNullMembers_ThrowsArgumentNullException()
    {
        // Arrange, Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new JsonObject(null));
        Assert.Equal("members", exception.ParamName);
    }

    /// <summary>
    /// Tests that the 'Value' property returns the correct non-null string when initialized with a valid string.
    /// </summary>
    [Fact]
    public void Value_WhenInitializedWithNonNullString_ReturnsCorrectValue()
    {
        // Arrange
        string expectedValue = "SampleText";
        var jsonString = new JsonString(expectedValue);
        // Act
        string actualValue = jsonString.Value;
        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    /// <summary>
    /// Tests that the 'Value' property returns null when initialized with a null string.
    /// </summary>
//     [Fact] [Error] (475-32)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Value_WhenInitializedWithNull_ReturnsNull()
//     {
//         // Arrange
//         string expectedValue = null;
//         var jsonString = new JsonString(expectedValue);
//         // Act
//         string actualValue = jsonString.Value;
//         // Assert
//         Assert.Null(actualValue);
//     }
}