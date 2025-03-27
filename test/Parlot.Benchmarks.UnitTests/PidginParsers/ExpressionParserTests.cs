using Parlot.Benchmarks.PidginParsers;
using Parlot.Tests.Calc;
using System;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Benchmarks.PidginParsers.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref = "ExprParser"/> class.
    /// </summary>
    public class ExprParserTests
    {
        /// <summary>
        /// Tests that ParseOrThrow returns a Number expression when a valid decimal literal is provided.
        /// </summary>
//         [Fact] [Error] (23-13)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression'
//         public void ParseOrThrow_ValidDecimalLiteral_ReturnsNumberExpression()
//         {
//             // Arrange
//             string input = "  3.14  ";
//             // Act
//             Expression result = ExprParser.ParseOrThrow(input);
//             // Assert
//             var number = Assert.IsType<Number>(result);
//             Assert.Equal(3.14m, number.Value);
//         }

        /// <summary>
        /// Tests that ParseOrThrow returns an Addition expression when a simple addition expression is provided.
        /// </summary>
//         [Fact] [Error] (38-13)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression'
//         public void ParseOrThrow_SimpleAddition_ReturnsAdditionExpression()
//         {
//             // Arrange
//             string input = "1+2";
//             // Act
//             Expression result = ExprParser.ParseOrThrow(input);
//             // Assert
//             var addition = Assert.IsType<Addition>(result);
//             var leftNumber = Assert.IsType<Number>(addition.Left);
//             var rightNumber = Assert.IsType<Number>(addition.Right);
//             Assert.Equal(1m, leftNumber.Value);
//             Assert.Equal(2m, rightNumber.Value);
//         }

        /// <summary>
        /// Tests that ParseOrThrow returns a NegateExpression when a negative literal is provided.
        /// </summary>
//         [Fact] [Error] (56-13)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression' [Error] (59-60)CS1061 'NegateExpression' does not contain a definition for 'Expression' and no accessible extension method 'Expression' accepting a first argument of type 'NegateExpression' could be found (are you missing a using directive or an assembly reference?)
//         public void ParseOrThrow_NegativeNumber_ReturnsNegateExpression()
//         {
//             // Arrange
//             string input = "-3";
//             // Act
//             Expression result = ExprParser.ParseOrThrow(input);
//             // Assert
//             var negate = Assert.IsType<NegateExpression>(result);
//             var innerNumber = Assert.IsType<Number>(negate.Expression);
//             Assert.Equal(3m, innerNumber.Value);
//         }

        /// <summary>
        /// Tests that ParseOrThrow correctly applies operator precedence.
        /// Expression: "2*3+4" should be parsed as Addition(Multiplication(2, 3), 4).
        /// </summary>
//         [Fact] [Error] (73-13)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression'
//         public void ParseOrThrow_MultiplicationPrecedence_AdditionParsedCorrectly()
//         {
//             // Arrange
//             string input = "2*3+4";
//             // Act
//             Expression result = ExprParser.ParseOrThrow(input);
//             // Assert
//             var addition = Assert.IsType<Addition>(result);
//             var multiplication = Assert.IsType<Multiplication>(addition.Left);
//             var rightNumber = Assert.IsType<Number>(addition.Right);
//             var multLeft = Assert.IsType<Number>(multiplication.Left);
//             var multRight = Assert.IsType<Number>(multiplication.Right);
//             Assert.Equal(2m, multLeft.Value);
//             Assert.Equal(3m, multRight.Value);
//             Assert.Equal(4m, rightNumber.Value);
//         }

        /// <summary>
        /// Tests that ParseOrThrow correctly applies operator precedence.
        /// Expression: "2+3*4" should be parsed as Addition(2, Multiplication(3,4)).
        /// </summary>
//         [Fact] [Error] (95-13)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression'
//         public void ParseOrThrow_AdditionPrecedence_MultiplicationParsedCorrectly()
//         {
//             // Arrange
//             string input = "2+3*4";
//             // Act
//             Expression result = ExprParser.ParseOrThrow(input);
//             // Assert
//             var addition = Assert.IsType<Addition>(result);
//             var leftNumber = Assert.IsType<Number>(addition.Left);
//             var multiplication = Assert.IsType<Multiplication>(addition.Right);
//             var multLeft = Assert.IsType<Number>(multiplication.Left);
//             var multRight = Assert.IsType<Number>(multiplication.Right);
//             Assert.Equal(2m, leftNumber.Value);
//             Assert.Equal(3m, multLeft.Value);
//             Assert.Equal(4m, multRight.Value);
//         }

        /// <summary>
        /// Tests that ParseOrThrow correctly parses a parenthesized expression.
        /// Expression: "2*(3+4)" should be parsed as Multiplication(2, Addition(3,4)).
        /// </summary>
//         [Fact] [Error] (117-13)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression'
//         public void ParseOrThrow_ParenthesizedExpression_ReturnsProperlyGroupedExpression()
//         {
//             // Arrange
//             string input = "2*(3+4)";
//             // Act
//             Expression result = ExprParser.ParseOrThrow(input);
//             // Assert
//             var multiplication = Assert.IsType<Multiplication>(result);
//             var leftNumber = Assert.IsType<Number>(multiplication.Left);
//             var addition = Assert.IsType<Addition>(multiplication.Right);
//             var addLeft = Assert.IsType<Number>(addition.Left);
//             var addRight = Assert.IsType<Number>(addition.Right);
//             Assert.Equal(2m, leftNumber.Value);
//             Assert.Equal(3m, addLeft.Value);
//             Assert.Equal(4m, addRight.Value);
//         }

        /// <summary>
        /// Tests that ParseOrThrow throws an exception when an invalid expression is provided.
        /// Expression: "1 +" is incomplete.
        /// </summary>
        [Fact]
        public void ParseOrThrow_IncompleteExpression_ThrowsException()
        {
            // Arrange
            string input = "1 +";
            // Act & Assert
            Assert.Throws<Exception>(() => ExprParser.ParseOrThrow(input));
        }

        /// <summary>
        /// Tests that ParseOrThrow throws an exception when an empty string is provided.
        /// </summary>
        [Fact]
        public void ParseOrThrow_EmptyString_ThrowsException()
        {
            // Arrange
            string input = "";
            // Act & Assert
            Assert.Throws<Exception>(() => ExprParser.ParseOrThrow(input));
        }

        /// <summary>
        /// Tests that ParseOrThrow correctly parses nested parenthesized expressions.
        /// Expression: "((1))" should be parsed as a Number expression with value 1.
        /// </summary>
//         [Fact] [Error] (164-13)CS0104 'Expression' is an ambiguous reference between 'Parlot.Tests.Calc.Expression' and 'System.Linq.Expressions.Expression'
//         public void ParseOrThrow_NestedParentheses_ReturnsNumberExpression()
//         {
//             // Arrange
//             string input = "((1))";
//             // Act
//             Expression result = ExprParser.ParseOrThrow(input);
//             // Assert
//             var number = Assert.IsType<Number>(result);
//             Assert.Equal(1m, number.Value);
//         }
    }
}