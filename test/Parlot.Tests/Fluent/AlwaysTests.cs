using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Parlot.Compilation;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Always{T}"/> class.
    /// </summary>
    public class AlwaysTests
    {
        private readonly int _testIntValue;
        private readonly string _testStringValue;

        public AlwaysTests()
        {
            _testIntValue = 42;
            _testStringValue = "test";
        }

        #region Parse Tests

        /// <summary>
        /// Tests that the Parse method sets the result with the correct offsets and value and returns true.
        /// </summary>
        [Fact]
        public void Parse_WithValidContextAndResult_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var alwaysParser = new Always<int>(_testIntValue);
            var fakeCursor = new FakeCursor(100);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeParseContext = new FakeParseContext(fakeScanner);
            var fakeParseResult = new FakeParseResult<int>();

            // Act
            bool parseResult = alwaysParser.Parse(fakeParseContext, ref fakeParseResult);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(100, fakeParseResult.Start);
            Assert.Equal(100, fakeParseResult.End);
            Assert.Equal(_testIntValue, fakeParseResult.Value);
            Assert.Equal(new List<string> { "Enter", "Exit" }, fakeParseContext.Calls);
        }

        /// <summary>
        /// Tests that the Parse method throws a NullReferenceException when provided a null context.
        /// </summary>
        [Fact]
        public void Parse_WithNullContext_ThrowsNullReferenceException()
        {
            // Arrange
            var alwaysParser = new Always<int>(_testIntValue);
            FakeParseResult<int> fakeParseResult = new FakeParseResult<int>();
            ParseContext nullContext = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => alwaysParser.Parse(nullContext, ref fakeParseResult));
        }

        #endregion

        #region Compile Tests

        /// <summary>
        /// Tests that the Compile method returns a compilation result with a constant expression representing the value and a success flag set to true.
        /// </summary>
        [Fact]
        public void Compile_WithValidContext_ReturnsCompilationResultWithExpectedExpression()
        {
            // Arrange
            var alwaysParser = new Always<int>(_testIntValue);
            var fakeCompilationContext = new FakeCompilationContext();

            // Act
            CompilationResult result = alwaysParser.Compile(fakeCompilationContext);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Expression);
            // Verify that the expression is a constant expression with the expected value and type.
            if (result.Expression is ConstantExpression constantExpr)
            {
                Assert.Equal(_testIntValue, constantExpr.Value);
                Assert.Equal(typeof(int), constantExpr.Type);
            }
            else
            {
                Assert.True(false, "Expected a ConstantExpression.");
            }
        }

        /// <summary>
        /// Tests that the Compile method throws a NullReferenceException when provided a null context.
        /// </summary>
        [Fact]
        public void Compile_WithNullContext_ThrowsNullReferenceException()
        {
            // Arrange
            var alwaysParser = new Always<int>(_testIntValue);
            CompilationContext nullContext = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => alwaysParser.Compile(nullContext));
        }

        #endregion

        #region Additional Tests for String Type

        /// <summary>
        /// Tests that the Parse method works correctly for a non-int generic type (string).
        /// </summary>
        [Fact]
        public void Parse_WithStringType_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var alwaysParser = new Always<string>(_testStringValue);
            var fakeCursor = new FakeCursor(50);
            var fakeScanner = new FakeScanner(fakeCursor);
            var fakeParseContext = new FakeParseContext(fakeScanner);
            var fakeParseResult = new FakeParseResult<string>();

            // Act
            bool parseResult = alwaysParser.Parse(fakeParseContext, ref fakeParseResult);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(50, fakeParseResult.Start);
            Assert.Equal(50, fakeParseResult.End);
            Assert.Equal(_testStringValue, fakeParseResult.Value);
            Assert.Equal(new List<string> { "Enter", "Exit" }, fakeParseContext.Calls);
        }

        /// <summary>
        /// Tests that the Compile method works correctly for a non-int generic type (string).
        /// </summary>
        [Fact]
        public void Compile_WithStringType_ReturnsCompilationResultWithExpectedExpression()
        {
            // Arrange
            var alwaysParser = new Always<string>(_testStringValue);
            var fakeCompilationContext = new FakeCompilationContext();

            // Act
            CompilationResult result = alwaysParser.Compile(fakeCompilationContext);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Expression);
            if (result.Expression is ConstantExpression constantExpr)
            {
                Assert.Equal(_testStringValue, constantExpr.Value);
                Assert.Equal(typeof(string), constantExpr.Type);
            }
            else
            {
                Assert.True(false, "Expected a ConstantExpression.");
            }
        }

        #endregion
    }

    #region Fake Helper Classes

    /// <summary>
    /// A fake implementation of the ParseContext for testing purposes.
    /// </summary>
    public class FakeParseContext : ParseContext
    {
        public List<string> Calls { get; } = new List<string>();

        public FakeParseContext(FakeScanner scanner)
        {
            Scanner = scanner;
        }

        public override void EnterParser(object parser)
        {
            Calls.Add("Enter");
        }

        public override void ExitParser(object parser)
        {
            Calls.Add("Exit");
        }
    }

    /// <summary>
    /// A fake implementation of the Scanner.
    /// </summary>
    public class FakeScanner : IScanner
    {
        public FakeScanner(FakeCursor cursor)
        {
            Cursor = cursor;
        }

        public ICursor Cursor { get; }
    }

    /// <summary>
    /// A fake implementation of a cursor.
    /// </summary>
    public class FakeCursor : ICursor
    {
        public FakeCursor(int offset)
        {
            Offset = offset;
        }

        public int Offset { get; set; }
    }

    /// <summary>
    /// A fake implementation of ParseResult for testing purposes.
    /// </summary>
    /// <typeparam name="T">The type of the parse result.</typeparam>
    public class FakeParseResult<T> : ParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T Value { get; private set; }

        public override void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    /// <summary>
    /// A fake implementation of the CompilationContext.
    /// </summary>
    public class FakeCompilationContext : CompilationContext
    {
        public override CompilationResult CreateCompilationResult<T>(bool success, Expression expression)
        {
            return new FakeCompilationResult(success, expression);
        }
    }

    /// <summary>
    /// A fake implementation of the CompilationResult.
    /// </summary>
    public class FakeCompilationResult : CompilationResult
    {
        public FakeCompilationResult(bool success, Expression expression)
        {
            Success = success;
            Expression = expression;
        }

        public override bool Success { get; }
        public override Expression Expression { get; }
    }

    #endregion

    #region Fake Interfaces and Abstract Classes

    // Minimal fake implementations to satisfy dependencies in tests. In a real scenario,
    // these would be provided by the Parlot library.

    public abstract class ParseContext
    {
        public IScanner Scanner { get; protected set; }
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
    }

    public interface IScanner
    {
        ICursor Cursor { get; }
    }

    public interface ICursor
    {
        int Offset { get; }
    }

    public abstract class ParseResult<T>
    {
        public abstract void Set(int start, int end, T value);
    }

    public abstract class CompilationContext
    {
        public abstract CompilationResult CreateCompilationResult<T>(bool success, Expression expression);
    }

    public abstract class CompilationResult
    {
        public abstract bool Success { get; }
        public abstract Expression Expression { get; }
    }

    #endregion
}
