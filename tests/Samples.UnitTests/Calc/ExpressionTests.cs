using System;
using Parlot.Tests.Calc;
using Xunit;

namespace Parlot.Tests.Calc.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Number"/> class.
    /// </summary>
    public class NumberTests
    {
        private readonly decimal _sampleValue;

        public NumberTests()
        {
            _sampleValue = 42.5m;
        }

        /// <summary>
        /// Tests that Evaluate returns the same value that was provided to the Number instance.
        /// </summary>
        [Fact]
        public void Evaluate_WhenCalled_ReturnsProvidedValue()
        {
            // Arrange
            var number = new Number(_sampleValue);

            // Act
            decimal result = number.Evaluate();

            // Assert
            Assert.Equal(_sampleValue, result);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="NegateExpression"/> class.
    /// </summary>
    public class NegateExpressionTests
    {
        /// <summary>
        /// Tests that Evaluate returns the negated result of its inner expression.
        /// </summary>
        [Fact]
        public void Evaluate_WhenInnerExpressionReturnsPositive_ReturnsNegatedValue()
        {
            // Arrange
            decimal value = 10m;
            var innerExpression = new Number(value);
            var negateExpression = new NegateExpression(innerExpression);

            // Act
            decimal result = negateExpression.Evaluate();

            // Assert
            Assert.Equal(-value, result);
        }

        /// <summary>
        /// Tests that Evaluate handles negative inner expression value correctly.
        /// </summary>
        [Fact]
        public void Evaluate_WhenInnerExpressionReturnsNegative_ReturnsNegatedValue()
        {
            // Arrange
            decimal value = -15m;
            var innerExpression = new Number(value);
            var negateExpression = new NegateExpression(innerExpression);

            // Act
            decimal result = negateExpression.Evaluate();

            // Assert
            Assert.Equal(15m, result);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="Addition"/> class.
    /// </summary>
    public class AdditionTests
    {
        /// <summary>
        /// Tests that Evaluate returns the sum of the left and right expressions.
        /// </summary>
        /// <param name="leftValue">Left operand value.</param>
        /// <param name="rightValue">Right operand value.</param>
        /// <param name="expectedSum">Expected sum.</param>
        [Theory]
        [InlineData(10, 5, 15)]
        [InlineData(-3, -7, -10)]
        [InlineData(0, 0, 0)]
        public void Evaluate_WhenCalled_ReturnsSum(decimal leftValue, decimal rightValue, decimal expectedSum)
        {
            // Arrange
            var leftExpression = new Number(leftValue);
            var rightExpression = new Number(rightValue);
            var addition = new Addition(leftExpression, rightExpression);

            // Act
            decimal result = addition.Evaluate();

            // Assert
            Assert.Equal(expectedSum, result);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="Subtraction"/> class.
    /// </summary>
    public class SubtractionTests
    {
        /// <summary>
        /// Tests that Evaluate returns the difference between the left and right expressions.
        /// </summary>
        /// <param name="leftValue">Minuend value.</param>
        /// <param name="rightValue">Subtrahend value.</param>
        /// <param name="expectedDifference">Expected difference.</param>
        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(-3, -7, 4)]
        [InlineData(0, 0, 0)]
        public void Evaluate_WhenCalled_ReturnsDifference(decimal leftValue, decimal rightValue, decimal expectedDifference)
        {
            // Arrange
            var leftExpression = new Number(leftValue);
            var rightExpression = new Number(rightValue);
            var subtraction = new Subtraction(leftExpression, rightExpression);

            // Act
            decimal result = subtraction.Evaluate();

            // Assert
            Assert.Equal(expectedDifference, result);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="Multiplication"/> class.
    /// </summary>
    public class MultiplicationTests
    {
        /// <summary>
        /// Tests that Evaluate returns the product of the left and right expressions.
        /// </summary>
        /// <param name="leftValue">Left operand value.</param>
        /// <param name="rightValue">Right operand value.</param>
        /// <param name="expectedProduct">Expected product.</param>
        [Theory]
        [InlineData(10, 5, 50)]
        [InlineData(-3, -7, 21)]
        [InlineData(-4, 6, -24)]
        [InlineData(0, 100, 0)]
        public void Evaluate_WhenCalled_ReturnsProduct(decimal leftValue, decimal rightValue, decimal expectedProduct)
        {
            // Arrange
            var leftExpression = new Number(leftValue);
            var rightExpression = new Number(rightValue);
            var multiplication = new Multiplication(leftExpression, rightExpression);

            // Act
            decimal result = multiplication.Evaluate();

            // Assert
            Assert.Equal(expectedProduct, result);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="Division"/> class.
    /// </summary>
    public class DivisionTests
    {
        /// <summary>
        /// Tests that Evaluate returns the quotient of the left and right expressions.
        /// </summary>
        /// <param name="numerator">Numerator value.</param>
        /// <param name="denominator">Denominator value.</param>
        /// <param name="expectedQuotient">Expected quotient.</param>
        [Theory]
        [InlineData(10, 5, 2)]
        [InlineData(-20, -4, 5)]
        [InlineData(0, 10, 0)]
        public void Evaluate_WhenCalled_ReturnsQuotient(decimal numerator, decimal denominator, decimal expectedQuotient)
        {
            // Arrange
            var leftExpression = new Number(numerator);
            var rightExpression = new Number(denominator);
            var division = new Division(leftExpression, rightExpression);

            // Act
            decimal result = division.Evaluate();

            // Assert
            Assert.Equal(expectedQuotient, result);
        }

        /// <summary>
        /// Tests that Evaluate throws a DivideByZeroException when the right expression evaluates to zero.
        /// </summary>
        [Fact]
        public void Evaluate_WhenDenominatorIsZero_ThrowsDivideByZeroException()
        {
            // Arrange
            var leftExpression = new Number(10);
            var rightExpression = new Number(0);
            var division = new Division(leftExpression, rightExpression);

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => division.Evaluate());
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="Exponent"/> class.
    /// </summary>
    public class ExponentTests
    {
        /// <summary>
        /// Tests that Evaluate returns the correct exponentiation result for provided operands.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="exponentValue">The exponent value.</param>
        /// <param name="expectedResult">The expected result.</param>
        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(5, 0, 1)]
        [InlineData(2, -2, 0.25)]
        [InlineData(10, 1, 10)]
        public void Evaluate_WhenCalled_ReturnsExponentiationResult(decimal baseValue, decimal exponentValue, decimal expectedResult)
        {
            // Arrange
            var leftExpression = new Number(baseValue);
            var rightExpression = new Number(exponentValue);
            var exponent = new Exponent(leftExpression, rightExpression);

            // Act
            decimal result = exponent.Evaluate();

            // Assert
            // Using a tolerance because of possible conversion and floating point imprecision.
            Assert.InRange(result, expectedResult - 0.0001m, expectedResult + 0.0001m);
        }
    }
}
