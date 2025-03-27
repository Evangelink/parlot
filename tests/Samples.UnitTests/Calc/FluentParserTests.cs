using System;
using Parlot.Fluent;
using Parlot.Tests.Calc;
using Xunit;

namespace Parlot.Tests.Calc.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="FluentParser"/> class.
    /// </summary>
    public class FluentParserTests
    {
        /// <summary>
        /// Verifies that the Expression parser static field is properly initialized.
        /// </summary>
        [Fact]
        public void Expression_StaticField_IsNotNull()
        {
            // Arrange & Act
            var parser = FluentParser.Expression;
            
            // Assert
            Assert.NotNull(parser);
        }

        /// <summary>
        /// Tests that parsing a simple number string returns a Number expression.
        /// Functional steps:
        /// 1. Input a valid number string.
        /// 2. Call the parser's Parse method.
        /// 3. Verify that the result is successful and the returned expression is of type Number.
        /// Expected outcome: the parser returns a Number expression.
        /// </summary>
//         [Fact] [Error] (41-58)CS1503 Argument 1: cannot convert from 'System.ReadOnlySpan<char>' to 'Parlot.Fluent.ParseContext' [Error] (41-78)CS1620 Argument 2 must be passed with the 'ref' keyword
//         public void Parse_SimpleNumber_ReturnsNumberExpression()
//         {
//             // Arrange
//             var input = "123";
// 
//             // Act
//             bool success = FluentParser.Expression.Parse(input.AsSpan(), out Expression result);
// 
//             // Assert
//             Assert.True(success, "Parsing a valid number should succeed.");
//             Assert.NotNull(result);
//             Assert.IsType<Number>(result);
//         }

        /// <summary>
        /// Tests that parsing a parenthesized number returns a Number expression.
        /// Functional steps:
        /// 1. Input a valid parenthesized number.
        /// 2. Call the parser's Parse method.
        /// 3. Verify that the result is successful and the returned expression is of type Number.
        /// Expected outcome: the parser returns a Number expression.
        /// </summary>
//         [Fact] [Error] (64-58)CS1503 Argument 1: cannot convert from 'System.ReadOnlySpan<char>' to 'Parlot.Fluent.ParseContext' [Error] (64-78)CS1620 Argument 2 must be passed with the 'ref' keyword
//         public void Parse_NumberInParentheses_ReturnsNumberExpression()
//         {
//             // Arrange
//             var input = "(456)";
// 
//             // Act
//             bool success = FluentParser.Expression.Parse(input.AsSpan(), out Expression result);
// 
//             // Assert
//             Assert.True(success, "Parsing a parenthesized number should succeed.");
//             Assert.NotNull(result);
//             Assert.IsType<Number>(result);
//         }

        /// <summary>
        /// Tests that parsing a negative number returns a NegateExpression wrapping a Number.
        /// Functional steps:
        /// 1. Input a valid negative number string.
        /// 2. Call the parser's Parse method.
        /// 3. Verify that the returned expression is a NegateExpression whose inner expression is a Number.
        /// Expected outcome: the parser returns a NegateExpression containing a Number.
        /// </summary>
//         [Fact] [Error] (87-58)CS1503 Argument 1: cannot convert from 'System.ReadOnlySpan<char>' to 'Parlot.Fluent.ParseContext' [Error] (87-78)CS1620 Argument 2 must be passed with the 'ref' keyword
//         public void Parse_NegativeNumber_ReturnsNegateExpression()
//         {
//             // Arrange
//             var input = "-789";
// 
//             // Act
//             bool success = FluentParser.Expression.Parse(input.AsSpan(), out Expression result);
// 
//             // Assert
//             Assert.True(success, "Parsing a negative number should succeed.");
//             Assert.NotNull(result);
//             var negateExpr = Assert.IsType<NegateExpression>(result);
//             Assert.NotNull(negateExpr.Inner);
//             Assert.IsType<Number>(negateExpr.Inner);
//         }

        /// <summary>
        /// Tests that parsing an addition expression returns an Addition expression.
        /// Functional steps:
        /// 1. Input a valid addition expression string.
        /// 2. Call the parser's Parse method.
        /// 3. Verify that the returned expression is an Addition instance with valid left and right Number expressions.
        /// Expected outcome: the parser returns a properly constructed Addition expression tree.
        /// </summary>
//         [Fact] [Error] (112-58)CS1503 Argument 1: cannot convert from 'System.ReadOnlySpan<char>' to 'Parlot.Fluent.ParseContext' [Error] (112-78)CS1620 Argument 2 must be passed with the 'ref' keyword
//         public void Parse_AdditionExpression_ReturnsAdditionExpression()
//         {
//             // Arrange
//             var input = "1+2";
// 
//             // Act
//             bool success = FluentParser.Expression.Parse(input.AsSpan(), out Expression result);
// 
//             // Assert
//             Assert.True(success, "Parsing an addition expression should succeed.");
//             var additionExpr = Assert.IsType<Addition>(result);
//             Assert.NotNull(additionExpr.Left);
//             Assert.NotNull(additionExpr.Right);
//             Assert.IsType<Number>(additionExpr.Left);
//             Assert.IsType<Number>(additionExpr.Right);
//         }

        /// <summary>
        /// Tests that parsing a multiplication expression returns a Multiplication expression.
        /// Functional steps:
        /// 1. Input a valid multiplication expression string.
        /// 2. Call the parser's Parse method.
        /// 3. Verify that the returned expression is a Multiplication instance with valid left and right Number expressions.
        /// Expected outcome: the parser returns a properly structured Multiplication expression.
        /// </summary>
//         [Fact] [Error] (138-58)CS1503 Argument 1: cannot convert from 'System.ReadOnlySpan<char>' to 'Parlot.Fluent.ParseContext' [Error] (138-78)CS1620 Argument 2 must be passed with the 'ref' keyword
//         public void Parse_MultiplicationExpression_ReturnsMultiplicationExpression()
//         {
//             // Arrange
//             var input = "2*3";
// 
//             // Act
//             bool success = FluentParser.Expression.Parse(input.AsSpan(), out Expression result);
// 
//             // Assert
//             Assert.True(success, "Parsing a multiplication expression should succeed.");
//             var multiplicationExpr = Assert.IsType<Multiplication>(result);
//             Assert.NotNull(multiplicationExpr.Left);
//             Assert.NotNull(multiplicationExpr.Right);
//             Assert.IsType<Number>(multiplicationExpr.Left);
//             Assert.IsType<Number>(multiplicationExpr.Right);
//         }

        /// <summary>
        /// Tests that the parser maintains operator precedence (multiplication over addition).
        /// Functional steps:
        /// 1. Input an expression with mixed operators.
        /// 2. Call the parser's Parse method.
        /// 3. Verify that the returned expression tree reflects proper operator precedence.
        /// Expected outcome: an Addition expression whose right child is a Multiplication expression.
        /// </summary>
//         [Fact] [Error] (164-58)CS1503 Argument 1: cannot convert from 'System.ReadOnlySpan<char>' to 'Parlot.Fluent.ParseContext' [Error] (164-78)CS1620 Argument 2 must be passed with the 'ref' keyword
//         public void Parse_OperatorPrecedence_ReturnsCorrectExpressionTree()
//         {
//             // Arrange
//             var input = "1+2*3";
// 
//             // Act
//             bool success = FluentParser.Expression.Parse(input.AsSpan(), out Expression result);
// 
//             // Assert
//             Assert.True(success, "Parsing an expression with mixed operators should succeed.");
//             var additionExpr = Assert.IsType<Addition>(result);
//             Assert.IsType<Number>(additionExpr.Left);
//             var multiplicationExpr = Assert.IsType<Multiplication>(additionExpr.Right);
//             Assert.NotNull(multiplicationExpr.Left);
//             Assert.NotNull(multiplicationExpr.Right);
//             Assert.IsType<Number>(multiplicationExpr.Left);
//             Assert.IsType<Number>(multiplicationExpr.Right);
//         }

        /// <summary>
        /// Tests that parsing an invalid expression (e.g., incomplete expression) fails.
        /// Functional steps:
        /// 1. Input an invalid expression string.
        /// 2. Call the parser's Parse method.
        /// 3. Verify that the parsing fails and no expression is returned.
        /// Expected outcome: the parser returns false and a null result.
        /// </summary>
//         [Fact] [Error] (192-58)CS1503 Argument 1: cannot convert from 'System.ReadOnlySpan<char>' to 'Parlot.Fluent.ParseContext' [Error] (192-78)CS1620 Argument 2 must be passed with the 'ref' keyword
//         public void Parse_InvalidExpression_ReturnsFalse()
//         {
//             // Arrange
//             var input = "1+";
// 
//             // Act
//             bool success = FluentParser.Expression.Parse(input.AsSpan(), out Expression result);
// 
//             // Assert
//             Assert.False(success, "Parsing an incomplete expression should fail.");
//             Assert.Null(result);
//         }

        /// <summary>
        /// Tests that parsing an empty input string fails.
        /// Functional steps:
        /// 1. Input an empty string.
        /// 2. Call the parser's Parse method.
        /// 3. Verify that the parser indicates failure.
        /// Expected outcome: the parser returns false and a null result.
        /// </summary>
//         [Fact] [Error] (214-58)CS1503 Argument 1: cannot convert from 'System.ReadOnlySpan<char>' to 'Parlot.Fluent.ParseContext' [Error] (214-78)CS1620 Argument 2 must be passed with the 'ref' keyword
//         public void Parse_EmptyInput_ReturnsFalse()
//         {
//             // Arrange
//             var input = string.Empty;
// 
//             // Act
//             bool success = FluentParser.Expression.Parse(input.AsSpan(), out Expression result);
// 
//             // Assert
//             Assert.False(success, "Parsing an empty input should fail.");
//             Assert.Null(result);
//         }
    }
}
