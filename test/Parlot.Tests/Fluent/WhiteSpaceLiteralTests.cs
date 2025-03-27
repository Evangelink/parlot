using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="WhiteSpaceLiteral"/> class.
    /// </summary>
    public class WhiteSpaceLiteralTests
    {
        /// <summary>
        /// Tests that Parse returns true when white space is present and _includeNewLines is false.
        /// </summary>
//         [Fact] [Error] (27-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (32-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (32-62)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (36-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (37-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (38-57)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value'
//         public void Parse_WhenWhiteSpacePresentAndIncludeNewLinesFalse_ReturnsTrue()
//         {
//             // Arrange
//             // Buffer has three white space characters followed by non-white-space characters.
//             string buffer = "   abc";
//             int initialOffset = 0;
//             // Configure the fake scanner to skip exactly 3 characters when SkipWhiteSpace is called.
//             var fakeScanner = new FakeScanner(buffer, initialOffset, skipDelta: 3);
//             var fakeContext = new FakeParseContext(fakeScanner);
//             var parser = new WhiteSpaceLiteral(includeNewLines: false);
//             var result = new FakeParseResult<TextSpan>();
// 
//             // Act
//             bool parseResult = parser.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.True(parseResult);
//             Assert.Equal(0, result.Start);
//             Assert.Equal(3, result.End);
//             Assert.Equal(buffer.Substring(0, 3), result.Value.ToString());
//         }

        /// <summary>
        /// Tests that Parse returns false when no white space is present (includeNewLines = false).
        /// </summary>
//         [Fact] [Error] (52-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (57-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (57-62)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (62-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (63-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (64-32)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value'
//         public void Parse_WhenNoWhiteSpacePresentAndIncludeNewLinesFalse_ReturnsFalse()
//         {
//             // Arrange
//             string buffer = "abc";
//             int initialOffset = 0;
//             // No whitespace to skip, so skipDelta is 0.
//             var fakeScanner = new FakeScanner(buffer, initialOffset, skipDelta: 0);
//             var fakeContext = new FakeParseContext(fakeScanner);
//             var parser = new WhiteSpaceLiteral(includeNewLines: false);
//             var result = new FakeParseResult<TextSpan>();
// 
//             // Act
//             bool parseResult = parser.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.False(parseResult);
//             // Since no advancement, start and end remain the same.
//             Assert.Equal(0, result.Start);
//             Assert.Equal(0, result.End);
//             Assert.Null(result.Value);
//         }

        /// <summary>
        /// Tests that Parse returns true when white space or new line is present and _includeNewLines is true.
        /// </summary>
//         [Fact] [Error] (78-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (83-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (83-62)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (87-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (88-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (89-57)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value'
//         public void Parse_WhenWhiteSpaceOrNewLinePresentAndIncludeNewLinesTrue_ReturnsTrue()
//         {
//             // Arrange
//             string buffer = "\r\n\tdef";
//             int initialOffset = 0;
//             // Configure the fake scanner to skip exactly 3 characters (e.g., newline and tab) when SkipWhiteSpaceOrNewLine is called.
//             var fakeScanner = new FakeScanner(buffer, initialOffset, skipDelta: 3);
//             var fakeContext = new FakeParseContext(fakeScanner);
//             var parser = new WhiteSpaceLiteral(includeNewLines: true);
//             var result = new FakeParseResult<TextSpan>();
// 
//             // Act
//             bool parseResult = parser.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.True(parseResult);
//             Assert.Equal(0, result.Start);
//             Assert.Equal(3, result.End);
//             Assert.Equal(buffer.Substring(0, 3), result.Value.ToString());
//         }

        /// <summary>
        /// Tests that Parse returns false when no white space or new line is present (includeNewLines = true).
        /// </summary>
//         [Fact] [Error] (102-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (107-45)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (107-62)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (111-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (112-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (113-32)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value'
//         public void Parse_WhenNoWhiteSpaceOrNewLinePresentAndIncludeNewLinesTrue_ReturnsFalse()
//         {
//             // Arrange
//             string buffer = "def";
//             int initialOffset = 0;
//             var fakeScanner = new FakeScanner(buffer, initialOffset, skipDelta: 0);
//             var fakeContext = new FakeParseContext(fakeScanner);
//             var parser = new WhiteSpaceLiteral(includeNewLines: true);
//             var result = new FakeParseResult<TextSpan>();
// 
//             // Act
//             bool parseResult = parser.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.False(parseResult);
//             Assert.Equal(0, result.Start);
//             Assert.Equal(0, result.End);
//             Assert.Null(result.Value);
//         }

        /// <summary>
        /// Tests that Compile returns a valid CompilationResult and adds the expected expressions when _includeNewLines is false.
        /// </summary>
//         [Fact] [Error] (127-55)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (132-36)CS0229 Ambiguity between 'CompilationResult.Body' and 'CompilationResult.Body'
//         public void Compile_WhenCalledWithIncludeNewLinesFalse_ReturnsCompilationResultWithExpectedExpressions()
//         {
//             // Arrange
//             var fakeContext = new FakeCompilationContext(skipNewLines: false);
//             var parser = new WhiteSpaceLiteral(includeNewLines: false);
// 
//             // Act
//             CompilationResult result = parser.Compile(fakeContext);
// 
//             // Assert
//             Assert.NotNull(result);
//             // Expecting two expression additions in the Body.
//             Assert.Equal(2, result.Body.Count);
//             // Verify that the first expression comes from SkipWhiteSpace.
//             Assert.Equal("SkipWhiteSpace", fakeContext.LastSkipMethod);
//         }

        /// <summary>
        /// Tests that Compile returns a valid CompilationResult and adds the expected expressions when _includeNewLines is true.
        /// </summary>
//         [Fact] [Error] (148-55)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (152-36)CS0229 Ambiguity between 'CompilationResult.Body' and 'CompilationResult.Body'
//         public void Compile_WhenCalledWithIncludeNewLinesTrue_ReturnsCompilationResultWithExpectedExpressions()
//         {
//             // Arrange
//             var fakeContext = new FakeCompilationContext(skipNewLines: true);
//             var parser = new WhiteSpaceLiteral(includeNewLines: true);
// 
//             // Act
//             CompilationResult result = parser.Compile(fakeContext);
// 
//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(2, result.Body.Count);
//             // Verify that the first expression comes from SkipWhiteSpaceOrNewLine.
//             Assert.Equal("SkipWhiteSpaceOrNewLine", fakeContext.LastSkipMethod);
//         }
    }

    #region Fake Implementations for Parse

    /// <summary>
    /// A fake implementation of a cursor used in scanning.
    /// </summary>
