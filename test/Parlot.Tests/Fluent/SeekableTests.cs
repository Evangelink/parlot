using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Parlot.Compilation;
using Parlot.Rewriting;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Seekable{T}"/> class.
    /// </summary>
    public class SeekableTests
    {
        private readonly string[] _expectedCharsInput = new[] { "a", "b", "a" };

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when the parser parameter is null.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange
            Parser<int> nullParser = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Seekable<int>(nullParser, skipWhiteSpace: false, expectedChars: ReadOnlySpan<char>.Empty));
            Assert.Equal("parser", exception.ParamName);
        }

        /// <summary>
        /// Tests that the constructor sets the properties correctly and filters distinct expected characters.
        /// </summary>
        [Fact]
        public void Constructor_ValidInputs_SetsPropertiesAndFiltersExpectedChars()
        {
            // Arrange
            // Create a fake parser that does nothing for Parse/Build.
            var fakeParser = new FakeParser<int>();

            // Provide expected characters with duplicates.
            ReadOnlySpan<char> expected1 = "abc";
            ReadOnlySpan<char> expected2 = "bcd"; // 'b', 'c' duplicated.
            var combinedExpected = new List<ReadOnlySpan<char>> { expected1, expected2 };

            // Act
            var seekable = new Seekable<int>(fakeParser, skipWhiteSpace: true, combinedExpected.ToArray());

            // Assert
            Assert.True(seekable.CanSeek);
            Assert.True(seekable.SkipWhitespace);
            // Expected distinct characters across spans: a, b, c, d.
            var expectedDistinct = new[] { 'a', 'b', 'c', 'd' };
            Assert.Equal(expectedDistinct.OrderBy(c => c), seekable.ExpectedChars.OrderBy(c => c));
            Assert.Equal(fakeParser, seekable.Parser);
        }

        /// <summary>
        /// Tests the Parse method when the underlying parser returns true.
        /// Verifies that the context's EnterParser and ExitParser methods are invoked.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserReturnsTrue_ReturnsTrue()
        {
            // Arrange
            // Setup a fake parser that returns true.
            var fakeParser = new FakeParser<int>
            {
                ParseFunc = (context, ref ParseResult<int> result) =>
                {
                    result.SetResult(42);
                    return true;
                }
            };

            var seekable = new Seekable<int>(fakeParser, skipWhiteSpace: false, ReadOnlySpan<char>.Empty);

            // Create a mock for ParseContext which records the calls to EnterParser and ExitParser.
            var mockParseContext = new Mock<ParseContext> { CallBase = true };
            int enterCount = 0, exitCount = 0;
            mockParseContext.Setup(c => c.EnterParser(It.IsAny<object>())).Callback<object>(p => enterCount++);
            mockParseContext.Setup(c => c.ExitParser(It.IsAny<object>())).Callback<object>(p => exitCount++);

            var parseContext = mockParseContext.Object;
            var resultValue = new ParseResult<int>();

            // Act
            bool success = seekable.Parse(parseContext, ref resultValue);

            // Assert
            Assert.True(success);
            Assert.Equal(42, resultValue.Value);
            Assert.Equal(1, enterCount);
            Assert.Equal(1, exitCount);
        }

        /// <summary>
        /// Tests the Parse method when the underlying parser returns false.
        /// Verifies that the context's EnterParser and ExitParser methods are still invoked.
        /// </summary>
        [Fact]
        public void Parse_WhenUnderlyingParserReturnsFalse_ReturnsFalse()
        {
            // Arrange
            // Setup a fake parser that returns false.
            var fakeParser = new FakeParser<int>
            {
                ParseFunc = (context, ref ParseResult<int> result) =>
                {
                    return false;
                }
            };

            var seekable = new Seekable<int>(fakeParser, skipWhiteSpace: false, ReadOnlySpan<char>.Empty);

            // Create a mock for ParseContext.
            var mockParseContext = new Mock<ParseContext> { CallBase = true };
            int enterCount = 0, exitCount = 0;
            mockParseContext.Setup(c => c.EnterParser(It.IsAny<object>())).Callback<object>(p => enterCount++);
            mockParseContext.Setup(c => c.ExitParser(It.IsAny<object>())).Callback<object>(p => exitCount++);

            var parseContext = mockParseContext.Object;
            var resultValue = new ParseResult<int>();

            // Act
            bool success = seekable.Parse(parseContext, ref resultValue);

            // Assert
            Assert.False(success);
            Assert.Equal(1, enterCount);
            Assert.Equal(1, exitCount);
        }

        /// <summary>
        /// Tests the Compile method to validate that it integrates the underlying parser's build result correctly.
        /// </summary>
        [Fact]
        public void Compile_WhenCalled_AddsParserCompilationBodyToResult()
        {
            // Arrange
            // Setup a fake expression for testing.
            var expectedExpression = Expression.Constant(100);
            var fakeBuildResult = new FakeBuildResult<int>
            {
                Variables = new List<ParameterExpression> { Expression.Parameter(typeof(int), "x") },
                Body = expectedExpression
            };

            var fakeParser = new FakeParser<int>
            {
                BuildFunc = (context, requireResult) => fakeBuildResult
            };

            var seekable = new Seekable<int>(fakeParser, skipWhiteSpace: false, ReadOnlySpan<char>.Empty);

            // Create a fake compilation context using Moq.
            var fakeCompilationResult = new DummyCompilationResult<int>();
            var mockCompilationContext = new Mock<CompilationContext>();
            mockCompilationContext.Setup(c => c.CreateCompilationResult<int>(true))
                                  .Returns(fakeCompilationResult);

            var compilationContext = mockCompilationContext.Object;

            // Act
            var compilationResult = seekable.Compile(compilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            // The block should have been added to the Body collection.
            Assert.Single(compilationResult.Body);
            // Verify that the added block contains the expected expression.
            var blockExpr = compilationResult.Body.First() as BlockExpression;
            Assert.NotNull(blockExpr);
            // Since the block was created from parserCompileResult.Body, check that it contains the expected constant.
            bool containsExpected = blockExpr.Expressions.Any(expr =>
                expr is ConstantExpression constExpr && constExpr.Value.Equals(100));
            Assert.True(containsExpected);
        }

        /// <summary>
        /// Tests that the ToString method returns the underlying parser's string representation appended with " (Seekable)".
        /// </summary>
        [Fact]
        public void ToString_ReturnsUnderlyingParserRepresentationWithSeekableSuffix()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                ToStringFunc = () => "FakeParser"
            };

            var seekable = new Seekable<int>(fakeParser, skipWhiteSpace: false, ReadOnlySpan<char>.Empty);

            // Act
            string stringValue = seekable.ToString();

            // Assert
            Assert.Equal("FakeParser (Seekable)", stringValue);
        }
    }

    /// <summary>
    /// A fake implementation of the Parser{T} abstract class to simulate parser behavior for testing.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the parser.</typeparam>
    internal class FakeParser<T> : Parser<T>
    {
        /// <summary>
        /// Delegate to simulate the Parse method.
        /// </summary>
        public Func<ParseContext, ParseResult<T>, bool> ParseFunc { get; set; } = (context, ref ParseResult<T> result) => false;

        /// <summary>
        /// Delegate to simulate the Build method.
        /// </summary>
        public Func<CompilationContext, bool, FakeBuildResult<T>> BuildFunc { get; set; } = (context, requireResult) => new FakeBuildResult<T>
        {
            Variables = new List<ParameterExpression>(),
            Body = Expression.Empty()
        };

        /// <summary>
        /// Delegate to simulate ToString method.
        /// </summary>
        public Func<string> ToStringFunc { get; set; } = () => "FakeParser";

        /// <inheritdoc />
        public override bool Parse(ParseContext context, ref ParseResult<T> result)
        {
            return ParseFunc(context, ref result);
        }

        /// <summary>
        /// Simulates the Build method.
        /// </summary>
        public override BuildResult Build(CompilationContext context, bool requireResult)
        {
            // Wrap the fake build result into a BuildResult expected by the compilation process.
            var fakeResult = BuildFunc(context, requireResult);
            return new BuildResult(fakeResult.Variables.ToList(), fakeResult.Body);
        }

        /// <inheritdoc />
        public override string ToString() => ToStringFunc();
    }

    /// <summary>
    /// A fake build result to simulate parser build outputs.
    /// </summary>
    /// <typeparam name="T">The type parameter of the parser result.</typeparam>
    internal class FakeBuildResult<T>
    {
        /// <summary>
        /// Gets or sets the variables for the build expression.
        /// </summary>
        public IEnumerable<ParameterExpression> Variables { get; set; }

        /// <summary>
        /// Gets or sets the body expression of the build.
        /// </summary>
        public Expression Body { get; set; }
    }

    /// <summary>
    /// A dummy implementation of the CompilationResult{T} class to support testing of the Compile method.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the parser.</typeparam>
    internal class DummyCompilationResult<T> : CompilationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyCompilationResult{T}"/> class.
        /// </summary>
        public DummyCompilationResult()
        {
            Body = new List<Expression>();
        }

        /// <summary>
        /// Gets the body expressions.
        /// </summary>
        public List<Expression> Body { get; }

        /// <summary>
        /// Implicit conversion to List of Expression.
        /// </summary>
        /// <returns>The list of expressions.</returns>
        public override IList<Expression> Expressions => Body;
    }

    /// <summary>
    /// A dummy abstract base class for CompilationResult to allow instantiation in tests.
    /// </summary>
    public abstract class CompilationResult
    {
        /// <summary>
        /// Gets the collection of expressions representing the compilation body.
        /// </summary>
        public abstract IList<Expression> Expressions { get; }
    }

    /// <summary>
    /// A dummy implementation of ParseResult{T} to simulate parsing results.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    public class ParseResult<T>
    {
        /// <summary>
        /// Gets or sets the parsed value.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Sets the parsed result value.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetResult(T value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// A dummy abstract base class for Parser{T} to support the fake parser implementation.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by the parser.</typeparam>
    public abstract class Parser<T>
    {
        /// <summary>
        /// When overridden in a derived class, parses the input using the specified context and updates the result.
        /// </summary>
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);

        /// <summary>
        /// When overridden in a derived class, builds an expression tree representing the parser.
        /// </summary>
        public abstract BuildResult Build(CompilationContext context, bool requireResult);
    }

    /// <summary>
    /// A dummy class to represent the result from building a parser.
    /// </summary>
    public class BuildResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildResult"/> class.
        /// </summary>
        /// <param name="variables">The list of variables used in the build.</param>
        /// <param name="body">The expression representing the build body.</param>
        public BuildResult(IList<ParameterExpression> variables, Expression body)
        {
            Variables = variables;
            Body = body;
        }

        /// <summary>
        /// Gets the variables used in the build.
        /// </summary>
        public IList<ParameterExpression> Variables { get; }

        /// <summary>
        /// Gets the expression that represents the body of the build.
        /// </summary>
        public Expression Body { get; }
    }

    /// <summary>
    /// A dummy class to represent the CompilationContext for building parsers.
    /// </summary>
    public class CompilationContext
    {
        /// <summary>
        /// Creates a new compilation result.
        /// </summary>
        /// <typeparam name="T">The type of the parser result.</typeparam>
        /// <param name="hasResult">A flag indicating if a result is required.</param>
        /// <returns>A new instance of <see cref="CompilationResult"/>.</returns>
        public virtual CompilationResult CreateCompilationResult<T>(bool hasResult) => new DummyCompilationResult<T>();
    }

    /// <summary>
    /// A dummy class to represent the ParseContext in which parsing occurs.
    /// </summary>
    public class ParseContext
    {
        /// <summary>
        /// Simulates entering a parser by recording the call.
        /// </summary>
        /// <param name="parser">The parser being entered.</param>
        public virtual void EnterParser(object parser)
        {
        }

        /// <summary>
        /// Simulates exiting a parser by recording the call.
        /// </summary>
        /// <param name="parser">The parser being exited.</param>
        public virtual void ExitParser(object parser)
        {
        }
    }
}
