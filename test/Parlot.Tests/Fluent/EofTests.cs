// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Eof{T}"/> class.
/// </summary>
// public class EofTests [Error] (443-2)CS1513 } expected
// {
//     /// <summary>
//     /// A fake implementation of the ParseContext used for testing.
//     /// </summary>
//     private class FakeParseContext : ParseContext [Error] (22-16)CS7036 There is no argument given that corresponds to the required parameter 'scanner' of 'ParseContext.ParseContext(Scanner, bool)'
//     {
//         public FakeScanner Scanner { get; set; } [Error] (20-28)CS0108 'EofTests.FakeParseContext.Scanner' hides inherited member 'ParseContext.Scanner'. Use the new keyword if hiding was intended.
// 
//         public FakeParseContext(bool eof)
//         {
//             Scanner = new FakeScanner(eof);
//         }
// 
//         public override void EnterParser(object parser) [Error] (27-30)CS0115 'EofTests.FakeParseContext.EnterParser(object)': no suitable method found to override
//         {
//         // No operation needed for testing.
//         }
// 
//         public override void ExitParser(object parser) [Error] (32-30)CS0115 'EofTests.FakeParseContext.ExitParser(object)': no suitable method found to override
//         {
//         // No operation needed for testing.
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of a scanner containing a cursor.
//     /// </summary>
//     private class FakeScanner
//     {
//         public FakeCursor Cursor { get; set; }
// 
//         public FakeScanner(bool eof)
//         {
//             Cursor = new FakeCursor(eof);
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of a cursor that indicates if the end-of-file has been reached.
//     /// </summary>
//     private class FakeCursor
//     {
//         public bool Eof { get; set; }
// 
//         public FakeCursor(bool eof)
//         {
//             Eof = eof;
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of ParseResult used for testing.
//     /// </summary>
//     private class FakeParseResult : ParseResult<int> [Error] (67-37)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     {
//     // No additional members required for the test.
//     }
// 
//     /// <summary>
//     /// A fake implementation of a parser that returns a predetermined boolean value.
//     /// </summary>
//     private class FakeParser : Parser<int> [Error] (75-19)CS0534 'EofTests.FakeParser' does not implement inherited abstract member 'Parser<int>.Parse(ParseContext, ref ParseResult<int>)'
//     {
//         private readonly bool _shouldSucceed;
//         public FakeParser(bool shouldSucceed)
//         {
//             _shouldSucceed = shouldSucceed;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (83-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             return _shouldSucceed;
//         }
//     }
// 
//     /// <summary>
//     /// Tests the Parse method when the inner parser returns true and the cursor indicates end-of-file.
//     /// Expected outcome: The Parse method returns true.
//     /// </summary>
//     [Fact] [Error] (100-9)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenInnerParserSucceedsAndCursorAtEof_ReturnsTrue()
//     {
//         // Arrange
//         var fakeInnerParser = new FakeParser(true);
//         var eofParser = new Eof<int>(fakeInnerParser);
//         var context = new FakeParseContext(eof: true);
//         ParseResult<int> result = new FakeParseResult();
//         // Act
//         bool parseResult = eofParser.Parse(context, ref result);
//         // Assert
//         Assert.True(parseResult);
//     }
// 
//     /// <summary>
//     /// Tests the Parse method when the inner parser returns false, irrespective of the cursor's state.
//     /// Expected outcome: The Parse method returns false.
//     /// </summary>
//     [Fact] [Error] (119-9)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenInnerParserFails_ReturnsFalse()
//     {
//         // Arrange
//         var fakeInnerParser = new FakeParser(false);
//         var eofParser = new Eof<int>(fakeInnerParser);
//         // Cursor state is irrelevant in this test; using true.
//         var context = new FakeParseContext(eof: true);
//         ParseResult<int> result = new FakeParseResult();
//         // Act
//         bool parseResult = eofParser.Parse(context, ref result);
//         // Assert
//         Assert.False(parseResult);
//     }
// 
//     /// <summary>
//     /// Tests the Parse method when the inner parser succeeds but the cursor is not at end-of-file.
//     /// Expected outcome: The Parse method returns false.
//     /// </summary>
//     [Fact] [Error] (137-9)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenInnerParserSucceedsButCursorNotAtEof_ReturnsFalse()
//     {
//         // Arrange
//         var fakeInnerParser = new FakeParser(true);
//         var eofParser = new Eof<int>(fakeInnerParser);
//         var context = new FakeParseContext(eof: false);
//         ParseResult<int> result = new FakeParseResult();
//         // Act
//         bool parseResult = eofParser.Parse(context, ref result);
//         // Assert
//         Assert.False(parseResult);
//     }
// 
//     /// <summary>
//     /// Tests the Parse method when a null ParseContext is passed.
//     /// Expected outcome: A NullReferenceException is thrown.
//     /// </summary>
//     [Fact] [Error] (154-9)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (156-9)CS0619 'Assert.Throws<T>(Func<Task>)' is obsolete: 'You must call Assert.ThrowsAsync<T> (and await the result) when testing async code.'
//     public void Parse_WhenContextIsNull_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var fakeInnerParser = new FakeParser(true);
//         var eofParser = new Eof<int>(fakeInnerParser);
//         ParseResult<int> result = new FakeParseResult();
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => eofParser.Parse(null, ref result));
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method constructs the expected expression tree when the context does not discard the result.
//     /// </summary>
//     [Fact] [Error] (167-41)CS0308 The non-generic type 'CompilationResult' cannot be used with type arguments [Error] (176-57)CS1660 Cannot convert lambda expression to type 'string' because it is not a delegate type [Error] (180-31)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (185-9)CS0308 The non-generic type 'CompilationResult' cannot be used with type arguments [Error] (197-49)CS0117 'ExpressionType' does not contain a definition for 'IfThen'
//     public void Compile_DiscardResultFalse_ConstructsExpectedExpressionTree()
//     {
//         // Arrange
//         // Create a fake parser which returns a controlled compilation result.
//         var fakeParserBuildResult = new CompilationResult<string>
//         {
//             // Create a dummy variable for testing purposes.
//             Value = Expression.Parameter(typeof(string), "parserValue"),
//             Success = Expression.Constant(true, typeof(bool))
//         };
//         // Add a dummy body expression.
//         fakeParserBuildResult.Body.Add(Expression.Constant("ParserBody"));
//         // Create a fake parser with a Build function that returns the fakeParserBuildResult.
//         var fakeParser = new FakeParser<string>(context => fakeParserBuildResult);
//         // Create an instance of Eof using the fake parser.
//         var eofParser = new Eof<string>(fakeParser);
//         // Create a fake compilation context with DiscardResult set to false.
//         var fakeContext = new FakeCompilationContext
//         {
//             DiscardResultFlag = false
//         };
//         // Act
//         CompilationResult<string> result = eofParser.Compile(fakeContext);
//         // Assert
//         // Expect the result to be non-null and its Body should contain one block expression added in Compile.
//         Assert.NotNull(result);
//         Assert.NotEmpty(result.Body);
//         // The outer expression should be a BlockExpression.
//         Assert.IsType<BlockExpression>(result.Body[0]);
//         BlockExpression outerBlock = (BlockExpression)result.Body[0];
//         // Attempt to locate the IfThen expression inside the outer block.
//         Expression ifThenExpr = null;
//         foreach (var expr in outerBlock.Expressions)
//         {
//             if (expr.NodeType == ExpressionType.IfThen)
//             {
//                 ifThenExpr = expr;
//                 break;
//             }
//         }
// 
//         Assert.NotNull(ifThenExpr);
//         Assert.IsType<ConditionalExpression>(ifThenExpr);
//         // Since the expression was built via Expression.IfThen,
//         // we cast using the pattern of ConditionalExpression.
//         ConditionalExpression conditional = (ConditionalExpression)ifThenExpr;
//         // The test part is built using Expression.AndAlso, so verify that.
//         Assert.Equal(ExpressionType.AndAlso, conditional.Test.NodeType);
//         // The if-true branch is expected to be a BlockExpression with two expressions.
//         Assert.IsType<BlockExpression>(conditional.IfTrue);
//         BlockExpression ifTrueBlock = (BlockExpression)conditional.IfTrue;
//         Assert.Equal(2, ifTrueBlock.Expressions.Count);
//         // When DiscardResult is false, the first expression should be an assignment to result.Value.
//         Expression firstExpr = ifTrueBlock.Expressions[0];
//         Assert.Equal(ExpressionType.Assign, firstExpr.NodeType);
//         var assignExpr = (BinaryExpression)firstExpr;
//         // Check that the left-hand side of the assignment is result.Value parameter.
//         Assert.Equal(result.Value.Name, ((ParameterExpression)assignExpr.Left).Name);
//         // The second expression should be an assignment to result.Success with a constant true.
//         Expression secondExpr = ifTrueBlock.Expressions[1];
//         Assert.Equal(ExpressionType.Assign, secondExpr.NodeType);
//         var assignSuccessExpr = (BinaryExpression)secondExpr;
//         Assert.Equal(result.Success.Name, ((ParameterExpression)assignSuccessExpr.Left).Name);
//         // Verify that the right-hand side is a constant true.
//         Assert.IsType<ConstantExpression>(assignSuccessExpr.Right);
//         var constExpr = (ConstantExpression)assignSuccessExpr.Right;
//         Assert.True((bool)constExpr.Value);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method constructs the expected expression tree when the context discards the result.
//     /// </summary>
//     [Fact] [Error] (239-41)CS0308 The non-generic type 'CompilationResult' cannot be used with type arguments [Error] (245-57)CS1660 Cannot convert lambda expression to type 'string' because it is not a delegate type [Error] (247-31)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (252-9)CS0308 The non-generic type 'CompilationResult' cannot be used with type arguments [Error] (262-49)CS0117 'ExpressionType' does not contain a definition for 'IfThen'
//     public void Compile_DiscardResultTrue_ConstructsExpectedExpressionTree()
//     {
//         // Arrange
//         var fakeParserBuildResult = new CompilationResult<string>
//         {
//             Value = Expression.Parameter(typeof(string), "parserValue"),
//             Success = Expression.Constant(true, typeof(bool))
//         };
//         fakeParserBuildResult.Body.Add(Expression.Constant("ParserBody"));
//         var fakeParser = new FakeParser<string>(context => fakeParserBuildResult);
//         var eofParser = new Eof<string>(fakeParser);
//         var fakeContext = new FakeCompilationContext
//         {
//             DiscardResultFlag = true
//         };
//         // Act
//         CompilationResult<string> result = eofParser.Compile(fakeContext);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotEmpty(result.Body);
//         Assert.IsType<BlockExpression>(result.Body[0]);
//         BlockExpression outerBlock = (BlockExpression)result.Body[0];
//         // Locate the IfThen expression in the outer block.
//         Expression ifThenExpr = null;
//         foreach (var expr in outerBlock.Expressions)
//         {
//             if (expr.NodeType == ExpressionType.IfThen)
//             {
//                 ifThenExpr = expr;
//                 break;
//             }
//         }
// 
//         Assert.NotNull(ifThenExpr);
//         Assert.IsType<ConditionalExpression>(ifThenExpr);
//         ConditionalExpression conditional = (ConditionalExpression)ifThenExpr;
//         Assert.Equal(ExpressionType.AndAlso, conditional.Test.NodeType);
//         Assert.IsType<BlockExpression>(conditional.IfTrue);
//         BlockExpression ifTrueBlock = (BlockExpression)conditional.IfTrue;
//         Assert.Equal(2, ifTrueBlock.Expressions.Count);
//         // When DiscardResult is true, the first expression should be an Expression.Empty.
//         Expression firstExpr = ifTrueBlock.Expressions[0];
//         Assert.Equal(ExpressionType.Default, firstExpr.NodeType);
//         // The second expression should be the assignment of result.Success.
//         Expression secondExpr = ifTrueBlock.Expressions[1];
//         Assert.Equal(ExpressionType.Assign, secondExpr.NodeType);
//         var assignSuccessExpr = (BinaryExpression)secondExpr;
//         Assert.Equal(result.Success.Name, ((ParameterExpression)assignSuccessExpr.Left).Name);
//         Assert.IsType<ConstantExpression>(assignSuccessExpr.Right);
//         var constExpr = (ConstantExpression)assignSuccessExpr.Right;
//         Assert.True((bool)constExpr.Value);
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method throws a NullReferenceException when the provided context is null.
//     /// </summary>
//     [Fact] [Error] (297-57)CS1660 Cannot convert lambda expression to type 'string' because it is not a delegate type
//     public void Compile_NullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         // Create a fake parser, though it will not be used because context is null.
//         var fakeParser = new FakeParser<string>(context => new CompilationResult<string>());
//         var eofParser = new Eof<string>(fakeParser);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => eofParser.Compile(null));
//     }
// 
//     /// <summary>
//     /// A dummy implementation of Parser&lt;int&gt; for testing purposes.
//     /// </summary>
//     private class DummyParser : Parser<int> [Error] (306-19)CS0534 'EofTests.DummyParser' does not implement inherited abstract member 'Parser<int>.Parse(ParseContext, ref ParseResult<int>)'
//     {
//         private readonly string _toStringValue;
//         /// <summary>
//         /// Initializes a new instance of the <see cref = "DummyParser"/> class with a specified string to return from ToString.
//         /// </summary>
//         /// <param name = "toStringValue">The string value to be returned by ToString.</param>
//         public DummyParser(string toStringValue)
//         {
//             _toStringValue = toStringValue;
//         }
// 
//         /// <summary>
//         /// Dummy implementation of the Parse method. Not used in these tests.
//         /// </summary>
//         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (321-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             result = default;
//             return false;
//         }
// 
//         /// <summary>
//         /// Returns the preset string value.
//         /// </summary>
//         public override string ToString()
//         {
//             return _toStringValue;
//         }
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns the underlying parser's ToString result appended with " (Eof)".
//     /// </summary>
//     [Fact]
//     public void ToString_WhenUnderlyingParserReturnsNonNull_ReturnsConcatenatedString()
//     {
//         // Arrange
//         string parserString = "DummyParser";
//         var dummyParser = new DummyParser(parserString);
//         var eof = new Eof<int>(dummyParser);
//         string expected = $"{parserString} (Eof)";
//         // Act
//         string actual = eof.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns " (Eof)" when the underlying parser's ToString returns null.
//     /// </summary>
//     [Fact]
//     public void ToString_WhenUnderlyingParserToStringReturnsNull_ReturnsEofSuffixOnly()
//     {
//         // Arrange
//         string parserString = null;
//         var dummyParser = new DummyParser(parserString);
//         var eof = new Eof<int>(dummyParser);
//         string expected = $"{(string)null} (Eof)"; // This will result in " (Eof)" due to string interpolation.
//         // Act
//         string actual = eof.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns " (Eof)" when a null parser is provided to the constructor.
//     /// This tests the edge case where the underlying parser is null.
//     /// </summary>
//     [Fact]
//     public void ToString_WhenParserIsNull_ReturnsEofSuffixOnly()
//     {
//         // Arrange
//         // Passing null to the constructor. The expected behavior is that null converts to an empty string in interpolation.
//         var eof = new Eof<int>(null);
//         string expected = $"{(string)null} (Eof)"; // Expected to be " (Eof)".
//         // Act
//         string actual = eof.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// A fake implementation of <see cref = "Parser{T}"/> used solely for testing the constructor of <see cref = "Eof{T}"/>.
//     /// </summary>
//     private class FakeParser<T> : Parser<T> [Error] (390-19)CS0534 'EofTests.FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//         private readonly string _name;
//         public FakeParser(string name = "FakeParser")
//         {
//             _name = name;
//         }
// 
//         /// <summary>
//         /// Provides a dummy parse implementation returning true.
//         /// </summary>
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (401-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             return true;
//         }
// 
//         /// <summary>
//         /// Overrides ToString to return a predefined name.
//         /// </summary>
//         public override string ToString() => _name;
//     }
// 
//     /// <summary>
//     /// Verifies that the <see cref = "Eof{T}"/> constructor correctly initializes the instance when provided with a valid parser.
//     /// The test confirms that <see cref = "Eof{T}.ToString"/> returns the expected formatted string.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithValidParser_InitializesCorrectly()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<int>("TestParser");
//         // Act
//         var eofInstance = new Eof<int>(fakeParser);
//         var result = eofInstance.ToString();
//         // Assert
//         Assert.Equal("TestParser (Eof)", result);
//     }
// 
//     /// <summary>
//     /// Verifies that the <see cref = "Eof{T}"/> constructor accepts a null parser.
//     /// In such case, when calling <see cref = "Eof{T}.ToString"/>, a <see cref = "NullReferenceException"/> is expected.
//     /// This test evaluates the edge case of providing a null dependency.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithNullParser_ToStringThrowsNullReferenceException()
//     {
//         // Arrange
//         Parser<int> nullParser = null;
//         // Act
//         var eofInstance = new Eof<int>(nullParser);
//         // Assert
//         Assert.Throws<NullReferenceException>(() => eofInstance.ToString());
//     }
// }