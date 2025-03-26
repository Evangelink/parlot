using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Identifier"/> class.
    /// </summary>
    public class IdentifierTests
    {
        /// <summary>
        /// Tests that Parse returns true and sets the correct TextSpan when a valid identifier is provided,
        /// without any extra start/part functions.
        /// </summary>
        [Fact]
        public void Parse_ValidIdentifier_NoExtra_ReturnsTrueAndSetsCorrectTextSpan()
        {
            // Arrange
            string input = "abc123";
            var fakeCursor = new FakeCursor(input);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<FakeTextSpan>();

            var identifier = new Identifier();

            // Act
            bool parseResult = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(0, result.Start);
            Assert.Equal(input.Length, result.End);
            Assert.Equal(input, result.Value.Buffer.Substring(result.Value.Start, result.Value.Length));
        }

        /// <summary>
        /// Tests that Parse returns false when the first character is not a valid identifier start and no extra function allows it.
        /// </summary>
        [Fact]
        public void Parse_InvalidIdentifier_NoExtra_ReturnsFalse()
        {
            // Arrange
            // '1' is typically not a valid identifier start.
            string input = "1abc";
            var fakeCursor = new FakeCursor(input);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<FakeTextSpan>();

            var identifier = new Identifier();

            // Act
            bool parseResult = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
        }

        /// <summary>
        /// Tests that Parse returns true and correctly parses the identifier when extraStart function
        /// allows digits as a valid start character.
        /// </summary>
        [Fact]
        public void Parse_WithExtraStart_AllowsDigitAsStart_ReturnsTrueAndParsesEntireIdentifier()
        {
            // Arrange
            string input = "1abc";
            var fakeCursor = new FakeCursor(input);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<FakeTextSpan>();

            // extraStart allows digit as valid start.
            Func<char, bool> extraStart = c => char.IsDigit(c);
            var identifier = new Identifier(extraStart: extraStart);

            // Act
            bool parseResult = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(0, result.Start);
            Assert.Equal(input.Length, result.End);
            Assert.Equal(input, result.Value.Buffer.Substring(result.Value.Start, result.Value.Length));
        }

        /// <summary>
        /// Tests that Parse returns true and correctly parses an identifier when extraPart function
        /// allows additional characters (e.g., '-') within the identifier.
        /// </summary>
        [Fact]
        public void Parse_WithExtraPart_AllowsHyphenInIdentifier_ReturnsTrueAndParsesEntireIdentifier()
        {
            // Arrange
            // 'a' is valid start, '-' is not normally a valid part so extraPart will allow it.
            string input = "a-bc";
            var fakeCursor = new FakeCursor(input);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<FakeTextSpan>();

            Func<char, bool> extraPart = c => c == '-';
            var identifier = new Identifier(extraPart: extraPart);

            // Act
            bool parseResult = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(0, result.Start);
            Assert.Equal(input.Length, result.End);
            Assert.Equal(input, result.Value.Buffer.Substring(result.Value.Start, result.Value.Length));
        }

        /// <summary>
        /// Tests that Parse returns false when provided with an empty input.
        /// </summary>
        [Fact]
        public void Parse_EmptyInput_ReturnsFalse()
        {
            // Arrange
            string input = "";
            var fakeCursor = new FakeCursor(input);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<FakeTextSpan>();

            var identifier = new Identifier();

            // Act
            bool parseResult = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
        }

        /// <summary>
        /// Tests that Compile returns a non-null CompilationResult containing expected expression blocks and variables.
        /// </summary>
        [Fact]
        public void Compile_ReturnsCompilationResult_WithExpressionsAndVariables()
        {
            // Arrange
            var fakeCompilationContext = new FakeCompilationContext();
            var identifier = new Identifier();

            // Act
            var compilationResult = identifier.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
            Assert.NotEmpty(compilationResult.Variables);
            // Check that one of the variables is of type char (the "first" parameter)
            bool hasCharParameter = compilationResult.Variables.Exists(p => p.Type == typeof(char));
            Assert.True(hasCharParameter);
        }

        /// <summary>
        /// Tests that Compile returns a valid CompilationResult when extra functions are provided.
        /// </summary>
        [Fact]
        public void Compile_WithExtraFunctions_ReturnsCompilationResult_WithExpressionsAndVariables()
        {
            // Arrange
            Func<char, bool> extraStart = c => char.IsDigit(c);
            Func<char, bool> extraPart = c => c == '-';
            var fakeCompilationContext = new FakeCompilationContext();
            var identifier = new Identifier(extraStart, extraPart);

            // Act
            var compilationResult = identifier.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
            Assert.NotEmpty(compilationResult.Variables);
        }
    }

    #region Fake Implementations for Parse

    /// <summary>
    /// Fake implementation of a TextSpan used for testing.
    /// </summary>
    public readonly struct FakeTextSpan
    {
        public string Buffer { get; }
        public int Start { get; }
        public int Length { get; }

        public FakeTextSpan(string buffer, int start, int length)
        {
            Buffer = buffer;
            Start = start;
            Length = length;
        }
    }

    /// <summary>
    /// Fake cursor simulating the behavior of a scanner's cursor.
    /// </summary>
    public class FakeCursor
    {
        private readonly string _buffer;
        public int Offset { get; private set; }

        public FakeCursor(string buffer)
        {
            _buffer = buffer;
            Offset = 0;
        }

        public char Current => Offset < _buffer.Length ? _buffer[Offset] : '\0';

        public bool Eof => Offset >= _buffer.Length;

        public void AdvanceNoNewLines(int count)
        {
            Offset += count;
        }

        public string Buffer => _buffer;
    }

    /// <summary>
    /// Fake scanner containing a fake cursor.
    /// </summary>
    public class FakeScanner
    {
        public FakeCursor Cursor { get; }

        public FakeScanner(FakeCursor cursor)
        {
            Cursor = cursor;
        }
    }

    /// <summary>
    /// Fake parse context used for testing.
    /// </summary>
    public class FakeParseContext
    {
        public FakeScanner Scanner { get; }

        public FakeParseContext(FakeScanner scanner)
        {
            Scanner = scanner;
        }

        public void EnterParser(object parser)
        {
            // No-op for testing.
        }

        public void ExitParser(object parser)
        {
            // No-op for testing.
        }
    }

    /// <summary>
    /// Fake parse result for capturing parsing outcomes.
    /// </summary>
    /// <typeparam name="T">Type of the parsed value.</typeparam>
    public class FakeParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T Value { get; private set; }
        public bool Success { get; private set; }

        public void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
            Success = true;
        }
    }

    #endregion

    #region Fake Implementations for Compilation

    /// <summary>
    /// Fake implementation of a compilation result.
    /// </summary>
    public class FakeCompilationResult<T>
    {
        public List<Expression> Body { get; } = new List<Expression>();
        public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
        public Expression Value { get; set; }
        public Expression Success { get; set; }
    }

    /// <summary>
    /// Fake compilation context used for testing the Compile method.
    /// </summary>
    public class FakeCompilationContext
    {
        private int _nextNumber = 1;

        public int NextNumber => _nextNumber++;

        public FakeCompilationResult<FakeTextSpan> CreateCompilationResult<FakeTextSpan>()
        {
            return new FakeCompilationResult<FakeTextSpan>();
        }

        public Expression Current()
        {
            return Expression.Constant('a');
        }

        public Expression Offset()
        {
            return Expression.Constant(0);
        }

        public Expression AdvanceNoNewLine(Expression amount)
        {
            return Expression.Empty();
        }

        public Expression Eof()
        {
            return Expression.Constant(false);
        }

        public Expression Buffer()
        {
            return Expression.Constant("dummy buffer");
        }

        public bool DiscardResult { get; set; } = false;

        public Expression NewTextSpan(Expression buffer, Expression start, Expression length)
        {
            // For testing, return a constant expression.
            return Expression.Constant(new FakeTextSpan("dummy buffer", 0, 0));
        }
    }

    #endregion
}
