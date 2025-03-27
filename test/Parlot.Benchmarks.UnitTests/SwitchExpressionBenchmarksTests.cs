// using Moq;
// using Parlot.Benchmarks;
// using Parlot.Fluent;
// using System;
// using System.Reflection;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "SwitchExpressionBenchmarks"/> class.
// /// </summary>
// public class SwitchExpressionBenchmarksTests
// {
//     /// <summary>
//     /// Tests that the Setup method initializes private fields correctly when Length is set to its minimum value of 2.
//     /// It validates that the fluent and compiled parser fields are initialized, the match and miss strings are set,
//     /// and that the match and miss strings are not equal.
//     /// </summary>
// //     [Fact] [Error] (30-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (35-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (41-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (47-37)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void Setup_WithLength2_InitializesFieldsCorrectly()
// //     {
// //         // Arrange
// //         var benchmark = new SwitchExpressionBenchmarks();
// //         const int testLength = 2;
// //         benchmark.Length = testLength;
// //         // Act
// //         benchmark.Setup();
// //         // Assert - use reflection to validate private fields
// //         // Access _fluent field and check it is not null.
// //         Type type = typeof(SwitchExpressionBenchmarks);
// //         FieldInfo fluentField = type.GetField("_fluent", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(fluentField);
// //         var fluentValue = fluentField.GetValue(benchmark);
// //         Assert.NotNull(fluentValue);
// //         // Access _compiled field and check it is not null and not the same as _fluent.
// //         FieldInfo compiledField = type.GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(compiledField);
// //         var compiledValue = compiledField.GetValue(benchmark);
// //         Assert.NotNull(compiledValue);
// //         Assert.NotSame(fluentValue, compiledValue);
// //         // Access _matchString field, check it is a one-character string.
// //         FieldInfo matchStringField = type.GetField("_matchString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(matchStringField);
// //         var matchString = matchStringField.GetValue(benchmark) as string;
// //         Assert.False(string.IsNullOrEmpty(matchString));
// //         Assert.Equal(1, matchString.Length);
// //         // Access _missString field, check it is a one-character string.
// //         FieldInfo missStringField = type.GetField("_missString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(missStringField);
// //         var missString = missStringField.GetValue(benchmark) as string;
// //         Assert.False(string.IsNullOrEmpty(missString));
// //         Assert.Equal(1, missString.Length);
// //         // Ensure that the match and miss string are different.
// //         Assert.NotEqual(matchString, missString);
// //     }
// 
//     /// <summary>
//     /// Tests that the Setup method initializes private fields correctly when Length is set to its maximum value of 255.
//     /// It validates that the fluent and compiled parser fields are properly constructed, the match and miss strings
//     /// are assigned correctly, and that they differ from each other.
//     /// </summary>
// //     [Fact] [Error] (73-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (78-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (84-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (90-37)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void Setup_WithLength255_InitializesFieldsCorrectly()
// //     {
// //         // Arrange
// //         var benchmark = new SwitchExpressionBenchmarks();
// //         const int testLength = 255;
// //         benchmark.Length = testLength;
// //         // Act
// //         benchmark.Setup();
// //         // Assert - use reflection to validate private fields
// //         // Access _fluent field and check it is not null.
// //         Type type = typeof(SwitchExpressionBenchmarks);
// //         FieldInfo fluentField = type.GetField("_fluent", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(fluentField);
// //         var fluentValue = fluentField.GetValue(benchmark);
// //         Assert.NotNull(fluentValue);
// //         // Access _compiled field and check it is not null and distinct from _fluent.
// //         FieldInfo compiledField = type.GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(compiledField);
// //         var compiledValue = compiledField.GetValue(benchmark);
// //         Assert.NotNull(compiledValue);
// //         Assert.NotSame(fluentValue, compiledValue);
// //         // Access _matchString field and verify it is a valid single-character string.
// //         FieldInfo matchStringField = type.GetField("_matchString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(matchStringField);
// //         var matchString = matchStringField.GetValue(benchmark) as string;
// //         Assert.False(string.IsNullOrEmpty(matchString));
// //         Assert.Equal(1, matchString.Length);
// //         // Access _missString field and verify it is a valid single-character string.
// //         FieldInfo missStringField = type.GetField("_missString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(missStringField);
// //         var missString = missStringField.GetValue(benchmark) as string;
// //         Assert.False(string.IsNullOrEmpty(missString));
// //         Assert.Equal(1, missString.Length);
// //         // Ensure that the match and miss strings are not equal.
// //         Assert.NotEqual(matchString, missString);
// //     }
// 
//     /// <summary>
//     /// Tests the LookupMatchFluent method to ensure it returns the expected character when the parser functions correctly.
//     /// This test arranges a benchmark instance by injecting a mocked parser that returns a specified character,
//     /// then invokes the method and verifies that the returned value matches the expectation.
//     /// </summary>
// //     [Fact] [Error] (114-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (115-9)CS8602 Dereference of a possibly null reference. [Error] (117-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (119-9)CS8602 Dereference of a possibly null reference.
// //     public void LookupMatchFluent_HappyPath_ReturnsExpectedChar()
// //     {
// //         // Arrange
// //         var benchmarks = new SwitchExpressionBenchmarks();
// //         // Create a mock for the Parser<char> dependency.
// //         var parserMock = new Mock<Parser<char>>();
// //         char expectedChar = 'A';
// //         parserMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(expectedChar);
// //         // Use reflection to inject the mock into the private field _fluent.
// //         FieldInfo fluentField = typeof(SwitchExpressionBenchmarks).GetField("_fluent", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fluentField.SetValue(benchmarks, parserMock.Object);
// //         // Set the private field _matchString to a valid non-null value.
// //         FieldInfo matchStringField = typeof(SwitchExpressionBenchmarks).GetField("_matchString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         string testInput = "dummy";
// //         matchStringField.SetValue(benchmarks, testInput);
// //         // Act
// //         char result = benchmarks.LookupMatchFluent();
// //         // Assert
// //         Assert.Equal(expectedChar, result);
// //     }
// 
//     /// <summary>
//     /// Tests the LookupMatchFluent method to ensure it throws a NullReferenceException when the fluent parser is not initialized.
//     /// This test arranges a benchmark instance with a null _fluent, sets a valid _matchString, and then verifies that the method invocation fails.
//     /// </summary>
// //     [Fact] [Error] (136-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (137-9)CS8602 Dereference of a possibly null reference. [Error] (139-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (140-9)CS8602 Dereference of a possibly null reference.
// //     public void LookupMatchFluent_WhenFluentParserIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new SwitchExpressionBenchmarks();
// //         // Set the private field _matchString to a valid non-null value.
// //         FieldInfo matchStringField = typeof(SwitchExpressionBenchmarks).GetField("_matchString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         matchStringField.SetValue(benchmarks, "dummy");
// //         // Ensure _fluent is explicitly set to null.
// //         FieldInfo fluentField = typeof(SwitchExpressionBenchmarks).GetField("_fluent", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fluentField.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.LookupMatchFluent());
// //     }
// 
//     /// <summary>
//     /// Tests the LookupMatchFluent method to ensure it throws an ArgumentNullException when _matchString is null and the parser is configured to throw on null input.
//     /// This test arranges a benchmark instance by injecting a mocked parser that throws an ArgumentNullException when its Parse method is called with null,
//     /// then sets _matchString to null and verifies that the appropriate exception is thrown.
//     /// </summary>
// //     [Fact] [Error] (159-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (160-9)CS8602 Dereference of a possibly null reference. [Error] (162-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (163-9)CS8602 Dereference of a possibly null reference.
// //     public void LookupMatchFluent_WhenMatchStringIsNull_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var benchmarks = new SwitchExpressionBenchmarks();
// //         // Create a mock for Parser<char> that throws ArgumentNullException when the input string is null.
// //         var parserMock = new Mock<Parser<char>>();
// //         parserMock.Setup(x => x.Parse(It.Is<string>(s => s == null))).Throws(new ArgumentNullException("input"));
// //         // Inject the mocked parser into the private field _fluent.
// //         FieldInfo fluentField = typeof(SwitchExpressionBenchmarks).GetField("_fluent", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fluentField.SetValue(benchmarks, parserMock.Object);
// //         // Set the private field _matchString to null.
// //         FieldInfo matchStringField = typeof(SwitchExpressionBenchmarks).GetField("_matchString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         matchStringField.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => benchmarks.LookupMatchFluent());
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMatchCompiled returns the expected character when _compiled and _matchString are properly set.
//     /// </summary>
//     [Fact]
//     public void LookupMatchCompiled_WhenCalledWithValidParserAndMatchString_ReturnsExpectedChar()
//     {
//         // Arrange
//         var benchmarksInstance = new SwitchExpressionBenchmarks();
//         // Create a mock for Parser<char> and set it up to return 'X' for any input.
//         var mockParser = new Mock<Parser<char>>();
//         mockParser.Setup(p => p.Parse(It.IsAny<string>())).Returns('X');
//         // Set the private field _compiled with the mock object.
//         SetPrivateField(benchmarksInstance, "_compiled", mockParser.Object);
//         // Set the private field _matchString with a valid non-null string.
//         SetPrivateField(benchmarksInstance, "_matchString", "test input");
//         // Act
//         char result = benchmarksInstance.LookupMatchCompiled();
//         // Assert
//         Assert.Equal('X', result);
//     }
// 
//     /// <summary>
//     /// Tests that LookupMatchCompiled throws a NullReferenceException when the _compiled field is null.
//     /// </summary>
// //     [Fact] [Error] (198-58)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void LookupMatchCompiled_WhenCompiledParserIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarksInstance = new SwitchExpressionBenchmarks();
// //         // Set _compiled to null.
// //         SetPrivateField(benchmarksInstance, "_compiled", null);
// //         // Set _matchString with a valid string.
// //         SetPrivateField(benchmarksInstance, "_matchString", "test input");
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarksInstance.LookupMatchCompiled());
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMatchCompiled propagates the exception thrown by the _compiled parser when _matchString is null.
//     /// </summary>
// //     [Fact] [Error] (215-33)CS0121 The call is ambiguous between the following methods or properties: 'Parser<T>.Parse(string)' and 'Parser<T>.Parse(ParseContext)' [Error] (220-61)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void LookupMatchCompiled_WhenMatchStringIsNull_PropagatesArgumentNullException()
// //     {
// //         // Arrange
// //         var benchmarksInstance = new SwitchExpressionBenchmarks();
// //         // Create a mock for Parser<char> that throws ArgumentNullException when Parse is called with null.
// //         var mockParser = new Mock<Parser<char>>();
// //         mockParser.Setup(p => p.Parse(null)).Throws(new ArgumentNullException());
// //         // Also, if any non-null value is passed, just return a default value.
// //         mockParser.Setup(p => p.Parse(It.IsNotNull<string>())).Returns('Y');
// //         // Set the private fields.
// //         SetPrivateField(benchmarksInstance, "_compiled", mockParser.Object);
// //         SetPrivateField(benchmarksInstance, "_matchString", null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => benchmarksInstance.LookupMatchCompiled());
// //     }
// 
//     /// <summary>
//     /// Helper method to set a private field's value via reflection.
//     /// </summary>
//     /// <param name = "instance">The instance whose field is to be set.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to assign to the field.</param>
// //     private static void SetPrivateField(object instance, string fieldName, object value) [Error] (239-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         if (instance == null)
// //         {
// //             throw new ArgumentNullException(nameof(instance));
// //         }
// // 
// //         Type type = instance.GetType();
// //         FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException($"Field '{fieldName}' not found in type '{type.FullName}'.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMissFluent returns the expected character when _missString is a valid non-empty string.
//     /// </summary>
// //     [Fact] [Error] (257-41)CS1660 Cannot convert lambda expression to type 'Parser<char>' because it is not a delegate type
// //     public void LookupMissFluent_ValidMissString_ReturnsExpectedCharacter()
// //     {
// //         // Arrange
// //         var benchmarks = new SwitchExpressionBenchmarks();
// //         // Setup _fluent with a lambda that returns 'x' when input equals "test"
// //         Parser<char> testParser = input =>
// //         {
// //             if (input == "test")
// //             {
// //                 return 'x';
// //             }
// // 
// //             return 'y';
// //         };
// //         SetPrivateField(benchmarks, "_fluent", testParser);
// //         SetPrivateField(benchmarks, "_missString", "test");
// //         // Act
// //         char result = benchmarks.LookupMissFluent();
// //         // Assert
// //         Assert.Equal('x', result);
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMissFluent throws a NullReferenceException when _fluent is null.
//     /// </summary>
// //     [Fact] [Error] (283-62)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void LookupMissFluent_FluentIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new SwitchExpressionBenchmarks();
// //         // Set _fluent to null to simulate missing dependency
// //         SetPrivateField<Parser<char>>(benchmarks, "_fluent", null);
// //         SetPrivateField(benchmarks, "_missString", "any");
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.LookupMissFluent());
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMissFluent returns the expected character when _missString is null.
//     /// </summary>
// //     [Fact] [Error] (298-41)CS1660 Cannot convert lambda expression to type 'Parser<char>' because it is not a delegate type [Error] (300-60)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void LookupMissFluent_MissStringIsNull_ReturnsExpectedCharacterBasedOnParserBehavior()
// //     {
// //         // Arrange
// //         var benchmarks = new SwitchExpressionBenchmarks();
// //         // Setup _fluent with a lambda that returns 'z' irrespective of input
// //         Parser<char> testParser = input => 'z';
// //         SetPrivateField(benchmarks, "_fluent", testParser);
// //         SetPrivateField<string>(benchmarks, "_missString", null);
// //         // Act
// //         char result = benchmarks.LookupMissFluent();
// //         // Assert
// //         Assert.Equal('z', result);
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMissFluent returns the expected character when _missString is empty.
//     /// </summary>
// //     [Fact] [Error] (316-41)CS1660 Cannot convert lambda expression to type 'Parser<char>' because it is not a delegate type
// //     public void LookupMissFluent_MissStringEmpty_ReturnsExpectedCharacterBasedOnParserBehavior()
// //     {
// //         // Arrange
// //         var benchmarks = new SwitchExpressionBenchmarks();
// //         // Setup _fluent with a lambda that returns 'e' if the input is an empty string.
// //         Parser<char> testParser = input =>
// //         {
// //             if (input == string.Empty)
// //             {
// //                 return 'e';
// //             }
// // 
// //             return 'n';
// //         };
// //         SetPrivateField(benchmarks, "_fluent", testParser);
// //         SetPrivateField(benchmarks, "_missString", string.Empty);
// //         // Act
// //         char result = benchmarks.LookupMissFluent();
// //         // Assert
// //         Assert.Equal('e', result);
// //     }
// 
//     /// <summary>
//     /// Helper method to set a private field using reflection.
//     /// </summary>
//     /// <typeparam name = "T">The type of the field to set.</typeparam>
//     /// <param name = "obj">The object instance containing the private field.</param>
//     /// <param name = "fieldName">The name of the private field.</param>
//     /// <param name = "value">The value to assign to the field.</param>
// //     private static void SetPrivateField<T>(object obj, string fieldName, T value) [Error] (342-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new ArgumentException($"Field '{fieldName}' not found in type '{obj.GetType().FullName}'.");
// //         }
// // 
// //         field.SetValue(obj, value);
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMissCompiled returns the expected character when _missString is a valid non-empty string.
//     /// </summary>
// //     [Fact] [Error] (364-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (365-9)CS8602 Dereference of a possibly null reference. [Error] (367-37)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (368-9)CS8602 Dereference of a possibly null reference.
// //     public void LookupMissCompiled_ValidMissString_ReturnsExpectedChar()
// //     {
// //         // Arrange
// //         var benchmarkInstance = new SwitchExpressionBenchmarks();
// //         string testMissString = "abc";
// //         char expectedChar = 'Z';
// //         var parserMock = new Mock<Parser<char>>();
// //         parserMock.Setup(p => p.Parse(testMissString)).Returns(expectedChar);
// //         // Set the private field _compiled via reflection
// //         FieldInfo compiledField = typeof(SwitchExpressionBenchmarks).GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         compiledField.SetValue(benchmarkInstance, parserMock.Object);
// //         // Set the private field _missString via reflection
// //         FieldInfo missStringField = typeof(SwitchExpressionBenchmarks).GetField("_missString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         missStringField.SetValue(benchmarkInstance, testMissString);
// //         // Act
// //         char actualResult = benchmarkInstance.LookupMissCompiled();
// //         // Assert
// //         Assert.Equal(expectedChar, actualResult);
// //         parserMock.Verify(p => p.Parse(testMissString), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMissCompiled returns the expected character when _missString is an empty string.
//     /// </summary>
// //     [Fact] [Error] (389-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (390-9)CS8602 Dereference of a possibly null reference. [Error] (392-37)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (393-9)CS8602 Dereference of a possibly null reference.
// //     public void LookupMissCompiled_EmptyMissString_ReturnsExpectedChar()
// //     {
// //         // Arrange
// //         var benchmarkInstance = new SwitchExpressionBenchmarks();
// //         string testMissString = string.Empty;
// //         char expectedChar = 'E';
// //         var parserMock = new Mock<Parser<char>>();
// //         parserMock.Setup(p => p.Parse(testMissString)).Returns(expectedChar);
// //         // Set the private field _compiled via reflection
// //         FieldInfo compiledField = typeof(SwitchExpressionBenchmarks).GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         compiledField.SetValue(benchmarkInstance, parserMock.Object);
// //         // Set the private field _missString via reflection
// //         FieldInfo missStringField = typeof(SwitchExpressionBenchmarks).GetField("_missString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         missStringField.SetValue(benchmarkInstance, testMissString);
// //         // Act
// //         char actualResult = benchmarkInstance.LookupMissCompiled();
// //         // Assert
// //         Assert.Equal(expectedChar, actualResult);
// //         parserMock.Verify(p => p.Parse(testMissString), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests that LookupMissCompiled throws an ArgumentNullException when _missString is null.
//     /// </summary>
// //     [Fact] [Error] (409-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (411-39)CS8604 Possible null reference argument for parameter 'text' in 'char Parser<char>.Parse(string text)'. [Error] (413-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (414-9)CS8602 Dereference of a possibly null reference. [Error] (416-37)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (417-9)CS8602 Dereference of a possibly null reference. [Error] (420-40)CS8604 Possible null reference argument for parameter 'text' in 'char Parser<char>.Parse(string text)'.
// //     public void LookupMissCompiled_NullMissString_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var benchmarkInstance = new SwitchExpressionBenchmarks();
// //         string testMissString = null;
// //         var parserMock = new Mock<Parser<char>>();
// //         parserMock.Setup(p => p.Parse(testMissString)).Throws(new ArgumentNullException(nameof(testMissString)));
// //         // Set the private field _compiled via reflection
// //         FieldInfo compiledField = typeof(SwitchExpressionBenchmarks).GetField("_compiled", BindingFlags.Instance | BindingFlags.NonPublic);
// //         compiledField.SetValue(benchmarkInstance, parserMock.Object);
// //         // Set the private field _missString via reflection
// //         FieldInfo missStringField = typeof(SwitchExpressionBenchmarks).GetField("_missString", BindingFlags.Instance | BindingFlags.NonPublic);
// //         missStringField.SetValue(benchmarkInstance, testMissString);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => benchmarkInstance.LookupMissCompiled());
// //         parserMock.Verify(p => p.Parse(testMissString), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests that the Length property is initialized to its default value.
//     /// Since Length is an auto-property, its default value should be 0.
//     /// </summary>
//     [Fact]
//     public void Length_DefaultValue_ReturnsZero()
//     {
//         // Arrange
//         var benchmark = new SwitchExpressionBenchmarks();
//         // Act
//         int actualValue = benchmark.Length;
//         // Assert
//         Assert.Equal(0, actualValue);
//     }
// 
//     /// <summary>
//     /// Tests that the Length property correctly reflects the assigned value
//     /// by setting various test values including edge cases.
//     /// </summary>
//     /// <param name = "value">The value to be assigned to the Length property.</param>
//     [Theory]
//     [InlineData(2)]
//     [InlineData(255)]
//     [InlineData(0)]
//     [InlineData(-1)]
//     [InlineData(1000)]
//     public void Length_SetAndGet_ReturnsSameValue(int value)
//     {
//         // Arrange
//         var benchmark = new SwitchExpressionBenchmarks();
//         // Act
//         benchmark.Length = value;
//         int actualValue = benchmark.Length;
//         // Assert
//         Assert.Equal(value, actualValue);
//     }
// }
