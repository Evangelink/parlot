using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="NonWhiteSpaceLiteral"/> class.
    /// </summary>
    public class NonWhiteSpaceLiteralTests
    {
        /// <summary>
        /// Tests that Parse returns false when the scanner is at EOF.
        /// Expected: Parse does not attempt any consumption and returns false.
        /// </summary>
//         [Fact] [Error] (22-34)CS1729 'FakeCursor' does not contain a constructor that takes 0 arguments [Error] (22-47)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset' [Error] (23-35)CS1729 'FakeScanner' does not contain a constructor that takes 0 arguments [Error] (25-17)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (26-17)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (31-35)CS1729 'FakeParseContext' does not contain a constructor that takes 0 arguments [Error] (31-54)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner' [Error] (36-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (36-62)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>'
//         public void Parse_WhenAtEof_ReturnsFalse()
//         {
//             // Arrange
//             var fakeCursor = new FakeCursor { Offset = 0, BufferLength = 0 };
//             var fakeScanner = new FakeScanner
//             {
//                 Buffer = string.Empty,
//                 Cursor = fakeCursor,
//                 // Values here are irrelevant since buffer is empty.
//                 NextOffsetForNonWhiteSpace = 0,
//                 NextOffsetForNonWhiteSpaceOrNewLine = 0
//             };
//             var fakeContext = new FakeParseContext { Scanner = fakeScanner };
//             var parser = new NonWhiteSpaceLiteral(); // default _includeNewLines = true.
//             var result = new FakeParseResult<TextSpan>();
// 
//             // Act
//             bool parseResult = parser.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.False(parseResult);
//             // Ensure that the parser entered and exited.
//             Assert.Contains(parser, fakeContext.EnteredParsers);
//             Assert.Contains(parser, fakeContext.ExitedParsers);
//         }

        /// <summary>
        /// Tests that Parse returns false when no characters are consumed.
        /// Expected: When the read method does not advance the cursor, Parse returns false.
        /// </summary>
//         [Fact] [Error] (53-34)CS1729 'FakeCursor' does not contain a constructor that takes 0 arguments [Error] (53-47)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset' [Error] (54-35)CS1729 'FakeScanner' does not contain a constructor that takes 0 arguments [Error] (56-17)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (57-17)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (62-35)CS1729 'FakeParseContext' does not contain a constructor that takes 0 arguments [Error] (62-54)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner' [Error] (68-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (68-62)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (73-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (74-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (75-32)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value'
//         public void Parse_WhenNoCharactersConsumed_ReturnsFalse()
//         {
//             // Arrange
//             var fakeCursor = new FakeCursor { Offset = 0, BufferLength = 10 };
//             var fakeScanner = new FakeScanner
//             {
//                 Buffer = "HelloWorld",
//                 Cursor = fakeCursor,
//                 // Simulate no consumption by leaving offset unchanged.
//                 NextOffsetForNonWhiteSpace = 0,
//                 NextOffsetForNonWhiteSpaceOrNewLine = 0
//             };
//             var fakeContext = new FakeParseContext { Scanner = fakeScanner };
//             // Create parser instance with includeNewLines = false.
//             var parser = new NonWhiteSpaceLiteral(includeNewLines: false);
//             var result = new FakeParseResult<TextSpan>();
// 
//             // Act
//             bool parseResult = parser.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.False(parseResult);
//             // Verify that result was not set.
//             Assert.Equal(0, result.Start);
//             Assert.Equal(0, result.End);
//             Assert.Null(result.Value);
//         }

        /// <summary>
        /// Tests that Parse returns true and correctly sets the result when characters are consumed.
        /// Expected: The parser advances the cursor and sets the TextSpan with the correctly consumed substring.
        /// </summary>
        /// <param name="includeNewLines">Indicates which reading method should be used.</param>
//         [Theory] [Error] (92-34)CS1729 'FakeCursor' does not contain a constructor that takes 0 arguments [Error] (92-47)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset' [Error] (93-35)CS1729 'FakeScanner' does not contain a constructor that takes 0 arguments [Error] (95-17)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (96-17)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (100-35)CS1729 'FakeParseContext' does not contain a constructor that takes 0 arguments [Error] (100-54)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner' [Error] (105-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (105-62)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (109-48)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (110-49)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (112-51)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value'
//         [InlineData(true)]
//         [InlineData(false)]
//         public void Parse_WhenCharactersAvailable_ReturnsTrueAndSetsResult(bool includeNewLines)
//         {
//             // Arrange
//             var initialOffset = 0;
//             var consumedOffset = 5;
//             var buffer = "HelloWorld";
//             var fakeCursor = new FakeCursor { Offset = initialOffset, BufferLength = buffer.Length };
//             var fakeScanner = new FakeScanner
//             {
//                 Buffer = buffer,
//                 Cursor = fakeCursor,
//                 NextOffsetForNonWhiteSpace = consumedOffset,
//                 NextOffsetForNonWhiteSpaceOrNewLine = consumedOffset
//             };
//             var fakeContext = new FakeParseContext { Scanner = fakeScanner };
//             var parser = new NonWhiteSpaceLiteral(includeNewLines);
//             var result = new FakeParseResult<TextSpan>();
// 
//             // Act
//             bool parseResult = parser.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.True(parseResult);
//             Assert.Equal(initialOffset, result.Start);
//             Assert.Equal(consumedOffset, result.End);
//             var expectedTextSpan = new TextSpan(buffer, initialOffset, consumedOffset - initialOffset);
//             Assert.Equal(expectedTextSpan, result.Value);
//         }

        /// <summary>
        /// Tests that Compile returns a compilation result with a non-empty expression body.
        /// Expected: The compilation result's Body should contain the generated expression logic.
        /// </summary>
//         [Fact] [Error] (123-46)CS1729 'FakeCompilationContext' does not contain a constructor that takes 0 arguments [Error] (123-71)CS0229 Ambiguity between 'FakeCompilationContext.DiscardResult' and 'FakeCompilationContext.DiscardResult' [Error] (127-52)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
//         public void Compile_ReturnsCompilationResultWithExpressions()
//         {
//             // Arrange
//             var fakeCompilationContext = new FakeCompilationContext { DiscardResult = false };
//             var parser = new NonWhiteSpaceLiteral(); // default includeNewLines = true
// 
//             // Act
//             var compilationResult = parser.Compile(fakeCompilationContext);
// 
//             // Assert
//             Assert.NotNull(compilationResult);
//             Assert.NotNull(compilationResult.Body);
//             Assert.NotEmpty(compilationResult.Body);
//         }
    }

    #region Fake Dependencies for Parse Tests

