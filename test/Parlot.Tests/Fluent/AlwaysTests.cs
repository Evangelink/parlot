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
        // Dummy implementations to support the tests without relying on full parser framework.

        /// <summary>
        /// Dummy implementation of a cursor.
        /// </summary>
        private class DummyCursor
        {
            public int Offset { get; set; }
        }

        /// <summary>
        /// Dummy implementation of a scanner.
        /// </summary>
        private class DummyScanner
        {
            public DummyCursor Cursor { get; } = new DummyCursor();
        }

        /// <summary>
        /// Dummy implementation of ParseContext.
        /// </summary>
        private class DummyParseContext : ParseContext
        {
            public DummyScanner Scanner { get; } = new DummyScanner();
            public int EnterCallCount { get; private set; }
            public int ExitCallCount { get; private set; }

            public override void EnterParser(object parser)
            {
                EnterCallCount++;
            }

            public override void ExitParser(object parser)
            {
                ExitCallCount++;
            }
        }

        /// <summary>
        /// Dummy implementation of ParseResult that captures the set values.
        /// </summary>
        /// <typeparam name="T">The type of the parsed value.</typeparam>
        private class DummyParseResult<T> : ParseResult<T>
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
        /// Dummy implementation of CompilationResult.
        /// </summary>
        private class DummyCompilationResult : CompilationResult
        {
            public bool IsValid { get; }
            public Expression Expression { get; }
            
            public DummyCompilationResult(bool isValid, Expression expression)
            {
                IsValid = isValid;
                Expression = expression;
            }
        }

        /// <summary>
        /// Dummy implementation of CompilationContext.
        /// </summary>
        private class DummyCompilationContext : CompilationContext
        {
            public bool CreateCalled { get; private set; }
            public bool ExpectedValidity { get; private set; }
            public Expression ProvidedExpression { get; private set; }
            
            public override CompilationResult CreateCompilationResult<T>(bool valid, Expression expression)
            {
                CreateCalled = true;
                ExpectedValidity = valid;
                ProvidedExpression = expression;
                return new DummyCompilationResult(valid, expression);
            }
        }

        /// <summary>
        /// Tests that the constructor of Always sets the Name property to "Always".
        /// </summary>
        [Fact]
        public void Constructor_ShouldSetNameToAlways()
        {
            // Arrange & Act
            var always = new Always<int>(42);

            // Assert
            Assert.Equal("Always", always.Name);
        }

        /// <summary>
        /// Tests the Parse method under happy path conditions.
        /// It verifies that the method returns true, sets the result with the correct value and cursor offsets,
        /// and calls EnterParser and ExitParser exactly once.
        /// </summary>
        [Fact]
        public void Parse_WithValidContext_SetsResultAndReturnsTrue()
        {
            // Arrange
            int expectedValue = 100;
            var always = new Always<int>(expectedValue);
            var dummyContext = new DummyParseContext();
            // Set a specific offset
            dummyContext.Scanner.Cursor.Offset = 10;
            var dummyResult = new DummyParseResult<int>();

            // Act
            bool parseResult = always.Parse(dummyContext, ref dummyResult);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(10, dummyResult.Start);
            Assert.Equal(10, dummyResult.End);
            Assert.Equal(expectedValue, dummyResult.Value);
            Assert.Equal(1, dummyContext.EnterCallCount);
            Assert.Equal(1, dummyContext.ExitCallCount);
        }

        /// <summary>
        /// Tests the Parse method when the parse context is null.
        /// Expects a NullReferenceException.
        /// </summary>
        [Fact]
        public void Parse_WithNullContext_ThrowsNullReferenceException()
        {
            // Arrange
            var always = new Always<int>(50);
            DummyParseContext nullContext = null;
            var dummyResult = new DummyParseResult<int>();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => always.Parse(nullContext, ref dummyResult));
        }

        /// <summary>
        /// Tests the Parse method when the result parameter is an uninitialized (null) instance.
        /// Since the method attempts to call Set on the result, it should throw a NullReferenceException.
        /// </summary>
        [Fact]
        public void Parse_WithNullResult_ThrowsNullReferenceException()
        {
            // Arrange
            var always = new Always<int>(75);
            var dummyContext = new DummyParseContext();
            DummyParseResult<int> nullResult = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => always.Parse(dummyContext, ref nullResult));
        }

        /// <summary>
        /// Tests the Compile method under happy path conditions.
        /// It verifies that the CompilationContext's CreateCompilationResult method is called,
        /// and that the returned CompilationResult contains a constant expression with the expected value.
        /// </summary>
        [Fact]
        public void Compile_WithValidContext_ReturnsCompilationResultWithExpectedConstant()
        {
            // Arrange
            string expectedValue = "test";
            var always = new Always<string>(expectedValue);
            var dummyCompilationContext = new DummyCompilationContext();

            // Act
            var compilationResult = always.Compile(dummyCompilationContext);

            // Assert
            Assert.True(dummyCompilationContext.CreateCalled);
            Assert.NotNull(dummyCompilationContext.ProvidedExpression);
            // Verify that the expression is a ConstantExpression with the expected value and type.
            var constantExpr = dummyCompilationContext.ProvidedExpression as ConstantExpression;
            Assert.NotNull(constantExpr);
            Assert.Equal(expectedValue, constantExpr.Value);
            Assert.Equal(typeof(string), constantExpr.Type);
            // Also check that the returned result carries the same expression.
            var dummyResult = compilationResult as DummyCompilationResult;
            Assert.NotNull(dummyResult);
            Assert.Equal(dummyCompilationContext.ProvidedExpression, dummyResult.Expression);
            Assert.True(dummyResult.IsValid);
        }

        /// <summary>
        /// Tests the Compile method when the CompilationContext is null.
        /// Expects a NullReferenceException.
        /// </summary>
        [Fact]
        public void Compile_WithNullContext_ThrowsNullReferenceException()
        {
            // Arrange
            var always = new Always<double>(3.14);
            DummyCompilationContext nullContext = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => always.Compile(nullContext));
        }
    }
}
