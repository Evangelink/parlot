using Moq;
using Parlot.Benchmarks;
using Parlot.Tests.Calc;
using System;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "ExprBench"/> class.
/// </summary>
public class ExprBenchTests
{
    /// <summary>
    /// Tests that the ParlotRawSmall method returns the expected Expression when the parser successfully parses a valid expression.
    /// </summary>
//     [Fact] [Error] (21-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (25-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (26-64)CS1503 Argument 1: cannot convert from 'Parlot.Tests.Calc.Expression' to '?' [Error] (31-9)CS8602 Dereference of a possibly null reference. [Error] (34-9)CS8602 Dereference of a possibly null reference.
//     public void ParlotRawSmall_HappyPath_ReturnsExpectedExpression()
//     {
//         // Arrange
//         // Create an expected Expression instance.
//         var expectedExpression = new Expression();
//         // Define a sample valid expression.
//         var testExpression = "1+2";
//         // Create a mock for the Parser<Expression> dependency.
//         var mockParser = new Mock<Parser<Expression>>();
//         mockParser.Setup(p => p.Parse(testExpression)).Returns(expectedExpression);
//         // Instantiate the class under test.
//         var exprBench = new ExprBench();
//         // Use reflection to inject the mock parser into the private readonly field _parser.
//         var parserField = typeof(ExprBench).GetField("_parser", BindingFlags.NonPublic | BindingFlags.Instance);
//         parserField.SetValue(exprBench, mockParser.Object);
//         // Use reflection to set the constant (simulated as a field for testing purposes) _expression1.
//         var expression1Field = typeof(ExprBench).GetField("_expression1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
//         expression1Field.SetValue(exprBench, testExpression);
//         // Act
//         var result = exprBench.ParlotRawSmall();
//         // Assert
//         Assert.Equal(expectedExpression, result);
//         mockParser.Verify(p => p.Parse(testExpression), Times.Once);
//     }

    /// <summary>
    /// Tests that the ParlotRawSmall method returns the expected Expression when the expression is empty.
    /// </summary>
//     [Fact] [Error] (50-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (54-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (55-64)CS1503 Argument 1: cannot convert from 'Parlot.Tests.Calc.Expression' to '?' [Error] (60-9)CS8602 Dereference of a possibly null reference. [Error] (63-9)CS8602 Dereference of a possibly null reference.
//     public void ParlotRawSmall_EmptyExpression_ReturnsExpectedExpression()
//     {
//         // Arrange
//         // Create an expected Expression instance.
//         var expectedExpression = new Expression();
//         // Define an empty expression.
//         var testExpression = string.Empty;
//         // Create a mock for the Parser<Expression> dependency.
//         var mockParser = new Mock<Parser<Expression>>();
//         mockParser.Setup(p => p.Parse(testExpression)).Returns(expectedExpression);
//         // Instantiate the class under test.
//         var exprBench = new ExprBench();
//         // Inject the mock parser.
//         var parserField = typeof(ExprBench).GetField("_parser", BindingFlags.NonPublic | BindingFlags.Instance);
//         parserField.SetValue(exprBench, mockParser.Object);
//         // Set the _expression1 field to an empty string.
//         var expression1Field = typeof(ExprBench).GetField("_expression1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
//         expression1Field.SetValue(exprBench, testExpression);
//         // Act
//         var result = exprBench.ParlotRawSmall();
//         // Assert
//         Assert.Equal(expectedExpression, result);
//         mockParser.Verify(p => p.Parse(testExpression), Times.Once);
//     }

    /// <summary>
    /// Tests that the ParlotRawSmall method propagates exceptions thrown by the parser.
    /// </summary>
//     [Fact] [Error] (83-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (89-9)CS8602 Dereference of a possibly null reference. [Error] (92-9)CS8602 Dereference of a possibly null reference.
//     public void ParlotRawSmall_WhenParserThrowsException_PropagatesException()
//     {
//         // Arrange
//         // Define a test expression that will trigger an exception.
//         var testExpression = "invalid";
//         // Create an expected exception.
//         var expectedException = new InvalidOperationException("Parsing error");
//         // Create a mock for the Parser<Expression> dependency that throws an exception.
//         var mockParser = new Mock<Parser<Expression>>();
//         mockParser.Setup(p => p.Parse(testExpression)).Throws(expectedException);
//         // Instantiate the class under test.
//         var exprBench = new ExprBench();
//         // Inject the mock parser.
//         var parserField = typeof(ExprBench).GetField("_parser", BindingFlags.NonPublic | BindingFlags.Instance);
//         parserField.SetValue(exprBench, mockParser.Object);
//         // Set the _expression1 field to the test expression.
//         var expression1Field = typeof(ExprBench).GetField("_expression1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
//         expression1Field.SetValue(exprBench, testExpression);
//         // Act & Assert
//         var exception = Assert.Throws<InvalidOperationException>(() => exprBench.ParlotRawSmall());
//         Assert.Equal(expectedException.Message, exception.Message);
//         mockParser.Verify(p => p.Parse(testExpression), Times.Once);
//     }

