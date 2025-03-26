using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using Parlot.Fluent;
using Parlot.Compilation;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Separated{U, T}"/> class.
    /// </summary>
    public class SeparatedTests
    {
        private readonly FakeParseContext _fakeContext;
        private readonly FakeCompilationContext _fakeCompilationContext;

        public SeparatedTests()
        {
            _fakeContext = new FakeParseContext();
            _fakeCompilationContext = new FakeCompilationContext();
        }

        #region Constructor Tests

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when the separator parser is null.
        /// </summary>
        [Fact]
        public void Constructor_NullSeparator_ThrowsArgumentNullException()
        {
            // Arrange
            Parser<int>? validParser = new FakeParser<int>(true, 0, 0, 0);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Separated<char, int>(null!, validParser));
        }

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when the main parser is null.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange
            Parser<char>? validSeparator = new FakeParser<char>(true, ' ', 0, 0);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Separated<char, int>(validSeparator, null!));
        }

        /// <summary>
        /// Tests that the constructor correctly sets the ISeekable properties when the main parser implements ISeekable.
        /// </summary>
        [Fact]
        public void Constructor_WhenParserImplementsISeekable_SetsPropertiesProperly()
        {
            // Arrange
            var expectedCanSeek = true;
            var expectedExpectedChars = new[] { 'a', 'b' };
            var expectedSkipWhitespace = false;
            var seekableParser = new FakeSeekableParser<int>(expectedCanSeek, expectedExpectedChars, expectedSkipWhitespace);
            var dummySeparator = new FakeParser<char>(true, ',', 0, 0);

            // Act
            var separated = new Separated<char, int>(dummySeparator, seekableParser);

            // Assert
            Assert.Equal(expectedCanSeek, separated.CanSeek);
            Assert.Equal(expectedExpectedChars, separated.ExpectedChars);
            Assert.Equal(expectedSkipWhitespace, separated.SkipWhitespace);
        }

        #endregion

        #region Parse Tests

        /// <summary>
        /// Tests that Parse returns false and does not modify the result when the first main parser parse fails.
        /// </summary>
        [Fact]
        public void Parse_FirstParseFails_ReturnsFalseAndNoResults()
        {
            // Arrange
            var separatorMock = new Mock<Parser<char>>();
            // _separator is not called on the first iteration because first is true.

            var parserMock = new Mock<Parser<int>>();
            // Setup: first call to Parse returns false.
            parserMock.Setup(p => p.Parse(It.IsAny<FakeParseContext>(), ref It.Ref<ParseResult<int>>.IsAny))
                      .Callback((FakeParseContext ctx, ref ParseResult<int> res) =>
                      {
                          // Do nothing; leave res untouched.
                      })
                      .Returns(false);

            var separated = new Separated<char, int>(separatorMock.Object, parserMock.Object);
            var result = new ParseResult<IReadOnlyList<int>>();

            // Act
            bool parseResult = separated.Parse(_fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.Empty(result.Value);
        }

        /// <summary>
        /// Tests that a successful single element parse returns a list containing the parsed value.
        /// </summary>
        [Fact]
        public void Parse_SuccessfulSingleElementParse_ReturnsListWithOneElement()
        {
            // Arrange
            var separatorMock = new Mock<Parser<char>>();
            // Separator should not be called because after first element, the next iteration stops by separator failure.
            separatorMock.Setup(p => p.Parse(It.IsAny<FakeParseContext>(), ref It.Ref<ParseResult<char>>.IsAny))
                         .Returns(false);

            var parserCallCount = 0;
            var parserMock = new Mock<Parser<int>>();
            parserMock.Setup(p => p.Parse(It.IsAny<FakeParseContext>(), ref It.Ref<ParseResult<int>>.IsAny))
                      .Callback((FakeParseContext ctx, ref ParseResult<int> res) =>
                      {
                          if (parserCallCount == 0)
                          {
                              res.Start = 0;
                              res.Value = 42;
                              // Simulate advancing the cursor.
                              ctx.Scanner.Cursor.Position.Offset = 10;
                          }
                      })
                      .Returns(() =>
                      {
                          return parserCallCount++ == 0;
                      });

            var separated = new Separated<char, int>(separatorMock.Object, parserMock.Object);
            var result = new ParseResult<IReadOnlyList<int>>();

            // Act
            bool parseResult = separated.Parse(_fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.NotNull(result.Value);
            var list = result.Value as List<int>;
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Equal(42, list[0]);
        }

        /// <summary>
        /// Tests that when a separator is found but not followed by a main parser success, the cursor resets and the parsed list contains only the first element.
        /// </summary>
        [Fact]
        public void Parse_SeparatorWithoutFollowingValue_ResetsCursorAndReturnsListWithOneElement()
        {
            // Arrange
            var separatorCallCount = 0;
            var separatorMock = new Mock<Parser<char>>();
            separatorMock.Setup(p => p.Parse(It.IsAny<FakeParseContext>(), ref It.Ref<ParseResult<char>>.IsAny))
                         .Callback((FakeParseContext ctx, ref ParseResult<char> res) =>
                         {
                             // For second iteration, simulate successful separator parse
                             if (separatorCallCount == 0)
                             {
                                 res.Start = 10;
                                 res.Value = ',';
                                 // Do not advance the cursor for separator.
                             }
                             separatorCallCount++;
                         })
                         .Returns(() =>
                         {
                             // Only succeed once.
                             return separatorCallCount == 1;
                         });

            var parserCallCount = 0;
            var parserMock = new Mock<Parser<int>>();
            parserMock.Setup(p => p.Parse(It.IsAny<FakeParseContext>(), ref It.Ref<ParseResult<int>>.IsAny))
                      .Callback((FakeParseContext ctx, ref ParseResult<int> res) =>
                      {
                          if (parserCallCount == 0)
                          {
                              res.Start = 0;
                              res.Value = 100;
                              // Advance cursor to offset 20.
                              ctx.Scanner.Cursor.Position.Offset = 20;
                          }
                      })
                      .Returns(() =>
                      {
                          // First call succeeds, second call fails.
                          return parserCallCount++ == 0;
                      });

            var separated = new Separated<char, int>(separatorMock.Object, parserMock.Object);
            var result = new ParseResult<IReadOnlyList<int>>();

            // Set initial cursor position
            _fakeContext.Scanner.Cursor.Position.Offset = 0;

            // Act
            bool parseResult = separated.Parse(_fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.NotNull(result.Value);
            var list = result.Value as List<int>;
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Equal(100, list[0]);
            // The cursor position should have been reset to the end of the last successful parse.
            Assert.Equal(20, _fakeContext.Scanner.Cursor.Position.Offset);
        }

        #endregion

        #region Compile Tests

        /// <summary>
        /// Tests that the Compile method returns a CompilationResult with a non-empty Body.
        /// </summary>
        [Fact]
        public void Compile_ReturnsCompilationResultWithBody()
        {
            // Arrange
            // For Compile test, we use mocks for parser and separator Build methods.
            var separatorMock = new Mock<Parser<char>>();
            separatorMock.Setup(p => p.Build(It.IsAny<CompilationContext>()))
                         .Returns(new FakeCompilationResult<char>());

            var parserMock = new Mock<Parser<int>>();
            parserMock.Setup(p => p.Build(It.IsAny<CompilationContext>()))
                      .Returns(new FakeCompilationResult<int>());

            var separated = new Separated<char, int>(separatorMock.Object, parserMock.Object);

            // Act
            var compilationResult = separated.Compile(_fakeCompilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
        }

        #endregion

        #region ToString Tests

        /// <summary>
        /// Tests that the ToString method returns the expected formatted string.
        /// </summary>
        [Fact]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            var fakeSeparator = new FakeParser<char>(true, 'x', 0, 0)
            {
                ToStringOverride = "FakeSeparator"
            };
            var fakeParser = new FakeParser<int>(true, 999, 0, 0)
            {
                ToStringOverride = "FakeParser"
            };

            var separated = new Separated<char, int>(fakeSeparator, fakeParser);

            // Act
            var result = separated.ToString();

            // Assert
            Assert.Contains("FakeSeparator", result);
            Assert.Contains("FakeParser", result);
        }

        #endregion
    }

    #region Fake and Helper Classes

    /// <summary>
    /// A fake implementation of Parser that can simulate Parse behavior.
    /// </summary>
    /// <typeparam name="T">The type of value parsed.</typeparam>
    internal class FakeParser<T> : Parser<T>
    {
        private readonly bool _defaultSuccess;
        private readonly T _defaultValue;
        private readonly int _defaultStart;
        private readonly int _defaultEnd;

        public string ToStringOverride { get; set; } = "";

        public FakeParser(bool defaultSuccess, T defaultValue, int defaultStart, int defaultEnd)
        {
            _defaultSuccess = defaultSuccess;
            _defaultValue = defaultValue;
            _defaultStart = defaultStart;
            _defaultEnd = defaultEnd;
        }

        public override bool Parse(ParseContext context, ref ParseResult<T> result)
        {
            if (_defaultSuccess)
            {
                result.Start = _defaultStart;
                result.Value = _defaultValue;
                // Simulate advancing the cursor
                context.Scanner.Cursor.Position.Offset = _defaultEnd;
            }
            return _defaultSuccess;
        }

        public override CompilationResult Build(CompilationContext context)
        {
            // Return a fake CompilationResult with a dummy expression.
            var dummyResult = new FakeCompilationResult<T>();
            return dummyResult;
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(ToStringOverride) ? ToStringOverride : base.ToString();
        }
    }

    /// <summary>
    /// A fake implementation of a Parser that implements ISeekable.
    /// </summary>
    /// <typeparam name="T">The type of value parsed.</typeparam>
    internal class FakeSeekableParser<T> : Parser<T>, ISeekable
    {
        public bool CanSeek { get; }
        public char[] ExpectedChars { get; }
        public bool SkipWhitespace { get; }

        public FakeSeekableParser(bool canSeek, char[] expectedChars, bool skipWhitespace)
        {
            CanSeek = canSeek;
            ExpectedChars = expectedChars;
            SkipWhitespace = skipWhitespace;
        }

        public override bool Parse(ParseContext context, ref ParseResult<T> result)
        {
            throw new NotImplementedException();
        }

        public override CompilationResult Build(CompilationContext context)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A fake implementation of ParseContext for testing purposes.
    /// </summary>
    internal class FakeParseContext : ParseContext
    {
        public FakeParseContext() : base(null!)
        {
            Scanner = new FakeScanner();
        }

        public override void EnterParser(object parser)
        {
            // No-op for testing.
        }

        public override void ExitParser(object parser)
        {
            // No-op for testing.
        }
    }

    /// <summary>
    /// A fake implementation of a scanner.
    /// </summary>
    internal class FakeScanner
    {
        public FakeCursor Cursor { get; }

        public FakeScanner()
        {
            Cursor = new FakeCursor();
        }
    }

    /// <summary>
    /// A fake implementation of a cursor.
    /// </summary>
    internal class FakeCursor
    {
        public FakePosition Position { get; set; }

        public FakeCursor()
        {
            Position = new FakePosition();
        }

        public void ResetPosition(FakePosition pos)
        {
            Position = pos;
        }
    }

    /// <summary>
    /// A fake implementation of a position.
    /// </summary>
    internal class FakePosition
    {
        public int Offset { get; set; }
    }

    /// <summary>
    /// A fake implementation of CompilationContext for testing purposes.
    /// </summary>
    internal class FakeCompilationContext : CompilationContext
    {
        private int _nextNumber = 1;
        public override int NextNumber => _nextNumber++;

        public override Expression Position()
        {
            return Expression.Constant(0);
        }

        public override Expression Eof()
        {
            // Always return false in this fake context.
            return Expression.Constant(false);
        }

        public override Expression ResetPosition(ParameterExpression positionVariable)
        {
            return Expression.Empty();
        }

        public override ParameterExpression DeclarePositionVariable(CompilationResult result)
        {
            return Expression.Variable(typeof(int), "pos");
        }
    }

    /// <summary>
    /// A fake implementation of CompilationResult for testing purposes.
    /// </summary>
    /// <typeparam name="TResult">The type of the result value.</typeparam>
    internal class FakeCompilationResult<TResult> : CompilationResult<TResult>
    {
        public FakeCompilationResult() : base(false, Expression.Constant(default(TResult)))
        {
            // Add a dummy expression to simulate a body.
            Body.Add(Expression.Empty());
        }
    }

    #endregion
}
