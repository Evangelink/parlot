using Moq;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Dummy implementation for TextSpan to support testing.
    /// </summary>
//     public readonly struct TextSpan [Error] (14-28)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'TextSpan'
//     {
//         public string Buffer { get; }
//         public int Start { get; }
//         public int Length { get; }
// 
//         public TextSpan(string buffer, int start, int length)
//         {
//             Buffer = buffer;
//             Start = start;
//             Length = length;
//         }
//     }

    /// <summary>
    /// Dummy implementation for Character to support testing.
    /// Mimics decoding by returning the substring from the TextSpan.
    /// </summary>
    public static class Character
    {
        public static string DecodeString(TextSpan span)
        {
            if (span.Buffer == null || span.Start < 0 || span.Start + span.Length > span.Buffer.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            return span.Buffer.Substring(span.Start, span.Length);
        }
    }

    /// <summary>
    /// Dummy implementation of a cursor used by FakeScanner.
    /// </summary>
//     public class FakeCursor [Error] (47-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCursor'
//     {
//         public int Offset { get; set; }
//     }

    /// <summary>
    /// Dummy implementation of a scanner used to simulate parsing.
    /// </summary>
//     public class FakeScanner [Error] (55-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeScanner' [Error] (62-13)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer'
//     {
//         public string Buffer { get; set; }
//         public FakeCursor Cursor { get; set; } = new FakeCursor(); [Error] (58-54)CS1729 'FakeCursor' does not contain a constructor that takes 0 arguments
// 
//         public FakeScanner(string buffer)
//         {
//             Buffer = buffer;
//         }
// 
//         // Simulate reading a single quoted string.
//         public bool ReadSingleQuotedString() [Error] (68-17)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (68-39)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (68-60)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (70-17)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (70-33)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer'
//         {
//             if (Buffer.Length >= 2 && Buffer[0] == '\'' && Buffer[^1] == '\'')
//             {
//                 Cursor.Offset = Buffer.Length;
//                 return true;
//             }
//             return false;
//         }
// 
//         // Simulate reading a double quoted string.
//         public bool ReadDoubleQuotedString() [Error] (79-17)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (79-39)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (79-60)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (81-17)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (81-33)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer'
//         {
//             if (Buffer.Length >= 2 && Buffer[0] == '\"' && Buffer[^1] == '\"')
//             {
//                 Cursor.Offset = Buffer.Length;
//                 return true;
//             }
//             return false;
//         }
// 
//         // Simulate reading a backtick quoted string.
//         public bool ReadBacktickString() [Error] (90-17)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (90-39)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (90-59)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (92-17)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (92-33)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer'
//         {
//             if (Buffer.Length >= 2 && Buffer[0] == '`' && Buffer[^1] == '`')
//             {
//                 Cursor.Offset = Buffer.Length;
//                 return true;
//             }
//             return false;
//         }
// 
//         // Simulate reading a quoted string (either single or double) for SingleOrDouble quotes.
//         public bool ReadQuotedString() [Error] (101-17)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (101-41)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (101-62)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (101-86)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (101-107)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (103-17)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (103-33)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer'
//         {
//             if (Buffer.Length >= 2 && ((Buffer[0] == '\'' && Buffer[^1] == '\'') || (Buffer[0] == '\"' && Buffer[^1] == '\"')))
//             {
//                 Cursor.Offset = Buffer.Length;
//                 return true;
//             }
//             return false;
//         }
// 
//         // For custom expected chars.
//         public bool ReadQuotedString(char[] expectedChars) [Error] (112-17)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (112-62)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (112-76)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (112-90)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (114-17)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (114-33)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer'
//         {
//             if (Buffer.Length >= 2 && expectedChars.Contains(Buffer[0]) && Buffer[^1] == Buffer[0])
//             {
//                 Cursor.Offset = Buffer.Length;
//                 return true;
//             }
//             return false;
//         }
//     }

    /// <summary>
    /// Dummy implementation of a ParseContext for testing.
    /// </summary>
//     public class FakeParseContext [Error] (124-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseContext' [Error] (130-13)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner'
//     {
//         public FakeScanner Scanner { get; set; } [Error] (126-28)CS0108 'FakeParseContext.Scanner' hides inherited member 'ParseContext.Scanner'. Use the new keyword if hiding was intended.
// 
//         public FakeParseContext(string buffer)
//         {
//             Scanner = new FakeScanner(buffer);
//         }
// 
//         public void EnterParser(object parser) { } [Error] (133-21)CS0114 'FakeParseContext.EnterParser(object)' hides inherited member 'ParseContext.EnterParser(object)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (133-21)CS0111 Type 'FakeParseContext' already defines a member called 'EnterParser' with the same parameter types
//         public void ExitParser(object parser) { } [Error] (134-21)CS0114 'FakeParseContext.ExitParser(object)' hides inherited member 'ParseContext.ExitParser(object)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (134-21)CS0111 Type 'FakeParseContext' already defines a member called 'ExitParser' with the same parameter types
//     }

    /// <summary>
    /// Dummy implementation of a ParseResult to capture parsed results.
    /// </summary>
    /// <typeparam name="T"></typeparam>
//     public class FakeParseResult<T> [Error] (141-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseResult'
//     {
//         public int Start { get; private set; } [Error] (143-20)CS0108 'FakeParseResult<T>.Start' hides inherited member 'ParseResult<T>.Start'. Use the new keyword if hiding was intended.
//         public int End { get; private set; } [Error] (144-20)CS0108 'FakeParseResult<T>.End' hides inherited member 'ParseResult<T>.End'. Use the new keyword if hiding was intended.
//         public T Value { get; private set; } [Error] (145-18)CS0108 'FakeParseResult<T>.Value' hides inherited member 'ParseResult<T>.Value'. Use the new keyword if hiding was intended.
//         public bool Success { get; private set; } [Error] (146-21)CS0108 'FakeParseResult<T>.Success' hides inherited member 'ParseResult<T>.Success'. Use the new keyword if hiding was intended.
// 
//         public void Set(int start, int end, T value) [Error] (148-21)CS0114 'FakeParseResult<T>.Set(int, int, T)' hides inherited member 'ParseResult<T>.Set(int, int, T)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (148-21)CS0111 Type 'FakeParseResult<T>' already defines a member called 'Set' with the same parameter types [Error] (150-13)CS0229 Ambiguity between 'FakeParseResult<T>.Start' and 'FakeParseResult<T>.Start' [Error] (151-13)CS0229 Ambiguity between 'FakeParseResult<T>.End' and 'FakeParseResult<T>.End' [Error] (152-13)CS0229 Ambiguity between 'FakeParseResult<T>.Value' and 'FakeParseResult<T>.Value'
//         {
//             Start = start;
//             End = end;
//             Value = value;
//             Success = true;
//         }
//     }

    /// <summary>
    /// Dummy CompilationResult to support testing of Compile method.
    /// </summary>
//     public class FakeCompilationResult [Error] (160-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationResult'
//     {
//         public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
//         public List<Expression> Body { get; } = new List<Expression>();
//         public ParameterExpression Success { get; set; }
//         public ParameterExpression Value { get; set; }
//     }

    /// <summary>
    /// Dummy CompilationContext to support testing of Compile method.
    /// </summary>
//     public class FakeCompilationContext [Error] (171-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationContext'
//     {
//         private int _number = 1;
//         public int NextNumber => _number++; [Error] (174-20)CS0114 'FakeCompilationContext.NextNumber' hides inherited member 'CompilationContext.NextNumber'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
// 
//         public FakeCompilationResult CreateCompilationResult<T>() [Error] (176-38)CS0114 'FakeCompilationContext.CreateCompilationResult<T>()' hides inherited member 'CompilationContext.CreateCompilationResult<T>()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (176-38)CS0111 Type 'FakeCompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types [Error] (180-17)CS0229 Ambiguity between 'FakeCompilationResult.Success' and 'FakeCompilationResult.Success'
//         {
//             var result = new FakeCompilationResult
//             {
//                 Success = Expression.Parameter(typeof(bool), "success"),
//                 Value = Expression.Parameter(typeof(TextSpan), "value")
//             };
//             return result;
//         }
// 
//         public Expression Offset() [Error] (186-27)CS0111 Type 'FakeCompilationContext' already defines a member called 'Offset' with the same parameter types
//         {
//             // For testing, we simply return a constant.
//             return Expression.Constant(0);
//         }
// 
//         public Expression Buffer() [Error] (192-27)CS0114 'FakeCompilationContext.Buffer()' hides inherited member 'CompilationContext.Buffer()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (192-27)CS0111 Type 'FakeCompilationContext' already defines a member called 'Buffer' with the same parameter types
//         {
//             // For testing, return a dummy constant.
//             return Expression.Constant("dummy");
//         }
// 
//         public Expression NewTextSpan(Expression buffer, Expression start, Expression length) [Error] (198-27)CS0114 'FakeCompilationContext.NewTextSpan(Expression, Expression, Expression)' hides inherited member 'CompilationContext.NewTextSpan(Expression, Expression, Expression)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (198-27)CS0111 Type 'FakeCompilationContext' already defines a member called 'NewTextSpan' with the same parameter types
//         {
//             var ctor = typeof(TextSpan).GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });
//             return Expression.New(ctor, buffer, start, length);
//         }
// 
//         // Dummy methods for reading various strings. For testing, they return an expression that evaluates to true.
//         public Expression ReadSingleQuotedString()
//         {
//             return Expression.Constant(true);
//         }
// 
//         public Expression ReadDoubleQuotedString()
//         {
//             return Expression.Constant(true);
//         }
// 
//         public Expression ReadQuotedString()
//         {
//             return Expression.Constant(true);
//         }
// 
//         public Expression ReadBacktickString()
//         {
//             return Expression.Constant(true);
//         }
// 
//         public Expression ReadCustomString(Expression expectedChars)
//         {
//             return Expression.Constant(true);
//         }
//     }

    /// <summary>
    /// Unit tests for the <see cref="StringLiteral"/> class.
    /// </summary>
    public class StringLiteralTests
    {
        /// <summary>
        /// Tests the constructor with StringLiteralQuotes.Single to ensure ExpectedChars is set to a single quote.
        /// </summary>
        [Fact]
        public void Ctor_WithSingleQuoteEnum_SetsExpectedCharsCorrectly()
        {
            // Arrange
            var expected = new[] { '\'' };

            // Act
            var parser = new StringLiteral(StringLiteralQuotes.Single);

            // Assert
            Assert.Equal(expected, parser.ExpectedChars);
            Assert.Equal("StringLiteral", parser.Name);
        }

        /// <summary>
        /// Tests the constructor with StringLiteralQuotes.Double to ensure ExpectedChars is set to a double quote.
        /// </summary>
        [Fact]
        public void Ctor_WithDoubleQuoteEnum_SetsExpectedCharsCorrectly()
        {
            // Arrange
            var expected = new[] { '\"' };

            // Act
            var parser = new StringLiteral(StringLiteralQuotes.Double);

            // Assert
            Assert.Equal(expected, parser.ExpectedChars);
            Assert.Equal("StringLiteral", parser.Name);
        }

        /// <summary>
        /// Tests the constructor with a char parameter for a single quote.
        /// </summary>
        [Fact]
        public void Ctor_WithCharSingle_SetsExpectedCharsCorrectly()
        {
            // Arrange
            var expected = new[] { '\'' };

            // Act
            var parser = new StringLiteral('\'');

            // Assert
            Assert.Equal(StringLiteralQuotes.Single, GetQuoteType(parser));
            Assert.Equal(expected, parser.ExpectedChars);
            Assert.Equal("StringLiteral", parser.Name);
        }

        /// <summary>
        /// Tests the constructor with a char parameter for a double quote.
        /// </summary>
        [Fact]
        public void Ctor_WithCharDouble_SetsExpectedCharsCorrectly()
        {
            // Arrange
            var expected = new[] { '\"' };

            // Act
            var parser = new StringLiteral('\"');

            // Assert
            Assert.Equal(StringLiteralQuotes.Double, GetQuoteType(parser));
            Assert.Equal(expected, parser.ExpectedChars);
            Assert.Equal("StringLiteral", parser.Name);
        }

        /// <summary>
        /// Tests the CanSeek property to ensure it always returns true.
        /// </summary>
        [Fact]
        public void CanSeekProperty_Always_ReturnsTrue()
        {
            // Arrange
            var parser = new StringLiteral(StringLiteralQuotes.Single);

            // Act & Assert
            Assert.True(parser.CanSeek);
        }

        /// <summary>
        /// Tests the Parse method for a successful parsing scenario using single quotes.
        /// </summary>
//         [Fact] [Error] (332-59)CS0103 The name 'Unsafe' does not exist in the current context [Error] (332-123)CS1510 A ref or out value must be an assignable variable [Error] (337-47)CS0229 Ambiguity between 'FakeParseResult<string>.Value' and 'FakeParseResult<string>.Value'
//         public void Parse_SingleQuotedString_HappyPath_ReturnsTrueAndSetsValue()
//         {
//             // Arrange
//             string content = "'hello'";
//             var parseContext = new FakeParseContext(content);
//             var parseResult = new FakeParseResult<string>();
//             var parser = new StringLiteral(StringLiteralQuotes.Single);
// 
//             // Act
//             bool success = parser.Parse(parseContext, ref Unsafe.As<FakeParseResult<TextSpan>, ParseResult<TextSpan>>(ref UnsafeUtility.As<FakeParseResult<string>, ParseResult<TextSpan>>(ref parseResult)));
// 
//             // Assert
//             Assert.True(success);
//             // Expect the decoded value to be the content without the quotes: "hello"
//             Assert.Equal("hello", parseResult.Value);
//             Assert.True(parseResult.Success);
//         }

        /// <summary>
        /// Tests the Parse method in a failure scenario due to mismatched quotes.
        /// </summary>
//         [Fact] [Error] (355-59)CS0103 The name 'Unsafe' does not exist in the current context
//         public void Parse_MismatchedQuotes_ReturnsFalse()
//         {
//             // Arrange
//             // For single quotes expected, supply a double quoted string.
//             string content = "\"hello\"";
//             var parseContext = new FakeParseContext(content);
//             var parseResult = new FakeParseResult<string>();
//             var parser = new StringLiteral(StringLiteralQuotes.Single);
// 
//             // Act
//             bool success = parser.Parse(parseContext, ref Unsafe.As<FakeParseResult<TextSpan>, ParseResult<TextSpan>>(ref parseResult));
// 
//             // Assert
//             Assert.False(success);
//             Assert.False(parseResult.Success);
//         }

        /// <summary>
        /// Tests the Compile method to ensure it returns a valid CompilationResult for single quoted strings.
        /// </summary>
//         [Fact] [Error] (369-42)CS1729 'FakeCompilationContext' does not contain a constructor that takes 0 arguments [Error] (373-52)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (380-102)CS0117 'ExpressionType' does not contain a definition for 'IfThen'
//         public void Compile_SingleQuotedString_ReturnsCompilationResultWithExpectedExpressions()
//         {
//             // Arrange
//             var compilationContext = new FakeCompilationContext();
//             var parser = new StringLiteral(StringLiteralQuotes.Single);
// 
//             // Act
//             var compilationResult = parser.Compile(compilationContext);
// 
//             // Assert
//             Assert.NotNull(compilationResult);
//             Assert.NotEmpty(compilationResult.Variables);
//             Assert.NotEmpty(compilationResult.Body);
//             // We can check that one of the expressions in the body is an IfThen expression.
//             bool containsIfThen = compilationResult.Body.Any(expr => expr.NodeType == ExpressionType.IfThen);
//             Assert.True(containsIfThen);
//         }

        /// <summary>
        /// Helper method to retrieve the StringLiteralQuotes value from the parser instance using reflection.
        /// </summary>
        /// <param name="parser">The StringLiteral instance.</param>
        /// <returns>The determined StringLiteralQuotes value.</returns>
        private static StringLiteralQuotes GetQuoteType(StringLiteral parser)
        {
            // Since the _quotes field is private, we use reflection to access it for testing.
            var field = typeof(StringLiteral).GetField("_quotes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (StringLiteralQuotes)field.GetValue(parser)!;
        }
    }

    /// <summary>
    /// Dummy definitions to allow the test code to compile against the expected types.
    /// In a real scenario, these would be provided by the Parlot library.
    /// </summary>
    #region Dummy Base Classes and Interfaces

    // Parser base class dummy implementation.
//     public abstract class Parser<T> [Error] (404-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'Parser'
//     {
//         public string Name { get; protected set; }
//         public abstract bool Parse(object context, ref ParseResult<T> result);
//     }

    // Dummy ParseResult class.
//     public class ParseResult<T> [Error] (411-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ParseResult'
//     {
//         public int Start { get; private set; }
//         public int End { get; private set; }
//         public T Value { get; private set; }
//         public bool Success { get; private set; }
// 
//         public void Set(int start, int end, T value) [Error] (418-21)CS0111 Type 'ParseResult<T>' already defines a member called 'Set' with the same parameter types [Error] (420-13)CS0229 Ambiguity between 'ParseResult<T>.Start' and 'ParseResult<T>.Start' [Error] (421-13)CS0229 Ambiguity between 'ParseResult<T>.End' and 'ParseResult<T>.End' [Error] (422-13)CS0229 Ambiguity between 'ParseResult<T>.Value' and 'ParseResult<T>.Value' [Error] (423-13)CS0229 Ambiguity between 'ParseResult<T>.Success' and 'ParseResult<T>.Success'
//         {
//             Start = start;
//             End = end;
//             Value = value;
//             Success = true;
//         }
//     }

    // Dummy ICompilable interface.
//     public interface ICompilable [Error] (428-22)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ICompilable'
//     {
//         object Compile(object context);
//     }

    // Dummy ISeekable interface.
//     public interface ISeekable { } [Error] (434-22)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'ISeekable'

    #endregion

    /// <summary>
    /// Unsafe helper classes for casting between our FakeParseResult and the expected ParseResult.
    /// This is solely for testing purposes.
    /// </summary>
    public static class UnsafeUtility
    {
        public static TOutput As<TInput, TOutput>(ref TInput input) where TInput : class where TOutput : class
        {
            return input as TOutput;
        }
    }
}
