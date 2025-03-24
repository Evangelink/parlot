using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Moq;
using Parlot.Compilation;
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
        /// Tests the Parse method with a valid identifier (happy path) using a character that is valid as an identifier start.
        /// </summary>
        [Fact]
        public void Parse_ValidIdentifier_HappyPath_ReturnsTrue()
        {
            // Arrange
            // Input string starting with 'a' which is a valid identifier start.
            const string input = "abc123";
            var fakeContext = new FakeParseContext(input);
            var result = new FakeParseResult<TextSpan>();
            var identifier = new Identifier();

            // Act
            bool success = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.True(success);
            Assert.True(result.Success);
            // The expected span is the maximal identifier part from the beginning.
            string expected = input;
            Assert.Equal(expected, result.Value.ToString());
        }

        /// <summary>
        /// Tests the Parse method with an input that does not begin with a valid identifier start character.
        /// </summary>
        [Fact]
        public void Parse_InvalidIdentifier_ReturnsFalse()
        {
            // Arrange
            // Input string starting with '1' which is not a valid identifier start by default.
            const string input = "1abc";
            var fakeContext = new FakeParseContext(input);
            var result = new FakeParseResult<TextSpan>();
            var identifier = new Identifier();

            // Act
            bool success = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.False(success);
            Assert.False(result.Success);
        }

        /// <summary>
        /// Tests the Parse method with an extraStart delegate provided to allow non-standard identifier start characters.
        /// </summary>
        [Fact]
        public void Parse_WithExtraStart_AllowsNonStandardIdentifierStart_ReturnsTrue()
        {
            // Arrange
            // Input string starting with '1' which is normally invalid if extraStart is not provided.
            const string input = "1abc";
            var fakeContext = new FakeParseContext(input);
            var result = new FakeParseResult<TextSpan>();
            // Provide an extraStart lambda that allows digits.
            Func<char, bool> extraStart = ch => char.IsDigit(ch);
            var identifier = new Identifier(extraStart: extraStart);

            // Act
            bool success = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.True(success);
            Assert.True(result.Success);
            // Expected span should cover the entire input if all characters are valid identifier parts.
            string expected = input;
            Assert.Equal(expected, result.Value.ToString());
        }

        /// <summary>
        /// Tests the Parse method with an extraPart delegate provided to allow additional characters in identifier parts.
        /// </summary>
        [Fact]
        public void Parse_WithExtraPart_AllowsAdditionalIdentifierParts_ReturnsTrue()
        {
            // Arrange
            // Input string where '-' is not normally a valid identifier part.
            const string input = "a-b";
            var fakeContext = new FakeParseContext(input);
            var result = new FakeParseResult<TextSpan>();
            // Provide an extraPart lambda that allows '-' character.
            Func<char, bool> extraPart = ch => ch == '-';
            var identifier = new Identifier(extraPart: extraPart);

            // Act
            bool success = identifier.Parse(fakeContext, ref result);

            // Assert
            Assert.True(success);
            Assert.True(result.Success);
            // Expected span should be the entire input.
            string expected = input;
            Assert.Equal(expected, result.Value.ToString());
        }

        /// <summary>
        /// Tests the Compile method to ensure it returns a valid CompilationResult containing a non-empty expression body.
        /// </summary>
        [Fact]
        public void Compile_ReturnsCompilationResult_WithNonEmptyBody()
        {
            // Arrange
            var fakeCompilationContext = new FakeCompilationContext();
            var identifier = new Identifier();

            // Act
            CompilationResult result = identifier.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Body);
            Assert.NotEmpty(result.Body);
        }
    }

    #region Fake and Helper Classes for Parsing

    /// <summary>
    /// A fake implementation of a parse result for testing purposes.
    /// </summary>
    /// <typeparam name="T">Type of the parsed value.</typeparam>
    internal class FakeParseResult<T> : ParseResult<T>
    {
        public bool Success { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public T Value { get; set; }

        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
            Success = true;
        }
    }

    /// <summary>
    /// A fake implementation of a parse context for testing the Parse method.
    /// </summary>
    internal class FakeParseContext : ParseContext
    {
        public FakeScanner Scanner { get; }

        public FakeParseContext(string input)
        {
            Scanner = new FakeScanner(input);
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
    /// A fake scanner that provides a fake cursor over an input string.
    /// </summary>
    internal class FakeScanner
    {
        public FakeCursor Cursor { get; }

        public FakeScanner(string input)
        {
            Cursor = new FakeCursor(input);
        }
    }

    /// <summary>
    /// A fake cursor that simulates scanning over an input string.
    /// </summary>
    internal class FakeCursor
    {
        private readonly string _buffer;
        public int Offset { get; private set; }

        public FakeCursor(string buffer)
        {
            _buffer = buffer;
            Offset = 0;
        }

        public bool Eof => Offset >= _buffer.Length;

        public char Current => !Eof ? _buffer[Offset] : '\0';

        public void AdvanceNoNewLines(int count)
        {
            Offset += count;
            if (Offset > _buffer.Length)
            {
                Offset = _buffer.Length;
            }
        }
    }

    /// <summary>
    /// A minimal implementation of the TextSpan struct for testing purposes.
    /// Assumes that the original TextSpan has a constructor that takes a buffer (string), a start index and a length.
    /// </summary>
    internal readonly struct TextSpan
    {
        private readonly string _buffer;
        public int Start { get; }
        public int Length { get; }

        public TextSpan(string buffer, int start, int length)
        {
            _buffer = buffer;
            Start = start;
            Length = length;
        }

        public override string ToString() => _buffer.Substring(Start, Length);
    }

    #endregion

    #region Fake and Helper Classes for Compilation

    /// <summary>
    /// A fake implementation of CompilationResult for testing the Compile method.
    /// </summary>
    internal class FakeCompilationResult<T> : CompilationResult
    {
        public List<Expression> Body { get; } = new List<Expression>();

        public Expression Value { get; set; }

        public FakeCompilationResult()
        {
            // Initialize Success as false.
            Success = false;
        }
    }

    /// <summary>
    /// A fake implementation of a CompilationResult.
    /// </summary>
    internal abstract class CompilationResult
    {
        public bool Success { get; set; }
        public List<Expression> Body { get; } = new List<Expression>();
    }

    /// <summary>
    /// A fake implementation of CompilationContext for testing the Compile method.
    /// </summary>
    internal class FakeCompilationContext : CompilationContext
    {
        private int _number = 0;

        public override int NextNumber => ++_number;

        public override CompilationResult CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }

        public override ParameterExpression Current()
        {
            // Returns a dummy parameter expression of type char.
            return Expression.Parameter(typeof(char), "dummy");
        }

        public override Expression Offset()
        {
            // Returns a constant expression representing the current offset (simulate as 0).
            return Expression.Constant(0);
        }

        public override Expression AdvanceNoNewLine(Expression count)
        {
            // Simulate advancing the cursor; no operation needed.
            return Expression.Empty();
        }

        public override Expression Eof()
        {
            // Simulate never reaching EOF.
            return Expression.Constant(false);
        }

        public override Expression Buffer()
        {
            // Return a dummy buffer.
            return Expression.Constant("dummy");
        }

        public override Expression NewTextSpan(Expression buffer, Expression start, Expression length)
        {
            // Create a dummy TextSpan expression.
            // Note: In real compilation, this would construct a TextSpan, but for testing we return a constant.
            return Expression.Constant(new TextSpan("dummy", 0, 0));
        }

        public override bool DiscardResult => false;
    }

    /// <summary>
    /// A fake base class for CompilationContext to satisfy the Compile method dependencies.
    /// Assumes that the original CompilationContext is abstract with the following members.
    /// </summary>
    internal abstract class CompilationContext
    {
        public abstract int NextNumber { get; }
        public abstract CompilationResult CreateCompilationResult<T>();
        public abstract ParameterExpression Current();
        public abstract Expression Offset();
        public abstract Expression AdvanceNoNewLine(Expression count);
        public abstract Expression Eof();
        public abstract Expression Buffer();
        public abstract Expression NewTextSpan(Expression buffer, Expression start, Expression length);
        public abstract bool DiscardResult { get; }
    }

    #endregion
}