//     internal class FakeCursor [Error] (138-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCursor'
//     {
//         public int Offset { get; set; }
//         public int BufferLength { get; set; }
//         public bool Eof => Offset >= BufferLength; [Error] (142-28)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
//     }

//     internal class FakeScanner [Error] (145-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeScanner'
//     {
//         public string Buffer { get; set; }
//         public FakeCursor Cursor { get; set; }
//         public int NextOffsetForNonWhiteSpace { get; set; }
//         public int NextOffsetForNonWhiteSpaceOrNewLine { get; set; }
// 
//         public void ReadNonWhiteSpace() [Error] (154-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor'
//         {
//             Cursor.Offset = NextOffsetForNonWhiteSpace;
//         }
// 
//         public void ReadNonWhiteSpaceOrNewLine() [Error] (159-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor'
//         {
//             Cursor.Offset = NextOffsetForNonWhiteSpaceOrNewLine;
//         }
//     }

//     internal class FakeParseContext [Error] (163-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseContext'
//     {
//         public FakeScanner Scanner { get; set; } [Error] (165-28)CS0108 'FakeParseContext.Scanner' hides inherited member 'ParseContext.Scanner'. Use the new keyword if hiding was intended.
//         public List<object> EnteredParsers { get; } = new List<object>();
//         public List<object> ExitedParsers { get; } = new List<object>();
// 
//         public void EnterParser(object parser) [Error] (169-21)CS0114 'FakeParseContext.EnterParser(object)' hides inherited member 'ParseContext.EnterParser(object)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (169-21)CS0111 Type 'FakeParseContext' already defines a member called 'EnterParser' with the same parameter types
//         {
//             EnteredParsers.Add(parser);
//         }
// 
//         public void ExitParser(object parser) [Error] (174-21)CS0114 'FakeParseContext.ExitParser(object)' hides inherited member 'ParseContext.ExitParser(object)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (174-21)CS0111 Type 'FakeParseContext' already defines a member called 'ExitParser' with the same parameter types
//         {
//             ExitedParsers.Add(parser);
//         }
//     }

