using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Rewriting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Then{T, U}"/> class.
    /// </summary>
    public class ThenTests
    {
        // -------------------------------
        // Helper Fake Classes for Testing
        // -------------------------------

        /// <summary>
        /// A fake parser for testing Parse method behavior.
        /// </summary>
        /// <typeparam name="T">The type of the parser result.</typeparam>
        private class FakeParser<T> : Parser<T>
        {
            public bool ShouldSucceed { get; set; }
            public T FakeValue { get; set; }
            public int StartPos { get; set; } = 0;
            public int EndPos { get; set; } = 1;

            public override bool Parse(ParseContext context, ref ParseResult<T> result)
            {
                if (ShouldSucceed)
                {
                    result.Set(StartPos, EndPos, FakeValue);
                    return true;
                }
                return false;
            }

            // Minimal implementation for Build method required by Then.Compile.
            public override CompilationResult Build(CompilationContext context, bool requireResult)
            {
                // Create a dummy compilation result.
                var dummyResult = new DummyCompilationResult<T>
                {
                    // Simulate that the underlying parser was successful.
                    Success = Expression.Constant(true),
                    // Simulate a value expression.
                    Value = Expression.Constant(FakeValue, typeof(T))
                };
                // Add a dummy expression to body.
                dummyResult.Body.Add(Expression.Constant("FakeParserBuilt"));
                return dummyResult;
            }
        }

        /// <summary>
        /// A dummy implementation of CompilationResult to be used in compile tests.
        /// </summary>
        /// <typeparam name="T">The type of the compilation result value.</typeparam>
        private class DummyCompilationResult<T> : CompilationResult
        {
            public DummyCompilationResult()
            {
                Body = new List<Expression>();
                Variables = new List<ParameterExpression>();
            }

            public override IList<Expression> Body { get; }

            public override IList<ParameterExpression> Variables { get; }

            public Expression Value { get; set; }

            public Expression Success { get; set; }
        }

        /// <summary>
        /// A fake compilation context for testing Compile method behavior.
        /// </summary>
        private class FakeCompilationContext : CompilationContext
        {
            public FakeCompilationContext(bool discardResult = false)
            {
                DiscardResult = discardResult;
                // For simplicity, use a constant expression as the parse context.
                ParseContext = Expression.Constant("FakeParseContext");
            }

            public override bool DiscardResult { get; }

            public override Expression ParseContext { get; }

            public override CompilationResult CreateCompilationResult<T>(bool success, Expression defaultValue)
            {
                return new DummyCompilationResult<T>
                {
                    // Initialize with default values; the Then.Compile method will add a block to Body.
                    Value = defaultValue,
                    Success = Expression.Constant(false)
                };
            }
        }

        // -------------------------------
        // Constructor Tests
        // -------------------------------

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when a null parser is provided.
        /// </summary>
        [Fact]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Then<int, string>(null!, x => x.ToString()));
            Assert.Throws<ArgumentNullException>(() => new Then<int, string>(null!, (ctx, x) => x.ToString()));
            Assert.Throws<ArgumentNullException>(() => new Then<int, string>(null!, "staticValue"));
        }

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when a null action (Func&lt;T, U&gt;) is provided.
        /// </summary>
        [Fact]
        public void Constructor_NullAction1_ThrowsArgumentNullException()
        {
            // Arrange
            var fakeParser = new FakeParser<int> { ShouldSucceed = true, FakeValue = 10 };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Then<int, string>(fakeParser, (Func<int, string>)null!));
        }

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when a null action (Func&lt;ParseContext, T, U&gt;) is provided.
        /// </summary>
        [Fact]
        public void Constructor_NullAction2_ThrowsArgumentNullException()
        {
            // Arrange
            var fakeParser = new FakeParser<int> { ShouldSucceed = true, FakeValue = 10 };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Then<int, string>(fakeParser, (Func<ParseContext, int, string>)null!));
        }

        // -------------------------------
        // Parse Method Tests
        // -------------------------------

        /// <summary>
        /// Tests the Parse method with a successful underlying parser execution using action1.
        /// Expected: The action is invoked and result is correctly set.
        /// </summary>
        [Fact]
        public void Parse_WithUnderlyingParserSuccess_UsingAction1_ReturnsTrueAndAppliesAction()
        {
            // Arrange
            var expectedValue = "20 processed";
            var fakeParser = new FakeParser<int>
            {
                ShouldSucceed = true,
                FakeValue = 20,
                StartPos = 5,
                EndPos = 10
            };

            var thenParser = new Then<int, string>(fakeParser, x => x.ToString() + " processed");
            var context = new ParseContext();
            var result = new ParseResult<string>();

            // Act
            bool parseResult = thenParser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(5, result.Start);
            Assert.Equal(10, result.End);
            Assert.Equal(expectedValue, result.Value);
        }

        /// <summary>
        /// Tests the Parse method with a successful underlying parser execution using action2.
        /// Expected: The action is invoked with context and result, and result is correctly set.
        /// </summary>
        [Fact]
        public void Parse_WithUnderlyingParserSuccess_UsingAction2_ReturnsTrueAndAppliesAction()
        {
            // Arrange
            var expectedValue = "30 obtained";
            var fakeParser = new FakeParser<int>
            {
                ShouldSucceed = true,
                FakeValue = 30,
                StartPos = 2,
                EndPos = 8
            };

            var thenParser = new Then<int, string>(fakeParser, (ctx, x) => x.ToString() + " obtained");
            var context = new ParseContext();
            var result = new ParseResult<string>();

            // Act
            bool parseResult = thenParser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(2, result.Start);
            Assert.Equal(8, result.End);
            Assert.Equal(expectedValue, result.Value);
        }

        /// <summary>
        /// Tests the Parse method with a successful underlying parser execution using a predefined value.
        /// Expected: The fixed value is set even if no action is provided.
        /// </summary>
        [Fact]
        public void Parse_WithUnderlyingParserSuccess_UsingValueConstructor_ReturnsTrueAndSetsFixedValue()
        {
            // Arrange
            var fixedValue = "fixed";
            var fakeParser = new FakeParser<int>
            {
                ShouldSucceed = true,
                FakeValue = 100,
                StartPos = 0,
                EndPos = 4
            };

            var thenParser = new Then<int, string>(fakeParser, fixedValue);
            var context = new ParseContext();
            var result = new ParseResult<string>();

            // Act
            bool parseResult = thenParser.Parse(context, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(0, result.Start);
            Assert.Equal(4, result.End);
            Assert.Equal(fixedValue, result.Value);
        }

        /// <summary>
        /// Tests the Parse method when the underlying parser fails.
        /// Expected: The Parse method returns false and does not set the result.
        /// </summary>
        [Fact]
        public void Parse_WithUnderlyingParserFailure_ReturnsFalse()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                ShouldSucceed = false,
                FakeValue = 999
            };

            var thenParser = new Then<int, string>(fakeParser, x => x.ToString());
            var context = new ParseContext();
            var result = new ParseResult<string>();

            // Act
            bool parseResult = thenParser.Parse(context, ref result);

            // Assert
            Assert.False(parseResult);
        }

        // -------------------------------
        // ToString Method Test
        // -------------------------------

        /// <summary>
        /// Tests that the ToString method returns a string that contains "(Then)".
        /// </summary>
        [Fact]
        public void ToString_ReturnsExpectedFormat_ContainsThenText()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                ShouldSucceed = true,
                FakeValue = 42
            };

            var thenParser = new Then<int, string>(fakeParser, x => x.ToString());
            
            // Act
            string text = thenParser.ToString();

            // Assert
            Assert.Contains("(Then)", text);
        }

        // -------------------------------
        // Compile Method Tests
        // -------------------------------

        /// <summary>
        /// Tests the Compile method with a then parser constructed with action1.
        /// Expected: The returned CompilationResult contains a non-empty body.
        /// </summary>
        [Fact]
        public void Compile_WithAction1_ReturnsCompilationResultWithBody()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                ShouldSucceed = true,
                FakeValue = 50
            };

            var thenParser = new Then<int, string>(fakeParser, x => x.ToString() + " compiled");
            var compilationContext = new FakeCompilationContext();

            // Act
            CompilationResult result = thenParser.Compile(compilationContext);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Body);
        }

        /// <summary>
        /// Tests the Compile method with a then parser constructed with action2.
        /// Expected: The returned CompilationResult contains a non-empty body.
        /// </summary>
        [Fact]
        public void Compile_WithAction2_ReturnsCompilationResultWithBody()
        {
            // Arrange
            var fakeParser = new FakeParser<int>
            {
                ShouldSucceed = true,
                FakeValue = 60
            };

            var thenParser = new Then<int, string>(fakeParser, (ctx, x) => x.ToString() + " compiled2");
            var compilationContext = new FakeCompilationContext();

            // Act
            CompilationResult result = thenParser.Compile(compilationContext);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Body);
        }

        /// <summary>
        /// Tests the Compile method with a then parser constructed with a fixed value.
        /// Expected: The returned CompilationResult contains a non-empty body.
        /// </summary>
        [Fact]
        public void Compile_WithValueConstructor_ReturnsCompilationResultWithBody()
        {
            // Arrange
            var fixedValue = "fixedCompiled";
            var fakeParser = new FakeParser<int>
            {
                ShouldSucceed = true,
                FakeValue = 70
            };

            var thenParser = new Then<int, string>(fakeParser, fixedValue);
            var compilationContext = new FakeCompilationContext();

            // Act
            CompilationResult result = thenParser.Compile(compilationContext);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Body);
        }
    }

    // -------------------------------
    // Minimal Stub Classes to Support Tests
    // (These stubs simulate minimal behavior required by Then methods)
    // -------------------------------

    /// <summary>
    /// Minimal stub implementation of ParseContext.
    /// </summary>
    public class ParseContext
    {
        private readonly Stack<object> _parserStack = new Stack<object>();

        public void EnterParser(object parser)
        {
            _parserStack.Push(parser);
        }

        public void ExitParser(object parser)
        {
            if (_parserStack.Count > 0)
            {
                _parserStack.Pop();
            }
        }
    }

    /// <summary>
    /// Minimal stub implementation of ParseResult.
    /// </summary>
    /// <typeparam name="T">The type of the parsed value.</typeparam>
    public class ParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T Value { get; private set; } = default!;

        public void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    /// <summary>
    /// Minimal abstract implementation of Parser.
    /// </summary>
    /// <typeparam name="T">The type of the parser result.</typeparam>
    public abstract class Parser<T>
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);

        public abstract CompilationResult Build(CompilationContext context, bool requireResult);
    }

    /// <summary>
    /// Minimal abstract implementation of CompilationContext.
    /// </summary>
    public abstract class CompilationContext
    {
        public abstract bool DiscardResult { get; }
        public abstract Expression ParseContext { get; }
        public abstract CompilationResult CreateCompilationResult<T>(bool success, Expression defaultValue);
    }

    /// <summary>
    /// Minimal abstract implementation of CompilationResult.
    /// </summary>
    public abstract class CompilationResult
    {
        public abstract IList<Expression> Body { get; }
        public abstract IList<ParameterExpression> Variables { get; }
    }
}
