// using Moq;
// using Parlot.Fluent.UnitTests;
// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Xunit;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the <see cref = "If{C, S, T}"/> class.
//     /// </summary>
//     public class IfTests
//     {
//         /// <summary>
//         /// Tests that the constructor throws an ArgumentNullException when the predicate is null.
//         /// </summary>
// //         [Fact] [Error] (22-51)CS1503 Argument 1: cannot convert from 'bool' to 'System.Func<Parlot.Fluent.UnitTests.IfTests.DummyParseContext, Parlot.Fluent.UnitTests.IfTests.DummyParseResult<int>, bool>' [Error] (25-79)CS0311 The type 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'If<C, S, T>'. There is no implicit reference conversion from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'DummyParseContext'. [Error] (25-111)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.FakeParser<int>' to 'Parlot.Fluent.UnitTests.Parser<int>'
// //         public void Constructor_NullPredicate_ThrowsArgumentNullException()
// //         {
// //             // Arrange
// //             var dummyParser = new FakeParser<int>(true, default);
// //             Func<DummyParseContext, object, bool> nullPredicate = null;
// //             // Act & Assert
// //             var exception = Assert.Throws<ArgumentNullException>(() => new If<DummyParseContext, object, int>(dummyParser, nullPredicate, null));
// //             Assert.Equal("predicate", exception.ParamName);
// //         }
// 
//         /// <summary>
//         /// Tests that the constructor throws an ArgumentNullException when the parser is null.
//         /// </summary>
// //         [Fact] [Error] (38-79)CS0311 The type 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'If<C, S, T>'. There is no implicit reference conversion from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'DummyParseContext'.
// //         public void Constructor_NullParser_ThrowsArgumentNullException()
// //         {
// //             // Arrange
// //             Func<DummyParseContext, object, bool> predicate = (ctx, state) => true;
// //             // Act & Assert
// //             var exception = Assert.Throws<ArgumentNullException>(() => new If<DummyParseContext, object, int>(null, predicate, null));
// //             Assert.Equal("parser", exception.ParamName);
// //         }
// 
//         /// <summary>
//         /// Tests that Parse does not invoke the inner parser when the predicate returns false.
//         /// </summary>
// //         [Fact] [Error] (51-78)CS0748 Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit [Error] (60-35)CS0311 The type 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'If<C, S, T>'. There is no implicit reference conversion from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'DummyParseContext'. [Error] (60-67)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.FakeParser<int>' to 'Parlot.Fluent.UnitTests.Parser<int>' [Error] (64-49)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'Parlot.Fluent.UnitTests.ParseContext' [Error] (64-67)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.IfTests.DummyParseResult<int>' to 'ref Parlot.Fluent.UnitTests.ParseResult<int>'
// //         public void Parse_PredicateReturnsFalse_DoesNotInvokeInnerParser_ReturnsFalse()
// //         {
// //             // Arrange
// //             bool innerParserCalled = false;
// //             // Dummy parser that sets flag if Parse is called.
// //             var dummyParser = new FakeParser<int>(parseResult: (context, ref DummyParseResult<int> res) =>
// //             {
// //                 innerParserCalled = true;
// //                 res.Success = true;
// //                 res.Value = 42;
// //                 return true;
// //             }, buildResult: CreateFakeBuildResult<int>);
// //             // Predicate returns false.
// //             Func<DummyParseContext, object, bool> predicate = (ctx, state) => false;
// //             var ifParser = new If<DummyParseContext, object, int>(dummyParser, predicate, "dummyState");
// //             var dummyContext = new DummyParseContext();
// //             var result = new DummyParseResult<int>();
// //             // Act
// //             bool returnedValid = ifParser.Parse(dummyContext, ref result);
// //             // Assert
// //             Assert.False(returnedValid);
// //             Assert.False(innerParserCalled);
// //         }
// 
//         /// <summary>
//         /// Tests that Parse invokes the inner parser when the predicate returns true and the inner parser succeeds.
//         /// It should return true and not reset the cursor position.
//         /// </summary>
// //         [Fact] [Error] (79-78)CS0748 Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit [Error] (87-35)CS0311 The type 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'If<C, S, T>'. There is no implicit reference conversion from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'DummyParseContext'. [Error] (87-67)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.FakeParser<int>' to 'Parlot.Fluent.UnitTests.Parser<int>' [Error] (93-49)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'Parlot.Fluent.UnitTests.ParseContext' [Error] (93-67)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.IfTests.DummyParseResult<int>' to 'ref Parlot.Fluent.UnitTests.ParseResult<int>'
// //         public void Parse_PredicateReturnsTrue_InnerParserSucceeds_DoesNotResetCursor_ReturnsTrue()
// //         {
// //             // Arrange
// //             bool innerParserCalled = false;
// //             var dummyParser = new FakeParser<int>(parseResult: (context, ref DummyParseResult<int> res) =>
// //             {
// //                 innerParserCalled = true;
// //                 res.Success = true;
// //                 res.Value = 100;
// //                 return true;
// //             }, buildResult: CreateFakeBuildResult<int>);
// //             Func<DummyParseContext, object, bool> predicate = (ctx, state) => true;
// //             var ifParser = new If<DummyParseContext, object, int>(dummyParser, predicate, "stateValue");
// //             var dummyContext = new DummyParseContext();
// //             // Set initial position.
// //             dummyContext.Scanner.Cursor.Position = 10;
// //             var result = new DummyParseResult<int>();
// //             // Act
// //             bool returnedValid = ifParser.Parse(dummyContext, ref result);
// //             // Assert
// //             Assert.True(returnedValid);
// //             Assert.True(innerParserCalled);
// //             // Since parser succeeded, cursor position should remain unchanged.
// //             Assert.Equal(10, dummyContext.Scanner.Cursor.Position);
// //             Assert.True(result.Success);
// //             Assert.Equal(100, result.Value);
// //         }
// 
//         /// <summary>
//         /// Tests that Parse invokes the inner parser when the predicate returns true but the inner parser fails.
//         /// It should reset the cursor position.
//         /// </summary>
// //         [Fact] [Error] (112-78)CS0748 Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit [Error] (120-35)CS0311 The type 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'If<C, S, T>'. There is no implicit reference conversion from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'DummyParseContext'. [Error] (120-67)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.FakeParser<int>' to 'Parlot.Fluent.UnitTests.Parser<int>' [Error] (125-49)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'Parlot.Fluent.UnitTests.ParseContext' [Error] (125-67)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.IfTests.DummyParseResult<int>' to 'ref Parlot.Fluent.UnitTests.ParseResult<int>'
// //         public void Parse_PredicateReturnsTrue_InnerParserFails_ResetsCursor_ReturnsTrue()
// //         {
// //             // Arrange
// //             bool innerParserCalled = false;
// //             var dummyParser = new FakeParser<int>(parseResult: (context, ref DummyParseResult<int> res) =>
// //             {
// //                 innerParserCalled = true;
// //                 res.Success = false;
// //                 res.Value = 0;
// //                 return false;
// //             }, buildResult: CreateFakeBuildResult<int>);
// //             Func<DummyParseContext, object, bool> predicate = (ctx, state) => true;
// //             var ifParser = new If<DummyParseContext, object, int>(dummyParser, predicate, null);
// //             var dummyContext = new DummyParseContext();
// //             dummyContext.Scanner.Cursor.Position = 20;
// //             var result = new DummyParseResult<int>();
// //             // Act
// //             bool returnedValid = ifParser.Parse(dummyContext, ref result);
// //             // Assert
// //             Assert.True(returnedValid);
// //             Assert.True(innerParserCalled);
// //             // Since parser failed, cursor should be reset to the original position.
// //             Assert.Equal(20, dummyContext.Scanner.Cursor.Position);
// //             Assert.False(result.Success);
// //         }
// 
//         /// <summary>
//         /// Tests that the ToString method returns the inner parser's string representation appended with " (If)".
//         /// </summary>
// //         [Fact] [Error] (141-78)CS0748 Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit [Error] (148-35)CS0311 The type 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'If<C, S, T>'. There is no implicit reference conversion from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'DummyParseContext'. [Error] (148-67)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.FakeParser<int>' to 'Parlot.Fluent.UnitTests.Parser<int>'
// //         public void ToString_ReturnsInnerParserToStringAppendedWithIf()
// //         {
// //             // Arrange
// //             var dummyParser = new FakeParser<int>(parseResult: (context, ref DummyParseResult<int> res) =>
// //             {
// //                 res.Success = true;
// //                 res.Value = 5;
// //                 return true;
// //             }, buildResult: CreateFakeBuildResult<int>, toString: "FakeParserRepresentation");
// //             Func<DummyParseContext, object, bool> predicate = (ctx, state) => true;
// //             var ifParser = new If<DummyParseContext, object, int>(dummyParser, predicate, null);
// //             // Act
// //             string result = ifParser.ToString();
// //             // Assert
// //             Assert.Equal("FakeParserRepresentation (If)", result);
// //         }
// 
//         /// <summary>
//         /// Tests that Compile method builds a non-null CompilationResult containing expressions.
//         /// </summary>
// //         [Fact] [Error] (162-78)CS0748 Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit [Error] (169-35)CS0311 The type 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'If<C, S, T>'. There is no implicit reference conversion from 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' to 'DummyParseContext'. [Error] (169-67)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.FakeParser<int>' to 'Parlot.Fluent.UnitTests.Parser<int>' [Error] (172-54)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.IfTests.FakeCompilationContext' to 'Parlot.Fluent.UnitTests.CompilationContext' [Error] (175-46)CS0229 Ambiguity between 'CompilationResult.Body' and 'CompilationResult.Body' [Error] (176-47)CS0229 Ambiguity between 'CompilationResult.Body' and 'CompilationResult.Body'
// //         public void Compile_Always_ReturnsCompilationResultWithBody()
// //         {
// //             // Arrange
// //             var dummyParser = new FakeParser<int>(parseResult: (context, ref DummyParseResult<int> res) =>
// //             {
// //                 res.Success = true;
// //                 res.Value = 123;
// //                 return true;
// //             }, buildResult: CreateFakeBuildResult<int>);
// //             Func<DummyParseContext, object, bool> predicate = (ctx, state) => true;
// //             var ifParser = new If<DummyParseContext, object, int>(dummyParser, predicate, null);
// //             var fakeCompilationContext = new FakeCompilationContext(new DummyParseContext());
// //             // Act
// //             var compilationResult = ifParser.Compile(fakeCompilationContext);
// //             // Assert
// //             Assert.NotNull(compilationResult);
// //             Assert.NotNull(compilationResult.Body);
// //             Assert.NotEmpty(compilationResult.Body);
// //         }
// 
//         /// <summary>
//         /// Helper method to create a fake build result for the FakeParser.
//         /// </summary>
//         /// <typeparam name = "T">The type parameter for the compilation result.</typeparam>
//         /// <returns>A fake compilation result.</returns>
//         private static FakeCompilationResult<T> CreateFakeBuildResult<T>()
//         {
//             // Create dummy variables and expressions to simulate a compilation result.
//             var result = new FakeCompilationResult<T>
//             {
//                 Variables = new List<ParameterExpression>(),
//                 Body = new List<Expression>
//                 {
//                     // A dummy expression to simulate parser behavior.
//                     Expression.Constant(true)
//                 },
//                 Success = Expression.Constant(true),
//                 Value = Expression.Constant(default(T))
//             };
//             return result;
//         }
// 
// #region Dummy and Fake Classes for Testing
//         /// <summary>
//         /// Dummy implementation of a parse context.
//         /// </summary>
//         public class DummyParseContext
//         {
//             private readonly List<object> _enteredParsers = new List<object>();
//             private readonly List<object> _exitedParsers = new List<object>();
//             public DummyParseContext()
//             {
//                 Scanner = new DummyScanner();
//             }
// 
//             public DummyScanner Scanner { get; }
// 
//             public void EnterParser(object parser)
//             {
//                 _enteredParsers.Add(parser);
//             }
// 
//             public void ExitParser(object parser)
//             {
//                 _exitedParsers.Add(parser);
//             }
//         }
// 
//         /// <summary>
//         /// Dummy scanner holding a cursor.
//         /// </summary>
//         public class DummyScanner
//         {
//             public DummyCursor Cursor { get; } = new DummyCursor();
//         }
// 
//         /// <summary>
//         /// Dummy cursor representing a position.
//         /// </summary>
//         public class DummyCursor
//         {
//             public int Position { get; set; }
//             public int ResetCallCount { get; private set; }
// 
//             public void ResetPosition(int pos)
//             {
//                 ResetCallCount++;
//                 Position = pos;
//             }
//         }
// 
//         /// <summary>
//         /// Dummy parse result used for testing.
//         /// </summary>
//         /// <typeparam name = "T"></typeparam>
//         public class DummyParseResult<T>
//         {
//             public bool Success { get; set; }
//             public T Value { get; set; }
//         }
// 
//         /// <summary>
//         /// Fake implementation of the Parser class.
//         /// </summary>
//         /// <typeparam name = "T">The output type.</typeparam>
// //         public class FakeParser<T> : Parser<T> [Error] (269-55)CS1073 Unexpected token 'ref'
// //         {
// //             private readonly Func<DummyParseContext, ref DummyParseResult<T>, bool> _parseResultFunc; [Error] (266-54)CS1073 Unexpected token 'ref'
//             private readonly Func<FakeCompilationResult<T>> _buildResultFunc;
//             private readonly string _toString;
//             public FakeParser(Func<DummyParseContext, ref DummyParseResult<T>, bool> parseResult, Func<FakeCompilationResult<T>> buildResult, string toString = "FakeParser")
//             {
//                 _parseResultFunc = parseResult;
//                 _buildResultFunc = buildResult;
//                 _toString = toString;
//             }
// 
// //             public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (279-36)CS0039 Cannot convert type 'Parlot.Fluent.UnitTests.IfTests.ParseContext' to 'Parlot.Fluent.UnitTests.IfTests.DummyParseContext' via a reference conversion, boxing conversion, unboxing conversion, wrapping conversion, or null type conversion [Error] (285-72)CS1615 Argument 2 may not be passed with the 'ref' keyword
// //             {
// //                 // Cast the context and result to our dummy types.
// //                 var dummyContext = context as DummyParseContext;
// //                 var dummyResult = new DummyParseResult<T>
// //                 {
// //                     Success = result.Success,
// //                     Value = result.Value
// //                 };
// //                 bool parseSuccess = _parseResultFunc(dummyContext, ref dummyResult);
// //                 result.Success = dummyResult.Success;
// //                 result.Value = dummyResult.Value;
// //                 return parseSuccess;
// //             }
// 
//             public override FakeCompilationResult<T> Build(CompilationContext context, bool requireResult)
//             {
//                 return _buildResultFunc();
//             }
// 
//             public override string ToString()
//             {
//                 return _toString;
//             }
//         }
// 
//         /// <summary>
//         /// Dummy base classes to allow Fake implementations.
//         /// </summary>
//         public abstract class Parser<T>
//         {
//             public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
//             public abstract FakeCompilationResult<T> Build(CompilationContext context, bool requireResult);
//         }
// 
//         /// <summary>
//         /// Dummy base class representing the parse context.
//         /// </summary>
//         public abstract class ParseContext
//         {
//         }
// 
//         /// <summary>
//         /// Dummy base class for parse results.
//         /// </summary>
//         public class ParseResult<T>
//         {
//             public bool Success { get; set; }
//             public T Value { get; set; }
//         }
// 
//         /// <summary>
//         /// Dummy base class representing the compilation context.
//         /// </summary>
//         public class CompilationContext
//         {
//             public bool DiscardResult { get; set; }
//             public Expression ParseContext { get; set; }
// 
//             public CompilationContext(DummyParseContext context)
//             {
//                 // Wrap the DummyParseContext in a constant expression.
//                 ParseContext = Expression.Constant(context);
//                 DiscardResult = false;
//             }
// 
//             public FakeCompilationResult<T> CreateCompilationResult<T>()
//             {
//                 return new FakeCompilationResult<T>
//                 {
//                     Body = new List<Expression>(),
//                     Variables = new List<ParameterExpression>(),
//                     Success = Expression.Variable(typeof(bool), "success"),
//                     Value = Expression.Variable(typeof(T), "value")
//                 };
//             }
// 
//             public ParameterExpression DeclarePositionVariable<T>(FakeCompilationResult<T> result)
//             {
//                 var positionVar = Expression.Variable(typeof(int), "start");
//                 return positionVar;
//             }
// 
//             public Expression ResetPosition(ParameterExpression position)
//             {
//                 // Dummy reset expression.
//                 return Expression.Empty();
//             }
//         }
// 
//         /// <summary>
//         /// Fake compilation result used for testing Compile method.
//         /// </summary>
//         /// <typeparam name = "T"></typeparam>
//         public class FakeCompilationResult<T>
//         {
//             public List<Expression> Body { get; set; }
//             public List<ParameterExpression> Variables { get; set; }
//             public Expression Success { get; set; }
//             public Expression Value { get; set; }
//         }
// 
//         /// <summary>
//         /// Fake compilation context to simulate compilation environment.
//         /// </summary>
//         public class FakeCompilationContext : CompilationContext
//         {
//             public FakeCompilationContext(DummyParseContext context) : base(context)
//             {
//             }
//         }
// #endregion
//     }
// 
//     /// <summary>
//     /// The If parser implementation as provided.
//     /// </summary>
//     /// <typeparam name = "C">The type of parse context.</typeparam>
//     /// <typeparam name = "S">The type of the state.</typeparam>
//     /// <typeparam name = "T">The output type.</typeparam>
// //     public sealed class If<C, S, T> : Parser<T>, ICompilable where C : DummyParseContext [Error] (396-72)CS0246 The type or namespace name 'DummyParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (396-25)CS0534 'If<C, S, T>' does not implement inherited abstract member 'Parser<T>.Build(CompilationContext)' [Error] (396-25)CS0534 'If<C, S, T>' does not implement inherited abstract member 'Parser<T>.Parse(object, ref ParseResult<T>)' [Error] (396-25)CS0534 'If<C, S, T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' [Error] (396-25)CS0534 'If<C, S, T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' [Error] (396-25)CS0534 'If<C, S, T>' does not implement inherited abstract member 'Parser<T>.Build(CompilationContext, bool)' [Error] (396-25)CS0534 'If<C, S, T>' does not implement inherited abstract member 'Parser<T>.Compile(CompilationContext)' [Error] (396-50)CS0535 'If<C, S, T>' does not implement interface member 'ICompilable.Compile(object)'
// //     {
// //         private readonly Func<C, S, bool> _predicate;
// //         private readonly S _state;
// //         private readonly Parser<T> _parser;
// //         public If(Parser<T> parser, Func<C, S, bool> predicate, S state)
// //         {
// //             _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
// //             _state = state;
// //             _parser = parser ?? throw new ArgumentNullException(nameof(parser));
// //         }
// // 
// //         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (408-30)CS0462 The inherited members 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' have the same signature in type 'If<C, S, T>', so they cannot be overridden [Error] (411-43)CS0246 The type or namespace name 'DummyParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (418-30)CS0121 The call is ambiguous between the following methods or properties: 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)' and 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
// //         {
// //             // Cast context to DummyParseContext to call EnterParser/ExitParser.
// //             var dummyContext = context as DummyParseContext;
// //             dummyContext?.EnterParser(this);
// //             bool valid = _predicate((C)dummyContext, _state);
// //             if (valid)
// //             {
// //                 // Save the starting cursor position.
// //                 int start = ((dynamic)dummyContext).Scanner.Cursor.Position;
// //                 if (!_parser.Parse(context, ref result))
// //                 {
// //                     ((dynamic)dummyContext).Scanner.Cursor.ResetPosition(start);
// //                 }
// //             }
// // 
// //             dummyContext?.ExitParser(this);
// //             return valid;
// //         }
// // 
// //         public CompilationResult Compile(CompilationContext context) [Error] (428-34)CS0114 'If<C, S, T>.Compile(CompilationContext)' hides inherited member 'Parser<T>.Compile(CompilationContext)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (430-34)CS0121 The call is ambiguous between the following methods or properties: 'CompilationContext.CreateCompilationResult<T>()' and 'CompilationContext.CreateCompilationResult<T>()' [Error] (433-157)CS0229 Ambiguity between 'CompilationContext.ParseContext' and 'CompilationContext.ParseContext' [Error] (433-311)CS0229 Ambiguity between 'CompilationResult.Body' and 'CompilationResult.Body' [Error] (433-466)CS0229 Ambiguity between 'CompilationContext.DiscardResult' and 'CompilationContext.DiscardResult' [Error] (435-20)CS0144 Cannot create an instance of the abstract type or interface 'CompilationResult' [Error] (437-17)CS0229 Ambiguity between 'CompilationResult.Body' and 'CompilationResult.Body'
// //         {
// //             var result = context.CreateCompilationResult<T>();
// //             var parserCompileResult = _parser.Build(context, requireResult: true);
// //             var start = context.DeclarePositionVariable(result);
// //             var block = Expression.Block(Expression.IfThen(Expression.Invoke(Expression.Constant(_predicate), new Expression[] { Expression.Convert(context.ParseContext, typeof(C)), Expression.Constant(_state, typeof(S)) }), Expression.Block(Expression.Block(parserCompileResult.Variables, parserCompileResult.Body), Expression.IfThen(parserCompileResult.Success, Expression.Block(Expression.Assign(result.Success, Expression.Constant(true, typeof(bool))), context.DiscardResult ? Expression.Empty() : Expression.Assign(result.Value, parserCompileResult.Value))))), Expression.IfThen(Expression.Not(result.Success), context.ResetPosition(start)));
// //             result.Body.Add(block);
// //             return new CompilationResult
// //             {
// //                 Body = result.Body
// //             };
// //         }
// // 
// //         public override string ToString() => $"{_parser} (If)";
// //     }
// 
//     /// <summary>
//     /// Dummy interface representing a compilable entity.
//     /// </summary>
// //     public interface ICompilable [Error] (447-22)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ICompilable'
// //     {
// //         CompilationResult Compile(CompilationContext context); [Error] (449-27)CS0111 Type 'ICompilable' already defines a member called 'Compile' with the same parameter types
// //     }
// 
//     /// <summary>
//     /// Dummy compilation result.
//     /// </summary>
// //     public class CompilationResult [Error] (455-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'CompilationResult'
// //     {
// //         public List<Expression> Body { get; set; } [Error] (457-33)CS0533 'CompilationResult.Body' hides inherited abstract member 'CompilationResultBase.Body' [Error] (457-33)CS0114 'CompilationResult.Body' hides inherited member 'CompilationResultBase.Body'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
// //     }
// }
