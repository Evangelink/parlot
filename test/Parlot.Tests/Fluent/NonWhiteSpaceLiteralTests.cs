// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "NonWhiteSpaceLiteral"/> class.
/// </summary>
// public class NonWhiteSpaceLiteralTests [Error] (286-2)CS1513 } expected
// {
//     /// <summary>
//     /// Tests that Parse returns false when the scanner is at EOF.
//     /// </summary>
//     [Fact] [Error] (25-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?) [Error] (26-31)CS0246 The type or namespace name 'FakeScanner' could not be found (are you missing a using directive or an assembly reference?) [Error] (27-31)CS0246 The type or namespace name 'FakeParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (29-26)CS0246 The type or namespace name 'FakeParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenScannerAtEOF_ReturnsFalse()
//     {
//         // Arrange
//         var fakeCursor = new FakeCursor(0, bufferLength: 0);
//         var fakeScanner = new FakeScanner("", fakeCursor, nonWhiteSpaceAdvance: 1, nonWhiteSpaceOrNewLineAdvance: 1);
//         var fakeContext = new FakeParseContext(fakeScanner);
//         var parser = new NonWhiteSpaceLiteral(); // default includeNewLines = true
//         var result = new FakeParseResult<TextSpan>();
//         // Act
//         bool parseResult = parser.Parse(fakeContext, ref result);
//         // Assert
//         Assert.False(parseResult);
//         // Since nothing was parsed, the result should not have been set (Start and End remain 0).
//         Assert.Equal(0, result.Start);
//         Assert.Equal(0, result.End);
//         // Verify that EnterParser and ExitParser were called exactly once.
//         Assert.Equal(1, fakeContext.EnterCallCount);
//         Assert.Equal(1, fakeContext.ExitCallCount);
//     }
// 
//     /// <summary>
//     /// Tests that Parse returns false when no characters are consumed.
//     /// </summary>
//     [Fact] [Error] (50-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?) [Error] (51-31)CS0246 The type or namespace name 'FakeScanner' could not be found (are you missing a using directive or an assembly reference?) [Error] (52-31)CS0246 The type or namespace name 'FakeParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (54-26)CS0246 The type or namespace name 'FakeParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenNoCharactersConsumed_ReturnsFalse()
//     {
//         // Arrange
//         // Buffer with content but simulate that reading does not advance the cursor.
//         var fakeCursor = new FakeCursor(0, bufferLength: 3);
//         var fakeScanner = new FakeScanner("abc", fakeCursor, nonWhiteSpaceAdvance: 0, nonWhiteSpaceOrNewLineAdvance: 0);
//         var fakeContext = new FakeParseContext(fakeScanner);
//         var parser = new NonWhiteSpaceLiteral(); // includeNewLines true so ReadNonWhiteSpaceOrNewLine is used.
//         var result = new FakeParseResult<TextSpan>();
//         // Act
//         bool parseResult = parser.Parse(fakeContext, ref result);
//         // Assert
//         Assert.False(parseResult);
//         Assert.Equal(0, result.Start);
//         Assert.Equal(0, result.End);
//         Assert.Equal(1, fakeContext.EnterCallCount);
//         Assert.Equal(1, fakeContext.ExitCallCount);
//     }
// 
//     /// <summary>
//     /// Tests that Parse returns true and sets the result correctly when characters are consumed using ReadNonWhiteSpaceOrNewLine.
//     /// </summary>
//     [Fact] [Error] (74-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?) [Error] (75-31)CS0246 The type or namespace name 'FakeScanner' could not be found (are you missing a using directive or an assembly reference?) [Error] (77-31)CS0246 The type or namespace name 'FakeParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (79-26)CS0246 The type or namespace name 'FakeParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenCharactersConsumedUsingIncludeNewLines_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         // Buffer with content; simulate consumption of 3 characters.
//         string buffer = "abc def";
//         var fakeCursor = new FakeCursor(0, buffer.Length);
//         var fakeScanner = new FakeScanner(buffer, fakeCursor, nonWhiteSpaceAdvance: 0, // not used
//  nonWhiteSpaceOrNewLineAdvance: 3);
//         var fakeContext = new FakeParseContext(fakeScanner);
//         var parser = new NonWhiteSpaceLiteral(); // includeNewLines = true
//         var result = new FakeParseResult<TextSpan>();
//         // Act
//         bool parseResult = parser.Parse(fakeContext, ref result);
//         // Assert
//         Assert.True(parseResult);
//         Assert.Equal(0, result.Start);
//         Assert.Equal(3, result.End);
//         Assert.Equal("abc", result.Value.Text);
//         Assert.Equal(1, fakeContext.EnterCallCount);
//         Assert.Equal(1, fakeContext.ExitCallCount);
//     }
// 
//     /// <summary>
//     /// Tests that Parse returns true and sets the result correctly when characters are consumed using ReadNonWhiteSpace.
//     /// </summary>
//     [Fact] [Error] (100-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?) [Error] (101-31)CS0246 The type or namespace name 'FakeScanner' could not be found (are you missing a using directive or an assembly reference?) [Error] (102-31)CS0246 The type or namespace name 'FakeParseContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (104-26)CS0246 The type or namespace name 'FakeParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenCharactersConsumedUsingNonIncludeNewLines_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         // Buffer with content; simulate consumption of 5 characters when newlines are not included.
//         string buffer = "hello world";
//         var fakeCursor = new FakeCursor(0, buffer.Length);
//         var fakeScanner = new FakeScanner(buffer, fakeCursor, nonWhiteSpaceAdvance: 5, nonWhiteSpaceOrNewLineAdvance: 0); // not used in this scenario
//         var fakeContext = new FakeParseContext(fakeScanner);
//         var parser = new NonWhiteSpaceLiteral(includeNewLines: false);
//         var result = new FakeParseResult<TextSpan>();
//         // Act
//         bool parseResult = parser.Parse(fakeContext, ref result);
//         // Assert
//         Assert.True(parseResult);
//         Assert.Equal(0, result.Start);
//         Assert.Equal(5, result.End);
//         Assert.Equal("hello", result.Value.Text);
//         Assert.Equal(1, fakeContext.EnterCallCount);
//         Assert.Equal(1, fakeContext.ExitCallCount);
//     }
// 
//     /// <summary>
//     /// Tests that Parse throws an exception when the context provided is null.
//     /// </summary>
//     [Fact] [Error] (124-26)CS0246 The type or namespace name 'FakeParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (126-9)CS0619 'Assert.Throws<T>(Func<Task>)' is obsolete: 'You must call Assert.ThrowsAsync<T> (and await the result) when testing async code.'
//     public void Parse_WhenContextIsNull_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var parser = new NonWhiteSpaceLiteral();
//         var result = new FakeParseResult<TextSpan>();
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => parser.Parse(null, ref result));
//     }
// 
//     /// <summary>
//     /// Tests the Compile method when _includeNewLines is true.
//     /// Verifies that the generated expression tree contains a call to ReadNonWhiteSpaceOrNewLine.
//     /// </summary>
//     [Fact] [Error] (138-31)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (147-77)CS0103 The name 'FakeCompilationContext' does not exist in the current context
//     public void Compile_WithIncludeNewLines_ExpressionTreeContainsReadNonWhiteSpaceOrNewLine()
//     {
//         // Arrange
//         var literal = new NonWhiteSpaceLiteral(includeNewLines: true);
//         var fakeContext = new FakeCompilationContext();
//         // Act
//         var result = literal.Compile(fakeContext);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotNull(result.Body);
//         Assert.Single(result.Body);
//         var ifThenExpr = result.Body[0] as ConditionalExpression;
//         Assert.NotNull(ifThenExpr);
//         bool containsCall = ExpressionTreeContainsMethod(ifThenExpr, nameof(FakeCompilationContext.ReadNonWhiteSpaceOrNewLine));
//         Assert.True(containsCall, "The expression tree should contain a call to ReadNonWhiteSpaceOrNewLine when includeNewLines is true.");
//     }
// 
//     /// <summary>
//     /// Tests the Compile method when _includeNewLines is false.
//     /// Verifies that the generated expression tree contains a call to ReadNonWhiteSpace.
//     /// </summary>
//     [Fact] [Error] (160-31)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (169-77)CS0103 The name 'FakeCompilationContext' does not exist in the current context
//     public void Compile_WithoutIncludeNewLines_ExpressionTreeContainsReadNonWhiteSpace()
//     {
//         // Arrange
//         var literal = new NonWhiteSpaceLiteral(includeNewLines: false);
//         var fakeContext = new FakeCompilationContext();
//         // Act
//         var result = literal.Compile(fakeContext);
//         // Assert
//         Assert.NotNull(result);
//         Assert.NotNull(result.Body);
//         Assert.Single(result.Body);
//         var ifThenExpr = result.Body[0] as ConditionalExpression;
//         Assert.NotNull(ifThenExpr);
//         bool containsCall = ExpressionTreeContainsMethod(ifThenExpr, nameof(FakeCompilationContext.ReadNonWhiteSpace));
//         Assert.True(containsCall, "The expression tree should contain a call to ReadNonWhiteSpace when includeNewLines is false.");
//     }
// 
//     /// <summary>
//     /// Recursively searches an expression tree for a method call with the specified method name.
//     /// </summary>
//     /// <param name = "expression">The expression tree to search.</param>
//     /// <param name = "methodName">The method name to look for.</param>
//     /// <returns>True if a method call with the given name is found; otherwise, false.</returns>
//     private static bool ExpressionTreeContainsMethod(Expression expression, string methodName)
//     {
//         if (expression == null)
//         {
//             return false;
//         }
// 
//         if (expression is MethodCallExpression methodCall)
//         {
//             if (methodCall.Method.Name == methodName)
//             {
//                 return true;
//             }
//         }
// 
//         // Recursively check sub-expressions
//         switch (expression)
//         {
//             case BlockExpression blockExpr:
//                 foreach (var exp in blockExpr.Expressions)
//                 {
//                     if (ExpressionTreeContainsMethod(exp, methodName))
//                     {
//                         return true;
//                     }
//                 }
// 
//                 if (blockExpr.Variables != null)
//                 {
//                     foreach (var variable in blockExpr.Variables)
//                     {
//                         if (ExpressionTreeContainsMethod(variable, methodName))
//                         {
//                             return true;
//                         }
//                     }
//                 }
// 
//                 break;
//             case ConditionalExpression condExpr:
//                 if (ExpressionTreeContainsMethod(condExpr.Test, methodName) || ExpressionTreeContainsMethod(condExpr.IfTrue, methodName) || ExpressionTreeContainsMethod(condExpr.IfFalse, methodName))
//                 {
//                     return true;
//                 }
// 
//                 break;
//             case BinaryExpression binaryExpr:
//                 if (ExpressionTreeContainsMethod(binaryExpr.Left, methodName) || ExpressionTreeContainsMethod(binaryExpr.Right, methodName))
//                 {
//                     return true;
//                 }
// 
//                 break;
//             case UnaryExpression unaryExpr:
//                 if (ExpressionTreeContainsMethod(unaryExpr.Operand, methodName))
//                 {
//                     return true;
//                 }
// 
//                 break;
//             case LambdaExpression lambdaExpr:
//                 if (ExpressionTreeContainsMethod(lambdaExpr.Body, methodName))
//                 {
//                     return true;
//                 }
// 
//                 break;
//         }
// 
//         return false;
//     }
// 
//     /// <summary>
//     /// Tests that the default constructor sets the Name property correctly and the _includeNewLines field to true.
//     /// </summary>
//     [Fact]
//     public void Constructor_DefaultParameter_SetsNameAndIncludeNewLinesTrue()
//     {
//         // Arrange & Act
//         var instance = new NonWhiteSpaceLiteral();
//         // Assert
//         Assert.NotNull(instance);
//         Assert.Equal("NonWhiteSpaceLiteral", instance.Name);
//         // Use reflection to verify the private field _includeNewLines is set to true.
//         FieldInfo fieldInfo = typeof(NonWhiteSpaceLiteral).GetField("_includeNewLines", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(fieldInfo);
//         bool includeNewLinesValue = (bool)fieldInfo.GetValue(instance);
//         Assert.True(includeNewLinesValue);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor with includeNewLines set to false sets the Name property correctly and the _includeNewLines field to false.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithIncludeNewLinesFalse_SetsNameAndIncludeNewLinesFalse()
//     {
//         // Arrange & Act
//         var instance = new NonWhiteSpaceLiteral(includeNewLines: false);
//         // Assert
//         Assert.NotNull(instance);
//         Assert.Equal("NonWhiteSpaceLiteral", instance.Name);
//         // Use reflection to verify the private field _includeNewLines is set to false.
//         FieldInfo fieldInfo = typeof(NonWhiteSpaceLiteral).GetField("_includeNewLines", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(fieldInfo);
//         bool includeNewLinesValue = (bool)fieldInfo.GetValue(instance);
//         Assert.False(includeNewLinesValue);
//     }
// }