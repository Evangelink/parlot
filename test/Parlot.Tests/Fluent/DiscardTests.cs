// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Discard{T, U}"/> class.
/// </summary>
// public class DiscardTests [Error] (248-2)CS1513 } expected
// {
//     /// <summary>
//     /// Tests that the Parse method returns true and sets the result with the provided discard value when the inner parser succeeds.
//     /// Arrange: A FakeParser is configured to succeed and set specific start and end positions.
//     /// Act: The Discard parser's Parse method is invoked.
//     /// Assert: The method returns true, the result is set with the discard value provided to the constructor, and the start and end positions match those set by the inner parser.
//     /// </summary>
//     [Fact] [Error] (28-51)CS1739 The best overload for 'FakeParser' does not have a parameter named 'shouldSucceed' [Error] (31-31)CS0246 The type or namespace name 'FakeParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (32-26)CS0246 The type or namespace name 'FakeParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenInnerParserSucceeds_ReturnsTrueAndSetsDiscardValue()
//     {
//         // Arrange
//         int expectedInnerStart = 5;
//         int expectedInnerEnd = 10;
//         // The inner parser returns an int value but Discard disregards it.
//         var fakeInnerParser = new FakeParser<int>(shouldSucceed: true, start: expectedInnerStart, end: expectedInnerEnd, value: 42);
//         string discardValue = "discarded";
//         var parserUnderTest = new Discard<int, string>(fakeInnerParser, discardValue);
//         var fakeContext = new FakeParseContext();
//         var result = new FakeParseResult<string>();
//         // Act
//         bool parseResult = parserUnderTest.Parse(fakeContext, ref result);
//         // Assert
//         Assert.True(parseResult);
//         Assert.Equal(expectedInnerStart, result.Start);
//         Assert.Equal(expectedInnerEnd, result.End);
//         Assert.Equal(discardValue, result.Value);
//         // Verify that the context recorded the Enter and Exit calls appropriately.
//         Assert.Equal(2, fakeContext.Logs.Count);
//         Assert.StartsWith("Enter:", fakeContext.Logs[0]);
//         Assert.StartsWith("Exit:", fakeContext.Logs[1]);
//     }
// 
//     /// <summary>
//     /// Tests that the Parse method returns false and does not modify the result when the inner parser fails.
//     /// Arrange: A FakeParser is configured to fail.
//     /// Act: The Discard parser's Parse method is invoked.
//     /// Assert: The method returns false and the result remains unchanged.
//     /// </summary>
//     [Fact] [Error] (56-51)CS1739 The best overload for 'FakeParser' does not have a parameter named 'shouldSucceed' [Error] (59-31)CS0246 The type or namespace name 'FakeParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (61-26)CS0246 The type or namespace name 'FakeParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenInnerParserFails_ReturnsFalse()
//     {
//         // Arrange
//         var fakeInnerParser = new FakeParser<int>(shouldSucceed: false, start: 0, end: 0, value: 0);
//         string discardValue = "discarded";
//         var parserUnderTest = new Discard<int, string>(fakeInnerParser, discardValue);
//         var fakeContext = new FakeParseContext();
//         // Initialize the result with sentinel values.
//         var result = new FakeParseResult<string>
//         {
//             Start = -1,
//             End = -1,
//             Value = null
//         };
//         // Act
//         bool parseOutcome = parserUnderTest.Parse(fakeContext, ref result);
//         // Assert
//         Assert.False(parseOutcome);
//         // The result should remain unchanged (not updated by Set) when inner parser fails.
//         Assert.Equal(-1, result.Start);
//         Assert.Equal(-1, result.End);
//         Assert.Null(result.Value);
//         // Verify that the context recorded the Enter and Exit calls.
//         Assert.Equal(2, fakeContext.Logs.Count);
//         Assert.StartsWith("Enter:", fakeContext.Logs[0]);
//         Assert.StartsWith("Exit:", fakeContext.Logs[1]);
//     }
// 
//     /// <summary>
//     /// Tests that the Parse method throws a NullReferenceException when a null context is passed.
//     /// Arrange: A valid FakeParser and a valid result are created.
//     /// Act & Assert: Passing a null context should throw a NullReferenceException.
//     /// </summary>
//     [Fact] [Error] (90-51)CS1739 The best overload for 'FakeParser' does not have a parameter named 'shouldSucceed' [Error] (93-26)CS0246 The type or namespace name 'FakeParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (95-9)CS0619 'Assert.Throws<T>(Func<Task>)' is obsolete: 'You must call Assert.ThrowsAsync<T> (and await the result) when testing async code.'
//     public void Parse_NullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var fakeInnerParser = new FakeParser<int>(shouldSucceed: true, start: 0, end: 0, value: 42);
//         string discardValue = "discarded";
//         var parserUnderTest = new Discard<int, string>(fakeInnerParser, discardValue);
//         var result = new FakeParseResult<string>();
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => parserUnderTest.Parse(null, ref result));
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method correctly builds the compilation result by incorporating the parser's build result.
//     /// The test verifies that the generated block expression contains the assignment from the fake parser's success expression to the result's success expression.
//     /// </summary>
//     [Fact] [Error] (107-36)CS0246 The type or namespace name 'FakeCompilationResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (120-30)CS0305 Using the generic type 'DiscardTests.FakeParser<T>' requires 1 type arguments [Error] (123-31)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?)
//     public void Compile_HappyPath_ReturnsExpectedCompilationResult()
//     {
//         // Arrange
//         // Create a fake parser compilation result (for T = int).
//         var fakeParserResult = new FakeCompilationResult<int>
//         {
//             Variables =
//             {
//                 Expression.Parameter(typeof(int), "v")
//             },
//             Body =
//             {
//                 Expression.Constant(42)
//             },
//             Success = Expression.Constant(false)
//         };
//         // Use the fake parser that returns the fakeParserResult.
//         var fakeParser = new FakeParser(fakeParserResult);
//         string expectedValue = "TestValue";
//         var discard = new Discard<int, string>(fakeParser, expectedValue);
//         var fakeContext = new FakeCompilationContext();
//         // Act
//         var result = discard.Compile(fakeContext);
//         // Assert
//         Assert.NotNull(result);
//         // Ensure that one expression was added to the body.
//         Assert.Single(result.Body);
//         // Verify the added expression is a BlockExpression.
//         var blockExpr = Assert.IsType<BlockExpression>(result.Body[0]);
//         Assert.NotEmpty(blockExpr.Expressions);
//         // The last expression in the block should be an assignment of result.Success from fakeParserResult.Success.
//         var lastExpr = blockExpr.Expressions[blockExpr.Expressions.Count - 1] as BinaryExpression;
//         Assert.NotNull(lastExpr);
//         Assert.Equal(result.Success, lastExpr.Left);
//         Assert.Equal(fakeParserResult.Success, lastExpr.Right);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method throws a NullReferenceException when a null CompilationContext is provided.
//     /// This verifies that the method does not handle a null context.
//     /// </summary>
//     [Fact] [Error] (148-36)CS0246 The type or namespace name 'FakeCompilationResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (160-30)CS0305 Using the generic type 'DiscardTests.FakeParser<T>' requires 1 type arguments
//     public void Compile_NullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var fakeParserResult = new FakeCompilationResult<int>
//         {
//             Variables =
//             {
//                 Expression.Parameter(typeof(int), "v")
//             },
//             Body =
//             {
//                 Expression.Constant(42)
//             },
//             Success = Expression.Constant(true)
//         };
//         var fakeParser = new FakeParser(fakeParserResult);
//         var discard = new Discard<int, string>(fakeParser, "TestValue");
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => discard.Compile(null));
//     }
// 
//     /// <summary>
//     /// A fake parser used for testing that returns a predetermined string from its ToString method.
//     /// </summary>
//     private class FakeParser<T> : Parser<T> [Error] (169-19)CS0534 'DiscardTests.FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//         private readonly string _representation;
//         public FakeParser(string representation)
//         {
//             _representation = representation;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (177-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => _representation;
//     }
// 
//     /// <summary>
//     /// Tests the ToString method when the underlying parser's ToString returns a normal non-empty string.
//     /// Expected outcome is that the returned string concatenates the parser's string with " (Discard)".
//     /// </summary>
//     [Fact]
//     public void ToString_WhenParserToStringReturnsNormalString_ReturnsExpectedConcatenatedString()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<string>("FakeParser");
//         var discard = new Discard<string, int>(fakeParser, 123);
//         var expected = "FakeParser (Discard)";
//         // Act
//         var actual = discard.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests the ToString method when the underlying parser's ToString returns an empty string.
//     /// Expected outcome is that the returned string concatenates an empty string with " (Discard)".
//     /// </summary>
//     [Fact]
//     public void ToString_WhenParserToStringReturnsEmpty_ReturnsExpectedConcatenatedString()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<string>(string.Empty);
//         var discard = new Discard<string, int>(fakeParser, 0);
//         var expected = " (Discard)";
//         // Act
//         var actual = discard.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests the ToString method when the underlying parser is null.
//     /// Expected outcome is a NullReferenceException when attempting to call ToString on a null parser.
//     /// </summary>
//     [Fact]
//     public void ToString_WhenParserIsNull_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var discard = new Discard<string, int>(null, 0);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => discard.ToString());
//     }
// 
//     /// <summary>
//     /// A simple override to simulate parsing behavior. Not used in constructor tests.
//     /// </summary>
//     public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (235-58)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (235-70)CS0246 The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         throw new NotImplementedException("FakeParser does not support Parse.");
//     }
// 
//     /// <summary>
//     /// Returns a fixed string to allow identification during tests.
//     /// </summary>
//     /// <returns>A fixed string identifier for FakeParser.</returns>
//     public override string ToString()
//     {
//         return "FakeParser";
//     }
// }