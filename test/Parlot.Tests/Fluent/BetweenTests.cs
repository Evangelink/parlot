// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Moq;
// using Parlot.Compilation;
// using Parlot.Fluent;
// using Parlot.Rewriting;
// using Xunit;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the <see cref="Between{A, T, B}"/> class.
//     /// </summary>
//     public class BetweenTests
//     {
//         private readonly string _fakeBeforeText = "FakeBefore";
//         private readonly string _fakeParserText = "FakeParser";
//         private readonly string _fakeAfterText = "FakeAfter";
// 
//         #region Constructor Tests
// 
//         /// <summary>
//         /// Tests that the constructor throws an ArgumentNullException when the "before" parser is null.
//         /// </summary>
// //         [Fact] [Error] (34-109)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (34-122)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>'
// //         public void Ctor_NullBefore_ThrowsArgumentNullException()
// //         {
// //             // Arrange
// //             var validParser = new FakeParser<int>(_fakeParserText) { ShouldSucceed = true, ReturnValue = 42 };
// //             var validAfter = new FakeParser<char>(_fakeAfterText) { ShouldSucceed = true };
// // 
// //             // Act & Assert
// //             var exception = Assert.Throws<ArgumentNullException>(() => new Between<string, int, char>(null, validParser, validAfter));
// //             Assert.Equal("before", exception.ParamName);
// //         }
// 
//         /// <summary>
//         /// Tests that the constructor throws an ArgumentNullException when the "parser" is null.
//         /// </summary>
// //         [Fact] [Error] (49-103)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (49-122)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>'
// //         public void Ctor_NullParser_ThrowsArgumentNullException()
// //         {
// //             // Arrange
// //             var validBefore = new FakeParser<string>(_fakeBeforeText) { ShouldSucceed = true, ReturnValue = "start" };
// //             var validAfter = new FakeParser<char>(_fakeAfterText) { ShouldSucceed = true };
// // 
// //             // Act & Assert
// //             var exception = Assert.Throws<ArgumentNullException>(() => new Between<string, int, char>(validBefore, null, validAfter));
// //             Assert.Equal("parser", exception.ParamName);
// //         }
// 
//         /// <summary>
//         /// Tests that the constructor throws an ArgumentNullException when the "after" parser is null.
//         /// </summary>
// //         [Fact] [Error] (64-103)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (64-116)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>'
// //         public void Ctor_NullAfter_ThrowsArgumentNullException()
// //         {
// //             // Arrange
// //             var validBefore = new FakeParser<string>(_fakeBeforeText) { ShouldSucceed = true, ReturnValue = "start" };
// //             var validParser = new FakeParser<int>(_fakeParserText) { ShouldSucceed = true, ReturnValue = 42 };
// // 
// //             // Act & Assert
// //             var exception = Assert.Throws<ArgumentNullException>(() => new Between<string, int, char>(validBefore, validParser, null));
// //             Assert.Equal("after", exception.ParamName);
// //         }
//         #endregion
// 
//         #region Parse Method Tests
// 
//         /// <summary>
//         /// Tests that the Parse method returns true when before, parser, and after all succeed, and that the result is set accordingly.
//         /// </summary>
// //         [Fact] [Error] (82-58)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (82-66)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (82-74)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>' [Error] (84-26)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<int>' [Error] (87-46)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (87-59)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>' [Error] (91-32)CS0229 Ambiguity between 'ParseResult<int>.Success' and 'ParseResult<int>.Success' [Error] (92-38)CS0229 Ambiguity between 'ParseResult<int>.Value' and 'ParseResult<int>.Value'
// //         public void Parse_AllParsersSucceed_ReturnsTrueAndSetsResultValue()
// //         {
// //             // Arrange
// //             var before = new FakeParser<string>(_fakeBeforeText) { ShouldSucceed = true, ReturnValue = "beforeValue" };
// //             var parser = new FakeParser<int>(_fakeParserText) { ShouldSucceed = true, ReturnValue = 100 };
// //             var after = new FakeParser<char>(_fakeAfterText) { ShouldSucceed = true };
// // 
// //             var between = new Between<string, int, char>(before, parser, after);
// //             var context = new FakeParseContext();
// //             var result = new ParseResult<int>();
// // 
// //             // Act
// //             bool parseResult = between.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.True(parseResult);
// //             Assert.True(result.Success);
// //             Assert.Equal(100, result.Value);
// //         }
// 
//         /// <summary>
//         /// Tests that the Parse method returns false when the "before" parser fails.
//         /// </summary>
// //         [Fact] [Error] (106-58)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (106-66)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (106-74)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>' [Error] (108-26)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<int>' [Error] (111-46)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (111-59)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>' [Error] (115-33)CS0229 Ambiguity between 'ParseResult<int>.Success' and 'ParseResult<int>.Success'
// //         public void Parse_BeforeParserFails_ReturnsFalse()
// //         {
// //             // Arrange
// //             var before = new FakeParser<string>(_fakeBeforeText) { ShouldSucceed = false };
// //             var parser = new FakeParser<int>(_fakeParserText) { ShouldSucceed = true, ReturnValue = 100 };
// //             var after = new FakeParser<char>(_fakeAfterText) { ShouldSucceed = true };
// // 
// //             var between = new Between<string, int, char>(before, parser, after);
// //             var context = new FakeParseContext();
// //             var result = new ParseResult<int>();
// // 
// //             // Act
// //             bool parseResult = between.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.False(parseResult);
// //             Assert.False(result.Success);
// //         }
// 
//         /// <summary>
//         /// Tests that the Parse method returns false and resets the cursor position when the "parser" fails.
//         /// </summary>
// //         [Fact] [Error] (129-58)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (129-66)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (129-74)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>' [Error] (132-21)CS0229 Ambiguity between 'ParseContext.Scanner' and 'ParseContext.Scanner' [Error] (133-43)CS0229 Ambiguity between 'ParseContext.Scanner' and 'ParseContext.Scanner' [Error] (134-26)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<int>' [Error] (137-46)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (137-59)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>' [Error] (141-33)CS0229 Ambiguity between 'ParseResult<int>.Success' and 'ParseResult<int>.Success' [Error] (142-51)CS0229 Ambiguity between 'ParseContext.Scanner' and 'ParseContext.Scanner'
// //         public void Parse_ParserFails_ResetsCursorAndReturnsFalse()
// //         {
// //             // Arrange
// //             var before = new FakeParser<string>(_fakeBeforeText) { ShouldSucceed = true, ReturnValue = "beforeValue" };
// //             var parser = new FakeParser<int>(_fakeParserText) { ShouldSucceed = false };
// //             var after = new FakeParser<char>(_fakeAfterText) { ShouldSucceed = true };
// // 
// //             var between = new Between<string, int, char>(before, parser, after);
// //             var context = new FakeParseContext();
// //             // Set initial cursor position.
// //             context.Scanner.Cursor.Position = 10;
// //             int initialPosition = context.Scanner.Cursor.Position;
// //             var result = new ParseResult<int>();
// // 
// //             // Act
// //             bool parseResult = between.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.False(parseResult);
// //             Assert.False(result.Success);
// //             Assert.Equal(initialPosition, context.Scanner.Cursor.Position);
// //         }
// 
//         /// <summary>
//         /// Tests that the Parse method returns false and resets the cursor position when the "after" parser fails.
//         /// </summary>
// //         [Fact] [Error] (156-58)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (156-66)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (156-74)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>' [Error] (159-21)CS0229 Ambiguity between 'ParseContext.Scanner' and 'ParseContext.Scanner' [Error] (160-43)CS0229 Ambiguity between 'ParseContext.Scanner' and 'ParseContext.Scanner' [Error] (161-26)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<int>' [Error] (164-46)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (164-59)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>' [Error] (168-33)CS0229 Ambiguity between 'ParseResult<int>.Success' and 'ParseResult<int>.Success' [Error] (169-51)CS0229 Ambiguity between 'ParseContext.Scanner' and 'ParseContext.Scanner'
// //         public void Parse_AfterParserFails_ResetsCursorAndReturnsFalse()
// //         {
// //             // Arrange
// //             var before = new FakeParser<string>(_fakeBeforeText) { ShouldSucceed = true, ReturnValue = "beforeValue" };
// //             var parser = new FakeParser<int>(_fakeParserText) { ShouldSucceed = true, ReturnValue = 200 };
// //             var after = new FakeParser<char>(_fakeAfterText) { ShouldSucceed = false };
// // 
// //             var between = new Between<string, int, char>(before, parser, after);
// //             var context = new FakeParseContext();
// //             // Set initial cursor position.
// //             context.Scanner.Cursor.Position = 15;
// //             int initialPosition = context.Scanner.Cursor.Position;
// //             var result = new ParseResult<int>();
// // 
// //             // Act
// //             bool parseResult = between.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.False(parseResult);
// //             Assert.False(result.Success);
// //             Assert.Equal(initialPosition, context.Scanner.Cursor.Position);
// //         }
//         #endregion
// 
//         #region ToString Method Test
// 
//         /// <summary>
//         /// Tests that the ToString method returns the expected string representation when Name is not set.
//         /// </summary>
// //         [Fact] [Error] (186-58)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (186-66)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (186-74)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>'
// //         public void ToString_WithoutName_ReturnsExpectedRepresentation()
// //         {
// //             // Arrange
// //             var before = new FakeParser<string>(_fakeBeforeText);
// //             var parser = new FakeParser<int>(_fakeParserText);
// //             var after = new FakeParser<char>(_fakeAfterText);
// // 
// //             var between = new Between<string, int, char>(before, parser, after);
// //             string expected = $"Between({before.ToString()},{parser.ToString()},{after.ToString()})";
// // 
// //             // Act
// //             string actual = between.ToString();
// // 
// //             // Assert
// //             Assert.Equal(expected, actual);
// //         }
//         #endregion
// 
//         #region Compile Method Test
// 
//         /// <summary>
//         /// Tests that the Compile method returns a CompilationResult with a non-empty body.
//         /// </summary>
// //         [Fact] [Error] (210-58)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (210-66)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (210-74)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>' [Error] (214-56)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (218-36)CS0229 Ambiguity between 'CompilationResult.Body' and 'CompilationResult.Body'
// //         public void Compile_ValidContext_ReturnsCompilationResultWithBody()
// //         {
// //             // Arrange
// //             var before = new FakeParser<string>(_fakeBeforeText) { ShouldSucceed = true, ReturnValue = "beforeValue" };
// //             var parser = new FakeParser<int>(_fakeParserText) { ShouldSucceed = true, ReturnValue = 300 };
// //             var after = new FakeParser<char>(_fakeAfterText) { ShouldSucceed = true };
// // 
// //             var between = new Between<string, int, char>(before, parser, after);
// //             var compilationContext = new FakeCompilationContext();
// // 
// //             // Act
// //             CompilationResult result = between.Compile(compilationContext);
// // 
// //             // Assert
// //             Assert.NotNull(result);
// //             Assert.NotEmpty(result.Body);
// //         }
//         #endregion
// 
//         #region ISeekable Property Tests
// 
//         /// <summary>
//         /// Tests that the ISeekable properties are properly set when the "before" parser implements ISeekable.
//         /// </summary>
// //         [Fact] [Error] (242-58)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeSeekableParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (242-74)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (242-82)CS1503 Argument 3: cannot convert from 'Parlot.Fluent.UnitTests.BetweenTests.FakeParser<char>' to 'Parlot.Fluent.Parser<char>'
// //         public void Constructor_BeforeImplementsISeekable_PropertiesAreCopied()
// //         {
// //             // Arrange
// //             var seekableBefore = new FakeSeekableParser<string>(_fakeBeforeText)
// //             {
// //                 ShouldSucceed = true,
// //                 ReturnValue = "seekable",
// //                 CanSeekValue = true,
// //                 ExpectedCharsValue = new char[] { 'a', 'b' },
// //                 SkipWhitespaceValue = true
// //             };
// //             var parser = new FakeParser<int>(_fakeParserText) { ShouldSucceed = true, ReturnValue = 400 };
// //             var after = new FakeParser<char>(_fakeAfterText) { ShouldSucceed = true };
// // 
// //             var between = new Between<string, int, char>(seekableBefore, parser, after);
// // 
// //             // Act & Assert
// //             Assert.Equal(seekableBefore.CanSeekValue, between.CanSeek);
// //             Assert.Equal(seekableBefore.ExpectedCharsValue, between.ExpectedChars);
// //             Assert.Equal(seekableBefore.SkipWhitespaceValue, between.SkipWhitespace);
// //         }
//         #endregion
// 
//         #region Fake Classes for Testing
// 
//         /// <summary>
//         /// A fake implementation of ParseContext for testing.
//         /// </summary>
// //         private class FakeParseContext : ParseContext [Error] (256-23)CS0534 'BetweenTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(Parser)' [Error] (256-23)CS0534 'BetweenTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(object)' [Error] (256-23)CS0534 'BetweenTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(Parser)' [Error] (256-23)CS0534 'BetweenTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(object)' [Error] (256-23)CS0534 'BetweenTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(Parser)' [Error] (256-23)CS0534 'BetweenTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(Parser)' [Error] (260-17)CS0229 Ambiguity between 'ParseContext.Scanner' and 'ParseContext.Scanner'
// //         {
// //             public FakeParseContext()
// //             {
// //                 Scanner = new FakeScanner();
// //             }
// // 
// //             public override void EnterParser(object parser)
// //             {
// //                 // No operation
// //             }
// // 
// //             public override void ExitParser(object parser)
// //             {
// //                 // No operation
// //             }
// //         }
// 
//         /// <summary>
//         /// A fake implementation of a scanner.
//         /// </summary>
//         private class FakeScanner
//         {
//             public FakeCursor Cursor { get; } = new FakeCursor();
//         }
// 
//         /// <summary>
//         /// A fake implementation of a cursor.
//         /// </summary>
//         private class FakeCursor
//         {
//             public int Position { get; set; } = 0;
// 
//             public void ResetPosition(int position)
//             {
//                 Position = position;
//             }
//         }
// 
//         /// <summary>
//         /// A fake parser used for testing that allows control over the success of the Parse method.
//         /// </summary>
//         /// <typeparam name="TResult">Type of the parsed result.</typeparam>
// //         private class FakeParser<TResult> : Parser<TResult> [Error] (299-23)CS0534 'BetweenTests.FakeParser<TResult>' does not implement inherited abstract member 'Parser<TResult>.Parse(object, ref ParseResult<TResult>)' [Error] (299-23)CS0534 'BetweenTests.FakeParser<TResult>' does not implement inherited abstract member 'Parser<TResult>.Parse(ParseContext, ref ParseResult<TResult>)' [Error] (299-23)CS0534 'BetweenTests.FakeParser<TResult>' does not implement inherited abstract member 'Parser<TResult>.Parse(ParseContext, ref ParseResult<TResult>)' [Error] (299-23)CS0534 'BetweenTests.FakeParser<TResult>' does not implement inherited abstract member 'Parser<TResult>.Build(CompilationContext, bool)' [Error] (299-23)CS0534 'BetweenTests.FakeParser<TResult>' does not implement inherited abstract member 'Parser<TResult>.Compile(CompilationContext)'
// //         {
// //             public bool ShouldSucceed { get; set; } = true;
// //             public TResult ReturnValue { get; set; }
// //             private readonly string _fakeText;
// // 
// //             public FakeParser(string fakeText)
// //             {
// //                 _fakeText = fakeText;
// //             }
// // 
// //             public override bool Parse(ParseContext context, ref ParseResult<TResult> result) [Error] (310-34)CS0462 The inherited members 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' have the same signature in type 'BetweenTests.FakeParser<TResult>', so they cannot be overridden [Error] (315-28)CS0229 Ambiguity between 'ParseResult<TResult>.Success' and 'ParseResult<TResult>.Success' [Error] (316-28)CS0229 Ambiguity between 'ParseResult<TResult>.Value' and 'ParseResult<TResult>.Value' [Error] (321-28)CS0229 Ambiguity between 'ParseResult<TResult>.Success' and 'ParseResult<TResult>.Success'
// //             {
// //                 // For testing, simply set the result according to ShouldSucceed.
// //                 if (ShouldSucceed)
// //                 {
// //                     result.Success = true;
// //                     result.Value = ReturnValue;
// //                     return true;
// //                 }
// //                 else
// //                 {
// //                     result.Success = false;
// //                     return false;
// //                 }
// //             }
// 
// //             public override CompilationResult Build(CompilationContext context) [Error] (329-42)CS0121 The call is ambiguous between the following methods or properties: 'CompilationContext.CreateCompilationResult<T>()' and 'CompilationContext.CreateCompilationResult<T>()'
// //             {
// //                 // Return a fake compilation result with minimal dummy expressions.
// //                 var fakeResult = context.CreateCompilationResult<TResult>();
// //                 fakeResult.Body.Add(Expression.Constant(0));
// //                 fakeResult.Success = Expression.Constant(ShouldSucceed);
// //                 fakeResult.Value = Expression.Constant(ReturnValue, typeof(TResult));
// //                 return fakeResult;
// //             }
// 
//             public override string ToString()
//             {
//                 return _fakeText;
//             }
//         }
// 
//         /// <summary>
//         /// A fake parser implementing ISeekable for testing the copying of seekable properties.
//         /// </summary>
//         /// <typeparam name="TResult">Type of the parsed result.</typeparam>
//         private class FakeSeekableParser<TResult> : FakeParser<TResult>, ISeekable
//         {
//             public bool CanSeekValue { get; set; }
//             public char[] ExpectedCharsValue { get; set; } = new char[0];
//             public bool SkipWhitespaceValue { get; set; }
// 
//             public FakeSeekableParser(string fakeText)
//                 : base(fakeText)
//             {
//             }
// 
//             public bool CanSeek => CanSeekValue;
// 
//             public char[] ExpectedChars => ExpectedCharsValue;
// 
//             public bool SkipWhitespace => SkipWhitespaceValue;
//         }
// 
//         /// <summary>
//         /// A fake implementation of CompilationContext for testing purposes.
//         /// </summary>
// //         private class FakeCompilationContext : CompilationContext [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ResetPosition(ParameterExpression)' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.Buffer()' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.SkipWhiteSpaceOrNewLine()' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ReadChar(char)' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ParseContext.get' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.Lambdas.get' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.NewTextSpan(Expression, Expression, Expression)' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DeclarePositionVariable(CompilationResult)' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.NextNumber.get' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ParseContext.get' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DeclareOffsetVariable(CompilationResult)' [Error] (367-23)CS0534 'BetweenTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.SkipWhiteSpace()'
// //         {
// //             public override bool DiscardResult { get; set; } = false;
// // 
// //             public override CompilationResult<T> CreateCompilationResult<T>()
// //             {
// //                 return new FakeCompilationResult<T>();
// //             }
// // 
// //             public override Expression DeclarePositionVariable<T>(CompilationResult<T> result) [Error] (376-40)CS0115 'BetweenTests.FakeCompilationContext.DeclarePositionVariable<T>(CompilationResult<T>)': no suitable method found to override
// //             {
// //                 // Return a dummy expression representing the declared position.
// //                 return Expression.Constant(0);
// //             }
// 
// //             public override Expression ResetPosition(Expression position) [Error] (382-40)CS0115 'BetweenTests.FakeCompilationContext.ResetPosition(Expression)': no suitable method found to override
// //             {
// //                 // Return an empty expression for testing.
// //                 return Expression.Empty();
// //             }
//         }
// 
//         /// <summary>
//         /// A fake implementation of CompilationResult for testing purposes.
//         /// </summary>
//         /// <typeparam name="TResult">Type of the compilation result value.</typeparam>
//         private class FakeCompilationResult<TResult> : CompilationResult<TResult>
//         {
//             public FakeCompilationResult()
//             {
//                 Body = new List<Expression>();
//                 Variables = new List<ParameterExpression>();
//             }
// 
// //             public override List<Expression> Body { get; } [Error] (401-46)CS0462 The inherited members 'CompilationResult<T>.Body' and 'CompilationResult<T>.Body' have the same signature in type 'BetweenTests.FakeCompilationResult<TResult>', so they cannot be overridden
//             public override List<ParameterExpression> Variables { get; }
// //             public override Expression Success { get; set; } [Error] (403-40)CS0462 The inherited members 'CompilationResult<T>.Success' and 'CompilationResult<T>.Success' have the same signature in type 'BetweenTests.FakeCompilationResult<TResult>', so they cannot be overridden
// //             public override Expression Value { get; set; } [Error] (404-40)CS0462 The inherited members 'CompilationResult<T>.Value' and 'CompilationResult<T>.Value' have the same signature in type 'BetweenTests.FakeCompilationResult<TResult>', so they cannot be overridden
//         }
// 
//         #endregion
//     }
// }
