// using Moq;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using Parlot.Benchmarks;
// using Parlot.Benchmarks.SpracheParsers;
// using Parlot.Benchmarks.SuperpowerParsers;
// using Parlot.Tests.Json;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using System.Text.Json;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "JsonBench"/> class.
// /// </summary>
// public class JsonBenchTests
// {
//     /// <summary>
//     /// Tests that the Setup method initializes the JSON fields and the compiled parser.
//     /// This test creates an instance of JsonBench, invokes Setup, and uses reflection to verify
//     /// that the private fields _bigJson, _longJson, _wideJson, _deepJson and _compiled are set to non-null values.
//     /// </summary>
// //     [Fact] [Error] (35-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (36-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (37-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (38-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (39-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (46-26)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (47-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (48-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (49-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (50-33)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void Setup_WhenCalled_InitializesAllJsonFieldsAndCompiledParser()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         Type type = typeof(JsonBench);
// //         // Act
// //         jsonBench.Setup();
// //         // Retrieve private fields using reflection
// //         FieldInfo bigJsonField = type.GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         FieldInfo longJsonField = type.GetField("_longJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         FieldInfo wideJsonField = type.GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         FieldInfo deepJsonField = type.GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         FieldInfo compiledField = type.GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         // Assert
// //         Assert.NotNull(bigJsonField);
// //         Assert.NotNull(longJsonField);
// //         Assert.NotNull(wideJsonField);
// //         Assert.NotNull(deepJsonField);
// //         Assert.NotNull(compiledField);
// //         string bigJson = bigJsonField.GetValue(jsonBench) as string;
// //         string longJson = longJsonField.GetValue(jsonBench) as string;
// //         string wideJson = wideJsonField.GetValue(jsonBench) as string;
// //         string deepJson = deepJsonField.GetValue(jsonBench) as string;
// //         object compiledParser = compiledField.GetValue(jsonBench);
// //         Assert.False(string.IsNullOrWhiteSpace(bigJson));
// //         Assert.False(string.IsNullOrWhiteSpace(longJson));
// //         Assert.False(string.IsNullOrWhiteSpace(wideJson));
// //         Assert.False(string.IsNullOrWhiteSpace(deepJson));
// //         Assert.NotNull(compiledParser);
// //     }
// 
//     /// <summary>
//     /// Tests that calling the Setup method twice reinitializes the fields consistently.
//     /// This verifies that repeated calls to Setup result in non-null and valid JSON strings.
//     /// </summary>
// //     [Fact] [Error] (71-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (72-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (73-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (74-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (75-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (76-31)CS8602 Dereference of a possibly null reference. [Error] (76-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (77-32)CS8602 Dereference of a possibly null reference. [Error] (77-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (78-32)CS8602 Dereference of a possibly null reference. [Error] (78-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (79-32)CS8602 Dereference of a possibly null reference. [Error] (79-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (80-32)CS8602 Dereference of a possibly null reference. [Error] (80-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (84-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (85-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (86-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (87-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (88-33)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void Setup_CalledTwice_ReinitializesFieldsConsistently()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         Type type = typeof(JsonBench);
// //         // Act
// //         jsonBench.Setup();
// //         // Capture the initial state of the private fields
// //         FieldInfo bigJsonField = type.GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         FieldInfo longJsonField = type.GetField("_longJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         FieldInfo wideJsonField = type.GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         FieldInfo deepJsonField = type.GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         FieldInfo compiledField = type.GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         string firstBigJson = bigJsonField.GetValue(jsonBench) as string;
// //         string firstLongJson = longJsonField.GetValue(jsonBench) as string;
// //         string firstWideJson = wideJsonField.GetValue(jsonBench) as string;
// //         string firstDeepJson = deepJsonField.GetValue(jsonBench) as string;
// //         object firstCompiled = compiledField.GetValue(jsonBench);
// //         // Call Setup a second time
// //         jsonBench.Setup();
// //         // Capture the state after the second call
// //         string secondBigJson = bigJsonField.GetValue(jsonBench) as string;
// //         string secondLongJson = longJsonField.GetValue(jsonBench) as string;
// //         string secondWideJson = wideJsonField.GetValue(jsonBench) as string;
// //         string secondDeepJson = deepJsonField.GetValue(jsonBench) as string;
// //         object secondCompiled = compiledField.GetValue(jsonBench);
// //         // Assert: Check that after multiple calls, the fields are not null or whitespace
// //         Assert.False(string.IsNullOrWhiteSpace(firstBigJson));
// //         Assert.False(string.IsNullOrWhiteSpace(firstLongJson));
// //         Assert.False(string.IsNullOrWhiteSpace(firstWideJson));
// //         Assert.False(string.IsNullOrWhiteSpace(firstDeepJson));
// //         Assert.NotNull(firstCompiled);
// //         Assert.False(string.IsNullOrWhiteSpace(secondBigJson));
// //         Assert.False(string.IsNullOrWhiteSpace(secondLongJson));
// //         Assert.False(string.IsNullOrWhiteSpace(secondWideJson));
// //         Assert.False(string.IsNullOrWhiteSpace(secondDeepJson));
// //         Assert.NotNull(secondCompiled);
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_ParlotCompiled returns the expected IJson result when provided with valid internal values.
//     /// </summary>
// //     [Fact] [Error] (114-35)CS0246 The type or namespace name 'Parser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (115-58)CS1503 Argument 1: cannot convert from 'JsonBenchTests.DummyJson' to '?' [Error] (117-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void BigJson_ParlotCompiled_HappyPath_ReturnsExpectedResult()
// //     {
// //         // Arrange: create an instance of JsonBench and inject valid values for _bigJson and _compiled.
// //         var jsonBench = new JsonBench();
// //         string testJson = "dummy json";
// //         // Create a dummy result for IJson.
// //         var dummyJsonResult = new DummyJson();
// //         // Create a mock for Parser<IJson> and setup the Parse method to return dummyJsonResult when passed testJson.
// //         var mockParser = new Mock<Parser<IJson>>(MockBehavior.Strict);
// //         mockParser.Setup(p => p.Parse(testJson)).Returns(dummyJsonResult);
// //         // Set the private fields _bigJson and _compiled using reflection.
// //         SetPrivateField(jsonBench, "_bigJson", testJson);
// //         SetPrivateField(jsonBench, "_compiled", mockParser.Object);
// //         // Act
// //         var result = jsonBench.BigJson_ParlotCompiled();
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.Equal(dummyJsonResult, result);
// //         mockParser.Verify(p => p.Parse(testJson), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_ParlotCompiled throws an exception when _bigJson is null and the underlying Parse method reacts accordingly.
//     /// </summary>
// //     [Fact] [Error] (137-35)CS0246 The type or namespace name 'Parser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (140-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void BigJson_ParlotCompiled_NullBigJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange: create an instance of JsonBench and inject null for _bigJson.
// //         var jsonBench = new JsonBench();
// //         string? testJson = null;
// //         // Create a mock for Parser<IJson> and setup the Parse method to throw an ArgumentNullException when passed null.
// //         var mockParser = new Mock<Parser<IJson>>(MockBehavior.Strict);
// //         mockParser.Setup(p => p.Parse(null)).Throws(new ArgumentNullException("json"));
// //         // Set the private fields _bigJson and _compiled using reflection.
// //         SetPrivateField(jsonBench, "_bigJson", testJson);
// //         SetPrivateField(jsonBench, "_compiled", mockParser.Object);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => jsonBench.BigJson_ParlotCompiled());
// //         mockParser.Verify(p => p.Parse(null), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_ParlotCompiled throws a NullReferenceException when _compiled is null.
//     /// </summary>
// //     [Fact] [Error] (157-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)' [Error] (158-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void BigJson_ParlotCompiled_NullCompiled_ThrowsNullReferenceException()
// //     {
// //         // Arrange: create an instance of JsonBench and inject a valid _bigJson but leave _compiled as null.
// //         var jsonBench = new JsonBench();
// //         string testJson = "dummy json";
// //         // Set the private field _bigJson to a valid value and _compiled to null.
// //         SetPrivateField(jsonBench, "_bigJson", testJson);
// //         SetPrivateField(jsonBench, "_compiled", null);
// //         // Act & Assert: Expect a NullReferenceException since _compiled is null.
// //         Assert.Throws<NullReferenceException>(() => jsonBench.BigJson_ParlotCompiled());
// //     }
// 
//     /// <summary>
//     /// Helper method to set a private field's value using reflection.
//     /// </summary>
//     /// <param name = "instance">The instance whose field is to be set.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to assign to the field.</param>
//     private static void SetPrivateField(object instance, string fieldName, object? value)
//     {
//         var fieldInfo = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
//         if (fieldInfo == null)
//         {
//             throw new InvalidOperationException($"Field '{fieldName}' not found on type '{instance.GetType()}'.");
//         }
// 
//         fieldInfo.SetValue(instance, value);
//     }
// 
//     /// <summary>
//     /// A dummy implementation of the IJson interface used for testing.
//     /// </summary>
//     private class DummyJson : IJson
//     {
//         // Override Equals to ensure proper comparison in unit tests.
//         public override bool Equals(object? obj)
//         {
//             return obj is DummyJson;
//         }
// 
//         public override int GetHashCode()
//         {
//             return 1;
//         }
//     }
// 
//     /// <summary>
//     /// Tests that BigJson_Parlot returns a non-null IJson object when provided with a valid JSON string.
//     /// This test sets the private _bigJson field with a valid JSON and calls the method under test.
//     /// The expected outcome is a non-null result which indicates successful parsing.
//     /// </summary>
// //     [Fact] [Error] (208-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (209-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Parlot_ValidJson_ReturnsIJson()
// //     {
// //         // Arrange
// //         var bench = new JsonBench();
// //         string validJson = "{\"key\": \"value\"}";
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         bigJsonField.SetValue(bench, validJson);
// //         // Act
// //         var result = bench.BigJson_Parlot();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_Parlot throws an exception when provided with an invalid JSON string.
//     /// This test sets the private _bigJson field with an invalid JSON string and calls the method under test.
//     /// The expected outcome is that the parsing fails and an exception is thrown.
//     /// </summary>
// //     [Fact] [Error] (227-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (228-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Parlot_InvalidJson_ThrowsException()
// //     {
// //         // Arrange
// //         var bench = new JsonBench();
// //         string invalidJson = "invalid json";
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         bigJsonField.SetValue(bench, invalidJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => bench.BigJson_Parlot());
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_Parlot throws an exception when the JSON string is null.
//     /// This test sets the private _bigJson field to null and calls the method under test.
//     /// The expected outcome is that the method fails to parse null and an exception is thrown.
//     /// </summary>
// //     [Fact] [Error] (243-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (244-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Parlot_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var bench = new JsonBench();
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         bigJsonField.SetValue(bench, null);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => bench.BigJson_Parlot());
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.BigJson_Pidgin"/> method with valid JSON input,
//     /// ensuring that it returns a non-null IJson result.
//     /// </summary>
// //     [Fact] [Error] (259-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (260-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Pidgin_ValidJson_ReturnsParsedResult()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "[]"; // Using an empty JSON array as valid input.
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         bigJsonField.SetValue(jsonBench, validJson);
// //         // Act
// //         var result = jsonBench.BigJson_Pidgin();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.BigJson_Pidgin"/> method with invalid JSON input,
//     /// ensuring that it throws an exception.
//     /// </summary>
// //     [Fact] [Error] (277-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (278-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Pidgin_InvalidJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string invalidJson = "invalid"; // Using an invalid JSON string.
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         bigJsonField.SetValue(jsonBench, invalidJson);
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => jsonBench.BigJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.BigJson_Pidgin"/> method with a null JSON input,
//     /// ensuring that it throws an exception.
//     /// </summary>
// //     [Fact] [Error] (292-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (293-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Pidgin_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         bigJsonField.SetValue(jsonBench, null);
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => jsonBench.BigJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Tests the BigJson_Newtonsoft method when _bigJson contains a valid JSON string.
//     /// Expected: Returns a valid JToken representing the parsed JSON.
//     /// </summary>
// //     [Fact] [Error] (309-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (310-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Newtonsoft_ValidJson_ReturnsJToken()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "{\"key\":\"value\"}";
// //         // Use reflection to set the private field _bigJson in jsonBench instance.
// //         FieldInfo fieldInfo = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fieldInfo.SetValue(jsonBench, validJson);
// //         // Act
// //         JToken result = jsonBench.BigJson_Newtonsoft();
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.Equal("value", result["key"]?.ToString());
// //     }
// 
//     /// <summary>
//     /// Tests the BigJson_Newtonsoft method when _bigJson is null.
//     /// Expected: Throws an ArgumentNullException.
//     /// </summary>
// //     [Fact] [Error] (328-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (329-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Newtonsoft_NullJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Set _bigJson to null via reflection.
// //         FieldInfo fieldInfo = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fieldInfo.SetValue(jsonBench, null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => jsonBench.BigJson_Newtonsoft());
// //     }
// 
//     /// <summary>
//     /// Tests the BigJson_Newtonsoft method when _bigJson contains an invalid JSON string.
//     /// Expected: Throws a JsonReaderException.
//     /// </summary>
// //     [Fact] [Error] (345-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (346-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Newtonsoft_InvalidJson_ThrowsJsonReaderException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string invalidJson = "{ invalid json }";
// //         // Set _bigJson to invalidJson using reflection.
// //         FieldInfo fieldInfo = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fieldInfo.SetValue(jsonBench, invalidJson);
// //         // Act & Assert
// //         Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => jsonBench.BigJson_Newtonsoft());
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_SystemTextJson successfully parses a valid JSON string.
//     /// Arrange: A valid JSON string is injected into the private _bigJson field.
//     /// Act: BigJson_SystemTextJson is executed.
//     /// Assert: The returned JsonDocument is not null and its root element is an object with expected properties.
//     /// </summary>
// //     [Fact] [Error] (363-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (364-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_SystemTextJson_ValidJson_ReturnsParsedJsonDocument()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "{\"key\":\"value\"}";
// //         FieldInfo field = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         field.SetValue(jsonBench, validJson);
// //         // Act
// //         JsonDocument result = jsonBench.BigJson_SystemTextJson();
// //         // Assert
// //         Assert.NotNull(result);
// //         JsonElement rootElement = result.RootElement;
// //         Assert.Equal(JsonValueKind.Object, rootElement.ValueKind);
// //         Assert.True(rootElement.TryGetProperty("key", out JsonElement value));
// //         Assert.Equal("value", value.GetString());
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_SystemTextJson throws a JsonException when an invalid JSON string is provided.
//     /// Arrange: An invalid JSON string is injected into the private _bigJson field.
//     /// Act & Assert: The method call raises a JsonException.
//     /// </summary>
// //     [Fact] [Error] (389-23)CS0104 'JsonException' is an ambiguous reference between 'Newtonsoft.Json.JsonException' and 'System.Text.Json.JsonException' [Error] (386-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (387-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_SystemTextJson_InvalidJson_ThrowsJsonException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string invalidJson = "Not a valid JSON string";
// //         FieldInfo field = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         field.SetValue(jsonBench, invalidJson);
// //         // Act & Assert
// //         Assert.Throws<JsonException>(() => jsonBench.BigJson_SystemTextJson());
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_SystemTextJson throws an ArgumentNullException when a null JSON string is provided.
//     /// Arrange: A null value is injected into the private _bigJson field.
//     /// Act & Assert: The method call results in an ArgumentNullException.
//     /// </summary>
// //     [Fact] [Error] (402-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (403-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_SystemTextJson_NullJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         FieldInfo field = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         field.SetValue(jsonBench, null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => jsonBench.BigJson_SystemTextJson());
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.BigJson_Sprache"/> method with a valid JSON string.
//     /// This test sets the private field "_bigJson" to a simple valid JSON and verifies that
//     /// the method returns a non-null IJson instance.
//     /// </summary>
// //     [Fact] [Error] (419-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (420-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Sprache_ValidJson_ReturnsNonNullIJson()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "{\"key\":\"value\"}";
// //         FieldInfo jsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         jsonField.SetValue(jsonBench, validJson);
// //         // Act
// //         var result = jsonBench.BigJson_Sprache();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.BigJson_Sprache"/> method with an empty JSON string.
//     /// This test sets the private field "_bigJson" to an empty string and verifies that
//     /// calling the method throws an exception.
//     /// </summary>
// //     [Fact] [Error] (438-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (439-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Sprache_EmptyJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string emptyJson = string.Empty;
// //         FieldInfo jsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         jsonField.SetValue(jsonBench, emptyJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.BigJson_Sprache());
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.BigJson_Sprache"/> method with a null JSON string.
//     /// This test sets the private field "_bigJson" to null and verifies that
//     /// calling the method throws an exception.
//     /// </summary>
// //     [Fact] [Error] (454-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (455-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (456-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJson_Sprache_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string nullJson = null;
// //         FieldInfo jsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         jsonField.SetValue(jsonBench, nullJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.BigJson_Sprache());
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_Superpower returns a valid IJson object when _bigJson contains a valid JSON string.
//     /// This test sets the private _bigJson field via reflection, invokes BigJson_Superpower, and verifies the result is not null.
//     /// </summary>
// //     [Fact] [Error] (471-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void BigJson_Superpower_ValidJson_ReturnsIJson()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "{\"key\": \"value\"}";
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(bigJsonField);
// //         bigJsonField.SetValue(jsonBench, validJson);
// //         // Act
// //         IJson result = jsonBench.BigJson_Superpower();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_Superpower throws an exception when _bigJson contains an invalid JSON string.
//     /// This test sets the private _bigJson field to an improperly formatted JSON string and expects an exception.
//     /// </summary>
// //     [Fact] [Error] (491-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void BigJson_Superpower_InvalidJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Missing closing brace
// //         string invalidJson = "{\"key\": \"value\"";
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(bigJsonField);
// //         bigJsonField.SetValue(jsonBench, invalidJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.BigJson_Superpower());
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_Superpower throws an exception when _bigJson is an empty string.
//     /// This test sets the private _bigJson field to an empty string and expects an exception.
//     /// </summary>
// //     [Fact] [Error] (508-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void BigJson_Superpower_EmptyJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string emptyJson = string.Empty;
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(bigJsonField);
// //         bigJsonField.SetValue(jsonBench, emptyJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.BigJson_Superpower());
// //     }
// 
//     /// <summary>
//     /// Tests that BigJson_Superpower throws an exception when _bigJson is null.
//     /// This test sets the private _bigJson field to null and expects an exception.
//     /// </summary>
// //     [Fact] [Error] (524-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (525-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void BigJson_Superpower_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string nullJson = null;
// //         FieldInfo bigJsonField = typeof(JsonBench).GetField("_bigJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(bigJsonField);
// //         bigJsonField.SetValue(jsonBench, nullJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.BigJson_Superpower());
// //     }
// 
//     /// <summary>
//     /// Tests the LongJson_ParlotCompiled method under a valid scenario where _longJson has a valid string and _compiled returns a valid IJson result.
//     /// This test sets up the private dependencies (_longJson and _compiled) using reflection, invokes the method, and verifies that the expected IJson object is returned.
//     /// </summary>
// //     [Fact] [Error] (544-35)CS0246 The type or namespace name 'Parser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (545-83)CS1503 Argument 1: cannot convert from 'JsonBenchTests.DummyJson' to '?' [Error] (547-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (551-35)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void LongJson_ParlotCompiled_HappyPath_ReturnsParsedJson()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string testJson = "{\"key\":\"value\"}";
// //         var expectedIJson = new DummyJson();
// //         // Create a mock for Parser<IJson> and set up the Parse method to return expectedIJson when called with testJson.
// //         var parserMock = new Mock<Parser<IJson>>();
// //         parserMock.Setup(p => p.Parse(It.Is<string>(s => s == testJson))).Returns(expectedIJson);
// //         // Use reflection to set the private _longJson field.
// //         FieldInfo longJsonField = typeof(JsonBench).GetField("_longJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(longJsonField);
// //         longJsonField.SetValue(jsonBench, testJson);
// //         // Use reflection to set the private _compiled field.
// //         FieldInfo compiledField = typeof(JsonBench).GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(compiledField);
// //         compiledField.SetValue(jsonBench, parserMock.Object);
// //         // Act
// //         IJson result = jsonBench.LongJson_ParlotCompiled();
// //         // Assert
// //         Assert.Same(expectedIJson, result);
// //         parserMock.Verify(p => p.Parse(It.Is<string>(s => s == testJson)), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests the LongJson_ParlotCompiled method when the _compiled parser is not initialized (null).
//     /// This test sets _compiled to null using reflection and expects a NullReferenceException when invoking the method.
//     /// </summary>
// //     [Fact] [Error] (572-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (576-35)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void LongJson_ParlotCompiled_NullCompiled_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string testJson = "{\"key\":\"value\"}";
// //         // Set _longJson field to a valid string.
// //         FieldInfo longJsonField = typeof(JsonBench).GetField("_longJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(longJsonField);
// //         longJsonField.SetValue(jsonBench, testJson);
// //         // Set _compiled field to null.
// //         FieldInfo compiledField = typeof(JsonBench).GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(compiledField);
// //         compiledField.SetValue(jsonBench, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => jsonBench.LongJson_ParlotCompiled());
// //     }
// 
//     /// <summary>
//     /// Tests the LongJson_ParlotCompiled method when the _longJson field is null.
//     /// This test sets _longJson to null and configures the parser mock to throw an ArgumentNullException when Parse is called with null.
//     /// It verifies that the exception is thrown as expected.
//     /// </summary>
// //     [Fact] [Error] (594-35)CS0246 The type or namespace name 'Parser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (597-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (601-35)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void LongJson_ParlotCompiled_NullLongJson_ParserThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Create a mock for Parser<IJson> and set up the Parse method to throw an exception when called with null.
// //         var parserMock = new Mock<Parser<IJson>>();
// //         parserMock.Setup(p => p.Parse(null)).Throws(new ArgumentNullException("json", "Input json cannot be null."));
// //         // Set _longJson field to null.
// //         FieldInfo longJsonField = typeof(JsonBench).GetField("_longJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(longJsonField);
// //         longJsonField.SetValue(jsonBench, null);
// //         // Set _compiled field to the mock object.
// //         FieldInfo compiledField = typeof(JsonBench).GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(compiledField);
// //         compiledField.SetValue(jsonBench, parserMock.Object);
// //         // Act & Assert
// //         ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => jsonBench.LongJson_ParlotCompiled());
// //         Assert.Equal("json", exception.ParamName);
// //         Assert.Contains("Input json cannot be null", exception.Message);
// //     }
// 
//     /// <summary>
//     /// Tests the LongJson_Parlot method with a valid JSON input.
//     /// Expected outcome: the method returns a non-null IJson object.
//     /// Steps:
//     /// 1. Create an instance of JsonBench.
//     /// 2. Set the private _longJson field to a valid JSON string ("{}").
//     /// 3. Invoke LongJson_Parlot.
//     /// 4. Assert that the returned result is not null.
//     /// </summary>
// //     [Fact] [Error] (624-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Parlot_ValidJson_ReturnsIJson()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_longJson", "{}"); // valid JSON object
// //         // Act
// //         IJson result = jsonBench.LongJson_Parlot();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests the LongJson_Parlot method with invalid JSON inputs.
//     /// Expected outcome: the method throws an exception when parsing invalid JSON.
//     /// Steps:
//     /// 1. Create an instance of JsonBench.
//     /// 2. Set the private _longJson field to an invalid JSON string.
//     /// 3. Invoke LongJson_Parlot.
//     /// 4. Assert that an exception is thrown.
//     /// </summary>
//     /// <param name = "invalidJson">A string representing an invalid JSON value.</param>
// //     [Theory] [Error] (649-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     [InlineData("not json")]
// //     [InlineData("")]
// //     [InlineData(null)]
// //     public void LongJson_Parlot_InvalidJson_ThrowsException(string invalidJson)
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_longJson", invalidJson);
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => jsonBench.LongJson_Parlot());
// //     }
// 
//     /// <summary>
//     /// Sets a private field for a given object instance via reflection.
//     /// </summary>
//     /// <param name = "obj">The object instance where the field exists.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to set for the field.</param>
// //     private static void SetPrivateField(object obj, string fieldName, object value) [Error] (660-25)CS0111 Type 'JsonBenchTests' already defines a member called 'SetPrivateField' with the same parameter types
// //     {
// //         var field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException($"Field '{fieldName}' not found in type '{obj.GetType().FullName}'.");
// //         }
// // 
// //         field.SetValue(obj, value);
// //     }
// 
//     /// <summary>
//     /// Tests the LongJson_Pidgin method with a valid JSON string.
//     /// It sets the _longJson field to a valid JSON, invokes the method, and verifies that a non-null IJson object is returned.
//     /// </summary>
// //     [Fact] [Error] (681-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Pidgin_ValidJson_ReturnsNonNullIJson()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Set the private field _longJson to a valid JSON string.
// //         SetPrivateField(jsonBench, "_longJson", "{}");
// //         // Act
// //         var result = jsonBench.LongJson_Pidgin();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests the LongJson_Pidgin method with an empty string.
//     /// It sets the _longJson field to an empty string, invokes the method, and expects an exception due to invalid JSON input.
//     /// </summary>
// //     [Fact] [Error] (698-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Pidgin_EmptyString_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Set the private field _longJson to an empty string.
// //         SetPrivateField(jsonBench, "_longJson", string.Empty);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.LongJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Tests the LongJson_Pidgin method with a null string.
//     /// It sets the _longJson field to null, invokes the method, and expects an exception due to invalid JSON input.
//     /// </summary>
// //     [Fact] [Error] (713-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Pidgin_NullString_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Set the private field _longJson to null.
// //         SetPrivateField(jsonBench, "_longJson", null);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.LongJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Helper method to set a private instance field using reflection.
//     /// </summary>
//     /// <param name = "instance">The object instance whose field is to be set.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to set.</param>
// //     private static void SetPrivateField(object instance, string fieldName, object value) [Error] (724-25)CS0111 Type 'JsonBenchTests' already defines a member called 'SetPrivateField' with the same parameter types
// //     {
// //         var fieldInfo = instance.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (fieldInfo == null)
// //         {
// //             throw new InvalidOperationException($"Field '{fieldName}' not found on type '{instance.GetType().FullName}'.");
// //         }
// // 
// //         fieldInfo.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Newtonsoft correctly parses a valid JSON string and returns the expected JToken.
//     /// </summary>
// //     [Fact] [Error] (744-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Newtonsoft_ValidJson_ReturnsParsedJToken()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "\"Hello, World!\""; // valid JSON string representing a string value
// //         SetPrivateField(jsonBench, "_longJson", validJson);
// //         // Act
// //         JToken result = jsonBench.LongJson_Newtonsoft();
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.Equal("Hello, World!", result.ToString());
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Newtonsoft throws JsonReaderException when provided with an invalid JSON string.
//     /// </summary>
// //     [Fact] [Error] (761-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Newtonsoft_InvalidJson_ThrowsJsonReaderException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string invalidJson = "invalid json"; // not a valid JSON format
// //         SetPrivateField(jsonBench, "_longJson", invalidJson);
// //         // Act & Assert
// //         Assert.Throws<JsonReaderException>(() => jsonBench.LongJson_Newtonsoft());
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Newtonsoft throws ArgumentNullException when _longJson is null.
//     /// </summary>
// //     [Fact] [Error] (774-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Newtonsoft_NullJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_longJson", null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => jsonBench.LongJson_Newtonsoft());
// //     }
// 
//     /// <summary>
//     /// Sets the value of a private field using reflection.
//     /// </summary>
//     /// <param name = "obj">The object instance containing the private field.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to set for the private field.</param>
// //     private static void SetPrivateField(object obj, string fieldName, object value) [Error] (785-25)CS0111 Type 'JsonBenchTests' already defines a member called 'SetPrivateField' with the same parameter types [Error] (787-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException($"Field '{fieldName}' not found in type '{obj.GetType()}'.");
// //         }
// // 
// //         field.SetValue(obj, value);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.LongJson_SystemTextJson"/> method with valid JSON input.
//     /// Expected outcome: the method returns a valid JsonDocument with the correct parsed content.
//     /// </summary>
// //     [Fact] [Error] (807-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (808-9)CS8602 Dereference of a possibly null reference.
// //     public void LongJson_SystemTextJson_ValidJson_ReturnsJsonDocument()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "{\"key\":\"value\"}";
// //         // Set the private _longJson field using reflection.
// //         FieldInfo fieldInfo = typeof(JsonBench).GetField("_longJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         fieldInfo.SetValue(jsonBench, validJson);
// //         // Act
// //         JsonDocument result = jsonBench.LongJson_SystemTextJson();
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.True(result.RootElement.TryGetProperty("key", out JsonElement element));
// //         Assert.Equal("value", element.GetString());
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.LongJson_SystemTextJson"/> method when _longJson is an empty string.
//     /// Expected outcome: the method throws a JsonException due to invalid JSON format.
//     /// </summary>
// //     [Fact] [Error] (831-23)CS0104 'JsonException' is an ambiguous reference between 'Newtonsoft.Json.JsonException' and 'System.Text.Json.JsonException' [Error] (828-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (829-9)CS8602 Dereference of a possibly null reference.
// //     public void LongJson_SystemTextJson_EmptyJson_ThrowsJsonException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string emptyJson = "";
// //         // Set the private _longJson field using reflection.
// //         FieldInfo fieldInfo = typeof(JsonBench).GetField("_longJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         fieldInfo.SetValue(jsonBench, emptyJson);
// //         // Act & Assert
// //         Assert.Throws<JsonException>(() => jsonBench.LongJson_SystemTextJson());
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.LongJson_SystemTextJson"/> method when _longJson is null.
//     /// Expected outcome: the method throws an ArgumentNullException.
//     /// </summary>
// //     [Fact] [Error] (843-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (845-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (846-9)CS8602 Dereference of a possibly null reference.
// //     public void LongJson_SystemTextJson_NullJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string nullJson = null;
// //         // Set the private _longJson field using reflection.
// //         FieldInfo fieldInfo = typeof(JsonBench).GetField("_longJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         fieldInfo.SetValue(jsonBench, nullJson);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => jsonBench.LongJson_SystemTextJson());
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Sprache returns a non-null IJson object when _longJson is set to a valid JSON string.
//     /// This test sets up a valid JSON input and verifies that the parser successfully returns a parsed result.
//     /// </summary>
// //     [Fact] [Error] (861-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Sprache_ValidJson_ReturnsNonNull()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Use a valid JSON string; assuming the parser can handle a simple JSON array.
// //         SetPrivateField(jsonBench, "_longJson", "[1, 2, 3]");
// //         // Act
// //         var result = jsonBench.LongJson_Sprache();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Sprache throws a FormatException when _longJson is set to an invalid JSON string.
//     /// This test sets an invalid JSON string and expects the parsing to fail with a FormatException.
//     /// </summary>
// //     [Fact] [Error] (878-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Sprache_InvalidJson_ThrowsFormatException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Set an invalid JSON string.
// //         SetPrivateField(jsonBench, "_longJson", "Invalid JSON");
// //         // Act & Assert
// //         Assert.Throws<FormatException>(() => jsonBench.LongJson_Sprache());
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Sprache throws an ArgumentNullException when _longJson is null.
//     /// This test ensures that providing a null JSON string causes the parser to throw an ArgumentNullException.
//     /// </summary>
// //     [Fact] [Error] (893-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Sprache_NullJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Set _longJson to null.
// //         SetPrivateField(jsonBench, "_longJson", null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => jsonBench.LongJson_Sprache());
// //     }
// 
//     /// <summary>
//     /// Helper method to set a private field via reflection.
//     /// </summary>
//     /// <param name = "obj">The object whose field is to be set.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to assign to the field.</param>
// //     private static void SetPrivateField(object obj, string fieldName, object value) [Error] (904-25)CS0111 Type 'JsonBenchTests' already defines a member called 'SetPrivateField' with the same parameter types
// //     {
// //         var field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new Exception($"Field '{fieldName}' not found on type {obj.GetType().FullName}.");
// //         }
// // 
// //         field.SetValue(obj, value);
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Superpower returns a non-null IJson when provided with a valid JSON string.
//     /// Arrange: Set _longJson to a valid JSON string.
//     /// Act: Invoke LongJson_Superpower.
//     /// Assert: The returned IJson object is not null.
//     /// </summary>
// //     [Fact] [Error] (926-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Superpower_ValidJson_ReturnsNonNullIJson()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_longJson", "{\"name\":\"test\", \"value\":123}");
// //         // Act
// //         var result = jsonBench.LongJson_Superpower();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Superpower throws an exception when provided with an invalid JSON string.
//     /// Arrange: Set _longJson to an invalid JSON string.
//     /// Act & Assert: The invocation of LongJson_Superpower throws an exception.
//     /// </summary>
// //     [Fact] [Error] (943-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Superpower_InvalidJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_longJson", "invalid json");
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => jsonBench.LongJson_Superpower());
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Superpower throws an exception when provided with an empty JSON string.
//     /// Arrange: Set _longJson to an empty string.
//     /// Act & Assert: The invocation of LongJson_Superpower throws an exception.
//     /// </summary>
// //     [Fact] [Error] (958-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Superpower_EmptyJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_longJson", string.Empty);
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => jsonBench.LongJson_Superpower());
// //     }
// 
//     /// <summary>
//     /// Tests that LongJson_Superpower throws an exception when _longJson is null.
//     /// Arrange: Set _longJson to null.
//     /// Act & Assert: The invocation of LongJson_Superpower throws an exception.
//     /// </summary>
// //     [Fact] [Error] (973-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void LongJson_Superpower_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_longJson", null);
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => jsonBench.LongJson_Superpower());
// //     }
// 
//     /// <summary>
//     /// Sets the private field of an object using reflection.
//     /// </summary>
//     /// <param name = "instance">The instance containing the private field.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to set.</param>
// //     private static void SetPrivateField(object instance, string fieldName, object value) [Error] (984-25)CS0111 Type 'JsonBenchTests' already defines a member called 'SetPrivateField' with the same parameter types
// //     {
// //         var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException($"Field '{fieldName}' not found in type '{instance.GetType().FullName}'.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests the DeepJson_ParlotCompiled method with valid deep JSON input.
//     /// Expected outcome: The method returns the parsed IJson instance as provided by the parser.
//     /// </summary>
// //     [Fact] [Error] (1004-33)CS0246 The type or namespace name 'FakeJson' could not be found (are you missing a using directive or an assembly reference?) [Error] (1006-35)CS0246 The type or namespace name 'Parser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (1011-9)CS8602 Dereference of a possibly null reference. [Error] (1012-9)CS8602 Dereference of a possibly null reference.
// //     public void DeepJson_ParlotCompiled_ValidDeepJson_ReturnsExpectedIJson()
// //     {
// //         // Arrange
// //         var bench = new JsonBench();
// //         var expectedIJson = new FakeJson();
// //         var validDeepJson = "{\"key\":\"value\"}";
// //         var mockParser = new Mock<Parser<IJson>>();
// //         mockParser.Setup(p => p.Parse(validDeepJson)).Returns(expectedIJson);
// //         // Set private fields using reflection.
// //         var compiledField = typeof(JsonBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
// //         var deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         compiledField.SetValue(bench, mockParser.Object);
// //         deepJsonField.SetValue(bench, validDeepJson);
// //         // Act
// //         var result = bench.DeepJson_ParlotCompiled();
// //         // Assert
// //         Assert.Equal(expectedIJson, result);
// //     }
// 
//     /// <summary>
//     /// Tests the DeepJson_ParlotCompiled method when _deepJson is null.
//     /// Expected outcome: The method should throw an ArgumentNullException as the parser throws when given a null input.
//     /// </summary>
// //     [Fact] [Error] (1028-35)CS0246 The type or namespace name 'Parser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (1032-9)CS8602 Dereference of a possibly null reference. [Error] (1033-9)CS8602 Dereference of a possibly null reference.
// //     public void DeepJson_ParlotCompiled_NullDeepJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var bench = new JsonBench();
// //         var mockParser = new Mock<Parser<IJson>>();
// //         mockParser.Setup(p => p.Parse(null)).Throws(new ArgumentNullException());
// //         var compiledField = typeof(JsonBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
// //         var deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         compiledField.SetValue(bench, mockParser.Object);
// //         deepJsonField.SetValue(bench, null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => bench.DeepJson_ParlotCompiled());
// //     }
// 
//     /// <summary>
//     /// Tests the DeepJson_ParlotCompiled method when _compiled is null.
//     /// Expected outcome: The method should throw a NullReferenceException as it attempts to call Parse on a null parser.
//     /// </summary>
// //     [Fact] [Error] (1048-9)CS8602 Dereference of a possibly null reference. [Error] (1050-9)CS8602 Dereference of a possibly null reference.
// //     public void DeepJson_ParlotCompiled_NullCompiled_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var bench = new JsonBench();
// //         var deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         deepJsonField.SetValue(bench, "{\"key\":\"value\"}");
// //         var compiledField = typeof(JsonBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
// //         compiledField.SetValue(bench, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => bench.DeepJson_ParlotCompiled());
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_Parlot returns a non-null IJson object when _deepJson contains valid JSON.
//     /// The test sets the private _deepJson field on the instance and then calls the method.
//     /// Expected outcome: a non-null IJson is returned.
//     /// </summary>
// //     [Fact] [Error] (1065-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void DeepJson_Parlot_ValidJson_ReturnsIJson()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_deepJson", "{\"name\":\"value\", \"number\":123}");
// //         // Act
// //         var result = jsonBench.DeepJson_Parlot();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_Parlot throws an exception when _deepJson is set to null.
//     /// The test prepares the instance with a null _deepJson field and expects an exception during parsing.
//     /// Expected outcome: an exception is thrown.
//     /// </summary>
// //     [Fact] [Error] (1082-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void DeepJson_Parlot_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_deepJson", null);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.DeepJson_Parlot());
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_Parlot throws an exception when _deepJson contains an invalid JSON string.
//     /// The test sets the private _deepJson field with an invalid JSON string.
//     /// Expected outcome: an exception is thrown during parsing.
//     /// </summary>
// //     [Fact] [Error] (1097-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void DeepJson_Parlot_InvalidJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_deepJson", "Not a valid JSON string");
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.DeepJson_Parlot());
// //     }
// 
//     /// <summary>
//     /// Helper method to set a private field value using reflection.
//     /// </summary>
//     /// <param name = "instance">The instance of the class where the private field is set.</param>
//     /// <param name = "fieldName">The name of the private field to set.</param>
//     /// <param name = "value">The value to assign to the private field.</param>
// //     private static void SetPrivateField(object instance, string fieldName, object value) [Error] (1108-25)CS0111 Type 'JsonBenchTests' already defines a member called 'SetPrivateField' with the same parameter types
// //     {
// //         var fieldInfo = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (fieldInfo == null)
// //         {
// //             throw new InvalidOperationException($"Field {fieldName} not found on type {instance.GetType().FullName}");
// //         }
// // 
// //         fieldInfo.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that the DeepJson_Pidgin method returns a non-null IJson object when provided with a valid deep JSON string.
//     /// </summary>
// //     [Fact] [Error] (1129-35)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJson_Pidgin_ValidDeepJson_ReturnsIJson()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         // Set a valid deep JSON string, e.g. a nested JSON object.
// //         string validDeepJson = "{\"a\":{\"b\":{\"c\":\"d\"}}}";
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (deepJsonField == null)
// //         {
// //             throw new Exception("The _deepJson field was not found.");
// //         }
// // 
// //         deepJsonField.SetValue(jsonBench, validDeepJson);
// //         // Act
// //         var result = jsonBench.DeepJson_Pidgin();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests that the DeepJson_Pidgin method throws an exception when the internal _deepJson field is null.
//     /// </summary>
// //     [Fact] [Error] (1150-35)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJson_Pidgin_NullDeepJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (deepJsonField == null)
// //         {
// //             throw new Exception("The _deepJson field was not found.");
// //         }
// // 
// //         deepJsonField.SetValue(jsonBench, null);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.DeepJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Tests that the DeepJson_Pidgin method throws an exception when the internal _deepJson field is an empty string.
//     /// </summary>
// //     [Fact] [Error] (1169-35)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJson_Pidgin_EmptyDeepJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (deepJsonField == null)
// //         {
// //             throw new Exception("The _deepJson field was not found.");
// //         }
// // 
// //         deepJsonField.SetValue(jsonBench, string.Empty);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.DeepJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Tests the DeepJson_Newtonsoft method when provided with valid JSON.
//     /// The test sets a valid JSON string into the internal _deepJson field,
//     /// invokes the method, and verifies that the returned JToken has the expected value.
//     /// </summary>
// //     [Fact] [Error] (1191-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1192-9)CS8602 Dereference of a possibly null reference.
// //     public void DeepJson_Newtonsoft_ValidJson_ReturnsJToken()
// //     {
// //         // Arrange
// //         var validJson = "{\"key\":\"value\"}";
// //         var jsonBenchInstance = new JsonBench();
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         deepJsonField.SetValue(jsonBenchInstance, validJson);
// //         // Act
// //         JToken result = jsonBenchInstance.DeepJson_Newtonsoft();
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.Equal("value", result["key"]?.Value<string>());
// //     }
// 
//     /// <summary>
//     /// Tests the DeepJson_Newtonsoft method when provided with an invalid JSON string.
//     /// The test sets an invalid JSON string into the internal _deepJson field and verifies that
//     /// a JsonReaderException is thrown.
//     /// </summary>
// //     [Fact] [Error] (1211-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1212-9)CS8602 Dereference of a possibly null reference.
// //     public void DeepJson_Newtonsoft_InvalidJson_ThrowsJsonReaderException()
// //     {
// //         // Arrange
// //         var invalidJson = "invalid json";
// //         var jsonBenchInstance = new JsonBench();
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         deepJsonField.SetValue(jsonBenchInstance, invalidJson);
// //         // Act & Assert
// //         Assert.Throws<JsonReaderException>(() => jsonBenchInstance.DeepJson_Newtonsoft());
// //     }
// 
//     /// <summary>
//     /// Tests the DeepJson_Newtonsoft method when provided with an empty string.
//     /// The test sets an empty string into the internal _deepJson field and verifies that
//     /// a JsonReaderException is thrown due to the inability to deserialize an empty JSON string.
//     /// </summary>
// //     [Fact] [Error] (1228-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1229-9)CS8602 Dereference of a possibly null reference.
// //     public void DeepJson_Newtonsoft_EmptyString_ThrowsJsonReaderException()
// //     {
// //         // Arrange
// //         var emptyJson = string.Empty;
// //         var jsonBenchInstance = new JsonBench();
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         deepJsonField.SetValue(jsonBenchInstance, emptyJson);
// //         // Act & Assert
// //         Assert.Throws<JsonReaderException>(() => jsonBenchInstance.DeepJson_Newtonsoft());
// //     }
// 
//     /// <summary>
//     /// Tests the DeepJson_Newtonsoft method when provided with a null JSON string.
//     /// The test sets a null value into the internal _deepJson field and verifies that
//     /// the method returns null, which is the expected behavior when deserializing a null string.
//     /// </summary>
// //     [Fact] [Error] (1243-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1245-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1246-9)CS8602 Dereference of a possibly null reference.
// //     public void DeepJson_Newtonsoft_NullJson_ReturnsNull()
// //     {
// //         // Arrange
// //         string nullJson = null;
// //         var jsonBenchInstance = new JsonBench();
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         deepJsonField.SetValue(jsonBenchInstance, nullJson);
// //         // Act
// //         JToken result = jsonBenchInstance.DeepJson_Newtonsoft();
// //         // Assert
// //         Assert.Null(result);
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_SystemTextJson returns a correctly parsed JsonDocument when provided with valid JSON.
//     /// </summary>
// //     [Fact] [Error] (1264-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1268-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJson_SystemTextJson_ValidJson_ReturnsParsedDocument()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "{\"test\":123}";
// //         var documentOptions = new JsonDocumentOptions();
// //         // Set the private field _deepJson
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(deepJsonField);
// //         deepJsonField.SetValue(jsonBench, validJson);
// //         // Set the private field _jsonDocumentOptions
// //         FieldInfo optionsField = typeof(JsonBench).GetField("_jsonDocumentOptions", BindingFlags.Static | BindingFlags.NonPublic);
// //         Assert.NotNull(optionsField);
// //         optionsField.SetValue(null, documentOptions);
// //         // Act
// //         JsonDocument result = jsonBench.DeepJson_SystemTextJson();
// //         // Assert
// //         Assert.NotNull(result);
// //         JsonElement root = result.RootElement;
// //         Assert.True(root.TryGetProperty("test", out JsonElement testProperty), "The root element does not contain the 'test' property.");
// //         Assert.Equal(123, testProperty.GetInt32());
// //         result.Dispose();
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_SystemTextJson throws a JsonException when provided with an invalid JSON string.
//     /// </summary>
// //     [Fact] [Error] (1300-23)CS0104 'JsonException' is an ambiguous reference between 'Newtonsoft.Json.JsonException' and 'System.Text.Json.JsonException' [Error] (1292-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1296-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJson_SystemTextJson_InvalidJson_ThrowsJsonException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string invalidJson = "invalid json";
// //         var documentOptions = new JsonDocumentOptions();
// //         // Set the private field _deepJson
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(deepJsonField);
// //         deepJsonField.SetValue(jsonBench, invalidJson);
// //         // Set the private field _jsonDocumentOptions
// //         FieldInfo optionsField = typeof(JsonBench).GetField("_jsonDocumentOptions", BindingFlags.Static | BindingFlags.NonPublic);
// //         Assert.NotNull(optionsField);
// //         optionsField.SetValue(null, documentOptions);
// //         // Act & Assert
// //         Assert.Throws<JsonException>(() => jsonBench.DeepJson_SystemTextJson());
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_SystemTextJson throws an ArgumentNullException when the _deepJson field is null.
//     /// </summary>
// //     [Fact] [Error] (1311-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1314-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1318-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJson_SystemTextJson_NullJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string nullJson = null;
// //         var documentOptions = new JsonDocumentOptions();
// //         // Set the private field _deepJson to null
// //         FieldInfo deepJsonField = typeof(JsonBench).GetField("_deepJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(deepJsonField);
// //         deepJsonField.SetValue(jsonBench, nullJson);
// //         // Set the private field _jsonDocumentOptions
// //         FieldInfo optionsField = typeof(JsonBench).GetField("_jsonDocumentOptions", BindingFlags.Static | BindingFlags.NonPublic);
// //         Assert.NotNull(optionsField);
// //         optionsField.SetValue(null, documentOptions);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => jsonBench.DeepJson_SystemTextJson());
// //     }
// 
//     /// <summary>
//     /// Sets a private instance field via reflection.
//     /// </summary>
//     /// <param name = "instance">The instance whose field is to be set.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The new value for the field.</param>
// //     private void SetPrivateField(object instance, string fieldName, object value) [Error] (1331-18)CS0111 Type 'JsonBenchTests' already defines a member called 'SetPrivateField' with the same parameter types
// //     {
// //         var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new Exception($"Field '{fieldName}' not found on type '{instance.GetType().FullName}'.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_Sprache returns a non-null IJson instance when provided with valid deep JSON data.
//     /// This test sets the _deepJson private field to a valid JSON string, calls DeepJson_Sprache, and verifies that
//     /// the returned IJson is not null.
//     /// </summary>
// //     [Fact] [Error] (1353-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void DeepJson_Sprache_ValidJson_ReturnsIJson()
// //     {
// //         // Arrange
// //         var validJson = "{\"key\":\"value\"}";
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_deepJson", validJson);
// //         // Act
// //         var result = jsonBench.DeepJson_Sprache();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_Sprache throws an exception when the _deepJson private field is null.
//     /// This simulates a scenario where the JSON data is missing.
//     /// </summary>
// //     [Fact] [Error] (1369-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void DeepJson_Sprache_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_deepJson", null);
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => jsonBench.DeepJson_Sprache());
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJson_Sprache throws an exception when the _deepJson private field is an empty string.
//     /// This simulates a scenario where the JSON data is present but empty.
//     /// </summary>
// //     [Fact] [Error] (1383-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void DeepJson_Sprache_EmptyJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_deepJson", string.Empty);
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => jsonBench.DeepJson_Sprache());
// //     }
// 
//     /// <summary>
//     /// Tests the WideJson_Parlot method with a valid JSON string.
//     /// The test sets the private field _wideJson via reflection to a valid JSON ("{}"),
//     /// calls the WideJson_Parlot method, and verifies that the returned IJson is not null.
//     /// </summary>
//     [Fact]
//     public void WideJson_Parlot_ValidJson_ReturnsParsedJson()
//     {
//         // Arrange
//         var jsonBench = new JsonBench();
//         var wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
//         if (wideJsonField == null)
//         {
//             throw new InvalidOperationException("Unable to locate the '_wideJson' field.");
//         }
// 
//         // Use a valid JSON string representing an empty JSON object.
//         wideJsonField.SetValue(jsonBench, "{}");
//         // Act
//         var result = jsonBench.WideJson_Parlot();
//         // Assert
//         Assert.NotNull(result);
//     }
// 
//     /// <summary>
//     /// Tests the WideJson_Parlot method with an empty JSON string.
//     /// The test sets the private field _wideJson to an empty string and expects that calling
//     /// WideJson_Parlot throws an exception due to invalid JSON input.
//     /// </summary>
//     [Fact]
//     public void WideJson_Parlot_EmptyJson_ThrowsException()
//     {
//         // Arrange
//         var jsonBench = new JsonBench();
//         var wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
//         if (wideJsonField == null)
//         {
//             throw new InvalidOperationException("Unable to locate the '_wideJson' field.");
//         }
// 
//         // Use an empty string as invalid JSON.
//         wideJsonField.SetValue(jsonBench, "");
//         // Act & Assert
//         Assert.Throws<Exception>(() => jsonBench.WideJson_Parlot());
//     }
// 
//     /// <summary>
//     /// Tests the WideJson_Parlot method with a null JSON string.
//     /// The test sets the private field _wideJson to null and expects that calling
//     /// WideJson_Parlot throws an exception due to missing JSON input.
//     /// </summary>
//     [Fact]
//     public void WideJson_Parlot_NullJson_ThrowsException()
//     {
//         // Arrange
//         var jsonBench = new JsonBench();
//         var wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
//         if (wideJsonField == null)
//         {
//             throw new InvalidOperationException("Unable to locate the '_wideJson' field.");
//         }
// 
//         // Set _wideJson value to null.
//         wideJsonField.SetValue(jsonBench, null);
//         // Act & Assert
//         Assert.Throws<Exception>(() => jsonBench.WideJson_Parlot());
//     }
// 
//     /// <summary>
//     /// Tests that the WideJson_Pidgin method returns a non-null IJson instance when provided with valid wide JSON.
//     /// </summary>
// //     [Fact] [Error] (1465-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1466-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJson_Pidgin_ValidWideJson_ReturnsParsedJson()
// //     {
// //         // Arrange: Create an instance of JsonBench and set the _wideJson field with valid JSON.
// //         var jsonBench = new JsonBench();
// //         // Using reflection to set the private field _wideJson to a valid JSON string (an empty array).
// //         FieldInfo wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         wideJsonField.SetValue(jsonBench, "[]");
// //         // Act: Call the method under test.
// //         var result = jsonBench.WideJson_Pidgin();
// //         // Assert: Verify that the returned result is not null.
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests that the WideJson_Pidgin method throws an exception when provided with an invalid JSON string.
//     /// </summary>
// //     [Fact] [Error] (1481-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1482-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJson_Pidgin_InvalidWideJson_ThrowsException()
// //     {
// //         // Arrange: Create an instance of JsonBench and set the _wideJson field with an invalid JSON string.
// //         var jsonBench = new JsonBench();
// //         FieldInfo wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         wideJsonField.SetValue(jsonBench, "invalid json");
// //         // Act & Assert: Expect an exception when parsing invalid JSON.
// //         Assert.Throws<Exception>(() => jsonBench.WideJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Tests that the WideJson_Pidgin method throws an exception when provided with an empty JSON string.
//     /// </summary>
// //     [Fact] [Error] (1495-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1496-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJson_Pidgin_EmptyWideJson_ThrowsException()
// //     {
// //         // Arrange: Create an instance of JsonBench and set the _wideJson field to an empty string.
// //         var jsonBench = new JsonBench();
// //         FieldInfo wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         wideJsonField.SetValue(jsonBench, string.Empty);
// //         // Act & Assert: Expect an exception when parsing an empty JSON string.
// //         Assert.Throws<Exception>(() => jsonBench.WideJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Tests that the WideJson_Pidgin method throws an exception when the _wideJson field is null.
//     /// </summary>
// //     [Fact] [Error] (1509-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1510-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJson_Pidgin_NullWideJson_ThrowsException()
// //     {
// //         // Arrange: Create an instance of JsonBench and explicitly set the _wideJson field to null.
// //         var jsonBench = new JsonBench();
// //         FieldInfo wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.Instance | BindingFlags.NonPublic);
// //         wideJsonField.SetValue(jsonBench, null);
// //         // Act & Assert: Expect an exception when parsing a null JSON string.
// //         Assert.Throws<Exception>(() => jsonBench.WideJson_Pidgin());
// //     }
// 
//     /// <summary>
//     /// Tests that WideJson_Newtonsoft returns a valid JToken when _wideJson contains a valid JSON string.
//     /// </summary>
// //     [Fact] [Error] (1524-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void WideJson_Newtonsoft_ValidJson_ReturnsJToken()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "{\"key\": \"value\"}";
// //         SetPrivateField(jsonBench, "_wideJson", validJson);
// //         // Act
// //         JToken result = jsonBench.WideJson_Newtonsoft();
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.Equal("value", result["key"]?.ToString());
// //     }
// 
//     /// <summary>
//     /// Tests that WideJson_Newtonsoft throws an ArgumentNullException when _wideJson is null.
//     /// </summary>
// //     [Fact] [Error] (1540-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void WideJson_Newtonsoft_NullJson_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_wideJson", null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => jsonBench.WideJson_Newtonsoft());
// //     }
// 
//     /// <summary>
//     /// Tests that WideJson_Newtonsoft throws a JsonReaderException when _wideJson is an empty string.
//     /// </summary>
// //     [Fact] [Error] (1553-9)CS0121 The call is ambiguous between the following methods or properties: 'JsonBenchTests.SetPrivateField(object, string, object?)' and 'JsonBenchTests.SetPrivateField(object, string, object)'
// //     public void WideJson_Newtonsoft_EmptyJson_ThrowsJsonReaderException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         SetPrivateField(jsonBench, "_wideJson", string.Empty);
// //         // Act & Assert
// //         Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => jsonBench.WideJson_Newtonsoft());
// //     }
// 
//     /// <summary>
//     /// Helper method to set the value of a private field using reflection.
//     /// </summary>
//     /// <param name = "instance">The instance whose field will be set.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to set.</param>
// //     private static void SetPrivateField(object instance, string fieldName, object value) [Error] (1564-25)CS0111 Type 'JsonBenchTests' already defines a member called 'SetPrivateField' with the same parameter types [Error] (1566-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException($"Field '{fieldName}' not found in type '{instance.GetType().FullName}'.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.WideJson_Sprache"/> method with a valid JSON string.
//     /// The test arranges a valid JSON input by setting the private '_wideJson' field via reflection,
//     /// then invokes the method and asserts that the returned value is not null.
//     /// Expected outcome: a non-null IJson object is returned.
//     /// </summary>
// //     [Fact] [Error] (1587-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1588-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJson_Sprache_ValidJson_ReturnsNotNull()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string validJson = "{\"name\":\"Test\", \"value\":123}";
// //         FieldInfo wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         wideJsonField.SetValue(jsonBench, validJson);
// //         // Act
// //         var result = jsonBench.WideJson_Sprache();
// //         // Assert
// //         Assert.NotNull(result);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.WideJson_Sprache"/> method with an invalid JSON string.
//     /// The test arranges an invalid JSON input by setting the private '_wideJson' field via reflection,
//     /// then invokes the method and asserts that an exception is thrown.
//     /// Expected outcome: an exception is thrown due to the invalid format.
//     /// </summary>
// //     [Fact] [Error] (1607-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1608-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJson_Sprache_InvalidJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string invalidJson = "Invalid JSON Content";
// //         FieldInfo wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         wideJsonField.SetValue(jsonBench, invalidJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.WideJson_Sprache());
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "JsonBench.WideJson_Sprache"/> method with a null JSON string.
//     /// The test arranges a null value for the private '_wideJson' field via reflection,
//     /// then invokes the method and asserts that an exception is thrown.
//     /// Expected outcome: an exception is thrown due to the null input.
//     /// </summary>
// //     [Fact] [Error] (1624-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1625-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1626-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJson_Sprache_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string nullJson = null;
// //         FieldInfo wideJsonField = typeof(JsonBench).GetField("_wideJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         wideJsonField.SetValue(jsonBench, nullJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.WideJson_Sprache());
// //     }
// 
//     /// <summary>
//     /// Sets the private _wideJson field of the JsonBench instance to the specified value via reflection.
//     /// </summary>
//     /// <param name = "instance">The JsonBench instance.</param>
//     /// <param name = "value">The value to set.</param>
// //     private static void SetWideJson(JsonBench instance, string value) [Error] (1638-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = typeof(JsonBench).GetField("_wideJson", BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("The _wideJson field was not found.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that WideJson_Superpower returns a non-null IJson object when a valid wide JSON string is provided.
//     /// Arrange: A JsonBench instance with a valid JSON string assigned to _wideJson.
//     /// Act: Invoke WideJson_Superpower.
//     /// Assert: The returned IJson object is not null.
//     /// </summary>
//     [Fact]
//     public void WideJson_Superpower_ValidJson_ReturnsIJson()
//     {
//         // Arrange
//         var jsonBench = new JsonBench();
//         string validJson = "[{\"key\":\"value\"}, {\"number\":123}]";
//         SetWideJson(jsonBench, validJson);
//         // Act
//         var result = jsonBench.WideJson_Superpower();
//         // Assert
//         Assert.NotNull(result);
//     }
// 
//     /// <summary>
//     /// Tests that WideJson_Superpower throws an exception when an empty JSON string is provided.
//     /// Arrange: A JsonBench instance with an empty string assigned to _wideJson.
//     /// Act & Assert: Invoking WideJson_Superpower throws an exception.
//     /// </summary>
//     [Fact]
//     public void WideJson_Superpower_EmptyJson_ThrowsException()
//     {
//         // Arrange
//         var jsonBench = new JsonBench();
//         string emptyJson = "";
//         SetWideJson(jsonBench, emptyJson);
//         // Act & Assert
//         Assert.ThrowsAny<Exception>(() => jsonBench.WideJson_Superpower());
//     }
// 
//     /// <summary>
//     /// Tests that WideJson_Superpower throws an exception when a null JSON string is provided.
//     /// Arrange: A JsonBench instance with a null value assigned to _wideJson.
//     /// Act & Assert: Invoking WideJson_Superpower throws an exception.
//     /// </summary>
// //     [Fact] [Error] (1692-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1693-32)CS8604 Possible null reference argument for parameter 'value' in 'void JsonBenchTests.SetWideJson(JsonBench instance, string value)'.
// //     public void WideJson_Superpower_NullJson_ThrowsException()
// //     {
// //         // Arrange
// //         var jsonBench = new JsonBench();
// //         string nullJson = null;
// //         SetWideJson(jsonBench, nullJson);
// //         // Act & Assert
// //         Assert.ThrowsAny<Exception>(() => jsonBench.WideJson_Superpower());
// //     }
// 
//     /// <summary>
//     /// Retrieves the private static BuildJson method from the JsonBench class using reflection.
//     /// </summary>
//     /// <returns>The MethodInfo for BuildJson.</returns>
//     private static MethodInfo GetBuildJsonMethod()
//     {
//         var type = typeof(JsonBench);
//         var method = type.GetMethod("BuildJson", BindingFlags.NonPublic | BindingFlags.Static);
//         if (method == null)
//         {
//             throw new InvalidOperationException("BuildJson method not found in JsonBench.");
//         }
// 
//         return method;
//     }
// 
//     /// <summary>
//     /// Tests that calling BuildJson with a length of zero returns a JsonArray with no items.
//     /// </summary>
// //     [Fact] [Error] (1726-25)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1731-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1733-29)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void BuildJson_WithZeroLength_ReturnsEmptyJsonArray()
// //     {
// //         // Arrange
// //         int length = 0;
// //         int depth = 1;
// //         int width = 1;
// //         MethodInfo buildJsonMethod = GetBuildJsonMethod();
// //         // Act
// //         object result = buildJsonMethod.Invoke(null, new object[] { length, depth, width });
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.Equal("JsonArray", result.GetType().Name);
// //         // Retrieve the Items property using reflection to count the contained elements.
// //         PropertyInfo itemsProperty = result.GetType().GetProperty("Items");
// //         Assert.NotNull(itemsProperty);
// //         object itemsValue = itemsProperty.GetValue(result);
// //         var enumerable = itemsValue as IEnumerable;
// //         int count = 0;
// //         if (enumerable != null)
// //         {
// //             foreach (object item in enumerable)
// //             {
// //                 count++;
// //             }
// //         }
// // 
// //         Assert.Equal(0, count);
// //     }
// 
//     /// <summary>
//     /// Tests that calling BuildJson with a positive length returns a JsonArray with the correct number of items.
//     /// </summary>
// //     [Fact] [Error] (1759-25)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1764-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1766-29)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void BuildJson_WithPositiveLength_ReturnsJsonArrayWithCorrectElementCount()
// //     {
// //         // Arrange
// //         int length = 3;
// //         int depth = 2;
// //         int width = 2;
// //         MethodInfo buildJsonMethod = GetBuildJsonMethod();
// //         // Act
// //         object result = buildJsonMethod.Invoke(null, new object[] { length, depth, width });
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.Equal("JsonArray", result.GetType().Name);
// //         // Retrieve the Items property using reflection to assert correct count and that items are not null.
// //         PropertyInfo itemsProperty = result.GetType().GetProperty("Items");
// //         Assert.NotNull(itemsProperty);
// //         object itemsValue = itemsProperty.GetValue(result);
// //         var enumerable = itemsValue as IEnumerable;
// //         int count = 0;
// //         foreach (object item in enumerable ?? Enumerable.Empty<object>())
// //         {
// //             Assert.NotNull(item);
// //             count++;
// //         }
// // 
// //         Assert.Equal(length, count);
// //     }
// 
//     /// <summary>
//     /// Tests that calling BuildJson with a negative length throws an ArgumentOutOfRangeException.
//     /// </summary>
//     [Fact]
//     public void BuildJson_WithNegativeLength_ThrowsArgumentOutOfRangeException()
//     {
//         // Arrange
//         int length = -1;
//         int depth = 1;
//         int width = 1;
//         MethodInfo buildJsonMethod = GetBuildJsonMethod();
//         // Act & Assert
//         // Since invoking a method via reflection wraps exceptions in a TargetInvocationException,
//         // we must inspect the InnerException.
//         var exception = Assert.Throws<TargetInvocationException>(() =>
//         {
//             buildJsonMethod.Invoke(null, new object[] { length, depth, width });
//         });
//         Assert.NotNull(exception.InnerException);
//         Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
//     }
// 
//     /// <summary>
//     /// Retrieves the MethodInfo for the private static BuildObject method.
//     /// </summary>
//     /// <returns>The MethodInfo for BuildObject.</returns>
//     private static MethodInfo GetBuildObjectMethodInfo()
//     {
//         var method = typeof(JsonBench).GetMethod("BuildObject", BindingFlags.NonPublic | BindingFlags.Static);
//         if (method == null)
//         {
//             throw new InvalidOperationException("BuildObject method not found.");
//         }
// 
//         return method;
//     }
// 
//     /// <summary>
//     /// Helper method to invoke the BuildObject method using reflection.
//     /// </summary>
//     /// <param name = "depth">The depth parameter.</param>
//     /// <param name = "width">The width parameter.</param>
//     /// <returns>The resulting IJson instance.</returns>
// //     private static IJson InvokeBuildObject(int depth, int width) [Error] (1827-20)CS8603 Possible null reference return. [Error] (1832-19)CS8597 Thrown value may be null.
// //     {
// //         var method = GetBuildObjectMethodInfo();
// //         try
// //         {
// //             var result = method.Invoke(null, new object[] { depth, width });
// //             return result as IJson;
// //         }
// //         catch (TargetInvocationException ex)
// //         {
// //             // Unwrap the inner exception to expose the actual error.
// //             throw ex.InnerException;
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests that BuildObject with depth equal to zero returns a JsonString with a value of length six.
//     /// </summary>
// //     [Fact] [Error] (1851-24)CS8602 Dereference of a possibly null reference.
// //     public void BuildObject_DepthZero_ReturnsJsonStringWithLengthSix()
// //     {
// //         // Arrange
// //         int depth = 0;
// //         int width = 5; // width is irrelevant when depth is zero.
// //         // Act
// //         IJson result = InvokeBuildObject(depth, width);
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.IsType<JsonString>(result);
// //         var jsonString = result as JsonString;
// //         Assert.NotNull(jsonString.Value);
// //         Assert.Equal(6, jsonString.Value.Length);
// //     }
// 
//     /// <summary>
//     /// Tests that BuildObject with a positive depth and zero width returns a JsonObject with an empty property collection.
//     /// </summary>
// //     [Fact] [Error] (1870-24)CS1503 Argument 1: cannot convert from 'method group' to 'object?' [Error] (1871-22)CS1503 Argument 1: cannot convert from 'method group' to 'System.Collections.IEnumerable'
// //     public void BuildObject_PositiveDepthZeroWidth_ReturnsEmptyJsonObject()
// //     {
// //         // Arrange
// //         int depth = 1;
// //         int width = 0;
// //         // Act
// //         IJson result = InvokeBuildObject(depth, width);
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.IsType<JsonObject>(result);
// //         var jsonObject = result as JsonObject;
// //         Assert.NotNull(jsonObject.Properties);
// //         Assert.Empty(jsonObject.Properties);
// //     }
// 
//     /// <summary>
//     /// Tests that BuildObject with a positive depth and positive width returns a nested JsonObject structure.
//     /// The outer JsonObject should contain a specified number of properties, and each nested object should conform to the expected structure.
//     /// </summary>
// //     [Fact] [Error] (1890-24)CS1503 Argument 1: cannot convert from 'method group' to 'object?' [Error] (1891-41)CS0119 'Extensions.Properties(IEnumerable<JObject>)' is a method, which is not valid in the given context [Error] (1892-29)CS0446 Foreach cannot operate on a 'method group'. Did you intend to invoke the 'method group'? [Error] (1896-28)CS1503 Argument 1: cannot convert from 'method group' to 'object?' [Error] (1897-39)CS1503 Argument 1: cannot convert from 'method group' to 'object?' [Error] (1898-31)CS0837 The first operand of an 'is' or 'as' operator may not be a lambda expression, anonymous method, or method group. [Error] (1899-28)CS1503 Argument 1: cannot convert from 'method group' to 'object?' [Error] (1900-45)CS0119 'Extensions.Properties(IEnumerable<JObject>)' is a method, which is not valid in the given context [Error] (1901-38)CS0446 Foreach cannot operate on a 'method group'. Did you intend to invoke the 'method group'? [Error] (1905-32)CS1503 Argument 1: cannot convert from 'method group' to 'object?' [Error] (1906-43)CS1503 Argument 1: cannot convert from 'method group' to 'object?' [Error] (1907-35)CS0837 The first operand of an 'is' or 'as' operator may not be a lambda expression, anonymous method, or method group.
// //     public void BuildObject_PositiveDepthPositiveWidth_ReturnsNestedJsonObjectStructure()
// //     {
// //         // Arrange
// //         int depth = 2;
// //         int width = 3;
// //         // Act
// //         IJson result = InvokeBuildObject(depth, width);
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.IsType<JsonObject>(result);
// //         var outerObject = result as JsonObject;
// //         Assert.NotNull(outerObject.Properties);
// //         Assert.Equal(width, outerObject.Properties.Count);
// //         foreach (var kvp in outerObject.Properties)
// //         {
// //             // The key should be a string of length 5.
// //             Assert.Equal(5, kvp.Key.Length);
// //             Assert.NotNull(kvp.Value);
// //             Assert.IsType<JsonObject>(kvp.Value);
// //             var innerObject = kvp.Value as JsonObject;
// //             Assert.NotNull(innerObject.Properties);
// //             Assert.Equal(width, innerObject.Properties.Count);
// //             foreach (var innerKvp in innerObject.Properties)
// //             {
// //                 // The inner key should be a string of length 5.
// //                 Assert.Equal(5, innerKvp.Key.Length);
// //                 Assert.NotNull(innerKvp.Value);
// //                 Assert.IsType<JsonString>(innerKvp.Value);
// //                 var innerString = innerKvp.Value as JsonString;
// //                 Assert.NotNull(innerString.Value);
// //                 // The JsonString value should be of length 6.
// //                 Assert.Equal(6, innerString.Value.Length);
// //             }
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests that BuildObject with a negative width throws an ArgumentOutOfRangeException.
//     /// This is due to the use of Enumerable.Repeat with a negative count.
//     /// </summary>
//     [Fact]
//     public void BuildObject_NegativeWidth_ThrowsArgumentOutOfRangeException()
//     {
//         // Arrange
//         int depth = 1;
//         int width = -1;
//         // Act & Assert
//         Assert.Throws<ArgumentOutOfRangeException>(() => InvokeBuildObject(depth, width));
//     }
// 
//     /// <summary>
//     /// Tests that BuildObject with a negative depth and zero width returns a JsonObject with an empty properties collection,
//     /// as the Enumerable.Repeat is not invoked when width is zero.
//     /// </summary>
// //     [Fact] [Error] (1945-24)CS1503 Argument 1: cannot convert from 'method group' to 'object?' [Error] (1946-22)CS1503 Argument 1: cannot convert from 'method group' to 'System.Collections.IEnumerable'
// //     public void BuildObject_NegativeDepthZeroWidth_ReturnsEmptyJsonObject()
// //     {
// //         // Arrange
// //         int depth = -1;
// //         int width = 0;
// //         // Act
// //         IJson result = InvokeBuildObject(depth, width);
// //         // Assert
// //         Assert.NotNull(result);
// //         Assert.IsType<JsonObject>(result);
// //         var jsonObject = result as JsonObject;
// //         Assert.NotNull(jsonObject.Properties);
// //         Assert.Empty(jsonObject.Properties);
// //     }
// 
//     /// <summary>
//     /// Tests that RandomString returns an empty string when the input length is zero.
//     /// </summary>
//     [Fact]
//     public void RandomString_LengthIsZero_ReturnsEmptyString()
//     {
//         // Arrange
//         int length = 0;
//         // Act
//         string result = JsonBench.RandomString(length);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(string.Empty, result);
//     }
// 
//     /// <summary>
//     /// Tests that RandomString returns a string of the correct length when positive length is provided.
//     /// </summary>
//     [Fact]
//     public void RandomString_PositiveLength_ReturnsStringOfCorrectLength()
//     {
//         // Arrange
//         int length = 10;
//         // Act
//         string result = JsonBench.RandomString(length);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal(length, result.Length);
//     }
// 
//     /// <summary>
//     /// Tests that RandomString throws an ArgumentOutOfRangeException when a negative length is provided.
//     /// This verifies that the underlying call to Enumerable.Repeat fails with an invalid count.
//     /// </summary>
//     [Fact]
//     public void RandomString_NegativeLength_ThrowsArgumentOutOfRangeException()
//     {
//         // Arrange
//         int length = -5;
//         // Act & Assert
//         Assert.Throws<ArgumentOutOfRangeException>(() => JsonBench.RandomString(length));
//     }
// }