    /// <summary>
    /// Tests that ParlotCompiledSmall returns the expected Expression when the parser successfully parses the expression.
    /// Arrange: Sets up a mock parser to return an expected Expression.
    /// Act: Calls ParlotCompiledSmall.
    /// Assert: Verifies that the returned Expression is as expected and that the parser's Parse method was called with the expected input.
    /// </summary>
//     [Fact] [Error] (111-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (112-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (114-112)CS1503 Argument 1: cannot convert from 'Parlot.Tests.Calc.Expression' to '?' [Error] (113-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (117-9)CS8602 Dereference of a possibly null reference. [Error] (121-57)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void ParlotCompiledSmall_WithValidParser_ReturnsExpectedExpression()
//     {
//         // Arrange
//         var benchInstance = new ExprBench();
//         // Create a mock for the parser dependency.
//         var mockParser = new Mock<Parser<Expression>>();
//         var expectedExpression = new Expression();
//         string capturedArgument = null;
//         mockParser.Setup(m => m.Parse(It.IsAny<string>())).Callback<string>(s => capturedArgument = s).Returns(expectedExpression);
//         // Inject the mock parser into the instance via reflection.
//         var compiledField = typeof(ExprBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
//         compiledField.SetValue(benchInstance, mockParser.Object);
//         // Retrieve the constant _expression1 value from the type.
//         // Note: Const fields are static and can be retrieved using GetRawConstantValue.
//         var expressionField = typeof(ExprBench).GetField("_expression1", BindingFlags.NonPublic | BindingFlags.Static);
//         var expressionInput = expressionField != null ? (string)expressionField.GetRawConstantValue() : null;
//         // Act
//         var result = benchInstance.ParlotCompiledSmall();
//         // Assert
//         Assert.Equal(expectedExpression, result);
//         Assert.Equal(expressionInput, capturedArgument);
//     }

    /// <summary>
    /// Tests that ParlotCompiledSmall propagates exceptions thrown by the parser.
    /// Arrange: Sets up a mock parser to throw an exception during parsing.
    /// Act & Assert: Verifies that the exception is propagated when ParlotCompiledSmall is called.
    /// </summary>
//     [Fact] [Error] (139-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (144-9)CS8602 Dereference of a possibly null reference.
//     public void ParlotCompiledSmall_WhenParserThrows_ExceptionPropagates()
//     {
//         // Arrange
//         var benchInstance = new ExprBench();
//         var mockParser = new Mock<Parser<Expression>>();
//         var expectedException = new InvalidOperationException("Parser error");
//         mockParser.Setup(m => m.Parse(It.IsAny<string>())).Throws(expectedException);
//         // Inject the mock parser into the instance via reflection.
//         var compiledField = typeof(ExprBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
//         compiledField.SetValue(benchInstance, mockParser.Object);
//         // Act & Assert
//         var exception = Assert.Throws<InvalidOperationException>(() => benchInstance.ParlotCompiledSmall());
//         Assert.Equal(expectedException.Message, exception.Message);
//     }

    /// <summary>
    /// Tests that ParlotCompiledSmall throws a NullReferenceException when the parser dependency is not set.
    /// Arrange: Ensures the _compiled field is null.
    /// Act & Assert: Verifies that calling ParlotCompiledSmall results in a NullReferenceException.
    /// </summary>
//     [Fact] [Error] (162-9)CS8602 Dereference of a possibly null reference.
//     public void ParlotCompiledSmall_WhenParserIsNull_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var benchInstance = new ExprBench();
//         // Explicitly set the _compiled field to null.
//         var compiledField = typeof(ExprBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
//         compiledField.SetValue(benchInstance, null);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => benchInstance.ParlotCompiledSmall());
//     }

