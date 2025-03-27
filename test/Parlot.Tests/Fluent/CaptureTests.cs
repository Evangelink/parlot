// using Moq;
// using Parlot;
// using Parlot.Compilation;
// using Parlot.Fluent;
// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Xunit;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Fake implementation of a position used in parsing.
//     /// </summary>
//     internal class FakePosition
//     {
//         public int Offset { get; set; }
// 
//         public FakePosition(int offset)
//         {
//             Offset = offset;
//         }
//     }
// 
//     /// <summary>
//     /// Fake implementation of a cursor used in parsing.
//     /// </summary>
// //     internal class FakeCursor [Error] (28-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCursor' [Error] (33-16)CS0111 Type 'FakeCursor' already defines a member called 'FakeCursor' with the same parameter types [Error] (35-13)CS0229 Ambiguity between 'FakeCursor.Position' and 'FakeCursor.Position' [Error] (36-13)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
// //     {
// //         public FakePosition Position { get; set; } [Error] (30-29)CS0053 Inconsistent accessibility: property type 'FakePosition' is less accessible than property 'FakeCursor.Position'
// //         public int Offset { get; set; }
// // 
// //         public FakeCursor(int offset)
// //         {
// //             Position = new FakePosition(offset);
// //             Offset = offset;
// //         }
// //     }
// 
//     /// <summary>
//     /// Fake implementation of a scanner used in parsing.
//     /// </summary>
// //     internal class FakeScanner [Error] (43-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeScanner' [Error] (50-13)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (51-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (51-26)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(int)' and 'FakeCursor.FakeCursor(int)'
// //     {
// //         public string Buffer { get; set; }
// //         public FakeCursor Cursor { get; set; }
// // 
// //         public FakeScanner(string buffer, int initialOffset)
// //         {
// //             Buffer = buffer;
// //             Cursor = new FakeCursor(initialOffset);
// //         }
// //     }
// 
//     /// <summary>
//     /// Fake implementation of a ParseContext used for testing.
//     /// </summary>
// //     internal class FakeParseContext : ParseContext [Error] (58-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseContext' [Error] (66-13)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner'
// //     {
// //         public FakeScanner FakeScanner { get; }
// // 
// //         public FakeParseContext(string buffer, int initialOffset)
// //         {
// //             FakeScanner = new FakeScanner(buffer, initialOffset);
// //             // Redirect Scanner property to our fake scanner.
// //             Scanner = new FakeScannerAdapter(FakeScanner);
// //         }
// // 
// //         public override void EnterParser(object parser) [Error] (69-30)CS0111 Type 'FakeParseContext' already defines a member called 'EnterParser' with the same parameter types
// //         {
// //         // No op for fake implementation.
// //         }
// // 
// //         public override void ExitParser(object parser) [Error] (74-30)CS0111 Type 'FakeParseContext' already defines a member called 'ExitParser' with the same parameter types
// //         {
// //         // No op for fake implementation.
// //         }
// // 
// //         /// <summary>
// //         /// Adapter to wrap FakeScanner as the expected Scanner type.
// //         /// </summary>
// //         private class FakeScannerAdapter : Scanner [Error] (85-20)CS7036 There is no argument given that corresponds to the required parameter 'buffer' of 'Scanner.Scanner(string)'
// //         {
// //             private readonly FakeScanner _fakeScanner;
// //             public FakeScannerAdapter(FakeScanner fakeScanner)
// //             {
// //                 _fakeScanner = fakeScanner;
// //             }
// // 
// //             public override string Buffer => _fakeScanner.Buffer; [Error] (90-36)CS0544 'FakeParseContext.FakeScannerAdapter.Buffer': cannot override because 'Scanner.Buffer' is not a property [Error] (90-59)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer'
// //             public override Cursor Cursor => new FakeCursorAdapter(_fakeScanner.Cursor); [Error] (91-36)CS0544 'FakeParseContext.FakeScannerAdapter.Cursor': cannot override because 'Scanner.Cursor' is not a property [Error] (91-81)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor'
// // 
// //             /// <summary>
// //             /// Adapter to wrap FakeCursor as the expected Cursor type.
// //             /// </summary>
// //             private class FakeCursorAdapter : Cursor [Error] (99-24)CS1729 'Cursor' does not contain a constructor that takes 0 arguments
// //             {
// //                 private readonly FakeCursor _fakeCursor;
// //                 public FakeCursorAdapter(FakeCursor fakeCursor)
// //                 {
// //                     _fakeCursor = fakeCursor;
// //                 }
// // 
// //                 public override int Offset { get => _fakeCursor.Offset; set => _fakeCursor.Offset = value; } [Error] (104-37)CS0506 'FakeParseContext.FakeScannerAdapter.FakeCursorAdapter.Offset': cannot override inherited member 'Cursor.Offset' because it is not marked virtual, abstract, or override [Error] (104-65)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset' [Error] (104-92)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
// //                 public override Position Position => new FakePositionAdapter(_fakeCursor.Position); [Error] (105-33)CS0246 The type or namespace name 'Position' could not be found (are you missing a using directive or an assembly reference?) [Error] (105-42)CS0506 'FakeParseContext.FakeScannerAdapter.FakeCursorAdapter.Position': cannot override inherited member 'Cursor.Position' because it is not marked virtual, abstract, or override [Error] (105-90)CS0229 Ambiguity between 'FakeCursor.Position' and 'FakeCursor.Position'
// // 
// //                 /// <summary>
// //                 /// Adapter to wrap FakePosition as the expected Position type.
// //                 /// </summary>
// //                 private class FakePositionAdapter : Position [Error] (110-53)CS0246 The type or namespace name 'Position' could not be found (are you missing a using directive or an assembly reference?)
// //                 {
// //                     private readonly FakePosition _fakePosition;
// //                     public FakePositionAdapter(FakePosition fakePosition)
// //                     {
// //                         _fakePosition = fakePosition;
// //                     }
// // 
// //                     public override int Offset => _fakePosition.Offset;
// //                 }
// //             }
// //         }
// //     }
// 
//     /// <summary>
//     /// Fake implementation of CompilationResult used for testing.
//     /// </summary>
// //     internal class FakeCompilationResult<T> : CompilationResult [Error] (127-20)CS0263 Partial declarations of 'FakeCompilationResult<T>' must not specify different base classes
// //     {
// //         public readonly List<Expression> _bodyExpressions = new List<Expression>();
// //         public override IList<Expression> Body => _bodyExpressions;
// //         public override ParameterExpression Value { get; set; }
// //         public override ParameterExpression Success { get; set; }
// //     }
// 
//     /// <summary>
//     /// Fake implementation of CompilationContext used for testing.
//     /// </summary>
// //     internal class FakeCompilationContext : CompilationContext [Error] (138-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationContext'
// //     {
// //         private int _nextNumber = 1;
// //         public override bool DiscardResult { get; set; }
// //         public override int NextNumber => _nextNumber++;
// // 
// //         public override CompilationResult CreateCompilationResult<T>() [Error] (146-24)CS0121 The call is ambiguous between the following methods or properties: 'FakeCompilationResult<T>.FakeCompilationResult()' and 'FakeCompilationResult<T>.FakeCompilationResult()'
// //         {
// //             return new FakeCompilationResult<T>();
// //         }
// // 
// //         public override ParameterExpression DeclarePositionVariable(CompilationResult result)
// //         {
// //             // For testing purposes, we simply return a parameter expression of type int.
// //             return Expression.Parameter(typeof(int), "start");
// //         }
// // 
// //         public override Expression Offset(ParameterExpression positionVariable) [Error] (155-36)CS0115 'FakeCompilationContext.Offset(ParameterExpression)': no suitable method found to override
// //         {
// //             // Return a constant expression to simulate obtaining the offset.
// //             return Expression.Constant(10);
// //         }
// // 
// //         public override Expression Buffer()
// //         {
// //             // Return a constant expression with a fake buffer.
// //             return Expression.Constant("fakebuffer");
// //         }
// // 
// //         public override Expression NewTextSpan(Expression buffer, Expression start, Expression length)
// //         {
// //             // Assume that TextSpan has a constructor (string, int, int).
// //             var ctor = typeof(TextSpan).GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });
// //             return Expression.New(ctor, buffer, start, length);
// //         }
// //     }
// 
//     /// <summary>
//     /// Fake implementation of a BuildResult used in compilation.
//     /// </summary>
//     internal class FakeBuildResult
//     {
//         public IList<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
//         public IList<Expression> Body { get; } = new List<Expression>
//         {
//             Expression.Constant(1)
//         };
//         public Expression Success { get; } = Expression.Constant(true);
//     }
// 
//     /// <summary>
//     /// Unit tests for the Capture<T> class.
//     /// </summary>
//     public class CaptureTests
//     {
//         private readonly string _testBuffer;
//         public CaptureTests()
//         {
//             _testBuffer = "test";
//         }
// 
//         /// <summary>
//         /// Tests that Parse returns true and sets the result correctly when the inner parser succeeds.
//         /// </summary>
// //         [Fact] [Error] (207-31)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<TextSpan>' [Error] (212-42)CS0121 The call is ambiguous between the following methods or properties: 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' [Error] (225-44)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.Parser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (227-40)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (227-57)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (230-40)CS0229 Ambiguity between 'ParseResult<TextSpan>.Value' and 'ParseResult<TextSpan>.Value' [Error] (232-51)CS0229 Ambiguity between 'ParseResult<TextSpan>.Value' and 'ParseResult<TextSpan>.Value' [Error] (234-41)CS0229 Ambiguity between 'ParseResult<TextSpan>.Value' and 'ParseResult<TextSpan>.Value' [Error] (235-41)CS0229 Ambiguity between 'ParseResult<TextSpan>.Value' and 'ParseResult<TextSpan>.Value'
// //         public void Parse_WhenInnerParserSucceeds_ReturnsTrueAndSetsResult()
// //         {
// //             // Arrange
// //             var fakeContext = new FakeParseContext(_testBuffer, 0);
// //             var parseResult = new ParseResult<TextSpan>();
// //             // Create a mock for the inner parser.
// //             var mockInnerParser = new Mock<Parser<int>>();
// //             // Setup the Parse method to simulate success.
// //             // When called, update the fake cursor's Offset to simulate advancement.
// //             mockInnerParser.Setup(p => p.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny)).Callback((ParseContext context, ref ParseResult<int> result) =>
// //             {
// //                 // Simulate advancing the cursor; assuming the adapter works with our FakeScanner.
// //                 // We need to cast the Scanner.Cursor to our adapter to update underlying FakeCursor.
// //                 if (context.Scanner is FakeParseContext.FakeScannerAdapter adapter)
// //                 {
// //                 // Update the underlying fake cursor.
// //                 // (In our adapter, the FakeCursorAdapter wraps the FakeCursor; update is done on the fake instance)
// //                 }
// // 
// //                 // Instead, update directly using our fake context.
// //                 fakeContext.FakeScanner.Cursor.Offset = 4;
// //             }).Returns(true);
// //             var capture = new Capture<int>(mockInnerParser.Object);
// //             // Act
// //             var result = capture.Parse(fakeContext, ref parseResult);
// //             // Assert
// //             Assert.True(result);
// //             Assert.NotNull(parseResult.Value);
// //             // Check that the TextSpan's buffer, offset and length are correctly assigned.
// //             Assert.Equal(_testBuffer, parseResult.Value.Buffer);
// //             // Start offset was 0 and end offset set to 4 by our callback; so length should be 4.
// //             Assert.Equal(0, parseResult.Value.Offset);
// //             Assert.Equal(4, parseResult.Value.Length);
// //         }
// 
//         /// <summary>
//         /// Tests that Parse returns false and does not set result when the inner parser fails.
//         /// </summary>
// //         [Fact] [Error] (246-31)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<TextSpan>' [Error] (249-42)CS0121 The call is ambiguous between the following methods or properties: 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' [Error] (250-44)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.Parser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (252-40)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (252-57)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (256-37)CS0229 Ambiguity between 'ParseResult<TextSpan>.Value' and 'ParseResult<TextSpan>.Value'
// //         public void Parse_WhenInnerParserFails_ReturnsFalse()
// //         {
// //             // Arrange
// //             var fakeContext = new FakeParseContext(_testBuffer, 0);
// //             var parseResult = new ParseResult<TextSpan>();
// //             // Create a mock for the inner parser that always fails.
// //             var mockInnerParser = new Mock<Parser<int>>();
// //             mockInnerParser.Setup(p => p.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny)).Returns(false);
// //             var capture = new Capture<int>(mockInnerParser.Object);
// //             // Act
// //             var result = capture.Parse(fakeContext, ref parseResult);
// //             // Assert
// //             Assert.False(result);
// //             // When parse fails, the value should remain null.
// //             Assert.Null(parseResult.Value);
// //         }
// 
//         /// <summary>
//         /// Tests that Compile returns a valid CompilationResult with an expected body when inner parser's Build is invoked.
//         /// </summary>
// //         [Fact] [Error] (270-44)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.Parser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (271-46)CS1729 'FakeCompilationContext' does not contain a constructor that takes 0 arguments [Error] (273-53)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
// //         public void Compile_WhenCalled_ReturnsCompilationResultWithBody()
// //         {
// //             // Arrange
// //             // Create a mock for the inner parser and setup the Build method.
// //             var mockInnerParser = new Mock<Parser<int>>();
// //             var fakeBuildResult = new FakeBuildResult();
// //             mockInnerParser.Setup(p => p.Build(It.IsAny<CompilationContext>())).Returns(fakeBuildResult);
// //             var capture = new Capture<int>(mockInnerParser.Object);
// //             var fakeCompilationContext = new FakeCompilationContext();
// //             // Act
// //             var compilationResult = capture.Compile(fakeCompilationContext);
// //             // Assert
// //             Assert.NotNull(compilationResult);
// //             // Verify that the compilation result body has been populated.
// //             Assert.NotEmpty(compilationResult.Body);
// //             // Additionally, check that the Success and Value properties have been assigned.
// //             Assert.NotNull(compilationResult.Success);
// //             Assert.NotNull(compilationResult.Value);
// //         }
// 
//         /// <summary>
//         /// Tests that ToString returns a string representation containing the indicator "(Capture)".
//         /// </summary>
// //         [Fact] [Error] (292-44)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.Parser<int>' to 'Parlot.Fluent.Parser<int>'
// //         public void ToString_ReturnsCorrectFormat()
// //         {
// //             // Arrange
// //             var mockInnerParser = new Mock<Parser<int>>();
// //             mockInnerParser.Setup(p => p.ToString()).Returns("InnerParser");
// //             var capture = new Capture<int>(mockInnerParser.Object);
// //             // Act
// //             var toStringResult = capture.ToString();
// //             // Assert
// //             Assert.Contains("(Capture)", toStringResult);
// //             Assert.Contains("InnerParser", toStringResult);
// //         }
//     }
// }
