using Parlot;
using System;
using System.Globalization;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "TextSpan"/> struct focusing on the ToString method.
/// </summary>
// public class TextSpanTests [Error] (1018-2)CS1038 #endregion directive expected
// {
//     /// <summary>
//     /// Tests that ToString returns the full string when the TextSpan is created using the implicit operator.
//     /// Given a non-null string, the returned value should match the input string.
//     /// </summary>
//     [Fact] [Error] (22-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void ToString_FullString_ReturnsSameString()
//     {
//         // Arrange
//         string input = "Hello, World!";
//         TextSpan textSpan = input; // Uses implicit conversion which should set Offset = 0 and Length = input.Length
//         // Act
//         string? result = textSpan.ToString();
//         // Assert
//         Assert.Equal(input, result);
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns the correct substring when the TextSpan is created with a specific offset and count.
//     /// This verifies that the method correctly extracts the substring from the buffer.
//     /// </summary>
//     [Fact] [Error] (40-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void ToString_WithOffsetAndCount_ReturnsSubstring()
//     {
//         // Arrange
//         string input = "Hello, World!";
//         int offset = 7;
//         int count = 5;
//         TextSpan textSpan = new TextSpan(input, offset, count);
//         // Act
//         string? result = textSpan.ToString();
//         // Assert
//         Assert.Equal("World", result);
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns null when the TextSpan is created with a null buffer.
//     /// Ensures that the method handles null buffers gracefully.
//     /// </summary>
//     [Fact] [Error] (53-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (56-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void ToString_NullBuffer_ReturnsNull()
//     {
//         // Arrange
//         string? input = null;
//         TextSpan textSpan = input; // Implicit conversion from null
//         // Act
//         string? result = textSpan.ToString();
//         // Assert
//         Assert.Null(result);
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns an empty string when the TextSpan is created with an empty string.
//     /// The expected behavior is that an empty buffer should yield an empty result.
//     /// </summary>
//     [Fact] [Error] (72-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void ToString_EmptyString_ReturnsEmptyString()
//     {
//         // Arrange
//         string input = string.Empty;
//         TextSpan textSpan = input; // Implicit conversion
//         // Act
//         string? result = textSpan.ToString();
//         // Assert
//         Assert.Equal(string.Empty, result);
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns an empty string when the TextSpan is created with an offset equal to the string length and count of zero.
//     /// This verifies edge case behavior where the substring extracted is empty.
//     /// </summary>
//     [Fact] [Error] (90-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void ToString_OffsetAtEndWithZeroCount_ReturnsEmptyString()
//     {
//         // Arrange
//         string input = "Data";
//         int offset = input.Length;
//         int count = 0;
//         TextSpan textSpan = new TextSpan(input, offset, count);
//         // Act
//         string? result = textSpan.ToString();
//         // Assert
//         Assert.Equal(string.Empty, result);
//     }
// 
//     /// <summary>
//     /// Tests the Equals(string?) method when the provided string is null and the TextSpan buffer is also null.
//     /// Expected outcome: Should return true.
//     /// </summary>
//     [Fact] [Error] (103-49)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (105-46)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void Equals_String_NullParameterAndNullBuffer_ReturnsTrue()
//     {
//         // Arrange: Create a TextSpan with a null buffer.
//         TextSpan textSpan = new TextSpan((string? )null);
//         // Act: Compare against a null string.
//         bool result = textSpan.Equals((string? )null);
//         // Assert: Expect true as both internal buffer and input are null.
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Equals(string?) method with matching non-null content.
//     /// Expected outcome: Should return true when the content of the TextSpan matches the provided string.
//     /// </summary>
//     [Fact]
//     public void Equals_String_MatchingContent_ReturnsTrue()
//     {
//         // Arrange: Create a TextSpan with a specific string.
//         string content = "hello world";
//         TextSpan textSpan = new TextSpan(content);
//         // Act: Call Equals with the same string.
//         bool result = textSpan.Equals(content);
//         // Assert: The content is identical so the comparison should return true.
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Equals(string?) method with different non-null content.
//     /// Expected outcome: Should return false when the content of the TextSpan differs from the provided string.
//     /// </summary>
//     [Fact]
//     public void Equals_String_NonMatchingContent_ReturnsFalse()
//     {
//         // Arrange: Create a TextSpan with a specific string.
//         string content = "hello world";
//         TextSpan textSpan = new TextSpan(content);
//         // Act: Compare against a different string.
//         bool result = textSpan.Equals("HELLO WORLD");
//         // Assert: Differences in case or content should cause the method to return false.
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the Equals(TextSpan) method when both TextSpan instances encapsulate identical content.
//     /// Expected outcome: Should return true as both instances represent the same text.
//     /// </summary>
//     [Fact]
//     public void Equals_TextSpan_IdenticalContent_ReturnsTrue()
//     {
//         // Arrange: Create two TextSpan instances from the same string.
//         string content = "unit testing";
//         TextSpan textSpan1 = new TextSpan(content);
//         TextSpan textSpan2 = new TextSpan(content);
//         // Act: Use the Equals(TextSpan) method.
//         bool result = textSpan1.Equals(textSpan2);
//         // Assert: The instances are expected to be equal.
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Equals(TextSpan) method when the two TextSpan instances encapsulate different content.
//     /// Expected outcome: Should return false as the instances represent different texts.
//     /// </summary>
//     [Fact]
//     public void Equals_TextSpan_DifferentContent_ReturnsFalse()
//     {
//         // Arrange: Create two TextSpan instances with differing strings.
//         TextSpan textSpan1 = new TextSpan("foo");
//         TextSpan textSpan2 = new TextSpan("bar");
//         // Act: Compare the two instances.
//         bool result = textSpan1.Equals(textSpan2);
//         // Assert: The comparison should return false.
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the Equals(TextSpan) method when both TextSpan instances have null buffers.
//     /// Expected outcome: Should return true as both instances lack content.
//     /// </summary>
//     [Fact] [Error] (183-50)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (184-50)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void Equals_TextSpan_BothNullBuffers_ReturnsTrue()
//     {
//         // Arrange: Create two TextSpan instances with null buffer.
//         TextSpan textSpan1 = new TextSpan((string? )null);
//         TextSpan textSpan2 = new TextSpan((string? )null);
//         // Act: Compare the two instances.
//         bool result = textSpan1.Equals(textSpan2);
//         // Assert: The comparison should return true when both buffers are null.
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Equals(TextSpan) method for reflexivity.
//     /// Expected outcome: An instance should always be equal to itself.
//     /// </summary>
//     [Fact]
//     public void Equals_TextSpan_Reflexivity_ReturnsTrue()
//     {
//         // Arrange: Create a TextSpan instance.
//         TextSpan textSpan = new TextSpan("reflexivity");
//         // Act: Compare the instance to itself.
//         bool result = textSpan.Equals(textSpan);
//         // Assert: The instance should be equal to itself.
//         Assert.True(result);
//     }
// 
// #region Equals(TextSpan) Tests
//     /// <summary>
//     /// Tests that Equals(TextSpan) returns true when both TextSpan instances represent the same string.
//     /// Arrange two TextSpan instances with the same content and compare them.
//     /// Expected outcome is that the method returns true.
//     /// </summary>
//     [Fact]
//     public void EqualsTextSpan_SameString_ReturnsTrue()
//     {
//         // Arrange
//         TextSpan span1 = new TextSpan("Test");
//         TextSpan span2 = new TextSpan("Test");
//         // Act
//         bool result = span1.Equals(span2);
//         // Assert
//         Assert.True(result, "Expected Equals(TextSpan) to return true for identical strings.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(TextSpan) returns false when the TextSpan instances represent different strings.
//     /// Arrange two TextSpan instances with different content and compare them.
//     /// Expected outcome is that the method returns false.
//     /// </summary>
//     [Fact]
//     public void EqualsTextSpan_DifferentString_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan span1 = new TextSpan("Test");
//         TextSpan span2 = new TextSpan("Different");
//         // Act
//         bool result = span1.Equals(span2);
//         // Assert
//         Assert.False(result, "Expected Equals(TextSpan) to return false for different strings.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(TextSpan) returns true when comparing a TextSpan created from a substring with one created directly.
//     /// Arrange one TextSpan using substring parameters and another using the complete string that matches the substring.
//     /// Expected outcome is that the method returns true.
//     /// </summary>
//     [Fact]
//     public void EqualsTextSpan_SubstringEquality_ReturnsTrue()
//     {
//         // Arrange
//         TextSpan spanFromSubstring = new TextSpan("Hello, World", 7, 5); // "World"
//         TextSpan spanDirect = new TextSpan("World");
//         // Act
//         bool result = spanFromSubstring.Equals(spanDirect);
//         // Assert
//         Assert.True(result, "Expected Equals(TextSpan) to return true when the underlying text spans are equal.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(TextSpan) returns true when both TextSpan instances have empty strings.
//     /// Arrange two TextSpan instances created from empty strings and compare them.
//     /// Expected outcome is that the method returns true.
//     /// </summary>
//     [Fact]
//     public void EqualsTextSpan_EmptyStrings_ReturnsTrue()
//     {
//         // Arrange
//         TextSpan span1 = new TextSpan("");
//         TextSpan span2 = new TextSpan("");
//         // Act
//         bool result = span1.Equals(span2);
//         // Assert
//         Assert.True(result, "Expected Equals(TextSpan) to return true for both empty strings.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(TextSpan) returns true when both TextSpan instances are created with a null buffer.
//     /// Arrange two TextSpan instances created from null and compare them.
//     /// Expected outcome is that the method returns true, assuming that a null buffer yields an empty span.
//     /// </summary>
//     [Fact]
//     public void EqualsTextSpan_BothNull_ReturnsTrue()
//     {
//         // Arrange
//         TextSpan span1 = new TextSpan(null);
//         TextSpan span2 = new TextSpan(null);
//         // Act
//         bool result = span1.Equals(span2);
//         // Assert
//         Assert.True(result, "Expected Equals(TextSpan) to return true when both instances are constructed from null.");
//     }
// 
// #endregion
// #region Equals(string?) Tests
//     /// <summary>
//     /// Tests that Equals(string?) returns true when the TextSpan represents the same string as the provided string.
//     /// Arrange a TextSpan created from a non-null string and compare with an identical string.
//     /// Expected outcome is that the method returns true.
//     /// </summary>
//     [Fact]
//     public void EqualsString_SameString_ReturnsTrue()
//     {
//         // Arrange
//         string testString = "Test";
//         TextSpan span = new TextSpan(testString);
//         // Act
//         bool result = span.Equals(testString);
//         // Assert
//         Assert.True(result, "Expected Equals(string) to return true when the content matches.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(string?) returns false when the TextSpan does not represent the same string as the provided string.
//     /// Arrange a TextSpan created from one string and compare with a different string.
//     /// Expected outcome is that the method returns false.
//     /// </summary>
//     [Fact]
//     public void EqualsString_DifferentString_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan span = new TextSpan("Test");
//         string differentString = "Different";
//         // Act
//         bool result = span.Equals(differentString);
//         // Assert
//         Assert.False(result, "Expected Equals(string) to return false when the content differs.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(string?) returns false when a non-null TextSpan is compared with a null string.
//     /// Arrange a TextSpan with a non-null string and compare with null.
//     /// Expected outcome is that the method returns false.
//     /// </summary>
//     [Fact] [Error] (338-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void EqualsString_NullStringComparedWithNonNull_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan span = new TextSpan("Test");
//         string? nullString = null;
//         // Act
//         bool result = span.Equals(nullString);
//         // Assert
//         Assert.False(result, "Expected Equals(string) to return false when comparing non-null instance with null.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(string?) returns true when the TextSpan is created from null and compared with a null string.
//     /// Arrange a TextSpan constructed from null and compare with null.
//     /// Expected outcome is that the method returns true if null is treated as an empty span.
//     /// </summary>
//     [Fact] [Error] (355-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void EqualsString_BothNull_ReturnsTrue()
//     {
//         // Arrange
//         TextSpan span = new TextSpan(null);
//         string? nullString = null;
//         // Act
//         bool result = span.Equals(nullString);
//         // Assert
//         Assert.True(result, "Expected Equals(string) to return true when both the TextSpan and the provided string are null (or empty).");
//     }
// 
// #endregion
// #region Equals(object?) Tests
//     /// <summary>
//     /// Tests that Equals(object) returns true when comparing a TextSpan to a TextSpan object representing the same string.
//     /// Arrange a TextSpan instance and compare its Equals(object) with another TextSpan with identical content.
//     /// Expected outcome is that the method returns true.
//     /// </summary>
//     [Fact]
//     public void EqualsObject_TextSpanSame_ReturnsTrue()
//     {
//         // Arrange
//         TextSpan span1 = new TextSpan("Test");
//         object span2 = new TextSpan("Test");
//         // Act
//         bool result = span1.Equals(span2);
//         // Assert
//         Assert.True(result, "Expected Equals(object) to return true when comparing TextSpan with identical content.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(object) returns false when comparing a TextSpan to a TextSpan object representing a different string.
//     /// Arrange a TextSpan instance and compare its Equals(object) with another TextSpan with different content.
//     /// Expected outcome is that the method returns false.
//     /// </summary>
//     [Fact]
//     public void EqualsObject_TextSpanDifferent_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan span1 = new TextSpan("Test");
//         object span2 = new TextSpan("Different");
//         // Act
//         bool result = span1.Equals(span2);
//         // Assert
//         Assert.False(result, "Expected Equals(object) to return false when comparing TextSpan with different content.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(object) returns true when comparing a TextSpan to a string object that is equal to its content.
//     /// Arrange a TextSpan instance and compare its Equals(object) with a string that matches the underlying content.
//     /// Expected outcome is that the method returns true.
//     /// </summary>
//     [Fact]
//     public void EqualsObject_StringEqual_ReturnsTrue()
//     {
//         // Arrange
//         TextSpan span = new TextSpan("Test");
//         object str = "Test";
//         // Act
//         bool result = span.Equals(str);
//         // Assert
//         Assert.True(result, "Expected Equals(object) to return true when comparing TextSpan with a string of identical content.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(object) returns false when comparing a TextSpan to a string object that is not equal to its content.
//     /// Arrange a TextSpan instance and compare its Equals(object) with a string that does not match the underlying content.
//     /// Expected outcome is that the method returns false.
//     /// </summary>
//     [Fact]
//     public void EqualsObject_StringDifferent_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan span = new TextSpan("Test");
//         object str = "Different";
//         // Act
//         bool result = span.Equals(str);
//         // Assert
//         Assert.False(result, "Expected Equals(object) to return false when comparing TextSpan with a string of different content.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(object) returns false when comparing a TextSpan to an object of an unrelated type.
//     /// Arrange a TextSpan instance and compare its Equals(object) with an integer.
//     /// Expected outcome is that the method returns false.
//     /// </summary>
//     [Fact]
//     public void EqualsObject_UnrelatedType_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan span = new TextSpan("Test");
//         object unrelated = 123;
//         // Act
//         bool result = span.Equals(unrelated);
//         // Assert
//         Assert.False(result, "Expected Equals(object) to return false when comparing TextSpan with an unrelated type.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(object) returns false when comparing a TextSpan to a null object.
//     /// Arrange a TextSpan instance and compare its Equals(object) with null.
//     /// Expected outcome is that the method returns false.
//     /// </summary>
//     [Fact] [Error] (459-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void EqualsObject_NullObject_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan span = new TextSpan("Test");
//         object? nullObject = null;
//         // Act
//         bool result = span.Equals(nullObject);
//         // Assert
//         Assert.False(result, "Expected Equals(object) to return false when comparing TextSpan with a null object.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(string) returns true when the underlying value matches the provided string.
//     /// </summary>
//     [Fact]
//     public void Equals_String_SameValue_ReturnsTrue()
//     {
//         // Arrange
//         string testValue = "hello";
//         TextSpan textSpan = testValue;
//         // Act
//         bool result = textSpan.Equals(testValue);
//         // Assert
//         Assert.True(result, "Expected Equals(string) to return true when the values are the same.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(string) returns false when the underlying value does not match the provided string.
//     /// </summary>
//     [Fact]
//     public void Equals_String_DifferentValue_ReturnsFalse()
//     {
//         // Arrange
//         string testValue = "hello";
//         string differentValue = "world";
//         TextSpan textSpan = testValue;
//         // Act
//         bool result = textSpan.Equals(differentValue);
//         // Assert
//         Assert.False(result, "Expected Equals(string) to return false when the values are different.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(string) returns false when a null string is provided.
//     /// </summary>
//     [Fact] [Error] (507-46)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void Equals_String_NullInput_ReturnsFalse()
//     {
//         // Arrange
//         string testValue = "hello";
//         TextSpan textSpan = testValue;
//         // Act
//         bool result = textSpan.Equals((string? )null);
//         // Assert
//         Assert.False(result, "Expected Equals(string) to return false when the input string is null.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(TextSpan) returns true when two TextSpan instances represent the same content.
//     /// </summary>
//     [Fact]
//     public void Equals_TextSpan_SameValue_ReturnsTrue()
//     {
//         // Arrange
//         string testValue = "hello";
//         TextSpan textSpan1 = testValue;
//         TextSpan textSpan2 = testValue;
//         // Act
//         bool result = textSpan1.Equals(textSpan2);
//         // Assert
//         Assert.True(result, "Expected Equals(TextSpan) to return true when both TextSpan instances represent the same content.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(TextSpan) returns false when two TextSpan instances represent different content.
//     /// </summary>
//     [Fact]
//     public void Equals_TextSpan_DifferentValue_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan textSpan1 = "hello";
//         TextSpan textSpan2 = "world";
//         // Act
//         bool result = textSpan1.Equals(textSpan2);
//         // Assert
//         Assert.False(result, "Expected Equals(TextSpan) to return false when the TextSpan instances represent different content.");
//     }
// 
//     /// <summary>
//     /// Tests that Equals(TextSpan) returns false when the internal span segments differ even if the underlying buffer is the same.
//     /// </summary>
//     [Fact]
//     public void Equals_TextSpan_DifferentOffsets_ReturnsFalse()
//     {
//         // Arrange
//         // Using the constructor TextSpan(string? buffer, int offset, int count) to create different segments.
//         string buffer = "abcdef";
//         TextSpan textSpan1 = new TextSpan(buffer, 1, 3); // represents "bcd"
//         TextSpan textSpan2 = new TextSpan(buffer, 2, 3); // represents "cde"
//         // Act
//         bool result = textSpan1.Equals(textSpan2);
//         // Assert
//         Assert.False(result, "Expected Equals(TextSpan) to return false when TextSpan instances have different offsets or counts.");
//     }
// 
//     /// <summary>
//     /// Tests that the overridden Equals(object) returns true when passed a TextSpan object with the same content.
//     /// </summary>
//     [Fact]
//     public void Equals_Object_TextSpanSameValue_ReturnsTrue()
//     {
//         // Arrange
//         TextSpan textSpan1 = "sample";
//         TextSpan textSpan2 = "sample";
//         object obj = textSpan2;
//         // Act
//         bool result = textSpan1.Equals(obj);
//         // Assert
//         Assert.True(result, "Expected Equals(object) to return true when the object is a TextSpan with the same content.");
//     }
// 
//     /// <summary>
//     /// Tests that the overridden Equals(object) returns false when passed an object that is not a TextSpan.
//     /// </summary>
//     [Fact]
//     public void Equals_Object_NonTextSpan_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan textSpan = "sample";
//         object nonTextSpan = "sample";
//         // Act
//         bool result = textSpan.Equals(nonTextSpan);
//         // Assert
//         Assert.False(result, "Expected Equals(object) to return false when the object is not a TextSpan.");
//     }
// 
//     /// <summary>
//     /// Tests that the overridden Equals(object) returns false when passed null.
//     /// </summary>
//     [Fact] [Error] (600-46)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void Equals_Object_Null_ReturnsFalse()
//     {
//         // Arrange
//         TextSpan textSpan = "sample";
//         // Act
//         bool result = textSpan.Equals((object? )null);
//         // Assert
//         Assert.False(result, "Expected Equals(object) to return false when passed a null object.");
//     }
// 
//     /// <summary>
//     /// Tests that GetHashCode returns a consistent value when called multiple times on the same instance.
//     /// </summary>
//     [Fact]
//     public void GetHashCode_Consistent_ReturnsSameValue()
//     {
//         // Arrange
//         string sampleText = "Hello World";
//         TextSpan textSpan = new TextSpan(sampleText);
//         // Act
//         int firstHashCode = textSpan.GetHashCode();
//         int secondHashCode = textSpan.GetHashCode();
//         // Assert
//         Assert.Equal(firstHashCode, secondHashCode);
//     }
// 
//     /// <summary>
//     /// Tests that two TextSpan instances created from the same string yield the same hash code.
//     /// </summary>
//     [Fact]
//     public void GetHashCode_SameUnderlyingText_ConstructedDifferently_ReturnsSameHashCode()
//     {
//         // Arrange
//         string sampleText = "Sample Text";
//         TextSpan textSpanFromImplicit = sampleText; // using implicit operator
//         TextSpan textSpanFromConstructor = new TextSpan(sampleText, 0, sampleText.Length);
//         // Act
//         int hashCodeImplicit = textSpanFromImplicit.GetHashCode();
//         int hashCodeConstructor = textSpanFromConstructor.GetHashCode();
//         // Assert
//         Assert.Equal(hashCodeImplicit, hashCodeConstructor);
//     }
// 
//     /// <summary>
//     /// Tests that GetHashCode for a TextSpan created with a null string behaves as expected by comparing it with an empty TextSpan.
//     /// </summary>
//     [Fact]
//     public void GetHashCode_NullText_ReturnsSameHashCodeAsEmptyString()
//     {
//         // Arrange
//         TextSpan textSpanFromNull = new TextSpan(null);
//         TextSpan textSpanFromEmpty = new TextSpan(string.Empty);
//         // Act
//         int hashCodeNull = textSpanFromNull.GetHashCode();
//         int hashCodeEmpty = textSpanFromEmpty.GetHashCode();
//         // Assert
//         Assert.Equal(hashCodeEmpty, hashCodeNull);
//     }
// 
//     /// <summary>
//     /// Tests that GetHashCode produces different values for TextSpan instances with different underlying texts.
//     /// </summary>
//     [Fact]
//     public void GetHashCode_DifferentTexts_ReturnsDifferentHashCodes()
//     {
//         // Arrange
//         TextSpan textSpan1 = new TextSpan("Text One");
//         TextSpan textSpan2 = new TextSpan("Text Two");
//         // Act
//         int hashCode1 = textSpan1.GetHashCode();
//         int hashCode2 = textSpan2.GetHashCode();
//         // Assert
//         Assert.NotEqual(hashCode1, hashCode2);
//     }
// 
//     /// <summary>
//     /// Tests that the TextSpan(string?) constructor correctly handles a null string,
//     /// setting Buffer to null, Offset to 0, and Length to 0.
//     /// </summary>
//     [Fact] [Error] (678-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void TextSpan_StringConstructor_WithNullString_SetsPropertiesToDefault()
//     {
//         // Arrange
//         string? input = null;
//         // Act
//         var textSpan = new TextSpan(input);
//         // Assert
//         Assert.Null(textSpan.Buffer);
//         Assert.Equal(0, textSpan.Offset);
//         Assert.Equal(0, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that the TextSpan(string?) constructor correctly sets properties when provided a non-null string.
//     /// </summary>
//     /// <param name = "input">The input string to initialize the TextSpan.</param>
//     [Theory]
//     [InlineData("hello")]
//     [InlineData("")]
//     public void TextSpan_StringConstructor_WithNonNullString_SetsPropertiesCorrectly(string input)
//     {
//         // Arrange
//         int expectedLength = input?.Length ?? 0;
//         // Act
//         var textSpan = new TextSpan(input);
//         // Assert
//         Assert.Equal(input, textSpan.Buffer);
//         Assert.Equal(0, textSpan.Offset);
//         Assert.Equal(expectedLength, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that the overloaded TextSpan(string?, int, int) constructor initializes properties correctly with valid parameters.
//     /// </summary>
//     /// <param name = "buffer">The buffer string.</param>
//     /// <param name = "offset">The starting offset.</param>
//     /// <param name = "count">The length count.</param>
//     [Theory] [Error] (715-98)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     [InlineData("abcdef", 2, 3)]
//     [InlineData("", 0, 0)]
//     public void TextSpan_OverloadedConstructor_WithValidParameters_SetsPropertiesCorrectly(string? buffer, int offset, int count)
//     {
//         // Arrange
//         // Act
//         var textSpan = new TextSpan(buffer, offset, count);
//         // Assert
//         Assert.Equal(buffer, textSpan.Buffer);
//         Assert.Equal(offset, textSpan.Offset);
//         Assert.Equal(count, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that the overloaded TextSpan(string?, int, int) constructor throws an ArgumentOutOfRangeException when the offset is negative.
//     /// </summary>
//     [Fact]
//     public void TextSpan_OverloadedConstructor_WithNegativeOffset_ThrowsArgumentOutOfRangeException()
//     {
//         // Arrange
//         string buffer = "abc";
//         int negativeOffset = -1;
//         int count = 1;
//         // Act & Assert
//         Assert.Throws<ArgumentOutOfRangeException>(() => new TextSpan(buffer, negativeOffset, count));
//     }
// 
//     /// <summary>
//     /// Tests that the overloaded TextSpan(string?, int, int) constructor throws an ArgumentOutOfRangeException when the count is negative.
//     /// </summary>
//     [Fact]
//     public void TextSpan_OverloadedConstructor_WithNegativeCount_ThrowsArgumentOutOfRangeException()
//     {
//         // Arrange
//         string buffer = "abc";
//         int offset = 0;
//         int negativeCount = -1;
//         // Act & Assert
//         Assert.Throws<ArgumentOutOfRangeException>(() => new TextSpan(buffer, offset, negativeCount));
//     }
// 
//     /// <summary>
//     /// Tests that the overloaded TextSpan(string?, int, int) constructor throws an ArgumentOutOfRangeException
//     /// when the sum of offset and count exceeds the length of the provided buffer.
//     /// </summary>
//     [Fact]
//     public void TextSpan_OverloadedConstructor_WithOffsetPlusCountExceedingBufferLength_ThrowsArgumentOutOfRangeException()
//     {
//         // Arrange
//         string buffer = "hello";
//         int offset = 3;
//         int count = 3; // 3 + 3 = 6 exceeds buffer length (5)
//         // Act & Assert
//         Assert.Throws<ArgumentOutOfRangeException>(() => new TextSpan(buffer, offset, count));
//     }
// 
//     /// <summary>
//     /// Tests that the overloaded TextSpan(string?, int, int) constructor correctly handles a null buffer 
//     /// when offset and count are both zero.
//     /// </summary>
//     [Fact] [Error] (777-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void TextSpan_OverloadedConstructor_WithNullBufferAndZeroOffsetAndCount_SetsPropertiesCorrectly()
//     {
//         // Arrange
//         string? buffer = null;
//         int offset = 0;
//         int count = 0;
//         // Act
//         var textSpan = new TextSpan(buffer, offset, count);
//         // Assert
//         Assert.Null(textSpan.Buffer);
//         Assert.Equal(0, textSpan.Offset);
//         Assert.Equal(0, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that the overloaded TextSpan(string?, int, int) constructor throws an ArgumentOutOfRangeException
//     /// when a null buffer is provided with a non-zero offset.
//     /// </summary>
//     [Fact] [Error] (796-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void TextSpan_OverloadedConstructor_WithNullBufferAndNonZeroOffset_ThrowsArgumentOutOfRangeException()
//     {
//         // Arrange
//         string? buffer = null;
//         int offset = 1;
//         int count = 0;
//         // Act & Assert
//         Assert.Throws<ArgumentOutOfRangeException>(() => new TextSpan(buffer, offset, count));
//     }
// 
//     /// <summary>
//     /// Tests that the overloaded TextSpan(string?, int, int) constructor throws an ArgumentOutOfRangeException
//     /// when a null buffer is provided with a non-zero count.
//     /// </summary>
//     [Fact] [Error] (811-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void TextSpan_OverloadedConstructor_WithNullBufferAndNonZeroCount_ThrowsArgumentOutOfRangeException()
//     {
//         // Arrange
//         string? buffer = null;
//         int offset = 0;
//         int count = 1;
//         // Act & Assert
//         Assert.Throws<ArgumentOutOfRangeException>(() => new TextSpan(buffer, offset, count));
//     }
// 
// #region Tests for the single-parameter constructor: TextSpan(string? value)
//     /// <summary>
//     /// Tests that initializing a TextSpan with a null string sets Buffer to null, Offset to 0, and Length to 0.
//     /// </summary>
//     [Fact] [Error] (826-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (829-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void TextSpanConstructor_WithNullValue_ShouldSetDefaults()
//     {
//         // Arrange
//         string? input = null;
//         int expectedOffset = 0;
//         int expectedLength = 0;
//         string? expectedBuffer = null;
//         // Act
//         var textSpan = new TextSpan(input);
//         // Assert
//         Assert.Equal(expectedBuffer, textSpan.Buffer);
//         Assert.Equal(expectedOffset, textSpan.Offset);
//         Assert.Equal(expectedLength, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that initializing a TextSpan with an empty string sets Buffer to an empty string,
//     /// Offset to 0, and Length to 0.
//     /// </summary>
//     [Fact]
//     public void TextSpanConstructor_WithEmptyString_ShouldSetDefaults()
//     {
//         // Arrange
//         string input = string.Empty;
//         int expectedOffset = 0;
//         int expectedLength = 0;
//         string expectedBuffer = string.Empty;
//         // Act
//         var textSpan = new TextSpan(input);
//         // Assert
//         Assert.Equal(expectedBuffer, textSpan.Buffer);
//         Assert.Equal(expectedOffset, textSpan.Offset);
//         Assert.Equal(expectedLength, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that initializing a TextSpan with a non-empty string correctly assigns Buffer,
//     /// sets Offset to 0, and Length to the length of the string.
//     /// </summary>
//     [Fact]
//     public void TextSpanConstructor_WithNonEmptyString_ShouldSetFieldsProperly()
//     {
//         // Arrange
//         string input = "Hello, World!";
//         int expectedOffset = 0;
//         int expectedLength = input.Length;
//         string expectedBuffer = input;
//         // Act
//         var textSpan = new TextSpan(input);
//         // Assert
//         Assert.Equal(expectedBuffer, textSpan.Buffer);
//         Assert.Equal(expectedOffset, textSpan.Offset);
//         Assert.Equal(expectedLength, textSpan.Length);
//     }
// 
// #endregion
// #region Tests for the three-parameter constructor: TextSpan(string? buffer, int offset, int count)
//     /// <summary>
//     /// Tests that initializing a TextSpan with a null buffer and zero offset and count correctly assigns the provided values.
//     /// </summary>
//     [Fact] [Error] (887-15)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void TextSpanConstructor_WithNullBuffer_ShouldSetFieldsProperly()
//     {
//         // Arrange
//         string? buffer = null;
//         int offset = 0;
//         int count = 0;
//         // Act
//         var textSpan = new TextSpan(buffer, offset, count);
//         // Assert
//         Assert.Null(textSpan.Buffer);
//         Assert.Equal(offset, textSpan.Offset);
//         Assert.Equal(count, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that initializing a TextSpan with a valid buffer and specific offset and count correctly assigns the values.
//     /// </summary>
//     [Fact]
//     public void TextSpanConstructor_WithValidParameters_ShouldSetFieldsProperly()
//     {
//         // Arrange
//         string buffer = "Hello, World!";
//         int offset = 7;
//         int count = 5;
//         // Expected values are exactly what is passed in.
//         int expectedOffset = offset;
//         int expectedLength = count;
//         string expectedBuffer = buffer;
//         // Act
//         var textSpan = new TextSpan(buffer, offset, count);
//         // Assert
//         Assert.Equal(expectedBuffer, textSpan.Buffer);
//         Assert.Equal(expectedOffset, textSpan.Offset);
//         Assert.Equal(expectedLength, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that initializing a TextSpan with atypical offset and count (such as negative values)
//     /// assigns the fields without performing validation.
//     /// </summary>
//     [Fact]
//     public void TextSpanConstructor_WithNegativeOffsetOrCount_ShouldAssignFields()
//     {
//         // Arrange
//         string buffer = "SomeText";
//         int offset = -3;
//         int count = -5;
//         // There is no validation in the constructor so the negative values should be assigned.
//         int expectedOffset = offset;
//         int expectedLength = count;
//         string expectedBuffer = buffer;
//         // Act
//         var textSpan = new TextSpan(buffer, offset, count);
//         // Assert
//         Assert.Equal(expectedBuffer, textSpan.Buffer);
//         Assert.Equal(expectedOffset, textSpan.Offset);
//         Assert.Equal(expectedLength, textSpan.Length);
//     }
// 
//     /// <summary>
//     /// Tests that the Span property returns an empty ReadOnlySpan when Buffer is null.
//     /// Arrange: Create a TextSpan instance with a null value.
//     /// Act: Retrieve the Span property.
//     /// Assert: The resulting span is empty.
//     /// </summary>
//     [Fact] [Error] (953-44)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void Span_WhenBufferIsNull_ReturnsEmptySpan()
//     {
//         // Arrange
//         var textSpan = new TextSpan((string? )null);
//         // Act
//         ReadOnlySpan<char> span = textSpan.Span;
//         // Assert
//         Assert.Equal(string.Empty, new string (span));
//     }
// 
//     /// <summary>
//     /// Tests that the Span property returns the correct substring when Buffer is non-null.
//     /// Arrange: Create a TextSpan instance with a buffer and specific offset and length.
//     /// Act: Retrieve the Span property.
//     /// Assert: The span matches the expected substring from the buffer.
//     /// </summary>
//     [Fact]
//     public void Span_WhenBufferIsNotNull_ReturnsCorrectSubstring()
//     {
//         // Arrange
//         string buffer = "Hello, world!";
//         int offset = 7;
//         int length = 5;
//         var textSpan = new TextSpan(buffer, offset, length);
//         string expected = "world";
//         // Act
//         ReadOnlySpan<char> span = textSpan.Span;
//         // Assert
//         Assert.Equal(expected, new string (span));
//     }
// 
//     /// <summary>
//     /// Tests that the Span property returns an empty ReadOnlySpan when the length is set to zero.
//     /// Arrange: Create a TextSpan instance with a non-null buffer but with zero length.
//     /// Act: Retrieve the Span property.
//     /// Assert: The resulting span is empty.
//     /// </summary>
//     [Fact]
//     public void Span_WhenCountIsZero_ReturnsEmptySpan()
//     {
//         // Arrange
//         string buffer = "Test";
//         int offset = 2;
//         int length = 0;
//         var textSpan = new TextSpan(buffer, offset, length);
//         // Act
//         ReadOnlySpan<char> span = textSpan.Span;
//         // Assert
//         Assert.Equal(string.Empty, new string (span));
//     }
// 
//     /// <summary>
//     /// Tests that the Span property returns the entire string when created via implicit conversion.
//     /// Arrange: Convert a non-null string to a TextSpan instance via the implicit operator.
//     /// Act: Retrieve the Span property.
//     /// Assert: The returned span is equal to the full string.
//     /// </summary>
//     [Fact]
//     public void Span_FromImplicitConversion_ReturnsFullStringSpan()
//     {
//         // Arrange
//         string source = "Implicit";
//         TextSpan textSpan = source; // Implicit conversion: should set offset to 0 and length equal to source.Length.
//         // Act
//         ReadOnlySpan<char> span = textSpan.Span;
//         // Assert
//         Assert.Equal(source, new string (span));
//     }
// }