//     internal class FakeCursor [Error] (163-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCursor'
//     {
//         public int Offset { get; set; }
//     }

    /// <summary>
    /// A fake implementation of a scanner used in parsing.
    /// </summary>
//     internal class FakeScanner [Error] (171-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeScanner' [Error] (180-13)CS0229 Ambiguity between 'FakeScanner.Buffer' and 'FakeScanner.Buffer' [Error] (181-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor' [Error] (181-26)CS1729 'FakeCursor' does not contain a constructor that takes 0 arguments [Error] (181-39)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
//     {
//         public string Buffer { get; }
//         public FakeCursor Cursor { get; }
// 
//         private readonly int _skipDelta;
// 
//         public FakeScanner(string buffer, int initialOffset, int skipDelta)
//         {
//             Buffer = buffer;
//             Cursor = new FakeCursor { Offset = initialOffset };
//             _skipDelta = skipDelta;
//         }
// 
//         /// <summary>
//         /// Simulates skipping white space (but not new lines) by advancing the cursor offset.
//         /// </summary>
//         public void SkipWhiteSpace() [Error] (190-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor'
//         {
//             Cursor.Offset += _skipDelta;
//         }
// 
//         /// <summary>
//         /// Simulates skipping white space including new lines by advancing the cursor offset.
//         /// </summary>
//         public void SkipWhiteSpaceOrNewLine() [Error] (198-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor'
//         {
//             Cursor.Offset += _skipDelta;
//         }
//     }

    /// <summary>
    /// A fake implementation of ParseContext.
    /// </summary>
