using Moq;
using Parlot.Benchmarks.PidginParsers;
using Parlot.Tests.Calc;
using Pidgin;
using Pidgin.Expression;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "ExprParser"/> class focusing on the 'Tok' methods.
/// </summary>
public class ExprParserTests
{
    /// <summary>
    /// Tests the generic Tok&lt;T&gt;(Parser<char , T>) method with a valid input.
    /// The test creates a parser that expects a specific character ('a') and verifies that using the Tok variant
    /// successfully parses the input and consumes trailing whitespace.
    /// Expected outcome: The parser returns the character 'a'.
    /// </summary>
//     [Fact] [Error] (26-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser' [Error] (39-23)CS0029 Cannot implicitly convert type 'Pidgin.Result<char, char>' to 'char' [Error] (32-28)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Tok_Generic_WithValidInput_ReturnsExpectedValue()
//     {
//         // Arrange: Create a simple parser that expects the character 'a'.
//         Parser<char, char> charParser = Parser.Char('a');
//         string input = "a   ";
//         // Use reflection to get the generic 'Tok' method.
//         MethodInfo genericTokMethod = typeof(ExprParser).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).Where(m => m.Name == "Tok" && m.IsGenericMethod).Single();
//         MethodInfo constructedMethod = genericTokMethod.MakeGenericMethod(typeof(char));
//         // Act: Invoke the generic Tok method to obtain a parser that wraps the provided parser.
//         object resultObj = constructedMethod.Invoke(null, new object[] { charParser });
//         var tokParser = resultObj as Parser<char, char>;
//         if (tokParser == null)
//         {
//             throw new InvalidOperationException("Failed to retrieve the generic Tok parser.");
//         }
// 
//         char result = tokParser.Parse(input);
//         // Assert: The result should be the expected character 'a'.
//         Assert.Equal('a', result);
//     }

    /// <summary>
    /// Tests the generic Tok&lt;T&gt;(Parser<char , T>) method with an invalid input.
    /// The test creates a parser that expects the character 'a' and verifies that providing input that does
    /// not match results in a parse exception.
    /// Expected outcome: A parse exception is thrown.
    /// </summary>
//     [Fact] [Error] (54-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser' [Error] (59-28)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Tok_Generic_WithInvalidInput_ThrowsParseException()
//     {
//         // Arrange: Create a parser that expects the character 'a'.
//         Parser<char, char> charParser = Parser.Char('a');
//         string input = "b   ";
//         // Use reflection to get the generic 'Tok' method.
//         MethodInfo genericTokMethod = typeof(ExprParser).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).Where(m => m.Name == "Tok" && m.IsGenericMethod).Single();
//         MethodInfo constructedMethod = genericTokMethod.MakeGenericMethod(typeof(char));
//         object resultObj = constructedMethod.Invoke(null, new object[] { charParser });
//         var tokParser = resultObj as Parser<char, char>;
//         if (tokParser == null)
//         {
//             throw new InvalidOperationException("Failed to retrieve the generic Tok parser.");
//         }
// 
//         // Act & Assert: Expect a ParseException when the input does not match.
//         Assert.Throws<ParseException>(() => tokParser.Parse(input));
//     }

    /// <summary>
    /// Tests the non-generic Tok(string) method with a valid input.
    /// The test provides a token string and verifies that the returned parser correctly matches the token and
    /// consumes trailing whitespace.
    /// Expected outcome: The parser returns the token string.
    /// </summary>
//     [Fact] [Error] (92-25)CS0029 Cannot implicitly convert type 'Pidgin.Result<char, string>' to 'string' [Error] (85-28)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Tok_String_WithValidInput_ReturnsExpectedToken()
//     {
//         // Arrange: Define the token string to be parsed.
//         string token = "abc";
//         string input = "abc    ";
//         // Use reflection to get the non-generic 'Tok' method that accepts a string parameter.
//         MethodInfo stringTokMethod = typeof(ExprParser).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).Where(m => m.Name == "Tok" && !m.IsGenericMethod).Single();
//         // Act: Invoke to retrieve the parser.
//         object resultObj = stringTokMethod.Invoke(null, new object[] { token });
//         var tokParser = resultObj as Parser<char, string>;
//         if (tokParser == null)
//         {
//             throw new InvalidOperationException("Failed to retrieve the string Tok parser.");
//         }
// 
//         string result = tokParser.Parse(input);
//         // Assert: The parser should return the original token.
//         Assert.Equal(token, result);
//     }

