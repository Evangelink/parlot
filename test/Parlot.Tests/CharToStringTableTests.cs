using Parlot;
using System;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "CharToStringTable"/> class.
/// </summary>
public class CharToStringTableTests
{
    /// <summary>
    /// Tests that GetString returns the corresponding string from the table when the input character index is within the bounds of the table.
    /// </summary>
//     [Fact] [Error] (28-25)CS0122 'CharToStringTable' is inaccessible due to its protection level
//     public void GetString_WhenCharWithinTableIndex_ReturnsMappedString()
//     {
//         // Arrange
//         string[] customTable = new string[]
//         {
//             "zero",
//             "one",
//             "two"
//         };
//         SetPrivateTableField(customTable);
//         char inputChar = (char)1; // Within range; expecting "one".
//         string expected = "one";
//         // Act
//         string actual = CharToStringTable.GetString(inputChar);
//         // Assert
//         Assert.Equal(expected, actual);
//     }

    /// <summary>
    /// Tests that GetString returns the character's string representation when the input character index is outside the bounds of the table.
    /// </summary>
//     [Fact] [Error] (50-25)CS0122 'CharToStringTable' is inaccessible due to its protection level
//     public void GetString_WhenCharOutOfTableIndex_ReturnsCharToString()
//     {
//         // Arrange
//         string[] customTable = new string[]
//         {
//             "zero",
//             "one",
//             "two"
//         };
//         SetPrivateTableField(customTable);
//         char inputChar = (char)5; // Outside range; expecting inputChar.ToString().
//         string expected = inputChar.ToString();
//         // Act
//         string actual = CharToStringTable.GetString(inputChar);
//         // Assert
//         Assert.Equal(expected, actual);
//     }

    /// <summary>
    /// Tests that GetString returns the character's string representation when the table is empty.
    /// </summary>
//     [Fact] [Error] (67-25)CS0122 'CharToStringTable' is inaccessible due to its protection level
//     public void GetString_WhenTableIsEmpty_ReturnsCharToString()
//     {
//         // Arrange
//         string[] customTable = new string[0];
//         SetPrivateTableField(customTable);
//         char inputChar = 'a'; // Any character; table is empty.
//         string expected = inputChar.ToString();
//         // Act
//         string actual = CharToStringTable.GetString(inputChar);
//         // Assert
//         Assert.Equal(expected, actual);
//     }

    /// <summary>
    /// Helper method that sets the private static readonly _table field in CharToStringTable using reflection.
    /// </summary>
    /// <param name = "table">The custom char-to-string mapping table to assign.</param>
//     private static void SetPrivateTableField(string[] table) [Error] (78-38)CS0122 'CharToStringTable' is inaccessible due to its protection level
//     {
//         FieldInfo fieldInfo = typeof(CharToStringTable).GetField("_table", BindingFlags.NonPublic | BindingFlags.Static);
//         if (fieldInfo == null)
//         {
//             throw new InvalidOperationException("Unable to find the _table field on CharToStringTable.");
//         }
// 
//         fieldInfo.SetValue(null, table);
//     }

    /// <summary>
    /// Tests that the static constructor of <see cref = "CharToStringTable"/> correctly initializes the char-to-string mapping table.
    /// This is achieved by using reflection to access the private fields and validating that each index is mapped to the correct string representation.
    /// Expected outcome: Each element at index i in the table equals ((char)i).ToString().
    /// </summary>
//     [Fact] [Error] (96-28)CS0122 'CharToStringTable' is inaccessible due to its protection level
//     public void StaticConstructor_ShouldInitializeTableCorrectly()
//     {
//         // Arrange: Get the type and use reflection to obtain the private fields.
//         Type type = typeof(CharToStringTable);
//         FieldInfo tableField = type.GetField("_table", BindingFlags.NonPublic | BindingFlags.Static);
//         FieldInfo sizeField = type.GetField("_size", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(tableField);
//         Assert.NotNull(sizeField);
//         // Act: Retrieve the values of the private fields.
//         var tableValue = tableField.GetValue(null) as string[];
//         // For constant fields, use GetRawConstantValue.
//         int sizeValue = (int)sizeField.GetRawConstantValue();
//         // Assert: Ensure the table is initialized and its length matches the defined size.
//         Assert.NotNull(tableValue);
//         Assert.Equal(sizeValue, tableValue.Length);
//         // Assert: Validate that each element is correctly set.
//         for (int i = 0; i < sizeValue; i++)
//         {
//             string expected = ((char)i).ToString();
//             Assert.Equal(expected, tableValue[i]);
//         }
//     }
}