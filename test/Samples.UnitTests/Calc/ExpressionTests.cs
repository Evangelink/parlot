using Moq;
using Parlot.Tests.Calc;
using System;
using Xunit;

namespace Parlot.Tests.Calc.UnitTests
{
    /// <summary>
    /// A stub expression used to simulate a constant expression in tests.
    /// </summary>
    internal class StubExpression : Expression
    {
        private readonly decimal _value;

        public StubExpression(decimal value)
        {
            _value = value;
        }

        public override decimal Evaluate() => _value;
    }

    /// <summary>
    /// Unit tests for the Number class.
    /// </summary>
    public class NumberTests
    {
        private readonly Number _number;

        public NumberTests()
        {
            _number = new Number(5m);
        }

        /// <summary>
        /// Tests that Evaluate returns the initialized number value.
        /// </summary>
        [Fact]
        public void Evaluate_WithValidNumber_ReturnsNumberValue()
        {
            // Arrange
            decimal expected = 5m;
            var number = new Number(expected);

            // Act
            decimal result = number.Evaluate();

            // Assert
            Assert.Equal(expected, result);
        }
    }

    /// <summary>
    /// Unit tests for the NegateExpression class.
    /// </summary>
    public class NegateExpressionTests
    {
        /// <summary>
        /// Tests that Evaluate returns the negated value of the inner expression.
        /// </summary>
        [Fact]
        public void Evaluate_WithPositiveInner_ReturnsNegativeValue()
        {
            // Arrange
            decimal innerValue = 10m;
            var innerExpression = new Number(innerValue);
            var negateExpression = new NegateExpression(innerExpression);

            // Act
            decimal result = negateExpression.Evaluate();

            // Assert
            Assert.Equal(-innerValue, result);
        }

        /// <summary>
        /// Tests that Evaluate returns zero when the inner expression evaluates to zero.
        /// </summary>
        [Fact]
        public void Evaluate_WithZeroInner_ReturnsZero()
        {
            // Arrange
            decimal innerValue = 0m;
            var innerExpression = new Number(innerValue);
            var negateExpression = new NegateExpression(innerExpression);

            // Act
            decimal result = negateExpression.Evaluate();

            // Assert
            Assert.Equal(0m, result);
        }
    }

    /// <summary>
    /// Unit tests for the Addition class.
    /// </summary>
    public class AdditionTests
    {
        /// <summary>
        /// Tests that Evaluate returns the sum of the left and right expressions.
        /// </summary>
        [Theory]
        [InlineData(5, 10, 15)]
        [InlineData(-5, 5, 0)]
        [InlineData(0, 0, 0)]
        public void Evaluate_WithValidOperands_ReturnsCorrectSum(decimal leftValue, decimal rightValue, decimal expected)
        {
            // Arrange
            var leftExpression = new Number(leftValue);
            var rightExpression = new Number(rightValue);
            var addition = new Addition(leftExpression, rightExpression);

            // Act
            decimal result = addition.Evaluate();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that the Left and Right properties are properly set via the constructor.
        /// </summary>
        [Fact]
        public void Constructor_WithOperands_SetsLeftAndRightProperties()
        {
            // Arrange
            var left = new Number(3m);
            var right = new Number(4m);
            var addition = new Addition(left, right);

            // Act & Assert
            Assert.Equal(left, addition.Left);
            Assert.Equal(right, addition.Right);
        }
    }

    /// <summary>
    /// Unit tests for the Subtraction class.
    /// </summary>
    public class SubtractionTests
    {
        /// <summary>
        /// Tests that Evaluate returns the difference of the left and right expressions.
        /// </summary>
        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(5, 10, -5)]
        [InlineData(0, 0, 0)]
        public void Evaluate_WithValidOperands_ReturnsCorrectDifference(decimal leftValue, decimal rightValue, decimal expected)
        {
            // Arrange
            var leftExpression = new Number(leftValue);
            var rightExpression = new Number(rightValue);
            var subtraction = new Subtraction(leftExpression, rightExpression);

            // Act
            decimal result = subtraction.Evaluate();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that the Left and Right properties are properly set via the constructor.
        /// </summary>
        [Fact]
        public void Constructor_WithOperands_SetsLeftAndRightProperties()
        {
            // Arrange
            var left = new Number(8m);
            var right = new Number(3m);
            var subtraction = new Subtraction(left, right);

            // Act & Assert
            Assert.Equal(left, subtraction.Left);
            Assert.Equal(right, subtraction.Right);
        }
    }

