// using Moq;
// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Xunit;
// using Parlot.Fluent;
// using Parlot.Compilation;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the Deferred&lt;T&gt; class.
//     /// </summary>
//     public class DeferredTests
//     {
//         // Fake implementations to support testing
//         
//         /// <summary>
//         /// A fake parser for int that implements ISeekable.
//         /// </summary>
// //         private class FakeParserInt : Parser<int>, ISeekable [Error] (21-23)CS0534 'DeferredTests.FakeParserInt' does not implement inherited abstract member 'Parser<int>.Parse(object, ref ParseResult<int>)' [Error] (21-23)CS0534 'DeferredTests.FakeParserInt' does not implement inherited abstract member 'Parser<int>.Parse(ParseContext, ref ParseResult<int>)' [Error] (21-23)CS0534 'DeferredTests.FakeParserInt' does not implement inherited abstract member 'Parser<int>.Parse(ParseContext, ref ParseResult<int>)' [Error] (21-23)CS0534 'DeferredTests.FakeParserInt' does not implement inherited abstract member 'Parser<int>.Build(CompilationContext, bool)' [Error] (21-23)CS0534 'DeferredTests.FakeParserInt' does not implement inherited abstract member 'Parser<int>.Compile(CompilationContext)'
// //         {
// //             public bool CanSeek { get; }
// //             public char[] ExpectedChars { get; }
// //             public bool SkipWhitespace { get; }
// // 
// //             private readonly bool _parseResult;
// //             private readonly int _value;
// // 
// //             public FakeParserInt(bool canSeek = false, char[] expectedChars = null, bool skipWhitespace = false, bool parseResult = true, int value = 42)
// //             {
// //                 CanSeek = canSeek;
// //                 ExpectedChars = expectedChars ?? Array.Empty<char>();
// //                 SkipWhitespace = skipWhitespace;
// //                 _parseResult = parseResult;
// //                 _value = value;
// //             }
// // 
// //             public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (39-34)CS0462 The inherited members 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' have the same signature in type 'DeferredTests.FakeParserInt', so they cannot be overridden [Error] (42-24)CS0229 Ambiguity between 'ParseResult<int>.Success' and 'ParseResult<int>.Success' [Error] (43-24)CS0229 Ambiguity between 'ParseResult<int>.Value' and 'ParseResult<int>.Value'
// //             {
// //                 // Simulate entering and exiting parser in context.
// //                 result.Success = _parseResult;
// //                 result.Value = _value;
// //                 return _parseResult;
// //             }
// 
//             public override object Build(CompilationContext context)
//             {
//                 // Return a fake compilation result expected by Deferred.Compile.
//                 return new FakeCompilationResult<int>();
//             }
// 
//             public override string ToString()
//             {
//                 return "FakeParserInt";
//             }
//         }
// 
//         /// <summary>
//         /// A fake compilation result that provides the necessary members for Deferred.Compile.
//         /// </summary>
//         private class FakeCompilationResult<T>
//         {
//             public List<ParameterExpression> Variables { get; set; } = new List<ParameterExpression>();
//             public List<Expression> Body { get; set; } = new List<Expression>();
//             public Expression Success { get; set; }
//             public Expression Value { get; set; }
// 
//             public FakeCompilationResult()
//             {
//                 Success = Expression.Constant(true);
//                 Value = Expression.Constant(default(T), typeof(T));
//             }
//         }
// 
//         /// <summary>
//         /// A fake compilation context with minimal implementation for testing.
//         /// </summary>
// //         private class FakeCompilationContext : CompilationContext [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ResetPosition(ParameterExpression)' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.Buffer()' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.SkipWhiteSpaceOrNewLine()' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ReadChar(char)' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.ParseContext.get' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.set' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.NewTextSpan(Expression, Expression, Expression)' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.CreateCompilationResult<T>()' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DiscardResult.get' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DeclarePositionVariable(CompilationResult)' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.DeclareOffsetVariable(CompilationResult)' [Error] (79-23)CS0534 'DeferredTests.FakeCompilationContext' does not implement inherited abstract member 'CompilationContext.SkipWhiteSpace()'
// //         {
// //             private int _nextNumber = 1;
// //             public override int NextNumber => _nextNumber++;
// // 
// //             public override bool DiscardResult { get; } = false; [Error] (84-34)CS8080 Auto-implemented properties must override all accessors of the overridden property.
// // 
// //             private readonly ParameterExpression _parseContext = Expression.Parameter(typeof(object), "parseContext");
// //             public override ParameterExpression ParseContext => _parseContext;
// // 
// // #if DEBUG
// //             public override List<LambdaExpression> Lambdas { get; } = new List<LambdaExpression>();
// // #endif
// // 
// //             public override CompilationResult<T> CreateCompilationResult<T>()
// //             {
// //                 return new FakeCompilationResultWrapper<T>();
// //             }
// //         }
// 
//         /// <summary>
//         /// A fake wrapper for CompilationResult to be returned by FakeCompilationContext.
//         /// </summary>
//         private class FakeCompilationResultWrapper<T> : CompilationResult<T>
//         {
//             public override List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
// //             public override List<Expression> Body { get; } = new List<Expression>(); [Error] (105-46)CS0462 The inherited members 'CompilationResult<T>.Body' and 'CompilationResult<T>.Body' have the same signature in type 'DeferredTests.FakeCompilationResultWrapper<T>', so they cannot be overridden
// //             public override ParameterExpression Success { get; } = Expression.Variable(typeof(bool), "success"); [Error] (106-49)CS0462 The inherited members 'CompilationResult<T>.Success' and 'CompilationResult<T>.Success' have the same signature in type 'DeferredTests.FakeCompilationResultWrapper<T>', so they cannot be overridden
// //             public override ParameterExpression Value { get; } = Expression.Variable(typeof(T), "value"); [Error] (107-49)CS0462 The inherited members 'CompilationResult<T>.Value' and 'CompilationResult<T>.Value' have the same signature in type 'DeferredTests.FakeCompilationResultWrapper<T>', so they cannot be overridden
//         }
// 
//         /// <summary>
//         /// A fake parse context with minimal implementation for testing.
//         /// </summary>
// //         private class FakeParseContext : ParseContext [Error] (113-23)CS0534 'DeferredTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(Parser)' [Error] (113-23)CS0534 'DeferredTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(object)' [Error] (113-23)CS0534 'DeferredTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(Parser)' [Error] (113-23)CS0534 'DeferredTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(object)' [Error] (113-23)CS0534 'DeferredTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.EnterParser(Parser)' [Error] (113-23)CS0534 'DeferredTests.FakeParseContext' does not implement inherited abstract member 'ParseContext.ExitParser(Parser)'
// //         {
// //             public List<object> EnteredParsers { get; } = new List<object>();
// // 
// //             public override void EnterParser(object parser)
// //             {
// //                 EnteredParsers.Add(parser);
// //             }
// // 
// //             public override void ExitParser(object parser)
// //             {
// //                 EnteredParsers.Remove(parser);
// //             }
// //         }
// 
//         /// <summary>
//         /// A fake parse result for int.
//         /// </summary>
//         private struct FakeParseResultInt
//         {
// //             public bool Success; [Error] (133-25)CS0649 Field 'DeferredTests.FakeParseResultInt.Success' is never assigned to, and will always have its default value false
// //             public int Value; [Error] (134-24)CS0649 Field 'DeferredTests.FakeParseResultInt.Value' is never assigned to, and will always have its default value 0
//         }
// 
//         /// <summary>
//         /// Tests that setting the Parser property to null throws an ArgumentNullException.
//         /// </summary>
//         [Fact]
//         public void ParserProperty_SetNull_ThrowsArgumentNullException()
//         {
//             // Arrange
//             var deferred = new Deferred<int>();
// 
//             // Act & Assert
//             var exception = Assert.Throws<ArgumentNullException>(() => deferred.Parser = null);
//             Assert.Equal("value", exception.ParamName);
//         }
// 
//         /// <summary>
//         /// Tests that calling Parse without initializing the inner Parser throws an InvalidOperationException.
//         /// </summary>
// //         [Fact] [Error] (160-26)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<int>' [Error] (163-91)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.DeferredTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (163-104)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>'
// //         public void Parse_NoParserSet_ThrowsInvalidOperationException()
// //         {
// //             // Arrange
// //             var deferred = new Deferred<int>();
// //             var context = new FakeParseContext();
// //             var result = new ParseResult<int>();
// // 
// //             // Act & Assert
// //             var exception = Assert.Throws<InvalidOperationException>(() => deferred.Parse(context, ref result));
// //             Assert.Equal("Parser has not been initialized", exception.Message);
// //         }
// 
//         /// <summary>
//         /// Tests that Parse invokes the inner parser and returns the expected outcome.
//         /// </summary>
// //         [Fact] [Error] (175-57)CS0029 Cannot implicitly convert type 'Parlot.Fluent.UnitTests.DeferredTests.FakeParserInt' to 'Parlot.Fluent.Parser<int>' [Error] (177-26)CS0144 Cannot create an instance of the abstract type or interface 'ParseResult<int>' [Error] (180-43)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.DeferredTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (180-56)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>' [Error] (184-32)CS0229 Ambiguity between 'ParseResult<int>.Success' and 'ParseResult<int>.Success' [Error] (185-38)CS0229 Ambiguity between 'ParseResult<int>.Value' and 'ParseResult<int>.Value'
// //         public void Parse_WithValidParser_ReturnsExpectedOutcome()
// //         {
// //             // Arrange
// //             var fakeParser = new FakeParserInt(parseResult: true, value: 100);
// //             var deferred = new Deferred<int> { Parser = fakeParser };
// //             var context = new FakeParseContext();
// //             var result = new ParseResult<int>();
// // 
// //             // Act
// //             bool outcome = deferred.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.True(outcome);
// //             Assert.True(result.Success);
// //             Assert.Equal(100, result.Value);
// //             // Verify that context methods were called
// //             Assert.DoesNotContain(deferred, context.EnteredParsers);
// //         }
// 
//         /// <summary>
//         /// Tests that the constructor with delegate initializes the inner Parser and ISeekable properties.
//         /// </summary>
// //         [Fact] [Error] (203-46)CS1503 Argument 1: cannot convert from 'System.Func<Parlot.Fluent.Deferred<int>, Parlot.Fluent.UnitTests.Parser<int>>' to 'System.Func<Parlot.Fluent.Deferred<int>, Parlot.Fluent.Parser<int>>'
// //         public void ConstructorWithDelegate_InitializesParserAndSeekableProperties()
// //         {
// //             // Arrange
// //             char[] expectedChars = new[] { 'a', 'b', 'c' };
// //             bool canSeek = true;
// //             bool skipWhitespace = true;
// //             Func<Deferred<int>, Parser<int>> factory = d => new FakeParserInt(canSeek, expectedChars, skipWhitespace);
// // 
// //             // Act
// //             var deferred = new Deferred<int>(factory);
// // 
// //             // Assert
// //             Assert.NotNull(deferred.Parser);
// //             // Since the inner parser implements ISeekable, ensure properties are propagated.
// //             Assert.Equal(canSeek, deferred.CanSeek);
// //             Assert.Equal(expectedChars, deferred.ExpectedChars);
// //             Assert.Equal(skipWhitespace, deferred.SkipWhitespace);
// //         }
// 
//         /// <summary>
//         /// Tests that calling Compile without initializing the inner Parser throws an InvalidOperationException.
//         /// </summary>
// //         [Fact] [Error] (224-93)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.DeferredTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
// //         public void Compile_NoParserSet_ThrowsInvalidOperationException()
// //         {
// //             // Arrange
// //             var deferred = new Deferred<int>();
// //             var compilationContext = new FakeCompilationContext();
// // 
// //             // Act & Assert
// //             var exception = Assert.Throws<InvalidOperationException>(() => deferred.Compile(compilationContext));
// //             Assert.Equal("Can't compile a Deferred Parser until it is fully initialized", exception.Message);
// //         }
// 
//         /// <summary>
//         /// Tests that Compile returns a valid CompilationResult when the inner Parser is set.
//         /// </summary>
// //         [Fact] [Error] (236-57)CS0029 Cannot implicitly convert type 'Parlot.Fluent.UnitTests.DeferredTests.FakeParserInt' to 'Parlot.Fluent.Parser<int>' [Error] (240-43)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.DeferredTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
// //         public void Compile_WithValidParser_ReturnsCompilationResult()
// //         {
// //             // Arrange
// //             var fakeParser = new FakeParserInt();
// //             var deferred = new Deferred<int> { Parser = fakeParser };
// //             var compilationContext = new FakeCompilationContext();
// // 
// //             // Act
// //             var result = deferred.Compile(compilationContext);
// // 
// //             // Assert
// //             Assert.NotNull(result);
// //             // Verify that variables and body were added
// //             Assert.NotEmpty(result.Variables);
// //             Assert.NotEmpty(result.Body);
// //             // Check that result contains the success and value variable assignments.
// //             Assert.NotNull(result.Success);
// //             Assert.NotNull(result.Value);
// //         }
// 
//         /// <summary>
//         /// Tests that ToString returns a string containing 'Deferred' and handles potential recursion.
//         /// </summary>
// //         [Fact] [Error] (260-57)CS0029 Cannot implicitly convert type 'Parlot.Fluent.UnitTests.DeferredTests.FakeParserInt' to 'Parlot.Fluent.Parser<int>'
// //         public void ToString_ReturnsExpectedString()
// //         {
// //             // Arrange
// //             var fakeParser = new FakeParserInt();
// //             var deferred = new Deferred<int> { Parser = fakeParser };
// // 
// //             // Act
// //             string result = deferred.ToString();
// // 
// //             // Assert
// //             Assert.Contains("Deferred", result);
// //             // If Name is not set, it should include the inner parser's ToString.
// //             Assert.Contains(fakeParser.ToString(), result);
// //         }
//     }
// 
//     // Dummy implementations to simulate the required external types for testing purposes
//     
// //     public abstract class Parser<T> [Error] (274-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'Parser'
// //     {
// //         public string Name { get; set; }
// //         public abstract bool Parse(ParseContext context, ref ParseResult<T> result); [Error] (277-30)CS0111 Type 'Parser<T>' already defines a member called 'Parse' with the same parameter types
// //         public abstract object Build(CompilationContext context);
// //     }
// 
//     public interface ICompilable
//     {
//         CompilationResult Compile(CompilationContext context);
//     }
// 
//     public interface ISeekable
//     {
//         bool CanSeek { get; }
//         char[] ExpectedChars { get; }
//         bool SkipWhitespace { get; }
//     }
// 
// //     public class ParseContext [Error] (293-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ParseContext'
// //     {
// //         public virtual void EnterParser(object parser) { }
// //         public virtual void ExitParser(object parser) { }
// //     }
// 
// //     public class ParseResult<T> [Error] (299-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ParseResult'
// //     {
// //         public bool Success { get; set; }
// //         public T Value { get; set; }
// //     }
// 
// //     public abstract class CompilationContext [Error] (305-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'CompilationContext'
// //     {
// //         public abstract int NextNumber { get; }
// //         public abstract bool DiscardResult { get; }
// //         public abstract ParameterExpression ParseContext { get; }
// // #if DEBUG
// //         public abstract List<LambdaExpression> Lambdas { get; }
// // #endif
// //         public abstract CompilationResult<T> CreateCompilationResult<T>(); [Error] (313-46)CS0111 Type 'CompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types
// //     }
// 
//     public abstract class CompilationResult
//     {
//     }
// 
// //     public abstract class CompilationResult<T> : CompilationResult [Error] (320-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'CompilationResult'
// //     {
// //         public abstract List<ParameterExpression> Variables { get; } [Error] (322-51)CS0108 'CompilationResult<T>.Variables' hides inherited member 'CompilationResult.Variables'. Use the new keyword if hiding was intended.
// //         public abstract List<Expression> Body { get; } [Error] (323-42)CS0108 'CompilationResult<T>.Body' hides inherited member 'CompilationResult.Body'. Use the new keyword if hiding was intended.
// //         public abstract ParameterExpression Success { get; } [Error] (324-45)CS0108 'CompilationResult<T>.Success' hides inherited member 'CompilationResult.Success'. Use the new keyword if hiding was intended.
// //         public abstract ParameterExpression Value { get; } [Error] (325-45)CS0108 'CompilationResult<T>.Value' hides inherited member 'CompilationResult.Value'. Use the new keyword if hiding was intended.
// //     }
// }