    /// <summary>
    /// Tests the non-generic Tok(string) method with an invalid input.
    /// The test supplies input that does not match the expected token string and verifies that a parse exception is thrown.
    /// Expected outcome: A parse exception is thrown.
    /// </summary>
//     [Fact] [Error] (110-28)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Tok_String_WithInvalidInput_ThrowsParseException()
//     {
//         // Arrange: Define the token string and an input that does not match.
//         string token = "abc";
//         string input = "abx";
//         // Use reflection to get the non-generic 'Tok' method.
//         MethodInfo stringTokMethod = typeof(ExprParser).GetMethods(BindingFlags.NonPublic | BindingFlags.Static).Where(m => m.Name == "Tok" && !m.IsGenericMethod).Single();
//         object resultObj = stringTokMethod.Invoke(null, new object[] { token });
//         var tokParser = resultObj as Parser<char, string>;
//         if (tokParser == null)
//         {
//             throw new InvalidOperationException("Failed to retrieve the string Tok parser.");
//         }
// 
//         // Act & Assert: Expect a ParseException when the input token does not match.
//         Assert.Throws<ParseException>(() => tokParser.Parse(input));
//     }

    /// <summary>
    /// Tests that the generic Tok method returns a parser that successfully parses the expected token and consumes trailing whitespaces.
    /// </summary>
//     [Fact] [Error] (130-42)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser' [Error] (134-25)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void TokGeneric_WithValidToken_ReturnsExpectedResult()
//     {
//         // Arrange: Obtain the private generic Tok<T> method info.
//         MethodInfo tokGenericMethod = typeof(ExprParser).GetMethods(BindingFlags.Static | BindingFlags.NonPublic).First(m => m.Name == "Tok" && m.IsGenericMethod);
//         // Prepare a simple token parser that parses the character 'a'
//         Parser<char, char> tokenParser = Parser.Char('a');
//         // Construct the generic method for type char.
//         MethodInfo constructedMethod = tokGenericMethod.MakeGenericMethod(typeof(char));
//         // Act: Invoke the method to get a new parser that wraps the token parser.
//         object result = constructedMethod.Invoke(null, new object[] { tokenParser });
//         var tokParser = result as Parser<char, char>;
//         // Verify that the obtained object is a valid parser.
//         Assert.NotNull(tokParser);
//         // Act: Parse an input that contains the token 'a' followed by whitespaces.
//         var parseResult = tokParser.Parse("a   ");
//         // Assert: Ensure the parser succeeds and returns the character 'a'.
//         Assert.True(parseResult.Success, "Expected the parser to succeed when input contains 'a' followed by whitespaces.");
//         Assert.Equal('a', parseResult.Value);
//     }

    /// <summary>
    /// Tests that the generic Tok method returns a parser that fails when the input does not start with the expected token.
    /// </summary>
//     [Fact] [Error] (154-42)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser' [Error] (158-25)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void TokGeneric_WithInvalidToken_ReturnsFailure()
//     {
//         // Arrange: Obtain the private generic Tok<T> method info.
//         MethodInfo tokGenericMethod = typeof(ExprParser).GetMethods(BindingFlags.Static | BindingFlags.NonPublic).First(m => m.Name == "Tok" && m.IsGenericMethod);
//         // Prepare a simple token parser that parses the character 'a'
//         Parser<char, char> tokenParser = Parser.Char('a');
//         // Construct the generic method for type char.
//         MethodInfo constructedMethod = tokGenericMethod.MakeGenericMethod(typeof(char));
//         // Act: Invoke to get the parser.
//         object result = constructedMethod.Invoke(null, new object[] { tokenParser });
//         var tokParser = result as Parser<char, char>;
//         Assert.NotNull(tokParser);
//         // Act: Parse an input that does not start with the expected token.
//         var parseResult = tokParser.Parse("b   ");
//         // Assert: Ensure the parser fails.
//         Assert.False(parseResult.Success, "Expected the parser to fail when input does not start with the expected token.");
//     }