//     internal class FakeParseResult<T> [Error] (180-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseResult'
//     {
//         public int Start { get; private set; } [Error] (182-20)CS0108 'FakeParseResult<T>.Start' hides inherited member 'ParseResult<T>.Start'. Use the new keyword if hiding was intended.
//         public int End { get; private set; } [Error] (183-20)CS0108 'FakeParseResult<T>.End' hides inherited member 'ParseResult<T>.End'. Use the new keyword if hiding was intended.
//         public T Value { get; private set; } [Error] (184-18)CS0108 'FakeParseResult<T>.Value' hides inherited member 'ParseResult<T>.Value'. Use the new keyword if hiding was intended.
//         public void Set(int start, int end, T value) [Error] (185-21)CS0114 'FakeParseResult<T>.Set(int, int, T)' hides inherited member 'ParseResult<T>.Set(int, int, T)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (185-21)CS0111 Type 'FakeParseResult<T>' already defines a member called 'Set' with the same parameter types [Error] (187-13)CS0229 Ambiguity between 'FakeParseResult<T>.Start' and 'FakeParseResult<T>.Start' [Error] (188-13)CS0229 Ambiguity between 'FakeParseResult<T>.End' and 'FakeParseResult<T>.End' [Error] (189-13)CS0229 Ambiguity between 'FakeParseResult<T>.Value' and 'FakeParseResult<T>.Value'
//         {
//             Start = start;
//             End = end;
//             Value = value;
//         }
//     }

    #endregion

    #region Fake Dependencies for Compile Tests

//     internal class FakeCompilationContext [Error] (197-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationContext'
//     {
//         public bool DiscardResult { get; set; } [Error] (199-21)CS0114 'FakeCompilationContext.DiscardResult' hides inherited member 'CompilationContext.DiscardResult'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
//         public Expression Eof()
//         {
//             return Expression.Constant(false);
//         }
// 
//         public Expression Offset()
//         {
//             return Expression.Constant(0);
//         }
// 
//         public Expression ReadNonWhiteSpace()
//         {
//             return Expression.Empty();
//         }
// 
//         public Expression ReadNonWhiteSpaceOrNewLine()
//         {
//             return Expression.Empty();
//         }
// 
//         public Expression Buffer() [Error] (220-27)CS0114 'FakeCompilationContext.Buffer()' hides inherited member 'CompilationContext.Buffer()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (220-27)CS0111 Type 'FakeCompilationContext' already defines a member called 'Buffer' with the same parameter types
//         {
//             return Expression.Constant("dummy buffer");
//         }
// 
//         public Expression NewTextSpan(Expression buffer, ParameterExpression start, Expression length)
//         {
//             var methodInfo = typeof(FakeCompilationContext).GetMethod(nameof(CreateTextSpan), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
//             return Expression.Call(methodInfo, buffer, start, length);
//         }
// 
//         public static TextSpan CreateTextSpan(string buffer, int start, int length)
//         {
//             return new TextSpan(buffer, start, length);
//         }
// 
//         public FakeCompilationResult<T> CreateCompilationResult<T>() [Error] (236-41)CS0050 Inconsistent accessibility: return type 'FakeCompilationResult<T>' is less accessible than method 'FakeCompilationContext.CreateCompilationResult<T>()' [Error] (236-41)CS0114 'FakeCompilationContext.CreateCompilationResult<T>()' hides inherited member 'CompilationContext.CreateCompilationResult<T>()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (236-41)CS0111 Type 'FakeCompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types [Error] (238-24)CS0121 The call is ambiguous between the following methods or properties: 'FakeCompilationResult<T>.FakeCompilationResult()' and 'FakeCompilationResult<T>.FakeCompilationResult()'
//         {
//             return new FakeCompilationResult<T>();
//         }
//     }

//     internal class FakeCompilationResult<T> [Error] (242-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationResult'
//     {
//         public List<Expression> Body { get; } = new List<Expression>();
//         public ParameterExpression Success { get; } = Expression.Parameter(typeof(bool), "success");
//         public ParameterExpression Value { get; } = Expression.Parameter(typeof(T), "value");
//     }

    #endregion

    #region Minimal Implementation of TextSpan

    /// <summary>
    /// Minimal implementation of TextSpan for testing purposes.
    /// </summary>
    public class TextSpan
    {
        public string Buffer { get; }
        public int Start { get; }
        public int Length { get; }

        public TextSpan(string buffer, int start, int length)
        {
            Buffer = buffer;
            Start = start;
            Length = length;
        }

        public override bool Equals(object obj)
        {
            if (obj is TextSpan other)
            {
                return Buffer == other.Buffer && Start == other.Start && Length == other.Length;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Buffer, Start, Length).GetHashCode();
        }

        public override string ToString()
        {
            if (Buffer == null || Start < 0 || Start + Length > Buffer.Length)
            {
                return string.Empty;
            }
            return Buffer.Substring(Start, Length);
        }
    }

    #endregion
}
