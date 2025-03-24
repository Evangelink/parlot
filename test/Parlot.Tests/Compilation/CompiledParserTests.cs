using System.Collections.Generic;
using Moq;
using Xunit;
using Parlot.Compilation;

namespace Parlot.Compilation.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CompiledParser{T}"/> class.
    /// </summary>
    public class CompiledParserTests
    {
        private readonly Parser<string> _dummySource;

        public CompiledParserTests()
        {
            // Create a dummy source parser using Moq.
            var dummySourceMock = new Mock<Parser<string>>();
            _dummySource = dummySourceMock.Object;
        }

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when the parse delegate is null.
        /// </summary>
        [Fact]
        public void Constructor_NullParse_ThrowsArgumentNullException()
        {
            // Arrange
            Func<FakeParseContext, (bool, string)> nullParse = null;

            // Act & Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new CompiledParser<string>(nullParse, _dummySource));
            Assert.Equal("parse", exception.ParamName);
        }

        /// <summary>
        /// Tests that the constructor correctly sets the Source property when valid arguments are provided.
        /// </summary>
        [Fact]
        public void Constructor_ValidArguments_SetsSourceProperty()
        {
            // Arrange
            Func<FakeParseContext, (bool, string)> validParse = ctx => (false, default);
            
            // Act
            CompiledParser<string> parser = new CompiledParser<string>(validParse, _dummySource);

            // Assert
            Assert.Equal(_dummySource, parser.Source);
        }

        /// <summary>
        /// Tests that the Parse method returns true, correctly sets the result when the parse delegate returns success,
        /// and calls EnterParser and ExitParser appropriately.
        /// </summary>
        [Fact]
        public void Parse_ReturnsTrue_WhenParseDelegateReturnsTrue()
        {
            // Arrange
            const int initialOffset = 5;
            const int newOffset = 10;
            const string expectedValue = "success";

            // Create a fake parse context with a fake scanner and cursor.
            FakeCursor cursor = new FakeCursor { Offset = initialOffset };
            FakeScanner scanner = new FakeScanner { Cursor = cursor };
            FakeParseContext context = new FakeParseContext { Scanner = scanner };

            // The parse delegate simulates a successful parse by updating the cursor and returning a tuple with success.
            Func<FakeParseContext, (bool, string)> parseDelegate = ctx =>
            {
                // Simulate advancing the cursor.
                ctx.Scanner.Cursor.Offset = newOffset;
                return (true, expectedValue);
            };

            CompiledParser<string> parser = new CompiledParser<string>(parseDelegate, _dummySource);
            FakeParseResult<string> result = new FakeParseResult<string>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.IsSet);
            Assert.Equal(initialOffset, result.Start);
            Assert.Equal(newOffset, result.End);
            Assert.Equal(expectedValue, result.Value);

            // Verify that EnterParser and ExitParser were called in order.
            Assert.Equal(new List<string> { "Enter", "Exit" }, context.CallLog);
        }

        /// <summary>
        /// Tests that the Parse method returns false and does not modify the result when the parse delegate returns failure.
        /// </summary>
        [Fact]
        public void Parse_ReturnsFalse_WhenParseDelegateReturnsFalse()
        {
            // Arrange
            const int initialOffset = 3;
            const int newOffset = 7;

            FakeCursor cursor = new FakeCursor { Offset = initialOffset };
            FakeScanner scanner = new FakeScanner { Cursor = cursor };
            FakeParseContext context = new FakeParseContext { Scanner = scanner };

            // Parse delegate simulates a failed parse by updating the cursor but indicating failure.
            Func<FakeParseContext, (bool, string)> parseDelegate = ctx =>
            {
                ctx.Scanner.Cursor.Offset = newOffset;
                return (false, default);
            };

            CompiledParser<string> parser = new CompiledParser<string>(parseDelegate, _dummySource);
            FakeParseResult<string> result = new FakeParseResult<string>();

            // Act
            bool parseResult = parser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            // Since parsing failed, the result should not be set.
            Assert.False(result.IsSet);

            // Verify that EnterParser and ExitParser were called.
            Assert.Equal(new List<string> { "Enter", "Exit" }, context.CallLog);
        }

        /// <summary>
        /// Tests that an exception thrown from the parse delegate is propagated by the Parse method.
        /// </summary>
        [Fact]
        public void Parse_ParseDelegateThrows_ExceptionIsPropagated()
        {
            // Arrange
            FakeCursor cursor = new FakeCursor { Offset = 0 };
            FakeScanner scanner = new FakeScanner { Cursor = cursor };
            FakeParseContext context = new FakeParseContext { Scanner = scanner };

            Exception expectedException = new InvalidOperationException("Test exception");
            Func<FakeParseContext, (bool, string)> parseDelegate = ctx =>
            {
                throw expectedException;
            };

            CompiledParser<string> parser = new CompiledParser<string>(parseDelegate, _dummySource);
            FakeParseResult<string> result = new FakeParseResult<string>();

            // Act & Assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => parser.Parse(context, ref result));
            Assert.Equal(expectedException.Message, exception.Message);
            // Since exception occurs, the call log should only have the EnterParser recorded.
            Assert.Equal(new List<string> { "Enter" }, context.CallLog);
        }

        /// <summary>
        /// Tests that the Parse method throws a NullReferenceException when the context is null.
        /// </summary>
        [Fact]
        public void Parse_NullContext_ThrowsNullReferenceException()
        {
            // Arrange
            Func<FakeParseContext, (bool, string)> parseDelegate = ctx => (false, default);
            CompiledParser<string> parser = new CompiledParser<string>(parseDelegate, _dummySource);
            FakeParseResult<string> result = new FakeParseResult<string>();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => parser.Parse(null, ref result));
        }

        /// <summary>
        /// Tests that when the parse delegate returns success but the result reference is null,
        /// a NullReferenceException is thrown.
        /// </summary>
        [Fact]
        public void Parse_SuccessfulParsing_NullResult_ThrowsNullReferenceException()
        {
            // Arrange
            FakeCursor cursor = new FakeCursor { Offset = 2 };
            FakeScanner scanner = new FakeScanner { Cursor = cursor };
            FakeParseContext context = new FakeParseContext { Scanner = scanner };

            Func<FakeParseContext, (bool, string)> parseDelegate = ctx =>
            {
                ctx.Scanner.Cursor.Offset = 5;
                return (true, "value");
            };

            CompiledParser<string> parser = new CompiledParser<string>(parseDelegate, _dummySource);
            FakeParseResult<string> result = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => parser.Parse(context, ref result));
        }
    }

    /// <summary>
    /// A fake implementation of a cursor used for testing.
    /// </summary>
    internal class FakeCursor
    {
        public int Offset { get; set; }
    }

    /// <summary>
    /// A fake implementation of a scanner which exposes a cursor.
    /// </summary>
    internal class FakeScanner
    {
        public FakeCursor Cursor { get; set; }
    }

    /// <summary>
    /// A fake implementation of a parse context used for testing.
    /// </summary>
    internal class FakeParseContext
    {
        /// <summary>
        /// Gets or sets the scanner associated with this context.
        /// </summary>
        public FakeScanner Scanner { get; set; }

        /// <summary>
        /// Records calls to EnterParser and ExitParser.
        /// </summary>
        public List<string> CallLog { get; } = new List<string>();

        /// <summary>
        /// Simulates entering a parser.
        /// </summary>
        /// <param name="parser">The parser being entered.</param>
        public void EnterParser(object parser)
        {
            CallLog.Add("Enter");
        }

        /// <summary>
        /// Simulates exiting a parser.
        /// </summary>
        /// <param name="parser">The parser being exited.</param>
        public void ExitParser(object parser)
        {
            CallLog.Add("Exit");
        }
    }

    /// <summary>
    /// A fake implementation of ParseResult used for testing purposes.
    /// </summary>
    /// <typeparam name="T">The type of the parsed value.</typeparam>
    internal class FakeParseResult<T> : ParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T Value { get; private set; }
        public bool IsSet { get; private set; } = false;

        /// <summary>
        /// Sets the result with the specified start and end positions and value.
        /// </summary>
        /// <param name="start">The start offset.</param>
        /// <param name="end">The end offset.</param>
        /// <param name="value">The parsed value.</param>
        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
            IsSet = true;
        }
    }
}
