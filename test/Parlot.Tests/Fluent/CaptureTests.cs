// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Capture{T}"/> class, specifically for the Parse method.
/// </summary>
// public class CaptureTests [Error] (251-2)CS1513 } expected
// {
//     /// <summary>
//     /// Tests that the Parse method returns true and correctly sets the result when the underlying parser succeeds.
//     /// The test simulates a successful parse by advancing the cursor and verifies that the TextSpan in the result
//     /// contains the expected buffer, start offset, and length.
//     /// </summary>
//     [Fact] [Error] (28-30)CS0308 The non-generic type 'CaptureTests.FakeParser' cannot be used with type arguments [Error] (33-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?) [Error] (37-31)CS0246 The type or namespace name 'FakeScanner' could not be found (are you missing a using directive or an assembly reference?) [Error] (42-31)CS0246 The type or namespace name 'FakeParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (58-50)CS1061 'TextSpan' does not contain a definition for 'Start' and no accessible extension method 'Start' accepting a first argument of type 'TextSpan' could be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenUnderlyingParserSucceeds_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         // Create a fake underlying parser that always succeeds.
//         // It will advance the cursor offset by a predetermined amount.
//         const int advanceBy = 5;
//         var fakeParser = new FakeParser<int>(shouldSucceed: true, result: 123, advance: advanceBy);
//         var capture = new Capture<int>(fakeParser);
//         // Setup fake parse context with a scanner having an initial offset.
//         var initialOffset = 0;
//         var bufferContent = "Hello World";
//         var fakeCursor = new FakeCursor
//         {
//             Offset = initialOffset
//         };
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = bufferContent,
//             Cursor = fakeCursor
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         // Prepare an empty ParseResult for TextSpan.
//         var result = new ParseResult<TextSpan>();
//         // Act
//         bool parseResult = capture.Parse(fakeContext, ref result);
//         // Assert
//         Assert.True(parseResult);
//         // The start offset should be the initial offset and the end offset should be advanced.
//         Assert.Equal(initialOffset, result.Start);
//         Assert.Equal(initialOffset + advanceBy, result.End);
//         // The length of the TextSpan should be equal to the amount advanced.
//         Assert.NotNull(result.Value);
//         Assert.Equal(bufferContent, result.Value.Buffer);
//         Assert.Equal(initialOffset, result.Value.Start);
//         Assert.Equal(advanceBy, result.Value.Length);
//         // Verify that the context logged the entering and exiting of the parser.
//         Assert.Equal(2, fakeContext.Log.Count);
//         Assert.Equal("Enter:Capture`1", fakeContext.Log[0]);
//         Assert.Equal("Exit:Capture`1", fakeContext.Log[1]);
//     }
// 
//     /// <summary>
//     /// Tests that the Parse method returns false and does not modify the result when the underlying parser fails.
//     /// The test simulates a failed parse by having the underlying FakeParser return false and verifies that the result's Value remains null.
//     /// </summary>
//     [Fact] [Error] (75-30)CS0308 The non-generic type 'CaptureTests.FakeParser' cannot be used with type arguments [Error] (80-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?) [Error] (84-31)CS0246 The type or namespace name 'FakeScanner' could not be found (are you missing a using directive or an assembly reference?) [Error] (89-31)CS0246 The type or namespace name 'FakeParseContext' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenUnderlyingParserFails_ReturnsFalseAndDoesNotSetResult()
//     {
//         // Arrange
//         // Create a fake underlying parser that fails.
//         var fakeParser = new FakeParser<int>(shouldSucceed: false, result: default, advance: 0);
//         var capture = new Capture<int>(fakeParser);
//         // Setup fake parse context with a scanner.
//         var initialOffset = 10;
//         var bufferContent = "Sample Buffer";
//         var fakeCursor = new FakeCursor
//         {
//             Offset = initialOffset
//         };
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = bufferContent,
//             Cursor = fakeCursor
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         // Prepare an empty ParseResult for TextSpan.
//         var result = new ParseResult<TextSpan>();
//         // Act
//         bool parseResult = capture.Parse(fakeContext, ref result);
//         // Assert
//         Assert.False(parseResult);
//         // Since parsing failed, the result should not have been set.
//         Assert.Equal(0, result.Start);
//         Assert.Equal(0, result.End);
//         Assert.Null(result.Value);
//         // Verify that the context logged the entering and exiting of the parser.
//         Assert.Equal(2, fakeContext.Log.Count);
//         Assert.Equal("Enter:Capture`1", fakeContext.Log[0]);
//         Assert.Equal("Exit:Capture`1", fakeContext.Log[1]);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method builds a compilation result containing an if-then block when the underlying parser succeeds.
//     /// </summary>
//     [Fact] [Error] (116-31)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (121-30)CS0308 The non-generic type 'CaptureTests.FakeParser' cannot be used with type arguments
//     public void Compile_WhenParserSucceeds_BuildsCompilationResultWithIfThenExpression()
//     {
//         // Arrange
//         var fakeContext = new FakeCompilationContext
//         {
//             NextNumber = 1,
//             DiscardResult = false
//         };
//         var fakeParser = new FakeParser<object>(true);
//         var capture = new Capture<object>(fakeParser);
//         // Act
//         var result = capture.Compile(fakeContext);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotEmpty(result.Body);
//         // Verify that the generated expression block contains a conditional (if-then) expression.
//         bool containsIfThen = false;
//         foreach (var expr in result.Body)
//         {
//             if (expr is BlockExpression block)
//             {
//                 foreach (var inner in block.Expressions)
//                 {
//                     if (inner is ConditionalExpression)
//                     {
//                         containsIfThen = true;
//                         break;
//                     }
//                 }
//             }
//         }
// 
//         Assert.True(containsIfThen, "The compiled expression body should contain an if-then condition based on parser success.");
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method builds a compilation result containing an if-then block when the underlying parser fails.
//     /// </summary>
//     [Fact] [Error] (155-31)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (160-30)CS0308 The non-generic type 'CaptureTests.FakeParser' cannot be used with type arguments
//     public void Compile_WhenParserFails_BuildsCompilationResultWithIfThenExpressionReflectingFailure()
//     {
//         // Arrange
//         var fakeContext = new FakeCompilationContext
//         {
//             NextNumber = 1,
//             DiscardResult = false
//         };
//         var fakeParser = new FakeParser<object>(false);
//         var capture = new Capture<object>(fakeParser);
//         // Act
//         var result = capture.Compile(fakeContext);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotEmpty(result.Body);
//         // Even when parser fails, the if-then block is built; check that the condition is a constant false.
//         bool foundFalseCondition = false;
//         foreach (var expr in result.Body)
//         {
//             if (expr is BlockExpression block)
//             {
//                 foreach (var inner in block.Expressions)
//                 {
//                     if (inner is ConditionalExpression condExpr && condExpr.Test is ConstantExpression constExpr && constExpr.Value is bool b && b == false)
//                     {
//                         foundFalseCondition = true;
//                         break;
//                     }
//                 }
//             }
//         }
// 
//         Assert.True(foundFalseCondition, "The compiled expression body should contain an if-then block with a false condition when the parser fails.");
//     }
// 
//     /// <summary>
//     /// Not implemented for testing.
//     /// </summary>
//     /// <param name = "context">The parse context.</param>
//     /// <param name = "result">The parse result.</param>
//     /// <returns>Throws NotImplementedException.</returns>
//     public override bool Parse(ParseContext context, ref ParseResult<TextSpan> result) => throw new NotImplementedException(); [Error] (193-26)CS0115 'CaptureTests.Parse(ParseContext, ref ParseResult<TextSpan>)': no suitable method found to override
//     /// <summary>
//     /// Returns a fixed string representation.
//     /// </summary>
//     /// <returns>The string "DummyParser".</returns>
//     public override string ToString() => "DummyParser";
//     /// <summary>
//     /// A fake parser used for testing purposes. Inherits from <see cref = "Parser{T}"/> and only overrides the minimal members.
//     /// </summary>
//     private class FakeParser : Parser<int>
//     {
//         /// <summary>
//         /// An override of the Parse method that is not used in these tests.
//         /// </summary>
//         /// <param name = "context">The parse context (unused).</param>
//         /// <param name = "result">The parse result (unused).</param>
//         /// <returns>Always throws <see cref = "NotImplementedException"/>.</returns>
//         public override bool Parse(ParseContext context, ref ParseResult<int> result)
//         {
//             throw new NotImplementedException();
//         }
// 
//         /// <summary>
//         /// Returns a preset string used to verify the composition of <see cref = "Capture{T}"/>.
//         /// </summary>
//         /// <returns>The string "FakeParser".</returns>
//         public override string ToString() => "FakeParser";
//     }
// 
//     /// <summary>
//     /// Tests that the Capture constructor, when provided with a valid parser,
//     /// initializes the instance such that its ToString method returns the expected output.
//     /// </summary>
//     [Fact]
//     public void CaptureConstructor_WithValidParser_ReturnsInstanceWithExpectedToString()
//     {
//         // Arrange: Create a fake parser that returns "FakeParser" when ToString is called.
//         var fakeParser = new FakeParser();
//         // Act: Create a Capture instance using the fake parser and retrieve its string representation.
//         var capture = new Capture<int>(fakeParser);
//         var result = capture.ToString();
//         // Assert: The expected output is the fake parser's string followed by " (Capture)".
//         Assert.Equal("FakeParser (Capture)", result);
//     }
// 
//     /// <summary>
//     /// Tests that the Capture constructor accepts a null parser and that calling ToString on the resulting
//     /// instance properly handles the null value by outputting " (Capture)".
//     /// </summary>
//     [Fact]
//     public void CaptureConstructor_WithNullParser_AllowsNullAndToStringReturnsExpected()
//     {
//         // Arrange & Act: Create a Capture instance with a null parser and call ToString.
//         var capture = new Capture<int>(null);
//         var result = capture.ToString();
//         // Assert: When a null parser is provided, the expected ToString output is " (Capture)".
//         Assert.Equal(" (Capture)", result);
//     }
// }