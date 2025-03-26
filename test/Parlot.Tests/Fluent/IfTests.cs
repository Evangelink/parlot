using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="If{C, S, T}"/> class.
    /// </summary>
    public class IfTests
    {
        private readonly int _testState = 42;

        /// <summary>
        /// Verifies that the constructor throws an ArgumentNullException when the predicate argument is null.
        /// </summary>
        [Fact]
        public void Constructor_NullPredicate_ThrowsArgumentNullException()
        {
            // Arrange
            var fakeParser = new FakeParser(shouldSucceed: true);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new If<FakeParseContext, int, string>(fakeParser, null, _testState));
        }

        /// <summary>
        /// Verifies that the constructor throws an ArgumentNullException when the parser argument is null.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange
            Func<FakeParseContext, int?, bool> predicate = (ctx, state) => true;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new If<FakeParseContext, int, string>(null, predicate, _testState));
        }

        /// <summary>
        /// Verifies that when the predicate returns false, the Parse method does not call the inner parser and returns false.
        /// </summary>
        [Fact]
        public void Parse_PredicateReturnsFalse_DoesNotCallInnerParserAndReturnsFalse()
        {
            // Arrange
            bool predicateInvoked = false;
            Func<FakeParseContext, int?, bool> predicate = (ctx, state) =>
            {
                predicateInvoked = true;
                return false;
            };
            var fakeParser = new FakeParser(shouldSucceed: true);
            var ifParser = new If<FakeParseContext, int, string>(fakeParser, predicate, _testState);
            var parseContext = new FakeParseContext();
            var result = new ParseResult<string>();

            // Act
            bool returned = ifParser.Parse(parseContext, ref result);

            // Assert
            Assert.True(predicateInvoked);
            Assert.False(fakeParser.ParseCalled);
            Assert.False(returned);
        }

        /// <summary>
        /// Verifies that when the predicate returns true and the inner parser succeeds, the Parse method returns true and sets the result.
        /// </summary>
        [Fact]
        public void Parse_PredicateReturnsTrue_InnerParserSucceeds_ReturnsTrueAndSetsResult()
        {
            // Arrange
            Func<FakeParseContext, int?, bool> predicate = (ctx, state) => true;
            var fakeParser = new FakeParser(shouldSucceed: true);
            var ifParser = new If<FakeParseContext, int, string>(fakeParser, predicate, _testState);
            var parseContext = new FakeParseContext();
            parseContext.Scanner.Cursor.Position = 100;
            var result = new ParseResult<string> { Value = "old", Success = false };

            // Act
            bool returned = ifParser.Parse(parseContext, ref result);

            // Assert
            Assert.True(fakeParser.ParseCalled);
            Assert.True(result.Success);
            Assert.Equal(fakeParser.ReturnedValue, result.Value);
            Assert.True(returned);
        }

        /// <summary>
        /// Verifies that when the predicate returns true but the inner parser fails, the Parse method resets the scanner position and returns true.
        /// </summary>
        [Fact]
        public void Parse_PredicateReturnsTrue_InnerParserFails_ResetsPositionAndReturnsTrue()
        {
            // Arrange
            Func<FakeParseContext, int?, bool> predicate = (ctx, state) => true;
            var fakeParser = new FakeParser(shouldSucceed: false);
            var ifParser = new If<FakeParseContext, int, string>(fakeParser, predicate, _testState);
            var parseContext = new FakeParseContext();
            parseContext.Scanner.Cursor.Position = 200;
            int originalPosition = parseContext.Scanner.Cursor.Position;
            var result = new ParseResult<string>();

            // Act
            bool returned = ifParser.Parse(parseContext, ref result);

            // Assert
            Assert.True(fakeParser.ParseCalled);
            // The fake scanner resets the position via ResetPosition, so check that it was called with the original position.
            Assert.Equal(originalPosition, parseContext.Scanner.Cursor.ResetPositionCalledWith);
            Assert.True(returned);
        }

        /// <summary>
        /// Verifies that the ToString method returns the inner parser's string representation appended with " (If)".
        /// </summary>
        [Fact]
        public void ToString_ReturnsParserRepresentationWithIfSuffix()
        {
            // Arrange
            Func<FakeParseContext, int?, bool> predicate = (ctx, state) => true;
            var fakeParser = new FakeParser(shouldSucceed: true) { ToStringResult = "FakeParserRepresentation" };
            var ifParser = new If<FakeParseContext, int, string>(fakeParser, predicate, _testState);

            // Act
            string result = ifParser.ToString();

            // Assert
            Assert.Equal("FakeParserRepresentation (If)", result);
        }

        /// <summary>
        /// Verifies that the Compile method returns a CompilationResult with a non-empty body.
        /// </summary>
        [Fact]
        public void Compile_ReturnsCompilationResultWithNonEmptyBody()
        {
            // Arrange
            Func<FakeParseContext, int?, bool> predicate = (ctx, state) => true;
            var fakeParser = new FakeParser(shouldSucceed: true);
            var ifParser = new If<FakeParseContext, int, string>(fakeParser, predicate, _testState);
            var fakeCompilationContext = new FakeCompilationContext();

            // Act
            var compilationResult = ifParser.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
        }
    }

    #region Fake and Helper Classes

    // Mimics the minimal behavior of ParseContext.
    public abstract class ParseContext
    {
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
        public FakeScanner Scanner { get; set; }
    }

    public class FakeParseContext : ParseContext
    {
        public FakeParseContext()
        {
            Scanner = new FakeScanner();
        }

        public override void EnterParser(object parser)
        {
            // No operation for fake.
        }

        public override void ExitParser(object parser)
        {
            // No operation for fake.
        }
    }

    public class FakeScanner
    {
        public FakeCursor Cursor { get; } = new FakeCursor();
    }

    public class FakeCursor
    {
        public int Position { get; set; }
        public int ResetPositionCalledWith { get; private set; } = -1;

        public void ResetPosition(int position)
        {
            ResetPositionCalledWith = position;
            Position = position;
        }
    }

    // Mimics the minimal behavior of ParseResult.
    public class ParseResult<T>
    {
        public bool Success { get; set; }
        public T Value { get; set; }
    }

    // Mimics an abstract Parser base class.
    public abstract class Parser<T>
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
        public abstract CompilationResult Build(CompilationContext context, bool requireResult);
    }

    // Fake implementation of the inner parser for testing.
    public class FakeParser : Parser<string>
    {
        public bool ParseCalled { get; private set; }
        public bool ShouldSucceed { get; }
        public string ReturnedValue { get; } = "fake";
        public string ToStringResult { get; set; } = "FakeParser";

        public FakeParser(bool shouldSucceed)
        {
            ShouldSucceed = shouldSucceed;
        }

        public override bool Parse(ParseContext context, ref ParseResult<string> result)
        {
            ParseCalled = true;
            if (ShouldSucceed)
            {
                result.Success = true;
                result.Value = ReturnedValue;
                return true;
            }
            else
            {
                result.Success = false;
                return false;
            }
        }

        public override CompilationResult Build(CompilationContext context, bool requireResult)
        {
            return new FakeParserCompileResult();
        }

        public override string ToString()
        {
            return ToStringResult;
        }
    }

    // Mimics the minimal behavior of CompilationResult.
    public class CompilationResult
    {
        public List<Expression> Body { get; } = new List<Expression>();
    }

    public class CompilationResult<T> : CompilationResult
    {
        public ParameterExpression Success { get; } = Expression.Variable(typeof(bool), "success");
        public ParameterExpression Value { get; } = Expression.Variable(typeof(T), "value");
    }

    // Mimics an abstract CompilationContext.
    public abstract class CompilationContext
    {
        public abstract bool DiscardResult { get; }
        public abstract Expression ParseContext { get; }
        public abstract CompilationResult<T> CreateCompilationResult<T>();
        public abstract Expression DeclarePositionVariable(CompilationResult result);
        public abstract Expression ResetPosition(Expression positionExpression);
    }

    public class FakeCompilationContext : CompilationContext
    {
        public override bool DiscardResult => false;

        public override Expression ParseContext => Expression.Constant(new FakeParseContext());

        public override CompilationResult<T> CreateCompilationResult<T>()
        {
            return new CompilationResult<T>();
        }

        public override Expression DeclarePositionVariable(CompilationResult result)
        {
            // Return a dummy expression representing a declared position.
            return Expression.Constant(0);
        }

        public override Expression ResetPosition(Expression positionExpression)
        {
            // Return an empty expression to simulate reset.
            return Expression.Empty();
        }
    }

    // Fake implementation for the compile result from the inner parser.
    public class FakeParserCompileResult : CompilationResult
    {
        public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
        public Expression Body { get; } = Expression.Empty();

        // Simulated success expression.
        public Expression Success => Expression.Constant(true);

        // Simulated value expression.
        public Expression Value => Expression.Constant("fake");
    }

    #endregion
}
