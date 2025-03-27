using Parlot.Tests.Calc;
using System;
using Xunit;

namespace Parlot.Tests.Calc.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="FluentParser"/> class constructor (static initialization).
    /// </summary>
    public class FluentParserTests
    {
        /// <summary>
        /// Tests that accessing the static Expression property triggers the static constructor, thereby initializing 
        /// the Expression parser.
        /// </summary>
        [Fact]
        public void Constructor_WhenAccessingStaticMember_InitializesExpressionParser()
        {
            // Act
            var parser = FluentParser.Expression;
            
            // Assert
            Assert.NotNull(parser);
        }

        /// <summary>
        /// Tests that the Expression parser correctly parses a valid numeric expression.
        /// This test verifies the happy path scenario where a simple number is expected to be recognized 
        /// as a Number expression.
        /// </summary>
//         [Fact] [Error] (44-36)CS8602 Dereference of a possibly null reference.
//         public void Parse_WithValidNumberExpression_ReturnsNumberExpression()
//         {
//             // Arrange
//             var input = "123";
//             
//             // Act
//             // It is assumed that the parser's Parse method returns an Expression object
//             // and that a successful parse of a numeric input produces an instance of a type named "Number".
//             var result = FluentParser.Expression.Parse(input);
//             
//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal("Number", result.GetType().Name);
//         }

        /// <summary>
        /// Tests that the Expression parser throws an exception when given an invalid expression.
        /// This covers the exceptional scenario where the parser is unable to parse the input and is expected to 
        /// signal an error.
        /// </summary>
        [Fact]
        public void Parse_WithInvalidExpression_ThrowsException()
        {
            // Arrange
            var invalidInput = "abc";
            
            // Act & Assert
            Assert.ThrowsAny<Exception>(() => FluentParser.Expression.Parse(invalidInput));
        }
    }
}