    /// <summary>
    /// Tests that the ParlotFluentSmall method returns a non-null Expression when invoked under happy path conditions.
    /// Since the method attempts to parse a predefined expression (_expression1) using FluentParser.Expression.TryParse,
    /// a valid input is assumed and a successfully parsed Expression should be returned.
    /// </summary>
    [Fact]
    public void ParlotFluentSmall_WithValidExpression_ReturnsNonNullExpression()
    {
        // Arrange
        var bench = new ExprBench();
        // Act
        Expression result = bench.ParlotFluentSmall();
        // Assert
        Assert.NotNull(result);
        Assert.IsType<Expression>(result);
    }

    /// <summary>
    /// Tests that the <see cref = "ExprBench.PidginSmall"/> method returns a non-null Expression instance when the internal expression is valid.
    /// 
    /// Arrange: An instance of ExprBench is created.
    /// Act: The PidginSmall method is invoked.
    /// Assert: The returned Expression instance is verified to be non-null.
    /// </summary>
    [Fact]
    public void PidginSmall_WhenCalled_ReturnsNonNullExpression()
    {
        // Arrange
        var exprBench = new ExprBench();
        // Act
        Expression result = exprBench.PidginSmall();
        // Assert
        Assert.NotNull(result);
    }

    /// <summary>
    /// Tests that ParlotRawBig returns the parsed expression when the parser successfully parses the input.
    /// </summary>
//     [Fact] [Error] (209-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (210-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (212-68)CS1503 Argument 1: cannot convert from 'Parlot.Tests.Calc.Expression' to '?' [Error] (214-21)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (216-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (217-9)CS8602 Dereference of a possibly null reference. [Error] (219-22)CS8602 Dereference of a possibly null reference.
//     public void ParlotRawBig_HappyPath_ReturnsParsedExpression()
//     {
//         // Arrange
//         var expectedExpression = new Expression();
//         var mockParser = new Mock<Parser<Expression>>();
//         // Setup the mock parser to return the expected expression when Parse is invoked with any string.
//         mockParser.Setup(p => p.Parse(It.IsAny<string>())).Returns(expectedExpression);
//         // Instantiate the ExprBench class.
//         var bench = (ExprBench)Activator.CreateInstance(typeof(ExprBench));
//         // Inject the mock parser into the private _parser field using reflection.
//         FieldInfo parserField = typeof(ExprBench).GetField("_parser", BindingFlags.Instance | BindingFlags.NonPublic);
//         parserField.SetValue(bench, mockParser.Object);
//         // Act
//         var result = bench.ParlotRawBig();
//         // Assert
//         Assert.Equal(expectedExpression, result);
//         mockParser.Verify(p => p.Parse(It.IsAny<string>()), Times.Once);
//     }

    /// <summary>
    /// Tests that ParlotRawBig throws a NullReferenceException when the parser dependency is null.
    /// </summary>
//     [Fact] [Error] (232-21)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (234-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (235-9)CS8602 Dereference of a possibly null reference. [Error] (237-53)CS8602 Dereference of a possibly null reference.
//     public void ParlotRawBig_WhenParserIsNull_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var bench = (ExprBench)Activator.CreateInstance(typeof(ExprBench));
//         // Set the private _parser field to null using reflection.
//         FieldInfo parserField = typeof(ExprBench).GetField("_parser", BindingFlags.Instance | BindingFlags.NonPublic);
//         parserField.SetValue(bench, null);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => bench.ParlotRawBig());
//     }

    /// <summary>
    /// Tests that ParlotRawBig propagates exceptions thrown by the parser dependency.
    /// </summary>
//     [Fact] [Error] (248-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (251-21)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (253-33)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (254-9)CS8602 Dereference of a possibly null reference. [Error] (256-72)CS8602 Dereference of a possibly null reference.
//     public void ParlotRawBig_WhenParserThrowsException_PropagatesException()
//     {
//         // Arrange
//         var exceptionMessage = "Parsing failed.";
//         var mockParser = new Mock<Parser<Expression>>();
//         // Setup the mock parser to throw an InvalidOperationException when Parse is invoked.
//         mockParser.Setup(p => p.Parse(It.IsAny<string>())).Throws(new InvalidOperationException(exceptionMessage));
//         var bench = (ExprBench)Activator.CreateInstance(typeof(ExprBench));
//         // Inject the mock parser using reflection.
//         FieldInfo parserField = typeof(ExprBench).GetField("_parser", BindingFlags.Instance | BindingFlags.NonPublic);
//         parserField.SetValue(bench, mockParser.Object);
//         // Act & Assert
//         var exception = Assert.Throws<InvalidOperationException>(() => bench.ParlotRawBig());
//         Assert.Equal(exceptionMessage, exception.Message);
//     }

