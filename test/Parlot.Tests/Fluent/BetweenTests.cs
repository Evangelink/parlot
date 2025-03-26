using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Rewriting;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Between{A, T, B}"/> class.
    /// </summary>
    public class BetweenTests
    {
        private readonly int _initialCursorPosition = 10;

        #region Constructor Tests

        /// <summary>
        /// Tests that the constructor throws ArgumentNullException when the 'before' parser is null.
        /// </summary>
        [Fact]
        public void Constructor_NullBefore_ThrowsArgumentNullException()
        {
            // Arrange
            Parser<int> validParser = new FakeParser<int>(true, 0);
            Parser<object> validAfter = new FakeParser<object>(true, null);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Between<object, int, object>(null, validParser, validAfter));
            Assert.Equal("before", exception.ParamName);
        }

        /// <summary>
        /// Tests that the constructor throws ArgumentNullException when the 'parser' argument is null.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange
            Parser<object> validBefore = new FakeParser<object>(true, null);
            Parser<object> validAfter = new FakeParser<object>(true, null);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Between<object, int, object>(validBefore, null, validAfter));
            Assert.Equal("parser", exception.ParamName);
        }

        /// <summary>
        /// Tests that the constructor throws ArgumentNullException when the 'after' parser is null.
        /// </summary>
        [Fact]
        public void Constructor_NullAfter_ThrowsArgumentNullException()
        {
            // Arrange
            Parser<object> validBefore = new FakeParser<object>(true, null);
            Parser<int> validParser = new FakeParser<int>(true, 0);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Between<object, int, object>(validBefore, validParser, null));
            Assert.Equal("after", exception.ParamName);
        }

        /// <summary>
        /// Tests that the properties CanSeek, ExpectedChars, and SkipWhitespace are set from the 'before' parser when it implements ISeekable.
        /// </summary>
        [Fact]
        public void Constructor_BeforeImplementsISeekable_SetsPropertiesFromBefore()
        {
            // Arrange
            var expectedCanSeek = true;
            char[] expectedChars = new[] { '(', '{' };
            var expectedSkipWhitespace = false;
            var seekableBefore = new FakeSeekableParser<object>(true, null, expectedCanSeek, expectedChars, expectedSkipWhitespace);
            Parser<int> validParser = new FakeParser<int>(true, 42);
            Parser<object> validAfter = new FakeParser<object>(true, null);

            // Act
            var between = new Between<object, int, object>(seekableBefore, validParser, validAfter);

            // Assert
            Assert.Equal(expectedCanSeek, between.CanSeek);
            Assert.Equal(expectedChars, between.ExpectedChars);
            Assert.Equal(expectedSkipWhitespace, between.SkipWhitespace);
        }

        #endregion

        #region Parse Method Tests

        /// <summary>
        /// Tests that Parse returns true and sets the result value when all parsers succeed.
        /// </summary>
        [Fact]
        public void Parse_AllParsersSucceed_ReturnsTrueAndSetsResultValue()
        {
            // Arrange
            int expectedValue = 123;
            var beforeParser = new FakeParser<string>(true, "before");
            var mainParser = new FakeParser<int>(true, expectedValue);
            var afterParser = new FakeParser<double>(true, 3.14);
            var between = new Between<string, int, double>(beforeParser, mainParser, afterParser);

            var context = new FakeParseContext(_initialCursorPosition);
            ParseResult<int> result = new ParseResult<int>();

            // Act
            bool parseOutcome = between.Parse(context, ref result);

            // Assert
            Assert.True(parseOutcome);
            Assert.Equal(expectedValue, result.Value);
            // Cursor position should remain advanced (simulate that parsers advanced the cursor themselves)
            Assert.Equal(_initialCursorPosition, context.Scanner.Cursor.Position);
        }

        /// <summary>
        /// Tests that Parse returns false when the 'before' parser fails.
        /// </summary>
        [Fact]
        public void Parse_BeforeParserFails_ReturnsFalse()
        {
            // Arrange
            var beforeParser = new FakeParser<string>(false, default);
            var mainParser = new FakeParser<int>(true, 456); // should not be called
            var afterParser = new FakeParser<double>(true, 0.0); // should not be called
            var between = new Between<string, int, double>(beforeParser, mainParser, afterParser);

            var context = new FakeParseContext(_initialCursorPosition);
            ParseResult<int> result = new ParseResult<int>();

            // Act
            bool parseOutcome = between.Parse(context, ref result);

            // Assert
            Assert.False(parseOutcome);
            // Since before parser failed, the position is expected to be maintained by the parser itself.
            Assert.Equal(_initialCursorPosition, context.Scanner.Cursor.Position);
        }

        /// <summary>
        /// Tests that Parse resets the cursor position when the main parser fails.
        /// </summary>
        [Fact]
        public void Parse_MainParserFails_ResetsCursorPositionAndReturnsFalse()
        {
            // Arrange
            var beforeParser = new FakeParser<string>(true, "abc");
            var mainParser = new FakeParser<int>(false, default);
            var afterParser = new FakeParser<double>(true, 0.0); // should not be called
            var between = new Between<string, int, double>(beforeParser, mainParser, afterParser);

            var context = new FakeParseContext(_initialCursorPosition);
            // Advance cursor to simulate that before parser advanced it.
            context.Scanner.Cursor.Position = _initialCursorPosition + 5;
            ParseResult<int> result = new ParseResult<int>();

            // Act
            bool parseOutcome = between.Parse(context, ref result);

            // Assert
            Assert.False(parseOutcome);
            // The cursor should be reset to the starting position
            Assert.Equal(_initialCursorPosition + 5, context.LastResetPosition);
        }

        /// <summary>
        /// Tests that Parse resets the cursor position when the 'after' parser fails.
        /// </summary>
        [Fact]
        public void Parse_AfterParserFails_ResetsCursorPositionAndReturnsFalse()
        {
            // Arrange
            var beforeParser = new FakeParser<string>(true, "start");
            var mainParser = new FakeParser<int>(true, 789);
            var afterParser = new FakeParser<double>(false, default);
            var between = new Between<string, int, double>(beforeParser, mainParser, afterParser);

            var context = new FakeParseContext(_initialCursorPosition);
            // Advance cursor to simulate that parsers advanced it.
            context.Scanner.Cursor.Position = _initialCursorPosition + 10;
            ParseResult<int> result = new ParseResult<int>();

            // Act
            bool parseOutcome = between.Parse(context, ref result);

            // Assert
            Assert.False(parseOutcome);
            // The cursor should be reset to the position before parsing main part
            Assert.Equal(_initialCursorPosition + 10, context.LastResetPosition);
        }

        #endregion

        #region Compile Method Tests

        /// <summary>
        /// Tests that Compile returns a valid CompilationResult and adds a body expression.
        /// </summary>
        [Fact]
        public void Compile_ValidBuildResults_ReturnsCompilationResultWithBody()
        {
            // Arrange
            // Using fake build results that simulate successful compiles.
            var fakeBefore = new FakeParserForCompile<string>(CreateFakeCompileResult("before"));
            var fakeMain = new FakeParserForCompile<int>(CreateFakeCompileResult(100));
            var fakeAfter = new FakeParserForCompile<double>(CreateFakeCompileResult(3.14));

            var between = new Between<string, int, double>(fakeBefore, fakeMain, fakeAfter);
            var context = new FakeCompilationContext();

            // Act
            CompilationResult compileResult = between.Compile(context);

            // Assert
            Assert.NotNull(compileResult);
            Assert.NotEmpty(compileResult.Body);
        }

        #endregion

        #region ToString Tests

        /// <summary>
        /// Tests that ToString returns the proper string representation of the Between instance.
        /// </summary>
        [Fact]
        public void ToString_NoNameSet_ReturnsFormattedString()
        {
            // Arrange
            var beforeParser = new FakeParser<string>(true, "x");
            var mainParser = new FakeParser<int>(true, 1);
            var afterParser = new FakeParser<double>(true, 2.0);
            var between = new Between<string, int, double>(beforeParser, mainParser, afterParser);

            // Act
            string toStringValue = between.ToString();

            // Assert
            string expectedSubstring = $"Between({beforeParser},{mainParser},{afterParser})";
            Assert.Equal(expectedSubstring, toStringValue);
        }

        #endregion

        #region Helper Classes

        /// <summary>
        /// A fake implementation of Parser&lt;T&gt; for testing Parse method behavior.
        /// </summary>
        /// <typeparam name="T">The type of the parse result.</typeparam>
        private class FakeParser<T> : Parser<T>
        {
            private readonly bool _shouldSucceed;
            private readonly T _value;

            public FakeParser(bool shouldSucceed, T value)
            {
                _shouldSucceed = shouldSucceed;
                _value = value;
            }

            public override bool Parse(ParseContext context, ref ParseResult<T> result)
            {
                // Simulate advancing the cursor for a successful parse.
                if (_shouldSucceed)
                {
                    result.Value = _value;
                    return true;
                }

                return false;
            }

            public override string ToString() => _value?.ToString() ?? "null";
        }

        /// <summary>
        /// A fake implementation of a parser that implements ISeekable.
        /// </summary>
        /// <typeparam name="T">The type of the parse result.</typeparam>
        private class FakeSeekableParser<T> : FakeParser<T>, ISeekable
        {
            public FakeSeekableParser(bool shouldSucceed, T value, bool canSeek, char[] expectedChars, bool skipWhitespace)
                : base(shouldSucceed, value)
            {
                CanSeek = canSeek;
                ExpectedChars = expectedChars;
                SkipWhitespace = skipWhitespace;
            }

            public bool CanSeek { get; }
            public char[] ExpectedChars { get; }
            public bool SkipWhitespace { get; }
        }

        /// <summary>
        /// A fake implementation of Parser&lt;T&gt; for compile tests.
        /// </summary>
        /// <typeparam name="T">The type of the compile result.</typeparam>
        private class FakeParserForCompile<T> : Parser<T>, ICompilable
        {
            private readonly CompilationResult _fakeResult;

            public FakeParserForCompile(CompilationResult fakeResult)
            {
                _fakeResult = fakeResult;
            }

            public override bool Parse(ParseContext context, ref ParseResult<T> result)
            {
                // Not needed for compile tests.
                throw new NotImplementedException();
            }

            public CompilationResult Compile(CompilationContext context)
            {
                return _fakeResult;
            }

            public CompilationResult Build(CompilationContext context)
            {
                // Return the provided fake compilation result.
                return _fakeResult;
            }

            public override string ToString() => "FakeParserForCompile";
        }

        /// <summary>
        /// Creates a fake CompilationResult with dummy expression lists.
        /// </summary>
        /// <typeparam name="T">The type parameter for the compilation result.</typeparam>
        /// <param name="dummyValue">A dummy value used to create a value expression.</param>
        /// <returns>A fake CompilationResult.</returns>
        private static CompilationResult CreateFakeCompileResult<T>(T dummyValue)
        {
            var result = new CompilationResult
            {
                // Create dummy variables and body
                Variables = new List<ParameterExpression>(),
                Body = new List<Expression>(),
                // Dummy success and value assignments
                Success = Expression.Parameter(typeof(bool), "success"),
                Value = Expression.Parameter(typeof(T), "value")
            };
            // Add a dummy expression block to simulate compile instructions.
            result.Body.Add(Expression.Constant(dummyValue));
            return result;
        }

        /// <summary>
        /// A fake implementation of ParseContext for testing Parse method behavior.
        /// </summary>
        private class FakeParseContext : ParseContext
        {
            public FakeParseContext(int cursorPosition)
            {
                Scanner = new FakeScanner(cursorPosition);
            }

            public int LastResetPosition { get; private set; } = -1;

            public override void EnterParser(ParserBase parser)
            {
                // No-op for fake
            }

            public override void ExitParser(ParserBase parser)
            {
                // No-op for fake
            }

            // Override the scanner setter to capture position reset calls.
            public new FakeScanner Scanner { get; }

            // Capture ResetPosition calls through the Cursor.
            public class FakeScanner : IScanner
            {
                public FakeScanner(int position)
                {
                    Cursor = new FakeCursor(position, OnReset);
                }

                public FakeCursor Cursor { get; }

                private void OnReset(int pos)
                {
                    // No action required - handled in FakeParseContext via cursor callback below.
                }
            }

            /// <summary>
            /// A fake implementation of a scanner cursor.
            /// </summary>
            public class FakeCursor : ICursor
            {
                private readonly Action<int> _onReset;
                public FakeCursor(int position, Action<int> onReset)
                {
                    Position = position;
                    _onReset = onReset;
                }

                public int Position { get; set; }

                public void ResetPosition(int position)
                {
                    Position = position;
                    // For the purpose of tests, we record the reset position.
                    if (CurrentContext != null)
                    {
                        CurrentContext.LastResetPosition = position;
                    }
                    _onReset(position);
                }

                // Reference back to context to record last reset. This is set when FakeParseContext is created.
                public FakeParseContext CurrentContext { get; set; }
            }

            // After creating FakeParseContext, set the CurrentContext on the Cursor.
            public override IScanner Scanner
            {
                get => Scanner;
                set { }
            }
        }

        /// <summary>
        /// A fake implementation of CompilationContext for testing Compile method.
        /// </summary>
        private class FakeCompilationContext : CompilationContext
        {
            public FakeCompilationContext()
            {
                DiscardResult = false;
            }

            public override CompilationResult CreateCompilationResult<T>()
            {
                return new CompilationResult
                {
                    Variables = new List<ParameterExpression>(),
                    Body = new List<Expression>(),
                    Success = Expression.Parameter(typeof(bool), "success"),
                    Value = Expression.Parameter(typeof(T), "value")
                };
            }

            public override ParameterExpression DeclarePositionVariable(CompilationResult result)
            {
                return Expression.Parameter(typeof(int), "pos");
            }

            public override Expression ResetPosition(ParameterExpression positionVariable)
            {
                return Expression.Empty();
            }
        }

        #endregion
    }

    #region Supporting Fake Types for Parsing and Compilation

    // Fake types to simulate the behavior required by the Between parser.

    /// <summary>
    /// A minimal fake implementation of ParseContext.
    /// </summary>
    public abstract class ParseContext
    {
        public abstract IScanner Scanner { get; set; }
        public abstract void EnterParser(ParserBase parser);
        public abstract void ExitParser(ParserBase parser);
    }

    /// <summary>
    /// A minimal fake interface for a scanner.
    /// </summary>
    public interface IScanner
    {
        ICursor Cursor { get; }
    }

    /// <summary>
    /// A minimal fake interface for a cursor.
    /// </summary>
    public interface ICursor
    {
        int Position { get; set; }
        void ResetPosition(int position);
    }

    /// <summary>
    /// A minimal fake base class for parsers.
    /// </summary>
    public abstract class ParserBase
    {
    }

    /// <summary>
    /// A minimal fake implementation of Parser&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T">The type of the parse result.</typeparam>
    public abstract class Parser<T> : ParserBase
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
    }

    /// <summary>
    /// A minimal fake implementation of a parse result.
    /// </summary>
    /// <typeparam name="T">The type of the parse value.</typeparam>
    public class ParseResult<T>
    {
        public T Value { get; set; }
    }

    /// <summary>
    /// A minimal fake interface for compilable parsers.
    /// </summary>
    public interface ICompilable
    {
        CompilationResult Compile(CompilationContext context);
    }

    /// <summary>
    /// A minimal fake interface for seekable parsers.
    /// </summary>
    public interface ISeekable
    {
        bool CanSeek { get; }
        char[] ExpectedChars { get; }
        bool SkipWhitespace { get; }
    }

    /// <summary>
    /// A minimal fake implementation of CompilationContext.
    /// </summary>
    public abstract class CompilationContext
    {
        public bool DiscardResult { get; protected set; }
        public abstract CompilationResult CreateCompilationResult<T>();
        public abstract ParameterExpression DeclarePositionVariable(CompilationResult result);
        public abstract Expression ResetPosition(ParameterExpression positionVariable);
    }

    /// <summary>
    /// A minimal fake implementation of a CompilationResult.
    /// </summary>
    public class CompilationResult
    {
        public List<ParameterExpression> Variables { get; set; } = new List<ParameterExpression>();
        public List<Expression> Body { get; set; } = new List<Expression>();
        public ParameterExpression Success { get; set; }
        public ParameterExpression Value { get; set; }
    }

    #endregion
}
