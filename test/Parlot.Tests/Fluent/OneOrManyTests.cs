using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        /// Tests that the constructor throws an ArgumentNullException when a null parser is provided.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new OneOrMany<int>(null!));
            Assert.Equal("parser", exception.ParamName);
        }

        /// <summary>
        /// Tests that the constructor correctly assigns ISeekable properties when the inner parser implements ISeekable.
        /// </summary>
        [Fact]
        public void Constructor_WithISeekableParser_SetsISeekableProperties()
        {
            // Arrange
            var fakeSeekable = new FakeSeekableParser(shouldParse: true)
            {
                CanSeekValue = true,
                ExpectedCharsValue = new char[] { 'a', 'b' },
                SkipWhitespaceValue = true
            };

            // Act
            var oneOrMany = new OneOrMany<int>(fakeSeekable);

            // Assert
            Assert.True(oneOrMany.CanSeek);
            Assert.Equal(new char[] { 'a', 'b' }, oneOrMany.ExpectedChars);
            Assert.True(oneOrMany.SkipWhitespace);
        }

        /// <summary>
        /// Tests that Parse returns false immediately when the inner parser fails on the first call.
        /// </summary>
        [Fact]
        public void Parse_WhenInnerParserFailsInitially_ReturnsFalse()
        {
            // Arrange
            var fakeParser = new FakeParser(new int[] { });
            var oneOrMany = new OneOrMany<int>(fakeParser);
            var context = new FakeParseContext();
            var result = new FakeParseResult<IReadOnlyList<int>>();

            // Act
            bool parseResult = oneOrMany.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
        }

        /// <summary>
        /// Tests that Parse correctly aggregates the values from multiple successful parser calls.
        /// </summary>
        [Fact]
        public void Parse_WhenSuccessful_ReturnsTrueAndCollectsResults()
        {
            // Arrange
            var expectedValues = new List<int> { 10, 20, 30 };
            var fakeParser = new FakeParser(expectedValues);
            var oneOrMany = new OneOrMany<int>(fakeParser);
            var context = new FakeParseContext();
            var result = new FakeParseResult<IReadOnlyList<int>>();

            // Act
            bool parseResult = oneOrMany.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(expectedValues, result.Value);
            // Check that the start and end indices were set from the inner parser calls.
            Assert.Equal(0, result.Start);
            Assert.Equal(expectedValues.Count, result.End);
        }

        /// <summary>
        /// Tests that ToString returns the inner parser's string representation with a trailing plus sign.
        /// </summary>
        [Fact]
        public void ToString_ReturnsInnerParserToStringWithPlusSign()
        {
            // Arrange
            var fakeParser = new FakeParser(new int[] { });
            fakeParser.OverrideToString = "FakeParser";
            var oneOrMany = new OneOrMany<int>(fakeParser);

            // Act
            string toStringResult = oneOrMany.ToString();

            // Assert
            Assert.Equal("FakeParser+", toStringResult);
        }

        /// <summary>
        /// Tests that Compile returns a CompilationResult with a non-empty body expression.
        /// </summary>
        [Fact]
        public void Compile_ReturnsCompilationResultWithNonEmptyBody()
        {
            // Arrange
            var mockParser = new Mock<Parser<int>>();
            // Setup the Build method on the inner parser to return a fake compilation result.
            var fakeInnerCompilationResult = new FakeCompilationResult<int>();
            // Add a dummy expression to simulate inner parser's compilation.
            fakeInnerCompilationResult.Body.Add(Expression.Empty());
            mockParser.Setup(p => p.Build(It.IsAny<FakeCompilationContext>()))
                      .Returns((FakeCompilationContext ctx) => fakeInnerCompilationResult);

            var oneOrMany = new OneOrMany<int>(mockParser.Object);
            var context = new FakeCompilationContext();

            // Act
            var compilationResult = oneOrMany.Compile(context);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.True(compilationResult.Body.Count > 0);
        }
    }

    #region Helper Fake Classes for Parse

    /// <summary>
    /// A fake implementation of ParseContext for testing.
    /// </summary>
    internal class FakeParseContext : ParseContext
    {
        public override void EnterParser(object parser) { }
        public override void ExitParser(object parser) { }
    }

    /// <summary>
    /// A fake implementation of ParseResult for testing.
    /// </summary>
    internal class FakeParseResult<T> : ParseResult<T>
    {
        public int Start { get; set; }
        public int End { get; set; }
        public T Value { get; set; } = default!;
        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    /// <summary>
    /// A fake parser implementation for testing the Parse method.
    /// </summary>
    internal class FakeParser : Parser<int>, ISeekable
    {
        private readonly IEnumerator<int> _enumerator;
        private int _currentCall = 0;
        public string OverrideToString { get; set; } = string.Empty;

        // ISeekable implementation properties with default values.
        public bool CanSeek { get; } = false;
        public char[] ExpectedChars { get; } = new char[0];
        public bool SkipWhitespace { get; } = false;

        public FakeParser(IEnumerable<int> values)
        {
            _enumerator = values.GetEnumerator();
        }

        public override bool Parse(ParseContext context, ref ParseResult<int> result)
        {
            // Simulate setting start index as call count and end as call count + 1.
            if (_enumerator.MoveNext())
            {
                result.Set(_currentCall, _currentCall + 1, _enumerator.Current);
                _currentCall++;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(OverrideToString))
            {
                return OverrideToString;
            }
            return base.ToString();
        }
    }

    /// <summary>
    /// A fake parser that implements ISeekable for testing constructor behavior.
    /// </summary>
    internal class FakeSeekableParser : Parser<int>, ISeekable
    {
        private readonly bool _shouldParse;
        public bool CanSeekValue { get; set; }
        public char[] ExpectedCharsValue { get; set; } = new char[0];
        public bool SkipWhitespaceValue { get; set; }

        public FakeSeekableParser(bool shouldParse)
        {
            _shouldParse = shouldParse;
        }

        public override bool Parse(ParseContext context, ref ParseResult<int> result)
        {
            if (_shouldParse)
            {
                result.Set(0, 1, 42);
                return true;
            }
            return false;
        }

        public bool CanSeek => CanSeekValue;
        public char[] ExpectedChars => ExpectedCharsValue;
        public bool SkipWhitespace => SkipWhitespaceValue;
    }
    #endregion

    #region Helper Fake Classes for Compilation

    /// <summary>
    /// A fake implementation of CompilationContext for testing.
    /// </summary>
    internal class FakeCompilationContext : CompilationContext
    {
        private int _nextNumber = 1;
        public override int NextNumber => _nextNumber++;
        public override bool DiscardResult { get; set; } = false;
        public override Expression Eof() => Expression.Constant(false);

        public override CompilationResult<T> CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }
    }

    /// <summary>
    /// A fake implementation of CompilationResult for testing.
    /// </summary>
    internal class FakeCompilationResult<T> : CompilationResult<T>
    {
        public FakeCompilationResult()
        {
            Variables = new List<ParameterExpression>();
            Body = new List<Expression>();
            Success = Expression.Variable(typeof(bool), "success");
            Value = Expression.Variable(typeof(T), "value");
        }

        public override IList<ParameterExpression> Variables { get; }
        public override IList<Expression> Body { get; }
        public override ParameterExpression Success { get; set; }
        public override ParameterExpression Value { get; set; }

        public override ParameterExpression DeclareVariable<U>(string name, Expression init)
        {
            var variable = Expression.Variable(typeof(U), name);
            Variables.Add(variable);
            // In a real scenario, an assignment expression would be added.
            return variable;
        }
    }
    #endregion
}