    /// <summary>
    /// Unit tests for the Multiplication class.
    /// </summary>
    public class MultiplicationTests
    {
        /// <summary>
        /// Tests that Evaluate returns the product of the left and right expressions.
        /// </summary>
        [Theory]
        [InlineData(2, 3, 6)]
        [InlineData(-2, 3, -6)]
        [InlineData(0, 5, 0)]
        public void Evaluate_WithValidOperands_ReturnsCorrectProduct(decimal leftValue, decimal rightValue, decimal expected)
        {
            // Arrange
            var leftExpression = new Number(leftValue);
            var rightExpression = new Number(rightValue);
            var multiplication = new Multiplication(leftExpression, rightExpression);

            // Act
            decimal result = multiplication.Evaluate();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that the Left and Right properties are properly set via the constructor.
        /// </summary>
        [Fact]
        public void Constructor_WithOperands_SetsLeftAndRightProperties()
        {
            // Arrange
            var left = new Number(7m);
            var right = new Number(8m);
            var multiplication = new Multiplication(left, right);

            // Act & Assert
            Assert.Equal(left, multiplication.Left);
            Assert.Equal(right, multiplication.Right);
        }
    }

    /// <summary>
    /// Unit tests for the Division class.
    /// </summary>
    public class DivisionTests
    {
        /// <summary>
        /// Tests that Evaluate returns the quotient of the left and right expressions.
        /// </summary>
        [Theory]
        [InlineData(10, 2, 5)]
        [InlineData(-10, 2, -5)]
        [InlineData(0, 5, 0)]
        public void Evaluate_WithValidOperands_ReturnsCorrectQuotient(decimal leftValue, decimal rightValue, decimal expected)
        {
            // Arrange
            var leftExpression = new Number(leftValue);
            var rightExpression = new Number(rightValue);
            var division = new Division(leftExpression, rightExpression);

            // Act
            decimal result = division.Evaluate();

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that Evaluate throws a DivideByZeroException when the right expression evaluates to zero.
        /// </summary>
        [Fact]
        public void Evaluate_WithZeroDivisor_ThrowsDivideByZeroException()
        {
            // Arrange
            var leftExpression = new Number(10m);
            var rightExpression = new Number(0m);
            var division = new Division(leftExpression, rightExpression);

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => division.Evaluate());
        }

        /// <summary>
        /// Tests that the Left and Right properties are properly set via the constructor.
        /// </summary>
        [Fact]
        public void Constructor_WithOperands_SetsLeftAndRightProperties()
        {
            // Arrange
            var left = new Number(9m);
            var right = new Number(3m);
            var division = new Division(left, right);

            // Act & Assert
            Assert.Equal(left, division.Left);
            Assert.Equal(right, division.Right);
        }
    }

    /// <summary>
    /// Unit tests for the Exponent class.
    /// </summary>
    public class ExponentTests
    {
        /// <summary>
        /// Tests that Evaluate returns the result of raising the left expression to the power of the right expression.
        /// </summary>
        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(5, 0, 1)]
        [InlineData(2, -1, 0.5)]
        public void Evaluate_WithValidOperands_ReturnsCorrectExponentiation(decimal baseValue, decimal exponentValue, double expected)
        {
            // Arrange
            var leftExpression = new Number(baseValue);
            var rightExpression = new Number(exponentValue);
            var exponent = new Exponent(leftExpression, rightExpression);

            // Act
            decimal result = exponent.Evaluate();

            // Assert
            // Due to conversion from double to decimal, allow a small tolerance.
            Assert.InRange((double)result, expected - 0.0001, expected + 0.0001);
        }

        /// <summary>
        /// Tests that the Left and Right properties are properly set via the constructor.
        /// </summary>
        [Fact]
        public void Constructor_WithOperands_SetsLeftAndRightProperties()
        {
            // Arrange
            var left = new Number(3m);
            var right = new Number(4m);
            var exponent = new Exponent(left, right);

            // Act & Assert
            Assert.Equal(left, exponent.Left);
            Assert.Equal(right, exponent.Right);
        }
    }
}
