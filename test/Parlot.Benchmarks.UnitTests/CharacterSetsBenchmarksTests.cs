// using Moq;
// using Parlot.Benchmarks;
// using System;
// using System.Reflection;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "CharacterSetsBenchmarks"/> class.
// /// </summary>
// public class CharacterSetsBenchmarksTests
// {
//     /// <summary>
//     /// Tests that Character_IsIdentifierPart_True returns true when _identifier1 starts with a valid identifier character.
//     /// This test manually sets the private static field _identifier1 to a valid identifier string ("a") using reflection.
//     /// Expected outcome: The method returns true.
//     /// </summary>
//     [Fact]
//     public void Character_IsIdentifierPart_True_ValidIdentifier_ReturnsTrue()
//     {
//         // Arrange
//         SetPrivateStaticField("_identifier1", "a");
//         var benchmarks = new CharacterSetsBenchmarks();
//         // Act
//         bool result = benchmarks.Character_IsIdentifierPart_True();
//         // Assert
//         Assert.True(result, "Expected true when the first character is a valid identifier part.");
//     }
// 
//     /// <summary>
//     /// Tests that Character_IsIdentifierPart_True throws an IndexOutOfRangeException when _identifier1 is an empty string.
//     /// This test manually sets the private static field _identifier1 to an empty string using reflection.
//     /// Expected outcome: An IndexOutOfRangeException is thrown as accessing the first character of an empty string is invalid.
//     /// </summary>
//     [Fact]
//     public void Character_IsIdentifierPart_True_EmptyIdentifier_ThrowsIndexOutOfRangeException()
//     {
//         // Arrange
//         SetPrivateStaticField("_identifier1", string.Empty);
//         var benchmarks = new CharacterSetsBenchmarks();
//         // Act & Assert
//         Assert.Throws<IndexOutOfRangeException>(() => benchmarks.Character_IsIdentifierPart_True());
//     }
// 
//     /// <summary>
//     /// Tests that Character_IsIdentifierPart_True throws a NullReferenceException when _identifier1 is null.
//     /// This test manually sets the private static field _identifier1 to null using reflection.
//     /// Expected outcome: A NullReferenceException is thrown because attempting to access an index on a null string is invalid.
//     /// </summary>
// //     [Fact] [Error] (53-47)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void Character_IsIdentifierPart_True_NullIdentifier_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         SetPrivateStaticField("_identifier1", null);
// //         var benchmarks = new CharacterSetsBenchmarks();
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.Character_IsIdentifierPart_True());
// //     }
// 
//     /// <summary>
//     /// Helper method to set a private static field in the CharacterSetsBenchmarks class using reflection.
//     /// </summary>
//     /// <param name = "fieldName">The name of the private static field to set.</param>
//     /// <param name = "value">The value to assign to the field.</param>
//     private static void SetPrivateStaticField(string fieldName, object value)
//     {
//         var type = typeof(CharacterSetsBenchmarks);
//         var fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
//         if (fieldInfo == null)
//         {
//             throw new InvalidOperationException($"Field '{fieldName}' not found in type '{type.FullName}'.");
//         }
// 
//         fieldInfo.SetValue(null, value);
//     }
// 
//     /// <summary>
//     /// Tests that Character_IsIdentifierPart_False returns false when _identifier2 contains a non-identifier character.
//     /// Expected outcome: returns false.
//     /// </summary>
//     [Fact]
//     public void Character_IsIdentifierPart_False_WithNonIdentifierChar_ReturnsFalse()
//     {
//         // Arrange
//         SetStaticIdentifier2("!");
//         var benchmarks = new CharacterSetsBenchmarks();
//         // Act
//         bool result = benchmarks.Character_IsIdentifierPart_False();
//         // Assert
//         Assert.False(result, "Expected result to be false for a non-identifier character.");
//     }
// 
//     /// <summary>
//     /// Tests that Character_IsIdentifierPart_False returns true when _identifier2 contains a valid identifier character.
//     /// Expected outcome: returns true.
//     /// </summary>
//     [Fact]
//     public void Character_IsIdentifierPart_False_WithValidIdentifierChar_ReturnsTrue()
//     {
//         // Arrange
//         SetStaticIdentifier2("A");
//         var benchmarks = new CharacterSetsBenchmarks();
//         // Act
//         bool result = benchmarks.Character_IsIdentifierPart_False();
//         // Assert
//         Assert.True(result, "Expected result to be true for a valid identifier character.");
//     }
// 
//     /// <summary>
//     /// Tests that Character_IsIdentifierPart_False throws a NullReferenceException when _identifier2 is null.
//     /// Expected outcome: throws NullReferenceException.
//     /// </summary>
// //     [Fact] [Error] (116-30)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void Character_IsIdentifierPart_False_WithNullIdentifierField_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         SetStaticIdentifier2(null);
// //         var benchmarks = new CharacterSetsBenchmarks();
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.Character_IsIdentifierPart_False());
// //     }
// 
//     /// <summary>
//     /// Tests that Character_IsIdentifierPart_False throws an IndexOutOfRangeException when _identifier2 is an empty string.
//     /// Expected outcome: throws IndexOutOfRangeException.
//     /// </summary>
//     [Fact]
//     public void Character_IsIdentifierPart_False_WithEmptyIdentifierField_ThrowsIndexOutOfRangeException()
//     {
//         // Arrange
//         SetStaticIdentifier2(string.Empty);
//         var benchmarks = new CharacterSetsBenchmarks();
//         // Act & Assert
//         Assert.Throws<IndexOutOfRangeException>(() => benchmarks.Character_IsIdentifierPart_False());
//     }
// 
//     /// <summary>
//     /// Helper method to set the private static field _identifier2 via reflection.
//     /// </summary>
//     /// <param name = "value">The value to assign to _identifier2.</param>
// //     private static void SetStaticIdentifier2(string value) [Error] (142-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = typeof(CharacterSetsBenchmarks).GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("Field '_identifier2' not found in CharacterSetsBenchmarks.");
// //         }
// // 
// //         field.SetValue(null, value);
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIndexOfAny_IsIdentifierPart_True returns true when the first character of _identifier1 is in the _identifierPart set.
//     /// </summary>
// //     [Fact] [Error] (166-23)CS0120 An object reference is required for the non-static field, method, or property 'CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True()'
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_True_FirstCharIsInSet_ReturnsTrue()
// //     {
// //         // Arrange
// //         // Set _identifier1 to a string that begins with a character contained in _identifierPart.
// //         // For testing, assume that a valid SearchValues<char> instance can be created from a char array.
// //         // Here, we assume _identifierPart contains the characters 'a', 'b', and 'c'.
// //         string testString = "abcXYZ";
// //         var identifierSet = CreateSearchValues(new[] { 'a', 'b', 'c' });
// //         SetStaticField("_identifier1", testString);
// //         SetStaticField("_identifierPart", identifierSet);
// //         // Act
// //         bool result = CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True();
// //         // Assert
// //         Assert.True(result, "Expected the method to return true when the first character of _identifier1 is in _identifierPart.");
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIndexOfAny_IsIdentifierPart_True returns false when the first character of _identifier1 is not in the _identifierPart set.
//     /// </summary>
// //     [Fact] [Error] (184-23)CS0120 An object reference is required for the non-static field, method, or property 'CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True()'
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_True_FirstCharNotInSet_ReturnsFalse()
// //     {
// //         // Arrange
// //         // _identifier1 starts with 'x' which is not in the set {'a', 'b', 'c'}.
// //         string testString = "xabcXYZ";
// //         var identifierSet = CreateSearchValues(new[] { 'a', 'b', 'c' });
// //         SetStaticField("_identifier1", testString);
// //         SetStaticField("_identifierPart", identifierSet);
// //         // Act
// //         bool result = CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True();
// //         // Assert
// //         Assert.False(result, "Expected the method to return false when the first character of _identifier1 is not in _identifierPart.");
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIndexOfAny_IsIdentifierPart_True returns false when _identifier1 is an empty string.
//     /// </summary>
// //     [Fact] [Error] (202-23)CS0120 An object reference is required for the non-static field, method, or property 'CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True()'
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_True_EmptyIdentifierString_ReturnsFalse()
// //     {
// //         // Arrange
// //         // An empty string should yield IndexOfAny result as -1, thus false.
// //         string testString = string.Empty;
// //         var identifierSet = CreateSearchValues(new[] { 'a', 'b', 'c' });
// //         SetStaticField("_identifier1", testString);
// //         SetStaticField("_identifierPart", identifierSet);
// //         // Act
// //         bool result = CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True();
// //         // Assert
// //         Assert.False(result, "Expected the method to return false when _identifier1 is empty.");
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIndexOfAny_IsIdentifierPart_True throws a NullReferenceException when _identifier1 is null.
//     /// </summary>
// //     [Fact] [Error] (220-53)CS0120 An object reference is required for the non-static field, method, or property 'CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True()' [Error] (215-29)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (217-40)CS8604 Possible null reference argument for parameter 'value' in 'void CharacterSetsBenchmarksTests.SetStaticField(string fieldName, object value)'.
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_True_NullIdentifierString_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         // Setting _identifier1 to null should cause a NullReferenceException when calling AsSpan().
// //         string testString = null;
// //         var identifierSet = CreateSearchValues(new[] { 'a', 'b', 'c' });
// //         SetStaticField("_identifier1", testString);
// //         SetStaticField("_identifierPart", identifierSet);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True());
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesIndexOfAny_IsIdentifierPart_True returns false when _identifierPart is an empty set.
//     /// </summary>
// //     [Fact] [Error] (236-23)CS0120 An object reference is required for the non-static field, method, or property 'CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True()'
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_True_EmptyIdentifierSet_ReturnsFalse()
// //     {
// //         // Arrange
// //         // With an empty _identifierPart, no character can be matched even if _identifier1 is non-empty.
// //         string testString = "abcXYZ";
// //         var identifierSet = CreateSearchValues(Array.Empty<char>());
// //         SetStaticField("_identifier1", testString);
// //         SetStaticField("_identifierPart", identifierSet);
// //         // Act
// //         bool result = CharacterSetsBenchmarks.SearchValuesIndexOfAny_IsIdentifierPart_True();
// //         // Assert
// //         Assert.False(result, "Expected the method to return false when _identifierPart is empty.");
// //     }
// 
//     /// <summary>
//     /// Helper method to set the private static field of CharacterSetsBenchmarks by name.
//     /// </summary>
//     /// <param name = "fieldName">The name of the private static field.</param>
//     /// <param name = "value">The value to assign to the field.</param>
// //     private static void SetStaticField(string fieldName, object value) [Error] (248-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = typeof(CharacterSetsBenchmarks).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new ArgumentException($"Field '{fieldName}' not found in {nameof(CharacterSetsBenchmarks)}.");
// //         }
// // 
// //         field.SetValue(null, value);
// //     }
// 
//     /// <summary>
//     /// Helper method to create an instance of SearchValues<char> using the provided character array.
//     /// This assumes that SearchValues<T> has a constructor that accepts a T[].
//     /// </summary>
//     /// <param name = "values">The character array to be used for creating a SearchValues instance.</param>
//     /// <returns>An instance of SearchValues<char>.</returns>
// //     private static object CreateSearchValues(char[] values) [Error] (267-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (275-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (284-20)CS8603 Possible null reference return.
// //     {
// //         // Get the type of SearchValues<char> from the CharacterSetsBenchmarks class.
// //         // Since _identifierPart is declared as SearchValues<char>, we retrieve its type.
// //         FieldInfo field = typeof(CharacterSetsBenchmarks).GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("The field '_identifierPart' was not found.");
// //         }
// // 
// //         Type searchValuesType = field.FieldType;
// //         // Try to find a constructor that accepts a char array.
// //         ConstructorInfo ctor = searchValuesType.GetConstructor(new Type[] { typeof(char[]) });
// //         if (ctor != null)
// //         {
// //             return ctor.Invoke(new object[] { values });
// //         }
// //         else
// //         {
// //             // If no suitable constructor is found, try to create an uninitialized instance and set necessary properties if any.
// //             // For testing purposes, if no constructor exists, return the default instance.
// //             return Activator.CreateInstance(searchValuesType);
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests that the method returns true when the first character in _identifier2 is contained in _identifierPart.
//     /// This represents a case where the identifier starts with a valid character.
//     /// </summary>
// //     [Fact] [Error] (298-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (300-9)CS8602 Dereference of a possibly null reference. [Error] (302-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (303-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_False_WhenFirstCharMatches_ReturnsTrue()
// //     {
// //         // Arrange
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         // Set _identifierPart with a SearchValues instance containing 'a'
// //         FieldInfo identifierPartField = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         object searchValuesInstance = CreateSearchValuesInstance(new char[] { 'a' });
// //         identifierPartField.SetValue(null, searchValuesInstance);
// //         // Set _identifier2 to a string starting with 'a'
// //         FieldInfo identifier2Field = benchmarkType.GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static);
// //         identifier2Field.SetValue(null, "a123");
// //         var benchmarkInstance = new CharacterSetsBenchmarks();
// //         // Act
// //         bool result = benchmarkInstance.SearchValuesIndexOfAny_IsIdentifierPart_False();
// //         // Assert
// //         Assert.True(result, "Expected the method to return true when the identifier starts with a valid character.");
// //     }
// 
//     /// <summary>
//     /// Tests that the method returns false when the first character in _identifier2 is not contained in _identifierPart.
//     /// This verifies that the method correctly identifies an invalid starting character.
//     /// </summary>
// //     [Fact] [Error] (321-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (323-9)CS8602 Dereference of a possibly null reference. [Error] (325-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (326-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_False_WhenFirstCharDoesNotMatch_ReturnsFalse()
// //     {
// //         // Arrange
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         // Set _identifierPart with a SearchValues instance containing only 'a'
// //         FieldInfo identifierPartField = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         object searchValuesInstance = CreateSearchValuesInstance(new char[] { 'a' });
// //         identifierPartField.SetValue(null, searchValuesInstance);
// //         // Set _identifier2 to a string starting with 'b'
// //         FieldInfo identifier2Field = benchmarkType.GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static);
// //         identifier2Field.SetValue(null, "b123");
// //         var benchmarkInstance = new CharacterSetsBenchmarks();
// //         // Act
// //         bool result = benchmarkInstance.SearchValuesIndexOfAny_IsIdentifierPart_False();
// //         // Assert
// //         Assert.False(result, "Expected the method to return false when the identifier does not start with a valid character.");
// //     }
// 
//     /// <summary>
//     /// Tests that the method returns false when _identifier2 is an empty string.
//     /// With no characters to evaluate, the method should not detect a match.
//     /// </summary>
// //     [Fact] [Error] (344-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (346-9)CS8602 Dereference of a possibly null reference. [Error] (348-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (349-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_False_WhenIdentifierIsEmpty_ReturnsFalse()
// //     {
// //         // Arrange
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         // Set _identifierPart with a SearchValues instance containing 'a'
// //         FieldInfo identifierPartField = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         object searchValuesInstance = CreateSearchValuesInstance(new char[] { 'a' });
// //         identifierPartField.SetValue(null, searchValuesInstance);
// //         // Set _identifier2 to an empty string
// //         FieldInfo identifier2Field = benchmarkType.GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static);
// //         identifier2Field.SetValue(null, string.Empty);
// //         var benchmarkInstance = new CharacterSetsBenchmarks();
// //         // Act
// //         bool result = benchmarkInstance.SearchValuesIndexOfAny_IsIdentifierPart_False();
// //         // Assert
// //         Assert.False(result, "Expected the method to return false when the identifier is empty.");
// //     }
// 
//     /// <summary>
//     /// Tests that the method throws a NullReferenceException when _identifier2 is null.
//     /// This verifies that the method does not internally handle null identifier strings.
//     /// </summary>
// //     [Fact] [Error] (367-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (369-9)CS8602 Dereference of a possibly null reference. [Error] (371-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (372-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesIndexOfAny_IsIdentifierPart_False_WhenIdentifierIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         // Set _identifierPart with a SearchValues instance containing 'a'
// //         FieldInfo identifierPartField = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         object searchValuesInstance = CreateSearchValuesInstance(new char[] { 'a' });
// //         identifierPartField.SetValue(null, searchValuesInstance);
// //         // Set _identifier2 to null to simulate a missing identifier
// //         FieldInfo identifier2Field = benchmarkType.GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static);
// //         identifier2Field.SetValue(null, null);
// //         var benchmarkInstance = new CharacterSetsBenchmarks();
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarkInstance.SearchValuesIndexOfAny_IsIdentifierPart_False());
// //     }
// 
//     /// <summary>
//     /// Helper method to create an instance of the SearchValues&lt;char&gt; struct.
//     /// It assumes that the SearchValues&lt;char&gt; type has a constructor that takes a char array.
//     /// </summary>
//     /// <param name = "values">The array of characters for which to construct the search values.</param>
//     /// <returns>An instance of SearchValues&lt;char&gt; initialized with the provided characters.</returns>
// //     private static object CreateSearchValuesInstance(char[] values) [Error] (387-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (388-33)CS8602 Dereference of a possibly null reference. [Error] (390-32)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (397-16)CS8603 Possible null reference return.
// //     {
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         FieldInfo identifierPartField = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         Type searchValuesType = identifierPartField.FieldType;
// //         // Try to locate a constructor that accepts a char array.
// //         ConstructorInfo ctor = searchValuesType.GetConstructor(new Type[] { typeof(char[]) });
// //         if (ctor != null)
// //         {
// //             return ctor.Invoke(new object[] { values });
// //         }
// // 
// //         // If no suitable constructor is found, return the default instance.
// //         return Activator.CreateInstance(searchValuesType);
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesContains_IsIdentifierPart_True returns true
//     /// when the SearchValues instance contains the character from identifier1.
//     /// </summary>
// //     [Fact] [Error] (410-41)CS0246 The type or namespace name 'SearchValues<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (414-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (415-9)CS8602 Dereference of a possibly null reference. [Error] (416-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (417-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesContains_IsIdentifierPart_True_WhenCharacterIsContained_ReturnsTrue()
// //     {
// //         // Arrange
// //         char identifierChar = 'a';
// //         string identifier1Value = "aTest";
// //         var mockSearchValues = new Mock<SearchValues<char>>();
// //         mockSearchValues.Setup(sv => sv.Contains(identifierChar)).Returns(true);
// //         // Set the private static fields via reflection.
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         FieldInfo fieldIdentifierPart = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         fieldIdentifierPart.SetValue(null, mockSearchValues.Object);
// //         FieldInfo fieldIdentifier1 = benchmarkType.GetField("_identifier1", BindingFlags.NonPublic | BindingFlags.Static);
// //         fieldIdentifier1.SetValue(null, identifier1Value);
// //         var benchmarksInstance = new CharacterSetsBenchmarks();
// //         // Act
// //         bool result = benchmarksInstance.SearchValuesContains_IsIdentifierPart_True();
// //         // Assert
// //         Assert.True(result, "Expected method to return true when the identifier character is contained in the SearchValues instance.");
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesContains_IsIdentifierPart_True returns false
//     /// when the SearchValues instance does not contain the character from identifier1.
//     /// </summary>
// //     [Fact] [Error] (435-41)CS0246 The type or namespace name 'SearchValues<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (439-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (440-9)CS8602 Dereference of a possibly null reference. [Error] (441-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (442-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesContains_IsIdentifierPart_True_WhenCharacterNotContained_ReturnsFalse()
// //     {
// //         // Arrange
// //         char identifierChar = 'b';
// //         string identifier1Value = "bTest";
// //         var mockSearchValues = new Mock<SearchValues<char>>();
// //         mockSearchValues.Setup(sv => sv.Contains(identifierChar)).Returns(false);
// //         // Set the private static fields via reflection.
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         FieldInfo fieldIdentifierPart = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         fieldIdentifierPart.SetValue(null, mockSearchValues.Object);
// //         FieldInfo fieldIdentifier1 = benchmarkType.GetField("_identifier1", BindingFlags.NonPublic | BindingFlags.Static);
// //         fieldIdentifier1.SetValue(null, identifier1Value);
// //         var benchmarksInstance = new CharacterSetsBenchmarks();
// //         // Act
// //         bool result = benchmarksInstance.SearchValuesContains_IsIdentifierPart_True();
// //         // Assert
// //         Assert.False(result, "Expected method to return false when the identifier character is not contained in the SearchValues instance.");
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesContains_IsIdentifierPart_True throws an exception
//     /// when identifier1 is an empty string, leading to an index out of range.
//     /// </summary>
// //     [Fact] [Error] (458-41)CS0246 The type or namespace name 'SearchValues<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (461-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (462-9)CS8602 Dereference of a possibly null reference. [Error] (463-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (464-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesContains_IsIdentifierPart_True_WhenIdentifier1IsEmpty_ThrowsException()
// //     {
// //         // Arrange
// //         var mockSearchValues = new Mock<SearchValues<char>>();
// //         // Set the private static fields via reflection.
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         FieldInfo fieldIdentifierPart = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         fieldIdentifierPart.SetValue(null, mockSearchValues.Object);
// //         FieldInfo fieldIdentifier1 = benchmarkType.GetField("_identifier1", BindingFlags.NonPublic | BindingFlags.Static);
// //         fieldIdentifier1.SetValue(null, string.Empty);
// //         var benchmarksInstance = new CharacterSetsBenchmarks();
// //         // Act & Assert
// //         Assert.ThrowsAny<IndexOutOfRangeException>(() => benchmarksInstance.SearchValuesContains_IsIdentifierPart_True());
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesContains_IsIdentifierPart_True throws a NullReferenceException
//     /// when identifier1 is null.
//     /// </summary>
// //     [Fact] [Error] (478-41)CS0246 The type or namespace name 'SearchValues<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (481-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (482-9)CS8602 Dereference of a possibly null reference. [Error] (483-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (484-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesContains_IsIdentifierPart_True_WhenIdentifier1IsNull_ThrowsException()
// //     {
// //         // Arrange
// //         var mockSearchValues = new Mock<SearchValues<char>>();
// //         // Set the private static fields via reflection.
// //         Type benchmarkType = typeof(CharacterSetsBenchmarks);
// //         FieldInfo fieldIdentifierPart = benchmarkType.GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static);
// //         fieldIdentifierPart.SetValue(null, mockSearchValues.Object);
// //         FieldInfo fieldIdentifier1 = benchmarkType.GetField("_identifier1", BindingFlags.NonPublic | BindingFlags.Static);
// //         fieldIdentifier1.SetValue(null, null);
// //         var benchmarksInstance = new CharacterSetsBenchmarks();
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarksInstance.SearchValuesContains_IsIdentifierPart_True());
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesContains_IsIdentifierPart_False returns false when the underlying SearchValues.Contains returns false.
//     /// </summary>
// //     [Fact] [Error] (498-41)CS0246 The type or namespace name 'SearchValues<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (501-9)CS8602 Dereference of a possibly null reference. [Error] (503-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesContains_IsIdentifierPart_False_WhenContainsReturnsFalse_ReturnsFalse()
// //     {
// //         // Arrange
// //         // Create a mock for SearchValues<char> and setup Contains to return false.
// //         var searchValuesMock = new Mock<SearchValues<char>>();
// //         searchValuesMock.Setup(m => m.Contains(It.IsAny<char>())).Returns(false);
// //         // Using reflection, set the private static field _identifierPart to the mocked object.
// //         typeof(CharacterSetsBenchmarks).GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, searchValuesMock.Object);
// //         // Set _identifier2 to a non-null, non-empty string so that _identifier2[0] is valid.
// //         typeof(CharacterSetsBenchmarks).GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, "abc");
// //         var benchmarks = new CharacterSetsBenchmarks();
// //         // Act
// //         bool result = benchmarks.SearchValuesContains_IsIdentifierPart_False();
// //         // Assert
// //         Assert.False(result, "Expected SearchValuesContains_IsIdentifierPart_False to return false when SearchValues.Contains returns false.");
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesContains_IsIdentifierPart_False throws a NullReferenceException when _identifier2 is null.
//     /// </summary>
// //     [Fact] [Error] (518-41)CS0246 The type or namespace name 'SearchValues<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (520-9)CS8602 Dereference of a possibly null reference. [Error] (522-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesContains_IsIdentifierPart_False_WhenIdentifier2IsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var searchValuesMock = new Mock<SearchValues<char>>();
// //         searchValuesMock.Setup(m => m.Contains(It.IsAny<char>())).Returns(false);
// //         typeof(CharacterSetsBenchmarks).GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, searchValuesMock.Object);
// //         // Set _identifier2 to null to simulate the exceptional scenario.
// //         typeof(CharacterSetsBenchmarks).GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, null);
// //         var benchmarks = new CharacterSetsBenchmarks();
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.SearchValuesContains_IsIdentifierPart_False());
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesContains_IsIdentifierPart_False throws an IndexOutOfRangeException when _identifier2 is an empty string.
//     /// </summary>
// //     [Fact] [Error] (535-41)CS0246 The type or namespace name 'SearchValues<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (537-9)CS8602 Dereference of a possibly null reference. [Error] (539-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesContains_IsIdentifierPart_False_WhenIdentifier2IsEmpty_ThrowsIndexOutOfRangeException()
// //     {
// //         // Arrange
// //         var searchValuesMock = new Mock<SearchValues<char>>();
// //         searchValuesMock.Setup(m => m.Contains(It.IsAny<char>())).Returns(false);
// //         typeof(CharacterSetsBenchmarks).GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, searchValuesMock.Object);
// //         // Set _identifier2 to an empty string which will cause _identifier2[0] to throw.
// //         typeof(CharacterSetsBenchmarks).GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, string.Empty);
// //         var benchmarks = new CharacterSetsBenchmarks();
// //         // Act & Assert
// //         Assert.ThrowsAny<IndexOutOfRangeException>(() => benchmarks.SearchValuesContains_IsIdentifierPart_False());
// //     }
// 
//     /// <summary>
//     /// Tests that SearchValuesContains_IsIdentifierPart_False throws a NullReferenceException when _identifierPart is null.
//     /// </summary>
// //     [Fact] [Error] (553-9)CS8602 Dereference of a possibly null reference. [Error] (555-9)CS8602 Dereference of a possibly null reference.
// //     public void SearchValuesContains_IsIdentifierPart_False_WhenIdentifierPartIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         // Set _identifierPart to null.
// //         typeof(CharacterSetsBenchmarks).GetField("_identifierPart", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, null);
// //         // Set _identifier2 to a valid non-empty string.
// //         typeof(CharacterSetsBenchmarks).GetField("_identifier2", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, "abc");
// //         var benchmarks = new CharacterSetsBenchmarks();
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.SearchValuesContains_IsIdentifierPart_False());
// //     }
// 
//     /// <summary>
//     /// Tests that the constructor of <see cref = "CharacterSetsBenchmarks"/> instantiates successfully 
//     /// when benchmark methods return the expected boolean values.
//     /// This test follows the Arrange-Act-Assert pattern:
//     /// Arrange: No specific arrangement is needed because the constructor requires no input.
//     /// Act: Instantiate the <see cref = "CharacterSetsBenchmarks"/> class.
//     /// Assert: Verify that no exception is thrown during instantiation.
//     /// </summary>
//     [Fact]
//     public void Constructor_HappyPath_InstantiatesSuccessfully()
//     {
//         // Act & Assert: Ensure that the constructor does not throw and the instance is created.
//         var exception = Record.Exception(() => new CharacterSetsBenchmarks());
//         Assert.Null(exception);
//     }
// }
