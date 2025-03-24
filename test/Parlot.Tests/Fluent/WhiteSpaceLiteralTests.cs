using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="WhiteSpaceLiteral"/> class.
    /// </summary>
    [TestClass]
    public class WhiteSpaceLiteralTests
    {
        // These fake classes simulate the minimal behavior required by WhiteSpaceLiteral.Parse.

        /// <summary>
        /// A fake cursor to simulate scanner cursor movement.
        /// </summary>
        private class FakeCursor
        {
            public int Offset { get; set; }
        }

        /// <summary>
        /// A fake scanner that simulates skipping whitespaces.
        /// </summary>
        private class FakeScanner
        {
            public FakeCursor Cursor { get; }
            public string Buffer { get; }

            public FakeScanner(string buffer)
            {
                Buffer = buffer;
                Cursor = new FakeCursor { Offset = 0 };
            }

            /// <summary>
            /// Skips space and tab characters.
            /// </summary>
            public void SkipWhiteSpace()
            {
                while (Cursor.Offset < Buffer.Length && (Buffer[Cursor.Offset] == ' ' || Buffer[Cursor.Offset] == '\t'))
                {
                    Cursor.Offset++;
                }
            }

            /// <summary>
            /// Skips all whitespace characters including newlines.
            /// </summary>
            public void SkipWhiteSpaceOrNewLine()
            {
                while (Cursor.Offset < Buffer.Length && char.IsWhiteSpace(Buffer[Cursor.Offset]))
                {
                    Cursor.Offset++;
                }
            }
        }

        /// <summary>
        /// A fake ParseContext to supply a fake scanner.
        /// </summary>
        private class FakeParseContext : ParseContext
        {
            public FakeScanner ScannerInstance { get; }

            public FakeParseContext(string buffer)
            {
                ScannerInstance = new FakeScanner(buffer);
                // Assign the fake scanner to the base class property.
                Scanner = new FakeScannerAdapter(ScannerInstance);
            }

            public override void EnterParser(object parser)
            {
                // No-op for fake context.
            }

            public override void ExitParser(object parser)
            {
                // No-op for fake context.
            }
        }

        /// <summary>
        /// An adapter for FakeScanner to satisfy the expected interface of the ParseContext.
        /// </summary>
        private class FakeScannerAdapter : Scanner
        {
            private readonly FakeScanner _fakeScanner;

            public FakeScannerAdapter(FakeScanner fakeScanner)
            {
                _fakeScanner = fakeScanner;
            }

            public override ICursor Cursor => new FakeCursorAdapter(_fakeScanner.Cursor);

            public override string Buffer => _fakeScanner.Buffer;

            public override void SkipWhiteSpace()
            {
                _fakeScanner.SkipWhiteSpace();
            }

            public override void SkipWhiteSpaceOrNewLine()
            {
                _fakeScanner.SkipWhiteSpaceOrNewLine();
            }
        }

        /// <summary>
        /// An adapter for FakeCursor to satisfy the expected interface of the scanner.
        /// </summary>
        private class FakeCursorAdapter : ICursor
        {
            private readonly FakeCursor _cursor;

            public FakeCursorAdapter(FakeCursor cursor)
            {
                _cursor = cursor;
            }

            public int Offset => _cursor.Offset;
        }

        /// <summary>
        /// A fake ParseResult to capture the result of parsing.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        private class FakeParseResult<T> : ParseResult<T>
        {
            public int StartPosition { get; private set; }
            public int EndPosition { get; private set; }
            public override T Value { get; protected set; }

            public override void Set(int start, int end, T value)
            {
                StartPosition = start;
                EndPosition = end;
                Value = value;
            }
        }

        // The following fake classes simulate a minimal CompilationContext and CompilationResult.

        /// <summary>
        /// A fake compilation result to capture expressions.
        /// </summary>
        /// <typeparam name="T">Type of the compilation result value.</typeparam>
        private class FakeCompilationResult<T> : CompilationResult
        {
            public List<Expression> Body { get; } = new List<Expression>();
            public Expression Success { get; set; }
            public Expression Value { get; set; }
        }

        /// <summary>
        /// A fake compilation context to simulate compilation generation.
        /// </summary>
        private class FakeCompilationContext : CompilationContext
        {
            public bool DiscardResult { get; set; }

            public override CompilationResult CreateCompilationResult<T>()
            {
                return new FakeCompilationResult<T>();
            }

            public override Expression DeclareOffsetVariable(CompilationResult result)
            {
                // Return a constant expression representing an offset variable.
                return Expression.Constant(0);
            }

            public override Expression SkipWhiteSpace()
            {
                return Expression.Constant("SkippedWhiteSpace");
            }

            public override Expression SkipWhiteSpaceOrNewLine()
            {
                return Expression.Constant("SkippedWhiteSpaceOrNewLine");
            }

            public override Expression Buffer()
            {
                return Expression.Constant("TestBuffer");
            }

            public override Expression NewTextSpan(Expression bufferExpr, Expression start, Expression length)
            {
                return Expression.Constant("NewTextSpan");
            }
        }

        /// <summary>
        /// Tests the Parse method when whitespace is present and _includeNewLines is false.
        /// Expected to return true and set the result with the skipped whitespace.
        /// </summary>
        [TestMethod]
        public void Parse_WithSpacesAndNoNewLineFlag_ReturnsTrueAndSetsResult()
        {
            // Arrange
            string input = "   abc";
            var fakeContext = new FakeParseContext(input);
            var parser = new WhiteSpaceLiteral(includeNewLines: false);
            var parseResult = new FakeParseResult<TextSpan>();

            // Act
            bool success = parser.Parse(fakeContext, ref parseResult);

            // Assert
            Assert.IsTrue(success, "Expected Parse to return true when whitespace is present.");
            Assert.AreEqual(0, parseResult.StartPosition, "Start position should be 0.");
            Assert.AreEqual(3, parseResult.EndPosition, "End position should be 3 for three space characters.");
            Assert.AreEqual(input.Substring(0, 3), parseResult.Value.ToString(), "Parsed TextSpan content does not match expected whitespace.");
        }

        /// <summary>
        /// Tests the Parse method when no whitespace exists at the beginning.
        /// Expected to return false and leave the parse result untouched.
        /// </summary>
        [TestMethod]
        public void Parse_WithNoWhiteSpace_ReturnsFalse()
        {
            // Arrange
            string input = "abc";
            var fakeContext = new FakeParseContext(input);
            var parser = new WhiteSpaceLiteral(includeNewLines: false);
            var parseResult = new FakeParseResult<TextSpan>();

            // Act
            bool success = parser.Parse(fakeContext, ref parseResult);

            // Assert
            Assert.IsFalse(success, "Expected Parse to return false when no whitespace is present at beginning.");
        }

        /// <summary>
        /// Tests the Parse method when a newline character is present and _includeNewLines is true.
        /// Expected to return true and include the newline in the parsed result.
        /// </summary>
        [TestMethod]
        public void Parse_WithNewLineAndIncludeNewLinesFlag_ReturnsTrueAndSetsResult()
        {
            // Arrange
            string input = "\nxyz";
            var fakeContext = new FakeParseContext(input);
            var parser = new WhiteSpaceLiteral(includeNewLines: true);
            var parseResult = new FakeParseResult<TextSpan>();

            // Act
            bool success = parser.Parse(fakeContext, ref parseResult);

            // Assert
            Assert.IsTrue(success, "Expected Parse to return true when newline whitespace is present and includeNewLines is true.");
            // Expectation: newline (\n) is skipped.
            Assert.AreEqual(0, parseResult.StartPosition, "Start position should be 0.");
            Assert.AreEqual(1, parseResult.EndPosition, "End position should be 1 for a newline character.");
            Assert.AreEqual(input.Substring(0, 1), parseResult.Value.ToString(), "Parsed TextSpan content does not match expected newline.");
        }

        /// <summary>
        /// Tests the Compile method when _includeNewLines is false.
        /// Expected to generate a compilation result that uses SkipWhiteSpace.
        /// </summary>
        [TestMethod]
        public void Compile_ExcludeNewLines_UsesSkipWhiteSpace()
        {
            // Arrange
            var fakeContext = new FakeCompilationContext { DiscardResult = false };
            var parser = new WhiteSpaceLiteral(includeNewLines: false);

            // Act
            CompilationResult result = parser.Compile(fakeContext);

            // Assert
            Assert.IsNotNull(result, "Compilation result should not be null.");
            var fakeResult = result as FakeCompilationResult<TextSpan>;
            Assert.IsNotNull(fakeResult, "Result should be of type FakeCompilationResult<TextSpan>.");
            // The first expression in the body should be the call to SkipWhiteSpace
            Assert.IsTrue(fakeResult.Body.Count >= 2, "Compilation result.Body should contain at least two expressions.");
            var constantExpr = fakeResult.Body[0] as ConstantExpression;
            Assert.IsNotNull(constantExpr, "First expression should be a ConstantExpression.");
            Assert.AreEqual("SkippedWhiteSpace", constantExpr.Value, "Expected SkipWhiteSpace call in compilation body when includeNewLines is false.");
        }

        /// <summary>
        /// Tests the Compile method when _includeNewLines is true.
        /// Expected to generate a compilation result that uses SkipWhiteSpaceOrNewLine.
        /// </summary>
        [TestMethod]
        public void Compile_IncludeNewLines_UsesSkipWhiteSpaceOrNewLine()
        {
            // Arrange
            var fakeContext = new FakeCompilationContext { DiscardResult = false };
            var parser = new WhiteSpaceLiteral(includeNewLines: true);

            // Act
            CompilationResult result = parser.Compile(fakeContext);

            // Assert
            Assert.IsNotNull(result, "Compilation result should not be null.");
            var fakeResult = result as FakeCompilationResult<TextSpan>;
            Assert.IsNotNull(fakeResult, "Result should be of type FakeCompilationResult<TextSpan>.");
            // The first expression in the body should be the call to SkipWhiteSpaceOrNewLine
            Assert.IsTrue(fakeResult.Body.Count >= 2, "Compilation result.Body should contain at least two expressions.");
            var constantExpr = fakeResult.Body[0] as ConstantExpression;
            Assert.IsNotNull(constantExpr, "First expression should be a ConstantExpression.");
            Assert.AreEqual("SkippedWhiteSpaceOrNewLine", constantExpr.Value, "Expected SkipWhiteSpaceOrNewLine call in compilation body when includeNewLines is true.");
        }
    }
}
