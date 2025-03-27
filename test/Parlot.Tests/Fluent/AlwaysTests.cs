// using Moq;
// using Parlot.Compilation;
// using Parlot.Fluent;
// using Parlot.Tests.Calc;
// using System;
// using System.Linq.Expressions;
// using Xunit;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Fake implementation of ParseContext for testing purposes.
//     /// </summary>
// //     public class FakeParseContext : ParseContext [Error] (14-18)CS0534 'FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(Parser)' [Error] (14-18)CS0534 'FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(object)' [Error] (14-18)CS0534 'FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(object)' [Error] (14-18)CS0534 'FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(Parser)' [Error] (18-13)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner'
// //     {
// //         public FakeParseContext(int offset)
// //         {
// //             Scanner = new FakeScanner(offset);
// //         }
// // 
// //         public override void EnterParser(object parser)
// //         {
// //             EnterCalled = true;
// //             EnteredParser = parser;
// //         }
// // 
// //         public override void ExitParser(object parser)
// //         {
// //             ExitCalled = true;
// //             ExitedParser = parser;
// //         }
// // 
// //         public bool EnterCalled { get; private set; }
// //         public bool ExitCalled { get; private set; }
// //         public object EnteredParser { get; private set; }
// //         public object ExitedParser { get; private set; }
// //         public FakeScanner Scanner { get; } [Error] (37-28)CS0108 'FakeParseContext.Scanner' hides inherited member 'ParseContext.Scanner'. Use the new keyword if hiding was intended.
//     }
// 
//     /// <summary>
//     /// Fake implementation of a scanner containing a cursor.
//     /// </summary>
// //     public class FakeScanner [Error] (47-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (47-26)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(int)' and 'FakeCursor.FakeCursor(int)'
// //     {
// //         public FakeScanner(int offset)
// //         {
// //             Cursor = new FakeCursor(offset);
// //         }
// // 
// //         public FakeCursor Cursor { get; }
// //     }
// 
//     /// <summary>
//     /// Fake implementation of a cursor.
//     /// </summary>
// //     public class FakeCursor [Error] (60-13)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
// //     {
// //         public FakeCursor(int offset)
// //         {
// //             Offset = offset;
// //         }
// // 
// //         public int Offset { get; }
// //     }
// 
//     /// <summary>
//     /// Fake implementation of ParseResult for testing purposes.
//     /// </summary>
//     /// <typeparam name = "T"></typeparam>
// //     public class FakeParseResult<T> : ParseResult<T> [Error] (70-18)CS0534 'FakeParseResult<T>' does not implement inherited abstract member 'ParseResult<T>.Set(int, int, T)'
// //     {
// //         public int Start { get; private set; } [Error] (72-20)CS0108 'FakeParseResult<T>.Start' hides inherited member 'ParseResult<T>.Start'. Use the new keyword if hiding was intended.
// //         public int End { get; private set; } [Error] (73-20)CS0108 'FakeParseResult<T>.End' hides inherited member 'ParseResult<T>.End'. Use the new keyword if hiding was intended.
// //         public T Value { get; private set; } [Error] (74-18)CS0108 'FakeParseResult<T>.Value' hides inherited member 'ParseResult<T>.Value'. Use the new keyword if hiding was intended.
// 
// //         public override void Set(int start, int end, T value) [Error] (76-30)CS0462 The inherited members 'ParseResult<T>.Set(int, int, T)' and 'ParseResult<T>.Set(int, int, T)' have the same signature in type 'FakeParseResult<T>', so they cannot be overridden [Error] (78-13)CS0229 Ambiguity between 'FakeParseResult<T>.Start' and 'FakeParseResult<T>.Start' [Error] (79-13)CS0229 Ambiguity between 'FakeParseResult<T>.End' and 'FakeParseResult<T>.End' [Error] (80-13)CS0229 Ambiguity between 'FakeParseResult<T>.Value' and 'FakeParseResult<T>.Value'
// //         {
// //             Start = start;
// //             End = end;
// //             Value = value;
// //         }
//     }
// 
//     /// <summary>
//     /// Fake implementation of CompilationContext for testing purposes.
//     /// </summary>
// //     public class FakeCompilationContext : CompilationContext [Error] (87-18)CS0534 'FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (87-18)CS0534 'FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (87-18)CS0534 'FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ParseContext.get' [Error] (87-18)CS0534 'FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (87-18)CS0534 'FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.Lambdas.get' [Error] (87-18)CS0534 'FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (87-18)CS0534 'FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get'
// //     {
// //         public override CompilationResult CreateCompilationResult<T>(bool success, Expression expression) [Error] (89-84)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression' [Error] (91-20)CS0029 Cannot implicitly convert type 'Parlot.Fluent.UnitTests.FakeCompilationResult' to 'Parlot.Fluent.UnitTests.CompilationResult'
// //         {
// //             return new FakeCompilationResult(success, expression);
// //         }
//     }
// 
//     /// <summary>
//     /// Fake implementation of CompilationResult for testing purposes.
//     /// </summary>
// //     public class FakeCompilationResult : CompilationResult [Error] (98-18)CS0263 Partial declarations of 'FakeCompilationResult' must not specify different base classes [Error] (100-52)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression' [Error] (102-13)CS0229 Ambiguity between 'FakeCompilationResult.Success' and 'FakeCompilationResult.Success'
// //     {
// //         public FakeCompilationResult(bool success, Expression expression)
// //         {
// //             Success = success;
// //             Expression = expression;
// //         }
// // 
// //         public override bool Success { get; }
// //         public override Expression Expression { get; } [Error] (107-25)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression'
// //     }
// 
//     /// <summary>
//     /// Unit tests for the Always&lt;T&gt; class.
//     /// </summary>
//     public class AlwaysTests
//     {
//         private readonly int _offset;
//         private readonly int _intValue;
//         public AlwaysTests()
//         {
//             _offset = 10;
//             _intValue = 42;
//         }
// 
//         /// <summary>
//         /// Tests that the Always constructor initializes the Name property to "Always".
//         /// </summary>
//         [Fact]
//         public void Constructor_InitializesNameToAlways()
//         {
//             // Arrange & Act
//             var alwaysParser = new Always<int>(_intValue);
//             // Assert
//             Assert.Equal("Always", alwaysParser.Name);
//         }
// 
//         /// <summary>
//         /// Tests that Parse method returns true and sets the result with correct start, end and value.
//         /// </summary>
// //         [Fact] [Error] (144-46)CS0452 The type 'int' must be a reference type in order to use it as parameter 'T' in the generic type or method 'FakeParseResult<T>' [Error] (146-53)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (146-66)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<int>' to 'ref Parlot.ParseResult<int>' [Error] (149-42)CS0229 Ambiguity between 'FakeParseResult<int>.Start' and 'FakeParseResult<int>.Start' [Error] (150-42)CS0229 Ambiguity between 'FakeParseResult<int>.End' and 'FakeParseResult<int>.End' [Error] (151-44)CS0229 Ambiguity between 'FakeParseResult<int>.Value' and 'FakeParseResult<int>.Value'
// //         public void Parse_ValidContextAndResult_ReturnsTrueAndSetsResult()
// //         {
// //             // Arrange
// //             var alwaysParser = new Always<int>(_intValue);
// //             var context = new FakeParseContext(_offset);
// //             var result = new FakeParseResult<int>();
// //             // Act
// //             bool parseReturned = alwaysParser.Parse(context, ref result);
// //             // Assert
// //             Assert.True(parseReturned);
// //             Assert.Equal(_offset, result.Start);
// //             Assert.Equal(_offset, result.End);
// //             Assert.Equal(_intValue, result.Value);
// //             Assert.True(context.EnterCalled);
// //             Assert.True(context.ExitCalled);
// //             Assert.Equal(alwaysParser, context.EnteredParser);
// //             Assert.Equal(alwaysParser, context.ExitedParser);
// //         }
// 
//         /// <summary>
//         /// Tests that Parse method throws a NullReferenceException when the context is null.
//         /// </summary>
// //         [Fact] [Error] (167-46)CS0452 The type 'int' must be a reference type in order to use it as parameter 'T' in the generic type or method 'FakeParseResult<T>' [Error] (169-76)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (169-89)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<int>' to 'ref Parlot.ParseResult<int>'
// //         public void Parse_NullContext_ThrowsNullReferenceException()
// //         {
// //             // Arrange
// //             var alwaysParser = new Always<int>(_intValue);
// //             FakeParseContext context = null;
// //             var result = new FakeParseResult<int>();
// //             // Act & Assert
// //             Assert.Throws<NullReferenceException>(() => alwaysParser.Parse(context, ref result));
// //         }
// 
//         /// <summary>
//         /// Tests that Parse method throws a NullReferenceException when the result is null.
//         /// </summary>
// //         [Fact] [Error] (181-29)CS0452 The type 'int' must be a reference type in order to use it as parameter 'T' in the generic type or method 'FakeParseResult<T>' [Error] (183-76)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (183-89)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<int>' to 'ref Parlot.ParseResult<int>'
// //         public void Parse_NullResult_ThrowsNullReferenceException()
// //         {
// //             // Arrange
// //             var alwaysParser = new Always<int>(_intValue);
// //             var context = new FakeParseContext(_offset);
// //             FakeParseResult<int> result = null;
// //             // Act & Assert
// //             Assert.Throws<NullReferenceException>(() => alwaysParser.Parse(context, ref result));
// //         }
// 
//         /// <summary>
//         /// Tests that Compile method returns a successful CompilationResult with a ConstantExpression matching the expected value.
//         /// </summary>
// //         [Fact] [Error] (194-42)CS1729 'FakeCompilationContext' does not contain a constructor that takes 0 arguments [Error] (196-68)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (199-25)CS1503 Argument 1: cannot convert from 'System.Linq.Expressions.Expression' to 'bool' [Error] (200-61)CS1061 'CompilationResult' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'CompilationResult' could be found (are you missing a using directive or an assembly reference?) [Error] (201-63)CS1061 'CompilationResult' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'CompilationResult' could be found (are you missing a using directive or an assembly reference?)
// //         public void Compile_ValidContext_ReturnsCompilationResultWithConstantExpression()
// //         {
// //             // Arrange
// //             var alwaysParser = new Always<int>(_intValue);
// //             var compilationContext = new FakeCompilationContext();
// //             // Act
// //             CompilationResult compileResult = alwaysParser.Compile(compilationContext);
// //             // Assert
// //             Assert.NotNull(compileResult);
// //             Assert.True(compileResult.Success);
// //             Assert.IsType<ConstantExpression>(compileResult.Expression);
// //             var constExpr = (ConstantExpression)compileResult.Expression;
// //             Assert.Equal(_intValue, constExpr.Value);
// //             Assert.Equal(typeof(int), constExpr.Type);
// //         }
// 
//         /// <summary>
//         /// Tests that Compile method throws a NullReferenceException when the compilation context is null.
//         /// </summary>
// //         [Fact] [Error] (216-78)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
// //         public void Compile_NullContext_ThrowsNullReferenceException()
// //         {
// //             // Arrange
// //             var alwaysParser = new Always<int>(_intValue);
// //             FakeCompilationContext compilationContext = null;
// //             // Act & Assert
// //             Assert.Throws<NullReferenceException>(() => alwaysParser.Compile(compilationContext));
// //         }
// 
//         /// <summary>
//         /// Tests that Always works correctly with a reference type when the value is null.
//         /// </summary>
// //         [Fact] [Error] (231-53)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (231-66)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<string>' to 'ref Parlot.ParseResult<string>' [Error] (234-42)CS0229 Ambiguity between 'FakeParseResult<string>.Start' and 'FakeParseResult<string>.Start' [Error] (235-42)CS0229 Ambiguity between 'FakeParseResult<string>.End' and 'FakeParseResult<string>.End' [Error] (236-32)CS0229 Ambiguity between 'FakeParseResult<string>.Value' and 'FakeParseResult<string>.Value'
// //         public void Parse_WithNullValueForReferenceType_ReturnsTrueAndSetsResultToNull()
// //         {
// //             // Arrange
// //             string nullValue = null;
// //             var alwaysParser = new Always<string>(nullValue);
// //             var context = new FakeParseContext(_offset);
// //             var result = new FakeParseResult<string>();
// //             // Act
// //             bool parseReturned = alwaysParser.Parse(context, ref result);
// //             // Assert
// //             Assert.True(parseReturned);
// //             Assert.Equal(_offset, result.Start);
// //             Assert.Equal(_offset, result.End);
// //             Assert.Null(result.Value);
// //         }
// 
//         /// <summary>
//         /// Tests that Compile method returns a CompilationResult with a ConstantExpression containing null for a reference type.
//         /// </summary>
// //         [Fact] [Error] (248-42)CS1729 'FakeCompilationContext' does not contain a constructor that takes 0 arguments [Error] (250-68)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (253-25)CS1503 Argument 1: cannot convert from 'System.Linq.Expressions.Expression' to 'bool' [Error] (254-61)CS1061 'CompilationResult' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'CompilationResult' could be found (are you missing a using directive or an assembly reference?) [Error] (255-63)CS1061 'CompilationResult' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'CompilationResult' could be found (are you missing a using directive or an assembly reference?)
// //         public void Compile_WithNullValueForReferenceType_ReturnsCompilationResultWithNullConstantExpression()
// //         {
// //             // Arrange
// //             string nullValue = null;
// //             var alwaysParser = new Always<string>(nullValue);
// //             var compilationContext = new FakeCompilationContext();
// //             // Act
// //             CompilationResult compileResult = alwaysParser.Compile(compilationContext);
// //             // Assert
// //             Assert.NotNull(compileResult);
// //             Assert.True(compileResult.Success);
// //             Assert.IsType<ConstantExpression>(compileResult.Expression);
// //             var constExpr = (ConstantExpression)compileResult.Expression;
// //             Assert.Null(constExpr.Value);
// //             Assert.Equal(typeof(string), constExpr.Type);
// //         }
//     }
// }
