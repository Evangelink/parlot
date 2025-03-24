using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Deferred{T}"/> class.
    /// </summary>
    public class DeferredTests
    {
        /// <summary>
        /// Tests that setting the Parser property to null throws an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Parser_SetToNull_ThrowsArgumentNullException()
        {
            // Arrange
            var deferred = new Deferred<int>();
            
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => deferred.Parser = null);
            Assert.Equal("value", exception.ParamName);
        }

        /// <summary>
        /// Tests that calling Parse when the Parser property is null throws an InvalidOperationException.
        /// </summary>
        [Fact]
        public void Parse_WhenParserNotInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var deferred = new Deferred<int>();
            var context = new FakeParseContext();
            var result = new ParseResult<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => deferred.Parse(context, ref result));
            Assert.Equal("Parser has not been initialized", exception.Message);
        }

        /// <summary>
        /// Tests that Parse delegates parsing to the underlying parser and returns its outcome.
        /// </summary>
        [Fact]
        public void Parse_WhenParserInitialized_ReturnsUnderlyingParserResult()
        {
            // Arrange
            var expectedSuccess = true;
            var expectedValue = 123;
            var fakeParser = new FakeParser(expectedSuccess, expectedValue);
            var deferred = new Deferred<int>((d) => fakeParser);
            var context = new FakeParseContext();
            var result = new ParseResult<int>();

            // Act
            bool outcome = deferred.Parse(context, ref result);

            // Assert
            Assert.Equal(expectedSuccess, outcome);
            Assert.Equal(expectedSuccess, result.Success);
            Assert.Equal(expectedValue, result.Value);
        }

        /// <summary>
        /// Tests that calling Compile when the Parser property is null throws an InvalidOperationException.
        /// </summary>
        [Fact]
        public void Compile_WhenParserNotInitialized_ThrowsInvalidOperationException()
        {
            // Arrange
            var deferred = new Deferred<int>();
            var compilationContext = new FakeCompilationContext();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => deferred.Compile(compilationContext));
            Assert.Equal("Can't compile a Deferred Parser until it is fully initialized", exception.Message);
        }

        /// <summary>
        /// Tests that Compile correctly builds a compilation result when the underlying parser is initialized.
        /// </summary>
        [Fact]
        public void Compile_WhenParserInitialized_ReturnsCompilationResult()
        {
            // Arrange
            var expectedSuccess = true;
            var expectedValue = 456;
            var fakeParser = new FakeParser(expectedSuccess, expectedValue);
            var deferred = new Deferred<int>((d) => fakeParser);
            var compilationContext = new FakeCompilationContext();

            // Act
            var compilationResult = deferred.Compile(compilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            // Check that the result contains at least one variable and appropriate body expressions were added.
            Assert.NotEmpty(compilationResult.Variables);
            Assert.NotEmpty(compilationResult.Body);
        }

        /// <summary>
        /// Tests that the ToString method returns the expected format when not in recursive call.
        /// </summary>
        [Fact]
        public void ToString_WhenCalled_ReturnsExpectedFormat()
        {
            // Arrange
            var fakeParser = new FakeParser(true, 0)
            {
                CustomToString = "FakeParser"
            };
            var deferred = new Deferred<int>((d) => fakeParser);

            // Act
            string result = deferred.ToString();

            // Assert
            Assert.Equal("FakeParser (Deferred)", result);
        }

        /// <summary>
        /// Tests that the ToString method correctly handles recursion by returning "(Deferred)" if already in call.
        /// </summary>
        [Fact]
        public void ToString_WhenRecursing_ReturnsDeferredOnly()
        {
            // Arrange
            var fakeParser = new FakeParser(true, 0)
            {
                CustomToString = "FakeParser"
            };
            var deferred = new Deferred<int>((d) => fakeParser);

            // Use reflection to set the _toString flag to true to simulate recursion.
            var toStringField = typeof(Deferred<int>).GetField("_toString", BindingFlags.NonPublic | BindingFlags.Instance);
            toStringField.SetValue(deferred, true);

            // Act
            string result = deferred.ToString();

            // Assert
            Assert.Equal("(Deferred)", result);
        }

        /// <summary>
        /// Tests that the Deferred constructor with delegate initializes the properties from an ISeekable underlying parser.
        /// </summary>
        [Fact]
        public void DeferredConstructor_WithSeekableParser_InitializesSeekableProperties()
        {
            // Arrange
            var fakeSeekableParser = new FakeSeekableParser(true, 789)
            {
                CustomToString = "SeekableFakeParser",
                CanSeek = true,
                ExpectedChars = new char[] { 'a', 'b', 'c' },
                SkipWhitespace = true
            };

            // Act
            var deferred = new Deferred<int>((d) => fakeSeekableParser);

            // Assert
            Assert.NotNull(deferred.Parser);
            Assert.Equal(fakeSeekableParser.CanSeek, deferred.CanSeek);
            Assert.Equal(fakeSeekableParser.ExpectedChars, deferred.ExpectedChars);
            Assert.Equal(fakeSeekableParser.SkipWhitespace, deferred.SkipWhitespace);
        }
    }

    #region Fake and Stub Classes

    /// <summary>
    /// A fake implementation of Parser&lt;int&gt; for testing purposes.
    /// </summary>
    internal class FakeParser : Parser<int>
    {
        private readonly bool _parseSuccess;
        private readonly int _value;

        /// <summary>
        /// Gets or sets a custom string to be returned by ToString.
        /// </summary>
        public string CustomToString { get; set; }

        public FakeParser(bool parseSuccess, int value)
        {
            _parseSuccess = parseSuccess;
            _value = value;
        }

        public override bool Parse(ParseContext context, ref ParseResult<int> result)
        {
            result.Success = _parseSuccess;
            result.Value = _value;
            return _parseSuccess;
        }

        public override FakeParserCompileResult<int> Build(CompilationContext context)
        {
            return new FakeParserCompileResult<int>
            {
                Variables = new List<ParameterExpression>(),
                Body = new List<Expression>(),
                Success = Expression.Constant(_parseSuccess),
                Value = Expression.Constant(_value)
            };
        }

        public override string ToString()
        {
            return CustomToString ?? base.ToString();
        }
    }

    /// <summary>
    /// A fake implementation of a parser that is also seekable.
    /// </summary>
    internal class FakeSeekableParser : FakeParser, ISeekable
    {
        public bool CanSeek { get; set; }
        public char[] ExpectedChars { get; set; }
        public bool SkipWhitespace { get; set; }

        public FakeSeekableParser(bool parseSuccess, int value)
            : base(parseSuccess, value)
        {
        }
    }

    /// <summary>
    /// A fake implementation of CompilationContext for testing Deferred.Compile.
    /// </summary>
    internal class FakeCompilationContext : CompilationContext
    {
        private int _nextNumber = 1;
        public override ParameterExpression ParseContext { get; } = Expression.Parameter(typeof(object), "parseContext");
        public override bool DiscardResult { get; set; } = false;
        public override int NextNumber => _nextNumber++;

#if DEBUG
        public override List<LambdaExpression> Lambdas { get; } = new List<LambdaExpression>();
#else
        public override List<LambdaExpression> Lambdas { get; } = null;
#endif

        public override CompilationResult<T> CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }
    }

    /// <summary>
    /// A fake implementation of CompilationResult for testing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FakeCompilationResult<T> : CompilationResult<T>
    {
        public FakeCompilationResult()
        {
            Variables = new List<ParameterExpression>();
            Body = new List<Expression>();
            Success = Expression.Parameter(typeof(bool), "success");
            Value = Expression.Parameter(typeof(T), "value");
        }
    }

    /// <summary>
    /// A fake implementation of the parser compile result used by the Build method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FakeParserCompileResult<T> : ParserCompileResult<T>
    {
        public override IEnumerable<ParameterExpression> Variables { get; set; }
        public override List<Expression> Body { get; set; }
        public override Expression Success { get; set; }
        public override Expression Value { get; set; }
    }

    /// <summary>
    /// A fake implementation of ParseContext for testing Deferred.Parse.
    /// </summary>
    internal class FakeParseContext : ParseContext
    {
        // For testing purposes, we simulate EnterParser and ExitParser.
        public bool EnterCalled { get; private set; }
        public bool ExitCalled { get; private set; }

        public override void EnterParser(object parser)
        {
            EnterCalled = true;
        }

        public override void ExitParser(object parser)
        {
            ExitCalled = true;
        }
    }

    #endregion

    #region Stub Base Classes

    // The following stub classes simulate minimal definitions to support testing.
    // In production these are provided by the Parlot library.

    /// <summary>
    /// Represents the result of a parse operation.
    /// </summary>
    public struct ParseResult<T>
    {
        public bool Success;
        public T Value;
    }

    /// <summary>
    /// Abstract base class for parsers.
    /// </summary>
    public abstract class Parser<T>
    {
        public string Name { get; set; }

        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);

        public abstract ParserCompileResult<T> Build(CompilationContext context);
    }

    /// <summary>
    /// Base class for parser compile results.
    /// </summary>
    public abstract class ParserCompileResult<T>
    {
        public abstract IEnumerable<ParameterExpression> Variables { get; set; }
        public abstract List<Expression> Body { get; set; }
        public abstract Expression Success { get; set; }
        public abstract Expression Value { get; set; }
    }

    /// <summary>
    /// Represents a compilation context.
    /// </summary>
    public abstract class CompilationContext
    {
        public abstract ParameterExpression ParseContext { get; }
        public abstract bool DiscardResult { get; }
        public abstract int NextNumber { get; }
#if DEBUG
        public abstract List<LambdaExpression> Lambdas { get; }
#endif

        public abstract CompilationResult<T> CreateCompilationResult<T>();
    }

    /// <summary>
    /// Represents a compilation result.
    /// </summary>
    public abstract class CompilationResult<T>
    {
        public List<ParameterExpression> Variables { get; set; }
        public List<Expression> Body { get; set; }
        public ParameterExpression Success { get; set; }
        public ParameterExpression Value { get; set; }
    }

    /// <summary>
    /// Represents the parsing context.
    /// </summary>
    public abstract class ParseContext
    {
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
    }

    #endregion
}