//     internal class FakeParseContext : ParseContext [Error] (205-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseContext' [Error] (207-16)CS0111 Type 'FakeParseContext' already defines a member called 'FakeParseContext' with the same parameter types [Error] (209-13)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner'
//     {
//         public FakeParseContext(FakeScanner scanner)
//         {
//             Scanner = scanner;
//         }
// 
//         public override void EnterParser(object parser) { /* No-op for testing */ } [Error] (212-30)CS0111 Type 'FakeParseContext' already defines a member called 'EnterParser' with the same parameter types
//         public override void ExitParser(object parser) { /* No-op for testing */ } [Error] (213-30)CS0111 Type 'FakeParseContext' already defines a member called 'ExitParser' with the same parameter types
//     }

    /// <summary>
    /// A fake implementation of ParseResult for capturing parsing result details.
    /// </summary>
    /// <typeparam name="T">The type of the parsed value.</typeparam>
//     internal class FakeParseResult<T> : ParseResult<T> where T : class [Error] (220-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseResult'
//     {
//         public int Start { get; private set; } [Error] (222-20)CS0108 'FakeParseResult<T>.Start' hides inherited member 'ParseResult<T>.Start'. Use the new keyword if hiding was intended.
//         public int End { get; private set; } [Error] (223-20)CS0108 'FakeParseResult<T>.End' hides inherited member 'ParseResult<T>.End'. Use the new keyword if hiding was intended.
//         public T Value { get; private set; } [Error] (224-18)CS0108 'FakeParseResult<T>.Value' hides inherited member 'ParseResult<T>.Value'. Use the new keyword if hiding was intended.
// 
//         public override void Set(int start, int end, T value) [Error] (226-30)CS0462 The inherited members 'ParseResult<T>.Set(int, int, T)' and 'ParseResult<T>.Set(int, int, T)' have the same signature in type 'FakeParseResult<T>', so they cannot be overridden [Error] (226-30)CS0111 Type 'FakeParseResult<T>' already defines a member called 'Set' with the same parameter types [Error] (228-13)CS0229 Ambiguity between 'FakeParseResult<T>.Start' and 'FakeParseResult<T>.Start' [Error] (229-13)CS0229 Ambiguity between 'FakeParseResult<T>.End' and 'FakeParseResult<T>.End' [Error] (230-13)CS0229 Ambiguity between 'FakeParseResult<T>.Value' and 'FakeParseResult<T>.Value'
//         {
//             Start = start;
//             End = end;
//             Value = value;
//         }
//     }

    /// <summary>
    /// A minimal implementation of TextSpan for testing purposes.
    /// </summary>
//     public class TextSpan [Error] (237-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'TextSpan' [Error] (243-16)CS0111 Type 'TextSpan' already defines a member called 'TextSpan' with the same parameter types
//     {
//         private readonly string _buffer;
//         private readonly int _start;
//         private readonly int _length;
// 
//         public TextSpan(string buffer, int start, int length)
//         {
//             _buffer = buffer;
//             _start = start;
//             _length = length;
//         }
// 
//         /// <summary>
//         /// Returns the substring represented by this TextSpan.
//         /// </summary>
//         public override string ToString() [Error] (253-32)CS0111 Type 'TextSpan' already defines a member called 'ToString' with the same parameter types
//         {
//             if (_buffer == null || _start < 0 || _length < 0 || _start + _length > _buffer.Length)
//             {
//                 return string.Empty;
//             }
//             return _buffer.Substring(_start, _length);
//         }
//     }
    #endregion

    #region Fake Implementations for Compilation

    /// <summary>
    /// A fake implementation of CompilationResult for testing purposes.
    /// </summary>
