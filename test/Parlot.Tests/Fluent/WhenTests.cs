using Parlot.Fluent;
using Parlot.Compilation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="When{T}"/> class.
    /// </summary>
    public class WhenTests
    {
        // Dummy type for testing
        private const int DummyValue = 42;

        #region Constructor Tests

        /// <summary>
        /// Tests that the obsolete constructor throws ArgumentNullException when provided with a null parser.
        /// </summary>
        [Fact]
        public void ObsoleteConstructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange
            Func<int, bool> dummyAction = _ => true;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new When<int>(null, dummyAction));
        }

        /// <summary>
        /// Tests that the obsolete constructor throws ArgumentNullException when provided with a null action.
        /// </summary>
        [Fact]
        public void ObsoleteConstructor_NullAction_ThrowsArgumentNullException()
        {
            // Arrange
            var fakeParser = new FakeParser<int>((ctx, result) => true);
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new When<int>(fakeParser, (Func<int, bool>)null));
        }

        /// <summary>
        /// Tests that the primary constructor throws ArgumentNullException when provided with a null parser.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange
            Func<ParseContext, int, bool> dummyAction = (_, i) => true;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new When<int>(null, dummyAction));
        }

        /// <summary>
        /// Tests that the primary constructor throws ArgumentNullException when provided with a null action.
        /// </summary>
        [Fact]
        public void Constructor_NullAction_ThrowsArgumentNullException()
        {
            // Arrange
            var fakeParser = new FakeParser<int>((ctx, result) => true);
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new When<int>(fakeParser, (Func<ParseContext, int, bool>)null));
        }
        #endregion

        #region Parse Method Tests

        /// <summary>
        /// Tests that Parse returns true and does not reset the cursor when the underlying parser succeeds and action returns true.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserSucceedsAndActionReturnsTrue_ReturnsTrue()
        {
            // Arrange
            var fakeParser = new FakeParser<int>((context, result) =>
            {
                result.Value = DummyValue;
                result.Success = true;
                return true;
            });

            // Action delegate always returns true.
            Func<ParseContext, int, bool> action = (ctx, val) => true;

            var whenParser = new When<int>(fakeParser, action);

            var fakeContext = new FakeParseContext();
            var result = new ParseResult<int>();

            // set initial cursor position
            fakeContext.Scanner.Cursor.Position = 100;

            // Act
            bool parseResult = whenParser.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.True(result.Success);
            Assert.Equal(DummyValue, result.Value);
            // Cursor should not have been reset; ResetCalled remains false.
            Assert.False(fakeContext.Scanner.Cursor.ResetCalled);
        }

        /// <summary>
        /// Tests that Parse returns false and resets the cursor when the underlying parser succeeds but action returns false.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserSucceedsButActionReturnsFalse_ReturnsFalseAndResetsCursor()
        {
            // Arrange
            var fakeParser = new FakeParser<int>((context, result) =>
            {
                result.Value = DummyValue;
                result.Success = true;
                return true;
            });

            // Action delegate returns false.
            Func<ParseContext, int, bool> action = (ctx, val) => false;

            var whenParser = new When<int>(fakeParser, action);

            var fakeContext = new FakeParseContext();
            var result = new ParseResult<int>();

            // set initial cursor position
            fakeContext.Scanner.Cursor.Position = 200;
            int initialPosition = fakeContext.Scanner.Cursor.Position;

            // Act
            bool parseResult = whenParser.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            // The cursor should have been reset to its initial position.
            Assert.True(fakeContext.Scanner.Cursor.ResetCalled);
            Assert.Equal(initialPosition, fakeContext.Scanner.Cursor.LastResetPosition);
        }

        /// <summary>
        /// Tests that Parse returns false and resets the cursor when the underlying parser fails.
        /// </summary>
        [Fact]
        public void Parse_UnderlyingParserFails_ReturnsFalseAndResetsCursor()
        {
            // Arrange
            var fakeParser = new FakeParser<int>((context, result) =>
            {
                result.Success = false;
                return false;
            });

            // Action delegate (should not be invoked in this case)
            bool actionInvoked = false;
            Func<ParseContext, int, bool> action = (ctx, val) =>
            {
                actionInvoked = true;
                return true;
            };

            var whenParser = new When<int>(fakeParser, action);

            var fakeContext = new FakeParseContext();
            var result = new ParseResult<int>();

            // set initial cursor position
            fakeContext.Scanner.Cursor.Position = 300;
            int initialPosition = fakeContext.Scanner.Cursor.Position;

            // Act
            bool parseResult = whenParser.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.False(actionInvoked);
            Assert.True(fakeContext.Scanner.Cursor.ResetCalled);
            Assert.Equal(initialPosition, fakeContext.Scanner.Cursor.LastResetPosition);
        }
        #endregion

        #region Compile Method Tests

        /// <summary>
        /// Tests that Compile returns a non-null CompilationResult and includes a body with at least one expression.
        /// </summary>
        [Fact]
        public void Compile_ValidParserAndAction_ReturnsCompilationResultWithBody()
        {
            // Arrange
            var dummyParseValue = DummyValue;
            var fakeParser = new FakeParser<int>((context, result) => true);
            fakeParser.FakeBuildResult = new FakeParserCompileResult<int>
            {
                Variables = new List<ParameterExpression>(),
                Body = new List<Expression>
                {
                    Expression.Empty()
                },
                Success = Expression.Constant(true),
                Value = Expression.Constant(dummyParseValue)
            };

            // Action always returns true.
            Func<ParseContext, int, bool> action = (ctx, val) => true;

            var whenParser = new When<int>(fakeParser, action);
            var fakeContext = new FakeCompilationContext();

            // Act
            var compilationResult = whenParser.Compile(fakeContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotNull(compilationResult.Body);
            Assert.NotEmpty(compilationResult.Body);
        }
        #endregion

        #region ToString Method Tests

        /// <summary>
        /// Tests that ToString returns a string representation that includes the substring "(When)".
        /// </summary>
        [Fact]
        public void ToString_ReturnsStringContainingWhen()
        {
            // Arrange
            var fakeParser = new FakeParser<int>((ctx, res) => true);
            Func<ParseContext, int, bool> action = (ctx, val) => true;
            var whenParser = new When<int>(fakeParser, action);

            // Act
            string result = whenParser.ToString();

            // Assert
            Assert.Contains("(When)", result);
        }

        #endregion
    }

    #region Fake Classes for Parse Tests

    /// <summary>
    /// A fake implementation of Parser<T> for testing purposes.
    /// </summary>
    /// <typeparam name="T">The type parameter.</typeparam>
    internal class FakeParser<T> : Parser<T>
    {
        private readonly Func<ParseContext, ParseResult<T>, bool> _parseFunc;

        /// <summary>
        /// Gets or sets the fake build result to be returned by Build.
        /// </summary>
        public FakeParserCompileResult<T> FakeBuildResult { get; set; }

        public FakeParser(Func<ParseContext, ParseResult<T>, bool> parseFunc)
        {
            _parseFunc = parseFunc;
        }

        public override bool Parse(ParseContext context, ref ParseResult<T> result)
        {
            return _parseFunc(context, result);
        }

        public override CompilationResult Compile(CompilationContext context)
        {
            throw new NotImplementedException();
        }

        public override ParserBuildResult<T> Build(CompilationContext context, bool requireResult)
        {
            return FakeBuildResult;
        }
    }

    /// <summary>
    /// A fake implementation of ParseContext for testing.
    /// </summary>
    internal class FakeParseContext : ParseContext
    {
        public FakeParseContext()
        {
            Scanner = new FakeScanner();
        }

        public override void EnterParser(Parser parser) { }

        public override void ExitParser(Parser parser) { }
    }

    /// <summary>
    /// A fake implementation of a scanner.
    /// </summary>
    internal class FakeScanner
    {
        public FakeCursor Cursor { get; } = new FakeCursor();
    }

    /// <summary>
    /// A fake implementation of a cursor.
    /// </summary>
    internal class FakeCursor
    {
        public int Position { get; set; }
        public bool ResetCalled { get; private set; }
        public int LastResetPosition { get; private set; }

        public void ResetPosition(int position)
        {
            ResetCalled = true;
            LastResetPosition = position;
            Position = position;
        }
    }

    /// <summary>
    /// A simple implementation of ParseResult for testing.
    /// </summary>
    internal class ParseResult<T>
    {
        public T Value { get; set; }
        public bool Success { get; set; }
    }
    #endregion

    #region Fake Classes for Compilation Tests

    /// <summary>
    /// A fake implementation of CompilationContext for testing purposes.
    /// </summary>
    internal class FakeCompilationContext : CompilationContext
    {
        private int _positionCounter = 0;
        public override ParseContext ParseContext => new FakeParseContext();

        public override bool DiscardResult => false;

        public override ParameterExpression DeclarePositionVariable(CompilationResult result)
        {
            // Create a dummy parameter representing a position variable.
            return Expression.Parameter(typeof(int), "start" + _positionCounter++);
        }

        public override Expression ResetPosition(ParameterExpression positionVar)
        {
            // Return an empty expression for resetting position.
            return Expression.Empty();
        }

        public override CompilationResult<T> CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
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
        }

        public override IList<ParameterExpression> Variables { get; } = new List<ParameterExpression>();

        public override IList<Expression> Body { get; }

        public override ParameterExpression Success { get; set; }

        public override ParameterExpression Value { get; set; }
    }

    /// <summary>
    /// A fake implementation of ParserBuildResult for testing purposes.
    /// </summary>
    internal class FakeParserCompileResult<T> : ParserBuildResult<T>
    {
        public override IList<ParameterExpression> Variables { get; set; } = new List<ParameterExpression>();

        public override IList<Expression> Body { get; set; } = new List<Expression>();

        public override Expression Success { get; set; }

        public override Expression Value { get; set; }
    }
    #endregion
}
