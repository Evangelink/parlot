using Moq;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParseContext"/> class.
    /// </summary>
    public class ParseContextTests
    {
        private readonly Scanner _scanner;
        
        public ParseContextTests()
        {
            _scanner = new Scanner();
            _scanner.Cursor.Position = new TextPosition { Offset = 0 };
        }

        /// <summary>
        /// Tests that constructing a ParseContext with a null scanner throws an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Constructor_NullScanner_ThrowsArgumentNullException()
        {
            // Arrange
            Scanner? nullScanner = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new ParseContext(nullScanner!));
            Assert.Equal("scanner", exception.ParamName);
        }

        /// <summary>
        /// Tests that the UseNewLines property is properly set based on the constructor parameter.
        /// </summary>
        /// <param name="useNewLines">The value for the useNewLines parameter.</param>
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Constructor_UseNewLinesParameter_SetsUseNewLinesProperty(bool useNewLines)
        {
            // Arrange
            var scanner = new Scanner();

            // Act
            var context = new ParseContext(scanner, useNewLines);

            // Assert
            Assert.Equal(useNewLines, context.UseNewLines);
        }

        /// <summary>
        /// Tests that the CompilationThreshold property is initialized to the static default value.
        /// </summary>
        [Fact]
        public void CompilationThreshold_DefaultValue_EqualsStaticDefaultCompilationThreshold()
        {
            // Arrange
            ParseContext.DefaultCompilationThreshold = 7;
            var scanner = new Scanner();

            // Act
            var context = new ParseContext(scanner);

            // Assert
            Assert.Equal(ParseContext.DefaultCompilationThreshold, context.CompilationThreshold);
        }

        /// <summary>
        /// Tests that the CompilationThreshold property can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void CompilationThreshold_SetAndGet_WorksAsExpected()
        {
            // Arrange
            var scanner = new Scanner();
            var context = new ParseContext(scanner);
            context.CompilationThreshold = 15;

            // Act
            int currentThreshold = context.CompilationThreshold;

            // Assert
            Assert.Equal(15, currentThreshold);
        }

        /// <summary>
        /// Tests that SkipWhiteSpace calls Scanner.SkipWhiteSpace when WhiteSpaceParser is null and UseNewLines is true.
        /// </summary>
        [Fact]
        public void SkipWhiteSpace_NoWhiteSpaceParser_UseNewLinesTrue_CallsSkipWhiteSpace()
        {
            // Arrange
            var scanner = new Scanner();
            scanner.Cursor.Position = new TextPosition { Offset = 0 };
            var context = new ParseContext(scanner, useNewLines: true)
            {
                WhiteSpaceParser = null
            };

            // Act
            context.SkipWhiteSpace();

            // Assert: Fake implementation of SkipWhiteSpace increases offset by 1.
            Assert.Equal(1, scanner.Cursor.Position.Offset);
        }

        /// <summary>
        /// Tests that SkipWhiteSpace calls Scanner.SkipWhiteSpaceOrNewLine when WhiteSpaceParser is null and UseNewLines is false.
        /// </summary>
        [Fact]
        public void SkipWhiteSpace_NoWhiteSpaceParser_UseNewLinesFalse_CallsSkipWhiteSpaceOrNewLine()
        {
            // Arrange
            var scanner = new Scanner();
            scanner.Cursor.Position = new TextPosition { Offset = 0 };
            var context = new ParseContext(scanner, useNewLines: false)
            {
                WhiteSpaceParser = null
            };

            // Act
            context.SkipWhiteSpace();

            // Assert: Fake implementation of SkipWhiteSpaceOrNewLine increases offset by 10.
            Assert.Equal(10, scanner.Cursor.Position.Offset);
        }

        /// <summary>
        /// Tests that SkipWhiteSpace invokes the WhiteSpaceParser when provided and correctly leverages caching to reset the cursor position.
        /// </summary>
        [Fact]
        public void SkipWhiteSpace_WhiteSpaceParserProvided_CachesAndResetsCursorPosition()
        {
            // Arrange
            var scanner = new Scanner();
            // Set initial position offset to 0.
            scanner.Cursor.Position = new TextPosition { Offset = 0 };
            // Use a fake parser which sets the Cursor's position to offset 100.
            var context = new ParseContext(scanner)
            {
                WhiteSpaceParser = new FakeWhiteSpaceParser(100)
            };

            // Act
            // First call: should invoke the fake white space parser.
            context.SkipWhiteSpace();
            // After first call, the FakeWhiteSpaceParser sets Cursor.Position.Offset to 100.
            Assert.Equal(100, scanner.Cursor.Position.Offset);

            // Manually simulate a scenario where the scanner's current offset equals the cached offset.
            // The cached offset was recorded from the initial Cursor.Position.Offset (which was 0).
            scanner.Cursor.Position = new TextPosition { Offset = 0 };
            context.SkipWhiteSpace();

            // Assert: Since the current offset equals the cached offset, the cursor should be reset to the cached position (100).
            Assert.Equal(100, scanner.Cursor.Position.Offset);
        }

        /// <summary>
        /// Tests that EnterParser calls the OnEnterParser delegate when it is assigned.
        /// </summary>
        [Fact]
        public void EnterParser_OnEnterParserSet_InvokesDelegate()
        {
            // Arrange
            bool delegateCalled = false;
            var dummyParser = new DummyParser<object>();
            var context = new ParseContext(new Scanner());
            context.OnEnterParser = (parser, ctx) =>
            {
                delegateCalled = true;
                Assert.Equal(dummyParser, parser);
                Assert.Equal(context, ctx);
            };

            // Act
            context.EnterParser(dummyParser);

            // Assert
            Assert.True(delegateCalled);
        }

        /// <summary>
        /// Tests that ExitParser calls the OnExitParser delegate when it is assigned.
        /// </summary>
        [Fact]
        public void ExitParser_OnExitParserSet_InvokesDelegate()
        {
            // Arrange
            bool delegateCalled = false;
            var dummyParser = new DummyParser<object>();
            var context = new ParseContext(new Scanner());
            context.OnExitParser = (parser, ctx) =>
            {
                delegateCalled = true;
                Assert.Equal(dummyParser, parser);
                Assert.Equal(context, ctx);
            };

            // Act
            context.ExitParser(dummyParser);

            // Assert
            Assert.True(delegateCalled);
        }
    }

    /// <summary>
    /// A fake implementation of Parser to simulate white space parsing behavior.
    /// </summary>
    public class FakeWhiteSpaceParser : Parser<TextSpan>
    {
        private readonly int _newOffset;

        /// <summary>
        /// Initializes a new instance of the FakeWhiteSpaceParser with the specified new offset.
        /// </summary>
        /// <param name="newOffset">The offset to set on the cursor during parsing.</param>
        public FakeWhiteSpaceParser(int newOffset)
        {
            _newOffset = newOffset;
        }

        /// <summary>
        /// Overrides the Parse method to set the cursor's position to a predetermined offset.
        /// </summary>
        /// <param name="context">The parse context.</param>
        /// <param name="result">The parse result (unused in this implementation).</param>
        public override void Parse(ParseContext context, ref ParseResult<TextSpan> result)
        {
            context.Scanner.Cursor.Position = new TextPosition { Offset = _newOffset };
        }
    }

    /// <summary>
    /// A dummy parser used for testing delegate invocations.
    /// </summary>
    public class DummyParser<T> : Parser<T>
    {
    }

    // Stub implementations to support testing in absence of full implementations.

    /// <summary>
    /// A stub implementation of a Scanner.
    /// </summary>
    public class Scanner
    {
        public Cursor Cursor { get; set; } = new Cursor();

        /// <summary>
        /// Simulates skipping white spaces by increasing the offset by 1.
        /// </summary>
        public void SkipWhiteSpace()
        {
            Cursor.Position = new TextPosition { Offset = Cursor.Position.Offset + 1 };
        }

        /// <summary>
        /// Simulates skipping white spaces or new lines by increasing the offset by 10.
        /// </summary>
        public void SkipWhiteSpaceOrNewLine()
        {
            Cursor.Position = new TextPosition { Offset = Cursor.Position.Offset + 10 };
        }
    }

    /// <summary>
    /// A stub implementation of a Cursor.
    /// </summary>
    public class Cursor
    {
        public TextPosition Position { get; set; } = new TextPosition();

        /// <summary>
        /// Resets the cursor's position to the given position.
        /// </summary>
        /// <param name="pos">The position to reset to.</param>
        public void ResetPosition(TextPosition pos)
        {
            Position = pos;
        }
    }

    /// <summary>
    /// A stub representation of a text position.
    /// </summary>
    public struct TextPosition
    {
        public int Offset { get; set; }
    }

    /// <summary>
    /// A stub representation of a text span.
    /// </summary>
    public class TextSpan
    {
    }

    /// <summary>
    /// A stub representation of a parse result.
    /// </summary>
    /// <typeparam name="T">The type of the parse result.</typeparam>
    public class ParseResult<T>
    {
    }

    /// <summary>
    /// A stub representation of a parser.
    /// </summary>
    /// <typeparam name="T">The result type of the parser.</typeparam>
    public class Parser<T>
    {
        /// <summary>
        /// Simulates parsing by doing nothing. This method is overridable.
        /// </summary>
        /// <param name="context">The parse context.</param>
        /// <param name="result">The parse result.</param>
        public virtual void Parse(ParseContext context, ref ParseResult<T> result)
        {
        }
    }
}