    /// <summary>
    /// Tests that the string overload of Tok returns a parser that successfully parses the given token string and consumes trailing whitespaces.
    /// </summary>
//     [Fact] [Error] (174-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (178-25)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void TokString_WithValidToken_ReturnsExpectedResult()
//     {
//         // Arrange: Get the private static Tok(string token) method info.
//         MethodInfo tokStringMethod = typeof(ExprParser).GetMethod("Tok", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
//         Assert.NotNull(tokStringMethod);
//         string tokenValue = "hello";
//         // Act: Invoke the Tok(string) method to obtain a parser.
//         object result = tokStringMethod.Invoke(null, new object[] { tokenValue });
//         var tokParser = result as Parser<char, string>;
//         Assert.NotNull(tokParser);
//         // Act: Parse an input that contains the token "hello" with trailing spaces.
//         var parseResult = tokParser.Parse("hello   ");
//         // Assert: Verify that the parser succeeds and returns the string "hello".
//         Assert.True(parseResult.Success, "Expected the parser to succeed when input contains 'hello' followed by whitespaces.");
//         Assert.Equal(tokenValue, parseResult.Value);
//     }

    /// <summary>
    /// Tests that the string overload of Tok returns a parser that fails when the input does not start with the provided token.
    /// </summary>
//     [Fact] [Error] (195-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (198-25)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void TokString_WithInvalidToken_ReturnsFailure()
//     {
//         // Arrange: Get the private static Tok(string token) method info.
//         MethodInfo tokStringMethod = typeof(ExprParser).GetMethod("Tok", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
//         Assert.NotNull(tokStringMethod);
//         string tokenValue = "hello";
//         object result = tokStringMethod.Invoke(null, new object[] { tokenValue });
//         var tokParser = result as Parser<char, string>;
//         Assert.NotNull(tokParser);
//         // Act: Parse an input that does not match the expected token.
//         var parseResult = tokParser.Parse("hi there");
//         // Assert: The parser should fail to parse the expected token.
//         Assert.False(parseResult.Success, "Expected the parser to fail when input does not start with the specified token string.");
//     }

    /// <summary>
    /// Tests that the string overload of Tok throws an exception when provided with a null token.
    /// </summary>
//     [Fact] [Error] (214-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (217-116)CS8625 Cannot convert null literal to non-nullable reference type.
//     public void TokString_WithNullToken_ThrowsException()
//     {
//         // Arrange: Get the private static Tok(string token) method info.
//         MethodInfo tokStringMethod = typeof(ExprParser).GetMethod("Tok", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
//         Assert.NotNull(tokStringMethod);
//         // Act & Assert: Expect an exception when passing null as the token.
//         var exception = Assert.Throws<TargetInvocationException>(() => tokStringMethod.Invoke(null, new object[] { null }));
//         Assert.NotNull(exception.InnerException);
//         // Depending on the implementation of the underlying String method in Pidgin, it might throw an ArgumentNullException.
//         Assert.True(exception.InnerException is ArgumentNullException, "Expected an ArgumentNullException when a null token is provided.");
//     }

    /// <summary>
    /// Tests that the Parenthesized method returns the inner parser's result when valid parentheses are present.
    /// This test uses reflection to access the private generic Parenthesized method.
    /// It creates an inner parser that always returns the constant value 42.
    /// Expected: Parsing a valid input with matching '(' and ')' succeeds and returns 42.
    /// </summary>
    /// <param name = "input">The input string to parse.</param>
//     [Theory] [Error] (237-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser' [Error] (239-42)CS8600 Converting null literal or possible null value to non-nullable type.
//     [InlineData("()")]
//     [InlineData(" ( ) ")]
//     public void Parenthesized_ValidInput_ReturnsInnerParserResult(string input)
//     {
//         // Arrange
//         // Create an inner parser that always returns 42 without consuming input.
//         Parser<char, int> innerParser = Parser.Return(42);
//         // Use reflection to get the private generic method Parenthesized<T> from ExprParser.
//         MethodInfo parenthesizedMethod = typeof(ExprParser).GetMethod("Parenthesized", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(parenthesizedMethod);
//         // Make the generic method for type int.
//         MethodInfo genericMethod = parenthesizedMethod.MakeGenericMethod(typeof(int));
//         // Invoke the method to create a parser that expects the inner parser to be between parentheses.
//         var parserObject = genericMethod.Invoke(null, new object[] { innerParser });
//         Assert.NotNull(parserObject);
//         var parser = parserObject as Parser<char, int>;
//         Assert.NotNull(parser);
//         // Act
//         int result = parser.ParseOrThrow(input);
//         // Assert
//         Assert.Equal(42, result);
//     }

