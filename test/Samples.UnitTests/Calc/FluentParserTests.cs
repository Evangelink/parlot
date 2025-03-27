using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Tests.Calc;
using Parlot.Tests.Json;
using System;
using Xunit;

namespace Parlot.Tests.Calc.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref = "FluentParser"/> class.
    /// These tests validate that the fluent arithmetic expression parser
    /// correctly parses valid arithmetic expressions and fails on invalid input.
    /// </summary>
    public class FluentParserTests
    {
        /// <summary>
        /// Tests that parsing a valid numeric input returns a Number expression.
        /// Input: "3"
        /// Expected Outcome: Parse succeeds with a Number result.
        /// </summary>
//         [Fact] [Error] (30-32)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (31-47)CS1061 'Expression' does not contain a definition for 'Remainder' and no accessible extension method 'Remainder' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (32-35)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (33-43)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidNumber_ReturnsNumberExpression()
//         {
//             // Arrange
//             string input = "3";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing should succeed for a valid numeric input.");
//             Assert.Equal(string.Empty, result.Remainder);
//             Assert.NotNull(result.Value);
//             Assert.Equal("Number", result.Value.GetType().Name);
//         }

        /// <summary>
        /// Tests that parsing a valid addition expression returns an Addition expression.
        /// Input: "1+2"
        /// Expected Outcome: Parse succeeds with an Addition result.
        /// </summary>
//         [Fact] [Error] (49-32)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (50-47)CS1061 'Expression' does not contain a definition for 'Remainder' and no accessible extension method 'Remainder' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (51-35)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (52-45)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidAddition_ReturnsAdditionExpression()
//         {
//             // Arrange
//             string input = "1+2";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing should succeed for a valid addition expression.");
//             Assert.Equal(string.Empty, result.Remainder);
//             Assert.NotNull(result.Value);
//             Assert.Equal("Addition", result.Value.GetType().Name);
//         }

        /// <summary>
        /// Tests that parsing a valid subtraction expression returns a Subtraction expression.
        /// Input: "4-2"
        /// Expected Outcome: Parse succeeds with a Subtraction result.
        /// </summary>
//         [Fact] [Error] (68-32)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (69-47)CS1061 'Expression' does not contain a definition for 'Remainder' and no accessible extension method 'Remainder' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (70-35)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (71-48)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidSubtraction_ReturnsSubtractionExpression()
//         {
//             // Arrange
//             string input = "4-2";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing should succeed for a valid subtraction expression.");
//             Assert.Equal(string.Empty, result.Remainder);
//             Assert.NotNull(result.Value);
//             Assert.Equal("Subtraction", result.Value.GetType().Name);
//         }

        /// <summary>
        /// Tests that parsing a valid multiplication expression returns a Multiplication expression.
        /// Input: "3*4"
        /// Expected Outcome: Parse succeeds with a Multiplication result.
        /// </summary>
//         [Fact] [Error] (87-32)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (88-47)CS1061 'Expression' does not contain a definition for 'Remainder' and no accessible extension method 'Remainder' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (89-35)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (90-51)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidMultiplication_ReturnsMultiplicationExpression()
//         {
//             // Arrange
//             string input = "3*4";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing should succeed for a valid multiplication expression.");
//             Assert.Equal(string.Empty, result.Remainder);
//             Assert.NotNull(result.Value);
//             Assert.Equal("Multiplication", result.Value.GetType().Name);
//         }

        /// <summary>
        /// Tests that parsing a valid division expression returns a Division expression.
        /// Input: "8/2"
        /// Expected Outcome: Parse succeeds with a Division result.
        /// </summary>
//         [Fact] [Error] (106-32)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (107-47)CS1061 'Expression' does not contain a definition for 'Remainder' and no accessible extension method 'Remainder' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (108-35)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (109-45)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidDivision_ReturnsDivisionExpression()
//         {
//             // Arrange
//             string input = "8/2";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing should succeed for a valid division expression.");
//             Assert.Equal(string.Empty, result.Remainder);
//             Assert.NotNull(result.Value);
//             Assert.Equal("Division", result.Value.GetType().Name);
//         }

        /// <summary>
        /// Tests that parsing a valid unary negation expression returns a NegateExpression.
        /// Input: "-5"
        /// Expected Outcome: Parse succeeds with a NegateExpression result.
        /// </summary>
//         [Fact] [Error] (125-32)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (126-47)CS1061 'Expression' does not contain a definition for 'Remainder' and no accessible extension method 'Remainder' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (127-35)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (128-53)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidUnaryNegation_ReturnsNegateExpression()
//         {
//             // Arrange
//             string input = "-5";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing should succeed for a valid unary negation expression.");
//             Assert.Equal(string.Empty, result.Remainder);
//             Assert.NotNull(result.Value);
//             Assert.Equal("NegateExpression", result.Value.GetType().Name);
//         }

        /// <summary>
        /// Tests that parsing a valid group expression returns the inner expression.
        /// Input: "(3)"
        /// Expected Outcome: Parse succeeds and returns a Number expression.
        /// </summary>
//         [Fact] [Error] (144-32)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (145-47)CS1061 'Expression' does not contain a definition for 'Remainder' and no accessible extension method 'Remainder' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (146-35)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (148-43)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ValidGroupedExpression_ReturnsNumberExpression()
//         {
//             // Arrange
//             string input = "(3)";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing should succeed for a valid grouped expression.");
//             Assert.Equal(string.Empty, result.Remainder);
//             Assert.NotNull(result.Value);
//             // Grouping returns the inner expression so we expect a Number.
//             Assert.Equal("Number", result.Value.GetType().Name);
//         }

        /// <summary>
        /// Tests that parsing an invalid input returns a failure.
        /// Input: "abc"
        /// Expected Outcome: Parse fails.
        /// </summary>
//         [Fact] [Error] (164-33)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_InvalidInput_ReturnsFailure()
//         {
//             // Arrange
//             string input = "abc";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.False(result.Success, "Parsing should fail for an invalid input that does not conform to grammar.");
//         }

        /// <summary>
        /// Tests that parsing an incomplete expression returns a failure.
        /// Input: "1+"
        /// Expected Outcome: Parse fails due to missing operand.
        /// </summary>
//         [Fact] [Error] (180-33)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_IncompleteExpression_ReturnsFailure()
//         {
//             // Arrange
//             string input = "1+";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.False(result.Success, "Parsing should fail when the expression is incomplete.");
//         }

        /// <summary>
        /// Tests that parsing a complex expression returns the expected hierarchical expression.
        /// Input: "(1+2)*3-4/5"
        /// Expected Outcome: Parse succeeds with a Subtraction as the root node.
        /// </summary>
//         [Fact] [Error] (196-32)CS1061 'Expression' does not contain a definition for 'Success' and no accessible extension method 'Success' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (197-47)CS1061 'Expression' does not contain a definition for 'Remainder' and no accessible extension method 'Remainder' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (198-35)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?) [Error] (201-48)CS1061 'Expression' does not contain a definition for 'Value' and no accessible extension method 'Value' accepting a first argument of type 'Expression' could be found (are you missing a using directive or an assembly reference?)
//         public void Parse_ComplexExpression_ReturnsValidExpressionTree()
//         {
//             // Arrange
//             string input = "(1+2)*3-4/5";
//             // Act
//             var result = FluentParser.Expression.Parse(input);
//             // Assert
//             Assert.True(result.Success, "Parsing should succeed for a valid complex arithmetic expression.");
//             Assert.Equal(string.Empty, result.Remainder);
//             Assert.NotNull(result.Value);
//             // The grammar defines additive expressions at the top level.
//             // For the given input, the subtraction operation should be the root.
//             Assert.Equal("Subtraction", result.Value.GetType().Name);
//         }
    }
}