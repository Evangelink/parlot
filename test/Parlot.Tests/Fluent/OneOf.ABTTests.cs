// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Moq;
// using Parlot.Compilation;
// using Parlot.Fluent;
// using Xunit;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Contains helper implementations to support testing of parser and compilation functionality.
//     /// </summary>
//     internal static class TestHelpers
//     {
//         /// <summary>
//         /// A fake implementation of ParseContext for testing purposes.
//         /// </summary>
// //         internal class FakeParseContext : ParseContext [Error] (19-24)CS0534 'TestHelpers.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(Parser)' [Error] (19-24)CS0534 'TestHelpers.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(object)' [Error] (19-24)CS0534 'TestHelpers.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(Parser)' [Error] (19-24)CS0534 'TestHelpers.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(object)' [Error] (19-24)CS0534 'TestHelpers.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(Parser)' [Error] (19-24)CS0534 'TestHelpers.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(Parser)'
// //         {
// //             public List<object> EnteredParsers { get; } = new List<object>();
// //             public List<object> ExitedParsers { get; } = new List<object>();
// // 
// //             public override void EnterParser(object parser)
// //             {
// //                 EnteredParsers.Add(parser);
// //             }
// // 
// //             public override void ExitParser(object parser)
// //             {
// //                 ExitedParsers.Add(parser);
// //             }
// //         }
// 
//         /// <summary>
//         /// A fake implementation of ParseResult&lt;T&gt; for testing purposes.
//         /// </summary>
//         /// <typeparam name="T">The type of the parse result value.</typeparam>
// //         internal class FakeParseResult<T> : ParseResult<T> [Error] (39-24)CS0534 'TestHelpers.FakeParseResult<T>' does not implement inherited abstract member 'ParseResult<T>.Set(int, int, T)'
// //         {
// //             public int StartValue { get; private set; }
// //             public int EndValue { get; private set; }
// //             public T ParsedValue { get; private set; }
// // 
// //             public override void Set(int start, int end, T value) [Error] (45-34)CS0462 The inherited members 'ParseResult<T>.Set(int, int, T)' and 'ParseResult<T>.Set(int, int, T)' have the same signature in type 'TestHelpers.FakeParseResult<T>', so they cannot be overridden
// //             {
// //                 StartValue = start;
// //                 EndValue = end;
// //                 ParsedValue = value;
// //             }
//         }
// 
//         /// <summary>
//         /// A fake implementation of CompilationResult&lt;T&gt; for testing purposes.
//         /// </summary>
//         /// <typeparam name="T">The type of the compilation result value.</typeparam>
//         internal class FakeCompilationResult<T> : CompilationResult
//         {
// //             public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>(); [Error] (59-46)CS0108 'TestHelpers.FakeCompilationResult<T>.Variables' hides inherited member 'CompilationResult.Variables'. Use the new keyword if hiding was intended.
// //             public List<Expression> Body { get; } = new List<Expression>(); [Error] (60-37)CS0108 'TestHelpers.FakeCompilationResult<T>.Body' hides inherited member 'CompilationResult.Body'. Use the new keyword if hiding was intended.
// //             public Expression Success { get; set; } [Error] (61-31)CS0108 'TestHelpers.FakeCompilationResult<T>.Success' hides inherited member 'CompilationResult.Success'. Use the new keyword if hiding was intended.
// //             public Expression Value { get; set; } [Error] (62-31)CS0108 'TestHelpers.FakeCompilationResult<T>.Value' hides inherited member 'CompilationResult.Value'. Use the new keyword if hiding was intended.
//         }
// 
//         /// <summary>
//         /// A fake implementation of CompilationContext for testing purposes.
//         /// </summary>
// //         internal class FakeCompilationContext : CompilationContext [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ResetPosition(ParameterExpression)' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.Buffer()' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.SkipWhiteSpaceOrNewLine()' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ReadChar(char)' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ParseContext.get' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.set' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.Lambdas.get' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.NewTextSpan(Expression, Expression, Expression)' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DeclarePositionVariable(CompilationResult)' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.NextNumber.get' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ParseContext.get' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DeclareOffsetVariable(CompilationResult)' [Error] (68-24)CS0534 'TestHelpers.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.SkipWhiteSpace()'
// //         {
// //             public bool DiscardResult { get; set; } [Error] (70-25)CS0114 'TestHelpers.FakeCompilationContext.DiscardResult' hides inherited member 'CompilationContext.DiscardResult'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
//             public override CompilationResult CreateCompilationResult<T>()
//             {
//                 return new FakeCompilationResult<T>();
//             }
//         }
//     }
// 
//     /// <summary>
//     /// A fake parser implementation to simulate parsing behavior for testing.
//     /// </summary>
//     /// <typeparam name="T">The type of value produced by this parser.</typeparam>
// //     internal class FakeParser<T> : Parser<T> [Error] (82-20)CS0534 'FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(object, ref ParseResult<T>)' [Error] (82-20)CS0534 'FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' [Error] (82-20)CS0534 'FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' [Error] (82-20)CS0534 'FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Build(CompilationContext, bool)' [Error] (82-20)CS0534 'FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Compile(CompilationContext)'
// //     {
// //         /// <summary>
// //         /// Determines whether the Parse method should simulate a successful parse.
// //         /// </summary>
// //         public bool ShouldParseSucceed { get; set; }
// // 
// //         /// <summary>
// //         /// The value to assign to the result if parsing succeeds.
// //         /// </summary>
// //         public T ParseValue { get; set; }
// // 
// //         /// <summary>
// //         /// Gets or sets a custom string representation for the parser.
// //         /// </summary>
// //         public string CustomToString { get; set; } = "FakeParser";
// // 
// //         /// <summary>
// //         /// Indicates what the Build method should return in the Success expression.
// //         /// </summary>
// //         public bool BuildSuccessOutcome { get; set; }
// // 
// //         /// <summary>
// //         /// Gets or sets the value to be used in the Build method for the Value expression.
// //         /// </summary>
// //         public T BuildValueOutcome { get; set; }
// // 
// //         /// <summary>
// //         /// Simulates parsing by setting the result if ShouldParseSucceed is true.
// //         /// </summary>
// //         /// <param name="context">The parse context.</param>
// //         /// <param name="result">The parse result passed by reference.</param>
// //         /// <returns>True if parsing is successful; otherwise, false.</returns>
// //         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (115-30)CS0462 The inherited members 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' have the same signature in type 'FakeParser<T>', so they cannot be overridden [Error] (119-24)CS0121 The call is ambiguous between the following methods or properties: 'ParseResult<T>.Set(int, int, T)' and 'ParseResult<T>.Set(int, int, T)'
// //         {
// //             if (ShouldParseSucceed)
// //             {
// //                 result.Set(0, 1, ParseValue);
// //                 return true;
// //             }
// //             return false;
// //         }
// 
//         /// <summary>
//         /// Simulates building a compilation result.
//         /// </summary>
//         /// <param name="context">The compilation context.</param>
//         /// <returns>A fake compilation result with preset expressions.</returns>
// //         public override CompilationResult Build(CompilationContext context) [Error] (132-71)CS0121 The call is ambiguous between the following methods or properties: 'CompilationContext.CreateCompilationResult<T>()' and 'CompilationContext.CreateCompilationResult<T>()'
// //         {
// //             var fakeResult = new TestHelpers.FakeCompilationContext().CreateCompilationResult<T>() as TestHelpers.FakeCompilationResult<T>;
// //             // Add a dummy variable for demonstration
// //             fakeResult.Variables.Add(Expression.Parameter(typeof(T), "dummy"));
// //             // Add a dummy body expression
// //             fakeResult.Body.Add(Expression.Constant("build"));
// //             fakeResult.Success = Expression.Constant(BuildSuccessOutcome);
// //             fakeResult.Value = Expression.Constant(BuildValueOutcome, typeof(T));
// //             return fakeResult;
// //         }
// 
//         /// <inheritdoc/>
//         public override string ToString() => CustomToString;
//     }
// 
//     /// <summary>
//     /// Unit tests for the <see cref="OneOf{A, B, T}"/> class.
//     /// </summary>
// //     public class OneOfTests [Error] (156-32)CS7036 There is no argument given that corresponds to the required parameter 'value' of 'FakeParser<string>.FakeParser(string)' [Error] (157-32)CS7036 There is no argument given that corresponds to the required parameter 'value' of 'FakeParser<string>.FakeParser(string)'
// //     {
// //         private readonly FakeParser<string> _fakeParserA;
// //         private readonly FakeParser<string> _fakeParserB;
// // 
// //         public OneOfTests()
// //         {
// //             _fakeParserA = new FakeParser<string> { CustomToString = "ParserA" };
// //             _fakeParserB = new FakeParser<string> { CustomToString = "ParserB" };
// //         }
// // 
// //         /// <summary>
// //         /// Tests that the constructor throws an ArgumentNullException when the first parser is null.
// //         /// </summary>
// //         [Fact] [Error] (168-31)CS7036 There is no argument given that corresponds to the required parameter 'value' of 'FakeParser<string>.FakeParser(string)' [Error] (171-99)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (171-108)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>'
// //         public void Constructor_NullParserA_ThrowsArgumentNullException()
// //         {
// //             // Arrange
// //             FakeParser<string> parserA = null;
// //             var parserB = new FakeParser<string>();
// // 
// //             // Act & Assert
// //             var ex = Assert.Throws<ArgumentNullException>(() => new OneOf<string, string, string>(parserA, parserB));
// //             Assert.Equal("parserA", ex.ParamName);
// //         }
// // 
// //         /// <summary>
// //         /// Tests that the constructor throws an ArgumentNullException when the second parser is null.
// //         /// </summary>
// //         [Fact] [Error] (182-31)CS7036 There is no argument given that corresponds to the required parameter 'value' of 'FakeParser<string>.FakeParser(string)' [Error] (186-99)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (186-108)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>'
// //         public void Constructor_NullParserB_ThrowsArgumentNullException()
// //         {
// //             // Arrange
// //             var parserA = new FakeParser<string>();
// //             FakeParser<string> parserB = null;
// // 
// //             // Act & Assert
// //             var ex = Assert.Throws<ArgumentNullException>(() => new OneOf<string, string, string>(parserA, parserB));
// //             Assert.Equal("parserB", ex.ParamName);
// //         }
// // 
// //         /// <summary>
// //         /// Tests that the Parse method returns true and sets the result when the first parser succeeds.
// //         /// </summary>
// //         [Fact] [Error] (200-59)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (200-73)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (205-40)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.TestHelpers.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (205-57)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<string>' to 'ref Parlot.ParseResult<string>'
// //         public void Parse_WhenParserASucceeds_ReturnsTrueAndSetsResult()
// //         {
// //             // Arrange
// //             _fakeParserA.ShouldParseSucceed = true;
// //             _fakeParserA.ParseValue = "ValueA";
// //             _fakeParserB.ShouldParseSucceed = false;
// //             var oneOf = new OneOf<string, string, string>(_fakeParserA, _fakeParserB);
// //             var fakeContext = new TestHelpers.FakeParseContext();
// //             var result = new FakeParseResult<string>();
// // 
// //             // Act
// //             bool success = oneOf.Parse(fakeContext, ref result);
// // 
// //             // Assert
// //             Assert.True(success);
// //             Assert.Equal("ValueA", result.ParsedValue);
// //             // Ensure that the parser entered and exited are recorded
// //             Assert.Contains(oneOf, fakeContext.EnteredParsers);
// //             Assert.Contains(oneOf, fakeContext.ExitedParsers);
// //         }
// // 
// //         /// <summary>
// //         /// Tests that the Parse method returns true and sets the result when the first parser fails but the second parser succeeds.
// //         /// </summary>
// //         [Fact] [Error] (225-59)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (225-73)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (230-40)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.TestHelpers.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (230-57)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<string>' to 'ref Parlot.ParseResult<string>'
// //         public void Parse_WhenParserAFailsButParserBSucceeds_ReturnsTrueAndSetsResult()
// //         {
// //             // Arrange
// //             _fakeParserA.ShouldParseSucceed = false;
// //             _fakeParserB.ShouldParseSucceed = true;
// //             _fakeParserB.ParseValue = "ValueB";
// //             var oneOf = new OneOf<string, string, string>(_fakeParserA, _fakeParserB);
// //             var fakeContext = new TestHelpers.FakeParseContext();
// //             var result = new FakeParseResult<string>();
// // 
// //             // Act
// //             bool success = oneOf.Parse(fakeContext, ref result);
// // 
// //             // Assert
// //             Assert.True(success);
// //             Assert.Equal("ValueB", result.ParsedValue);
// //             Assert.Contains(oneOf, fakeContext.EnteredParsers);
// //             Assert.Contains(oneOf, fakeContext.ExitedParsers);
// //         }
// // 
// //         /// <summary>
// //         /// Tests that the Parse method returns false when both parsers fail.
// //         /// </summary>
// //         [Fact] [Error] (248-59)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (248-73)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (253-40)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.TestHelpers.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (253-57)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<string>' to 'ref Parlot.ParseResult<string>'
// //         public void Parse_WhenBothParsersFail_ReturnsFalse()
// //         {
// //             // Arrange
// //             _fakeParserA.ShouldParseSucceed = false;
// //             _fakeParserB.ShouldParseSucceed = false;
// //             var oneOf = new OneOf<string, string, string>(_fakeParserA, _fakeParserB);
// //             var fakeContext = new TestHelpers.FakeParseContext();
// //             var result = new FakeParseResult<string>();
// // 
// //             // Act
// //             bool success = oneOf.Parse(fakeContext, ref result);
// // 
// //             // Assert
// //             Assert.False(success);
// //             Assert.Contains(oneOf, fakeContext.EnteredParsers);
// //             Assert.Contains(oneOf, fakeContext.ExitedParsers);
// //         }
// // 
// //         /// <summary>
// //         /// Tests that the ToString method returns a string combining the string representations of both parsers.
// //         /// </summary>
// //         [Fact] [Error] (270-59)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (270-73)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>'
// //         public void ToString_ReturnsCombinedParserToString()
// //         {
// //             // Arrange
// //             _fakeParserA.CustomToString = "Alpha";
// //             _fakeParserB.CustomToString = "Beta";
// //             var oneOf = new OneOf<string, string, string>(_fakeParserA, _fakeParserB);
// // 
// //             // Act
// //             string result = oneOf.ToString();
// // 
// //             // Assert
// //             Assert.Equal("Alpha | Beta", result);
// //         }
// // 
// //         /// <summary>
// //         /// Tests that the Compile method returns a compilation result with a non-empty body.
// //         /// </summary>
// //         [Fact] [Error] (290-59)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (290-73)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.FakeParser<string>' to 'Parlot.Fluent.Parser<string>' [Error] (296-51)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.TestHelpers.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (306-58)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.TestHelpers.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
// //         public void Compile_ReturnsCompilationResultContainingExpectedBlock()
// //         {
// //             // Arrange
// //             _fakeParserA.BuildSuccessOutcome = true;
// //             _fakeParserA.BuildValueOutcome = "CompiledA";
// //             _fakeParserB.BuildSuccessOutcome = false;
// //             _fakeParserB.BuildValueOutcome = "CompiledB";
// //             var oneOf = new OneOf<string, string, string>(_fakeParserA, _fakeParserB);
// // 
// //             // Test for non-discarded result
// //             var fakeCompilationContext = new TestHelpers.FakeCompilationContext { DiscardResult = false };
// // 
// //             // Act
// //             var compilationResult = oneOf.Compile(fakeCompilationContext) as TestHelpers.FakeCompilationResult<string>;
// // 
// //             // Assert
// //             Assert.NotNull(compilationResult);
// //             Assert.NotEmpty(compilationResult.Body);
// //             // We expect that the block expression was added by the Compile method.
// //             Assert.Single(compilationResult.Body, b => b.NodeType == ExpressionType.Block);
// // 
// //             // Test for discarded result scenario
// //             fakeCompilationContext.DiscardResult = true;
// //             var compilationResultDiscard = oneOf.Compile(fakeCompilationContext) as TestHelpers.FakeCompilationResult<string>;
// //             Assert.NotNull(compilationResultDiscard);
// //             Assert.NotEmpty(compilationResultDiscard.Body);
// //             Assert.Single(compilationResultDiscard.Body, b => b.NodeType == ExpressionType.Block);
// //         }
// //     }
// 
//     #region Minimal Abstract Base Class Implementations for Testing
// 
//     // Minimal implementations to allow testing. In an actual project, these would be part of the library.
// //     public abstract class ParseContext [Error] (316-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ParseContext'
// //     {
// //         public abstract void EnterParser(object parser); [Error] (318-30)CS0111 Type 'ParseContext' already defines a member called 'EnterParser' with the same parameter types
// //         public abstract void ExitParser(object parser); [Error] (319-30)CS0111 Type 'ParseContext' already defines a member called 'ExitParser' with the same parameter types
// //     }
// 
// //     public abstract class ParseResult<T> [Error] (322-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ParseResult'
// //     {
// //         public abstract void Set(int start, int end, T value); [Error] (324-30)CS0111 Type 'ParseResult<T>' already defines a member called 'Set' with the same parameter types
// //     }
// 
// //     public abstract class CompilationContext [Error] (327-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'CompilationContext'
// //     {
// //         public bool DiscardResult { get; set; }
// //         public abstract CompilationResult CreateCompilationResult<T>(); [Error] (330-43)CS0111 Type 'CompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types
// //     }
// 
// //     public abstract class CompilationResult [Error] (333-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'CompilationResult'
// //     {
// //     }
// 
//     #endregion
// }
