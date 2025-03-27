using System;
using Xunit;
using Parlot.Tests.Calc;

namespace Parlot.Tests.Calc.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Parser"/> class.
    /// </summary>
    public class ParserTests
    {
        private readonly Parser _parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserTests"/> class.
        /// </summary>
        public ParserTests()
        {
            _parser = new Parser();
        }

        /// <summary>
        /// Tests that Parse returns a Number expression when given a valid numeric string.
        /// The test verifies a simple happy path scenario.
        /// </summary>
        /// <param name="input">The numeric string input.</param>
        /// <param name="expectedValue">The expected decimal value.</param>
        [Theory]
        [InlineData("123", 123)]
        [InlineData(" 456 ", 456)]
        public void Parse_SimpleNumber_ReturnsNumberExpression(string input, decimal expectedValue)
        {
            // Act
            Expression result = _parser.Parse(input);

            // Assert
            var number = Assert.IsType<Number>(result);
            Assert.Equal(expectedValue, number.Value);
        }

        /// <summary>
        /// Tests that Parse returns an Addition expression for a valid addition string.
        /// Verifies that the left and right operands are parsed correctly.
        /// </summary>
        [Fact]
        public void Parse_Addition_ReturnsAdditionExpression()
        {
            // Arrange
            const string input = "1+2";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            var addition = Assert.IsType<Addition>(result);
            var left = Assert.IsType<Number>(addition.Left);
            var right = Assert.IsType<Number>(addition.Right);
            Assert.Equal(1, left.Value);
            Assert.Equal(2, right.Value);
        }

        /// <summary>
        /// Tests that Parse returns a Multiplication expression for a valid multiplication string.
        /// Verifies that the left and right operands of multiplication are correct.
        /// </summary>
        [Fact]
        public void Parse_Multiplication_ReturnsMultiplicationExpression()
        {
            // Arrange
            const string input = "3*4";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            var multiplication = Assert.IsType<Multiplication>(result);
            var left = Assert.IsType<Number>(multiplication.Left);
            var right = Assert.IsType<Number>(multiplication.Right);
            Assert.Equal(3, left.Value);
            Assert.Equal(4, right.Value);
        }

        /// <summary>
        /// Tests that Parse correctly respects operator precedence.
        /// Expression "1+2*3" should be parsed as an Addition where the right operand is a Multiplication.
        /// </summary>
        [Fact]
        public void Parse_OrderOfOperations_ReturnsCorrectTree()
        {
            // Arrange
            const string input = "1+2*3";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            // Verifying that addition is the root and that multiplication is applied on the right side.
            var addition = Assert.IsType<Addition>(result);
            var left = Assert.IsType<Number>(addition.Left);
            Assert.Equal(1, left.Value);
            var multiplication = Assert.IsType<Multiplication>(addition.Right);
            var multLeft = Assert.IsType<Number>(multiplication.Left);
            var multRight = Assert.IsType<Number>(multiplication.Right);
            Assert.Equal(2, multLeft.Value);
            Assert.Equal(3, multRight.Value);
        }

        /// <summary>
        /// Tests that Parse correctly handles a unary minus operation.
        /// Expression "-5" should result in a NegateExpression wrapping a Number with value 5.
        /// </summary>
        [Fact]
        public void Parse_UnaryMinus_ReturnsNegateExpression()
        {
            // Arrange
            const string input = "-5";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            var negate = Assert.IsType<NegateExpression>(result);
            var inner = Assert.IsType<Number>(negate.Inner);
            Assert.Equal(5, inner.Value);
        }

        /// <summary>
        /// Tests that Parse correctly parses an expression with parentheses.
        /// Expression "(1+2)" should be parsed as the grouped addition expression.
        /// </summary>
        [Fact]
        public void Parse_Parentheses_ReturnsGroupedExpression()
        {
            // Arrange
            const string input = "(1+2)";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            // Parentheses do not produce a separate node; the inner expression is returned.
            var addition = Assert.IsType<Addition>(result);
            var left = Assert.IsType<Number>(addition.Left);
            var right = Assert.IsType<Number>(addition.Right);
            Assert.Equal(1, left.Value);
            Assert.Equal(2, right.Value);
        }

        /// <summary>
        /// Tests that Parse throws a ParseException when a closing parenthesis is missing.
        /// Ensures that the parser properly identifies rejected input scenarios.
        /// </summary>
        [Fact]
        public void Parse_MissingClosingParenthesis_ThrowsParseException()
        {
            // Arrange
            const string input = "(1+2";

            // Act & Assert
            var ex = Assert.Throws<ParseException>(() => _parser.Parse(input));
            Assert.Contains("Expected ')'", ex.Message);
        }

        /// <summary>
        /// Tests that Parse throws a ParseException when provided with an empty string.
        /// Verifies that no primary expression leads to the correct error message.
        /// </summary>
        [Fact]
        public void Parse_EmptyString_ThrowsParseException()
        {
            // Arrange
            const string input = "";

            // Act & Assert
            var ex = Assert.Throws<ParseException>(() => _parser.Parse(input));
            Assert.Contains("Expected primary expression", ex.Message);
        }

        /// <summary>
        /// Tests that Parse throws a ParseException when the input ends abruptly after an operator.
        /// Expression "1+" lacks a right-hand operand, which should trigger an error.
        /// </summary>
        [Fact]
        public void Parse_IncompleteExpression_ThrowsParseException()
        {
            // Arrange
            const string input = "1+";

            // Act & Assert
            var ex = Assert.Throws<ParseException>(() => _parser.Parse(input));
            Assert.Contains("Expected primary expression", ex.Message);
        }
    }
}
