// using Moq;
// using Parlot.Benchmarks;
// using System;
// using System.Reflection;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "IndexOfBenchmarks"/> class.
// /// </summary>
// public class IndexOfBenchmarksTests
// {
//     /// <summary>
//     /// Tests the RosIndexOfChar method to ensure that it returns the correct
//     /// index for the first occurrence of the backslash ('\') character in the
//     /// _helloWorldNoEscape string.
//     /// 
//     /// Steps:
//     /// 1. Retrieve the value of the private constant field "_helloWorldNoEscape"
//     ///    via reflection.
//     /// 2. Compute the expected index using AsSpan().IndexOf('\\') on that value.
//     /// 3. Invoke the RosIndexOfChar method.
//     /// 4. Assert that the returned value matches the expected index.
//     /// 
//     /// Expected outcome: The method returns an integer value equal to the index
//     /// of the first occurrence of the backslash character, or -1 if not found.
//     /// </summary>
// //     [Fact] [Error] (33-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (35-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void RosIndexOfChar_WhenCalled_ReturnsIndexOfFirstOccurrenceOfBackslash()
// //     {
// //         // Arrange
// //         var benchmarksInstance = new IndexOfBenchmarks();
// //         // Retrieve the value of the private const field "_helloWorldNoEscape" using reflection.
// //         FieldInfo fieldInfo = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.NonPublic | BindingFlags.Static);
// //         Assert.NotNull(fieldInfo);
// //         string helloWorldValue = fieldInfo.GetValue(null) as string;
// //         // Ensure that the field value is not null. In a valid benchmark, this value should be set.
// //         Assert.NotNull(helloWorldValue);
// //         int expectedIndex = helloWorldValue.AsSpan().IndexOf('\\');
// //         // Act
// //         int actualIndex = benchmarksInstance.RosIndexOfChar();
// //         // Assert
// //         Assert.Equal(expectedIndex, actualIndex);
// //     }
// 
//     /// <summary>
//     /// Tests that RosIndexOfString returns the expected index by comparing the result with the index computed from the underlying _helloWorldNoEscape string.
//     /// The test retrieves the constant value via reflection and uses it to determine the expected outcome.
//     /// </summary>
// //     [Fact] [Error] (54-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (61-34)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void RosIndexOfString_WhenCalled_ReturnsExpectedIndex()
// //     {
// //         // Arrange
// //         var instance = new IndexOfBenchmarks();
// //         FieldInfo field = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new Exception("Field _helloWorldNoEscape not found.");
// //         }
// // 
// //         // Retrieve the underlying string value used by RosIndexOfString.
// //         string underlyingValue = field.GetValue(null) as string;
// //         // If the underlying value is null, then calling AsSpan() on it would throw a NullReferenceException.
// //         if (underlyingValue is null)
// //         {
// //             Assert.Throws<NullReferenceException>(() => instance.RosIndexOfString());
// //             return;
// //         }
// // 
// //         int expected = underlyingValue.IndexOf("\\", StringComparison.Ordinal);
// //         // Act
// //         int actual = instance.RosIndexOfString();
// //         // Assert
// //         Assert.Equal(expected, actual);
// //     }
// 
//     /// <summary>
//     /// Tests that RosIndexOfString throws a NullReferenceException when the underlying _helloWorldNoEscape string is null.
//     /// This test attempts to modify the field via reflection and will be skipped if the field is a compile-time constant.
//     /// </summary>
// //     [Fact(Skip = "Cannot modify constant field _helloWorldNoEscape if it is compile-time constant.")] [Error] (85-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (92-32)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void RosIndexOfString_WhenUnderlyingStringIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var instance = new IndexOfBenchmarks();
// //         FieldInfo field = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new Exception("Field _helloWorldNoEscape not found.");
// //         }
// // 
// //         // Backup the original field value.
// //         object originalValue = field.GetValue(null);
// //         try
// //         {
// //             // Only attempt modification if the field is not a literal (compile-time constant).
// //             if (field.IsLiteral)
// //             {
// //                 return;
// //             }
// // 
// //             // Set _helloWorldNoEscape to null.
// //             field.SetValue(null, null);
// //             // Act & Assert: Expect a NullReferenceException from calling AsSpan() on a null string.
// //             Assert.Throws<NullReferenceException>(() => instance.RosIndexOfString());
// //         }
// //         finally
// //         {
// //             // Restore the original field value if possible.
// //             if (!field.IsLiteral)
// //             {
// //                 field.SetValue(null, originalValue);
// //             }
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests that RosIndexOfStringOrdinal returns the correct index when the backing string contains a backslash.
//     /// In the test, the string "test\\value" is used and it is expected to return the index position of the first backslash.
//     /// </summary>
// //     [Fact] [Error] (127-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (133-32)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void RosIndexOfStringOrdinal_WithBackslashInString_ReturnsCorrectIndex()
// //     {
// //         // Arrange
// //         const string testValue = "test\\value";
// //         int expectedIndex = testValue.AsSpan().IndexOf("\\", StringComparison.Ordinal);
// //         var benchmarksInstance = new IndexOfBenchmarks();
// //         FieldInfo field = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("Field _helloWorldNoEscape not found.");
// //         }
// // 
// //         object originalValue = field.GetValue(null);
// //         try
// //         {
// //             // Set the backing field to a string containing a backslash.
// //             field.SetValue(null, testValue);
// //             // Act
// //             int result = benchmarksInstance.RosIndexOfStringOrdinal();
// //             // Assert
// //             Assert.Equal(expectedIndex, result);
// //         }
// //         finally
// //         {
// //             // Restore the original value of the backing field.
// //             field.SetValue(null, originalValue);
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests that RosIndexOfStringOrdinal returns -1 when the backing string does not contain a backslash.
//     /// In this test, the string "hello" is used which does not contain a backslash.
//     /// </summary>
// //     [Fact] [Error] (161-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (167-32)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void RosIndexOfStringOrdinal_WithoutBackslashInString_ReturnsNegativeOne()
// //     {
// //         // Arrange
// //         const string testValue = "hello";
// //         int expectedIndex = testValue.AsSpan().IndexOf("\\", StringComparison.Ordinal);
// //         var benchmarksInstance = new IndexOfBenchmarks();
// //         FieldInfo field = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("Field _helloWorldNoEscape not found.");
// //         }
// // 
// //         object originalValue = field.GetValue(null);
// //         try
// //         {
// //             // Set the backing field to a string without a backslash.
// //             field.SetValue(null, testValue);
// //             // Act
// //             int result = benchmarksInstance.RosIndexOfStringOrdinal();
// //             // Assert
// //             Assert.Equal(expectedIndex, result);
// //         }
// //         finally
// //         {
// //             // Restore the original value of the backing field.
// //             field.SetValue(null, originalValue);
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests that RosIndexOfStringOrdinal throws a NullReferenceException when the backing string is null.
//     /// This verifies that calling AsSpan() on a null string triggers the expected exception.
//     /// </summary>
// //     [Fact] [Error] (192-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (194-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (200-32)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void RosIndexOfStringOrdinal_WithNullString_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         const string testValue = null;
// //         var benchmarksInstance = new IndexOfBenchmarks();
// //         FieldInfo field = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.NonPublic | BindingFlags.Static);
// //         if (field == null)
// //         {
// //             throw new InvalidOperationException("Field _helloWorldNoEscape not found.");
// //         }
// // 
// //         object originalValue = field.GetValue(null);
// //         try
// //         {
// //             // Set the backing field to null.
// //             field.SetValue(null, testValue);
// //             // Act & Assert
// //             Assert.Throws<NullReferenceException>(() => benchmarksInstance.RosIndexOfStringOrdinal());
// //         }
// //         finally
// //         {
// //             // Restore the original value of the backing field.
// //             field.SetValue(null, originalValue);
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "IndexOfBenchmarks.StringIndexOfChar"/> method to ensure it returns the expected index.
//     /// 
//     /// The test retrieves the compile-time constant value of <c>_helloWorldNoEscape</c> using reflection.
//     /// If the constant is defined (non-null), the expected result is computed using <c>String.IndexOf('\\')</c>
//     /// on that constant and compared with the actual result returned by the method.
//     /// If the constant is null, the method should throw a <see cref = "NullReferenceException"/>.
//     /// </summary>
// //     [Fact] [Error] (228-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void StringIndexOfChar_HappyPath_ReturnsExpectedIndexOrThrowsOnNull()
// //     {
// //         // Arrange
// //         var benchmarks = new IndexOfBenchmarks();
// //         FieldInfo field = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.Static | BindingFlags.NonPublic);
// //         Assert.NotNull(field);
// //         var constantValue = field.GetRawConstantValue() as string;
// //         // Act & Assert
// //         if (constantValue == null)
// //         {
// //             // If the constant value is null, calling the method should throw a NullReferenceException.
// //             Assert.Throws<NullReferenceException>(() => benchmarks.StringIndexOfChar());
// //         }
// //         else
// //         {
// //             int expectedIndex = constantValue.IndexOf('\\');
// //             int actualIndex = benchmarks.StringIndexOfChar();
// //             Assert.Equal(expectedIndex, actualIndex);
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "IndexOfBenchmarks.StringIndexOfString"/> method when the backing field
//     /// does not contain the search character, expecting an index of -1 as result.
//     /// </summary>
//     [Fact]
//     public void StringIndexOfString_WhenNoBackslashInString_ReturnsMinusOne()
//     {
//         // Arrange
//         // Instantiate the IndexOfBenchmarks class. 
//         // It is assumed that the constant _helloWorldNoEscape has a value that does not contain a backslash.
//         var benchmarks = new IndexOfBenchmarks();
//         // Act
//         int result = benchmarks.StringIndexOfString();
//         // Assert
//         // Since a backslash is not expected to be present, IndexOf should return -1.
//         Assert.Equal(-1, result);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "IndexOfBenchmarks.StringIndexOfString"/> method behavior when the backing field contains a backslash.
//     /// Due to the constant nature of _helloWorldNoEscape, reflection is used to simulate a scenario where it contains a backslash.
//     /// This test shows how the method would behave if the constant were changed (in a non-const scenario).
//     /// </summary>
//     [Fact]
//     public void StringIndexOfString_WhenBackslashPresent_ReturnsValidIndex()
//     {
//         // Arrange
//         // Create an instance of the benchmarks class.
//         var benchmarks = new IndexOfBenchmarks();
//         // Use reflection to set the value of the private constant field _helloWorldNoEscape.
//         // Note: In actual C#, const fields are inlined at compile time and cannot be changed via reflection.
//         // For the purpose of this test, it is assumed that _helloWorldNoEscape is a non-const field or that the
//         // method behavior can be simulated by overriding via reflection.
//         var type = typeof(IndexOfBenchmarks);
//         var field = type.GetField("_helloWorldNoEscape", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
//         if (field == null)
//         {
//             // Skip the test if the field cannot be accessed, as it is a const.
//             return;
//         }
// 
//         // Backup the original value if possible.
//         var originalValue = field.GetValue(null) as string;
//         try
//         {
//             // Set the field to a string containing a backslash.
//             field.SetValue(null, "Test\\String");
//             // Act
//             int result = benchmarks.StringIndexOfString();
//             // Assert
//             // Expecting the index of '\' in "Test\\String", which should be 4.
//             Assert.Equal(4, result);
//         }
//         finally
//         {
//             // Restore the original value.
//             if (field != null)
//             {
//                 field.SetValue(null, originalValue);
//             }
//         }
//     }
// 
//     /// <summary>
//     /// Tests that StringIndexOfStringOrdinal returns -1 when the source string does not contain a backslash.
//     /// </summary>
//     [Fact]
//     public void StringIndexOfStringOrdinal_WhenNoBackslashInString_ReturnsMinusOne()
//     {
//         // Arrange
//         string originalValue = GetPrivateConstantValue();
//         try
//         {
//             // Set the private constant _helloWorldNoEscape to a string with no backslash.
//             SetPrivateConstantValue("HelloWorld");
//             var benchmarks = new IndexOfBenchmarks();
//             // Act
//             int result = benchmarks.StringIndexOfStringOrdinal();
//             // Assert
//             Assert.Equal(-1, result);
//         }
//         finally
//         {
//             // Restore the original value.
//             SetPrivateConstantValue(originalValue);
//         }
//     }
// 
//     /// <summary>
//     /// Tests that StringIndexOfStringOrdinal returns the correct index when the source string contains a backslash.
//     /// </summary>
//     [Fact]
//     public void StringIndexOfStringOrdinal_WhenBackslashExists_ReturnsCorrectIndex()
//     {
//         // Arrange
//         string originalValue = GetPrivateConstantValue();
//         try
//         {
//             // "Hello\\World" has a backslash at index 5.
//             SetPrivateConstantValue("Hello\\World");
//             var benchmarks = new IndexOfBenchmarks();
//             // Act
//             int result = benchmarks.StringIndexOfStringOrdinal();
//             // Assert
//             Assert.Equal(5, result);
//         }
//         finally
//         {
//             // Restore the original value.
//             SetPrivateConstantValue(originalValue);
//         }
//     }
// 
//     /// <summary>
//     /// Tests that StringIndexOfStringOrdinal throws a NullReferenceException when the source string is null.
//     /// </summary>
// //     [Fact] [Error] (369-37)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void StringIndexOfStringOrdinal_WhenFieldIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         string originalValue = GetPrivateConstantValue();
// //         try
// //         {
// //             // Set the private constant _helloWorldNoEscape to null.
// //             SetPrivateConstantValue(null);
// //             var benchmarks = new IndexOfBenchmarks();
// //             // Act & Assert
// //             Assert.Throws<NullReferenceException>(() => benchmarks.StringIndexOfStringOrdinal());
// //         }
// //         finally
// //         {
// //             // Restore the original value.
// //             SetPrivateConstantValue(originalValue);
// //         }
// //     }
// 
//     /// <summary>
//     /// Retrieves the current value of the private constant field _helloWorldNoEscape.
//     /// </summary>
//     /// <returns>The value of _helloWorldNoEscape.</returns>
// //     private string GetPrivateConstantValue() [Error] (387-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (388-16)CS8602 Dereference of a possibly null reference. [Error] (388-16)CS8603 Possible null reference return.
// //     {
// //         FieldInfo field = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.Static | BindingFlags.NonPublic);
// //         return field.GetValue(null) as string;
// //     }
// 
//     /// <summary>
//     /// Sets a new value for the private constant field _helloWorldNoEscape.
//     /// Note: This uses reflection to override a compile-time constant for unit testing purposes.
//     /// </summary>
//     /// <param name = "value">The new string value to set.</param>
// //     private void SetPrivateConstantValue(string value) [Error] (398-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (401-9)CS8602 Dereference of a possibly null reference.
// //     {
// //         FieldInfo field = typeof(IndexOfBenchmarks).GetField("_helloWorldNoEscape", BindingFlags.Static | BindingFlags.NonPublic);
// //         // Although _helloWorldNoEscape is a const, for testing purposes we assume that the JIT does not inline its value
// //         // and that reflection can modify the field value.
// //         field.SetValue(null, value);
// //     }
// }
