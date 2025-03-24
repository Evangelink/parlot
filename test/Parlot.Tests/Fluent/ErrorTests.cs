using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using Xunit;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    #region Fake Classes for Parsing

    /// <summary>
    /// A fake implementation of a ParseContext used for testing.
    /// </summary>
    public class FakeParseContext
    {
        public FakeScanner Scanner { get; } = new FakeScanner();

        public void EnterParser(object parser)
        {
            // No-op for fake context.
        }

        public void ExitParser(object parser)
        {
            // No-op for fake context.
        }
    }

    /// <summary>
    /// A fake scanner containing a fake cursor.
    /// </summary>
    public class FakeScanner
    {
        public FakeCursor Cursor { get; } = new FakeCursor();
    }

    /// <summary>
    /// A fake cursor with a preset position.
    /// </summary>
    public class FakeCursor
    {
        public int Position { get; set; } = 123;
    }

    /// <summary>
    /// A fake ParseResult used in testing the Parse methods.
    /// </summary>
    /// <typeparam name="T">The type parsed.</typeparam>
    public class FakeParseResult<T>
    {
        public T Value { get; set; }
    }

    #endregion

    #region Fake Classes for Compilation

    /// <summary>
    /// A fake compilation context for testing Compile methods.
    /// </summary>
    public class FakeCompilationContext
    {
        public bool DiscardResult { get; set; } = false;

        public FakeCompilationResult<T> CreateCompilationResult<T>(bool requireResult = false)
        {
            return new FakeCompilationResult<T>();
        }

        public Expression ThrowParseException(Expression message)
        {
            // Return an expression that throws a dummy exception.
            return Expression.Throw(Expression.New(typeof(Exception).GetConstructor(new[] { typeof(string) }),
                message));
        }
    }

    /// <summary>
    /// A fake compilation result for testing.
    /// </summary>
    /// <typeparam name="T">The type being compiled.</typeparam>
    public class FakeCompilationResult<T>
    {
        public List<Expression> Body { get; } = new List<Expression>();
        public ParameterExpression Value { get; } = Expression.Parameter(typeof(T), "value");
    }

    /// <summary>
    /// A fake result returned from a parser's Build method.
    /// </summary>
    /// <typeparam name="T">The type parameter of the parser.</typeparam>
    public class FakeParserCompilationResult<T>
    {
        public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
        public List<Expression> Body { get; } = new List<Expression>();
        public Expression Success { get; set; } = Expression.Constant(true);
        public Expression Value { get; set; } = Expression.Constant(default(T));
    }

    #endregion

    /// <summary>
    /// Unit tests for the <see cref="ElseError{T}"/> class.
    /// </summary>
    public class ElseErrorTests
    {
        private readonly string _errorMessage = "Error occurred in ElseError";
        
        /// <summary>
        /// Tests that the constructor of ElseError throws ArgumentNullException when a null parser is provided.
        /// </summary>
        [Fact]
        public void Ctor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ElseError<int>(null, _errorMessage));
        }

        /// <summary>
        /// Tests that Parse returns true and does not throw when underlying parser returns true.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserSucceeds_ReturnsTrue()
        {
            // Arrange
            var fakeResult = new FakeParseResult<int>();
            var fakeContext = new FakeParseContext();
            var mockParser = new Mock<Parser<int>>();
            // Setup underlying parser to return true.
            mockParser.Setup(p => p.Parse(It.IsAny<object>(), ref It.Ref<FakeParseResult<int>>.IsAny))
                      .Returns(true)
                      .Callback((object ctx, ref FakeParseResult<int> res) => res.Value = 42);
            var elseError = new ElseError<int>(mockParser.Object, _errorMessage);

            // Act
            bool result = elseError.Parse(fakeContext, ref fakeResult);

            // Assert
            Assert.True(result);
            Assert.Equal(42, fakeResult.Value);
        }

        /// <summary>
        /// Tests that Parse throws a ParseException with the provided message when the underlying parser fails.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserFails_ThrowsParseException()
        {
            // Arrange
            var fakeResult = new FakeParseResult<int>();
            var fakeContext = new FakeParseContext();
            var mockParser = new Mock<Parser<int>>();
            // Setup underlying parser to return false.
            mockParser.Setup(p => p.Parse(It.IsAny<object>(), ref It.Ref<FakeParseResult<int>>.IsAny))
                      .Returns(false);
            var elseError = new ElseError<int>(mockParser.Object, _errorMessage);

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => elseError.Parse(fakeContext, ref fakeResult));
            Assert.Equal(_errorMessage, exception.Message);
            Assert.Equal(fakeContext.Scanner.Cursor.Position, exception.ErrorPosition);
        }

        /// <summary>
        /// Tests that ToString returns the underlying parser string appended with " (ElseError)".
        /// </summary>
        [Fact]
        public void ToString_ReturnsUnderlyingParserToStringWithElseErrorSuffix()
        {
            // Arrange
            var fakeParser = new Mock<Parser<int>>();
            fakeParser.Setup(p => p.ToString()).Returns("FakeParser");
            var elseError = new ElseError<int>(fakeParser.Object, _errorMessage);

            // Act
            var text = elseError.ToString();

            // Assert
            Assert.Equal("FakeParser (ElseError)", text);
        }

        /// <summary>
        /// Tests the Compile method to ensure it returns a non-null compilation result.
        /// </summary>
        [Fact]
        public void Compile_ValidContext_ReturnsCompilationResultWithBody()
        {
            // Arrange
            var fakeContext = new FakeCompilationContext();
            var mockParser = new Mock<Parser<int>>();
            var fakeParserCompilationResult = new FakeParserCompilationResult<int>();
            // For testing, add a dummy expression in body.
            fakeParserCompilationResult.Body.Add(Expression.Constant(1));
            mockParser.Setup(p => p.Build(It.IsAny<FakeCompilationContext>(), true))
                      .Returns(fakeParserCompilationResult);

            var elseError = new ElseError<int>(mockParser.Object, _errorMessage);

            // Act
            var result = elseError.Compile(fakeContext);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Body);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="Error{T}"/> class.
    /// </summary>
    public class ErrorTests
    {
        private readonly string _errorMessage = "Error occurred in Error";

        /// <summary>
        /// Tests that the constructor of Error throws ArgumentNullException when a null parser is provided.
        /// </summary>
        [Fact]
        public void Ctor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Error<int>(null, _errorMessage));
        }

        /// <summary>
        /// Tests that Parse throws a ParseException when the underlying parser succeeds.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserSucceeds_ThrowsParseException()
        {
            // Arrange
            var fakeResult = new FakeParseResult<int>();
            var fakeContext = new FakeParseContext();
            var mockParser = new Mock<Parser<int>>();
            // Setup underlying parser to return true.
            mockParser.Setup(p => p.Parse(It.IsAny<object>(), ref It.Ref<FakeParseResult<int>>.IsAny))
                      .Returns(true);
            var errorParser = new Error<int>(mockParser.Object, _errorMessage);

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => errorParser.Parse(fakeContext, ref fakeResult));
            Assert.Equal(_errorMessage, exception.Message);
            Assert.Equal(fakeContext.Scanner.Cursor.Position, exception.ErrorPosition);
        }

        /// <summary>
        /// Tests that Parse returns false when the underlying parser fails.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserFails_ReturnsFalse()
        {
            // Arrange
            var fakeResult = new FakeParseResult<int>();
            var fakeContext = new FakeParseContext();
            var mockParser = new Mock<Parser<int>>();
            // Setup underlying parser to return false.
            mockParser.Setup(p => p.Parse(It.IsAny<object>(), ref It.Ref<FakeParseResult<int>>.IsAny))
                      .Returns(false);
            var errorParser = new Error<int>(mockParser.Object, _errorMessage);

            // Act
            bool result = errorParser.Parse(fakeContext, ref fakeResult);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Tests that ToString returns the underlying parser string appended with " (Error)".
        /// </summary>
        [Fact]
        public void ToString_ReturnsUnderlyingParserToStringWithErrorSuffix()
        {
            // Arrange
            var fakeParser = new Mock<Parser<int>>();
            fakeParser.Setup(p => p.ToString()).Returns("FakeParser");
            var errorParser = new Error<int>(fakeParser.Object, _errorMessage);

            // Act
            var text = errorParser.ToString();

            // Assert
            Assert.Equal("FakeParser (Error)", text);
        }

        /// <summary>
        /// Tests the Compile method to ensure it returns a non-null compilation result with a non-empty body.
        /// </summary>
        [Fact]
        public void Compile_ValidContext_ReturnsCompilationResultWithBody()
        {
            // Arrange
            var fakeContext = new FakeCompilationContext();
            var mockParser = new Mock<Parser<int>>();
            var fakeParserCompilationResult = new FakeParserCompilationResult<int>();
            fakeParserCompilationResult.Body.Add(Expression.Constant(1));
            mockParser.Setup(p => p.Build(It.IsAny<FakeCompilationContext>(), false))
                      .Returns(fakeParserCompilationResult);

            var errorParser = new Error<int>(mockParser.Object, _errorMessage);

            // Act
            var result = errorParser.Compile(fakeContext);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Body);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="Error{T, U}"/> class.
    /// </summary>
    public class ErrorGenericTests
    {
        private readonly string _errorMessage = "Error occurred in Error<T, U>";

        /// <summary>
        /// Tests that the constructor of Error{T, U} throws ArgumentNullException when a null parser is provided.
        /// </summary>
        [Fact]
        public void Ctor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Error<int, string>(null, _errorMessage));
        }

        /// <summary>
        /// Tests that Parse throws a ParseException when the underlying parser succeeds.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserSucceeds_ThrowsParseException()
        {
            // Arrange
            var fakeResult = new FakeParseResult<string>();
            var fakeContext = new FakeParseContext();
            var mockParser = new Mock<Parser<int>>();
            // Setup underlying parser to return true.
            mockParser.Setup(p => p.Parse(It.IsAny<object>(), ref It.Ref<FakeParseResult<int>>.IsAny))
                      .Returns(true);
            var errorParser = new Error<int, string>(mockParser.Object, _errorMessage);

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => errorParser.Parse(fakeContext, ref fakeResult));
            Assert.Equal(_errorMessage, exception.Message);
            Assert.Equal(fakeContext.Scanner.Cursor.Position, exception.ErrorPosition);
        }

        /// <summary>
        /// Tests that Parse returns false when the underlying parser fails.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserFails_ReturnsFalse()
        {
            // Arrange
            var fakeResult = new FakeParseResult<string>();
            var fakeContext = new FakeParseContext();
            var mockParser = new Mock<Parser<int>>();
            // Setup underlying parser to return false.
            mockParser.Setup(p => p.Parse(It.IsAny<object>(), ref It.Ref<FakeParseResult<int>>.IsAny))
                      .Returns(false);
            var errorParser = new Error<int, string>(mockParser.Object, _errorMessage);

            // Act
            bool result = errorParser.Parse(fakeContext, ref fakeResult);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Tests that ToString returns the underlying parser string appended with " (Error)".
        /// </summary>
        [Fact]
        public void ToString_ReturnsUnderlyingParserToStringWithErrorSuffix()
        {
            // Arrange
            var fakeParser = new Mock<Parser<int>>();
            fakeParser.Setup(p => p.ToString()).Returns("FakeParser");
            var errorParser = new Error<int, string>(fakeParser.Object, _errorMessage);

            // Act
            var text = errorParser.ToString();

            // Assert
            Assert.Equal("FakeParser (Error)", text);
        }

        /// <summary>
        /// Tests the Compile method to ensure it returns a non-null compilation result with a non-empty body.
        /// </summary>
        [Fact]
        public void Compile_ValidContext_ReturnsCompilationResultWithBody()
        {
            // Arrange
            var fakeContext = new FakeCompilationContext();
            var mockParser = new Mock<Parser<int>>();
            var fakeParserCompilationResult = new FakeParserCompilationResult<int>();
            fakeParserCompilationResult.Body.Add(Expression.Constant(1));
            mockParser.Setup(p => p.Build(It.IsAny<FakeCompilationContext>(), false))
                      .Returns(fakeParserCompilationResult);

            var errorParser = new Error<int, string>(mockParser.Object, _errorMessage);

            // Act
            var result = errorParser.Compile(fakeContext);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Body);
        }
    }

    #region Dummy ParseException Definition

    /// <summary>
    /// A dummy ParseException class for testing purposes.
    /// </summary>
    public class ParseException : Exception
    {
        public int ErrorPosition { get; }

        public ParseException(string message, int errorPosition)
            : base(message)
        {
            ErrorPosition = errorPosition;
        }
    }

    #endregion

    #region Dummy Parser Base Class Definition

    /// <summary>
    /// A dummy abstract Parser class to allow testing of parser combinators.
    /// </summary>
    /// <typeparam name="T">The type that the parser produces.</typeparam>
    public abstract class Parser<T>
    {
        public abstract bool Parse(object context, ref FakeParseResult<T> result);

        public virtual object Build(object context, bool requireResult)
        {
            // This dummy implementation is only used in tests.
            return null;
        }
    }

    #endregion
}
