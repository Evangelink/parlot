// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "If{C, S, T}"/> class focusing on the Parse method.
/// </summary>
// public class IfTests [Error] (335-2)CS1513 } expected
// {
//     /// <summary>
//     /// Tests that when the predicate returns false, the parser is not invoked and Parse returns false.
//     /// </summary>
//     [Fact] [Error] (23-30)CS0246 The type or namespace name 'FakeParser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (29-38)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (39-9)CS0200 Property or indexer 'Cursor.Position' cannot be assigned to -- it is read only [Error] (40-26)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (48-52)CS1061 'Cursor' does not contain a definition for 'ResetCount' and no accessible extension method 'ResetCount' accepting a first argument of type 'Cursor' could be found (are you missing a using directive or an assembly reference?) [Error] (50-37)CS1061 'IfTests.FakeParseContext' does not contain a definition for 'EnterCount' and no accessible extension method 'EnterCount' accepting a first argument of type 'IfTests.FakeParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (51-37)CS1061 'IfTests.FakeParseContext' does not contain a definition for 'ExitCount' and no accessible extension method 'ExitCount' accepting a first argument of type 'IfTests.FakeParseContext' could be found (are you missing a using directive or an assembly reference?)
//     public void Parse_PredicateFalse_DoesNotInvokeParserAndReturnsFalse()
//     {
//         // Arrange
//         // Create a fake parser that would set a flag if invoked.
//         var fakeParser = new FakeParser<string>
//         {
//             ParseReturnValue = true // value doesn't matter because predicate is false
//         };
//         bool predicateCalled = false;
//         // Predicate always returns false.
//         Func<FakeParseContext, object?, bool> predicate = (ctx, state) =>
//         {
//             predicateCalled = true;
//             return false;
//         };
//         // Create the instance of the If parser.
//         var ifParser = new If<FakeParseContext, object, string>(fakeParser, predicate, new object ());
//         // Create a fake parse context.
//         var fakeContext = new FakeParseContext();
//         // Set an initial cursor position.
//         fakeContext.Scanner.Cursor.Position = 42;
//         var result = new ParseResult<string>();
//         // Act
//         bool returned = ifParser.Parse(fakeContext, ref result);
//         // Assert
//         Assert.False(returned);
//         Assert.True(predicateCalled);
//         Assert.False(fakeParser.WasCalled);
//         // Since predicate is false, ResetPosition should not be called.
//         Assert.Equal(0, fakeContext.Scanner.Cursor.ResetCount);
//         // Ensure that EnterParser and ExitParser were both called.
//         Assert.Equal(1, fakeContext.EnterCount);
//         Assert.Equal(1, fakeContext.ExitCount);
//     }
// 
//     /// <summary>
//     /// Tests that when the predicate returns true and the underlying parser succeeds, Parse returns true without resetting the cursor.
//     /// </summary>
//     [Fact] [Error] (61-30)CS0246 The type or namespace name 'FakeParser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (66-38)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (70-9)CS0200 Property or indexer 'Cursor.Position' cannot be assigned to -- it is read only [Error] (72-26)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (79-52)CS1061 'Cursor' does not contain a definition for 'ResetCount' and no accessible extension method 'ResetCount' accepting a first argument of type 'Cursor' could be found (are you missing a using directive or an assembly reference?) [Error] (82-37)CS1061 'IfTests.FakeParseContext' does not contain a definition for 'EnterCount' and no accessible extension method 'EnterCount' accepting a first argument of type 'IfTests.FakeParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (83-37)CS1061 'IfTests.FakeParseContext' does not contain a definition for 'ExitCount' and no accessible extension method 'ExitCount' accepting a first argument of type 'IfTests.FakeParseContext' could be found (are you missing a using directive or an assembly reference?)
//     public void Parse_PredicateTrue_ParserReturnsTrue_ReturnsTrueWithoutReset()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<string>
//         {
//             ParseReturnValue = true
//         };
//         // Predicate always returns true.
//         Func<FakeParseContext, object?, bool> predicate = (ctx, state) => true;
//         var ifParser = new If<FakeParseContext, object, string>(fakeParser, predicate, new object ());
//         var fakeContext = new FakeParseContext();
//         // Set an initial cursor position.
//         fakeContext.Scanner.Cursor.Position = 100;
//         var initialPosition = fakeContext.Scanner.Cursor.Position;
//         var result = new ParseResult<string>();
//         // Act
//         bool returned = ifParser.Parse(fakeContext, ref result);
//         // Assert
//         Assert.True(returned);
//         Assert.True(fakeParser.WasCalled);
//         // Since underlying parser returns true, ResetPosition should not be called.
//         Assert.Equal(0, fakeContext.Scanner.Cursor.ResetCount);
//         // The cursor position remains unchanged.
//         Assert.Equal(initialPosition, fakeContext.Scanner.Cursor.Position);
//         Assert.Equal(1, fakeContext.EnterCount);
//         Assert.Equal(1, fakeContext.ExitCount);
//     }
// 
//     /// <summary>
//     /// Tests that when the predicate returns true and the underlying parser fails,
//     /// the cursor position is reset to its original state and Parse returns true.
//     /// </summary>
//     [Fact] [Error] (94-30)CS0246 The type or namespace name 'FakeParser<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (99-38)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (103-9)CS0200 Property or indexer 'Cursor.Position' cannot be assigned to -- it is read only [Error] (105-26)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (112-52)CS1061 'Cursor' does not contain a definition for 'ResetCount' and no accessible extension method 'ResetCount' accepting a first argument of type 'Cursor' could be found (are you missing a using directive or an assembly reference?) [Error] (113-66)CS1061 'Cursor' does not contain a definition for 'LastResetPosition' and no accessible extension method 'LastResetPosition' accepting a first argument of type 'Cursor' could be found (are you missing a using directive or an assembly reference?) [Error] (116-37)CS1061 'IfTests.FakeParseContext' does not contain a definition for 'EnterCount' and no accessible extension method 'EnterCount' accepting a first argument of type 'IfTests.FakeParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (117-37)CS1061 'IfTests.FakeParseContext' does not contain a definition for 'ExitCount' and no accessible extension method 'ExitCount' accepting a first argument of type 'IfTests.FakeParseContext' could be found (are you missing a using directive or an assembly reference?)
//     public void Parse_PredicateTrue_ParserReturnsFalse_CallsResetAndReturnsTrue()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<string>
//         {
//             ParseReturnValue = false
//         };
//         // Predicate always returns true.
//         Func<FakeParseContext, object?, bool> predicate = (ctx, state) => true;
//         var ifParser = new If<FakeParseContext, object, string>(fakeParser, predicate, new object ());
//         var fakeContext = new FakeParseContext();
//         // Set an initial cursor position.
//         fakeContext.Scanner.Cursor.Position = 250;
//         var initialPosition = fakeContext.Scanner.Cursor.Position;
//         var result = new ParseResult<string>();
//         // Act
//         bool returned = ifParser.Parse(fakeContext, ref result);
//         // Assert
//         Assert.True(returned);
//         Assert.True(fakeParser.WasCalled);
//         // Since underlying parser returns false, ResetPosition should be called exactly once.
//         Assert.Equal(1, fakeContext.Scanner.Cursor.ResetCount);
//         Assert.Equal(initialPosition, fakeContext.Scanner.Cursor.LastResetPosition);
//         // The cursor position is reset to initial value.
//         Assert.Equal(initialPosition, fakeContext.Scanner.Cursor.Position);
//         Assert.Equal(1, fakeContext.EnterCount);
//         Assert.Equal(1, fakeContext.ExitCount);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method returns a non-null CompilationResult with a non-empty Body when the CompilationContext.DiscardResult is false.
//     /// </summary>
//     [Fact] [Error] (127-42)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (131-30)CS0246 The type or namespace name 'FakeParser' could not be found (are you missing a using directive or an assembly reference?) [Error] (133-44)CS0246 The type or namespace name 'FakeParserCompileResult' could not be found (are you missing a using directive or an assembly reference?) [Error] (135-14)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (138-31)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (138-74)CS1503 Argument 2: cannot convert from 'System.Func<TestParseContext, int?, bool>' to 'System.Func<TestParseContext, int, bool>'
//     public void Compile_ValidContextAndDiscardResultFalse_ReturnsCompilationResultWithNonEmptyBody()
//     {
//         // Arrange
//         var fakeCompilationContext = new FakeCompilationContext
//         {
//             DiscardResult = false
//         };
//         var fakeParser = new FakeParser();
//         // Set the fake parser compile result (dummy implementation)
//         fakeParser.FakeCompileResult = new FakeParserCompileResult();
//         // Use a predicate that always returns true.
//         Func<TestParseContext, int?, bool> predicate = (ctx, state) => true;
//         int stateValue = 42;
//         // Create an instance of If<TestParseContext, int, string> using the fake parser.
//         var ifParser = new If<TestParseContext, int, string>(fakeParser, predicate, stateValue);
//         // Act
//         var result = ifParser.Compile(fakeCompilationContext);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotEmpty(result.Body);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method returns a non-null CompilationResult with a non-empty Body when the CompilationContext.DiscardResult is true.
//     /// </summary>
//     [Fact] [Error] (153-42)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (157-30)CS0246 The type or namespace name 'FakeParser' could not be found (are you missing a using directive or an assembly reference?) [Error] (158-44)CS0246 The type or namespace name 'FakeParserCompileResult' could not be found (are you missing a using directive or an assembly reference?) [Error] (160-14)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (163-31)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (163-74)CS1503 Argument 2: cannot convert from 'System.Func<TestParseContext, int?, bool>' to 'System.Func<TestParseContext, int, bool>'
//     public void Compile_ValidContextAndDiscardResultTrue_ReturnsCompilationResultWithNonEmptyBody()
//     {
//         // Arrange
//         var fakeCompilationContext = new FakeCompilationContext
//         {
//             DiscardResult = true
//         };
//         var fakeParser = new FakeParser();
//         fakeParser.FakeCompileResult = new FakeParserCompileResult();
//         // Use a predicate that always returns true.
//         Func<TestParseContext, int?, bool> predicate = (ctx, state) => true;
//         int stateValue = 100;
//         // Create an instance of If<TestParseContext, int, string> using the fake parser.
//         var ifParser = new If<TestParseContext, int, string>(fakeParser, predicate, stateValue);
//         // Act
//         var result = ifParser.Compile(fakeCompilationContext);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotEmpty(result.Body);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method throws a NullReferenceException when a null CompilationContext is passed.
//     /// </summary>
//     [Fact] [Error] (178-30)CS0246 The type or namespace name 'FakeParser' could not be found (are you missing a using directive or an assembly reference?) [Error] (179-44)CS0246 The type or namespace name 'FakeParserCompileResult' could not be found (are you missing a using directive or an assembly reference?) [Error] (180-14)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (181-31)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (181-74)CS1503 Argument 2: cannot convert from 'System.Func<TestParseContext, int?, bool>' to 'System.Func<TestParseContext, int, bool>'
//     public void Compile_NullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var fakeParser = new FakeParser();
//         fakeParser.FakeCompileResult = new FakeParserCompileResult();
//         Func<TestParseContext, int?, bool> predicate = (ctx, state) => true;
//         var ifParser = new If<TestParseContext, int, string>(fakeParser, predicate, 0);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => ifParser.Compile(null));
//     }
// 
//     /// <summary>
//     /// Dummy implementation of ParseContext for testing purposes.
//     /// </summary>
//     private class DummyParseContext : ParseContext [Error] (189-19)CS7036 There is no argument given that corresponds to the required parameter 'scanner' of 'ParseContext.ParseContext(Scanner, bool)'
//     {
//     // No additional implementation required for ToString tests.
//     }
// 
//     /// <summary>
//     /// Dummy parser that returns a fixed string value from its ToString method.
//     /// </summary>
//     private class DummyParser : Parser<string> [Error] (197-19)CS0263 Partial declarations of 'IfTests.DummyParser' must not specify different base classes
//     {
//         private readonly string _toStringValue;
//         public DummyParser(string toStringValue)
//         {
//             _toStringValue = toStringValue;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<string> result) [Error] (205-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => _toStringValue;
//     }
// 
//     /// <summary>
//     /// Dummy parser that returns null from its ToString method.
//     /// </summary>
//     private class NullParser : Parser<string> [Error] (216-19)CS0534 'IfTests.NullParser' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<string> result) [Error] (218-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => null;
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns the underlying parser's ToString value concatenated with " (If)".
//     /// </summary>
//     [Fact] [Error] (237-66)CS1503 Argument 1: cannot convert from '.IfTests.DummyParser' to 'Parlot.Fluent.Parser<string>'
//     public void ToString_WithValidParser_ReturnsParserToStringPlusIfSuffix()
//     {
//         // Arrange
//         string parserString = "DummyParserValue";
//         var dummyParser = new DummyParser(parserString);
//         Func<DummyParseContext, object, bool> dummyPredicate = (ctx, state) => true;
//         object state = null;
//         var ifParser = new If<DummyParseContext, object, string>(dummyParser, dummyPredicate, state);
//         // Act
//         string result = ifParser.ToString();
//         // Assert
//         string expected = $"{parserString} (If)";
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests that ToString handles a null underlying parser ToString value gracefully.
//     /// </summary>
//     [Fact]
//     public void ToString_WhenUnderlyingParserToStringReturnsNull_ReturnsExpectedSuffixOnly()
//     {
//         // Arrange
//         var nullParser = new NullParser();
//         Func<DummyParseContext, object, bool> dummyPredicate = (ctx, state) => true;
//         object state = null;
//         var ifParser = new If<DummyParseContext, object, string>(nullParser, dummyPredicate, state);
//         // Act
//         string result = ifParser.ToString();
//         // Assert
//         // When the underlying parser's ToString returns null, string interpolation yields an empty string.
//         string expected = " (If)";
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Dummy implementation of <see cref = "Parser{T}"/> for testing purposes.
//     /// </summary>
//     private class DummyParser : Parser<int> [Error] (267-19)CS0102 The type 'IfTests' already contains a definition for 'DummyParser'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (269-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => "DummyParser"; [Error] (274-32)CS0111 Type 'IfTests.DummyParser' already defines a member called 'ToString' with the same parameter types
//         public CompilationResult Compile(CompilationContext context)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// Dummy implementation of <see cref = "ParseContext"/> for testing purposes.
//     /// </summary>
//     private class FakeParseContext : ParseContext [Error] (284-19)CS7036 There is no argument given that corresponds to the required parameter 'scanner' of 'ParseContext.ParseContext(Scanner, bool)'
//     {
//     // Minimal dummy implementation for testing purposes.
//     }
// 
//     /// <summary>
//     /// Tests that the constructor of <see cref = "If{C, S, T}"/> creates an instance when provided with valid non-null parameters.
//     /// </summary>
//     [Fact] [Error] (296-31)CS7036 There is no argument given that corresponds to the required parameter 'toStringValue' of 'IfTests.DummyParser.DummyParser(string)' [Error] (300-59)CS1503 Argument 1: cannot convert from '.IfTests.DummyParser' to 'Parlot.Fluent.Parser<int>' [Error] (300-72)CS1503 Argument 2: cannot convert from 'System.Func<.IfTests.FakeParseContext, int?, bool>' to 'System.Func<.IfTests.FakeParseContext, int, bool>'
//     public void IfConstructor_WithValidParameters_ShouldCreateInstance()
//     {
//         // Arrange
//         var dummyParser = new DummyParser();
//         Func<FakeParseContext, int?, bool> predicate = (context, state) => true;
//         int state = 42;
//         // Act
//         var instance = new If<FakeParseContext, int, int>(dummyParser, predicate, state);
//         // Assert
//         Assert.NotNull(instance);
//         Assert.Contains("(If)", instance.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the constructor of <see cref = "If{C, S, T}"/> throws an <see cref = "ArgumentNullException"/> when a null predicate is provided.
//     /// </summary>
//     [Fact] [Error] (313-31)CS7036 There is no argument given that corresponds to the required parameter 'toStringValue' of 'IfTests.DummyParser.DummyParser(string)' [Error] (317-103)CS1503 Argument 1: cannot convert from '.IfTests.DummyParser' to 'Parlot.Fluent.Parser<int>' [Error] (317-116)CS1503 Argument 2: cannot convert from 'System.Func<.IfTests.FakeParseContext, int?, bool>' to 'System.Func<.IfTests.FakeParseContext, int, bool>'
//     public void IfConstructor_WithNullPredicate_ShouldThrowArgumentNullException()
//     {
//         // Arrange
//         var dummyParser = new DummyParser();
//         int state = 42;
//         Func<FakeParseContext, int?, bool> nullPredicate = null;
//         // Act & Assert
//         var exception = Assert.Throws<ArgumentNullException>(() => new If<FakeParseContext, int, int>(dummyParser, nullPredicate, state));
//         Assert.Equal("predicate", exception.ParamName);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor of <see cref = "If{C, S, T}"/> throws an <see cref = "ArgumentNullException"/> when a null parser is provided.
//     /// </summary>
//     [Fact] [Error] (332-103)CS1503 Argument 1: cannot convert from '.IfTests.DummyParser' to 'Parlot.Fluent.Parser<int>' [Error] (332-115)CS1503 Argument 2: cannot convert from 'System.Func<.IfTests.FakeParseContext, int?, bool>' to 'System.Func<.IfTests.FakeParseContext, int, bool>'
//     public void IfConstructor_WithNullParser_ShouldThrowArgumentNullException()
//     {
//         // Arrange
//         Func<FakeParseContext, int?, bool> predicate = (context, state) => true;
//         int state = 42;
//         DummyParser nullParser = null;
//         // Act & Assert
//         var exception = Assert.Throws<ArgumentNullException>(() => new If<FakeParseContext, int, int>(nullParser, predicate, state));
//         Assert.Equal("parser", exception.ParamName);
//     }
// }