    /// <summary>
    /// Tests the ParlotCompiledBig method when the parser returns a valid Expression.
    /// Expected outcome: The method should return the Expression provided by the parser.
    /// </summary>
//     [Fact] [Error] (269-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (270-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (271-64)CS1503 Argument 1: cannot convert from 'Parlot.Tests.Calc.Expression' to '?' [Error] (275-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (276-9)CS8602 Dereference of a possibly null reference. [Error] (278-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (279-9)CS8602 Dereference of a possibly null reference. [Error] (281-23)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (281-23)CS8602 Dereference of a possibly null reference.
//     public void ParlotCompiledBig_HappyPath_ReturnsExpectedExpression()
//     {
//         // Arrange
//         const string testExpression = "1 + 2";
//         var expectedExpression = new Expression();
//         var parserMock = new Mock<Parser<Expression>>();
//         parserMock.Setup(p => p.Parse(testExpression)).Returns(expectedExpression);
//         // Create instance of ExprBench.
//         var benchInstance = Activator.CreateInstance(typeof(ExprBench));
//         // Inject the mock parser into the private field _compiled.
//         FieldInfo compiledField = typeof(ExprBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
//         compiledField.SetValue(benchInstance, parserMock.Object);
//         // Inject test expression into the private field _expression2.
//         FieldInfo expression2Field = typeof(ExprBench).GetField("_expression2", BindingFlags.NonPublic | BindingFlags.Instance);
//         expression2Field.SetValue(benchInstance, testExpression);
//         // Act
//         var result = ((ExprBench)benchInstance).ParlotCompiledBig();
//         // Assert
//         Assert.Equal(expectedExpression, result);
//         parserMock.Verify(p => p.Parse(testExpression), Times.Once);
//     }

    /// <summary>
    /// Tests the ParlotCompiledBig method when the parser throws an exception.
    /// Expected outcome: The method should propagate the exception thrown by the parser.
    /// </summary>
//     [Fact] [Error] (297-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (300-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (301-9)CS8602 Dereference of a possibly null reference. [Error] (302-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (303-9)CS8602 Dereference of a possibly null reference. [Error] (305-73)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (305-73)CS8602 Dereference of a possibly null reference.
//     public void ParlotCompiledBig_WhenParserThrows_PropagatesException()
//     {
//         // Arrange
//         const string testExpression = "invalid expression";
//         var expectedException = new InvalidOperationException("Parsing failed");
//         var parserMock = new Mock<Parser<Expression>>();
//         parserMock.Setup(p => p.Parse(testExpression)).Throws(expectedException);
//         var benchInstance = Activator.CreateInstance(typeof(ExprBench));
//         FieldInfo compiledField = typeof(ExprBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
//         compiledField.SetValue(benchInstance, parserMock.Object);
//         FieldInfo expression2Field = typeof(ExprBench).GetField("_expression2", BindingFlags.NonPublic | BindingFlags.Instance);
//         expression2Field.SetValue(benchInstance, testExpression);
//         // Act & Assert
//         var exception = Assert.Throws<InvalidOperationException>(() => ((ExprBench)benchInstance).ParlotCompiledBig());
//         Assert.Equal(expectedException.Message, exception.Message);
//         parserMock.Verify(p => p.Parse(testExpression), Times.Once);
//     }

