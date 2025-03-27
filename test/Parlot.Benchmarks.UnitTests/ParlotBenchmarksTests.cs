// using Moq;
// using Parlot;
// using Parlot.Benchmarks;
// using Parlot.Fluent;
// using Parlot.Tests.Calc;
// using Parlot.Tests.Json;
// using System;
// using System.Reflection;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "ParlotBenchmarks"/> class.
// /// </summary>
// public class ParlotBenchmarksTests
// {
//     /// <summary>
//     /// Tests that the Setup method correctly invokes the Setup method of the JsonBench dependency.
//     /// </summary>
// //     [Fact] [Error] (30-9)CS8602 Dereference of a possibly null reference.
// //     public void Setup_WhenCalled_InvokesJsonBenchSetup()
// //     {
// //         // Arrange
// //         // Create an instance of ParlotBenchmarks.
// //         var benchmarksInstance = new ParlotBenchmarks();
// //         // Create a mock for the JsonBench dependency.
// //         var mockJsonBench = new Mock<JsonBench>();
// //         mockJsonBench.Setup(x => x.Setup()).Verifiable();
// //         // Replace the private readonly _jsonBench field with the mock instance using reflection.
// //         var jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         jsonBenchField.SetValue(benchmarksInstance, mockJsonBench.Object);
// //         // Act
// //         benchmarksInstance.Setup();
// //         // Assert
// //         mockJsonBench.Verify(x => x.Setup(), Times.Once, "Expected JsonBench.Setup() to be called exactly once from ParlotBenchmarks.Setup().");
// //     }
// 
//     /// <summary>
//     /// Tests that the CreateCompiledSmallParser method returns a non-null parser instance.
//     /// </summary>
//     [Fact]
//     public void CreateCompiledSmallParser_WhenCalled_ReturnsNonNullParser()
//     {
//         // Arrange
//         var benchmarks = new ParlotBenchmarks();
//         // Act
//         var parser = benchmarks.CreateCompiledSmallParser();
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the parser returned by CreateCompiledSmallParser successfully parses valid input characters.
//     /// </summary>
//     /// <param name = "input">The valid input string containing one of the supported characters.</param>
//     /// <param name = "expected">The expected parsed character.</param>
// //     [Theory] [Error] (67-36)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.Parser<char>' to 'Parlot.Fluent.Parsers.Parser<char>'
// //     [InlineData("a", 'a')]
// //     [InlineData("b", 'b')]
// //     [InlineData("v", 'v')]
// //     [InlineData("d", 'd')]
// //     public void CreateCompiledSmallParser_WithValidInput_ReturnsExpectedCharacter(string input, char expected)
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         var parser = benchmarks.CreateCompiledSmallParser();
// //         // Act
// //         char result = InvokeParser(parser, input);
// //         // Assert
// //         Assert.Equal(expected, result);
// //     }
// 
//     /// <summary>
//     /// Tests that the parser returned by CreateCompiledSmallParser throws an exception when provided invalid input.
//     /// </summary>
// //     [Fact] [Error] (83-53)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.Parser<char>' to 'Parlot.Fluent.Parsers.Parser<char>'
// //     public void CreateCompiledSmallParser_WithInvalidInput_ThrowsException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         var parser = benchmarks.CreateCompiledSmallParser();
// //         string input = "z";
// //         // Act & Assert
// //         Assert.Throws<Exception>(() => InvokeParser(parser, input));
// //     }
// 
//     /// <summary>
//     /// A helper method to simulate invoking the parser with an input string.
//     /// Assumes that the parser has a method to process input strings and yield a parsed character.
//     /// In an actual implementation, this method should invoke the parsing functionality of the parser.
//     /// </summary>
//     /// <param name = "parser">The parser instance derived from CreateCompiledSmallParser.</param>
//     /// <param name = "input">The input string to parse.</param>
//     /// <returns>The parsed character if the input is valid.</returns>
//     /// <exception cref = "Exception">Thrown when the input does not match any of the expected characters.</exception>
// //     private char InvokeParser(Parsers.Parser<char> parser, string input) [Error] (95-39)CS0426 The type name 'Parser<>' does not exist in the type 'Parsers'
// //     {
// //         if (string.IsNullOrEmpty(input))
// //         {
// //             throw new ArgumentException("Input must be non-empty.", nameof(input));
// //         }
// // 
// //         // Dummy implementation to simulate parser behavior:
// //         // The actual parser would process the input string.
// //         char firstChar = input[0];
// //         if (firstChar == 'a' || firstChar == 'b' || firstChar == 'v' || firstChar == 'd')
// //         {
// //             return firstChar;
// //         }
// // 
// //         throw new Exception("Parsing failed: invalid input.");
// //     }
// 
//     /// <summary>
//     /// Tests that <see cref = "ParlotBenchmarks.CreateCompiledExpressionParser"/> returns a non-null instance.
//     /// Arrange: Instantiate the <see cref = "ParlotBenchmarks"/> class.
//     /// Act: Call the CreateCompiledExpressionParser method.
//     /// Assert: Verify that the returned parser is not null.
//     /// </summary>
//     [Fact]
//     public void CreateCompiledExpressionParser_WhenCalled_ReturnsNonNullParser()
//     {
//         // Arrange
//         var benchmarks = new ParlotBenchmarks();
//         // Act
//         Parser<Expression> parser = benchmarks.CreateCompiledExpressionParser();
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that <see cref = "ParlotBenchmarks.CreateCompiledExpressionParser"/> returns an instance of the expected type.
//     /// Arrange: Instantiate the <see cref = "ParlotBenchmarks"/> class.
//     /// Act: Call the CreateCompiledExpressionParser method.
//     /// Assert: Verify that the returned parser is of type <see cref = "Parser{Expression}"/>.
//     /// </summary>
//     [Fact]
//     public void CreateCompiledExpressionParser_WhenCalled_ReturnsParserOfExpressionType()
//     {
//         // Arrange
//         var benchmarks = new ParlotBenchmarks();
//         // Act
//         var parser = benchmarks.CreateCompiledExpressionParser();
//         // Assert
//         Assert.IsType<Parser<Expression>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the CursorMatchHello method returns "hello" when the underlying parser is set up to return "hello".
//     /// </summary>
//     [Fact]
//     public void CursorMatchHello_WhenUnderlyingParserReturnsHello_ReturnsHello()
//     {
//         // Arrange
//         var benchmarksInstance = new ParlotBenchmarks();
//         // Create a mock for Parser<string> and configure it to return "hello" when Parse is called with "hello".
//         var parserMock = new Mock<Parser<string>>();
//         parserMock.Setup(p => p.Parse("hello")).Returns("hello");
//         // Use reflection to set the private readonly field _matchStringExpression in the ParlotBenchmarks instance.
//         var benchmarksType = typeof(ParlotBenchmarks);
//         var fieldInfo = benchmarksType.GetField("_matchStringExpression", BindingFlags.Instance | BindingFlags.NonPublic);
//         if (fieldInfo == null)
//         {
//             throw new Exception("Field _matchStringExpression not found.");
//         }
// 
//         fieldInfo.SetValue(benchmarksInstance, parserMock.Object);
//         // Act
//         var result = benchmarksInstance.CursorMatchHello();
//         // Assert
//         Assert.Equal("hello", result);
//         parserMock.Verify(p => p.Parse("hello"), Times.Once);
//     }
// 
//     /// <summary>
//     /// Tests that the CursorMatchGoodbye method returns "goodbye" when the parser is configured to return "goodbye".
//     /// </summary>
// //     [Fact] [Error] (185-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (186-9)CS8602 Dereference of a possibly null reference.
// //     public void CursorMatchGoodbye_WhenParserReturnsGoodbye_ReturnsGoodbye()
// //     {
// //         // Arrange: Create an instance of ParlotBenchmarks and inject a mock parser with expected behavior.
// //         var benchmarks = new ParlotBenchmarks();
// //         var mockParser = new Mock<Parser<string>>();
// //         mockParser.Setup(p => p.Parse("goodbye")).Returns("goodbye");
// //         // Use reflection to set the private readonly field _matchStringExpression to the mock.
// //         FieldInfo field = typeof(ParlotBenchmarks).GetField("_matchStringExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         field.SetValue(benchmarks, mockParser.Object);
// //         // Optionally call the Setup method if needed.
// //         benchmarks.Setup();
// //         // Act: Call the method under test.
// //         string result = benchmarks.CursorMatchGoodbye();
// //         // Assert: Verify that the returned result matches the expected output.
// //         Assert.Equal("goodbye", result);
// //     }
// 
//     /// <summary>
//     /// Tests that the CursorMatchGoodbye method propagates exceptions thrown by the parser.
//     /// </summary>
// //     [Fact] [Error] (207-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (208-9)CS8602 Dereference of a possibly null reference.
// //     public void CursorMatchGoodbye_WhenParserThrowsException_PropagatesException()
// //     {
// //         // Arrange: Create an instance of ParlotBenchmarks and inject a mock parser that throws an exception.
// //         var benchmarks = new ParlotBenchmarks();
// //         var expectedException = new InvalidOperationException("Test exception");
// //         var mockParser = new Mock<Parser<string>>();
// //         mockParser.Setup(p => p.Parse("goodbye")).Throws(expectedException);
// //         // Use reflection to set the private readonly field _matchStringExpression to the mock.
// //         FieldInfo field = typeof(ParlotBenchmarks).GetField("_matchStringExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         field.SetValue(benchmarks, mockParser.Object);
// //         // Optionally call the Setup method if needed.
// //         benchmarks.Setup();
// //         // Act & Assert: Verify that calling CursorMatchGoodbye propagates the expected exception.
// //         var exception = Assert.Throws<InvalidOperationException>(() => benchmarks.CursorMatchGoodbye());
// //         Assert.Equal(expectedException.Message, exception.Message);
// //     }
// 
//     /// <summary>
//     /// A fake parser implementation for testing purposes.
//     /// Inherits from <see cref = "Parser{T}"/> and uses a delegate to perform parsing.
//     /// </summary>
// //     private class FakeStringParser : Parser<string> [Error] (220-19)CS0534 'ParlotBenchmarksTests.FakeStringParser' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)'
// //     {
// //         private readonly Func<string, string> _parseFunc;
// //         /// <summary>
// //         /// Initializes a new instance of the <see cref = "FakeStringParser"/> class.
// //         /// </summary>
// //         /// <param name = "parseFunc">A delegate that defines the parsing logic.</param>
// //         public FakeStringParser(Func<string, string> parseFunc)
// //         {
// //             _parseFunc = parseFunc;
// //         }
// // 
// //         /// <summary>
// //         /// Parses the given input string using the provided delegate.
// //         /// </summary>
// //         /// <param name = "input">The input string to parse.</param>
// //         /// <returns>The result of the parsing operation.</returns>
// //         public override string Parse(string input) [Error] (237-32)CS0506 'ParlotBenchmarksTests.FakeStringParser.Parse(string)': cannot override inherited member 'Parser<string>.Parse(string)' because it is not marked virtual, abstract, or override
// //         {
// //             return _parseFunc(input);
// //         }
//     }
// 
//     /// <summary>
//     /// Tests that the CursorMatchNone method returns the expected output when the parser successfully parses the input.
//     /// </summary>
// //     [Fact] [Error] (263-31)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void CursorMatchNone_WithValidParser_ReturnsExpectedString()
// //     {
// //         // Arrange
// //         // Create a fake parser that returns "expected" when the specific input "hellllo" is provided.
// //         var fakeParser = new FakeStringParser(input =>
// //         {
// //             if (input == "hellllo")
// //             {
// //                 return "expected";
// //             }
// // 
// //             return "unexpected";
// //         });
// //         // Instantiate the benchmarks class.
// //         var benchmarks = new ParlotBenchmarks();
// //         // Use reflection to set the private readonly field _matchStringExpression with our fake parser.
// //         FieldInfo fieldInfo = typeof(ParlotBenchmarks).GetField("_matchStringExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (fieldInfo == null)
// //         {
// //             throw new InvalidOperationException("Field '_matchStringExpression' not found.");
// //         }
// // 
// //         fieldInfo.SetValue(benchmarks, fakeParser);
// //         // Act
// //         string result = benchmarks.CursorMatchNone();
// //         // Assert
// //         Assert.Equal("expected", result);
// //     }
// 
//     /// <summary>
//     /// Tests that the CursorMatchNone method propagates exceptions thrown by the underlying parser.
//     /// </summary>
// //     [Fact] [Error] (291-31)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void CursorMatchNone_WhenParserThrows_PropagatesException()
// //     {
// //         // Arrange
// //         // Create a fake parser that throws an InvalidOperationException when Parse is called.
// //         var fakeParser = new FakeStringParser(input =>
// //         {
// //             throw new InvalidOperationException("Parser failure");
// //         });
// //         // Instantiate the benchmarks class.
// //         var benchmarks = new ParlotBenchmarks();
// //         // Use reflection to set the private readonly field _matchStringExpression with our fake parser.
// //         FieldInfo fieldInfo = typeof(ParlotBenchmarks).GetField("_matchStringExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (fieldInfo == null)
// //         {
// //             throw new InvalidOperationException("Field '_matchStringExpression' not found.");
// //         }
// // 
// //         fieldInfo.SetValue(benchmarks, fakeParser);
// //         // Act & Assert
// //         var exception = Assert.Throws<InvalidOperationException>(() => benchmarks.CursorMatchNone());
// //         Assert.Equal("Parser failure", exception.Message);
// //     }
// 
//     /// <summary>
//     /// Parses the given string input.
//     /// Returns 'd' if the input is "d", otherwise throws an exception.
//     /// </summary>
//     /// <param name = "input">The string to parse.</param>
//     /// <returns>The parsed character.</returns>
// //     public override char Parse(string input) [Error] (309-26)CS0115 'ParlotBenchmarksTests.Parse(string)': no suitable method found to override
// //     {
// //         if (input == "d")
// //         {
// //             return 'd';
// //         }
// // 
// //         throw new ArgumentException("Unexpected input");
// //     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace_0 method when the _whitespaceExpression field is properly set.
//     /// It arranges a custom parser that returns a known non-default character and verifies that the method returns the expected value.
//     /// </summary>
// //     [Fact] [Error] (329-44)CS1660 Cannot convert lambda expression to type 'Parser<char>' because it is not a delegate type [Error] (330-37)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void SkipWhiteSpace_0_HappyPath_ReturnsExpectedCharacter()
// //     {
// //         // Arrange: Create an instance of ParlotBenchmarks and set the _whitespaceExpression field via reflection.
// //         var benchmarks = new ParlotBenchmarks();
// //         // Set up a dummy parser that ignores the ParseContext and returns a predetermined character.
// //         Parser<char> dummyParser = context => 'w';
// //         FieldInfo whitespaceField = typeof(ParlotBenchmarks).GetField("_whitespaceExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (whitespaceField == null)
// //         {
// //             throw new InvalidOperationException("The field '_whitespaceExpression' was not found.");
// //         }
// // 
// //         whitespaceField.SetValue(benchmarks, dummyParser);
// //         // Act: Call the method under test.
// //         char result = benchmarks.SkipWhiteSpace_0();
// //         // Assert: Verify that the returned character is the one supplied by the dummy parser.
// //         Assert.Equal('w', result);
// //     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace_0 method when the _whitespaceExpression field is not set (null).
//     /// This simulates an exceptional scenario where the dependency was not initialized. 
//     /// The test verifies that a NullReferenceException is thrown.
//     /// </summary>
// //     [Fact] [Error] (353-37)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void SkipWhiteSpace_0_NullWhitespaceExpression_ThrowsNullReferenceException()
// //     {
// //         // Arrange: Create an instance of ParlotBenchmarks and set the _whitespaceExpression field to null using reflection.
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo whitespaceField = typeof(ParlotBenchmarks).GetField("_whitespaceExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (whitespaceField == null)
// //         {
// //             throw new InvalidOperationException("The field '_whitespaceExpression' was not found.");
// //         }
// // 
// //         whitespaceField.SetValue(benchmarks, null);
// //         // Act & Assert: Expect a NullReferenceException when invoking the method.
// //         Assert.Throws<NullReferenceException>(() => benchmarks.SkipWhiteSpace_0());
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_1 returns the expected character when the parser returns 'a'.
//     /// </summary>
// //     [Fact] [Error] (376-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (377-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpace_1_HappyPath_ReturnsExpectedCharacter()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         var mockParser = new Mock<Parser<char>>();
// //         // Setup the mock parser to return 'a' when Parse is called with any ParseContext.
// //         mockParser.Setup(p => p.Parse(It.IsAny<ParseContext>())).Returns('a');
// //         // Use reflection to set the private _whitespaceExpression field.
// //         FieldInfo fieldInfo = typeof(ParlotBenchmarks).GetField("_whitespaceExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fieldInfo.SetValue(benchmarks, mockParser.Object);
// //         // Act
// //         char result = benchmarks.SkipWhiteSpace_1();
// //         // Assert
// //         Assert.Equal('a', result);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_1 returns the default character ('\0') when the parser returns default.
//     /// </summary>
// //     [Fact] [Error] (395-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (396-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpace_1_WhenParserReturnsDefault_ReturnsDefaultChar()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         var mockParser = new Mock<Parser<char>>();
// //         // Setup the mock parser to return default(char) when Parse is called.
// //         mockParser.Setup(p => p.Parse(It.IsAny<ParseContext>())).Returns(default(char));
// //         FieldInfo fieldInfo = typeof(ParlotBenchmarks).GetField("_whitespaceExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fieldInfo.SetValue(benchmarks, mockParser.Object);
// //         // Act
// //         char result = benchmarks.SkipWhiteSpace_1();
// //         // Assert
// //         Assert.Equal(default(char), result);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_1 propagates exceptions thrown by the parser.
//     /// </summary>
// //     [Fact] [Error] (414-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (415-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpace_1_WhenParserThrowsException_PropagatesException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         var mockParser = new Mock<Parser<char>>();
// //         // Setup the mock parser to throw an exception when Parse is called.
// //         mockParser.Setup(p => p.Parse(It.IsAny<ParseContext>())).Throws(new Exception("Test exception"));
// //         FieldInfo fieldInfo = typeof(ParlotBenchmarks).GetField("_whitespaceExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fieldInfo.SetValue(benchmarks, mockParser.Object);
// //         // Act & Assert
// //         Exception ex = Assert.Throws<Exception>(() => benchmarks.SkipWhiteSpace_1());
// //         Assert.Equal("Test exception", ex.Message);
// //     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace_1 throws a NullReferenceException when the _whitespaceExpression dependency is null.
//     /// </summary>
// //     [Fact] [Error] (429-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (431-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpace_1_WhenWhitespaceExpressionNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo fieldInfo = typeof(ParlotBenchmarks).GetField("_whitespaceExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         // Explicitly set the _whitespaceExpression field to null.
// //         fieldInfo.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.SkipWhiteSpace_1());
// //     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace_10 method to ensure that it correctly invokes the whitespace parser
//     /// and returns the expected character. The test injects a mocked Parser<char> into the benchmark instance
//     /// and validates that the parser receives a ParseContext constructed with the expected scanner input.
//     /// </summary>
// //     [Fact] [Error] (448-40)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (452-9)CS8602 Dereference of a possibly null reference.
// //     public void SkipWhiteSpace_10_WithValidInput_ReturnsExpectedCharacter()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         // Create a mock for Parser<char> to simulate whitespace parsing behavior.
// //         var mockParser = new Mock<Parser<char>>();
// //         ParseContext capturedContext = null;
// //         mockParser.Setup(p => p.Parse(It.IsAny<ParseContext>())).Callback<ParseContext>(ctx => capturedContext = ctx).Returns('a');
// //         // Use reflection to inject the mock into the private readonly _whitespaceExpression field.
// //         var fieldInfo = typeof(ParlotBenchmarks).GetField("_whitespaceExpression", BindingFlags.Instance | BindingFlags.NonPublic);
// //         fieldInfo.SetValue(benchmarks, mockParser.Object);
// //         // Act
// //         char result = benchmarks.SkipWhiteSpace_10();
// //         // Assert
// //         Assert.Equal('a', result);
// //         Assert.NotNull(capturedContext);
// //         // Retrieve the Scanner from the captured ParseContext.
// //         var scannerProperty = capturedContext.GetType().GetProperty("Scanner", BindingFlags.Public | BindingFlags.Instance);
// //         Assert.NotNull(scannerProperty);
// //         var scanner = scannerProperty.GetValue(capturedContext);
// //         Assert.NotNull(scanner);
// //         // Retrieve the text from the Scanner.
// //         var textProperty = scanner.GetType().GetProperty("Text", BindingFlags.Public | BindingFlags.Instance);
// //         Assert.NotNull(textProperty);
// //         var scannerText = textProperty.GetValue(scanner) as string;
// //         Assert.Equal("          a", scannerText);
// //     }
// 
//     /// <summary>
//     /// Tests the DecodeStringWithoutEscapes method to ensure that when the input string contains no escape sequences,
//     /// the method returns a TextSpan that matches the expected result computed via Character.DecodeString.
//     /// </summary>
// //     [Fact] [Error] (481-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (483-24)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (485-47)CS8604 Possible null reference argument for parameter 's' in 'TextSpan Character.DecodeString(string s)'.
// //     public void DecodeStringWithoutEscapes_HappyPath_ReturnsExpectedTextSpan()
// //     {
// //         // Arrange: Create an instance of the benchmark class.
// //         var benchmarks = new ParlotBenchmarks();
// //         // Retrieve the constant _stringWithoutEscapes value via reflection.
// //         // This field is a private constant, so we obtain its value to use for expected output calculation.
// //         FieldInfo fieldInfo = typeof(ParlotBenchmarks).GetField("_stringWithoutEscapes", BindingFlags.NonPublic | BindingFlags.Static);
// //         Assert.NotNull(fieldInfo);
// //         string input = fieldInfo.GetValue(null) as string;
// //         // If input is null, assume Character.DecodeString will process it accordingly.
// //         var expected = Character.DecodeString(input);
// //         // Act: Invoke the DecodeStringWithoutEscapes method.
// //         var actual = benchmarks.DecodeStringWithoutEscapes();
// //         // Assert: Verify that the actual result matches the expected TextSpan.
// //         Assert.Equal(expected, actual);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "ParlotBenchmarks.DecodeStringWithEscapes"/> method to ensure it correctly decodes 
//     /// a string containing escape sequences by delegating to <see cref = "Character.DecodeString"/>.
//     /// The test retrieves the expected string input from the private constant field using reflection.
//     /// </summary>
// //     [Fact] [Error] (503-27)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (504-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DecodeStringWithEscapes_WhenCalled_ReturnsExpectedTextSpan()
// //     {
// //         // Arrange
// //         var benchmarkInstance = new ParlotBenchmarks();
// //         // Retrieve the private constant _stringWithEscapes from ParlotBenchmarks using reflection.
// //         FieldInfo field = typeof(ParlotBenchmarks).GetField("_stringWithEscapes", BindingFlags.NonPublic | BindingFlags.Static);
// //         string stringWithEscapes = field?.GetRawConstantValue() as string;
// //         // If the constant is not set, use a default test value.
// //         if (stringWithEscapes == null)
// //         {
// //             stringWithEscapes = "Test\\nString\\tWithEscapes";
// //         }
// // 
// //         // Calculate the expected result using the underlying decoding logic.
// //         var expected = Character.DecodeString(stringWithEscapes);
// //         // Act
// //         var result = benchmarkInstance.DecodeStringWithEscapes();
// //         // Assert
// //         Assert.Equal(expected, result);
// //     }
// 
//     /// <summary>
//     /// Tests that the ExpressionCompiledSmall method returns the expected Expression when the dependency is properly initialized.
//     /// The test sets up a mocked ExprBench instance, assigns it to the private field and confirms the method returns the expected result.
//     /// </summary>
// //     [Fact] [Error] (527-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (531-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (532-9)CS8602 Dereference of a possibly null reference.
// //     public void ExpressionCompiledSmall_WithValidExprBench_ReturnsExpectedExpression()
// //     {
// //         // Arrange
// //         var expectedExpression = new Expression();
// //         var mockExprBench = new Mock<ExprBench>();
// //         mockExprBench.Setup(m => m.ParlotCompiledSmall()).Returns(expectedExpression);
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         exprBenchField.SetValue(benchmarks, mockExprBench.Object);
// //         // Act
// //         Expression actualExpression = benchmarks.ExpressionCompiledSmall();
// //         // Assert
// //         Assert.Same(expectedExpression, actualExpression);
// //     }
// 
//     /// <summary>
//     /// Tests that the ExpressionCompiledSmall method throws a NullReferenceException when the _exprBench dependency is not initialized.
//     /// The test explicitly sets the private field to null and confirms that calling the method results in a NullReferenceException.
//     /// </summary>
// //     [Fact] [Error] (548-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (549-9)CS8602 Dereference of a possibly null reference.
// //     public void ExpressionCompiledSmall_WithNullExprBench_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         exprBenchField.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.ExpressionCompiledSmall());
// //     }
// 
//     /// <summary>
//     /// Tests the ExpressionFluentSmall method when the underlying _exprBench dependency returns a valid Expression.
//     /// Expected outcome is that ExpressionFluentSmall returns the same Expression instance provided by _exprBench.ParlotFluentSmall.
//     /// </summary>
// //     [Fact] [Error] (562-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (567-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void ExpressionFluentSmall_WhenExprBenchReturnsValidExpression_ReturnsExpectedExpression()
// //     {
// //         // Arrange
// //         var expectedExpression = new Expression();
// //         var exprBenchMock = new Mock<ExprBench>();
// //         exprBenchMock.Setup(x => x.ParlotFluentSmall()).Returns(expectedExpression);
// //         var benchmarksInstance = new ParlotBenchmarks();
// //         // Inject the mocked _exprBench dependency via reflection.
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (exprBenchField == null)
// //         {
// //             throw new Exception("Unable to find the _exprBench field in ParlotBenchmarks class.");
// //         }
// // 
// //         exprBenchField.SetValue(benchmarksInstance, exprBenchMock.Object);
// //         // Act
// //         Expression actualExpression = benchmarksInstance.ExpressionFluentSmall();
// //         // Assert
// //         Assert.NotNull(actualExpression);
// //         Assert.Same(expectedExpression, actualExpression);
// //         exprBenchMock.Verify(x => x.ParlotFluentSmall(), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests the ExpressionFluentSmall method when the _exprBench dependency is null.
//     /// Expected outcome is that accessing _exprBench will result in a NullReferenceException.
//     /// </summary>
// //     [Fact] [Error] (592-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void ExpressionFluentSmall_WhenExprBenchIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarksInstance = new ParlotBenchmarks();
// //         // Force the _exprBench field to be null via reflection.
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (exprBenchField == null)
// //         {
// //             throw new Exception("Unable to find the _exprBench field in ParlotBenchmarks class.");
// //         }
// // 
// //         exprBenchField.SetValue(benchmarksInstance, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() =>
// //         {
// //             var _ = benchmarksInstance.ExpressionFluentSmall();
// //         });
// //     }
// 
//     /// <summary>
//     /// Tests the ExpressionRawBig method to ensure it returns the expected Expression object
//     /// provided by the _exprBench dependency.
//     /// </summary>
// //     [Fact] [Error] (615-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (622-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void ExpressionRawBig_WhenExprBenchReturnsValidExpression_ReturnsExpectedExpression()
// //     {
// //         // Arrange
// //         // Create a dummy Expression object that we expect to be returned.
// //         var expectedExpression = new Expression();
// //         // Create a mock for the dependency 'ExprBench' and set up its ParlotRawBig method.
// //         var mockExprBench = new Mock<ExprBench>();
// //         mockExprBench.Setup(eb => eb.ParlotRawBig()).Returns(expectedExpression);
// //         // Instantiate the ParlotBenchmarks class.
// //         var benchmarks = new ParlotBenchmarks();
// //         // Using reflection to inject the mocked dependency into the private readonly field '_exprBench'.
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (exprBenchField == null)
// //         {
// //             throw new Exception("The field '_exprBench' was not found on ParlotBenchmarks.");
// //         }
// // 
// //         exprBenchField.SetValue(benchmarks, mockExprBench.Object);
// //         // Act
// //         Expression actualExpression = benchmarks.ExpressionRawBig();
// //         // Assert
// //         Assert.Equal(expectedExpression, actualExpression);
// //     }
// 
//     /// <summary>
//     /// Tests the ExpressionCompiledBig method to ensure it returns the expected Expression
//     /// when the internal _exprBench dependency returns a valid Expression.
//     /// </summary>
// //     [Fact] [Error] (643-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (648-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void ExpressionCompiledBig_HappyPath_ReturnsExpectedExpression()
// //     {
// //         // Arrange
// //         var expectedExpression = new Expression(); // Assumes Expression has a parameterless constructor.
// //         var exprBenchMock = new Mock<ExprBench>();
// //         exprBenchMock.Setup(x => x.ParlotCompiledBig()).Returns(expectedExpression);
// //         var benchmarks = new ParlotBenchmarks();
// //         // Inject the fake dependency using reflection.
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         Assert.NotNull(exprBenchField); // Ensure the field exists.
// //         exprBenchField.SetValue(benchmarks, exprBenchMock.Object);
// //         // Act
// //         Expression result = benchmarks.ExpressionCompiledBig();
// //         // Assert
// //         Assert.Equal(expectedExpression, result);
// //     }
// 
//     /// <summary>
//     /// Tests the ExpressionCompiledBig method to ensure that when the internal _exprBench dependency
//     /// is null, a NullReferenceException is thrown.
//     /// </summary>
// //     [Fact] [Error] (667-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void ExpressionCompiledBig_WhenExprBenchIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         // Set the _exprBench field to null using reflection.
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         Assert.NotNull(exprBenchField); // Ensure the field exists.
// //         exprBenchField.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.ExpressionCompiledBig());
// //     }
// 
//     /// <summary>
//     /// Tests the ExpressionFluentBig method to verify that it returns the expected Expression when _exprBench.ParlotFluentBig returns a valid instance.
//     /// </summary>
// //     [Fact] [Error] (681-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (686-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (687-9)CS8602 Dereference of a possibly null reference.
// //     public void ExpressionFluentBig_WhenExprBenchReturnsValidExpression_ReturnsExpectedExpression()
// //     {
// //         // Arrange
// //         var expectedExpression = new Expression(); // Assuming Expression has a public parameterless constructor.
// //         var mockExprBench = new Mock<ExprBench>();
// //         mockExprBench.Setup(x => x.ParlotFluentBig()).Returns(expectedExpression);
// //         var benchmarksInstance = new ParlotBenchmarks();
// //         // Inject the mock into the private readonly _exprBench field using reflection.
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         exprBenchField.SetValue(benchmarksInstance, mockExprBench.Object);
// //         // Act
// //         Expression result = benchmarksInstance.ExpressionFluentBig();
// //         // Assert
// //         Assert.Equal(expectedExpression, result);
// //     }
// 
//     /// <summary>
//     /// Tests the ExpressionFluentBig method to verify that it returns null when _exprBench.ParlotFluentBig returns null.
//     /// </summary>
// //     [Fact] [Error] (701-41)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (703-63)CS8604 Possible null reference argument for parameter 'value' in 'IReturnsResult<ExprBench> IReturns<ExprBench, Expression>.Returns(Expression value)'. [Error] (706-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (707-9)CS8602 Dereference of a possibly null reference.
// //     public void ExpressionFluentBig_WhenExprBenchReturnsNull_ReturnsNull()
// //     {
// //         // Arrange
// //         Expression expectedExpression = null;
// //         var mockExprBench = new Mock<ExprBench>();
// //         mockExprBench.Setup(x => x.ParlotFluentBig()).Returns(expectedExpression);
// //         var benchmarksInstance = new ParlotBenchmarks();
// //         // Inject the mock into the private readonly _exprBench field using reflection.
// //         FieldInfo exprBenchField = typeof(ParlotBenchmarks).GetField("_exprBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         exprBenchField.SetValue(benchmarksInstance, mockExprBench.Object);
// //         // Act
// //         Expression result = benchmarksInstance.ExpressionFluentBig();
// //         // Assert
// //         Assert.Null(result);
// //     }
// 
//     /// <summary>
//     /// Tests that the BigJsonCompiled method returns the expected IJson instance when the dependency returns a valid object.
//     /// This verifies the happy path where _jsonBench.BigJson_ParlotCompiled() produces a correct non-null IJson.
//     /// </summary>
// //     [Fact] [Error] (726-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (727-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJsonCompiled_HappyPath_ReturnsExpectedIJson()
// //     {
// //         // Arrange
// //         var expectedIJson = new DummyJson();
// //         var jsonBenchMock = new Mock<JsonBench>();
// //         jsonBenchMock.Setup(jb => jb.BigJson_ParlotCompiled()).Returns(expectedIJson);
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         jsonBenchField.SetValue(benchmarks, jsonBenchMock.Object);
// //         // Act
// //         IJson result = benchmarks.BigJsonCompiled();
// //         // Assert
// //         Assert.Equal(expectedIJson, result);
// //     }
// 
//     /// <summary>
//     /// Tests that the BigJsonCompiled method returns null when the dependency returns null.
//     /// This verifies that a null value from _jsonBench.BigJson_ParlotCompiled() is properly returned.
//     /// </summary>
// //     [Fact] [Error] (743-72)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (743-72)CS8625 Cannot convert null literal to non-nullable reference type. [Error] (745-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (746-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJsonCompiled_DependencyReturnsNull_ReturnsNull()
// //     {
// //         // Arrange
// //         var jsonBenchMock = new Mock<JsonBench>();
// //         jsonBenchMock.Setup(jb => jb.BigJson_ParlotCompiled()).Returns((IJson)null);
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         jsonBenchField.SetValue(benchmarks, jsonBenchMock.Object);
// //         // Act
// //         IJson result = benchmarks.BigJsonCompiled();
// //         // Assert
// //         Assert.Null(result);
// //     }
// 
//     /// <summary>
//     /// Tests that the BigJsonCompiled method throws a NullReferenceException when the dependency is null.
//     /// This verifies that the method does not guard against a null _jsonBench and that a proper exception is thrown.
//     /// </summary>
// //     [Fact] [Error] (762-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (763-9)CS8602 Dereference of a possibly null reference.
// //     public void BigJsonCompiled_DependencyIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         jsonBenchField.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.BigJsonCompiled());
// //     }
// 
//     /// <summary>
//     /// A dummy implementation of the IJson interface for testing purposes.
//     /// </summary>
//     private class DummyJson : IJson
//     {
//     // Implement IJson members as needed for testing.
//     // Assuming IJson does not require any specific behavior for these tests.
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "ParlotBenchmarks.DeepJson"/> method to ensure it returns the expected IJson instance 
//     /// when the underlying _jsonBench dependency returns a valid result.
//     /// This test arranges a mocked JsonBench instance to return a predetermined IJson object,
//     /// injects the mock via reflection, and asserts that the result of DeepJson is the same as expected.
//     /// </summary>
// //     [Fact] [Error] (795-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJson_WhenJsonBenchIsProper_ReturnsExpectedJson()
// //     {
// //         // Arrange
// //         var expectedJsonMock = new Mock<IJson>();
// //         IJson expectedJson = expectedJsonMock.Object;
// //         // Create a mock for JsonBench. Assumes that DeepJson_Parlot is overridable.
// //         var jsonBenchMock = new Mock<JsonBench>();
// //         jsonBenchMock.Setup(j => j.DeepJson_Parlot()).Returns(expectedJson);
// //         // Create the instance of ParlotBenchmarks.
// //         var benchmarks = new ParlotBenchmarks();
// //         // Inject the mocked _jsonBench using reflection.
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (jsonBenchField == null)
// //         {
// //             throw new Exception("Field _jsonBench not found.");
// //         }
// // 
// //         jsonBenchField.SetValue(benchmarks, jsonBenchMock.Object);
// //         // Optionally ensure any additional setup is performed.
// //         benchmarks.Setup();
// //         // Act
// //         IJson result = benchmarks.DeepJson();
// //         // Assert
// //         Assert.Equal(expectedJson, result);
// //         jsonBenchMock.Verify(j => j.DeepJson_Parlot(), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "ParlotBenchmarks.DeepJson"/> method to ensure it throws a NullReferenceException 
//     /// when the underlying _jsonBench dependency is null.
//     /// This test sets the _jsonBench field to null using reflection and verifies that calling DeepJson causes an exception.
//     /// </summary>
// //     [Fact] [Error] (822-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJson_WhenJsonBenchIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         // Inject null into the private _jsonBench using reflection.
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (jsonBenchField == null)
// //         {
// //             throw new Exception("Field _jsonBench not found.");
// //         }
// // 
// //         jsonBenchField.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.DeepJson());
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJsonCompiled returns the expected IJson instance when _jsonBench.DeepJson_ParlotCompiled returns a valid IJson.
//     /// </summary>
// //     [Fact] [Error] (845-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJsonCompiled_DependencyReturnsValidIJson_ReturnsExpectedIJson()
// //     {
// //         // Arrange
// //         var expectedIJson = new FakeJson();
// //         var mockJsonBench = new Mock<JsonBench>();
// //         mockJsonBench.Setup(x => x.DeepJson_ParlotCompiled()).Returns(expectedIJson);
// //         var benchmarks = new ParlotBenchmarks();
// //         // Inject the mock into the private _jsonBench field via reflection.
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (jsonBenchField == null)
// //         {
// //             throw new InvalidOperationException("Field '_jsonBench' not found.");
// //         }
// // 
// //         jsonBenchField.SetValue(benchmarks, mockJsonBench.Object);
// //         // Act
// //         IJson actualIJson = benchmarks.DeepJsonCompiled();
// //         // Assert
// //         Assert.Equal(expectedIJson, actualIJson);
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJsonCompiled returns null when _jsonBench.DeepJson_ParlotCompiled returns null.
//     /// </summary>
// //     [Fact] [Error] (865-31)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (867-71)CS8604 Possible null reference argument for parameter 'value' in 'IReturnsResult<JsonBench> IReturns<JsonBench, IJson>.Returns(IJson value)'. [Error] (869-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJsonCompiled_DependencyReturnsNull_ReturnsNull()
// //     {
// //         // Arrange
// //         IJson expectedIJson = null;
// //         var mockJsonBench = new Mock<JsonBench>();
// //         mockJsonBench.Setup(x => x.DeepJson_ParlotCompiled()).Returns(expectedIJson);
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (jsonBenchField == null)
// //         {
// //             throw new InvalidOperationException("Field '_jsonBench' not found.");
// //         }
// // 
// //         jsonBenchField.SetValue(benchmarks, mockJsonBench.Object);
// //         // Act
// //         IJson actualIJson = benchmarks.DeepJsonCompiled();
// //         // Assert
// //         Assert.Null(actualIJson);
// //     }
// 
//     /// <summary>
//     /// Tests that DeepJsonCompiled propagates exceptions thrown by _jsonBench.DeepJson_ParlotCompiled.
//     /// </summary>
// //     [Fact] [Error] (893-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void DeepJsonCompiled_DependencyThrowsException_ThrowsException()
// //     {
// //         // Arrange
// //         var expectedException = new Exception("Test exception");
// //         var mockJsonBench = new Mock<JsonBench>();
// //         mockJsonBench.Setup(x => x.DeepJson_ParlotCompiled()).Throws(expectedException);
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         if (jsonBenchField == null)
// //         {
// //             throw new InvalidOperationException("Field '_jsonBench' not found.");
// //         }
// // 
// //         jsonBenchField.SetValue(benchmarks, mockJsonBench.Object);
// //         // Act & Assert
// //         Exception actualException = Assert.Throws<Exception>(() => benchmarks.DeepJsonCompiled());
// //         Assert.Equal(expectedException, actualException);
// //     }
// 
//     /// <summary>
//     /// Tests that the LongJson method returns the expected IJson object when the dependency returns a non-null value.
//     /// </summary>
// //     [Fact] [Error] (917-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void LongJson_WhenDependencyReturnsExpectedValue_ReturnsExpectedIJson()
// //     {
// //         // Arrange
// //         var expectedIJson = new Mock<IJson>().Object;
// //         var jsonBenchMock = new Mock<JsonBench>();
// //         jsonBenchMock.Setup(jb => jb.LongJson_Parlot()).Returns(expectedIJson);
// //         var benchmarks = new ParlotBenchmarks();
// //         // Use reflection to set the private readonly _jsonBench field to our mocked instance.
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (jsonBenchField == null)
// //         {
// //             throw new Exception("Field '_jsonBench' not found in ParlotBenchmarks.");
// //         }
// // 
// //         jsonBenchField.SetValue(benchmarks, jsonBenchMock.Object);
// //         // Act
// //         IJson result = benchmarks.LongJson();
// //         // Assert
// //         Assert.Same(expectedIJson, result);
// //         jsonBenchMock.Verify(jb => jb.LongJson_Parlot(), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests that the LongJson method returns null when the dependency returns null.
//     /// </summary>
// //     [Fact] [Error] (939-65)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (939-65)CS8625 Cannot convert null literal to non-nullable reference type. [Error] (942-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void LongJson_WhenDependencyReturnsNull_ReturnsNull()
// //     {
// //         // Arrange
// //         var jsonBenchMock = new Mock<JsonBench>();
// //         jsonBenchMock.Setup(jb => jb.LongJson_Parlot()).Returns((IJson)null);
// //         var benchmarks = new ParlotBenchmarks();
// //         // Use reflection to set the private readonly _jsonBench field to our mocked instance.
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (jsonBenchField == null)
// //         {
// //             throw new Exception("Field '_jsonBench' not found in ParlotBenchmarks.");
// //         }
// // 
// //         jsonBenchField.SetValue(benchmarks, jsonBenchMock.Object);
// //         // Act
// //         IJson result = benchmarks.LongJson();
// //         // Assert
// //         Assert.Null(result);
// //         jsonBenchMock.Verify(jb => jb.LongJson_Parlot(), Times.Once);
// //     }
// 
//     /// <summary>
//     /// Tests that the LongJson method throws a NullReferenceException when the _jsonBench dependency is null.
//     /// </summary>
// //     [Fact] [Error] (965-36)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void LongJson_WhenDependencyIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         // Use reflection to set the private readonly _jsonBench field to null.
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (jsonBenchField == null)
// //         {
// //             throw new Exception("Field '_jsonBench' not found in ParlotBenchmarks.");
// //         }
// // 
// //         jsonBenchField.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.LongJson());
// //     }
// 
//     /// <summary>
//     /// Tests that the LongJsonCompiled method returns the expected IJson instance when the underlying _jsonBench returns a valid IJson instance.
//     /// </summary>
//     [Fact]
//     public void LongJsonCompiled_WhenCalled_ReturnsExpectedIJson()
//     {
//         // Arrange
//         var expectedIJson = new Mock<IJson>().Object;
//         var jsonBenchMock = new Mock<JsonBench>();
//         jsonBenchMock.Setup(x => x.LongJson_ParlotCompiled()).Returns(expectedIJson);
//         // Create an instance of ParlotBenchmarks
//         var benchmarks = new ParlotBenchmarks();
//         // Use reflection to inject the mock into the private _jsonBench field.
//         var fieldInfo = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.NonPublic | BindingFlags.Instance);
//         if (fieldInfo == null)
//         {
//             throw new Exception("Field '_jsonBench' not found via reflection.");
//         }
// 
//         fieldInfo.SetValue(benchmarks, jsonBenchMock.Object);
//         // Act
//         IJson result = benchmarks.LongJsonCompiled();
//         // Assert
//         Assert.Equal(expectedIJson, result);
//         jsonBenchMock.Verify(x => x.LongJson_ParlotCompiled(), Times.Once);
//     }
// 
//     /// <summary>
//     /// Tests that the LongJsonCompiled method returns null when the underlying _jsonBench returns null.
//     /// </summary>
// //     [Fact] [Error] (1011-71)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1011-71)CS8625 Cannot convert null literal to non-nullable reference type.
// //     public void LongJsonCompiled_WhenJsonBenchReturnsNull_ReturnsNull()
// //     {
// //         // Arrange
// //         var jsonBenchMock = new Mock<JsonBench>();
// //         jsonBenchMock.Setup(x => x.LongJson_ParlotCompiled()).Returns((IJson)null);
// //         // Create an instance of ParlotBenchmarks
// //         var benchmarks = new ParlotBenchmarks();
// //         // Use reflection to inject the mock into the private _jsonBench field.
// //         var fieldInfo = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         if (fieldInfo == null)
// //         {
// //             throw new Exception("Field '_jsonBench' not found via reflection.");
// //         }
// // 
// //         fieldInfo.SetValue(benchmarks, jsonBenchMock.Object);
// //         // Act
// //         IJson result = benchmarks.LongJsonCompiled();
// //         // Assert
// //         Assert.Null(result);
// //         jsonBenchMock.Verify(x => x.LongJson_ParlotCompiled(), Times.Once);
// //     }
// 
//     /// <summary>
//     /// A fake implementation of IJson for testing purposes.
//     /// </summary>
//     private class FakeJson : IJson
//     {
//     // This class intentionally left blank as a dummy implementation.
//     }
// 
//     /// <summary>
//     /// A fake implementation of the dependency used by ParlotBenchmarks.
//     /// It provides a configurable return value for the WideJson_Parlot method.
//     /// </summary>
//     private class FakeJsonBench
//     {
//         private readonly IJson _returnedJson;
//         public FakeJsonBench(IJson returnedJson)
//         {
//             _returnedJson = returnedJson;
//         }
// 
//         /// <summary>
//         /// Returns the preconfigured IJson instance.
//         /// </summary>
//         /// <returns>The fake IJson object.</returns>
//         public IJson WideJson_Parlot()
//         {
//             return _returnedJson;
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the WideJson method returns the expected IJson object when the dependency is properly set.
//     /// </summary>
// //     [Fact] [Error] (1070-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void WideJson_HappyPath_ReturnsExpectedIJson()
// //     {
// //         // Arrange
// //         var expectedJson = new FakeJson();
// //         var fakeJsonBench = new FakeJsonBench(expectedJson);
// //         var benchmarks = new ParlotBenchmarks();
// //         // Use reflection to set the private readonly field _jsonBench to our fake dependency.
// //         FieldInfo field = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         Assert.NotNull(field);
// //         field.SetValue(benchmarks, fakeJsonBench);
// //         // Act
// //         IJson result = benchmarks.WideJson();
// //         // Assert
// //         Assert.Same(expectedJson, result);
// //     }
// 
//     /// <summary>
//     /// Tests that the WideJson method throws a NullReferenceException when the _jsonBench dependency is not set.
//     /// </summary>
// //     [Fact] [Error] (1088-27)CS8600 Converting null literal or possible null value to non-nullable type.
// //     public void WideJson_NullJsonBench_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         // Explicitly set the private readonly field _jsonBench to null using reflection.
// //         FieldInfo field = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.NonPublic | BindingFlags.Instance);
// //         Assert.NotNull(field);
// //         field.SetValue(benchmarks, null);
// //         // Act & Assert: Expect a NullReferenceException when _jsonBench is null.
// //         Assert.Throws<NullReferenceException>(() => benchmarks.WideJson());
// //     }
// 
//     /// <summary>
//     /// Tests the WideJsonCompiled method when JsonBench returns a valid IJson object.
//     /// Expected outcome is that the returned IJson is the same as provided by JsonBench.
//     /// </summary>
// //     [Fact] [Error] (1108-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1109-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJsonCompiled_WhenCalled_ReturnsJsonObject()
// //     {
// //         // Arrange
// //         var expectedJson = new Mock<IJson>().Object;
// //         var jsonBenchMock = new Mock<JsonBench>();
// //         jsonBenchMock.Setup(j => j.WideJson_ParlotCompiled()).Returns(expectedJson);
// //         var benchmarks = new ParlotBenchmarks();
// //         // Set the private _jsonBench field via reflection
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         jsonBenchField.SetValue(benchmarks, jsonBenchMock.Object);
// //         // Act
// //         var result = benchmarks.WideJsonCompiled();
// //         // Assert
// //         Assert.Equal(expectedJson, result);
// //     }
// 
//     /// <summary>
//     /// Tests the WideJsonCompiled method when JsonBench returns a null IJson object.
//     /// Expected outcome is that the method returns null.
//     /// </summary>
// //     [Fact] [Error] (1125-77)CS8603 Possible null reference return. [Error] (1127-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1128-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJsonCompiled_WhenJsonBenchReturnsNull_ReturnsNull()
// //     {
// //         // Arrange
// //         var jsonBenchMock = new Mock<JsonBench>();
// //         jsonBenchMock.Setup(j => j.WideJson_ParlotCompiled()).Returns(() => null);
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         jsonBenchField.SetValue(benchmarks, jsonBenchMock.Object);
// //         // Act
// //         var result = benchmarks.WideJsonCompiled();
// //         // Assert
// //         Assert.Null(result);
// //     }
// 
//     /// <summary>
//     /// Tests the WideJsonCompiled method when the _jsonBench dependency is not initialized.
//     /// Expected outcome is that a NullReferenceException is thrown.
//     /// </summary>
// //     [Fact] [Error] (1144-36)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (1146-9)CS8602 Dereference of a possibly null reference.
// //     public void WideJsonCompiled_WhenJsonBenchIsNull_ThrowsNullReferenceException()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         FieldInfo jsonBenchField = typeof(ParlotBenchmarks).GetField("_jsonBench", BindingFlags.Instance | BindingFlags.NonPublic);
// //         // Explicitly set _jsonBench to null to simulate missing dependency
// //         jsonBenchField.SetValue(benchmarks, null);
// //         // Act & Assert
// //         Assert.Throws<NullReferenceException>(() => benchmarks.WideJsonCompiled());
// //     }
// 
//     /// <summary>
//     /// Tests the CursorCtor method to ensure it returns a Cursor instance with the expected values.
//     /// <para>
//     /// This test verifies that the returned Cursor is not null, and that its properties 'Source' and 'Position'
//     /// match the expected values ("hello" and TextPosition.Start, respectively). Reflection is used to access
//     /// these properties since their definitions are not provided in the benchmark class.
//     /// </para>
//     /// </summary>
// //     [Fact] [Error] (1179-32)CS8602 Dereference of a possibly null reference.
// //     public void CursorCtor_WhenCalled_ReturnsCursorWithExpectedValues()
// //     {
// //         // Arrange
// //         var benchmarks = new ParlotBenchmarks();
// //         // Act
// //         var cursor = benchmarks.CursorCtor();
// //         // Assert
// //         Assert.NotNull(cursor);
// //         // Use reflection to verify the 'Source' property equals "hello"
// //         var cursorType = cursor.GetType();
// //         var sourceProperty = cursorType.GetProperty("Source");
// //         Assert.NotNull(sourceProperty);
// //         var sourceValue = sourceProperty.GetValue(cursor) as string;
// //         Assert.Equal("hello", sourceValue);
// //         // Use reflection to verify the 'Position' property equals TextPosition.Start
// //         var positionProperty = cursorType.GetProperty("Position");
// //         Assert.NotNull(positionProperty);
// //         var positionValue = positionProperty.GetValue(cursor);
// //         // Retrieve the expected TextPosition.Start value via reflection
// //         var textPositionType = positionValue.GetType();
// //         var startField = textPositionType.GetField("Start");
// //         Assert.NotNull(startField);
// //         var expectedPosition = startField.GetValue(null);
// //         Assert.Equal(expectedPosition, positionValue);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "ParlotBenchmarks.ScannerCtor"/> method to ensure it returns a non-null instance of <see cref = "Scanner"/>.
//     /// The test follows the Arrange-Act-Assert pattern: it creates an instance of ParlotBenchmarks, calls the ScannerCtor method,
//     /// and verifies that the returned instance is non-null and of type Scanner.
//     /// </summary>
//     [Fact]
//     public void ScannerCtor_WhenCalled_ReturnsNonNullScannerInstance()
//     {
//         // Arrange
//         var benchmarks = new ParlotBenchmarks();
//         // Act
//         Scanner scanner = benchmarks.ScannerCtor();
//         // Assert
//         Assert.NotNull(scanner);
//         Assert.IsType<Scanner>(scanner);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "ParlotBenchmarks.ParseContextCtor"/> method to ensure it returns a valid ParseContext instance when _scanner is properly set.
//     /// The test sets the private _scanner field using reflection to a valid Scanner instance then verifies that the returned ParseContext instance is not null.
//     /// </summary>
// //     [Fact] [Error] (1222-9)CS8602 Dereference of a possibly null reference.
// //     public void ParseContextCtor_WhenScannerIsSet_ReturnsParseContext()
// //     {
// //         // Arrange: Create an instance of ParlotBenchmarks and inject a non-null Scanner.
// //         var benchmarks = new ParlotBenchmarks();
// //         // Obtain the Scanner type from the assembly.
// //         var scannerType = typeof(ParlotBenchmarks).Assembly.GetType("Parlot.Benchmarks.Scanner");
// //         if (scannerType == null)
// //         {
// //             throw new InvalidOperationException("Type 'Parlot.Benchmarks.Scanner' was not found in the assembly.");
// //         }
// // 
// //         var scannerInstance = Activator.CreateInstance(scannerType);
// //         // Use reflection to set the private field '_scanner' to the created Scanner instance.
// //         var scannerField = typeof(ParlotBenchmarks).GetField("_scanner", BindingFlags.NonPublic | BindingFlags.Instance);
// //         scannerField.SetValue(benchmarks, scannerInstance);
// //         // Act: Call the ParseContextCtor method.
// //         var parseContext = benchmarks.ParseContextCtor();
// //         // Assert: Validate that a non-null ParseContext is returned.
// //         Assert.NotNull(parseContext);
// //         Assert.Equal("ParseContext", parseContext.GetType().Name);
// //     }
// 
//     /// <summary>
//     /// Tests the <see cref = "ParlotBenchmarks.ParseContextCtor"/> method to ensure it throws an <see cref = "ArgumentNullException"/> when _scanner is null.
//     /// The test explicitly sets the private _scanner field to null and expects an ArgumentNullException.
//     /// </summary>
// //     [Fact] [Error] (1240-9)CS8602 Dereference of a possibly null reference.
// //     public void ParseContextCtor_WhenScannerIsNull_ThrowsArgumentNullException()
// //     {
// //         // Arrange: Create an instance of ParlotBenchmarks and ensure _scanner is null.
// //         var benchmarks = new ParlotBenchmarks();
// //         var scannerField = typeof(ParlotBenchmarks).GetField("_scanner", BindingFlags.NonPublic | BindingFlags.Instance);
// //         scannerField.SetValue(benchmarks, null);
// //         // Act & Assert: Expect an ArgumentNullException when ParseContextCtor is invoked.
// //         Assert.Throws<ArgumentNullException>(() => benchmarks.ParseContextCtor());
// //     }
// }
