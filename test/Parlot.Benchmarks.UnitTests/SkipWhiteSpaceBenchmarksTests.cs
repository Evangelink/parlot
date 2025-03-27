// using Moq;
// using Parlot.Benchmarks;
// using System;
// using System.Reflection;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "SkipWhiteSpaceBenchmarks"/> class.
// /// This test class specifically tests the Setup method.
// /// </summary>
// public class SkipWhiteSpaceBenchmarksTests
// {
//     /// <summary>
//     /// Tests that the Setup method correctly initializes the _source field for a given valid Length.
//     /// It verifies that the _source field is constructed as expected: a string of spaces of length equal to Length followed by "a".
//     /// </summary>
//     /// <param name = "length">The number of white space characters to include.</param>
//     /// <param name = "expected">The expected value of _source field after Setup is called.</param>
// //     [Theory] [Error] (32-31)CS8600 Converting null literal or possible null value to non-nullable type.
// //     [InlineData(0, "a")]
// //     [InlineData(1, " a")]
// //     [InlineData(2, "  a")]
// //     [InlineData(10, "          a")]
// //     public void Setup_WithValidLength_ShouldInitializeSourceCorrectly(int length, string expected)
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         benchmarks.Length = length;
// //         // Act
// //         benchmarks.Setup();
// //         // Assert
// //         FieldInfo fieldInfo = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
// //         Assert.NotNull(fieldInfo);
// //         var actual = fieldInfo.GetValue(benchmarks) as string;
// //         Assert.Equal(expected, actual);
// //     }
// 
//     /// <summary>
//     /// Tests that the Setup method throws an ArgumentOutOfRangeException when Length is negative.
//     /// This verifies the exception behavior of the string constructor used within Setup.
//     /// </summary>
//     [Fact]
//     public void Setup_WithNegativeLength_ShouldThrowArgumentOutOfRangeException()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         benchmarks.Length = -1;
//         // Act & Assert
//         Assert.Throws<ArgumentOutOfRangeException>(() => benchmarks.Setup());
//     }
// 
//     /// <summary>
//     /// Helper method to set the private _source field of SkipWhiteSpaceBenchmarks.
//     /// </summary>
//     /// <param name = "instance">The instance of SkipWhiteSpaceBenchmarks.</param>
//     /// <param name = "value">The value to assign to _source.</param>
//     private static void SetSource(SkipWhiteSpaceBenchmarks instance, string value)
//     {
//         var field = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.Instance | BindingFlags.NonPublic);
//         if (field == null)
//         {
//             throw new InvalidOperationException("Field _source not found.");
//         }
// 
//         field.SetValue(instance, value);
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace_Default method with an empty string.
//     /// Expected outcome: returns false as there is no whitespace to skip.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_Default_EmptyInput_ReturnsFalse()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetSource(benchmarks, string.Empty);
//         // Act
//         bool result = benchmarks.SkipWhiteSpace_Default();
//         // Assert
//         Assert.False(result, "Expected SkipWhiteSpace_Default to return false when input is an empty string.");
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace_Default method with a string that has no leading whitespace.
//     /// Expected outcome: returns false as there is no whitespace to skip.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_Default_NoLeadingWhiteSpace_ReturnsFalse()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetSource(benchmarks, "abc");
//         // Act
//         bool result = benchmarks.SkipWhiteSpace_Default();
//         // Assert
//         Assert.False(result, "Expected SkipWhiteSpace_Default to return false when input does not begin with whitespace.");
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace_Default method with a string that starts with whitespace followed by non-whitespace.
//     /// Expected outcome: returns true indicating that whitespace was skipped.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_Default_LeadingWhiteSpace_ReturnsTrue()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetSource(benchmarks, "   abc");
//         // Act
//         bool result = benchmarks.SkipWhiteSpace_Default();
//         // Assert
//         Assert.True(result, "Expected SkipWhiteSpace_Default to return true when leading whitespace is present.");
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace_Default method with a string consisting only of whitespace.
//     /// Expected outcome: returns true indicating that whitespace was skipped, even though no non-whitespace remains.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_Default_AllWhiteSpace_ReturnsTrue()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetSource(benchmarks, "      ");
//         // Act
//         bool result = benchmarks.SkipWhiteSpace_Default();
//         // Assert
//         Assert.True(result, "Expected SkipWhiteSpace_Default to return true when input contains only whitespace.");
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace_Default method when the source is null.
//     /// Expected outcome: throws an ArgumentNullException.
//     /// </summary>
// //     [Fact] [Error] (141-31)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void SkipWhiteSpace_Default_NullSource_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetSource(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => benchmarks.SkipWhiteSpace_Default());
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine_Default returns false when the source string is empty.
//     /// This test sets the private _source field to an empty string and calls the method. The expected outcome is that
//     /// no whitespace is skipped, thereby returning false.
//     /// </summary>
// //     [Fact] [Error] (156-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (157-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpaceOrNewLine_Default_EmptyString_ReturnsFalse()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         FieldInfo sourceField = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
// //         sourceField.SetValue(benchmarks, string.Empty);
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLine_Default();
// //         // Assert
// //         Assert.False(result, "Expected false when provided an empty string with no white space to skip.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine_Default returns true when the source string consists solely of a single whitespace.
//     /// This test sets the private _source field to a string containing only a single whitespace character, expecting the
//     /// scanner to skip it and return true.
//     /// </summary>
// //     [Fact] [Error] (174-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (175-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpaceOrNewLine_Default_SingleSpace_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         FieldInfo sourceField = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
// //         sourceField.SetValue(benchmarks, " ");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLine_Default();
// //         // Assert
// //         Assert.True(result, "Expected true when a single whitespace character is skipped.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine_Default returns false when the source string starts with a non-whitespace character.
//     /// In this scenario, the input string has no leading whitespace to skip so the method is expected to return false.
//     /// </summary>
// //     [Fact] [Error] (191-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (192-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpaceOrNewLine_Default_NoLeadingWhiteSpace_ReturnsFalse()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         FieldInfo sourceField = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
// //         sourceField.SetValue(benchmarks, "abc");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLine_Default();
// //         // Assert
// //         Assert.False(result, "Expected false when there are no leading whitespace characters to skip.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine_Default returns true when the source string has leading whitespace followed by non-whitespace.
//     /// This test verifies that the method correctly skips the leading whitespace characters.
//     /// </summary>
// //     [Fact] [Error] (208-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (209-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpaceOrNewLine_Default_LeadingWhiteSpace_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         FieldInfo sourceField = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
// //         sourceField.SetValue(benchmarks, "   abc");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLine_Default();
// //         // Assert
// //         Assert.True(result, "Expected true when leading whitespace characters are skipped.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine_Default returns true when the source string contains a mix of whitespace and newline characters.
//     /// This test ensures that the method considers various whitespace characters (such as spaces, newlines, and tabs)
//     /// when skipping whitespace.
//     /// </summary>
// //     [Fact] [Error] (226-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (227-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpaceOrNewLine_Default_MixedWhiteSpaceAndNewLine_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         FieldInfo sourceField = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
// //         sourceField.SetValue(benchmarks, " \n\t ");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLine_Default();
// //         // Assert
// //         Assert.True(result, "Expected true when a mix of whitespace and newline characters are skipped.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_Vectorized returns false when the source string is empty.
//     /// Expected behavior: When the source is empty, the underlying scanner's cursor should indicate end-of-file and the method returns false.
//     /// </summary>
// //     [Fact] [Error] (243-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_Vectorized_EmptyString_ReturnsFalse()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, string.Empty);
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpace_Vectorized();
// //         // Assert
// //         Assert.False(result);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_Vectorized returns false when the source string starts immediately with a non-white space character.
//     /// Expected behavior: When the first character is not a white space, the method should not advance the cursor and return false.
//     /// </summary>
// //     [Fact] [Error] (260-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_Vectorized_InputStartingWithNonWhite_ReturnsFalse()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         // "a" is assumed to be a non-white space character.
// //         SetPrivateSource(benchmarks, "a");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpace_Vectorized();
// //         // Assert
// //         Assert.False(result);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_Vectorized returns true when the source string consists entirely of white space characters.
//     /// Expected behavior: If the entire span is white spaces, the method advances the cursor to the end and returns true.
//     /// </summary>
// //     [Fact] [Error] (277-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_Vectorized_InputAllWhiteSpaces_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         // A string with only white space characters.
// //         SetPrivateSource(benchmarks, "     ");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpace_Vectorized();
// //         // Assert
// //         Assert.True(result);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_Vectorized returns true when the source string starts with white space characters followed by a non-white space character.
//     /// Expected behavior: The method should advance the cursor by the number of leading white spaces and return true.
//     /// </summary>
// //     [Fact] [Error] (294-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_Vectorized_InputWhiteSpacesThenNonWhite_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         // The first character is a white space, followed by non-white space character.
// //         SetPrivateSource(benchmarks, "  a");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpace_Vectorized();
// //         // Assert
// //         Assert.True(result);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_Vectorized throws an ArgumentNullException when the source string is null.
//     /// Expected behavior: The underlying Scanner constructor is expected to throw an ArgumentNullException if _source is null.
//     /// </summary>
// //     [Fact] [Error] (310-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_Vectorized_NullSource_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => benchmarks.SkipWhiteSpace_Vectorized());
// //     }
// 
//     /// <summary>
//     /// Helper method to set the private _source field of SkipWhiteSpaceBenchmarks.
//     /// </summary>
//     /// <param name = "instance">An instance of SkipWhiteSpaceBenchmarks.</param>
//     /// <param name = "value">The string value to set for _source.</param>
// //     private static void SetPrivateSource(SkipWhiteSpaceBenchmarks instance, string value) [Error] (322-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("Field '_source' not found in SkipWhiteSpaceBenchmarks.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Sets a private field value via reflection.
//     /// </summary>
//     /// <typeparam name = "T">The type of the field.</typeparam>
//     /// <param name = "instance">The instance containing the field.</param>
//     /// <param name = "fieldName">The field name.</param>
//     /// <param name = "value">The value to set.</param>
// //     private static void SetPrivateField<T>(object instance, string fieldName, T value) [Error] (340-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new ArgumentException($"Field '{fieldName}' was not found on type '{instance.GetType().FullName}'.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_Vectorized returns false when the source is empty.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLines_Vectorized_EmptySource_ReturnsFalse()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetPrivateField(benchmarks, "_source", string.Empty);
//         // Act
//         bool result = benchmarks.SkipWhiteSpaceOrNewLines_Vectorized();
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_Vectorized returns false when the source begins with a non-white space character.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLines_Vectorized_NonWhiteSpaceAtStart_ReturnsFalse()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         // The source starts immediately with a non-white space character "A"
//         SetPrivateField(benchmarks, "_source", "A");
//         // Act
//         bool result = benchmarks.SkipWhiteSpaceOrNewLines_Vectorized();
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_Vectorized returns true when the source consists exclusively of white space and newline characters.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLines_Vectorized_AllWhiteSpace_ReturnsTrue()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         // The source is all white space (assuming space and/or newline characters are considered)
//         SetPrivateField(benchmarks, "_source", "   \n  \r\n");
//         // Act
//         bool result = benchmarks.SkipWhiteSpaceOrNewLines_Vectorized();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_Vectorized returns true when the source begins with white space followed by non-white space content.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLines_Vectorized_WhiteSpacePrefixBeforeContent_ReturnsTrue()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         // The source has leading white spaces then a non-white space character "A"
//         SetPrivateField(benchmarks, "_source", "   A");
//         // Act
//         bool result = benchmarks.SkipWhiteSpaceOrNewLines_Vectorized();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_Vectorized throws an exception when the source is null.
//     /// </summary>
// //     [Fact] [Error] (420-56)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void SkipWhiteSpaceOrNewLines_Vectorized_NullSource_ThrowsException()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateField<string>(benchmarks, "_source", null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.SkipWhiteSpaceOrNewLines_Vectorized());
// //     }
// 
//     /// <summary>
//     /// Tests that when the input string is empty, the method returns true.
//     /// The expectation is that an empty input leads to no characters being processed and the method returns true.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_PeekSearchValue_InputEmpty_ReturnsTrue()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetPrivateField(benchmarks, "_source", string.Empty);
//         // Act
//         bool result = benchmarks.SkipWhiteSpace_PeekSearchValue();
//         // Assert
//         Assert.True(result, "Expected method to return true for an empty input string.");
//     }
// 
//     /// <summary>
//     /// Tests that when the input string starts with a non-white-space character, the method returns false.
//     /// The expectation is that if the first character is not a whitespace, the loop returns false immediately.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_PeekSearchValue_FirstCharNonWhiteSpace_ReturnsFalse()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetPrivateField(benchmarks, "_source", "A");
//         // Act
//         bool result = benchmarks.SkipWhiteSpace_PeekSearchValue();
//         // Assert
//         Assert.False(result, "Expected method to return false when the first character is not a whitespace.");
//     }
// 
//     /// <summary>
//     /// Tests that when the input string has leading white-space before the first non-white-space character, the method returns true.
//     /// The expectation is that whitespace is skipped and the method returns true after advancing the cursor.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_PeekSearchValue_LeadingWhiteSpaceBeforeNonWhiteSpace_ReturnsTrue()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetPrivateField(benchmarks, "_source", " A");
//         // Act
//         bool result = benchmarks.SkipWhiteSpace_PeekSearchValue();
//         // Assert
//         Assert.True(result, "Expected method to return true when the input starts with whitespace followed by a non-whitespace character.");
//     }
// 
//     /// <summary>
//     /// Tests that when the input string consists entirely of white-space characters, the method returns true.
//     /// The expectation is that after processing all characters (all of which are whitespace), the method returns true.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_PeekSearchValue_AllWhiteSpace_ReturnsTrue()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetPrivateField(benchmarks, "_source", "   ");
//         // Act
//         bool result = benchmarks.SkipWhiteSpace_PeekSearchValue();
//         // Assert
//         Assert.True(result, "Expected method to return true for an input string that contains only whitespace.");
//     }
// 
//     /// <summary>
//     /// Tests that when the input string is null, the method throws an ArgumentNullException.
//     /// The expectation is that creating a Scanner with a null source will result in an exception.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_PeekSearchValue_NullSource_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         SetPrivateField(benchmarks, "_source", null as string);
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => benchmarks.SkipWhiteSpace_PeekSearchValue());
//     }
// 
//     /// <summary>
//     /// Sets the value of a non-public field on the target object using reflection.
//     /// </summary>
//     /// <param name = "target">The object whose field is being set.</param>
//     /// <param name = "fieldName">The name of the field to set.</param>
//     /// <param name = "value">The value to assign to the field.</param>
//     private static void SetPrivateField(object target, string fieldName, object value)
//     {
//         var field = target.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
//         if (field == null)
//         {
//             throw new InvalidOperationException($"Field '{fieldName}' not found on type '{target.GetType().FullName}'.");
//         }
// 
//         field.SetValue(target, value);
//     }
// 
//     /// <summary>
//     /// Tests that when the source string is empty, the method returns true.
//     /// The method should call AdvanceNoNewLines with count 0 and return true.
//     /// </summary>
// //     [Fact] [Error] (529-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpaceOrNewLines_PeekSearchValue_EmptySource_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, "");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLines_PeekSearchValue();
// //         // Assert
// //         Assert.True(result);
// //     }
// 
//     /// <summary>
//     /// Tests that when the source string contains only whitespace and new line characters,
//     /// the method returns true after processing the entire span.
//     /// </summary>
// //     [Fact] [Error] (546-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpaceOrNewLines_PeekSearchValue_AllWhiteSpace_ReturnsTrue()
// //     {
// //         // Arrange
// //         var whiteSpaceSource = " \n\t\r  ";
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, whiteSpaceSource);
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLines_PeekSearchValue();
// //         // Assert
// //         Assert.True(result);
// //     }
// 
//     /// <summary>
//     /// Tests that when the source string has leading whitespace followed by a non-whitespace character,
//     /// the method advances past the whitespace and returns true.
//     /// </summary>
// //     [Fact] [Error] (563-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpaceOrNewLines_PeekSearchValue_LeadingWhiteSpaceNonWhite_ReturnsTrue()
// //     {
// //         // Arrange
// //         var source = "   a";
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, source);
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLines_PeekSearchValue();
// //         // Assert
// //         Assert.True(result);
// //     }
// 
//     /// <summary>
//     /// Tests that when the source string starts with a non-whitespace character,
//     /// the method returns false without advancing the cursor.
//     /// </summary>
// //     [Fact] [Error] (580-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpaceOrNewLines_PeekSearchValue_NonWhiteAtStart_ReturnsFalse()
// //     {
// //         // Arrange
// //         var source = "a   ";
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, source);
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpaceOrNewLines_PeekSearchValue();
// //         // Assert
// //         Assert.False(result);
// //     }
// 
//     /// <summary>
//     /// Tests that when the source is null, the method throws an exception.
//     /// It is assumed that a null source is invalid and should result in an ArgumentNullException.
//     /// </summary>
// //     [Fact] [Error] (596-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpaceOrNewLines_PeekSearchValue_NullSource_ThrowsException()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => benchmarks.SkipWhiteSpaceOrNewLines_PeekSearchValue());
// //     }
// 
//     /// <summary>
//     /// Helper method to set the private _source field of the SkipWhiteSpaceBenchmarks instance via reflection.
//     /// </summary>
//     /// <param name = "instance">The instance of SkipWhiteSpaceBenchmarks.</param>
//     /// <param name = "value">The string value to set as the _source field.</param>
// //     private static void SetPrivateSource(SkipWhiteSpaceBenchmarks instance, string value) [Error] (606-25)CS0111 Type 'SkipWhiteSpaceBenchmarksTests' already defines a member called 'SetPrivateSource' with the same parameter types [Error] (608-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     {
// //         FieldInfo field = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("Field '_source' not found in SkipWhiteSpaceBenchmarks.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_PeekCharacter returns false when the source starts 
//     /// with a non-whitespace character (i.e. no leading whitespace to skip).
//     /// </summary>
// //     [Fact] [Error] (626-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_PeekCharacter_SourceStartsWithNonWhitespace_ReturnsFalse()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, "a");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpace_PeekCharacter();
// //         // Assert
// //         Assert.False(result, "Expected false when the first character is non-whitespace.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_PeekCharacter returns true when the source has leading whitespace
//     /// followed by a non-whitespace character, indicating that whitespace was skipped.
//     /// </summary>
// //     [Fact] [Error] (642-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_PeekCharacter_LeadingWhitespaceFollowedByNonWhitespace_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, "   a");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpace_PeekCharacter();
// //         // Assert
// //         Assert.True(result, "Expected true when leading whitespace is correctly skipped.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_PeekCharacter returns true when the source consists entirely 
//     /// of whitespace characters, as the method should advance over all characters.
//     /// </summary>
// //     [Fact] [Error] (658-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_PeekCharacter_AllWhitespace_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, "     ");
// //         // Act
// //         bool result = benchmarks.SkipWhiteSpace_PeekCharacter();
// //         // Assert
// //         Assert.True(result, "Expected true when all characters are whitespace.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_PeekCharacter throws an ArgumentNullException when the source string is null.
//     /// This verifies the method's behavior when provided with an invalid input.
//     /// </summary>
// //     [Fact] [Error] (674-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpace_PeekCharacter_NullSource_ThrowsArgumentNullException()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<ArgumentNullException>(() => benchmarks.SkipWhiteSpace_PeekCharacter());
// //     }
// 
//     /// <summary>
//     /// Helper method to set the private '_source' field of a SkipWhiteSpaceBenchmarks instance using reflection.
//     /// </summary>
//     /// <param name = "instance">The instance of SkipWhiteSpaceBenchmarks.</param>
//     /// <param name = "value">The string value to assign to the _source field.</param>
// //     private static void SetPrivateSource(SkipWhiteSpaceBenchmarks instance, string value) [Error] (684-25)CS0111 Type 'SkipWhiteSpaceBenchmarksTests' already defines a member called 'SetPrivateSource' with the same parameter types
// //     {
// //         var field = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new Exception("Unable to find the '_source' field on SkipWhiteSpaceBenchmarks.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_PeekCharacter returns true when the source string is empty.
//     /// This test simulates the edge case where there are no characters to iterate and expects the method to return true.
//     /// </summary>
// //     [Fact] [Error] (704-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpaceOrNewLines_PeekCharacter_EmptySource_ReturnsTrue()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, string.Empty);
// //         // Act
// //         var result = benchmarks.SkipWhiteSpaceOrNewLines_PeekCharacter();
// //         // Assert
// //         Assert.True(result, "Expected true when source is empty.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_PeekCharacter returns false when the source string starts with a non-whitespace character.
//     /// The method should detect that no leading whitespace was skipped and thus return false immediately.
//     /// </summary>
// //     [Fact] [Error] (720-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpaceOrNewLines_PeekCharacter_NoLeadingWhiteSpace_ReturnsFalse()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, "a");
// //         // Act
// //         var result = benchmarks.SkipWhiteSpaceOrNewLines_PeekCharacter();
// //         // Assert
// //         Assert.False(result, "Expected false when source starts with a non-whitespace character.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_PeekCharacter returns true when the source string has leading whitespace followed by a non-whitespace character.
//     /// The method should advance the cursor by the count of leading whitespace and return true.
//     /// </summary>
//     /// <param name = "source">The input string with leading whitespace.</param>
// //     [Theory] [Error] (741-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     [InlineData(" a")]
// //     [InlineData("\ta")]
// //     [InlineData("\na")]
// //     [InlineData(" \na")]
// //     public void SkipWhiteSpaceOrNewLines_PeekCharacter_LeadingWhiteSpace_ReturnsTrue(string source)
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, source);
// //         // Act
// //         var result = benchmarks.SkipWhiteSpaceOrNewLines_PeekCharacter();
// //         // Assert
// //         Assert.True(result, $"Expected true when source '{source}' has leading whitespace followed by a non-whitespace character.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_PeekCharacter returns true when the source string consists entirely of whitespace.
//     /// The method should process the entire span and return true.
//     /// </summary>
//     /// <param name = "source">The input string composed entirely of whitespace.</param>
// //     [Theory] [Error] (762-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     [InlineData(" ")]
// //     [InlineData("   ")]
// //     [InlineData("\t\t")]
// //     [InlineData("\n\n")]
// //     public void SkipWhiteSpaceOrNewLines_PeekCharacter_AllWhiteSpace_ReturnsTrue(string source)
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, source);
// //         // Act
// //         var result = benchmarks.SkipWhiteSpaceOrNewLines_PeekCharacter();
// //         // Assert
// //         Assert.True(result, $"Expected true when source '{source}' consists exclusively of whitespace characters.");
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLines_PeekCharacter throws a NullReferenceException when _source is null.
//     /// Since the method instantiates a Scanner with a null source, a NullReferenceException is expected.
//     /// </summary>
// //     [Fact] [Error] (778-9)CS0121 The call is ambiguous between the following methods or properties: 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)' and 'SkipWhiteSpaceBenchmarksTests.SetPrivateSource(SkipWhiteSpaceBenchmarks, string)'
// //     public void SkipWhiteSpaceOrNewLines_PeekCharacter_NullSource_ThrowsException()
// //     {
// //         // Arrange
// //         var benchmarks = new SkipWhiteSpaceBenchmarks();
// //         SetPrivateSource(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.SkipWhiteSpaceOrNewLines_PeekCharacter());
// //     }
// 
//     /// <summary>
//     /// Helper method to set the private _source field of a SkipWhiteSpaceBenchmarks instance using reflection.
//     /// </summary>
//     /// <param name = "instance">An instance of SkipWhiteSpaceBenchmarks.</param>
//     /// <param name = "value">The string value to set for the _source field, can be null.</param>
// //     private static void SetPrivateSource(SkipWhiteSpaceBenchmarks instance, string value) [Error] (788-25)CS0111 Type 'SkipWhiteSpaceBenchmarksTests' already defines a member called 'SetPrivateSource' with the same parameter types
// //     {
// //         var field = typeof(SkipWhiteSpaceBenchmarks).GetField("_source", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("Field '_source' not found in SkipWhiteSpaceBenchmarks.");
// //         }
// // 
// //         field.SetValue(instance, value);
// //     }
// 
//     /// <summary>
//     /// Tests that the default value of the 'Length' property is zero.
//     /// </summary>
//     [Fact]
//     public void Length_DefaultValue_IsZero()
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         // Act
//         int defaultValue = benchmarks.Length;
//         // Assert
//         Assert.Equal(0, defaultValue);
//     }
// 
//     /// <summary>
//     /// Tests that the 'Length' property correctly gets and sets various valid values.
//     /// </summary>
//     /// <param name = "value">The value to assign to the Length property.</param>
//     [Theory]
//     [InlineData(0)]
//     [InlineData(1)]
//     [InlineData(2)]
//     [InlineData(10)]
//     [InlineData(-1)]
//     public void Length_GetSet_ReturnsAssignedValue(int value)
//     {
//         // Arrange
//         var benchmarks = new SkipWhiteSpaceBenchmarks();
//         // Act
//         benchmarks.Length = value;
//         int result = benchmarks.Length;
//         // Assert
//         Assert.Equal(value, result);
//     }
// }
