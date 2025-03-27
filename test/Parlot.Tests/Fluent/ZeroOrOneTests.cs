using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Rewriting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "ZeroOrOne{T}"/> class focusing on the Parse method.
/// </summary>
// public class ZeroOrOneTests [Error] (429-12)CS1520 Method must have a return type
// {
//     /// <summary>
//     /// Tests that when the underlying parser succeeds, the Parse method sets the result with the parsed value.
//     /// </summary>
//     [Fact] [Error] (26-49)CS1739 The best overload for 'FakeParser' does not have a parameter named 'parseResult' [Error] (29-27)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (30-26)CS0246 The type or namespace name 'TestParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_ParserSucceeds_SetsResultWithParsedValue()
//     {
//         // Arrange
//         // Create a fake parser that returns success with specific start, end and value.
//         var expectedStart = 10;
//         var expectedEnd = 20;
//         var parsedValue = "parsed";
//         var fakeParser = new FakeParser<string>(parseResult: true, start: expectedStart, end: expectedEnd, value: parsedValue);
//         var defaultValue = "default";
//         var zeroOrOne = new ZeroOrOne<string>(fakeParser, defaultValue);
//         var context = new TestParseContext();
//         var result = new TestParseResult<string>();
//         // Act
//         bool returnValue = zeroOrOne.Parse(context, ref result);
//         // Assert
//         Assert.True(returnValue);
//         Assert.Equal(expectedStart, result.Start);
//         Assert.Equal(expectedEnd, result.End);
//         // Since the underlying parser succeeded, the resulting value should be from the fake parser.
//         Assert.Equal(parsedValue, result.Value);
//         // Verify that the context recorded the entry and exit of the parser.
//         Assert.Contains(zeroOrOne, context.EnteredParsers);
//         Assert.Contains(zeroOrOne, context.ExitedParsers);
//     }
// 
//     /// <summary>
//     /// Tests that when the underlying parser fails, the Parse method sets the result with the default value.
//     /// </summary>
//     [Fact] [Error] (54-49)CS1739 The best overload for 'FakeParser' does not have a parameter named 'parseResult' [Error] (57-27)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (58-26)CS0246 The type or namespace name 'TestParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_ParserFails_SetsResultWithDefaultValue()
//     {
//         // Arrange
//         // Create a fake parser that returns failure but still sets specific start/end values.
//         var expectedStart = 30;
//         var expectedEnd = 40;
//         var fakeParser = new FakeParser<string>(parseResult: false, start: expectedStart, end: expectedEnd, value: "ignored");
//         var defaultValue = "default";
//         var zeroOrOne = new ZeroOrOne<string>(fakeParser, defaultValue);
//         var context = new TestParseContext();
//         var result = new TestParseResult<string>();
//         // Act
//         bool returnValue = zeroOrOne.Parse(context, ref result);
//         // Assert
//         Assert.True(returnValue);
//         Assert.Equal(expectedStart, result.Start);
//         Assert.Equal(expectedEnd, result.End);
//         // Since the underlying parser failed, the resulting value should be the default value.
//         Assert.Equal(defaultValue, result.Value);
//         // Verify that the context recorded the entry and exit of the parser.
//         Assert.Contains(zeroOrOne, context.EnteredParsers);
//         Assert.Contains(zeroOrOne, context.ExitedParsers);
//     }
// 
//     /// <summary>
//     /// Tests that passing a null ParseContext to the Parse method throws a NullReferenceException.
//     /// </summary>
//     [Fact] [Error] (79-49)CS1739 The best overload for 'FakeParser' does not have a parameter named 'parseResult' [Error] (82-9)CS0246 The type or namespace name 'TestParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (83-26)CS0246 The type or namespace name 'TestParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (85-9)CS0619 'Assert.Throws<T>(Func<Task>)' is obsolete: 'You must call Assert.ThrowsAsync<T> (and await the result) when testing async code.'
//     public void Parse_NullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<string>(parseResult: true, start: 0, end: 0, value: "value");
//         var defaultValue = "default";
//         var zeroOrOne = new ZeroOrOne<string>(fakeParser, defaultValue);
//         TestParseContext context = null;
//         var result = new TestParseResult<string>();
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => zeroOrOne.Parse(context, ref result));
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method builds a compilation result containing an IfThenElse expression
//     /// when the CompilationContext.DiscardResult property is false.
//     /// </summary>
//     [Fact] [Error] (108-80)CS1503 Argument 1: cannot convert from 'ZeroOrOneTests.FakeCompilationData<int>' to 'Parlot.Compilation.CompilationResult' [Error] (108-31)CS0854 An expression tree may not contain a call or invocation that uses optional arguments
//     public void Compile_WhenDiscardResultFalse_ReturnsCompilationResultWithIfThenElseBlock()
//     {
//         // Arrange
//         int defaultValue = 0;
//         var fakeCompilationData = new FakeCompilationData<int>
//         {
//             Variables = Array.Empty<ParameterExpression>(),
//             Body = new Expression[]
//             {
//                 Expression.Constant(42)
//             },
//             Success = Expression.Constant(true),
//             Value = Expression.Constant(100)
//         };
//         var mockParser = new Mock<Parser<int>>();
//         mockParser.Setup(p => p.Build(It.IsAny<CompilationContext>())).Returns(fakeCompilationData);
//         var sut = new ZeroOrOne<int>(mockParser.Object, defaultValue);
//         var context = new FakeCompilationContext
//         {
//             DiscardResult = false
//         };
//         // Act
//         var compileResult = sut.Compile(context);
//         // Assert
//         Assert.NotNull(compileResult);
//         Assert.NotNull(compileResult.Value);
//         Assert.Single(compileResult.Body);
//         var blockExpr = compileResult.Body[0] as BlockExpression;
//         Assert.NotNull(blockExpr);
//         // Traverse the expression tree to check that an IfThenElse conditional is present.
//         var visitor = new ConditionalFinder();
//         visitor.Visit(blockExpr);
//         Assert.True(visitor.Found, "Expected an IfThenElse expression in the compiled block when DiscardResult is false.");
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method builds a compilation result without an IfThenElse expression,
//     /// and thus uses Expression.Empty(), when the CompilationContext.DiscardResult property is true.
//     /// </summary>
//     [Fact] [Error] (148-80)CS1503 Argument 1: cannot convert from 'ZeroOrOneTests.FakeCompilationData<int>' to 'Parlot.Compilation.CompilationResult' [Error] (148-31)CS0854 An expression tree may not contain a call or invocation that uses optional arguments
//     public void Compile_WhenDiscardResultTrue_ReturnsCompilationResultWithEmptyBranch()
//     {
//         // Arrange
//         int defaultValue = 5;
//         var fakeCompilationData = new FakeCompilationData<int>
//         {
//             Variables = Array.Empty<ParameterExpression>(),
//             Body = new Expression[]
//             {
//                 Expression.Constant(24)
//             },
//             Success = Expression.Constant(false),
//             Value = Expression.Constant(200)
//         };
//         var mockParser = new Mock<Parser<int>>();
//         mockParser.Setup(p => p.Build(It.IsAny<CompilationContext>())).Returns(fakeCompilationData);
//         var sut = new ZeroOrOne<int>(mockParser.Object, defaultValue);
//         var context = new FakeCompilationContext
//         {
//             DiscardResult = true
//         };
//         // Act
//         var compileResult = sut.Compile(context);
//         // Assert
//         Assert.NotNull(compileResult);
//         Assert.NotNull(compileResult.Value);
//         Assert.Single(compileResult.Body);
//         var blockExpr = compileResult.Body[0] as BlockExpression;
//         Assert.NotNull(blockExpr);
//         // Traverse the expression tree to verify that no IfThenElse conditional expression is present.
//         var visitor = new ConditionalFinder();
//         visitor.Visit(blockExpr);
//         Assert.False(visitor.Found, "Expected no IfThenElse expression in the compiled block when DiscardResult is true.");
//     }
// 
//     /// <summary>
//     /// Helper class to search for ConditionalExpression nodes within an expression tree.
//     /// </summary>
//     private class ConditionalFinder : ExpressionVisitor
//     {
//         /// <summary>
//         /// Gets a value indicating whether a ConditionalExpression node was found.
//         /// </summary>
//         public bool Found { get; private set; }
// 
//         /// <summary>
//         /// Visits the children of the <see cref = "Expression"/> and sets Found to true if a ConditionalExpression is encountered.
//         /// </summary>
//         /// <param name = "node">The expression node to visit.</param>
//         /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
//         public override Expression Visit(Expression node)
//         {
//             if (node is ConditionalExpression)
//             {
//                 Found = true;
//             }
// 
//             return base.Visit(node);
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of CompilationContext for testing purposes.
//     /// </summary>
//     private class FakeCompilationContext : CompilationContext
//     {
//         /// <summary>
//         /// Gets or sets a value indicating whether the result should be discarded.
//         /// </summary>
//         public bool DiscardResult { get; set; } [Error] (202-21)CS0108 'ZeroOrOneTests.FakeCompilationContext.DiscardResult' hides inherited member 'CompilationContext.DiscardResult'. Use the new keyword if hiding was intended.
// 
//         /// <summary>
//         /// Creates a fake compilation result with an initial value.
//         /// </summary>
//         /// <typeparam name = "T">The type parameter for the compilation result.</typeparam>
//         /// <param name = "flag">A boolean flag (unused in this fake implementation).</param>
//         /// <param name = "initial">The initial expression value.</param>
//         /// <returns>A fake compilation result.</returns>
//         public FakeCompilationResult<T> CreateCompilationResult<T>(bool flag, Expression initial) [Error] (211-41)CS0108 'ZeroOrOneTests.FakeCompilationContext.CreateCompilationResult<T>(bool, Expression)' hides inherited member 'CompilationContext.CreateCompilationResult<TValue>(bool, Expression?)'. Use the new keyword if hiding was intended.
//         {
//             return new FakeCompilationResult<T>(initial);
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of a compilation result used for testing.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter for the result value.</typeparam>
//     private class FakeCompilationResult<T>
//     {
//         /// <summary>
//         /// Gets the result value represented as a ParameterExpression.
//         /// </summary>
//         public ParameterExpression Value { get; }
//         /// <summary>
//         /// Gets the list of expressions composing the compilation body.
//         /// </summary>
//         public List<Expression> Body { get; }
// 
//         /// <summary>
//         /// Initializes a new instance of the <see cref = "FakeCompilationResult{T}"/> class.
//         /// </summary>
//         /// <param name = "initial">The initial expression (unused in this fake beyond creation of the result parameter).</param>
//         public FakeCompilationResult(Expression initial)
//         {
//             Value = Expression.Parameter(typeof(T), "result");
//             Body = new List<Expression>();
//         }
//     }
// 
//     /// <summary>
//     /// A fake data holder to simulate the result of a parser's Build method.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter for the parser result.</typeparam>
//     private class FakeCompilationData<T>
//     {
//         /// <summary>
//         /// Gets or sets the variables used in the compilation.
//         /// </summary>
//         public IEnumerable<ParameterExpression> Variables { get; set; } = Array.Empty<ParameterExpression>();
//         /// <summary>
//         /// Gets or sets the body expressions resulted from the parser build.
//         /// </summary>
//         public Expression[] Body { get; set; } = Array.Empty<Expression>();
//         /// <summary>
//         /// Gets or sets the expression representing the success check of the parser.
//         /// </summary>
//         public Expression Success { get; set; }
//         /// <summary>
//         /// Gets or sets the expression representing the parser value.
//         /// </summary>
//         public Expression Value { get; set; }
//     }
// 
//     /// <summary>
//     /// Dummy parser class for testing purposes.
//     /// Inherits from Parser<int> and allows customization of the ToString() output.
//     /// </summary>
//     private class DummyParser : Parser<int> [Error] (271-19)CS0534 'ZeroOrOneTests.DummyParser' does not implement inherited abstract member 'Parser<int>.Parse(ParseContext, ref ParseResult<int>)'
//     {
//         private readonly string _toStringValue;
//         public DummyParser(string toStringValue)
//         {
//             _toStringValue = toStringValue;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (279-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => _toStringValue;
//     }
// 
//     /// <summary>
//     /// Tests that the ToString method returns the parser's ToString value appended with a question mark.
//     /// This verifies the happy path where a valid parser is provided.
//     /// </summary>
//     [Fact]
//     public void ToString_WithValidParser_ReturnsParserToStringAppendedWithQuestionMark()
//     {
//         // Arrange
//         string expectedParserString = "DummyParser";
//         var dummyParser = new DummyParser(expectedParserString);
//         var zeroOrOne = new ZeroOrOne<int>(dummyParser, default);
//         string expected = $"{expectedParserString}?";
//         // Act
//         string actual = zeroOrOne.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that the ToString method correctly handles a parser whose ToString returns an empty string.
//     /// Expected result is a question mark only.
//     /// </summary>
//     [Fact]
//     public void ToString_WithEmptyParserToString_ReturnsQuestionMarkOnly()
//     {
//         // Arrange
//         string expectedParserString = string.Empty;
//         var dummyParser = new DummyParser(expectedParserString);
//         var zeroOrOne = new ZeroOrOne<int>(dummyParser, default);
//         string expected = $"{expectedParserString}?"; // Expected: "?" 
//         // Act
//         string actual = zeroOrOne.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that the ToString method correctly handles the case when the parser is null.
//     /// Expected result is a question mark only, since null is interpolated to an empty string.
//     /// </summary>
//     [Fact]
//     public void ToString_WithNullParser_ReturnsQuestionMarkOnly()
//     {
//         // Arrange
//         Parser<int> nullParser = null;
//         var zeroOrOne = new ZeroOrOne<int>(nullParser, default);
//         string expected = $"{nullParser}?"; // Expected: "?" because null is rendered as empty string
//         // Act
//         string actual = zeroOrOne.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Verifies that the ZeroOrOne constructor throws an ArgumentNullException when a null parser is provided.
//     /// </summary>
//     [Fact]
//     public void ZeroOrOne_Constructor_NullParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Parser<string> nullParser = null;
//         string defaultValue = "default";
//         // Act & Assert
//         ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new ZeroOrOne<string>(nullParser, defaultValue));
//         Assert.Equal("parser", exception.ParamName);
//     }
// 
//     /// <summary>
//     /// Verifies that the ZeroOrOne constructor initializes successfully when a valid non-seekable parser is provided.
//     /// </summary>
//     [Fact]
//     public void ZeroOrOne_Constructor_ValidNonSeekableParser_InitializesWithoutSeekableInterface()
//     {
//         // Arrange
//         var fakeParser = new DummyNonSeekableParser<string>();
//         string defaultValue = "default";
//         // Act
//         var zeroOrOne = new ZeroOrOne<string>(fakeParser, defaultValue);
//         // Assert
//         // Since fakeParser does not implement ISeekable, the seekable properties are not explicitly set.
//         // The object should still be instantiated successfully.
//         Assert.NotNull(zeroOrOne);
//     }
// 
//     /// <summary>
//     /// Verifies that the ZeroOrOne constructor correctly assigns seekable properties when the provided parser implements ISeekable.
//     /// </summary>
//     [Fact]
//     public void ZeroOrOne_Constructor_ValidSeekableParser_SetsSeekableProperties()
//     {
//         // Arrange
//         bool expectedCanSeek = true;
//         char[] expectedChars = new char[]
//         {
//             'a',
//             'b'
//         };
//         bool expectedSkipWhitespace = false;
//         var fakeSeekableParser = new DummySeekableParser<string>(expectedCanSeek, expectedChars, expectedSkipWhitespace);
//         string defaultValue = "default";
//         // Act
//         var zeroOrOne = new ZeroOrOne<string>(fakeSeekableParser, defaultValue);
//         // Assert
//         Assert.Equal(expectedCanSeek, zeroOrOne.CanSeek);
//         Assert.Equal(expectedChars, zeroOrOne.ExpectedChars);
//         Assert.Equal(expectedSkipWhitespace, zeroOrOne.SkipWhitespace);
//     }
// 
//     // Dummy parser that does NOT implement ISeekable.
//     private class DummyNonSeekableParser<T> : Parser<T> [Error] (396-19)CS0534 'ZeroOrOneTests.DummyNonSeekableParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (398-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     // Dummy parser that implements ISeekable.
//     private class DummySeekableParser<T> : Parser<T>, ISeekable [Error] (405-19)CS0534 'ZeroOrOneTests.DummySeekableParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//         public DummySeekableParser(bool canSeek, char[] expectedChars, bool skipWhitespace)
//         {
//             CanSeek = canSeek;
//             ExpectedChars = expectedChars;
//             SkipWhitespace = skipWhitespace;
//         }
// 
//         public bool CanSeek { get; private set; }
//         public char[] ExpectedChars { get; private set; }
//         public bool SkipWhitespace { get; private set; }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (418-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     private readonly bool _canSeek;
//     /// <summary>
//     /// Initializes a new instance of the <see cref = "DummyParser{T}"/> class.
//     /// </summary>
//     /// <param name = "canSeek">Sets the value to be returned by the <see cref = "ISeekable.CanSeek"/> property.</param>
//     public DummyParser(bool canSeek)
//     {
//         _canSeek = canSeek;
//     }
// 
//     /// <summary>
//     /// Gets a value indicating whether the parser supports seeking.
//     /// </summary>
//     public bool CanSeek => _canSeek;
// 
//     /// <summary>
//     /// Stub implementation of Parse. Not used in tests.
//     /// </summary>
//     /// <param name = "context">The parsing context.</param>
//     /// <param name = "result">The parsing result.</param>
//     /// <returns>A boolean value.</returns>
//     public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (445-58)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (445-70)CS0246 The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         throw new NotImplementedException();
//     }
// 
//     /// <summary>
//     /// A minimal fake implementation of Parser{T} to be used in testing ZeroOrOne.
//     /// </summary>
//     private class FakeParser<T> : Parser<T> [Error] (453-19)CS0534 'ZeroOrOneTests.FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (455-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the ExpectedChars property is not null.
//     /// Arrange: Create a ZeroOrOne instance with a fake parser and default value.
//     /// Act: Access the ExpectedChars property.
//     /// Assert: It should not be null.
//     /// </summary>
//     [Fact]
//     public void ExpectedChars_WhenAccessed_IsNotNull()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<int>();
//         var zeroOrOne = new ZeroOrOne<int>(fakeParser, defaultValue: 0);
//         // Act
//         var result = zeroOrOne.ExpectedChars;
//         // Assert
//         Assert.NotNull(result);
//     }
// 
//     /// <summary>
//     /// Tests that the ExpectedChars property returns an empty char array.
//     /// Arrange: Create a ZeroOrOne instance with a fake parser and default value.
//     /// Act: Access the ExpectedChars property.
//     /// Assert: It should be an empty array.
//     /// </summary>
//     [Fact]
//     public void ExpectedChars_WhenAccessed_ReturnsEmptyArray()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<string>();
//         var zeroOrOne = new ZeroOrOne<string>(fakeParser, defaultValue: string.Empty);
//         // Act
//         var result = zeroOrOne.ExpectedChars;
//         // Assert
//         Assert.Empty(result);
//     }
// 
//     /// <summary>
//     /// Minimal implementation of the Parse method.
//     /// </summary>
//     /// <param name = "context">The parse context.</param>
//     /// <param name = "result">The parse result.</param>
//     /// <returns>Always returns false.</returns>
//     public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (503-58)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (503-70)CS0246 The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?) [Error] (503-26)CS0111 Type 'ZeroOrOneTests' already defines a member called 'Parse' with the same parameter types [Error] (505-22)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (505-34)CS0246 The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         result = new ParseResult<T>();
//         return false;
//     }
// }