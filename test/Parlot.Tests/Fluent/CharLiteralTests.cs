using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="CharLiteral"/> class.
    /// </summary>
    public class CharLiteralTests
    {
        private readonly char _testChar;
        private readonly CharLiteral _charLiteral;

        /// <summary>
        /// Initializes test instance with a specific character.
        /// </summary>
        public CharLiteralTests()
        {
            _testChar = 'a';
            _charLiteral = new CharLiteral(_testChar);
        }

        /// <summary>
        /// Tests that the constructor properly initializes properties.
        /// </summary>
        [Fact]
        public void Constructor_Always_InitializesPropertiesCorrectly()
        {
            // Arrange & Act done in constructor

            // Assert
            Assert.Equal(_testChar, _charLiteral.Char);
            Assert.True(_charLiteral.CanSeek);
            Assert.NotNull(_charLiteral.ExpectedChars);
            Assert.Single(_charLiteral.ExpectedChars);
            Assert.Equal(_testChar, _charLiteral.ExpectedChars[0]);
        }

        /// <summary>
        /// Tests that ToString method returns the expected string representation.
        /// </summary>
        [Fact]
        public void ToString_Always_ReturnsExpectedFormat()
        {
            // Arrange
            var expectedString = $"Char('{_testChar}')";

            // Act
            var actualString = _charLiteral.ToString();

            // Assert
            Assert.Equal(expectedString, actualString);
        }

        /// <summary>
        /// Tests the Parse method when the input starts with the expected character.
        /// Expected outcome is that parsing succeeds and result is updated correctly.
        /// </summary>
        [Fact]
        public void Parse_InputStartsWithExpectedChar_ReturnsTrueAndUpdatesResult()
        {
            // Arrange
            string input = _testChar + "rest";
            var context = new FakeParseContext(input);
            var result = new FakeParseResult<char>();

            // Act
            bool parseSuccess = _charLiteral.Parse(context, ref result);

            // Assert
            Assert.True(parseSuccess);
            Assert.Equal(0, result.Start);
            Assert.Equal(1, result.End);
            Assert.Equal(_testChar, result.Value);
            Assert.True(context.EnteredParserCalled);
            Assert.True(context.ExitedParserCalled);
        }

        /// <summary>
        /// Tests the Parse method when the input does not start with the expected character.
        /// Expected outcome is that parsing fails and result remains unmodified.
        /// </summary>
        [Fact]
        public void Parse_InputDoesNotStartWithExpectedChar_ReturnsFalse()
        {
            // Arrange
            string input = "brest";
            var context = new FakeParseContext(input);
            var result = new FakeParseResult<char>();

            // Act
            bool parseSuccess = _charLiteral.Parse(context, ref result);

            // Assert
            Assert.False(parseSuccess);
            // Since result.Set is never called, Start and End should be default (0)
            Assert.Equal(0, result.Start);
            Assert.Equal(0, result.End);
            Assert.Equal(default(char), result.Value);
            Assert.True(context.EnteredParserCalled);
            Assert.True(context.ExitedParserCalled);
        }

        /// <summary>
        /// Tests the Compile method when DiscardResult is false.
        /// Expected outcome is that the compilation result contains an assignment to the result.Value.
        /// </summary>
        [Fact]
        public void Compile_DiscardResultFalse_BuildsExpressionWithValueAssignment()
        {
            // Arrange
            var context = new FakeCompilationContext { DiscardResult = false };

            // Act
            var compilationResult = _charLiteral.Compile(context);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotNull(compilationResult.Body);
            Assert.Single(compilationResult.Body);

            // Examine the IfThenExpression added in the compilation result body.
            var ifThenExpr = compilationResult.Body[0] as ConditionalExpression;
            Assert.NotNull(ifThenExpr);

            // The test expression is built within an IfThen, so verify the Then branch is a BlockExpression.
            var thenBlock = ifThenExpr.IfTrue as BlockExpression;
            Assert.NotNull(thenBlock);
            // With DiscardResult false, the block should have two expressions: one assignment to success and one assignment to value.
            Assert.Equal(2, thenBlock.Expressions.Count);
            // Check that second expression is an assignment expression.
            var assignValueExpr = thenBlock.Expressions[1] as BinaryExpression;
            Assert.NotNull(assignValueExpr);
            Assert.Equal(ExpressionType.Assign, assignValueExpr.NodeType);
        }

        /// <summary>
        /// Tests the Compile method when DiscardResult is true.
        /// Expected outcome is that the compilation result contains an Expression.Empty instead of assigning a value.
        /// </summary>
        [Fact]
        public void Compile_DiscardResultTrue_BuildsExpressionWithoutValueAssignment()
        {
            // Arrange
            var context = new FakeCompilationContext { DiscardResult = true };

            // Act
            var compilationResult = _charLiteral.Compile(context);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotNull(compilationResult.Body);
            Assert.Single(compilationResult.Body);

            // Examine the IfThenExpression added in the compilation result body.
            var ifThenExpr = compilationResult.Body[0] as ConditionalExpression;
            Assert.NotNull(ifThenExpr);

            // The Then branch should be a BlockExpression.
            var thenBlock = ifThenExpr.IfTrue as BlockExpression;
            Assert.NotNull(thenBlock);
            // With DiscardResult true, the block should have two expressions: assignment to success and an Expression.Empty.
            Assert.Equal(2, thenBlock.Expressions.Count);
            // Second expression should be a default no-op (Empty)
            var emptyExpr = thenBlock.Expressions[1];
            Assert.Equal(ExpressionType.Default, emptyExpr.NodeType);
        }
    }

    #region FakeParse Helpers
    /// <summary>
    /// A fake implementation of ParseContext for testing purposes.
    /// </summary>
    internal class FakeParseContext
    {
        public FakeScanner Scanner { get; }
        public bool EnteredParserCalled { get; private set; }
        public bool ExitedParserCalled { get; private set; }

        public FakeParseContext(string input)
        {
            Scanner = new FakeScanner(input);
        }

        public void EnterParser(object parser)
        {
            EnteredParserCalled = true;
        }

        public void ExitParser(object parser)
        {
            ExitedParserCalled = true;
        }
    }

    /// <summary>
    /// A fake scanner that provides a cursor over the input string.
    /// </summary>
    internal class FakeScanner
    {
        public FakeCursor Cursor { get; }

        public FakeScanner(string input)
        {
            Cursor = new FakeCursor(input);
        }
    }

    /// <summary>
    /// A fake cursor that simulates scanning characters in a string.
    /// </summary>
    internal class FakeCursor
    {
        private readonly string _input;
        public int Offset { get; private set; }

        public FakeCursor(string input)
        {
            _input = input;
            Offset = 0;
        }

        public bool Match(char c)
        {
            return Offset < _input.Length && _input[Offset] == c;
        }

        public void Advance()
        {
            if (Offset < _input.Length)
            {
                Offset++;
            }
        }
    }

    /// <summary>
    /// A fake parse result to capture parsing outcomes.
    /// </summary>
    internal class FakeParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T Value { get; private set; }

        public void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }
    #endregion

    #region FakeCompilation Helpers
    /// <summary>
    /// A fake compilation result to capture compilation expressions.
    /// </summary>
    internal class FakeCompilationResult<T>
    {
        public List<Expression> Body { get; } = new List<Expression>();
        public ParameterExpression Success { get; } = Expression.Parameter(typeof(bool), "success");
        public ParameterExpression Value { get; } = Expression.Parameter(typeof(T), "value");
    }

    /// <summary>
    /// A fake compilation context providing necessary methods for compilation.
    /// </summary>
    internal class FakeCompilationContext
    {
        public bool DiscardResult { get; set; }

        public FakeCompilationResult<T> CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }

        public Expression ReadChar(char c)
        {
            // Simulate reading a char by always returning true.
            return Expression.Constant(true);
        }
    }
    #endregion
}
