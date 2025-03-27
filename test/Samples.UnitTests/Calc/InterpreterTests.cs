using System;
using Parlot.Tests.Calc;
using Xunit;

namespace Parlot.Tests.Calc.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Interpreter"/> class.
    /// </summary>
    public class InterpreterTests
    {
        private readonly Interpreter _interpreter;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterpreterTests"/> class.
        /// </summary>
        public InterpreterTests()
        {
            _interpreter = new Interpreter();
        }

        /// <summary>
        /// Tests that Evaluate returns the correct numeric value when parsing a simple number.
        /// </summary>
        [Theory]
        [InlineData("42", 42)]
        [InlineData("   123   ", 123)]
        public void Evaluate_ValidNumber_ReturnsNumber(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly computes addition operations.
        /// </summary>
        [Theory]
        [InlineData("1+2", 3)]
        [InlineData(" 10 + 20 ", 30)]
        public void Evaluate_Addition_ReturnsSum(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly computes subtraction operations.
        /// </summary>
        [Theory]
        [InlineData("5-3", 2)]
        [InlineData(" 20 - 5 ", 15)]
        public void Evaluate_Subtraction_ReturnsDifference(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly computes multiplication operations.
        /// </summary>
        [Theory]
        [InlineData("2*3", 6)]
        [InlineData(" 4 * 5 ", 20)]
        public void Evaluate_Multiplication_ReturnsProduct(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly computes division operations.
        /// </summary>
        [Theory]
        [InlineData("6/2", 3)]
        [InlineData(" 20 / 4 ", 5)]
        public void Evaluate_Division_ReturnsQuotient(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly handles operator precedence in a combined expression.
        /// </summary>
        [Fact]
        public void Evaluate_CombinedExpression_ReturnsCorrectValue()
        {
            // Arrange
            string expression = "1+2*3"; // 1 + (2*3) = 7

            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(7, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly computes expressions with parentheses overriding precedence.
        /// </summary>
        [Fact]
        public void Evaluate_WithParentheses_ReturnsCorrectValue()
        {
            // Arrange
            string expression = "(1+2)*3"; // (1+2)*3 = 9

            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(9, result);
        }

        /// <summary>
        /// Tests that Evaluate handles unary minus correctly.
        /// </summary>
        [Theory]
        [InlineData("-5", -5)]
        [InlineData("1 + -2", -1)]
        public void Evaluate_UnaryMinus_ReturnsCorrectValue(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly handles nested unary minus operators.
        /// </summary>
        [Fact]
        public void Evaluate_NestedUnaryMinus_ReturnsCorrectValue()
        {
            // Arrange
            string expression = "--5"; // -(-5) = 5

            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(5, result);
        }

        /// <summary>
        /// Tests that Evaluate throws a ParseException when a closing parenthesis is missing.
        /// </summary>
        [Fact]
        public void Evaluate_MissingClosingParenthesis_ThrowsParseException()
        {
            // Arrange
            string expression = "(1+2";

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => _interpreter.Evaluate(expression));
            Assert.Contains("Expected ')'", exception.Message);
        }

        /// <summary>
        /// Tests that Evaluate throws a ParseException when an invalid primary expression is encountered.
        /// </summary>
        [Fact]
        public void Evaluate_InvalidPrimary_ThrowsParseException()
        {
            // Arrange
            string expression = "abc";

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => _interpreter.Evaluate(expression));
            Assert.Contains("Expected primary expression", exception.Message);
        }

        /// <summary>
        /// Tests that Evaluate throws a DivideByZeroException when division by zero occurs.
        /// </summary>
        [Fact]
        public void Evaluate_DivisionByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            string expression = "1/0";

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => _interpreter.Evaluate(expression));
        }
    }
}
