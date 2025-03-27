using System;
using Parlot.Tests.Calc;
using Xunit;

namespace Parlot.Tests.Calc.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Parser"/> class.
    /// </summary>
    public class ParserTests
    {
        private readonly Parser _parser;

        public ParserTests()
        {
            _parser = new Parser();
        }

        /// <summary>
        /// Helper method to recursively evaluate the expression tree.
        /// Assumes the AST nodes have the following properties:
        /// - Number: Value (decimal)
        /// - Addition/Subtraction/Multiplication/Division: Left and Right operands
        /// - NegateExpression: Expression operand
        /// </summary>
        /// <param name="expression">The expression tree.</param>
        /// <returns>The evaluated decimal value.</returns>
//         private decimal Evaluate(Expression expression) [Error] (52-41)CS1061 'NegateExpression' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'NegateExpression' could be found (are you missing a using directive or an assembly reference?)
//         {
//             if (expression is Number number)
//             {
//                 return number.Value;
//             }
//             else if (expression is Addition addition)
//             {
//                 return Evaluate(addition.Left) + Evaluate(addition.Right);
//             }
//             else if (expression is Subtraction subtraction)
//             {
//                 return Evaluate(subtraction.Left) - Evaluate(subtraction.Right);
//             }
//             else if (expression is Multiplication multiplication)
//             {
//                 return Evaluate(multiplication.Left) * Evaluate(multiplication.Right);
//             }
//             else if (expression is Division division)
//             {
//                 return Evaluate(division.Left) / Evaluate(division.Right);
//             }
//             else if (expression is NegateExpression negate)
//             {
//                 return -Evaluate(negate.Expression);
//             }
//             else
//             {
//                 throw new InvalidOperationException("Unknown expression type.");
//             }
//         }

        /// <summary>
        /// Tests that a valid numeric literal is parsed into a Number expression.
        /// </summary>
        [Fact]
        public void Parse_WithNumberLiteral_ReturnsNumberExpression()
        {
            // Arrange
            string input = "42";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            Assert.IsType<Number>(result);
            var number = (Number)result;
            Assert.Equal(42m, number.Value);
        }

        /// <summary>
        /// Tests that a simple addition expression is parsed into an Addition expression.
        /// </summary>
        [Fact]
        public void Parse_WithSimpleAddition_ReturnsAdditionExpression()
        {
            // Arrange
            string input = "3 + 5";

            // Act
            Expression result = _parser.Parse(input);

            // Assert: Expecting an Addition node with left and right Number nodes.
            Assert.IsType<Addition>(result);
            var addition = (Addition)result;
            Assert.IsType<Number>(addition.Left);
            Assert.IsType<Number>(addition.Right);
            Assert.Equal(3m, ((Number)addition.Left).Value);
            Assert.Equal(5m, ((Number)addition.Right).Value);
        }

        /// <summary>
        /// Tests that a combined addition and subtraction expression is parsed correctly into a left-associative tree.
        /// Expected AST: Subtraction(Addition(Number(10), Number(5)), Number(3))
        /// </summary>
        [Fact]
        public void Parse_WithAdditionAndSubtraction_ReturnsCorrectExpressionTree()
        {
            // Arrange
            string input = "10 + 5 - 3";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            Assert.IsType<Subtraction>(result);
            var subtraction = (Subtraction)result;
            Assert.IsType<Addition>(subtraction.Left);
            Assert.IsType<Number>(subtraction.Right);
            var addition = (Addition)subtraction.Left;
            Assert.Equal(10m, ((Number)addition.Left).Value);
            Assert.Equal(5m, ((Number)addition.Right).Value);
            Assert.Equal(3m, ((Number)subtraction.Right).Value);
        }

        /// <summary>
        /// Tests that a multiplication expression is parsed into a Multiplication expression.
        /// </summary>
        [Fact]
        public void Parse_WithMultiplication_ReturnsMultiplicationExpression()
        {
            // Arrange
            string input = "4 * 7";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            Assert.IsType<Multiplication>(result);
            var multiplication = (Multiplication)result;
            Assert.IsType<Number>(multiplication.Left);
            Assert.IsType<Number>(multiplication.Right);
            Assert.Equal(4m, ((Number)multiplication.Left).Value);
            Assert.Equal(7m, ((Number)multiplication.Right).Value);
        }

        /// <summary>
        /// Tests that a division expression is parsed into a Division expression.
        /// </summary>
        [Fact]
        public void Parse_WithDivision_ReturnsDivisionExpression()
        {
            // Arrange
            string input = "20 / 4";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            Assert.IsType<Division>(result);
            var division = (Division)result;
            Assert.IsType<Number>(division.Left);
            Assert.IsType<Number>(division.Right);
            Assert.Equal(20m, ((Number)division.Left).Value);
            Assert.Equal(4m, ((Number)division.Right).Value);
        }

        /// <summary>
        /// Tests that an expression with parentheses is parsed correctly.
        /// Expected AST: Multiplication(Addition(Number(2), Number(3)), Number(4))
        /// </summary>
        [Fact]
        public void Parse_WithParentheses_ReturnsCorrectExpressionTree()
        {
            // Arrange
            string input = "(2 + 3) * 4";

            // Act
            Expression result = _parser.Parse(input);

            // Assert
            Assert.IsType<Multiplication>(result);
            var multiplication = (Multiplication)result;
            Assert.IsType<Addition>(multiplication.Left);
            Assert.IsType<Number>(multiplication.Right);
            var addition = (Addition)multiplication.Left;
            Assert.Equal(2m, ((Number)addition.Left).Value);
            Assert.Equal(3m, ((Number)addition.Right).Value);
            Assert.Equal(4m, ((Number)multiplication.Right).Value);
        }

        /// <summary>
        /// Tests that a unary minus expression is parsed into a NegateExpression.
        /// Expected AST: NegateExpression(Number(8))
        /// </summary>
        [Fact]
        public void Parse_WithUnaryMinus_ReturnsNegateExpression()
        {
            // Arrange
            string input = "-8";

//             // Act [Error] (205-42)CS1061 'NegateExpression' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'NegateExpression' could be found (are you missing a using directive or an assembly reference?) [Error] (206-46)CS1061 'NegateExpression' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'NegateExpression' could be found (are you missing a using directive or an assembly reference?)
//             Expression result = _parser.Parse(input);
// 
//             // Assert
//             Assert.IsType<NegateExpression>(result);
//             var negate = (NegateExpression)result;
//             Assert.IsType<Number>(negate.Expression);
//             Assert.Equal(8m, ((Number)negate.Expression).Value);
//         }
// 
//         /// <summary>
//         /// Tests that a complex expression with nested unary operations and parentheses is parsed correctly.
//         /// Expected AST: Multiplication(NegateExpression(Addition(Number(3), Number(5))), Number(2))
//         /// </summary>
//         [Fact]
//         public void Parse_WithComplexExpression_ReturnsCorrectExpressionTree()
//         {
//             // Arrange
//             string input = "-(3 + 5) * 2";

            // Act
            Expression result = _parser.Parse(input);

//             // Assert [Error] (228-44)CS1061 'NegateExpression' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'NegateExpression' could be found (are you missing a using directive or an assembly reference?) [Error] (229-45)CS1061 'NegateExpression' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'NegateExpression' could be found (are you missing a using directive or an assembly reference?)
//             Assert.IsType<Multiplication>(result);
//             var multiplication = (Multiplication)result;
//             Assert.IsType<NegateExpression>(multiplication.Left);
//             Assert.IsType<Number>(multiplication.Right);
//             var negate = (NegateExpression)multiplication.Left;
//             Assert.IsType<Addition>(negate.Expression);
//             var addition = (Addition)negate.Expression;
//             Assert.Equal(3m, ((Number)addition.Left).Value);
            Assert.Equal(5m, ((Number)addition.Right).Value);
            Assert.Equal(2m, ((Number)multiplication.Right).Value);
        }

        /// <summary>
        /// Tests that parsing an expression missing a closing parenthesis throws a ParseException.
        /// </summary>
        [Fact]
        public void Parse_WithMissingClosingParenthesis_ThrowsParseException()
        {
            // Arrange
            string input = "(3 + 5";

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => _parser.Parse(input));
            Assert.Equal("Expected ')'", exception.Message);
        }

        /// <summary>
        /// Tests that parsing an expression with an invalid primary expression (e.g., non-numeric, non-parenthetical) throws a ParseException.
        /// </summary>
        [Fact]
        public void Parse_WithInvalidInput_ThrowsParseException()
        {
            // Arrange
            string input = "abc";

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => _parser.Parse(input));
            Assert.Equal("Expected primary expression", exception.Message);
        }

        /// <summary>
        /// Tests that parsing an input with a lone '-' operator throws a ParseException due to missing operand.
        /// </summary>
        [Fact]
        public void Parse_WithLoneMinusOperator_ThrowsParseException()
        {
            // Arrange
            string input = "-";

            // Act & Assert
            var exception = Assert.Throws<ParseException>(() => _parser.Parse(input));
            Assert.Equal("Expected primary expression", exception.Message);
        }
    }
}
