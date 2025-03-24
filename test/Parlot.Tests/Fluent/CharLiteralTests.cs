using System.Collections.Generic;
using System.Linq.Expressions;
using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Rewriting;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CharLiteral"/> class.
    /// </summary>
    public class CharLiteralTests
    {
        private readonly char _testChar;

        public CharLiteralTests()
        {
            _testChar = 'a';
        }

        /// <summary>
        /// Tests that the constructor initializes the properties correctly.
        /// </summary>
        [Fact]
        public void Constructor_ValidCharacter_PropertiesSet()
        {
            // Arrange & Act
            var charLiteral = new CharLiteral(_testChar);

            // Assert
            Assert.Equal(_testChar, charLiteral.Char);
            Assert.True(charLiteral.CanSeek);
            Assert.NotNull(charLiteral.ExpectedChars);
            Assert.Single(charLiteral.ExpectedChars);
            Assert.Equal(_testChar, charLiteral.ExpectedChars[0]);
        }

        /// <summary>
        /// Tests the Parse method when the expected character is present at the beginning of the input.
        /// </summary>
        [Fact]
        public void Parse_CharacterPresent_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var input = _testChar + "bc";
            var fakeContext = new FakeParseContext(input);
            var result = new FakeParseResult<char>();
            var charLiteral = new CharLiteral(_testChar);
            int initialOffset = fakeContext.Scanner.Cursor.Offset;

            // Act
            bool parseResult = charLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(initialOffset, result.Start);
            Assert.Equal(initialOffset + 1, result.End);
            Assert.Equal(_testChar, result.Value);
        }

        /// <summary>
        /// Tests the Parse method when the expected character is absent.
        /// </summary>
        [Fact]
        public void Parse_CharacterAbsent_ReturnsFalse()
        {
            // Arrange
            var input = "xyz";
            var fakeContext = new FakeParseContext(input);
            var result = new FakeParseResult<char>();
            var charLiteral = new CharLiteral(_testChar); // expecting 'a'
            int initialOffset = fakeContext.Scanner.Cursor.Offset;

            // Act
            bool parseResult = charLiteral.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            // Validate that the cursor did not advance on a failed match.
            Assert.Equal(initialOffset, fakeContext.Scanner.Cursor.Offset);
        }

        /// <summary>
        /// Tests the Compile method to verify that it returns a compilation result with an expression body.
        /// </summary>
        [Fact]
        public void Compile_ReturnsCompilationResultWithExpressionBody()
        {
            // Arrange
            var fakeCompilationContext = new FakeCompilationContext();
            var charLiteral = new CharLiteral(_testChar);

            // Act
            var compilationResult = charLiteral.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotNull(compilationResult.Body);
            Assert.NotEmpty(compilationResult.Body);
            // Verify that the first expression is a conditional (if-then) expression.
            Assert.IsType<ConditionalExpression>(compilationResult.Body[0]);
        }

        /// <summary>
        /// Tests that the ToString method returns the expected formatted string.
        /// </summary>
        [Fact]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var charLiteral = new CharLiteral(_testChar);
            var expected = $"Char('{_testChar}')";
            
            // Act
            var actual = charLiteral.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }

    // Fake implementations to support testing of the CharLiteral class.

    /// <summary>
    /// A fake implementation of ParseContext for testing purposes.
    /// </summary>
    internal class FakeParseContext : ParseContext
    {
        public FakeParseContext(string input)
        {
            Scanner = new FakeScanner(input);
        }

        public override IScanner Scanner { get; }

        public override void EnterParser(object parser)
        {
            // No action needed for fake context.
        }

        public override void ExitParser(object parser)
        {
            // No action needed for fake context.
        }
    }

    /// <summary>
    /// A fake implementation of IScanner for testing purposes.
    /// </summary>
    internal class FakeScanner : IScanner
    {
        public FakeScanner(string input)
        {
            Cursor = new FakeCursor(input);
        }

        public ICursor Cursor { get; }
    }

    /// <summary>
    /// A fake implementation of ICursor to simulate a scanning cursor.
    /// </summary>
    internal class FakeCursor : ICursor
    {
        private readonly string _input;
        private int _position;

        public FakeCursor(string input)
        {
            _input = input;
            _position = 0;
        }

        public int Offset => _position;

        public bool Match(char c)
        {
            if (_position < _input.Length)
            {
                return _input[_position] == c;
            }
            return false;
        }

        public void Advance()
        {
            if (_position < _input.Length)
            {
                _position++;
            }
        }
    }

    /// <summary>
    /// A fake implementation of ParseResult for testing purposes.
    /// </summary>
    internal class FakeParseResult<T> : ParseResult<T>
    {
        public FakeParseResult()
        {
            Start = 0;
            End = 0;
        }

        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    /// <summary>
    /// A fake implementation of CompilationContext for testing purposes.
    /// </summary>
    internal class FakeCompilationContext : CompilationContext
    {
        public override bool DiscardResult { get; set; } = false;

        public override CompilationResult<T> CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }

        public override Expression ReadChar(char c)
        {
            // Returns a constant expression to simulate a successful character read.
            return Expression.Constant(true);
        }
    }

    /// <summary>
    /// A fake implementation of CompilationResult for testing purposes.
    /// </summary>
    internal class FakeCompilationResult<T> : CompilationResult<T>
    {
        public FakeCompilationResult()
        {
            Body = new List<Expression>();
            Success = Expression.Parameter(typeof(bool), "success");
            Value = Expression.Parameter(typeof(T), "value");
        }

        public override List<Expression> Body { get; }
        public override ParameterExpression Success { get; }
        public override ParameterExpression Value { get; }
    }

    // Minimal abstract base classes and interfaces to support the fake implementations.

    /// <summary>
    /// Minimal representation of the ParseContext abstract class.
    /// </summary>
    public abstract class ParseContext
    {
        public abstract IScanner Scanner { get; }
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
    }

    /// <summary>
    /// Minimal representation of the IScanner interface.
    /// </summary>
    public interface IScanner
    {
        ICursor Cursor { get; }
    }

    /// <summary>
    /// Minimal representation of the ICursor interface.
    /// </summary>
    public interface ICursor
    {
        int Offset { get; }
        bool Match(char c);
        void Advance();
    }

    /// <summary>
    /// Minimal representation of the ParseResult abstract class.
    /// </summary>
    public abstract class ParseResult<T>
    {
        public int Start { get; protected set; }
        public int End { get; protected set; }
        public T Value { get; protected set; }
        public abstract void Set(int start, int end, T value);
    }

    /// <summary>
    /// Minimal representation of the CompilationContext abstract class.
    /// </summary>
    public abstract class CompilationContext
    {
        public abstract bool DiscardResult { get; set; }
        public abstract CompilationResult<T> CreateCompilationResult<T>();
        public abstract Expression ReadChar(char c);
    }

    /// <summary>
    /// Minimal representation of the CompilationResult abstract class.
    /// </summary>
    public abstract class CompilationResult<T>
    {
        public abstract List<Expression> Body { get; }
        public abstract ParameterExpression Success { get; }
        public abstract ParameterExpression Value { get; }
    }
}
