// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Parlot.Compilation;
// using Parlot.Fluent;
// using Xunit;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the <see cref="Not{T}"/> class.
//     /// </summary>
//     public class NotTests
//     {
//         /// <summary>
//         /// Tests that the constructor of Not{T} throws an ArgumentNullException when a null parser is provided.
//         /// </summary>
//         [Fact]
//         public void Constructor_NullParser_ThrowsArgumentNullException()
//         {
//             // Arrange & Act & Assert
//             var exception = Assert.Throws<ArgumentNullException>(() => new Not<string>(null));
//             Assert.Equal("parser", exception.ParamName);
//         }
// 
//         /// <summary>
//         /// Tests that Parse returns true when the inner parser fails (returns false) and does not reset the cursor position.
//         /// </summary>
// //         [Fact] [Error] (34-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParserForParse' to 'Parlot.Fluent.Parser<string>' [Error] (36-34)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(int)' and 'FakeCursor.FakeCursor(int)' [Error] (37-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeScanner.FakeScanner(FakeCursor)' and 'FakeScanner.FakeScanner(FakeCursor)' [Error] (38-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (39-26)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<string>' [Error] (42-48)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (42-65)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<string>' to 'ref Parlot.ParseResult<string>'
// //         public void Parse_WhenInnerParserReturnsFalse_ReturnsTrueAndDoesNotResetCursor()
// //         {
// //             // Arrange
// //             var fakeInnerParser = new FakeParserForParse(false);
// //             var notParser = new Not<string>(fakeInnerParser);
// // 
// //             var fakeCursor = new FakeCursor(10);
// //             var fakeScanner = new FakeScanner(fakeCursor);
// //             var fakeContext = new FakeParseContext(fakeScanner);
// //             var result = new ParseResult<string>();
// // 
// //             // Act
// //             bool parseResult = notParser.Parse(fakeContext, ref result);
// // 
// //             // Assert
// //             Assert.True(parseResult);
// //             Assert.False(fakeCursor.ResetCalled);
// //         }
// 
//         /// <summary>
//         /// Tests that Parse returns false when the inner parser succeeds (returns true) and resets the cursor to the starting position.
//         /// </summary>
// //         [Fact] [Error] (57-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParserForParse' to 'Parlot.Fluent.Parser<string>' [Error] (59-34)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(int)' and 'FakeCursor.FakeCursor(int)' [Error] (60-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeScanner.FakeScanner(FakeCursor)' and 'FakeScanner.FakeScanner(FakeCursor)' [Error] (61-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (62-26)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<string>' [Error] (65-48)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (65-65)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<string>' to 'ref Parlot.ParseResult<string>'
// //         public void Parse_WhenInnerParserReturnsTrue_ResetsCursorAndReturnsFalse()
// //         {
// //             // Arrange
// //             var fakeInnerParser = new FakeParserForParse(true);
// //             var notParser = new Not<string>(fakeInnerParser);
// // 
// //             var fakeCursor = new FakeCursor(15);
// //             var fakeScanner = new FakeScanner(fakeCursor);
// //             var fakeContext = new FakeParseContext(fakeScanner);
// //             var result = new ParseResult<string>();
// // 
// //             // Act
// //             bool parseResult = notParser.Parse(fakeContext, ref result);
// // 
// //             // Assert
// //             Assert.False(parseResult);
// //             Assert.True(fakeCursor.ResetCalled);
// //             Assert.Equal(15, fakeCursor.ResetCalledWith);
// //         }
// 
//         /// <summary>
//         /// Tests that Compile returns a CompilationResult containing an Expression.IfThenElse with the expected branches.
//         /// </summary>
// //         [Fact] [Error] (83-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParserForCompile' to 'Parlot.Fluent.Parser<string>' [Error] (85-35)CS1729 'FakeCompilationContext' does not contain a constructor that takes 0 arguments [Error] (88-77)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (93-46)CS0229 Ambiguity between 'CompilationResult<string>.Body' and 'CompilationResult<string>.Body' [Error] (94-45)CS0229 Ambiguity between 'CompilationResult<string>.Body' and 'CompilationResult<string>.Body' [Error] (97-47)CS0229 Ambiguity between 'CompilationResult<string>.Body' and 'CompilationResult<string>.Body'
// //         public void Compile_ReturnsCompilationResultWithIfThenElseExpression()
// //         {
// //             // Arrange
// //             // Test with inner parser's Build returning success expression as false.
// //             bool innerSuccess = false;
// //             var fakeInnerParser = new FakeParserForCompile(innerSuccess);
// //             var notParser = new Not<string>(fakeInnerParser);
// // 
// //             var fakeContext = new FakeCompilationContext();
// //             
// //             // Act
// //             CompilationResult<string> compilationResult = notParser.Compile(fakeContext);
// // 
// //             // Assert
// //             // Verify that a body was added in the compilation result.
// //             Assert.NotNull(compilationResult);
// //             Assert.NotNull(compilationResult.Body);
// //             Assert.Single(compilationResult.Body);
// // 
// //             // The added expression should be a BlockExpression containing an IfThenElse.
// //             var blockExpr = compilationResult.Body[0] as BlockExpression;
// //             Assert.NotNull(blockExpr);
// // 
// //             // Find the IfThenElse expression within the block.
// //             bool foundIfThenElse = false;
// //             foreach (var expression in blockExpr.Expressions)
// //             {
// //                 if (expression is ConditionalExpression condExpr)
// //                 {
// //                     foundIfThenElse = true;
// //                     // Check that the false branch assigns true to the result.Success
// //                     Assert.IsType<BinaryExpression>(condExpr.IfFalse);
// //                 }
// //             }
// //             Assert.True(foundIfThenElse, "Expected an IfThenElse expression in the compilation result body.");
// //         }
// 
//         /// <summary>
//         /// Tests that ToString returns the expected string format.
//         /// </summary>
// //         [Fact] [Error] (125-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParserForParse' to 'Parlot.Fluent.Parser<string>'
// //         public void ToString_ReturnsExpectedString()
// //         {
// //             // Arrange
// //             var fakeInnerParser = new FakeParserForParse(false)
// //             {
// //                 FakeToString = "FakeParser"
// //             };
// //             var notParser = new Not<string>(fakeInnerParser);
// // 
// //             // Act
// //             string toStringResult = notParser.ToString();
// // 
// //             // Assert
// //             Assert.Equal("Not (FakeParser)", toStringResult);
// //         }
//     }
// 
//     #region Fake / Helper Classes
// 
//     /// <summary>
//     /// A fake implementation of Parser&lt;T&gt; for testing the Parse method.
//     /// </summary>
// //     public class FakeParserForParse : Parser<string> [Error] (140-18)CS0534 'FakeParserForParse' does not implement inherited abstract member 'Parser<string>.Parse(object, ref ParseResult<string>)' [Error] (140-18)CS0534 'FakeParserForParse' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)' [Error] (140-18)CS0534 'FakeParserForParse' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)' [Error] (140-18)CS0534 'FakeParserForParse' does not implement inherited abstract member 'Parser<string>.Build(CompilationContext, bool)' [Error] (140-18)CS0534 'FakeParserForParse' does not implement inherited abstract member 'Parser<string>.Build(CompilationContext)'
// //     {
// //         private readonly bool _returnValue;
// // 
// //         /// <summary>
// //         /// Gets or sets a string to be returned by ToString.
// //         /// </summary>
// //         public string FakeToString { get; set; } = "FakeParserForParse";
// // 
// //         public FakeParserForParse(bool returnValue)
// //         {
// //             _returnValue = returnValue;
// //         }
// // 
// //         public override bool Parse(ParseContext context, ref ParseResult<string> result) [Error] (154-30)CS0462 The inherited members 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' have the same signature in type 'FakeParserForParse', so they cannot be overridden
// //         {
// //             return _returnValue;
// //         }
// 
//         public override CompilationResult Compile(CompilationContext context)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => FakeToString;
//     }
// 
//     /// <summary>
//     /// A fake implementation of Parser&lt;T&gt; for testing the Compile method.
//     /// </summary>
// //     public class FakeParserForCompile : Parser<string> [Error] (170-18)CS0534 'FakeParserForCompile' does not implement inherited abstract member 'Parser<string>.Parse(object, ref ParseResult<string>)' [Error] (170-18)CS0534 'FakeParserForCompile' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)' [Error] (170-18)CS0534 'FakeParserForCompile' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)' [Error] (170-18)CS0534 'FakeParserForCompile' does not implement inherited abstract member 'Parser<string>.Build(CompilationContext, bool)' [Error] (170-18)CS0534 'FakeParserForCompile' does not implement inherited abstract member 'Parser<string>.Build(CompilationContext)'
// //     {
// //         private readonly bool _buildSuccess;
// // 
// //         public FakeParserForCompile(bool buildSuccess)
// //         {
// //             _buildSuccess = buildSuccess;
// //         }
// // 
// //         public override bool Parse(ParseContext context, ref ParseResult<string> result) [Error] (179-30)CS0462 The inherited members 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' have the same signature in type 'FakeParserForCompile', so they cannot be overridden
// //         {
// //             throw new NotImplementedException();
// //         }
// 
// //         public override CompilationResult Compile(CompilationContext context) [Error] (187-34)CS0246 The type or namespace name 'FakeCompileResult' could not be found (are you missing a using directive or an assembly reference?)
// //         {
// //             // Create a fake compile result with a predetermined success expression.
// //             var fakeResult = new FakeCompileResult();
// //             fakeResult.Success = Expression.Constant(_buildSuccess, typeof(bool));
// //             // Variables and Body can be left empty for testing purposes.
// //             fakeResult.Variables = new List<ParameterExpression>();
// //             fakeResult.Body = new List<Expression>();
// //             return fakeResult;
// //         }
// 
//         public override string ToString() => "FakeParserForCompile";
//     }
// 
//     /// <summary>
//     /// A fake implementation of CompilationResult&lt;T&gt; for testing.
//     /// </summary>
// //     public class FakeCompilationResult : CompilationResult<string> [Error] (201-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationResult' [Error] (207-13)CS0229 Ambiguity between 'FakeCompilationResult.Success' and 'FakeCompilationResult.Success'
// //     {
// //         public FakeCompilationResult()
// //         {
// //             Body = new List<Expression>();
// //             // Create a dummy success parameter for assignment.
// //             Success = Expression.Variable(typeof(bool), "success");
// //         }
// //     }
// 
//     /// <summary>
//     /// A fake implementation of CompilationContext for testing the Compile method.
//     /// </summary>
// //     public class FakeCompilationContext : CompilationContext [Error] (214-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationContext'
// //     {
// //         public override CompilationResult<T> CreateCompilationResult<T>() [Error] (216-46)CS0111 Type 'FakeCompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types
// //         {
// //             return new FakeCompilationResult() as CompilationResult<T>;
// //         }
// // 
// //         public override ParameterExpression DeclarePositionVariable(CompilationResult result) [Error] (221-45)CS0111 Type 'FakeCompilationContext' already defines a member called 'DeclarePositionVariable' with the same parameter types
// //         {
// //             // Return a dummy parameter expression representing the cursor position.
// //             return Expression.Variable(typeof(int), "cursorPosition");
// //         }
// // 
// //         public override Expression ResetPosition(ParameterExpression positionVariable)
// //         {
// //             // Return an empty expression to simulate cursor reset.
// //             return Expression.Empty();
// //         }
// //     }
// 
//     /// <summary>
//     /// A fake implementation of ParseContext used for testing Parse.
//     /// </summary>
// //     public class FakeParseContext : ParseContext [Error] (237-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseContext' [Error] (239-16)CS0111 Type 'FakeParseContext' already defines a member called 'FakeParseContext' with the same parameter types [Error] (241-13)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner'
// //     {
// //         public FakeParseContext(FakeScanner scanner)
// //         {
// //             Scanner = scanner;
// //         }
// // 
// //         public override void EnterParser(Parser parser) [Error] (244-42)CS0305 Using the generic type 'Parser<T>' requires 1 type arguments [Error] (244-30)CS0111 Type 'FakeParseContext' already defines a member called 'EnterParser' with the same parameter types
// //         {
// //             // No operation required for testing.
// //         }
// // 
// //         public override void ExitParser(Parser parser) [Error] (249-41)CS0305 Using the generic type 'Parser<T>' requires 1 type arguments [Error] (249-30)CS0111 Type 'FakeParseContext' already defines a member called 'ExitParser' with the same parameter types
// //         {
// //             // No operation required for testing.
// //         }
// //     }
// 
//     /// <summary>
//     /// A fake implementation of a scanner that contains a cursor.
//     /// </summary>
// //     public class FakeScanner [Error] (258-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeScanner' [Error] (262-16)CS0111 Type 'FakeScanner' already defines a member called 'FakeScanner' with the same parameter types [Error] (264-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor'
// //     {
// //         public FakeCursor Cursor { get; }
// // 
// //         public FakeScanner(FakeCursor cursor)
// //         {
// //             Cursor = cursor;
// //         }
// //     }
// 
//     /// <summary>
//     /// A fake implementation of a cursor to test position management.
//     /// </summary>
// //     public class FakeCursor [Error] (271-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCursor' [Error] (279-16)CS0111 Type 'FakeCursor' already defines a member called 'FakeCursor' with the same parameter types [Error] (281-13)CS0229 Ambiguity between 'FakeCursor.Position' and 'FakeCursor.Position'
// //     {
// //         public int Position { get; private set; }
// // 
// //         public bool ResetCalled { get; private set; }
// // 
// //         public int ResetCalledWith { get; private set; }
// // 
// //         public FakeCursor(int initialPosition)
// //         {
// //             Position = initialPosition;
// //         }
// // 
// //         public void ResetPosition(int position) [Error] (288-13)CS0229 Ambiguity between 'FakeCursor.Position' and 'FakeCursor.Position'
// //         {
// //             ResetCalled = true;
// //             ResetCalledWith = position;
// //             Position = position;
// //         }
// //     }
// 
//     /// <summary>
//     /// A minimal implementation of ParseResult&lt;T&gt; used for testing.
//     /// </summary>
// //     public class ParseResult<T> [Error] (295-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ParseResult'
// //     {
// //         // Additional members can be added if necessary.
// //     }
// 
//     #endregion
// }
