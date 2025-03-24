using System.Threading;
using Moq;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    #region Test Stubs
    // Minimal stub implementations for required types used in Parser<T>.
    public class ParseContext
    {
        public int CompilationThreshold { get; set; } = 1;
        public Scanner Scanner { get; }
        public ParseContext(Scanner scanner)
        {
            Scanner = scanner;
        }
    }

    public class Scanner
    {
        public string Text { get; }
        public Scanner(string text)
        {
            Text = text;
        }
    }

    public class ParseResult<T>
    {
        public T? Value { get; set; }
    }

    public class ParseError
    {
        public string? Message { get; set; }
        public int Position { get; set; }
    }

    public class ParseException : Exception
    {
        public int Position { get; }
        public ParseException(string message, int position) : base(message)
        {
            Position = position;
        }
    }
    #endregion

    // FakeParser is a concrete implementation of the abstract Parser<T> class
    // used for testing the public members of Parser<T>.
    public class FakeParser : Parser<int>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the parse operation should succeed.
        /// </summary>
        public bool ShouldSucceed { get; set; } = true;

        /// <summary>
        /// Gets or sets the value to return when parsing succeeds.
        /// </summary>
        public int ReturnValue { get; set; } = 42;

        /// <summary>
        /// Gets or sets a value indicating whether the parse method should throw an exception.
        /// </summary>
        public bool ThrowException { get; set; } = false;

        /// <summary>
        /// Gets or sets the expected exception message when an exception is thrown.
        /// </summary>
        public string? ExpectedExceptionMessage { get; set; }

        /// <summary>
        /// Overrides the abstract Compile method to simulate compilation.
        /// </summary>
        /// <returns>Returns the current instance after "compilation".</returns>
        protected override Parser<int> Compile()
        {
            // For testing, simply return this instance.
            return this;
        }

        /// <summary>
        /// Overrides the Parse method that takes a ParseContext and a ParseResult reference.
        /// Simulates a parsing operation based on the property values.
        /// </summary>
        /// <param name="context">The parsing context.</param>
        /// <param name="result">The result object to store the parsed value.</param>
        /// <returns>True if parsing succeeds; otherwise false.</returns>
        public override bool Parse(ParseContext context, ref ParseResult<int> result)
        {
            if (ThrowException)
            {
                throw new ParseException(ExpectedExceptionMessage ?? "Test exception", 10);
            }
            result.Value = ReturnValue;
            return ShouldSucceed;
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="Parser{T}"/> class.
    /// </summary>
    public class ParserTests
    {
        private readonly FakeParser _fakeParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserTests"/> class.
        /// </summary>
        public ParserTests()
        {
            _fakeParser = new FakeParser();
        }

        /// <summary>
        /// Tests that Parse(string) returns the expected value when parsing succeeds.
        /// </summary>
        [Fact]
        public void Parse_StringInput_Success_ReturnsExpectedValue()
        {
            // Arrange
            _fakeParser.ShouldSucceed = true;
            _fakeParser.ReturnValue = 100;
            string input = "test input";
            // Set a compilation threshold to ensure compilation path is exercised.
            var context = new ParseContext(new Scanner(input)) { CompilationThreshold = 1 };

            // Act
            int? result = _fakeParser.Parse(input);

            // Assert
            Assert.Equal(100, result);
        }

        /// <summary>
        /// Tests that Parse(string) returns default when parsing fails.
        /// </summary>
        [Fact]
        public void Parse_StringInput_Failure_ReturnsDefault()
        {
            // Arrange
            _fakeParser.ShouldSucceed = false;
            _fakeParser.ReturnValue = 200; // Should be ignored on failure.
            string input = "failure input";
            var context = new ParseContext(new Scanner(input)) { CompilationThreshold = 1 };

            // Act
            int? result = _fakeParser.Parse(input);

            // Assert
            Assert.Equal(default(int), result);
        }

        /// <summary>
        /// Tests that Parse(ParseContext) returns the expected value when parsing succeeds.
        /// </summary>
        [Fact]
        public void Parse_ParseContext_Success_ReturnsExpectedValue()
        {
            // Arrange
            _fakeParser.ShouldSucceed = true;
            _fakeParser.ReturnValue = 300;
            var context = new ParseContext(new Scanner("context success")) { CompilationThreshold = 1 };

            // Act
            int? result = _fakeParser.Parse(context);

            // Assert
            Assert.Equal(300, result);
        }

        /// <summary>
        /// Tests that Parse(ParseContext) returns default when parsing fails.
        /// </summary>
        [Fact]
        public void Parse_ParseContext_Failure_ReturnsDefault()
        {
            // Arrange
            _fakeParser.ShouldSucceed = false;
            _fakeParser.ReturnValue = 400;
            var context = new ParseContext(new Scanner("context failure")) { CompilationThreshold = 1 };

            // Act
            int? result = _fakeParser.Parse(context);

            // Assert
            Assert.Equal(default(int), result);
        }

        /// <summary>
        /// Tests that TryParse(string, out T) returns true and sets the expected value when parsing succeeds.
        /// </summary>
        [Fact]
        public void TryParse_StringInput_Success_ReturnsTrueAndExpectedValue()
        {
            // Arrange
            _fakeParser.ShouldSucceed = true;
            _fakeParser.ReturnValue = 500;
            string input = "tryparse success";

            // Act
            bool success = _fakeParser.TryParse(input, out int? value);

            // Assert
            Assert.True(success);
            Assert.Equal(500, value);
        }

        /// <summary>
        /// Tests that TryParse(string, out T, out ParseError) returns false and default value when parsing fails.
        /// </summary>
        [Fact]
        public void TryParse_StringInput_Failure_ReturnsFalseAndDefault()
        {
            // Arrange
            _fakeParser.ShouldSucceed = false;
            _fakeParser.ReturnValue = 600;
            string input = "tryparse failure";

            // Act
            bool success = _fakeParser.TryParse(input, out int value, out ParseError? error);

            // Assert
            Assert.False(success);
            Assert.Equal(default(int), value);
            Assert.Null(error);
        }

        /// <summary>
        /// Tests that TryParse(ParseContext, out T, out ParseError) returns true and the expected value upon successful parsing.
        /// </summary>
        [Fact]
        public void TryParse_ParseContext_Success_ReturnsTrueAndExpectedValue()
        {
            // Arrange
            _fakeParser.ShouldSucceed = true;
            _fakeParser.ReturnValue = 700;
            var context = new ParseContext(new Scanner("context tryparse success")) { CompilationThreshold = 1 };

            // Act
            bool success = _fakeParser.TryParse(context, out int value, out ParseError? error);

            // Assert
            Assert.True(success);
            Assert.Equal(700, value);
            Assert.Null(error);
        }

        /// <summary>
        /// Tests that TryParse(ParseContext, out T, out ParseError) returns false, default value, and an error when a ParseException is thrown.
        /// </summary>
        [Fact]
        public void TryParse_ParseContext_Exception_ReturnsFalseAndSetsError()
        {
            // Arrange
            _fakeParser.ThrowException = true;
            _fakeParser.ExpectedExceptionMessage = "Parsing error occurred";
            var context = new ParseContext(new Scanner("exception input")) { CompilationThreshold = 1 };

            // Act
            bool success = _fakeParser.TryParse(context, out int value, out ParseError? error);

            // Assert
            Assert.False(success);
            Assert.Equal(default(int), value);
            Assert.NotNull(error);
            Assert.Equal("Parsing error occurred", error!.Message);
            Assert.Equal(10, error.Position);
        }

        /// <summary>
        /// Tests that ToString returns the type name when the Name property is not set.
        /// </summary>
        [Fact]
        public void ToString_NameNotSet_ReturnsTypeName()
        {
            // Arrange
            _fakeParser.Name = null;
            string expected = _fakeParser.GetType().Name;

            // Act
            string result = _fakeParser.ToString();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that ToString returns the Name property value when it is set.
        /// </summary>
        [Fact]
        public void ToString_NameSet_ReturnsName()
        {
            // Arrange
            _fakeParser.Name = "CustomParserName";

            // Act
            string result = _fakeParser.ToString();

            // Assert
            Assert.Equal("CustomParserName", result);
        }

        /// <summary>
        /// Tests that multiple invocations with a higher CompilationThreshold trigger the compilation process.
        /// </summary>
        [Fact]
        public void TryParse_CompilationThreshold_ReachesCompilation_UsesCompiledParser()
        {
            // Arrange
            _fakeParser.ShouldSucceed = true;
            _fakeParser.ReturnValue = 800;
            string input = "compilation test";
            // Set threshold to 2 so that on the second call the parser is compiled.
            var context = new ParseContext(new Scanner(input)) { CompilationThreshold = 2 };

            // Act
            // First call: The parser is not yet compiled.
            int? firstResult = _fakeParser.Parse(input);
            // Second call: Should trigger compilation in CheckCompiled.
            int? secondResult = _fakeParser.Parse(input);

            // Assert
            Assert.Equal(800, firstResult);
            Assert.Equal(800, secondResult);
        }
    }
}
