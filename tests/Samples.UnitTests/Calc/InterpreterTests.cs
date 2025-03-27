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

        public InterpreterTests()
        {
            _interpreter = new Interpreter();
        }

        /// <summary>
        /// Tests that Evaluate returns the correct result for a simple addition.
        /// Expression: "1+2" should return 3.
        /// </summary>
        [Theory]
        [InlineData("1+2", 3)]
        [InlineData("  1 + 2  ", 3)]
        public void Evaluate_SimpleAddition_ReturnsSum(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate returns the correct result for subtraction.
        /// Expression: "5-2" should return 3.
        /// </summary>
        [Theory]
        [InlineData("5-2", 3)]
        [InlineData(" 5 - 2 ", 3)]
        public void Evaluate_SimpleSubtraction_ReturnsDifference(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly handles multiplication and division.
        /// Expression: "2*3" should return 6, "6/3" should return 2.
        /// </summary>
        [Theory]
        [InlineData("2*3", 6)]
        [InlineData("6/3", 2)]
        public void Evaluate_MultiplicationAndDivision_ReturnsCorrectResult(string expression, decimal expected)
        {
            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate respects operator precedence.
        /// Expression: "2+3*4" should return 14 (multiplication before addition).
        /// </summary>
        [Fact]
        public void Evaluate_OperatorPrecedence_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "2+3*4";
            decimal expected = 14;

            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly processes parentheses overriding operator precedence.
        /// Expression: "(2+3)*4" should return 20.
        /// </summary>
        [Fact]
        public void Evaluate_ParenthesesOverridesPrecedence_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "(2+3)*4";
            decimal expected = 20;

            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly handles unary negation.
        /// Expression: "-5" should return -5.
        /// </summary>
        [Fact]
        public void Evaluate_UnaryNegation_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "-5";
            decimal expected = -5;

            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly processes multiple unary negations.
        /// Expression: "--2" should return 2.
        /// </summary>
        [Fact]
        public void Evaluate_MultipleUnaryNegation_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "--2";
            decimal expected = 2;

            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate correctly parses and computes expressions with decimal numbers.
        /// Expression: "1.5+2.3" should return 3.8.
        /// </summary>
        [Fact]
        public void Evaluate_DecimalNumbers_ReturnsCorrectResult()
        {
            // Arrange
            string expression = "1.5+2.3";
            decimal expected = 3.8m;

            // Act
            decimal result = _interpreter.Evaluate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate throws a ParseException when the input expression is empty.
        /// Expected behavior: Throws ParseException with message indicating expected primary expression.
        /// </summary>
        [Fact]
        public void Evaluate_EmptyInput_ThrowsParseException()
        {
            // Arrange
            string expression = "";

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => _interpreter.Evaluate(expression));
            Assert.Contains("Expected primary expression", exception.Message);
        }

        /// <summary>
        /// Tests that Evaluate throws a ParseException when the input expression is invalid.
        /// Expression: "abc" is invalid.
        /// </summary>
        [Fact]
        public void Evaluate_InvalidInput_ThrowsParseException()
        {
            // Arrange
            string expression = "abc";

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => _interpreter.Evaluate(expression));
            Assert.Contains("Expected primary expression", exception.Message);
        }

        /// <summary>
        /// Tests that Evaluate throws a ParseException when there is a missing closing parenthesis.
        /// Expression: "(2+3" should trigger an exception.
        /// </summary>
        [Fact]
        public void Evaluate_MissingClosingParenthesis_ThrowsParseException()
        {
            // Arrange
            string expression = "(2+3";

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => _interpreter.Evaluate(expression));
            Assert.Contains("Expected ')'", exception.Message);
        }

        /// <summary>
        /// Tests that Evaluate throws a DivideByZeroException when division by zero occurs.
        /// Expression: "5/0" should trigger a runtime exception.
        /// </summary>
        [Fact]
        public void Evaluate_DivisionByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            string expression = "5/0";

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => _interpreter.Evaluate(expression));
        }
    }
}
