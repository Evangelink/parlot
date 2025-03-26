using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Xunit;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="When{T}"/> class.
    /// </summary>
    public class WhenTests
    {
        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when the parser argument is null.
        /// </summary>
        [Fact]
        public void Constructor_WhenParserIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new When<int>(null, (ParseContext ctx, int val) => true));
        }

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when the action argument is null.
        /// </summary>
        [Fact]
        public void Constructor_WhenActionIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var fakeParser = new FakeParser();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new When<int>(fakeParser, (Func<ParseContext, int, bool>)null));
        }

        /// <summary>
        /// Tests that the obsolete constructor throws an ArgumentNullException when the action argument is null.
        /// </summary>
        [Fact]
        public void ObsoleteConstructor_WhenActionIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var fakeParser = new FakeParser();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new When<int>(fakeParser, (Func<int, bool>)null));
        }

        /// <summary>
        /// Tests the Parse method when the inner parser succeeds and the action returns true.
        /// Expected outcome: Parse returns true, result value is set, and the scanner's cursor position is not reset.
        /// </summary>
        [Fact]
        public void Parse_WhenInnerParserSucceedsAndActionReturnsTrue_ReturnsTrueAndPreservesPosition()
        {
            // Arrange
            var context = new FakeParseContext();
            context.Scanner.Cursor.Position = 0;
            var fakeParser = new FakeParser
            {
                ParseResultToReturn = true,
                ValueToReturn = 42,
                AdvancedPosition = 10
            };
            var whenParser = new When<int>(fakeParser, (ctx, val) => true);
            var result = new ParseResult<int>();

            // Act
            bool parseOutcome = whenParser.Parse(context, ref result);

            // Assert
            Assert.True(parseOutcome);
            Assert.Equal(42, result.Value);
            Assert.Equal(fakeParser.AdvancedPosition, context.Scanner.Cursor.Position);
        }

        /// <summary>
        /// Tests the Parse method when the inner parser succeeds but the action returns false.
        /// Expected outcome: Parse returns false and the scanner's cursor position is reset to the initial position.
        /// </summary>
        [Fact]
        public void Parse_WhenInnerParserSucceedsButActionReturnsFalse_ReturnsFalseAndResetsPosition()
        {
            // Arrange
            var context = new FakeParseContext();
            context.Scanner.Cursor.Position = 5;
            var fakeParser = new FakeParser
            {
                ParseResultToReturn = true,
                ValueToReturn = 100,
                AdvancedPosition = 20
            };
            var whenParser = new When<int>(fakeParser, (ctx, val) => false);
            var result = new ParseResult<int>();

            // Act
            bool parseOutcome = whenParser.Parse(context, ref result);

            // Assert
            Assert.False(parseOutcome);
            // Expect the cursor to be reset to the original position (5)
            Assert.Equal(5, context.Scanner.Cursor.Position);
        }

        /// <summary>
        /// Tests the Parse method when the inner parser fails.
        /// Expected outcome: Parse returns false and the scanner's cursor position remains unchanged (reset to the starting position).
        /// </summary>
        [Fact]
        public void Parse_WhenInnerParserFails_ReturnsFalseAndResetsPosition()
        {
            // Arrange
            var context = new FakeParseContext();
            context.Scanner.Cursor.Position = 15;
            var fakeParser = new FakeParser
            {
                ParseResultToReturn = false,
                AdvancedPosition = 100
            };
            var whenParser = new When<int>(fakeParser, (ctx, val) => true);
            var result = new ParseResult<int>();

            // Act
            bool parseOutcome = whenParser.Parse(context, ref result);

            // Assert
            Assert.False(parseOutcome);
            Assert.Equal(15, context.Scanner.Cursor.Position);
        }

        /// <summary>
        /// Tests that the ToString method returns a string that contains the word "When".
        /// </summary>
        [Fact]
        public void ToString_ReturnsStringContainingWhen()
        {
            // Arrange
            var fakeParser = new FakeParser();
            var whenParser = new When<int>(fakeParser, (ctx, val) => true);

            // Act
            var resultString = whenParser.ToString();

            // Assert
            Assert.Contains("When", resultString);
        }

        /// <summary>
        /// Tests the Compile method to ensure it returns a non-null compilation result with expressions appended.
        /// </summary>
        [Fact]
        public void Compile_ReturnsNonNullCompilationResult()
        {
            // Arrange
            var fakeCompilationContext = new FakeCompilationContext();
            var fakeParser = new FakeParser
            {
                BuildResult = new FakeParserBuildResult
                {
                    Variables = new List<ParameterExpression>(),
                    Body = new List<Expression> { Expression.Constant(1) },
                    Success = Expression.Constant(true),
                    Value = Expression.Constant(42)
                }
            };
            var whenParser = new When<int>(fakeParser, (ctx, val) => true);

            // Act
            var compilationResult = whenParser.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
        }
    }

    #region Fake Implementations and Minimal Abstract Classes

    // Minimal abstract definitions that mimic the expected production classes.

    public abstract class Parser<T>
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
        public abstract ParserBuildResult Build(CompilationContext context, bool requireResult);
    }

    public abstract class ParserBuildResult
    {
    }

    public abstract class ParseContext
    {
        public abstract Scanner Scanner { get; }
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
    }

    public abstract class Scanner
    {
        public abstract Cursor Cursor { get; }
    }

    public abstract class Cursor
    {
        public abstract int Position { get; set; }
        public abstract void ResetPosition(int position);
    }

    public class ParseResult<T>
    {
        public bool Success { get; set; }
        public T Value { get; set; }
    }

    public abstract class CompilationContext
    {
        public abstract bool DiscardResult { get; }
        public abstract CompilationResult<T> CreateCompilationResult<T>();
        public abstract ParameterExpression DeclarePositionVariable<T>(CompilationResult<T> result);
        public abstract Expression ResetPosition(ParameterExpression positionVariable);
    }

    public abstract class CompilationResult<T>
    {
        public abstract List<Expression> Body { get; }
        public abstract ParameterExpression Success { get; }
        public abstract ParameterExpression Value { get; }
    }

    // Fake implementation of Parser for testing purposes.
    internal class FakeParser : Parser<int>
    {
        public bool ParseResultToReturn { get; set; }
        public int ValueToReturn { get; set; }
        // Simulate advanced cursor position after successful parse
        public int AdvancedPosition { get; set; } = 0;

        public override bool Parse(ParseContext context, ref ParseResult<int> result)
        {
            // Simulate advancing the cursor.
            context.Scanner.Cursor.Position = AdvancedPosition;
            if (ParseResultToReturn)
            {
                result.Value = ValueToReturn;
            }
            result.Success = ParseResultToReturn;
            return ParseResultToReturn;
        }

        public FakeParserBuildResult BuildResult { get; set; }

        public override ParserBuildResult Build(CompilationContext context, bool requireResult)
        {
            return BuildResult;
        }
    }

    // Fake implementation of ParserBuildResult for testing purposes.
    internal class FakeParserBuildResult : ParserBuildResult
    {
        public List<Expression> Body { get; set; } = new List<Expression>();
        public Expression Success { get; set; }
        public Expression Value { get; set; }
        public List<ParameterExpression> Variables { get; set; } = new List<ParameterExpression>();
    }

    // Fake ParseContext implementation.
    internal class FakeParseContext : ParseContext
    {
        public FakeParseContext()
        {
            Scanner = new FakeScanner();
        }

        public override Scanner Scanner { get; } = new FakeScanner();

        public override void EnterParser(object parser)
        {
            // No-op for testing.
        }

        public override void ExitParser(object parser)
        {
            // No-op for testing.
        }
    }

    // Fake Scanner implementation.
    internal class FakeScanner : Scanner
    {
        public FakeScanner()
        {
            Cursor = new FakeCursor();
        }

        public override Cursor Cursor { get; } = new FakeCursor();
    }

    // Fake Cursor implementation.
    internal class FakeCursor : Cursor
    {
        private int _position;

        public override int Position
        {
            get => _position;
            set => _position = value;
        }

        public override void ResetPosition(int position)
        {
            _position = position;
        }
    }

    // Fake CompilationContext implementation.
    internal class FakeCompilationContext : CompilationContext
    {
        public override bool DiscardResult { get; } = false;

        public override CompilationResult<T> CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }

        public override ParameterExpression DeclarePositionVariable<T>(CompilationResult<T> result)
        {
            return Expression.Parameter(typeof(int), "pos");
        }

        public override Expression ResetPosition(ParameterExpression positionVariable)
        {
            return Expression.Empty();
        }
    }

    // Fake CompilationResult implementation.
    internal class FakeCompilationResult<T> : CompilationResult<T>
    {
        public FakeCompilationResult()
        {
            Body = new List<Expression>();
        }

        public override List<Expression> Body { get; }

        public override ParameterExpression Success { get; } = Expression.Parameter(typeof(bool), "success");

        public override ParameterExpression Value { get; } = Expression.Parameter(typeof(T), "value");
    }

    #endregion
}
