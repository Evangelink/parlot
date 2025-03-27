// using Moq;
// using Parlot.Compilation;
// using Parlot.Fluent;
// using System;
// using System.Linq.Expressions;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "Always{T}"/> class.
// /// </summary>
// public class AlwaysTests
// {
//     /// <summary>
//     /// Verifies that the Parse method returns true, sets the result with the correct offsets and value,
//     /// and records calls to EnterParser and ExitParser when provided with a valid context.
//     /// </summary>
// //     [Fact] [Error] (23-31)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (24-21)CS0119 'ExpressionHelper.Scanner(CompilationContext)' is a method, which is not valid in the given context [Error] (25-26)CS0246 The type or namespace name 'TestParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
// //     public void Parse_ValidContext_ReturnsTrueAndSetsResult()
// //     {
// //         // Arrange
// //         int expectedValue = 42;
// //         var parser = new Always<int>(expectedValue);
// //         var testContext = new TestParseContext();
// //         testContext.Scanner.Cursor.Offset = 10;
// //         var result = new TestParseResult<int>();
// //         // Act
// //         bool returnValue = parser.Parse(testContext, ref result);
// //         // Assert
// //         Assert.True(returnValue);
// //         Assert.Equal(10, result.Start);
// //         Assert.Equal(10, result.End);
// //         Assert.Equal(expectedValue, result.Value);
// //         Assert.Equal(1, testContext.EnterParserCallCount);
// //         Assert.Equal(1, testContext.ExitParserCallCount);
// //     }
// 
//     /// <summary>
//     /// Verifies that the Parse method throws a NullReferenceException when the context is null.
//     /// </summary>
// //     [Fact] [Error] (45-26)CS0246 The type or namespace name 'TestParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (47-9)CS0619 'Assert.Throws<T>(Func<Task>)' is obsolete: 'You must call Assert.ThrowsAsync<T> (and await the result) when testing async code.'
// //     public void Parse_NullContext_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var parser = new Always<string>("test");
// //         var result = new TestParseResult<string>();
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => parser.Parse(null, ref result));
// //     }
// 
//     /// <summary>
//     /// Verifies that the Parse method correctly sets the result when the cursor offset is zero.
//     /// </summary>
// //     [Fact] [Error] (59-31)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (60-21)CS0119 'ExpressionHelper.Scanner(CompilationContext)' is a method, which is not valid in the given context [Error] (61-26)CS0246 The type or namespace name 'TestParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
// //     public void Parse_ZeroOffset_ReturnsTrueAndSetsResultToZero()
// //     {
// //         // Arrange
// //         string expectedValue = "always";
// //         var parser = new Always<string>(expectedValue);
// //         var testContext = new TestParseContext();
// //         testContext.Scanner.Cursor.Offset = 0;
// //         var result = new TestParseResult<string>();
// //         // Act
// //         bool returnValue = parser.Parse(testContext, ref result);
// //         // Assert
// //         Assert.True(returnValue);
// //         Assert.Equal(0, result.Start);
// //         Assert.Equal(0, result.End);
// //         Assert.Equal(expectedValue, result.Value);
// //     }
// 
//     /// <summary>
//     /// A fake implementation of CompilationResult for testing purposes.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
// //     private class FakeCompilationResult<T> : CompilationResult [Error] (91-16)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
// //     {
// //         /// <summary>
// //         /// Gets a value indicating whether the compilation was successful.
// //         /// </summary>
// //         public bool Success { get; } [Error] (80-21)CS9031 Required member 'CompilationResult.Success' cannot be hidden by 'AlwaysTests.FakeCompilationResult<T>.Success'.
//         /// <summary>
//         /// Gets the generated expression.
//         /// </summary>
//         public Expression Expression { get; }
// 
//         /// <summary>
//         /// Initializes a new instance of the <see cref = "FakeCompilationResult{T}"/> class.
//         /// </summary>
//         /// <param name = "success">Indicates if the compilation was successful.</param>
//         /// <param name = "expression">The generated expression.</param>
//         public FakeCompilationResult(bool success, Expression expression)
//         {
//             Success = success;
//             Expression = expression;
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method returns a compilation result containing a ConstantExpression with the expected integer value.
//     /// </summary>
//     [Fact]
//     public void Compile_WithValidContextAndInt_ReturnsCompilationResultWithConstantExpression()
//     {
//         // Arrange
//         int expectedValue = 42;
//         var always = new Always<int>(expectedValue);
//         var mockContext = new Mock<CompilationContext>();
//         Expression capturedExpression = null;
//         mockContext.Setup(c => c.CreateCompilationResult<int>(true, It.IsAny<Expression>())).Callback<bool, Expression>((success, expr) => capturedExpression = expr).Returns(() => new FakeCompilationResult<int>(true, capturedExpression));
//         // Act
//         var result = always.Compile(mockContext.Object);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<ConstantExpression>(capturedExpression);
//         var constantExpr = (ConstantExpression)capturedExpression;
//         Assert.Equal(expectedValue, constantExpr.Value);
//         Assert.Equal(typeof(int), constantExpr.Type);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method returns a compilation result containing a ConstantExpression with a null value when T is string.
//     /// </summary>
//     [Fact]
//     public void Compile_WithValidContextAndNullString_ReturnsCompilationResultWithNullConstantExpression()
//     {
//         // Arrange
//         string expectedValue = null;
//         var always = new Always<string>(expectedValue);
//         var mockContext = new Mock<CompilationContext>();
//         Expression capturedExpression = null;
//         mockContext.Setup(c => c.CreateCompilationResult<string>(true, It.IsAny<Expression>())).Callback<bool, Expression>((success, expr) => capturedExpression = expr).Returns(() => new FakeCompilationResult<string>(true, capturedExpression));
//         // Act
//         var result = always.Compile(mockContext.Object);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<ConstantExpression>(capturedExpression);
//         var constantExpr = (ConstantExpression)capturedExpression;
//         Assert.Null(constantExpr.Value);
//         Assert.Equal(typeof(string), constantExpr.Type);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method throws a NullReferenceException when a null CompilationContext is provided.
//     /// </summary>
//     [Fact]
//     public void Compile_WithNullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         int expectedValue = 42;
//         var always = new Always<int>(expectedValue);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => always.Compile(null));
//     }
// 
//     /// <summary>
//     /// Verifies that the Always constructor sets the Name property to "Always" when instantiated with a non-null int value.
//     /// Arrange: Creates an instance of Always{int} with a valid integer.
//     /// Act: Retrieves the Name property.
//     /// Assert: The Name property equals "Always" and the instance is created successfully.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithIntValue_SetsNameToAlways()
//     {
//         // Arrange
//         int inputValue = 42;
//         // Act
//         var alwaysParser = new Always<int>(inputValue);
//         // Assert
//         Assert.NotNull(alwaysParser);
//         Assert.Equal("Always", alwaysParser.Name);
//     }
// 
//     /// <summary>
//     /// Verifies that the Always constructor sets the Name property to "Always" when instantiated with a null string value.
//     /// Arrange: Creates an instance of Always{string} with a null value.
//     /// Act: Retrieves the Name property.
//     /// Assert: The Name property equals "Always" and the instance is created successfully.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithNullStringValue_SetsNameToAlways()
//     {
//         // Arrange
//         string inputValue = null;
//         // Act
//         var alwaysParser = new Always<string>(inputValue);
//         // Assert
//         Assert.NotNull(alwaysParser);
//         Assert.Equal("Always", alwaysParser.Name);
//     }
// }