    /// <summary>
    /// Tests that the Parenthesized method throws an exception when the input is missing the closing parenthesis.
    /// This test uses reflection to access the private generic Parenthesized method.
    /// Expected: Parsing input with an opening parenthesis but missing ')' results in a parsing exception.
    /// </summary>
//     [Fact] [Error] (263-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser' [Error] (264-42)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Parenthesized_MissingClosingParenthesis_ThrowsException()
//     {
//         // Arrange
//         Parser<char, int> innerParser = Parser.Return(42);
//         MethodInfo parenthesizedMethod = typeof(ExprParser).GetMethod("Parenthesized", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(parenthesizedMethod);
//         MethodInfo genericMethod = parenthesizedMethod.MakeGenericMethod(typeof(int));
//         var parserObject = genericMethod.Invoke(null, new object[] { innerParser });
//         Assert.NotNull(parserObject);
//         var parser = parserObject as Parser<char, int>;
//         Assert.NotNull(parser);
//         string input = "("; // Missing closing parenthesis
//         // Act & Assert
//         Assert.Throws<Exception>(() => parser.ParseOrThrow(input));
//     }

    /// <summary>
    /// Tests that the Parenthesized method throws an exception when the input is missing the opening parenthesis.
    /// This test uses reflection to access the private generic Parenthesized method.
    /// Expected: Parsing input without an opening parenthesis but with a closing parenthesis results in a parsing exception.
    /// </summary>
//     [Fact] [Error] (285-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser' [Error] (286-42)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Parenthesized_MissingOpeningParenthesis_ThrowsException()
//     {
//         // Arrange
//         Parser<char, int> innerParser = Parser.Return(42);
//         MethodInfo parenthesizedMethod = typeof(ExprParser).GetMethod("Parenthesized", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(parenthesizedMethod);
//         MethodInfo genericMethod = parenthesizedMethod.MakeGenericMethod(typeof(int));
//         var parserObject = genericMethod.Invoke(null, new object[] { innerParser });
//         Assert.NotNull(parserObject);
//         var parser = parserObject as Parser<char, int>;
//         Assert.NotNull(parser);
//         string input = ")"; // Missing opening parenthesis
//         // Act & Assert
//         Assert.Throws<Exception>(() => parser.ParseOrThrow(input));
//     }

    /// <summary>
    /// Tests that the Binary method returns a delegate which produces an Addition instance when the operator is "+".
    /// </summary>
//     [Fact] [Error] (305-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser'
//     public void Binary_WhenOperatorIsPlus_ReturnsAdditionDelegate()
//     {
//         // Arrange
//         Parser<char, string> opParser = Parser.Return<char, string>("+");
//         var binaryParser = InvokeBinary(opParser);
//         // Act
//         Func<Expression, Expression, Expression> opDelegate = binaryParser.ParseOrThrow(string.Empty);
//         var left = new DummyExpression();
//         var right = new DummyExpression();
//         Expression result = opDelegate(left, right);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<Addition>(result);
//     }

    /// <summary>
    /// Tests that the Binary method returns a delegate which produces a Subtraction instance when the operator is "-".
    /// </summary>
//     [Fact] [Error] (324-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser'
//     public void Binary_WhenOperatorIsMinus_ReturnsSubtractionDelegate()
//     {
//         // Arrange
//         Parser<char, string> opParser = Parser.Return<char, string>("-");
//         var binaryParser = InvokeBinary(opParser);
//         // Act
//         Func<Expression, Expression, Expression> opDelegate = binaryParser.ParseOrThrow(string.Empty);
//         var left = new DummyExpression();
//         var right = new DummyExpression();
//         Expression result = opDelegate(left, right);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<Subtraction>(result);
//     }

