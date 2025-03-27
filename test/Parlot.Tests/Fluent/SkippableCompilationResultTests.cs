using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="SkippableCompilationResult"/> class.
    /// </summary>
    public class SkippableCompilationResultTests
    {
        /// <summary>
        /// Tests that the constructor correctly assigns the properties when provided with a non-null CompilationResult and a skip flag.
        /// </summary>
//         [Fact] [Error] (20-37)CS0144 Cannot create an instance of the abstract type or interface 'CompilationResult' [Error] (24-77)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.CompilationResult' to 'Parlot.Compilation.CompilationResult' [Error] (27-26)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.CompilationResult' to 'System.DateTime' [Error] (27-45)CS1503 Argument 2: cannot convert from 'Parlot.Compilation.CompilationResult' to 'System.DateTime'
//         public void Constructor_WithValidCompilationResult_SetsPropertiesCorrectly()
//         {
//             // Arrange
//             var compilationResult = new CompilationResult();
//             bool skipFlag = true;
// 
//             // Act
//             var skippableCompilationResult = new SkippableCompilationResult(compilationResult, skipFlag);
// 
//             // Assert
//             Assert.Equal(compilationResult, skippableCompilationResult.CompilationResult);
//             Assert.Equal(skipFlag, skippableCompilationResult.Skip);
//         }

        /// <summary>
        /// Tests that the constructor correctly assigns the properties when a null CompilationResult is provided.
        /// </summary>
//         [Fact] [Error] (38-30)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (42-77)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.CompilationResult' to 'Parlot.Compilation.CompilationResult'
//         public void Constructor_WithNullCompilationResult_AllowsNullValue()
//         {
//             // Arrange
//             CompilationResult? compilationResult = null;
//             bool skipFlag = false;
// 
//             // Act
//             var skippableCompilationResult = new SkippableCompilationResult(compilationResult, skipFlag);
// 
//             // Assert
//             Assert.Null(skippableCompilationResult.CompilationResult);
//             Assert.Equal(skipFlag, skippableCompilationResult.Skip);
//         }

        /// <summary>
        /// Tests that the properties of SkippableCompilationResult can be updated after object creation.
        /// </summary>
//         [Fact] [Error] (56-44)CS0144 Cannot create an instance of the abstract type or interface 'CompilationResult' [Error] (57-77)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.CompilationResult' to 'Parlot.Compilation.CompilationResult' [Error] (58-40)CS0144 Cannot create an instance of the abstract type or interface 'CompilationResult' [Error] (62-60)CS0029 Cannot implicitly convert type 'Parlot.Fluent.UnitTests.CompilationResult' to 'Parlot.Compilation.CompilationResult' [Error] (66-26)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.CompilationResult' to 'System.DateTime' [Error] (66-48)CS1503 Argument 2: cannot convert from 'Parlot.Compilation.CompilationResult' to 'System.DateTime'
//         public void Properties_Setter_UpdatesValuesCorrectly()
//         {
//             // Arrange
//             var initialCompilationResult = new CompilationResult();
//             var skippableCompilationResult = new SkippableCompilationResult(initialCompilationResult, false);
//             var newCompilationResult = new CompilationResult();
//             bool newSkipFlag = true;
// 
//             // Act
//             skippableCompilationResult.CompilationResult = newCompilationResult;
//             skippableCompilationResult.Skip = newSkipFlag;
// 
//             // Assert
//             Assert.Equal(newCompilationResult, skippableCompilationResult.CompilationResult);
//             Assert.Equal(newSkipFlag, skippableCompilationResult.Skip);
//         }
    }
}
