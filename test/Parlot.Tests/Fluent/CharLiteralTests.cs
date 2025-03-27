// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Moq;
// using Parlot.Fluent;
// using Xunit;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the <see cref="CharLiteral"/> class.
//     /// </summary>
//     public class CharLiteralTests
//     {
//         private readonly char _testChar;
// 
//         public CharLiteralTests()
//         {
//             _testChar = 'a';
//         }
// 
//         /// <summary>
//         /// Tests that the constructor correctly sets the Char and ExpectedChars properties.
//         /// </summary>
//         [Fact]
//         public void Constructor_ValidInput_PropertiesSetCorrectly()
//         {
//             // Arrange & Act
//             var parser = new CharLiteral(_testChar);
// 
//             // Assert
//             Assert.Equal(_testChar, parser.Char);
//             Assert.NotNull(parser.ExpectedChars);
//             Assert.Single(parser.ExpectedChars);
//             Assert.Equal(_testChar, parser.ExpectedChars[0]);
//             Assert.True(parser.CanSeek);
//             Assert.False(parser.SkipWhitespace);
//         }
// 
//         /// <summary>
//         /// Tests that Parse method returns true and sets result correctly when the input matches the expected character.
//         /// </summary>
// //         [Fact] [Error] (49-34)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(string)' and 'FakeCursor.FakeCursor(string)' [Error] (50-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeScanner.FakeScanner(FakeCursor)' and 'FakeScanner.FakeScanner(FakeCursor)' [Error] (51-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (52-46)CS0452 The type 'char' must be a reference type in order to use it as parameter 'T' in the generic type or method 'FakeParseResult<T>' [Error] (55-46)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (55-63)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<char>' to 'ref Parlot.ParseResult<char>' [Error] (59-36)CS0229 Ambiguity between 'FakeParseResult<char>.Start' and 'FakeParseResult<char>.Start' [Error] (60-36)CS0229 Ambiguity between 'FakeParseResult<char>.End' and 'FakeParseResult<char>.End' [Error] (61-44)CS0229 Ambiguity between 'FakeParseResult<char>.Value' and 'FakeParseResult<char>.Value'
// //         public void Parse_InputMatches_ReturnsTrueAndSetsResult()
// //         {
// //             // Arrange
// //             var parser = new CharLiteral(_testChar);
// //             // Create a fake cursor with input that starts with _testChar.
// //             var fakeCursor = new FakeCursor(_testChar.ToString());
// //             var fakeScanner = new FakeScanner(fakeCursor);
// //             var fakeContext = new FakeParseContext(fakeScanner);
// //             var result = new FakeParseResult<char>();
// // 
// //             // Act
// //             bool parseSuccess = parser.Parse(fakeContext, ref result);
// // 
// //             // Assert
// //             Assert.True(parseSuccess);
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(1, result.End);
// //             Assert.Equal(_testChar, result.Value);
// //         }
// 
//         /// <summary>
//         /// Tests that Parse method returns false when the input does not match the expected character.
//         /// </summary>
// //         [Fact] [Error] (74-34)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(string)' and 'FakeCursor.FakeCursor(string)' [Error] (75-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeScanner.FakeScanner(FakeCursor)' and 'FakeScanner.FakeScanner(FakeCursor)' [Error] (76-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (77-46)CS0452 The type 'char' must be a reference type in order to use it as parameter 'T' in the generic type or method 'FakeParseResult<T>' [Error] (80-46)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (80-63)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<char>' to 'ref Parlot.ParseResult<char>' [Error] (85-36)CS0229 Ambiguity between 'FakeParseResult<char>.Start' and 'FakeParseResult<char>.Start' [Error] (86-36)CS0229 Ambiguity between 'FakeParseResult<char>.End' and 'FakeParseResult<char>.End' [Error] (87-48)CS0229 Ambiguity between 'FakeParseResult<char>.Value' and 'FakeParseResult<char>.Value'
// //         public void Parse_InputDoesNotMatch_ReturnsFalse()
// //         {
// //             // Arrange
// //             var parser = new CharLiteral(_testChar);
// //             // Create a fake cursor with input that does NOT start with _testChar.
// //             string input = (_testChar == 'a') ? "b" : "a";
// //             var fakeCursor = new FakeCursor(input);
// //             var fakeScanner = new FakeScanner(fakeCursor);
// //             var fakeContext = new FakeParseContext(fakeScanner);
// //             var result = new FakeParseResult<char>();
// // 
// //             // Act
// //             bool parseSuccess = parser.Parse(fakeContext, ref result);
// // 
// //             // Assert
// //             Assert.False(parseSuccess);
// //             // Since parsing failed, result should remain unset (default values).
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(0, result.End);
// //             Assert.Equal(default(char), result.Value);
// //         }
// 
//         /// <summary>
//         /// Tests that Compile method adds an expected Expression to the CompilationResult Body.
//         /// </summary>
// //         [Fact] [Error] (98-46)CS1729 'FakeCompilationContext' does not contain a constructor that takes 0 arguments [Error] (100-17)CS0229 Ambiguity between 'FakeCompilationContext.DiscardResult' and 'FakeCompilationContext.DiscardResult' [Error] (104-52)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
// //         public void Compile_Always_AddsIfThenExpressionToBody()
// //         {
// //             // Arrange
// //             var parser = new CharLiteral(_testChar);
// //             var fakeCompilationContext = new FakeCompilationContext
// //             {
// //                 DiscardResult = false
// //             };
// // 
// //             // Act
// //             var compilationResult = parser.Compile(fakeCompilationContext);
// // 
// //             // Assert
// //             Assert.NotNull(compilationResult);
// //             Assert.NotNull(compilationResult.Body);
// //             Assert.Single(compilationResult.Body);
// //             // Validate that the expression added is a ConditionalExpression (from the Expression.IfThen call).
// //             Assert.IsType<ConditionalExpression>(compilationResult.Body[0]);
// //         }
// 
//         /// <summary>
//         /// Tests that ToString returns the correct string representation of the parser.
//         /// </summary>
//         [Fact]
//         public void ToString_ReturnsCorrectFormat()
//         {
//             // Arrange
//             var parser = new CharLiteral(_testChar);
//             var expected = $"Char('{_testChar}')";
// 
//             // Act
//             string actual = parser.ToString();
// 
//             // Assert
//             Assert.Equal(expected, actual);
//         }
//     }
// 
//     #region Fake Implementations for Testing Parse
// 
//     /// <summary>
//     /// Minimal fake implementation of a cursor to simulate text scanning.
//     /// </summary>
// //     internal class FakeCursor [Error] (137-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCursor' [Error] (144-13)CS0229 Ambiguity between 'FakeCursor._input' and 'FakeCursor._input' [Error] (145-13)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
// //     {
// //         private readonly string _input; [Error] (139-33)CS0169 The field 'FakeCursor._input' is never used
// //         public int Offset { get; private set; }
// // 
// //         public FakeCursor(string input)
// //         {
// //             _input = input;
// //             Offset = 0;
// //         }
// // 
// //         /// <summary>
// //         /// Simulates matching a character at the current offset.
// //         /// </summary>
// //         /// <param name="c">The character to match.</param>
// //         /// <returns>True if character matches; otherwise, false.</returns>
// //         public bool Match(char c) [Error] (155-17)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset' [Error] (155-26)CS0229 Ambiguity between 'FakeCursor._input' and 'FakeCursor._input' [Error] (155-43)CS0229 Ambiguity between 'FakeCursor._input' and 'FakeCursor._input' [Error] (155-50)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
// //         {
// //             if (Offset < _input.Length && _input[Offset] == c)
// //             {
// //                 return true;
// //             }
// //             return false;
// //         }
// // 
// //         /// <summary>
// //         /// Advances the cursor by one character.
// //         /// </summary>
// //         public void Advance() [Error] (167-17)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset' [Error] (167-26)CS0229 Ambiguity between 'FakeCursor._input' and 'FakeCursor._input' [Error] (169-17)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
// //         {
// //             if (Offset < _input.Length)
// //             {
// //                 Offset++;
// //             }
// //         }
// //     }
// 
//     /// <summary>
//     /// Minimal fake implementation of a scanner that holds a cursor.
//     /// </summary>
// //     internal class FakeScanner [Error] (177-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeScanner' [Error] (183-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor'
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
//     /// Minimal fake implementation of a parse context used by the parser.
//     /// </summary>
// //     internal class FakeParseContext : ParseContext [Error] (190-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseContext' [Error] (194-13)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner'
// //     {
// //         public FakeParseContext(FakeScanner scanner)
// //         {
// //             Scanner = scanner;
// //         }
// // 
// //         public override void EnterParser(Parser parser) [Error] (197-42)CS0305 Using the generic type 'Parser<T>' requires 1 type arguments
// //         {
// //             // No operation needed for testing.
// //         }
// // 
// //         public override void ExitParser(Parser parser) [Error] (202-41)CS0305 Using the generic type 'Parser<T>' requires 1 type arguments
// //         {
// //             // No operation needed for testing.
// //         }
// //     }
// 
//     /// <summary>
//     /// Minimal fake implementation of a parse result to store parsing outcomes.
//     /// </summary>
// //     internal class FakeParseResult<T> : ParseResult<T> [Error] (211-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseResult'
// //     {
// //         public override void Set(int start, int end, T value) [Error] (213-30)CS0462 The inherited members 'ParseResult<T>.Set(int, int, T)' and 'ParseResult<T>.Set(int, int, T)' have the same signature in type 'FakeParseResult<T>', so they cannot be overridden [Error] (213-30)CS0111 Type 'FakeParseResult<T>' already defines a member called 'Set' with the same parameter types [Error] (215-13)CS0229 Ambiguity between 'FakeParseResult<T>.Start' and 'FakeParseResult<T>.Start' [Error] (216-13)CS0229 Ambiguity between 'FakeParseResult<T>.End' and 'FakeParseResult<T>.End' [Error] (217-13)CS0229 Ambiguity between 'FakeParseResult<T>.Value' and 'FakeParseResult<T>.Value'
// //         {
// //             Start = start;
// //             End = end;
// //             Value = value;
// //         }
// //     }
// 
//     #endregion
// 
//     #region Fake Implementations for Testing Compilation
// 
//     /// <summary>
//     /// Minimal fake implementation of a compilation context.
//     /// </summary>
// //     internal class FakeCompilationContext : CompilationContext [Error] (228-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationContext'
// //     {
// //         public override bool DiscardResult { get; set; }
// // 
// //         public override CompilationResult<T> CreateCompilationResult<T>() [Error] (232-46)CS0111 Type 'FakeCompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types [Error] (234-24)CS0121 The call is ambiguous between the following methods or properties: 'FakeCompilationResult<T>.FakeCompilationResult()' and 'FakeCompilationResult<T>.FakeCompilationResult()'
// //         {
// //             return new FakeCompilationResult<T>();
// //         }
// // 
// //         public override Expression ReadChar(char expected)
// //         {
// //             // For testing purposes, simulate a check that always returns true.
// //             return Expression.Constant(true);
// //         }
// //     }
// 
//     /// <summary>
//     /// Minimal fake implementation of a compilation result.
//     /// </summary>
// //     internal class FakeCompilationResult<T> : CompilationResult<T> [Error] (247-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationResult' [Error] (251-13)CS0229 Ambiguity between 'FakeCompilationResult<T>.Body' and 'FakeCompilationResult<T>.Body' [Error] (252-13)CS0229 Ambiguity between 'FakeCompilationResult<T>.Success' and 'FakeCompilationResult<T>.Success' [Error] (253-13)CS0229 Ambiguity between 'FakeCompilationResult<T>.Value' and 'FakeCompilationResult<T>.Value'
// //     {
// //         public FakeCompilationResult()
// //         {
// //             Body = new List<Expression>();
// //             Success = Expression.Parameter(typeof(bool), "success");
// //             Value = Expression.Parameter(typeof(T), "value");
// //         }
// //     }
// 
//     #endregion
// 
//     #region Minimal Base Classes to Support Fake Implementations
// 
//     /// <summary>
//     /// Minimal base implementation of a parse context.
//     /// </summary>
//     public abstract class ParseContext
//     {
//         public FakeScanner Scanner { get; set; }
// 
// //         public abstract void EnterParser(Parser parser); [Error] (268-42)CS0305 Using the generic type 'Parser<T>' requires 1 type arguments
// //         public abstract void ExitParser(Parser parser); [Error] (269-41)CS0305 Using the generic type 'Parser<T>' requires 1 type arguments
//     }
// 
//     /// <summary>
//     /// Minimal base implementation of a parse result.
//     /// </summary>
//     public abstract class ParseResult<T>
//     {
//         public int Start { get; set; }
//         public int End { get; set; }
//         public T Value { get; set; }
//         public abstract void Set(int start, int end, T value);
//     }
// 
//     /// <summary>
//     /// Minimal base implementation of a compilation context.
//     /// </summary>
//     public abstract class CompilationContext
//     {
//         public abstract bool DiscardResult { get; set; }
//         public abstract CompilationResult<T> CreateCompilationResult<T>();
//         public abstract Expression ReadChar(char expected);
//     }
// 
//     /// <summary>
//     /// Minimal base implementation of a compilation result.
//     /// </summary>
//     public abstract class CompilationResult<T>
//     {
// //         public List<Expression> Body { get; set; } [Error] (298-33)CS0108 'CompilationResult<T>.Body' hides inherited member 'CompilationResult.Body'. Use the new keyword if hiding was intended.
// //         public ParameterExpression Success { get; set; } [Error] (299-36)CS0108 'CompilationResult<T>.Success' hides inherited member 'CompilationResult.Success'. Use the new keyword if hiding was intended.
// //         public ParameterExpression Value { get; set; } [Error] (300-36)CS0108 'CompilationResult<T>.Value' hides inherited member 'CompilationResult.Value'. Use the new keyword if hiding was intended.
//     }
// 
//     /// <summary>
//     /// Minimal base class for a parser.
//     /// </summary>
//     public abstract class Parser<T>
//     {
//         public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
//     }
// 
//     #endregion
// }