    /// <summary>
    /// Tests that the Binary method returns a delegate which produces a Multiplication instance when the operator is "*".
    /// </summary>
//     [Fact] [Error] (343-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser'
//     public void Binary_WhenOperatorIsMultiply_ReturnsMultiplicationDelegate()
//     {
//         // Arrange
//         Parser<char, string> opParser = Parser.Return<char, string>("*");
//         var binaryParser = InvokeBinary(opParser);
//         // Act
//         Func<Expression, Expression, Expression> opDelegate = binaryParser.ParseOrThrow(string.Empty);
//         var left = new DummyExpression();
//         var right = new DummyExpression();
//         Expression result = opDelegate(left, right);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<Multiplication>(result);
//     }

    /// <summary>
    /// Tests that the Binary method returns a delegate which produces a Division instance when the operator is "/".
    /// </summary>
//     [Fact] [Error] (362-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser'
//     public void Binary_WhenOperatorIsDivide_ReturnsDivisionDelegate()
//     {
//         // Arrange
//         Parser<char, string> opParser = Parser.Return<char, string>("/");
//         var binaryParser = InvokeBinary(opParser);
//         // Act
//         Func<Expression, Expression, Expression> opDelegate = binaryParser.ParseOrThrow(string.Empty);
//         var left = new DummyExpression();
//         var right = new DummyExpression();
//         Expression result = opDelegate(left, right);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<Division>(result);
//     }

    /// <summary>
    /// Tests that the Binary method returns a delegate which produces null when the operator is not recognized.
    /// </summary>
//     [Fact] [Error] (381-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser'
//     public void Binary_WhenOperatorIsUnrecognized_ReturnsNullDelegateResult()
//     {
//         // Arrange
//         Parser<char, string> opParser = Parser.Return<char, string>("%");
//         var binaryParser = InvokeBinary(opParser);
//         // Act
//         Func<Expression, Expression, Expression> opDelegate = binaryParser.ParseOrThrow(string.Empty);
//         var left = new DummyExpression();
//         var right = new DummyExpression();
//         Expression result = opDelegate(left, right);
//         // Assert
//         Assert.Null(result);
//     }

    /// <summary>
    /// Uses reflection to invoke the private static Binary method of the ExprParser class with the specified operator parser.
    /// </summary>
    /// <param name = "opParser">A parser that returns an operator string.</param>
    /// <returns>A parser that produces a delegate combining two Expression objects.</returns>
//     private static Parser<char, Func<Expression, Expression, Expression>> InvokeBinary(Parser<char, string> opParser) [Error] (400-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (402-25)CS8600 Converting null literal or possible null value to non-nullable type.
//     {
//         var exprParserType = typeof(ExprParser);
//         MethodInfo binaryMethod = exprParserType.GetMethod("Binary", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(binaryMethod);
//         object result = binaryMethod.Invoke(null, new object[] { opParser });
//         Assert.NotNull(result);
//         return (Parser<char, Func<Expression, Expression, Expression>>)result;
//     }

    /// <summary>
    /// A dummy implementation of the Expression class used for testing purposes.
    /// </summary>
//     private class DummyExpression : Expression [Error] (410-19)CS0534 'ExprParserTests.DummyExpression' does not implement inherited abstract member 'Expression.Evaluate()'
//     {
//     }

