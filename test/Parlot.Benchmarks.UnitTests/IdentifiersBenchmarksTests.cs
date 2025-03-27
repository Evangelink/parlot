// using Moq;
// using Parlot.Benchmarks;
// using System;
// using System.Reflection;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "IdentifiersBenchmarks"/> class.
// /// </summary>
// public class IdentifiersBenchmarksTests
// {
//     /// <summary>
//     /// Sets the private static readonly _identifier1 field on the IdentifiersBenchmarks class via reflection.
//     /// </summary>
//     /// <param name = "value">The string value to set for _identifier1.</param>
// //     private static void SetIdentifier1(string value) [Error] (18-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = typeof(IdentifiersBenchmarks).GetField("_identifier1", BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("Field '_identifier1' not found.");
// //         }
// // 
// //         // For readonly fields, we can set the value using reflection.
// //         field.SetValue(null, value);
// //     }
// 
//     /// <summary>
//     /// Tests NaiveIdentifierMatch when the identifier is completely valid.
//     /// Expectation: returns -1 indicating the entire string is a valid identifier.
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierMatch_WhenIdentifierIsValid_ReturnsMinusOne()
//     {
//         // Arrange
//         // "Aabc" is a valid identifier assuming letters are valid identifier start and parts.
//         string validIdentifier = "Aabc";
//         SetIdentifier1(validIdentifier);
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.NaiveIdentifierMatch();
//         // Assert
//         Assert.Equal(-1, result);
//     }
// 
//     /// <summary>
//     /// Tests NaiveIdentifierMatch when the first character is invalid for an identifier.
//     /// Expectation: returns -1 immediately since the first character is not valid.
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierMatch_WhenFirstCharIsInvalid_ReturnsMinusOne()
//     {
//         // Arrange
//         // "1abc": '1' is assumed to be invalid as an identifier start.
//         string invalidStartIdentifier = "1abc";
//         SetIdentifier1(invalidStartIdentifier);
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.NaiveIdentifierMatch();
//         // Assert
//         Assert.Equal(-1, result);
//     }
// 
//     /// <summary>
//     /// Tests NaiveIdentifierMatch when an invalid character is encountered in the middle of the identifier.
//     /// Expectation: returns the index of the first invalid character.
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierMatch_WhenMiddleCharIsInvalid_ReturnsIndexOfInvalidChar()
//     {
//         // Arrange
//         // "Ab#c": 'A' is valid, 'b' is valid, '#' is assumed invalid for identifier parts.
//         string identifierWithInvalidMidChar = "Ab#c";
//         SetIdentifier1(identifierWithInvalidMidChar);
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         int expectedIndex = 2; // The '#' is at index 2.
//         // Act
//         int result = benchmarks.NaiveIdentifierMatch();
//         // Assert
//         Assert.Equal(expectedIndex, result);
//     }
// 
//     /// <summary>
//     /// Tests NaiveIdentifierMatch when the identifier is a single valid character.
//     /// Expectation: returns -1 as a single valid identifier remains valid.
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierMatch_WhenSingleValidCharacter_ReturnsMinusOne()
//     {
//         // Arrange
//         string singleCharIdentifier = "A"; // Valid single character.
//         SetIdentifier1(singleCharIdentifier);
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.NaiveIdentifierMatch();
//         // Assert
//         Assert.Equal(-1, result);
//     }
// 
//     /// <summary>
//     /// Tests NaiveIdentifierMatch when the identifier string is empty.
//     /// Expectation: throws an IndexOutOfRangeException when attempting to access _identifier1[0].
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierMatch_WhenIdentifierIsEmpty_ThrowsIndexOutOfRangeException()
//     {
//         // Arrange
//         string emptyIdentifier = "";
//         SetIdentifier1(emptyIdentifier);
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         // Act & Assert
//         Assert.Throws<IndexOutOfRangeException>(() => benchmarks.NaiveIdentifierMatch());
//     }
// 
//     /// <summary>
//     /// Tests NaiveIdentifierMatch when the identifier is null.
//     /// Expectation: throws a NullReferenceException when attempting to access _identifier1[0].
//     /// </summary>
// //     [Fact] [Error] (123-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (124-24)CS8604 Possible null reference argument for parameter 'value' in 'void IdentifiersBenchmarksTests.SetIdentifier1(string value)'.
// //     public void NaiveIdentifierMatch_WhenIdentifierIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         string nullIdentifier = null;
// //         SetIdentifier1(nullIdentifier);
// //         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.NaiveIdentifierMatch());
// //     }
// 
//     /// <summary>
//     /// Sets a private static field value via reflection.
//     /// </summary>
//     /// <param name = "type">Type containing the field.</param>
//     /// <param name = "fieldName">Name of the field.</param>
//     /// <param name = "value">Value to set.</param>
// //     private static void SetStaticField(Type type, string fieldName, object value) [Error] (138-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException($"Field '{fieldName}' not found in type '{type.FullName}'.");
// //         }
// // 
// //         field.SetValue(null, value);
// //     }
// 
//     /// <summary>
//     /// Configures the static fields of IdentifiersBenchmarks and SearchValuesHelper for testing.
//     /// </summary>
//     /// <param name = "identifierValue">The value to assign to IdentifiersBenchmarks._identifier1.</param>
//     /// <param name = "identifierStart">The value to assign to SearchValuesHelper._identifierStart.</param>
//     /// <param name = "identifierPart">The value to assign to SearchValuesHelper._identifierPart.</param>
// //     private static void ConfigureStaticFields(string identifierValue, string identifierStart, string identifierPart) [Error] (158-33)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         // Set IdentifiersBenchmarks._identifier1
// //         SetStaticField(typeof(IdentifiersBenchmarks), "_identifier1", identifierValue);
// //         // Get the SearchValuesHelper type
// //         Type searchHelperType = typeof(IdentifiersBenchmarks).Assembly.GetType("Parlot.Benchmarks.SearchValuesHelper");
// //         if (searchHelperType == null)
// //         {
// //             throw new InvalidOperationException("Type 'Parlot.Benchmarks.SearchValuesHelper' not found.");
// //         }
// // 
// //         // Set SearchValuesHelper._identifierStart and _identifierPart
// //         SetStaticField(searchHelperType, "_identifierStart", identifierStart);
// //         SetStaticField(searchHelperType, "_identifierPart", identifierPart);
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIdentifierMatch returns -1 when the first character of the identifier is not a valid starting character.
//     /// The expected behavior is to return -1.
//     /// </summary>
//     [Fact]
//     public void SearchValuesIdentifierMatch_InvalidStart_ReturnsMinusOne()
//     {
//         // Arrange
//         // Configure valid starting characters as "abc" and valid part as "123"
//         // Use an identifier that starts with 'x' which is not in "abc"
//         string testIdentifier = "x123";
//         ConfigureStaticFields(testIdentifier, "abc", "123");
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.SearchValuesIdentifierMatch();
//         // Assert
//         Assert.Equal(-1, result);
//     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIdentifierMatch returns the correct index when a valid identifier has an invalid character in its subsequent part.
//     /// For identifier "a1123d" with allowed start "abc" and allowed part "123", the first invalid character in the part is at index 4,
//     /// so the method should return 5 (4 + 1).
//     /// </summary>
//     [Fact]
//     public void SearchValuesIdentifierMatch_ValidStartWithInvalidPartCharacter_ReturnsIndexPlusOne()
//     {
//         // Arrange
//         // Configure valid starting characters as "abc" and valid part as "123"
//         string testIdentifier = "a1123d";
//         ConfigureStaticFields(testIdentifier, "abc", "123");
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.SearchValuesIdentifierMatch();
//         // Assert
//         // In "a1123d", after the first character, "1123d" is processed.
//         // '1', '1', '2', and '3' are allowed (since allowed part is "123"), 
//         // and 'd' is invalid, which is at index 4 in the sliced span, so expected result = 4 + 1 = 5.
//         Assert.Equal(5, result);
//     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIdentifierMatch returns 0 when the entire identifier (after the first character) is valid.
//     /// For identifier "a123" with allowed start "abc" and allowed part "123", all characters after the first are valid,
//     /// so IndexOfAnyExcept returns -1, and the method should return 0.
//     /// </summary>
//     [Fact]
//     public void SearchValuesIdentifierMatch_FullyValidIdentifier_ReturnsZero()
//     {
//         // Arrange
//         // Configure valid starting characters as "abc" and valid part as "123"
//         string testIdentifier = "a123";
//         ConfigureStaticFields(testIdentifier, "abc", "123");
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.SearchValuesIdentifierMatch();
//         // Assert
//         // All characters after the first are valid, hence result is -1 + 1 = 0.
//         Assert.Equal(0, result);
//     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIdentifierMatch throws an exception when the identifier string is empty.
//     /// Accessing _identifier1[0] on an empty string should trigger an IndexOutOfRangeException.
//     /// </summary>
//     [Fact]
//     public void SearchValuesIdentifierMatch_EmptyIdentifier_ThrowsIndexOutOfRangeException()
//     {
//         // Arrange
//         // Configure valid start and part as arbitrary values; identifier is an empty string.
//         string testIdentifier = "";
//         ConfigureStaticFields(testIdentifier, "abc", "123");
//         IdentifiersBenchmarks benchmarks = new IdentifiersBenchmarks();
//         // Act & Assert
//         Assert.Throws<IndexOutOfRangeException>(() => benchmarks.SearchValuesIdentifierMatch());
//     }
// 
//     /// <summary>
//     /// Tests that when the first character of _identifier1 is not contained in SearchValuesHelper._identifierStart,
//     /// the method NaiveIdentifierAndContainsMatch returns -1.
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierAndContainsMatch_FirstCharNotValid_ReturnsMinusOne()
//     {
//         // Arrange
//         // Set _identifier1 to a value whose first character is not valid according to _identifierStart.
//         SetPrivateStaticField(typeof(IdentifiersBenchmarks), "_identifier1", "abc");
//         // Configure SearchValuesHelper so that its _identifierStart does NOT contain the first char 'a'.
//         SetPrivateStaticField(typeof(SearchValuesHelper), "_identifierStart", "z");
//         // The _identifierPart is arbitrary here since first char check fails.
//         SetPrivateStaticField(typeof(SearchValuesHelper), "_identifierPart", "abc");
//         var benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.NaiveIdentifierAndContainsMatch();
//         // Assert
//         Assert.Equal(-1, result);
//     }
// 
//     /// <summary>
//     /// Tests that when _identifier1 is fully valid (all characters are contained in the appropriate sets),
//     /// the method NaiveIdentifierAndContainsMatch returns -1, indicating a full match.
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierAndContainsMatch_AllCharsValid_ReturnsMinusOne()
//     {
//         // Arrange
//         // Set _identifier1 to a valid identifier.
//         SetPrivateStaticField(typeof(IdentifiersBenchmarks), "_identifier1", "a123");
//         // Configure SearchValuesHelper so that the first character 'a' is valid.
//         SetPrivateStaticField(typeof(SearchValuesHelper), "_identifierStart", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_");
//         // Configure _identifierPart to contain valid identifier characters.
//         SetPrivateStaticField(typeof(SearchValuesHelper), "_identifierPart", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_0123456789");
//         var benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.NaiveIdentifierAndContainsMatch();
//         // Assert
//         Assert.Equal(-1, result);
//     }
// 
//     /// <summary>
//     /// Tests that when a character (other than the first) in _identifier1 is not contained in SearchValuesHelper._identifierPart,
//     /// the method NaiveIdentifierAndContainsMatch returns the index at which the invalid character occurs.
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierAndContainsMatch_InvalidCharAtIndex_ReturnsIndex()
//     {
//         // Arrange
//         // Set _identifier1 with an invalid character at index 2.
//         SetPrivateStaticField(typeof(IdentifiersBenchmarks), "_identifier1", "a1?3");
//         // Configure SearchValuesHelper so that the first character is valid.
//         SetPrivateStaticField(typeof(SearchValuesHelper), "_identifierStart", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_");
//         // Configure _identifierPart to exclude the '?' character.
//         SetPrivateStaticField(typeof(SearchValuesHelper), "_identifierPart", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_0123456789");
//         var benchmarks = new IdentifiersBenchmarks();
//         // Act
//         int result = benchmarks.NaiveIdentifierAndContainsMatch();
//         // Assert
//         // Expecting index 2 as the position where '?' is encountered.
//         Assert.Equal(2, result);
//     }
// 
//     /// <summary>
//     /// Tests that when _identifier1 is an empty string, accessing the first character results in an exception.
//     /// </summary>
//     [Fact]
//     public void NaiveIdentifierAndContainsMatch_EmptyIdentifier_ThrowsException()
//     {
//         // Arrange
//         // Set _identifier1 to an empty string.
//         SetPrivateStaticField(typeof(IdentifiersBenchmarks), "_identifier1", string.Empty);
//         // Configure SearchValuesHelper with arbitrary valid values.
//         SetPrivateStaticField(typeof(SearchValuesHelper), "_identifierStart", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_");
//         SetPrivateStaticField(typeof(SearchValuesHelper), "_identifierPart", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_0123456789");
//         var benchmarks = new IdentifiersBenchmarks();
//         // Act & Assert
//         Assert.Throws<IndexOutOfRangeException>(() => benchmarks.NaiveIdentifierAndContainsMatch());
//     }
// 
//     /// <summary>
//     /// Helper method to set the value of a private static field using reflection.
//     /// </summary>
//     /// <param name = "type">The type containing the private static field.</param>
//     /// <param name = "fieldName">The name of the field to set.</param>
//     /// <param name = "value">The value to assign to the field.</param>
// //     private static void SetPrivateStaticField(Type type, string fieldName, object value) [Error] (335-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new ArgumentException($"Field '{fieldName}' was not found in type '{type.FullName}'.");
// //         }
// // 
// //         field.SetValue(null, value);
// //     }
// 
//     /// <summary>
//     /// Tests that the constructor of <see cref = "IdentifiersBenchmarks"/> completes without throwing an exception when all benchmark method results match the expected length.
//     /// Since the benchmark methods are stubbed out and default to returning 0 and the static field _length is 0 by default,
//     /// the construction should succeed.
//     /// </summary>
// //     [Fact] [Error] (354-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (355-32)CS8602 Dereference of a possibly null reference. [Error] (355-32)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void Constructor_DefaultConditions_CompletesWithoutException()
// //     {
// //         // Arrange
// //         // Ensure _length is set to 0 (default value) by resetting via reflection.
// //         FieldInfo lengthField = typeof(IdentifiersBenchmarks).GetField("_length", BindingFlags.Static | BindingFlags.NonPublic);
// //         object originalValue = lengthField.GetValue(null);
// //         try
// //         {
// //             lengthField.SetValue(null, 0);
// //             // Act & Assert
// //             var exception = Record.Exception(() => new IdentifiersBenchmarks());
// //             Assert.Null(exception);
// //         }
// //         finally
// //         {
// //             // Restore the original value to avoid side-effects for other tests.
// //             lengthField.SetValue(null, originalValue);
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests that the constructor of <see cref = "IdentifiersBenchmarks"/> throws an <see cref = "InvalidOperationException"/>
//     /// when the result of NaiveIdentifierMatch does not equal the expected _length.
//     /// This is simulated by using reflection to modify the static readonly field _length to a value (e.g., 1) different from the default 0.
//     /// The exception message should be "NaiveIdentifierMatch".
//     /// </summary>
// //     [Fact] [Error] (380-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (381-32)CS8602 Dereference of a possibly null reference. [Error] (381-32)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void Constructor_NaiveIdentifierMatchMismatch_ThrowsInvalidOperationException()
// //     {
// //         // Arrange
// //         FieldInfo lengthField = typeof(IdentifiersBenchmarks).GetField("_length", BindingFlags.Static | BindingFlags.NonPublic);
// //         object originalValue = lengthField.GetValue(null);
// //         try
// //         {
// //             // Set _length to 1 so that benchmark methods (which return 0 by default) do not match.
// //             lengthField.SetValue(null, 1);
// //             // Act & Assert
// //             InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => new IdentifiersBenchmarks());
// //             Assert.Equal("NaiveIdentifierMatch", exception.Message);
// //         }
// //         finally
// //         {
// //             // Restore the original _length value.
// //             lengthField.SetValue(null, originalValue);
// //         }
// //     }
// }
