using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using Xunit;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="OneOrMany{T}"/> class.
    /// </summary>
    public class OneOrManyTests
    {
        /// <summary>
        /// Tests that constructing a OneOrMany instance with a null parser throws an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new OneOrMany<int>(null!));
            Assert.Equal("parser", exception.ParamName);
        }

        /// <summary>
        /// Tests that Parse returns false when the underlying parser fails on the first attempt.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserFailsInitially_ReturnsFalse()
        {
            // Arrange
            var fakeParser = new FakeParserForParse(new List<int>());
            var oneOrMany = new OneOrMany<int>(fakeParser);
            var context = new FakeParseContext();
            var result = new FakeParseResult<IReadOnlyList<int>>();

            // Act
            bool parseResult = oneOrMany.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.Null(result.Value);
        }

        /// <summary>
        /// Tests that Parse returns a collection with one element when the underlying parser succeeds only once.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserSucceedsOnce_ReturnsOneResult()
        {
            // Arrange
            var expectedValues = new List<int> { 42 };
            var fakeParser = new FakeParserForParse(expectedValues);
            var oneOrMany = new OneOrMany<int>(fakeParser);
            var context = new FakeParseContext();
            var result = new FakeParseResult<IReadOnlyList<int>>();

            // Act
            bool parseResult = oneOrMany.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.NotNull(result.Value);
            Assert.Single(result.Value);
            Assert.Equal(42, result.Value[0]);
        }

        /// <summary>
        /// Tests that Parse collects all consecutive successful parse results from the underlying parser.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserSucceedsMultipleTimes_ReturnsAllResults()
        {
            // Arrange
            var expectedValues = new List<int> { 1, 2, 3, 4 };
            var fakeParser = new FakeParserForParse(expectedValues);
            var oneOrMany = new OneOrMany<int>(fakeParser);
            var context = new FakeParseContext();
            var result = new FakeParseResult<IReadOnlyList<int>>();

            // Act
            bool parseResult = oneOrMany.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.NotNull(result.Value);
            Assert.Equal(expectedValues.Count, result.Value.Count);
            Assert.Equal(expectedValues, result.Value);
        }

        /// <summary>
        /// Tests that when the underlying parser implements ISeekable, OneOrMany properties reflect the underlying parser's properties.
        /// </summary>
        [Fact]
        public void Constructor_WhenUnderlyingParserImplementsISeekable_SetsProperties()
        {
            // Arrange
            var expectedChars = new char[] { 'x', 'y' };
            var fakeParser = new FakeParserWithISeekable(new List<int> { 7 })
            {
                SeekableCanSeek = true,
                SeekableExpectedChars = expectedChars,
                SeekableSkipWhitespace = true
            };

            // Act
            var oneOrMany = new OneOrMany<int>(fakeParser);

            // Assert
            Assert.True(oneOrMany.CanSeek);
            Assert.Equal(expectedChars, oneOrMany.ExpectedChars);
            Assert.True(oneOrMany.SkipWhitespace);
        }

        /// <summary>
        /// Tests that the ToString method returns the underlying parser's string representation with a trailing '+'.
        /// </summary>
        [Fact]
        public void ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var fakeParser = new FakeParserForParse(new List<int> { }) { FakeToString = "FakeParser" };
            var oneOrMany = new OneOrMany<int>(fakeParser);

            // Act
            string result = oneOrMany.ToString();

            // Assert
            Assert.Equal("FakeParser+", result);
        }

        /// <summary>
        /// Tests that Compile returns a compilation result with a non-empty body.
        /// </summary>
        [Fact]
        public void Compile_WhenInvoked_ReturnsNonEmptyCompilationResult()
        {
            // Arrange
            var fakeCompilableParser = new FakeCompilableParser();
            var oneOrMany = new OneOrMany<int>(fakeCompilableParser);
            var compilationContext = new FakeCompilationContext();

            // Act
            var compilationResult = oneOrMany.Compile(compilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
        }
    }

    #region Fake Implementations for Parse

    // Minimal stub for ParseContext.
    internal class FakeParseContext : ParseContext
    {
        public override void EnterParser(object parser)
        {
            // No-op for testing.
        }

        public override void ExitParser(object parser)
        {
            // No-op for testing.
        }
    }

    // Minimal stub for ParseResult<T>.
    internal class FakeParseResult<T> : ParseResult<T>
    {
        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    // Fake parser that simulates parsing by returning items from a predetermined collection.
    internal class FakeParserForParse : Parser<int>
    {
        private readonly Queue<int> _items;
        public string FakeToString { get; set; } = "FakeParser";

        public FakeParserForParse(IEnumerable<int> items)
        {
            _items = new Queue<int>(items);
        }

        public override bool Parse(ParseContext context, ref ParseResult<int> result)
        {
            if (_items.Count == 0)
            {
                return false;
            }
            int value = _items.Dequeue();
            result.Set(0, 1, value);
            return true;
        }

        public override string ToString()
        {
            return FakeToString;
        }
    }

    // Fake parser that also implements ISeekable.
    internal class FakeParserWithISeekable : FakeParserForParse, ISeekable
    {
        public bool SeekableCanSeek { get; set; }
        public char[] SeekableExpectedChars { get; set; } = Array.Empty<char>();
        public bool SeekableSkipWhitespace { get; set; }

        public FakeParserWithISeekable(IEnumerable<int> items)
            : base(items)
        {
        }

        public bool CanSeek => SeekableCanSeek;

        public char[] ExpectedChars => SeekableExpectedChars;

        public bool SkipWhitespace => SeekableSkipWhitespace;
    }

    #endregion

    #region Fake Implementations for Compilation

    // Minimal stubs for CompilationContext and CompilationResult.

    internal abstract class CompilationContext
    {
        public abstract int NextNumber { get; }
        public bool DiscardResult { get; set; }
        public abstract CompilationResult CreateCompilationResult<T>();
        public abstract Expression Eof();
    }

    internal abstract class CompilationResult
    {
        public abstract List<ParameterExpression> Variables { get; }
        public abstract List<Expression> Body { get; }
        public abstract ParameterExpression Success { get; }
        public abstract ParameterExpression Value { get; }
    }

    internal class FakeCompilationContext : CompilationContext
    {
        private int _counter = 0;
        public override int NextNumber => ++_counter;
        public override CompilationResult CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }
        public override Expression Eof() => Expression.Constant(false);
    }

    internal class FakeCompilationResult<T> : CompilationResult
    {
        public override List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
        public override List<Expression> Body { get; } = new List<Expression>();
        public override ParameterExpression Success { get; } = Expression.Variable(typeof(bool), "success");
        public override ParameterExpression Value { get; } = Expression.Variable(typeof(T), "value");

        public ParameterExpression DeclareVariable<U>(string name, NewExpression initializer)
        {
            var variable = Expression.Variable(typeof(U), name);
            Variables.Add(variable);
            Body.Add(Expression.Assign(variable, initializer));
            return variable;
        }
    }

    // Fake compilable parser that returns a dummy compilation result.
    internal class FakeCompilableParser : Parser<int>, ICompilable, ISeekable
    {
        public bool CanSeek => true;
        public char[] ExpectedChars => new char[] { 'a', 'b' };
        public bool SkipWhitespace => false;

        public override bool Parse(ParseContext context, ref ParseResult<int> result)
        {
            // Not used in compile tests.
            return false;
        }

        public CompilationResult Build(CompilationContext context)
        {
            var compResult = context.CreateCompilationResult<IReadOnlyList<int>>();
            // For test purposes, add a simple constant expression to the body.
            compResult.Body.Add(Expression.Constant(true));
            return compResult;
        }
    }

    #endregion

    #region Minimal Abstract Base Classes (Stubs)

    // Minimal abstract definitions to support the fake implementations used in tests.
    internal abstract class ParseContext
    {
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
    }

    internal abstract class ParseResult<T>
    {
        public int Start { get; set; }
        public int End { get; set; }
        public T? Value { get; set; }
        public abstract void Set(int start, int end, T value);
    }

    // Minimal abstract definition for Parser<T> to allow fake implementations.
    internal abstract class Parser<T>
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
    }

    // Minimal definitions for ICompilable and ISeekable, as required by OneOrMany<T>.
    internal interface ICompilable
    {
        CompilationResult Build(CompilationContext context);
    }

    internal interface ISeekable
    {
        bool CanSeek { get; }
        char[] ExpectedChars { get; }
        bool SkipWhitespace { get; }
    }

    #endregion
}