    /// <summary>
    /// Tests that the Unary method returns a parser which produces a function that constructs a NegateExpression when provided with a valid parser.
    /// </summary>
//     [Fact] [Error] (422-41)CS0104 'Parser' is an ambiguous reference between 'Parlot.Tests.Calc.Parser' and 'Pidgin.Parser' [Error] (424-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (427-34)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Unary_WithValidParser_ReturnsFunctionThatCreatesNegateExpression()
//     {
//         // Arrange
//         // Create a parser that always returns a dummy string value.
//         Parser<char, string> opParser = Parser.Return("-");
//         // Use reflection to get the private static Unary method.
//         MethodInfo unaryMethod = typeof(ExprParser).GetMethod("Unary", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(unaryMethod);
//         // Act
//         object resultParserObj = unaryMethod.Invoke(null, new object[] { opParser });
//         Assert.IsType<Parser<char, Func<Expression, Expression>>>(resultParserObj);
//         var resultParser = (Parser<char, Func<Expression, Expression>>)resultParserObj;
//         // Execute the resulting parser. The parser does not depend on input.
//         var parseResult = resultParser.Parse("");
//         Assert.True(parseResult.Success, "Expected the parser to successfully parse an empty input.");
//         // Create a dummy Expression using Moq.
//         var dummyExpression = new Mock<Expression>().Object;
//         // Obtain the function produced by the Unary method and invoke it.
//         Func<Expression, Expression> negateFunc = parseResult.Value;
//         Expression resultExpression = negateFunc(dummyExpression);
//         // Assert: The resulting Expression should be a NegateExpression.
//         Assert.NotNull(resultExpression);
//         Assert.IsType<NegateExpression>(resultExpression);
//     }

    /// <summary>
    /// Tests that the Unary method throws a NullReferenceException when a null parser is provided as an argument.
    /// </summary>
//     [Fact] [Error] (450-43)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (451-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (454-134)CS8601 Possible null reference assignment.
//     public void Unary_WithNullParser_ThrowsException()
//     {
//         // Arrange
//         Parser<char, string> nullParser = null;
//         MethodInfo unaryMethod = typeof(ExprParser).GetMethod("Unary", BindingFlags.NonPublic | BindingFlags.Static);
//         Assert.NotNull(unaryMethod);
//         // Act & Assert
//         TargetInvocationException exception = Assert.Throws<TargetInvocationException>(() => unaryMethod.Invoke(null, new object[] { nullParser }));
//         Assert.IsType<NullReferenceException>(exception.InnerException);
//     }

    /// <summary>
    /// Tests that ParseOrThrow returns an Addition expression when given a valid simple addition expression.
    /// Input is "1+2" and expected to produce an Addition expression.
    /// </summary>
    [Fact]
    public void ParseOrThrow_ValidAddition_ReturnsAdditionExpression()
    {
        // Arrange
        string input = "1+2";
        // Act
        Expression result = ExprParser.ParseOrThrow(input);
        // Assert
        Assert.NotNull(result);
        Assert.IsType<Addition>(result);
    }

    /// <summary>
    /// Tests that ParseOrThrow returns an expression with the correct operator precedence for a complex expression.
    /// Input "1+2*3" is expected to be parsed as Addition where the Right operand is a Multiplication expression.
    /// </summary>
    [Fact]
    public void ParseOrThrow_ValidComplexExpression_ReturnsCorrectExpressionStructure()
    {
        // Arrange
        string input = "1+2*3";
        // Act
        Expression result = ExprParser.ParseOrThrow(input);
        // Assert
        Assert.NotNull(result);
        // The top-level operator for the expression should be Addition.
        Assert.IsType<Addition>(result);
    }

    /// <summary>
    /// Tests that ParseOrThrow throws a ParseException when provided an invalid expression input.
    /// Various invalid inputs such as "1++2", "++", and "1+" should trigger a ParseException.
    /// </summary>
    /// <param name = "input">The invalid expression input to parse.</param>
    [Theory]
    [InlineData("1++2")]
    [InlineData("++")]
    [InlineData("1+")]
    public void ParseOrThrow_InvalidExpression_ThrowsParseException(string input)
    {
        // Act & Assert
        Assert.Throws<ParseException>(() => ExprParser.ParseOrThrow(input));
    }

    /// <summary>
    /// Tests that ParseOrThrow throws a ParseException when provided an empty input string.
    /// </summary>
    [Fact]
    public void ParseOrThrow_EmptyInput_ThrowsParseException()
    {
        // Arrange
        string input = string.Empty;
        // Act & Assert
        Assert.Throws<ParseException>(() => ExprParser.ParseOrThrow(input));
    }

    /// <summary>
    /// Tests that ParseOrThrow throws an ArgumentNullException when provided a null input.
    /// </summary>
//     [Fact] [Error] (525-24)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void ParseOrThrow_NullInput_ThrowsArgumentNullException()
//     {
//         // Arrange
//         string input = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => ExprParser.ParseOrThrow(input));
//     }
}