    /// <summary>
    /// Tests the ParlotCompiledBig method when an empty expression is provided.
    /// Expected outcome: The method should call the parser with the empty string and return the corresponding Expression.
    /// </summary>
//     [Fact] [Error] (319-34)CS0144 Cannot create an instance of the abstract type or interface 'Expression' [Error] (320-35)CS0308 The non-generic type 'Parser' cannot be used with type arguments [Error] (321-64)CS1503 Argument 1: cannot convert from 'Parlot.Tests.Calc.Expression' to '?' [Error] (323-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (324-9)CS8602 Dereference of a possibly null reference. [Error] (325-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (326-9)CS8602 Dereference of a possibly null reference. [Error] (328-23)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (328-23)CS8602 Dereference of a possibly null reference.
//     public void ParlotCompiledBig_WithEmptyExpression_ReturnsExpectedExpression()
//     {
//         // Arrange
//         const string testExpression = "";
//         var expectedExpression = new Expression();
//         var parserMock = new Mock<Parser<Expression>>();
//         parserMock.Setup(p => p.Parse(testExpression)).Returns(expectedExpression);
//         var benchInstance = Activator.CreateInstance(typeof(ExprBench));
//         FieldInfo compiledField = typeof(ExprBench).GetField("_compiled", BindingFlags.NonPublic | BindingFlags.Instance);
//         compiledField.SetValue(benchInstance, parserMock.Object);
//         FieldInfo expression2Field = typeof(ExprBench).GetField("_expression2", BindingFlags.NonPublic | BindingFlags.Instance);
//         expression2Field.SetValue(benchInstance, testExpression);
//         // Act
//         var result = ((ExprBench)benchInstance).ParlotCompiledBig();
//         // Assert
//         Assert.Equal(expectedExpression, result);
//         parserMock.Verify(p => p.Parse(testExpression), Times.Once);
//     }

    /// <summary>
    /// Tests that the <see cref = "ExprBench.ParlotFluentBig"/> method returns a non-null Expression when invoked.
    /// This represents the happy path scenario where the underlying FluentParser successfully parses the expression.
    /// </summary>
    [Fact]
    public void ParlotFluentBig_WhenCalled_WithValidExpression_ReturnsNonNullExpression()
    {
        // Arrange
        var exprBench = new ExprBench();
        // Act
        Expression result = exprBench.ParlotFluentBig();
        // Assert
        Assert.NotNull(result);
    }

    /// <summary>
    /// Tests that the <see cref = "ExprBench.ParlotFluentBig"/> method completes without throwing an exception.
    /// This verifies that the method handles internal parsing gracefully.
    /// </summary>
    [Fact]
    public void ParlotFluentBig_WhenCalled_DoesNotThrowException()
    {
        // Arrange
        var exprBench = new ExprBench();
        // Act & Assert
        var exception = Record.Exception(() => exprBench.ParlotFluentBig());
        Assert.Null(exception);
    }

    /// <summary>
    /// Tests that the <see cref = "ExprBench.PidginBig"/> method returns a non-null Expression when a valid expression is provided.
    /// The test arranges by setting the private static field "_expression2" to a valid expression string,
    /// then calls the method and asserts that the returned Expression is not null.
    /// </summary>
    [Fact]
    public void PidginBig_ValidExpression_ReturnsNonNullExpression()
    {
        // Arrange
        const string validExpression = "1+2"; // Assuming "1+2" is a valid expression.
        var fieldInfo = typeof(ExprBench).GetField("_expression2", BindingFlags.Static | BindingFlags.NonPublic);
        if (fieldInfo == null)
        {
            throw new Exception("Could not find the '_expression2' field.");
        }

        var originalValue = fieldInfo.GetValue(null) as string;
        try
        {
            // Set the hidden expression to a valid one.
            fieldInfo.SetValue(null, validExpression);
            var bench = new ExprBench();
            // Act
            var result = bench.PidginBig();
            // Assert
            Assert.NotNull(result);
        }
        finally
        {
            // Restore the original value to avoid test pollution.
            fieldInfo.SetValue(null, originalValue);
        }
    }

    /// <summary>
    /// Tests that the <see cref = "ExprBench.PidginBig"/> method throws an Exception when an invalid expression is provided.
    /// The test arranges by setting the private static field "_expression2" to an invalid expression string,
    /// then calls the method and asserts that an Exception is thrown.
    /// </summary>
    [Fact]
    public void PidginBig_InvalidExpression_ThrowsException()
    {
        // Arrange
        const string invalidExpression = "invalid expression"; // Assuming this is not a valid expression.
        var fieldInfo = typeof(ExprBench).GetField("_expression2", BindingFlags.Static | BindingFlags.NonPublic);
        if (fieldInfo == null)
        {
            throw new Exception("Could not find the '_expression2' field.");
        }

        var originalValue = fieldInfo.GetValue(null) as string;
        try
        {
            // Set the hidden expression to an invalid one.
            fieldInfo.SetValue(null, invalidExpression);
            var bench = new ExprBench();
            // Act and Assert
            Assert.Throws<Exception>(() => bench.PidginBig());
        }
        finally
        {
            // Restore the original value.
            fieldInfo.SetValue(null, originalValue);
        }
    }
}