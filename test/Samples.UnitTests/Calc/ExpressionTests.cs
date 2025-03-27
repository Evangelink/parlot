using Moq;
using Parlot.Tests.Calc;
using System;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Number"/> class.
/// </summary>
// public class NumberTests [Error] (173-12)CS1520 Method must have a return type [Error] (209-12)CS1520 Method must have a return type [Error] (223-12)CS1520 Method must have a return type [Error] (175-9)CS0229 Ambiguity between 'NumberTests._value' and 'NumberTests._value' [Error] (209-53)CS1729 'object' does not contain a constructor that takes 1 arguments [Error] (225-9)CS0229 Ambiguity between 'NumberTests._value' and 'NumberTests._value'
// {
//     /// <summary>
//     /// Tests that Evaluate returns the correct value for a positive number.
//     /// </summary>
//     [Fact]
//     public void Evaluate_WithPositiveValue_ReturnsTheSameValue()
//     {
//         // Arrange
//         decimal expected = 5m;
//         var number = new Number(expected);
//         // Act
//         decimal result = number.Evaluate();
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests that Evaluate returns the correct value for a negative number.
//     /// </summary>
//     [Fact]
//     public void Evaluate_WithNegativeValue_ReturnsTheSameValue()
//     {
//         // Arrange
//         decimal expected = -7m;
//         var number = new Number(expected);
//         // Act
//         decimal result = number.Evaluate();
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests that Evaluate returns zero when the number is zero.
//     /// </summary>
//     [Fact]
//     public void Evaluate_WithZero_ReturnsZero()
//     {
//         // Arrange
//         decimal expected = 0m;
//         var number = new Number(expected);
//         // Act
//         decimal result = number.Evaluate();
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests that Evaluate returns the number's value.
//     /// </summary>
//     [Fact]
//     public void Evaluate_NumberValue_ReturnsSameValue()
//     {
//         // Arrange
//         decimal expectedValue = 5.5m;
//         var number = new Number(expectedValue);
//         // Act
//         decimal result = number.Evaluate();
//         // Assert
//         Assert.Equal(expectedValue, result);
//     }
// 
//     /// <summary>
//     /// Tests Evaluate with two positive numbers to verify the sum is correctly computed.
//     /// </summary>
//     [Theory]
//     [InlineData(3, 5, 8)]
//     [InlineData(0, 0, 0)]
//     [InlineData(100.5, 200.25, 300.75)]
//     public void Evaluate_WithPositiveNumbers_ReturnsCorrectSum(decimal leftValue, decimal rightValue, decimal expectedSum)
//     {
//         // Arrange
//         Expression left = new Number(leftValue);
//         Expression right = new Number(rightValue);
//         Addition addition = new Addition(left, right);
//         // Act
//         decimal result = addition.Evaluate();
//         // Assert
//         Assert.Equal(expectedSum, result);
//     }
// 
//     /// <summary>
//     /// Tests Evaluate with negative numbers to verify the sum is correctly computed.
//     /// </summary>
//     [Theory]
//     [InlineData(-3, -5, -8)]
//     [InlineData(-10, 5, -5)]
//     public void Evaluate_WithNegativeNumbers_ReturnsCorrectSum(decimal leftValue, decimal rightValue, decimal expectedSum)
//     {
//         // Arrange
//         Expression left = new Number(leftValue);
//         Expression right = new Number(rightValue);
//         Addition addition = new Addition(left, right);
//         // Act
//         decimal result = addition.Evaluate();
//         // Assert
//         Assert.Equal(expectedSum, result);
//     }
// 
//     /// <summary>
//     /// Tests that Evaluate returns the number's value.
//     /// </summary>
//     [Fact] [Error] (112-17)CS0111 Type 'NumberTests' already defines a member called 'Evaluate_NumberValue_ReturnsSameValue' with the same parameter types
//     public void Evaluate_NumberValue_ReturnsSameValue()
//     {
//         // Arrange
//         decimal expected = 5.5m;
//         var number = new Number(expected);
//         // Act
//         decimal actual = number.Evaluate();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// A dummy concrete implementation of <see cref = "Expression"/> for testing purposes.
//     /// </summary>
//     private class TestExpression : Expression
//     {
//         private readonly decimal _constant;
//         public TestExpression(decimal constant)
//         {
//             _constant = constant;
//         }
// 
//         public override decimal Evaluate() => _constant;
//     }
// 
//     /// <summary>
//     /// Tests that the Evaluate method of a dummy Expression returns the expected constant value.
//     /// </summary>
//     [Fact]
//     public void Evaluate_WithConstantValue_ReturnsConstant()
//     {
//         // Arrange
//         decimal expected = 42m;
//         var testExpression = new TestExpression(expected);
//         // Act
//         decimal result = testExpression.Evaluate();
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests that Evaluate returns the negated value of its inner expression for various inputs.
//     /// </summary>
//     /// <param name = "innerValue">The value of the inner expression.</param>
//     /// <param name = "expected">The expected negated result.</param>
//     [Theory]
//     [InlineData(5, -5)]
//     [InlineData(-5, 5)]
//     [InlineData(0, 0)]
//     public void Evaluate_WithVariousInnerValues_ReturnsNegatedValue(decimal innerValue, decimal expected)
//     {
//         // Arrange
//         Expression innerExpression = new Number(innerValue);
//         Expression negateExpression = new NegateExpression(innerExpression);
//         // Act
//         decimal result = negateExpression.Evaluate();
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     private readonly decimal _value; [Error] (172-30)CS0169 The field 'NumberTests._value' is never used
//     public DummyExpression(decimal value)
//     {
//         _value = value;
//     }
// 
//     public override decimal Evaluate() [Error] (178-29)CS0115 'NumberTests.Evaluate()': no suitable method found to override [Error] (180-16)CS0229 Ambiguity between 'NumberTests._value' and 'NumberTests._value'
//     {
//         return _value;
//     }
// 
//     /// <summary>
//     /// Tests that Evaluate returns the number's value.
//     /// </summary>
//     [Theory]
//     [InlineData(0)]
//     [InlineData(123.45)]
//     [InlineData(-987.65)]
//     public void Evaluate_WithVariousValues_ReturnsValue(decimal value)
//     {
//         // Arrange
//         var number = new Number(value);
//         // Act
//         decimal result = number.Evaluate();
//         // Assert
//         Assert.Equal(value, result);
//     }
// 
//     /// <summary>
//     /// Evaluates the dummy expression.
//     /// </summary>
//     /// <returns>A constant decimal value.</returns>
//     public override decimal Evaluate() => 0m; [Error] (204-29)CS0115 'NumberTests.Evaluate()': no suitable method found to override [Error] (204-29)CS0111 Type 'NumberTests' already defines a member called 'Evaluate' with the same parameter types
//     /// <summary>
//     /// Initializes a new instance of the <see cref = "DummyUnaryExpression"/> class.
//     /// </summary>
//     /// <param name = "inner">The inner expression to be used.</param>
//     public DummyUnaryExpression(Expression inner) : base(inner)
//     {
//     }
// 
//     /// <summary>
//     /// Dummy implementation of the Evaluate method.
//     /// </summary>
//     /// <returns>A constant decimal value.</returns>
//     public override decimal Evaluate() => 0m; [Error] (217-29)CS0115 'NumberTests.Evaluate()': no suitable method found to override [Error] (217-29)CS0111 Type 'NumberTests' already defines a member called 'Evaluate' with the same parameter types
//     private readonly decimal _value; [Error] (218-30)CS0102 The type 'NumberTests' already contains a definition for '_value' [Error] (218-30)CS0169 The field 'NumberTests._value' is never used
//     /// <summary>
//     /// Initializes a new instance of the <see cref = "DummyExpression"/> class with the specified value.
//     /// </summary>
//     /// <param name = "value">The numeric value to be returned by Evaluate.</param>
//     public DummyExpression(decimal value)
//     {
//         _value = value;
//     }
// 
//     /// <summary>
//     /// Evaluates the expression and returns the numeric value.
//     /// </summary>
//     /// <returns>The numeric value provided during construction.</returns>
//     public override decimal Evaluate() [Error] (232-29)CS0115 'NumberTests.Evaluate()': no suitable method found to override [Error] (232-29)CS0111 Type 'NumberTests' already defines a member called 'Evaluate' with the same parameter types [Error] (234-16)CS0229 Ambiguity between 'NumberTests._value' and 'NumberTests._value'
//     {
//         return _value;
//     }
// 
//     /// <summary>
//     /// A fake implementation of the abstract Expression class to be used for testing.
//     /// </summary>
//     private class FakeExpression : Expression
//     {
//         /// <summary>
//         /// Evaluates the expression and returns a constant value.
//         /// </summary>
//         /// <returns>A constant decimal value.</returns>
//         public override decimal Evaluate() => 1m;
//     }
// 
//     /// <summary>
//     /// Tests that the Addition constructor correctly assigns non-null left and right operands.
//     /// The test creates two fake expressions and passes them to the constructor. It then verifies that
//     /// the Left and Right properties of the created Addition instance match the provided expressions.
//     /// </summary>
//     [Fact]
//     public void AdditionConstructor_ValidOperands_SetsLeftAndRight()
//     {
//         // Arrange
//         var leftOperand = new FakeExpression();
//         var rightOperand = new FakeExpression();
//         // Act
//         var addition = new Addition(leftOperand, rightOperand);
//         // Assert
//         Assert.Same(leftOperand, addition.Left);
//         Assert.Same(rightOperand, addition.Right);
//     }
// 
//     /// <summary>
//     /// Tests that the Addition constructor throws an ArgumentNullException when the left operand is null.
//     /// The test passes a null left operand and a valid right operand, and expects an exception.
//     /// </summary>
//     [Fact] [Error] (275-34)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void AdditionConstructor_NullLeftOperand_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression leftOperand = null;
//         var rightOperand = new FakeExpression();
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Addition(leftOperand, rightOperand));
//     }
// 
//     /// <summary>
//     /// Tests that the Addition constructor throws an ArgumentNullException when the right operand is null.
//     /// The test passes a valid left operand and a null right operand, and expects an exception.
//     /// </summary>
//     [Fact] [Error] (290-35)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void AdditionConstructor_NullRightOperand_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var leftOperand = new FakeExpression();
//         Expression rightOperand = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Addition(leftOperand, rightOperand));
//     }
// 
//     /// <summary>
//     /// Tests that the Addition constructor throws an ArgumentNullException when both operands are null.
//     /// This test verifies that the constructor handles the scenario where neither operand is provided.
//     /// </summary>
//     [Fact] [Error] (303-34)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (304-35)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void AdditionConstructor_BothOperandsNull_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression leftOperand = null;
//         Expression rightOperand = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Addition(leftOperand, rightOperand));
//     }
// 
//     /// <summary>
//     /// A dummy implementation of the abstract <see cref = "Expression"/> class for testing purposes.
//     /// </summary>
//     private class DummyExpression : Expression
//     {
//         private readonly decimal _value;
//         public DummyExpression(decimal value)
//         {
//             _value = value;
//         }
// 
//         public override decimal Evaluate() => _value;
//     }
// 
//     /// <summary>
//     /// Tests that the <see cref = "Subtraction"/> constructor correctly assigns the left and right expressions when valid expressions are provided.
//     /// Expected outcome: The constructor creates an instance with the provided left and right expressions.
//     /// </summary>
//     [Fact]
//     public void SubtractionConstructor_ValidExpressions_CreatesInstance()
//     {
//         // Arrange
//         var left = new DummyExpression(10m);
//         var right = new DummyExpression(5m);
//         // Act
//         var subtraction = new Subtraction(left, right);
//         // Assert
//         Assert.NotNull(subtraction);
//         Assert.Equal(left, subtraction.Left);
//         Assert.Equal(right, subtraction.Right);
//     }
// 
//     /// <summary>
//     /// Tests that the <see cref = "Subtraction"/> constructor throws an <see cref = "ArgumentNullException"/> when a null left expression is provided.
//     /// Expected outcome: The constructor throws an ArgumentNullException indicating that the left parameter is null.
//     /// </summary>
//     [Fact] [Error] (349-27)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void SubtractionConstructor_NullLeftExpression_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression left = null;
//         var right = new DummyExpression(5m);
//         // Act & Assert
//         var exception = Assert.Throws<ArgumentNullException>(() => new Subtraction(left, right));
//         Assert.Contains("left", exception.ParamName, StringComparison.OrdinalIgnoreCase);
//     }
// 
//     /// <summary>
//     /// Tests that the <see cref = "Subtraction"/> constructor throws an <see cref = "ArgumentNullException"/> when a null right expression is provided.
//     /// Expected outcome: The constructor throws an ArgumentNullException indicating that the right parameter is null.
//     /// </summary>
//     [Fact] [Error] (365-28)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void SubtractionConstructor_NullRightExpression_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var left = new DummyExpression(10m);
//         Expression right = null;
//         // Act & Assert
//         var exception = Assert.Throws<ArgumentNullException>(() => new Subtraction(left, right));
//         Assert.Contains("right", exception.ParamName, StringComparison.OrdinalIgnoreCase);
//     }
// 
//     /// <summary>
//     /// Tests that the Multiplication constructor correctly assigns the left and right operands when valid expressions are provided.
//     /// This test follows the Arrange-Act-Assert pattern by creating valid Number expressions, constructing a Multiplication instance,
//     /// and asserting that the operands are stored correctly.
//     /// </summary>
//     [Fact]
//     public void MultiplicationConstructor_ValidOperands_StoresOperands()
//     {
//         // Arrange
//         Expression leftOperand = new Number(3m);
//         Expression rightOperand = new Number(4m);
//         // Act
//         var multiplication = new Multiplication(leftOperand, rightOperand);
//         // Assert
//         Assert.Equal(leftOperand, multiplication.Left);
//         Assert.Equal(rightOperand, multiplication.Right);
//     }
// 
//     /// <summary>
//     /// Tests that the Multiplication constructor throws an ArgumentNullException when the left operand is null.
//     /// The test arranges for a null left operand and asserts that the expected exception is thrown.
//     /// </summary>
//     [Fact]
//     public void MultiplicationConstructor_NullLeftOperand_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression rightOperand = new Number(4m);
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Multiplication(null, rightOperand));
//     }
// 
//     /// <summary>
//     /// Tests that the Multiplication constructor throws an ArgumentNullException when the right operand is null.
//     /// The test arranges for a null right operand and asserts that the expected exception is thrown.
//     /// </summary>
//     [Fact]
//     public void MultiplicationConstructor_NullRightOperand_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression leftOperand = new Number(3m);
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Multiplication(leftOperand, null));
//     }
// 
//     /// <summary>
//     /// Tests that the Division constructor correctly initializes the Left and Right properties when provided with valid non-null expressions.
//     /// </summary>
//     [Fact]
//     public void DivisionConstructor_WithValidExpressions_SetsProperties()
//     {
//         // Arrange
//         Expression left = new Number(10);
//         Expression right = new Number(2);
//         // Act
//         Division divisionExpression = new Division(left, right);
//         // Assert
//         Assert.Same(left, divisionExpression.Left);
//         Assert.Same(right, divisionExpression.Right);
//     }
// 
//     /// <summary>
//     /// Tests that the Division constructor throws an ArgumentNullException when the left expression is null.
//     /// </summary>
//     [Fact]
//     public void DivisionConstructor_NullLeft_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression right = new Number(2);
//         // Act & Assert
//         ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new Division(null, right));
//         Assert.Equal("left", exception.ParamName);
//     }
// 
//     /// <summary>
//     /// Tests that the Division constructor throws an ArgumentNullException when the right expression is null.
//     /// </summary>
//     [Fact]
//     public void DivisionConstructor_NullRight_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression left = new Number(10);
//         // Act & Assert
//         ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new Division(left, null));
//         Assert.Equal("right", exception.ParamName);
//     }
// 
//     /// <summary>
//     /// Tests that the Exponent constructor properly assigns the Left and Right expressions when provided with valid non-null expressions.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithValidExpressions_AssignsLeftAndRight()
//     {
//         // Arrange
//         Expression left = new Number(2);
//         Expression right = new Number(3);
//         // Act
//         Exponent exponent = new Exponent(left, right);
//         // Assert
//         Assert.Equal(left, exponent.Left);
//         Assert.Equal(right, exponent.Right);
//     }
// 
//     /// <summary>
//     /// Tests that the Exponent constructor throws an ArgumentNullException when the left expression is null.
//     /// </summary>
//     [Fact] [Error] (480-27)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Constructor_WithNullLeft_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression left = null;
//         Expression right = new Number(3);
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Exponent(left, right));
//     }
// 
//     /// <summary>
//     /// Tests that the Exponent constructor throws an ArgumentNullException when the right expression is null.
//     /// </summary>
//     [Fact] [Error] (494-28)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Constructor_WithNullRight_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Expression left = new Number(2);
//         Expression right = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Exponent(left, right));
//     }
// 
//     /// <summary>
//     /// Tests that the <see cref = "Number"/> constructor correctly sets the Value property when provided a positive decimal.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithPositiveValue_SetsValueProperty()
//     {
//         // Arrange
//         decimal expectedValue = 42.5m;
//         // Act
//         var number = new Number(expectedValue);
//         // Assert
//         Assert.Equal(expectedValue, number.Value);
//     }
// 
//     /// <summary>
//     /// Tests that the <see cref = "Number"/> constructor correctly sets the Value property when provided a zero value.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithZeroValue_SetsValueProperty()
//     {
//         // Arrange
//         decimal expectedValue = 0m;
//         // Act
//         var number = new Number(expectedValue);
//         // Assert
//         Assert.Equal(expectedValue, number.Value);
//     }
// 
//     /// <summary>
//     /// Tests that the <see cref = "Number"/> constructor correctly sets the Value property when provided a negative decimal.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithNegativeValue_SetsValueProperty()
//     {
//         // Arrange
//         decimal expectedValue = -15.75m;
//         // Act
//         var number = new Number(expectedValue);
//         // Assert
//         Assert.Equal(expectedValue, number.Value);
//     }
// 
//     /// <summary>
//     /// Tests that the <see cref = "Number"/> constructor correctly sets the Value property when provided edge boundary values.
//     /// </summary>
//     /// <param name = "valueString">The string representation of the decimal edge value.</param>
//     [Theory]
//     [InlineData("79228162514264337593543950335")] // decimal.MaxValue
//     [InlineData("-79228162514264337593543950335")] // decimal.MinValue
//     public void Constructor_WithEdgeValues_SetsValueProperty(string valueString)
//     {
//         // Arrange
//         decimal expectedValue = decimal.Parse(valueString);
//         // Act
//         var number = new Number(expectedValue);
//         // Assert
//         Assert.Equal(expectedValue, number.Value);
//     }
// 
//     /// <summary>
//     /// A test double class derived from BinaryExpression used for testing purposes.
//     /// </summary>
//     private class TestBinaryExpression : BinaryExpression
//     {
//         /// <summary>
//         /// Initializes a new instance of the <see cref = "TestBinaryExpression"/> class with given left and right expressions.
//         /// </summary>
//         /// <param name = "left">The left expression.</param>
//         /// <param name = "right">The right expression.</param>
//         public TestBinaryExpression(Expression left, Expression right) : base(left, right)
//         {
//         }
// 
//         /// <summary>
//         /// Provides a dummy implementation of the Evaluate method.
//         /// </summary>
//         /// <returns>A constant decimal value.</returns>
//         public override decimal Evaluate() => 0m;
//     }
// 
//     /// <summary>
//     /// Tests that the Left property returns the same expression instance that was provided in the constructor.
//     /// </summary>
//     [Fact]
//     public void Left_WithValidExpression_ReturnsSameInstance()
//     {
//         // Arrange
//         Expression expectedLeft = new Number(5);
//         Expression dummyRight = new Number(10);
//         var testExpression = new TestBinaryExpression(expectedLeft, dummyRight);
//         // Act
//         Expression actualLeft = testExpression.Left;
//         // Assert
//         Assert.Equal(expectedLeft, actualLeft);
//     }
// 
//     /// <summary>
//     /// Tests that the Left property returns null when null is passed in the constructor.
//     /// This test covers the edge case of a null left expression.
//     /// </summary>
//     [Fact] [Error] (603-35)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (606-73)CS8604 Possible null reference argument for parameter 'left' in 'TestBinaryExpression.TestBinaryExpression(Expression left, Expression right)'. [Error] (610-59)CS8604 Possible null reference argument for parameter 'left' in 'TestBinaryExpression.TestBinaryExpression(Expression left, Expression right)'.
//     public void Left_WithNullExpression_ReturnsNull()
//     {
//         // Arrange
//         Expression expectedLeft = null;
//         Expression dummyRight = new Number(10);
//         // Act
//         var exception = Record.Exception(() => new TestBinaryExpression(expectedLeft, dummyRight));
//         // If no exception is thrown during construction, verify that Left property is null.
//         if (exception == null)
//         {
//             var testExpression = new TestBinaryExpression(expectedLeft, dummyRight);
//             Expression actualLeft = testExpression.Left;
//             Assert.Null(actualLeft);
//         }
//         else
//         {
//             // If an exception is thrown, then the test verifies that the exception is of type ArgumentNullException.
//             Assert.IsType<ArgumentNullException>(exception);
//         }
//     }
// 
//     /// <summary>
//     /// Verifies that the 'Right' property returns the same expression that was provided during construction.
//     /// </summary>
//     [Fact]
//     public void RightProperty_WithValidExpression_ReturnsSameInstance()
//     {
//         // Arrange
//         var left = new Number(5m);
//         var right = new Number(10m);
//         // Act
//         var addition = new Addition(left, right);
//         // Assert
//         Assert.Same(right, addition.Right);
//     }
// 
//     /// <summary>
//     /// Verifies that constructing an Addition with a null right expression throws an ArgumentNullException.
//     /// </summary>
//     [Fact]
//     public void RightProperty_WithNullExpression_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var left = new Number(5m);
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Addition(left, null));
//     }
// 
//     /// <summary>
//     /// Tests that the Inner property returns the same Expression instance that was provided to the constructor.
//     /// This is a happy path test ensuring proper assignment and retrieval via the property getter.
//     /// </summary>
//     [Fact]
//     public void Inner_Get_WithValidExpression_ReturnsProvidedInnerExpression()
//     {
//         // Arrange
//         decimal numberValue = 5m;
//         Expression innerExpression = new Number(numberValue);
//         NegateExpression negateExpression = new NegateExpression(innerExpression);
//         // Act
//         Expression resultInner = negateExpression.Inner;
//         // Assert
//         Assert.Equal(innerExpression, resultInner);
//     }
// 
//     /// <summary>
//     /// Tests the behavior of the Inner property when a null Expression is provided.
//     /// This test covers an edge case scenario by verifying that the property getter returns null
//     /// if the constructor is called with a null argument.
//     /// </summary>
//     [Fact] [Error] (674-38)CS8600 Converting null literal or possible null value to non-nullable type.
//     public void Inner_Get_WithNullExpression_ReturnsNull()
//     {
//         // Arrange
//         Expression innerExpression = null;
//         NegateExpression negateExpression = new NegateExpression(innerExpression);
//         // Act
//         Expression resultInner = negateExpression.Inner;
//         // Assert
//         Assert.Null(resultInner);
//     }
// 
//     /// <summary>
//     /// Verifies that the Value property returns the expected positive decimal value provided at construction.
//     /// </summary>
//     /// <param name = "input">A positive decimal number.</param>
//     [Theory]
//     [InlineData(1.23)]
//     [InlineData(1000)]
//     [InlineData(0.0001)]
//     public void Value_WhenInitializedWithPositiveDecimal_ReturnsExpectedValue(decimal input)
//     {
//         // Arrange
//         Number number = new Number(input);
//         // Act
//         decimal actualValue = number.Value;
//         // Assert
//         Assert.Equal(input, actualValue);
//     }
// 
//     /// <summary>
//     /// Verifies that the Value property returns the expected negative decimal value provided at construction.
//     /// </summary>
//     /// <param name = "input">A negative decimal number.</param>
//     [Theory]
//     [InlineData(-1.23)]
//     [InlineData(-1000)]
//     [InlineData(-0.0001)]
//     public void Value_WhenInitializedWithNegativeDecimal_ReturnsExpectedValue(decimal input)
//     {
//         // Arrange
//         Number number = new Number(input);
//         // Act
//         decimal actualValue = number.Value;
//         // Assert
//         Assert.Equal(input, actualValue);
//     }
// 
//     /// <summary>
//     /// Verifies that the Value property returns zero when initialized with zero.
//     /// </summary>
//     [Fact]
//     public void Value_WhenInitializedWithZero_ReturnsZero()
//     {
//         // Arrange
//         decimal input = 0m;
//         Number number = new Number(input);
//         // Act
//         decimal actualValue = number.Value;
//         // Assert
//         Assert.Equal(0m, actualValue);
//     }
// }