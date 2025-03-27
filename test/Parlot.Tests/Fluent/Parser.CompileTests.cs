// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Parser{T}"/> class focusing on the Compile method.
/// </summary>
// public class ParserTests [Error] (245-2)CS1513 } expected
// {
//     static ParserTests()
//     {
//         // Ensure the _valueTupleConstructor field is initialized.
//         // This field is private static readonly in Parser<T>; set it via reflection if it is null.
//         var parserType = typeof(Parser<string>);
//         var field = parserType.GetField("_valueTupleConstructor", BindingFlags.NonPublic | BindingFlags.Static);
//         if (field != null && field.GetValue(null) == null)
//         {
//             var ctor = typeof(ValueTuple<bool, string>).GetConstructor(new[] { typeof(bool), typeof(string) });
//             field.SetValue(null, ctor);
//         }
//     }
// 
//     /// <summary>
//     /// Tests that when the parser instance already implements ICompiledParser, Compile returns the same instance.
//     /// </summary>
//     [Fact]
//     public void Compile_WhenAlreadyCompiled_ReturnsSelf()
//     {
//         // Arrange
//         var alreadyCompiledParser = new FakeCompiledParser();
//         // Act
//         Parser<string> compiledParser = alreadyCompiledParser.Compile();
//         // Assert
//         Assert.Same(alreadyCompiledParser, compiledParser);
//     }
// 
//     /// <summary>
//     /// Tests that when the parser instance is not compiled, Compile returns a new compiled parser,
//     /// which when invoked produces the expected result.
//     /// </summary>
//     [Fact]
//     public void Compile_WhenNotCompiled_ReturnsCompiledParser()
//     {
//         // Arrange
//         var nonCompiledParser = new FakeNonCompiledParser();
//         // Act
//         Parser<string> compiledParser = nonCompiledParser.Compile();
//         // Assert
//         // The returned instance should not be the same as the original non-compiled parser.
//         Assert.NotSame(nonCompiledParser, compiledParser);
//         // Also, the returned instance should implement the ICompiledParser marker interface.
//         Assert.IsAssignableFrom<ICompiledParser>(compiledParser);
//         // Invoke the compiled delegate by using reflection to get the underlying lambda.
//         // Assuming that the compiled parser wraps a delegate that can be invoked.
//         // We use the Parse method on the compiled parser.
//         // Since the original Parser<T> class doesn't expose a public Parse method,
//         // we simulate its execution by using the delegate inside the compiled parser.
//         // For testing we reflect to get the private field which holds the delegate.
//         var compiledParserType = compiledParser.GetType();
//         var fieldInfo = compiledParserType.GetField("_parser", BindingFlags.NonPublic | BindingFlags.Instance);
//         Assert.NotNull(fieldInfo);
//         var del = fieldInfo.GetValue(compiledParser) as Func<ParseContext, (bool, string)>;
//         Assert.NotNull(del);
//         // Create a dummy ParseContext instance.
//         var parseContext = new FakeParseContext();
//         (bool success, string value) = del(parseContext);
//         Assert.True(success);
//         Assert.Equal("expected", value);
//     }
// 
// #region Fake and Helper Classes
//     /// <summary>
//     /// Marker interface indicating that a parser is compiled.
//     /// </summary>
//     public interface ICompiledParser
//     {
//     }
// 
//     /// <summary>
//     /// A fake compiled parser that simulates an already compiled parser.
//     /// </summary>
//     private class FakeCompiledParser : Parser<string>, ICompiledParser [Error] (88-19)CS0534 'ParserTests.FakeCompiledParser' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)'
//     {
//         // The Build method is never called when the parser is already compiled.
//         public override CompilationResult Build(CompilationContext context, bool requireResult = false) [Error] (91-43)CS0506 'ParserTests.FakeCompiledParser.Build(CompilationContext, bool)': cannot override inherited member 'Parser<string>.Build(CompilationContext, bool)' because it is not marked virtual, abstract, or override
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// A fake non-compiled parser that overrides Build to provide a minimal compilation result.
//     /// </summary>
//     private class FakeNonCompiledParser : Parser<string> [Error] (100-19)CS0534 'ParserTests.FakeNonCompiledParser' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)'
//     {
//         /// <summary>
//         /// Overrides Build to return a fake compilation result that yields (true, "expected").
//         /// </summary>
//         /// <param name = "context">The compilation context.</param>
//         /// <param name = "requireResult">Flag indicating whether a result is required.</param>
//         /// <returns>A fake <see cref = "CompilationResult"/>.</returns>
//         public override CompilationResult Build(CompilationContext context, bool requireResult = false) [Error] (108-43)CS0506 'ParserTests.FakeNonCompiledParser.Build(CompilationContext, bool)': cannot override inherited member 'Parser<string>.Build(CompilationContext, bool)' because it is not marked virtual, abstract, or override
//         {
//             // Create a fake compilation result with minimal expressions.
//             var fakeResult = new FakeCompilationResult
//             {
//                 Success = Expression.Constant(true),
//                 Value = Expression.Constant("expected")
//             };
//             return fakeResult;
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of <see cref = "CompilationResult"/> for testing purposes.
//     /// </summary>
//     private class FakeCompilationResult : CompilationResult [Error] (126-16)CS0122 'CompilationResult.CompilationResult()' is inaccessible due to its protection level
//     {
//         // Use lists for Variables and Body as required by the Compile method.
//         public FakeCompilationResult()
//         {
//             Variables = new List<ParameterExpression>();
//             Body = new List<Expression>();
//         }
// 
//         public override List<ParameterExpression> Variables { get; } [Error] (132-51)CS0506 'ParserTests.FakeCompilationResult.Variables': cannot override inherited member 'CompilationResult.Variables' because it is not marked virtual, abstract, or override
//         public override List<Expression> Body { get; } [Error] (133-42)CS0506 'ParserTests.FakeCompilationResult.Body': cannot override inherited member 'CompilationResult.Body' because it is not marked virtual, abstract, or override
//         public override Expression Success { get; set; } [Error] (134-36)CS0506 'ParserTests.FakeCompilationResult.Success': cannot override inherited member 'CompilationResult.Success' because it is not marked virtual, abstract, or override
//         public override Expression Value { get; set; } [Error] (135-36)CS0506 'ParserTests.FakeCompilationResult.Value': cannot override inherited member 'CompilationResult.Value' because it is not marked virtual, abstract, or override
//     }
// 
//     /// <summary>
//     /// A fake implementation of <see cref = "CompilationContext"/> for testing purposes.
//     /// </summary>
//     private class FakeCompilationContext : CompilationContext [Error] (145-13)CS0200 Property or indexer 'CompilationContext.NextNumber' cannot be assigned to -- it is read only [Error] (146-13)CS0200 Property or indexer 'CompilationContext.GlobalVariables' cannot be assigned to -- it is read only [Error] (147-13)CS0200 Property or indexer 'CompilationContext.GlobalExpressions' cannot be assigned to -- it is read only [Error] (148-13)CS0200 Property or indexer 'CompilationContext.ParseContext' cannot be assigned to -- it is read only
//     {
//         public FakeCompilationContext()
//         {
//             NextNumber = 1;
//             GlobalVariables = new List<ParameterExpression>();
//             GlobalExpressions = new List<Expression>();
//             ParseContext = Expression.Parameter(typeof(FakeParseContext), "parseContext");
//         }
//     }
// 
//     /// <summary>
//     /// A fake ParseContext class for testing purposes.
//     /// </summary>
//     public class FakeParseContext : ParseContext [Error] (155-18)CS7036 There is no argument given that corresponds to the required parameter 'scanner' of 'ParseContext.ParseContext(Scanner, bool)'
//     {
//     }
// 
//     public bool DiscardResult { get; set; }
// 
//     /// <summary>
//     /// Tests that BuildAsNonCompilableParser creates a CompilationResult with proper variables and body when DiscardResult is false.
//     /// The test uses a FakeCompilationContext with DiscardResult set to false so that both the parse result variable and the value variable are added.
//     /// Expected outcome: The returned CompilationResult contains two variables, two expressions in the body, and a non-null Value property.
//     /// </summary>
//     [Fact] [Error] (172-13)CS0200 Property or indexer 'CompilationContext.NextNumber' cannot be assigned to -- it is read only [Error] (174-13)CS0200 Property or indexer 'CompilationContext.ParseContext' cannot be assigned to -- it is read only [Error] (174-32)CS7036 There is no argument given that corresponds to the required parameter 'scanner' of 'ParseContext.ParseContext(Scanner, bool)' [Error] (176-26)CS0246 The type or namespace name 'FakeParser' could not be found (are you missing a using directive or an assembly reference?) [Error] (182-35)CS0308 The non-generic type 'CompilationResult' cannot be used with type arguments
//     public void BuildAsNonCompilableParser_DiscardResultFalse_ReturnsCompiledResultWithValue()
//     {
//         // Arrange
//         var fakeCompilationContext = new FakeCompilationContext
//         {
//             NextNumber = 1,
//             DiscardResult = false,
//             ParseContext = new ParseContext()
//         };
//         var parser = new FakeParser();
//         MethodInfo method = typeof(Parser<int>).GetMethod("BuildAsNonCompilableParser", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(method);
//         // Act
//         var resultObj = method.Invoke(parser, new object[] { fakeCompilationContext });
//         Assert.NotNull(resultObj);
//         var result = resultObj as CompilationResult<int>;
//         Assert.NotNull(result);
//         // Assert
//         // Expect two variables: one for parseResult and one for value (since DiscardResult is false)
//         Assert.Equal(2, result.Variables.Count);
//         // Expect two expressions in the body: one for assignment of success and one for the IfThen block
//         Assert.Equal(2, result.Body.Count);
//         // Value should be set when result is not discarded.
//         Assert.NotNull(result.Value);
//         // Check that the variable names follow the naming convention based on NextNumber.
//         Assert.Equal("value1", result.Variables[0].Name);
//         Assert.Equal("value1", ((ParameterExpression)result.Value).Name);
//     }
// 
//     /// <summary>
//     /// Tests that BuildAsNonCompilableParser creates a CompilationResult without the value variable when DiscardResult is true.
//     /// The test uses a FakeCompilationContext with DiscardResult set to true so that only the parse result variable is added.
//     /// Expected outcome: The returned CompilationResult contains one variable, one expression in the body, and a null Value property.
//     /// </summary>
//     [Fact] [Error] (207-13)CS0200 Property or indexer 'CompilationContext.NextNumber' cannot be assigned to -- it is read only [Error] (209-13)CS0200 Property or indexer 'CompilationContext.ParseContext' cannot be assigned to -- it is read only [Error] (209-32)CS7036 There is no argument given that corresponds to the required parameter 'scanner' of 'ParseContext.ParseContext(Scanner, bool)' [Error] (211-26)CS0246 The type or namespace name 'FakeParser' could not be found (are you missing a using directive or an assembly reference?) [Error] (217-35)CS0308 The non-generic type 'CompilationResult' cannot be used with type arguments
//     public void BuildAsNonCompilableParser_DiscardResultTrue_ReturnsCompiledResultWithoutValue()
//     {
//         // Arrange
//         var fakeCompilationContext = new FakeCompilationContext
//         {
//             NextNumber = 2,
//             DiscardResult = true,
//             ParseContext = new ParseContext()
//         };
//         var parser = new FakeParser();
//         MethodInfo method = typeof(Parser<int>).GetMethod("BuildAsNonCompilableParser", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(method);
//         // Act
//         var resultObj = method.Invoke(parser, new object[] { fakeCompilationContext });
//         Assert.NotNull(resultObj);
//         var result = resultObj as CompilationResult<int>;
//         Assert.NotNull(result);
//         // Assert
//         // Expect one variable: only the parseResult variable should be added
//         Assert.Single(result.Variables);
//         // Expect one expression in the body: only the assignment of success is added
//         Assert.Single(result.Body);
//         // Since DiscardResult is true, the Value property should remain null.
//         Assert.Null(result.Value);
//         // Check that the variable name follows expected naming based on NextNumber.
//         Assert.Equal("value2", result.Variables[0].Name);
//     }
// 
//     /// <summary>
//     /// Tests that BuildAsNonCompilableParser throws a TargetInvocationException when a null CompilationContext is provided.
//     /// The exception is expected due to a null reference being used within the method.
//     /// </summary>
//     [Fact] [Error] (238-26)CS0246 The type or namespace name 'FakeParser' could not be found (are you missing a using directive or an assembly reference?)
//     public void BuildAsNonCompilableParser_NullCompilationContext_ThrowsException()
//     {
//         // Arrange
//         var parser = new FakeParser();
//         MethodInfo method = typeof(Parser<int>).GetMethod("BuildAsNonCompilableParser", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(method);
//         // Act & Assert
//         Assert.Throws<TargetInvocationException>(() => method.Invoke(parser, new object[] { null }));
//     }
// #endregion
// }