//     public class CompilationResult : CompilationResultBase [Error] (269-18)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'CompilationResult' [Error] (273-13)CS0229 Ambiguity between 'CompilationResult.Body' and 'CompilationResult.Body'
//     {
//         public CompilationResult()
//         {
//             Body = new List<Expression>();
//         }
// 
//         public override IList<Expression> Body { get; }
//     }

    /// <summary>
    /// Base class for compilation results.
    /// </summary>
    public abstract class CompilationResultBase
    {
        public abstract IList<Expression> Body { get; }
        public Expression Success { get; set; }
        public Expression Value { get; set; }
    }

    /// <summary>
    /// A fake implementation of CompilationContext for testing purposes.
    /// </summary>
//     internal class FakeCompilationContext : CompilationContext [Error] (292-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationContext'
//     {
//         private int _variableCounter = 0;
//         public string LastSkipMethod { get; set; }
//         public bool DiscardResult { get; set; } = false; [Error] (296-21)CS0114 'FakeCompilationContext.DiscardResult' hides inherited member 'CompilationContext.DiscardResult'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
//         private readonly bool _skipNewLines;
// 
//         public FakeCompilationContext(bool skipNewLines)
//         {
//             _skipNewLines = skipNewLines;
//             BufferValue = "dummy buffer";
//         }
// 
//         public string BufferValue { get; }
// 
//         public override CompilationResult CreateCompilationResult<T>() [Error] (307-43)CS0111 Type 'FakeCompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types [Error] (309-20)CS0144 Cannot create an instance of the abstract type or interface 'CompilationResult'
//         {
//             return new CompilationResult();
//         }
// 
//         public override Expression DeclareOffsetVariable(CompilationResult result)
//         {
//             // return a new parameter expression and increment counter.
//             return Expression.Parameter(typeof(int), "offset" + _variableCounter++);
//         }
// 
//         public override Expression SkipWhiteSpace()
//         {
//             LastSkipMethod = "SkipWhiteSpace";
//             // Return a constant expression for testing.
//             return Expression.Constant("SkipWhiteSpaceCalled");
//         }
// 
//         public override Expression SkipWhiteSpaceOrNewLine()
//         {
//             LastSkipMethod = "SkipWhiteSpaceOrNewLine";
//             return Expression.Constant("SkipWhiteSpaceOrNewLineCalled");
//         }
// 
//         public override Expression Buffer() [Error] (331-36)CS0111 Type 'FakeCompilationContext' already defines a member called 'Buffer' with the same parameter types
//         {
//             return Expression.Constant(BufferValue);
//         }
// 
//         public override Expression NewTextSpan(Expression buffer, Expression start, Expression length) [Error] (336-36)CS0111 Type 'FakeCompilationContext' already defines a member called 'NewTextSpan' with the same parameter types
//         {
//             // Return a simple constant expression representing creation of new TextSpan.
//             return Expression.Constant("NewTextSpanCreated");
//         }
//     }

    /// <summary>
    /// Base class for CompilationContext.
    /// </summary>
//     public abstract class CompilationContext [Error] (346-27)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'CompilationContext'
//     {
//         public abstract CompilationResult CreateCompilationResult<T>(); [Error] (348-43)CS0111 Type 'CompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types
//         public abstract Expression DeclareOffsetVariable(CompilationResult result);
//         public abstract Expression SkipWhiteSpace();
//         public abstract Expression SkipWhiteSpaceOrNewLine();
//         public abstract Expression Buffer();
//         public abstract Expression NewTextSpan(Expression buffer, Expression start, Expression length);
//         public virtual bool DiscardResult { get; set; }
//     }
    #endregion
}
