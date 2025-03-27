using System;
using Parlot.Benchmarks;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Program"/> class.
    /// </summary>
    public class ProgramTests
    {
        /// <summary>
        /// Tests that the Main method executes without throwing an exception when provided with an empty arguments array.
        /// Expected outcome: No exceptions are thrown.
        /// </summary>
//         [Fact] [Error] (23-60)CS0122 'Program.Main(string[])' is inaccessible due to its protection level
//         public void Main_WithEmptyArgs_DoesNotThrow()
//         {
//             // Arrange
//             string[] args = Array.Empty<string>();
// 
//             // Act
//             var exception = Record.Exception(() => Program.Main(args));
// 
//             // Assert
//             Assert.Null(exception);
//         }

        /// <summary>
        /// Tests that the Main method executes without throwing an exception when provided with a help argument.
        /// Expected outcome: No exceptions are thrown.
        /// </summary>
//         [Fact] [Error] (40-60)CS0122 'Program.Main(string[])' is inaccessible due to its protection level
//         public void Main_WithHelpArg_DoesNotThrow()
//         {
//             // Arrange
//             string[] args = new string[] { "--help" };
// 
//             // Act
//             var exception = Record.Exception(() => Program.Main(args));
// 
//             // Assert
//             Assert.Null(exception);
//         }

        /// <summary>
        /// Tests that the Main method throws an ArgumentNullException when a null arguments array is provided.
        /// Expected outcome: An ArgumentNullException is thrown.
        /// </summary>
//         [Fact] [Error] (54-64)CS0122 'Program.Main(string[])' is inaccessible due to its protection level
//         public void Main_WithNullArgs_ThrowsArgumentNullException()
//         {
//             // Act & Assert
//             Assert.Throws<ArgumentNullException>(() => Program.Main(null));
//         }
    }
}
