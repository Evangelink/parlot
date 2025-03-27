// using Moq;
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
//     /// Unit tests for the <see cref="Identifier"/> class.
//     /// </summary>
//     public class IdentifierTests
//     {
//         private readonly Identifier _identifierWithoutExtras;
//         private readonly Identifier _identifierWithExtras;
// 
//         public IdentifierTests()
//         {
//             _identifierWithoutExtras = new Identifier();
//             // Extra delegate that treats digits as valid identifier start and part.
//             Func<char, bool> extraPredicate = c => char.IsDigit(c);
//             _identifierWithExtras = new Identifier(extraPredicate, extraPredicate);
//         }
// 
//         #region Parse Method Tests
// 
//         /// <summary>
//         /// Tests the Parse method using a valid identifier without extra delegates.
//         /// It verifies that a correct identifier is parsed and the TextSpan is set properly.
//         /// </summary>
// //         [Fact] [Error] (42-59)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IdentifierTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (42-72)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.IdentifierTests.FakeParseResult<Parlot.Fluent.UnitTests.IdentifierTests.FakeTextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (48-46)CS0121 The call is ambiguous between the following methods or properties: 'object.ToString()' and 'object.ToString()'
// //         public void Parse_ValidIdentifierWithoutExtras_ReturnsTrueAndSetsTextSpan()
// //         {
// //             // Arrange
// //             string input = "abc";
// //             var context = new FakeParseContext(input);
// //             var result = new FakeParseResult<FakeTextSpan>();
// // 
// //             // Act
// //             bool success = _identifierWithoutExtras.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.True(success);
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(input.Length, result.End);
// //             Assert.Equal(input, result.Value?.ToString());
// //         }
// 
//         /// <summary>
//         /// Tests the Parse method using an input with an invalid starting character.
//         /// The test verifies that Parse returns false and does not modify the result.
//         /// </summary>
// //         [Fact] [Error] (65-59)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IdentifierTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (65-72)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.IdentifierTests.FakeParseResult<Parlot.Fluent.UnitTests.IdentifierTests.FakeTextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>'
// //         public void Parse_InvalidIdentifierStart_ReturnsFalse()
// //         {
// //             // Arrange
// //             // '1' is assumed to be an invalid start per default Character rules.
// //             string input = "1abc";
// //             var context = new FakeParseContext(input);
// //             var result = new FakeParseResult<FakeTextSpan>();
// // 
// //             // Act
// //             bool success = _identifierWithoutExtras.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.False(success);
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(0, result.End);
// //             Assert.Null(result.Value);
// //         }
// 
//         /// <summary>
//         /// Tests the Parse method when extraStart delegate is provided.
//         /// This verifies that an otherwise invalid start character is accepted.
//         /// </summary>
// //         [Fact] [Error] (88-56)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IdentifierTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (88-69)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.IdentifierTests.FakeParseResult<Parlot.Fluent.UnitTests.IdentifierTests.FakeTextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (94-46)CS0121 The call is ambiguous between the following methods or properties: 'object.ToString()' and 'object.ToString()'
// //         public void Parse_ValidIdentifierWithExtraStart_ReturnsTrueAndSetsTextSpan()
// //         {
// //             // Arrange
// //             // '1' is normally invalid, but allowed via the extraStart delegate.
// //             string input = "1abc";
// //             var context = new FakeParseContext(input);
// //             var result = new FakeParseResult<FakeTextSpan>();
// // 
// //             // Act
// //             bool success = _identifierWithExtras.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.True(success);
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(input.Length, result.End);
// //             Assert.Equal(input, result.Value?.ToString());
// //         }
// 
//         /// <summary>
//         /// Tests the Parse method when extraPart delegate is provided.
//         /// This verifies that additional characters normally not allowed as parts are accepted.
//         /// </summary>
// //         [Fact] [Error] (111-56)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IdentifierTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (111-69)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.IdentifierTests.FakeParseResult<Parlot.Fluent.UnitTests.IdentifierTests.FakeTextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (117-46)CS0121 The call is ambiguous between the following methods or properties: 'object.ToString()' and 'object.ToString()'
// //         public void Parse_ValidIdentifierWithExtraPart_ReturnsTrueAndSetsTextSpan()
// //         {
// //             // Arrange
// //             // '1' at a non-first position is normally invalid, but allowed via the extraPart delegate.
// //             string input = "abc1";
// //             var context = new FakeParseContext(input);
// //             var result = new FakeParseResult<FakeTextSpan>();
// // 
// //             // Act
// //             bool success = _identifierWithExtras.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.True(success);
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(input.Length, result.End);
// //             Assert.Equal(input, result.Value?.ToString());
// //         }
// 
//         #endregion
// 
//         #region Compile Method Tests
// 
//         /// <summary>
//         /// Tests the Compile method to ensure it returns a CompilationResult with a non-empty body and variables.
//         /// </summary>
// //         [Fact] [Error] (134-70)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IdentifierTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
// //         public void Compile_ReturnsValidCompilationResult()
// //         {
// //             // Arrange
// //             var context = new FakeCompilationContext();
// // 
// //             // Act
// //             var compilationResult = _identifierWithoutExtras.Compile(context);
// // 
// //             // Assert
// //             Assert.NotNull(compilationResult);
// //             Assert.NotEmpty(compilationResult.Body);
// //             Assert.NotEmpty(compilationResult.Variables);
// //         }
// 
//         /// <summary>
//         /// Tests the Compile method when DiscardResult is enabled.
//         /// Expected behavior is that the expression assigning the value is not generated.
//         /// </summary>
// //         [Fact] [Error] (153-70)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IdentifierTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
// //         public void Compile_WithDiscardResult_DoesNotAssignValue()
// //         {
// //             // Arrange
// //             var context = new FakeCompilationContext { DiscardResult = true };
// // 
// //             // Act
// //             var compilationResult = _identifierWithoutExtras.Compile(context);
// // 
// //             // Assert
// //             Assert.NotNull(compilationResult);
// //             // Since expression details are abstracted, we ensure that the Body contains expressions.
// //             Assert.NotEmpty(compilationResult.Body);
// //         }
// 
//         #endregion
// 
//         #region Fake Classes for Parse Testing
// 
//         /// <summary>
//         /// A fake implementation of ParseContext for simulating the scanner and cursor.
//         /// </summary>
//         private class FakeParseContext
//         {
//             public FakeScanner Scanner { get; }
// 
//             public FakeParseContext(string buffer)
//             {
//                 Scanner = new FakeScanner(buffer);
//             }
// 
//             public void EnterParser(object parser) { }
//             public void ExitParser(object parser) { }
//         }
// 
//         /// <summary>
//         /// A fake scanner that holds a cursor over an input string.
//         /// </summary>
//         private class FakeScanner
//         {
//             public FakeCursor Cursor { get; }
// 
//             public FakeScanner(string buffer)
//             {
//                 Cursor = new FakeCursor(buffer);
//             }
//         }
// 
//         /// <summary>
//         /// A fake cursor that iterates over an input buffer.
//         /// </summary>
//         private class FakeCursor
//         {
//             private readonly string _buffer;
//             public int Offset { get; private set; }
// 
//             public FakeCursor(string buffer)
//             {
//                 _buffer = buffer;
//                 Offset = 0;
//             }
// 
//             public char Current => Offset < _buffer.Length ? _buffer[Offset] : '\0';
//             public bool Eof => Offset >= _buffer.Length;
// 
//             public void AdvanceNoNewLines(int count)
//             {
//                 Offset += count;
//                 if (Offset > _buffer.Length)
//                 {
//                     Offset = _buffer.Length;
//                 }
//             }
//         }
// 
//         /// <summary>
//         /// A fake result to hold the outcome of parsing.
//         /// </summary>
//         /// <typeparam name="T">The type of the parsed value.</typeparam>
//         private class FakeParseResult<T>
//         {
//             public int Start { get; set; }
//             public int End { get; set; }
// //             public T? Value { get; set; } [Error] (229-21)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
// 
//             public void Set(int start, int end, T value)
//             {
//                 Start = start;
//                 End = end;
//                 Value = value;
//             }
//         }
// 
//         /// <summary>
//         /// A fake TextSpan class that mimics parsed text segments.
//         /// </summary>
// //         private class FakeTextSpan [Error] (250-17)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Buffer' and 'IdentifierTests.FakeTextSpan.Buffer' [Error] (251-17)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Start' and 'IdentifierTests.FakeTextSpan.Start' [Error] (252-17)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Length' and 'IdentifierTests.FakeTextSpan.Length'
// //         {
// //             public string Buffer { get; }
// //             public int Start { get; }
// //             public int Length { get; }
// // 
// //             public FakeTextSpan(string buffer, int start, int length)
// //             {
// //                 Buffer = buffer;
// //                 Start = start;
// //                 Length = length;
// //             }
// // 
// //             public override string ToString() [Error] (257-24)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Buffer' and 'IdentifierTests.FakeTextSpan.Buffer' [Error] (257-41)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Start' and 'IdentifierTests.FakeTextSpan.Start' [Error] (257-48)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Length' and 'IdentifierTests.FakeTextSpan.Length'
// //             {
// //                 return Buffer.Substring(Start, Length);
// //             }
// //         }
// 
//         #endregion
// 
//         #region Fake Classes for Compilation Testing
// 
//         /// <summary>
//         /// A fake implementation of CompilationContext for simulating expression compilation.
//         /// </summary>
// //         private class FakeCompilationContext : CompilationContext [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ResetPosition(ParameterExpression)' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.SkipWhiteSpaceOrNewLine()' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ReadChar(char)' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ParseContext.get' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.Lambdas.get' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DeclarePositionVariable(CompilationResult)' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ParseContext.get' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DeclareOffsetVariable(CompilationResult)' [Error] (268-23)CS0534 'IdentifierTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.SkipWhiteSpace()'
// //         {
// //             private int _counter = 1;
// //             public override int NextNumber => _counter++;
// //             public override bool DiscardResult { get; set; }
// // 
// //             public override CompilationResult<T> CreateCompilationResult<T>()
// //             {
// //                 return (CompilationResult<T>)(object)new FakeCompilationResult<T>();
// //             }
// // 
// //             public override Expression Current() [Error] (279-40)CS0115 'IdentifierTests.FakeCompilationContext.Current()': no suitable method found to override
// //             {
// //                 return Expression.Parameter(typeof(char), "current");
// //             }
// 
// //             public override Expression Offset() [Error] (284-40)CS0115 'IdentifierTests.FakeCompilationContext.Offset()': no suitable method found to override
// //             {
// //                 return Expression.Constant(0);
// //             }
// 
// //             public override Expression AdvanceNoNewLine(Expression count) [Error] (289-40)CS0115 'IdentifierTests.FakeCompilationContext.AdvanceNoNewLine(Expression)': no suitable method found to override
// //             {
// //                 return Expression.Empty();
// //             }
// 
// //             public override Expression Eof() [Error] (294-40)CS0115 'IdentifierTests.FakeCompilationContext.Eof()': no suitable method found to override
// //             {
// //                 return Expression.Constant(false);
// //             }
// 
//             public override Expression Buffer()
//             {
//                 return Expression.Constant("dummyBuffer");
//             }
// 
//             public override Expression NewTextSpan(Expression buffer, Expression start, Expression length)
//             {
//                 var ctor = typeof(FakeTextSpan).GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });
//                 return Expression.New(ctor, buffer, start, length);
//             }
//         }
// 
//         /// <summary>
//         /// A fake compilation result that records expression bodies and variables.
//         /// </summary>
//         /// <typeparam name="T">The type of the compilation result value.</typeparam>
//         private class FakeCompilationResult<T> : CompilationResult<T>
//         {
// //             public override List<Expression> Body { get; } = new List<Expression>(); [Error] (317-46)CS0462 The inherited members 'CompilationResult<T>.Body' and 'CompilationResult<T>.Body' have the same signature in type 'IdentifierTests.FakeCompilationResult<T>', so they cannot be overridden
//             public override List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
// //             public override ParameterExpression Value { get; set; } = Expression.Parameter(typeof(T), "value"); [Error] (319-49)CS0462 The inherited members 'CompilationResult<T>.Value' and 'CompilationResult<T>.Value' have the same signature in type 'IdentifierTests.FakeCompilationResult<T>', so they cannot be overridden
// //             public override ParameterExpression Success { get; set; } = Expression.Parameter(typeof(bool), "success"); [Error] (320-49)CS0462 The inherited members 'CompilationResult<T>.Success' and 'CompilationResult<T>.Success' have the same signature in type 'IdentifierTests.FakeCompilationResult<T>', so they cannot be overridden
//         }
// 
//         /// <summary>
//         /// A fake TextSpan used during compilation testing.
//         /// </summary>
// //         private class FakeTextSpan [Error] (326-23)CS0102 The type 'IdentifierTests' already contains a definition for 'FakeTextSpan' [Error] (332-20)CS0111 Type 'IdentifierTests.FakeTextSpan' already defines a member called 'FakeTextSpan' with the same parameter types [Error] (334-17)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Buffer' and 'IdentifierTests.FakeTextSpan.Buffer' [Error] (335-17)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Start' and 'IdentifierTests.FakeTextSpan.Start' [Error] (336-17)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Length' and 'IdentifierTests.FakeTextSpan.Length'
// //         {
// //             public string Buffer { get; }
// //             public int Start { get; }
// //             public int Length { get; }
// // 
// //             public FakeTextSpan(string buffer, int start, int length)
// //             {
// //                 Buffer = buffer;
// //                 Start = start;
// //                 Length = length;
// //             }
// // 
// //             public override string ToString() [Error] (339-36)CS0111 Type 'IdentifierTests.FakeTextSpan' already defines a member called 'ToString' with the same parameter types [Error] (341-24)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Buffer' and 'IdentifierTests.FakeTextSpan.Buffer' [Error] (341-41)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Start' and 'IdentifierTests.FakeTextSpan.Start' [Error] (341-48)CS0229 Ambiguity between 'IdentifierTests.FakeTextSpan.Length' and 'IdentifierTests.FakeTextSpan.Length'
// //             {
// //                 return Buffer.Substring(Start, Length);
// //             }
// //         }
// 
//         #endregion
//     }
// }
