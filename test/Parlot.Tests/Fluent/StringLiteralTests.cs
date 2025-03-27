using Moq;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "StringLiteral"/> class focusing on the Parse method.
/// </summary>
public class StringLiteralTests
{
    /// <summary>
    /// A fake cursor to simulate scanner cursor behavior.
    /// </summary>
    private class FakeCursor
    {
        public int Offset { get; set; }
    }

    /// <summary>
    /// A fake scanner to simulate scanner methods and buffer.
    /// </summary>
    private class FakeScanner
    {
        public string Buffer { get; set; }
        public FakeCursor Cursor { get; set; } = new FakeCursor();
        public Func<bool> ReadSingleQuotedStringDelegate { get; set; }
        public Func<bool> ReadDoubleQuotedStringDelegate { get; set; }
        public Func<bool> ReadQuotedStringDelegate { get; set; }
        public Func<bool> ReadBacktickStringDelegate { get; set; }
        public Func<char[], bool> ReadQuotedStringCustomDelegate { get; set; }

        public bool ReadSingleQuotedString()
        {
            return ReadSingleQuotedStringDelegate?.Invoke() ?? false;
        }

        public bool ReadDoubleQuotedString()
        {
            return ReadDoubleQuotedStringDelegate?.Invoke() ?? false;
        }

        public bool ReadQuotedString()
        {
            return ReadQuotedStringDelegate?.Invoke() ?? false;
        }

        public bool ReadBacktickString()
        {
            return ReadBacktickStringDelegate?.Invoke() ?? false;
        }

        public bool ReadQuotedString(char[] expectedChars)
        {
            return ReadQuotedStringCustomDelegate?.Invoke(expectedChars) ?? false;
        }
    }

    /// <summary>
    /// A fake parse context to simulate parser context behavior.
    /// </summary>
    private class FakeParseContext
    {
        public FakeScanner Scanner { get; set; }

        public void EnterParser(object parser)
        { /* No operation for testing */
        }

        public void ExitParser(object parser)
        { /* No operation for testing */
        }
    }

    /// <summary>
    /// A fake parse result to record the outcome of the parse.
    /// </summary>
    private class FakeParseResult
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public string Value { get; private set; }
        public bool SetCalled { get; private set; } = false;

        public void Set(int start, int end, string value)
        {
            SetCalled = true;
            Start = start;
            End = end;
            Value = value;
        }
    }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.Single when the scanner returns success.
    /// Checks that the method returns true and the result is set correctly.
    /// </summary>
//     [Fact] [Error] (114-17)CS0841 Cannot use local variable 'fakeScanner' before it is declared [Error] (125-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_SingleQuote_Success_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         string text = "'abc'";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadSingleQuotedStringDelegate = () =>
//             {
//                 // Simulate successful parsing by updating the cursor offset to end of buffer.
//                 fakeScanner.Cursor.Offset = text.Length;
//                 return true;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Single);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.True(parseSuccess);
//         Assert.True(fakeResult.SetCalled);
//         Assert.Equal(0, fakeResult.Start);
//         Assert.Equal(text.Length, fakeResult.End);
//         // The decoded string should be the inner content without quotes.
//         Assert.Equal("abc", fakeResult.Value);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.Single when the scanner returns failure.
    /// Checks that the method returns false and the result is not set.
    /// </summary>
//     [Fact] [Error] (164-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_SingleQuote_Failure_ReturnsFalse()
//     {
//         // Arrange
//         string text = "'abc'";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadSingleQuotedStringDelegate = () =>
//             {
//                 // Simulate failure by not updating the cursor.
//                 return false;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Single);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.False(parseSuccess);
//         Assert.False(fakeResult.SetCalled);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.Double when the scanner returns success.
    /// Checks that the method returns true and the result is set correctly.
    /// </summary>
//     [Fact] [Error] (188-17)CS0841 Cannot use local variable 'fakeScanner' before it is declared [Error] (199-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_DoubleQuote_Success_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         string text = "\"def\"";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadDoubleQuotedStringDelegate = () =>
//             {
//                 fakeScanner.Cursor.Offset = text.Length;
//                 return true;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Double);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.True(parseSuccess);
//         Assert.True(fakeResult.SetCalled);
//         Assert.Equal(0, fakeResult.Start);
//         Assert.Equal(text.Length, fakeResult.End);
//         Assert.Equal("def", fakeResult.Value);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.Double when the scanner returns failure.
    /// Checks that the method returns false and the result is not set.
    /// </summary>
//     [Fact] [Error] (236-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_DoubleQuote_Failure_ReturnsFalse()
//     {
//         // Arrange
//         string text = "\"def\"";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadDoubleQuotedStringDelegate = () =>
//             {
//                 return false;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Double);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.False(parseSuccess);
//         Assert.False(fakeResult.SetCalled);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.SingleOrDouble when the scanner returns success.
    /// Checks that the method returns true and the result is set correctly.
    /// </summary>
//     [Fact] [Error] (261-17)CS0841 Cannot use local variable 'fakeScanner' before it is declared [Error] (272-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_SingleOrDouble_Success_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         // Here we simulate a quoted string using double quotes.
//         string text = "\"ghi\"";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadQuotedStringDelegate = () =>
//             {
//                 fakeScanner.Cursor.Offset = text.Length;
//                 return true;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.SingleOrDouble);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.True(parseSuccess);
//         Assert.True(fakeResult.SetCalled);
//         Assert.Equal(0, fakeResult.Start);
//         Assert.Equal(text.Length, fakeResult.End);
//         Assert.Equal("ghi", fakeResult.Value);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.SingleOrDouble when the scanner returns failure.
    /// Checks that the method returns false and the result is not set.
    /// </summary>
//     [Fact] [Error] (309-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_SingleOrDouble_Failure_ReturnsFalse()
//     {
//         // Arrange
//         string text = "\"ghi\"";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadQuotedStringDelegate = () =>
//             {
//                 return false;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.SingleOrDouble);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.False(parseSuccess);
//         Assert.False(fakeResult.SetCalled);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.Backtick when the scanner returns success.
    /// Checks that the method returns true and the result is set correctly.
    /// </summary>
//     [Fact] [Error] (333-17)CS0841 Cannot use local variable 'fakeScanner' before it is declared [Error] (344-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_Backtick_Success_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         string text = "`jkl`";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadBacktickStringDelegate = () =>
//             {
//                 fakeScanner.Cursor.Offset = text.Length;
//                 return true;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Backtick);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.True(parseSuccess);
//         Assert.True(fakeResult.SetCalled);
//         Assert.Equal(0, fakeResult.Start);
//         Assert.Equal(text.Length, fakeResult.End);
//         Assert.Equal("jkl", fakeResult.Value);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.Backtick when the scanner returns failure.
    /// Checks that the method returns false and the result is not set.
    /// </summary>
//     [Fact] [Error] (381-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_Backtick_Failure_ReturnsFalse()
//     {
//         // Arrange
//         string text = "`jkl`";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadBacktickStringDelegate = () =>
//             {
//                 return false;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Backtick);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.False(parseSuccess);
//         Assert.False(fakeResult.SetCalled);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.Custom when the scanner returns success.
    /// Checks that the method returns true and the result is set correctly using ExpectedChars.
    /// </summary>
//     [Fact] [Error] (406-17)CS0841 Cannot use local variable 'fakeScanner' before it is declared [Error] (418-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_Custom_Success_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         string text = "\"mno\"";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadQuotedStringCustomDelegate = (expectedChars) =>
//             {
//                 // In a custom scenario, we simulate similar behavior as ReadQuotedString.
//                 fakeScanner.Cursor.Offset = text.Length;
//                 return true;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         // Using the custom constructor by passing a char quote.
//         var stringLiteral = new StringLiteral('x'); // This constructor sets up the custom quote scenario internally.
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.True(parseSuccess);
//         Assert.True(fakeResult.SetCalled);
//         Assert.Equal(0, fakeResult.Start);
//         Assert.Equal(text.Length, fakeResult.End);
//         Assert.Equal("mno", fakeResult.Value);
//     }

    /// <summary>
    /// Tests the Parse method for StringLiteralQuotes.Custom when the scanner returns failure.
    /// Checks that the method returns false and the result is not set.
    /// </summary>
//     [Fact] [Error] (455-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_Custom_Failure_ReturnsFalse()
//     {
//         // Arrange
//         string text = "\"mno\"";
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = 0
//             },
//             ReadQuotedStringCustomDelegate = (expectedChars) =>
//             {
//                 return false;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral('x'); // Custom scenario
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.False(parseSuccess);
//         Assert.False(fakeResult.SetCalled);
//     }

    /// <summary>
    /// Tests the Parse method when the scanner's cursor has a non-zero starting offset.
    /// Verifies that the method correctly calculates start and end positions and extracts the inner content.
    /// </summary>
//     [Fact] [Error] (483-17)CS0841 Cannot use local variable 'fakeScanner' before it is declared [Error] (494-77)CS1510 A ref or out value must be an assignable variable
//     public void Parse_NonZeroStart_Success_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         // Create a buffer with extra characters before the quoted string.
//         string prefix = "xxx";
//         string quotedPart = "'abc'";
//         string text = prefix + quotedPart;
//         var fakeScanner = new FakeScanner
//         {
//             Buffer = text,
//             Cursor = new FakeCursor
//             {
//                 Offset = prefix.Length
//             },
//             ReadSingleQuotedStringDelegate = () =>
//             {
//                 // Simulate parsing from the non-zero offset.
//                 fakeScanner.Cursor.Offset = prefix.Length + quotedPart.Length;
//                 return true;
//             }
//         };
//         var fakeContext = new FakeParseContext
//         {
//             Scanner = fakeScanner
//         };
//         var fakeResult = new FakeParseResult();
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Single);
//         // Act
//         bool parseSuccess = stringLiteral.Parse(fakeContext as dynamic, ref fakeResult as dynamic);
//         // Assert
//         Assert.True(parseSuccess);
//         Assert.True(fakeResult.SetCalled);
//         Assert.Equal(prefix.Length, fakeResult.Start);
//         Assert.Equal(prefix.Length + quotedPart.Length, fakeResult.End);
//         // The expected decoded value is the inner content of the quoted part.
//         Assert.Equal("abc", fakeResult.Value);
//     }

    /// <summary>
    /// A fake implementation of a compilation result used for testing.
    /// </summary>
    /// <typeparam name = "T">The type argument.</typeparam>
    private class FakeCompilationResult<T>
    {
        /// <summary>
        /// Gets the list of variables added during compilation.
        /// </summary>
        public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
        /// <summary>
        /// Gets the list of body expressions generated during compilation.
        /// </summary>
        public List<Expression> Body { get; } = new List<Expression>();
        /// <summary>
        /// Gets or sets the success expression.
        /// </summary>
        public ParameterExpression Success { get; set; }
        /// <summary>
        /// Gets or sets the value expression.
        /// </summary>
        public Expression Value { get; set; }
    }

    /// <summary>
    /// A fake implementation of the CompilationContext to simulate required behavior.
    /// </summary>
    private class FakeCompilationContext
    {
        private int _nextNumber = 1;
        /// <summary>
        /// Gets or sets a value indicating whether the result should be discarded.
        /// </summary>
        public bool DiscardResult { get; set; }

        /// <summary>
        /// Returns a constant expression representing an offset.
        /// </summary>
        public Expression Offset() => Expression.Constant(100);
        /// <summary>
        /// Returns a constant expression representing the buffer.
        /// </summary>
        public Expression Buffer() => Expression.Constant("Buffer");
        /// <summary>
        /// Gets the next number for unique naming.
        /// </summary>
        public int NextNumber => _nextNumber++;

        /// <summary>
        /// Simulates reading a single quoted string.
        /// </summary>
        public Expression ReadSingleQuotedString() => Expression.Constant(true, typeof(bool));
        /// <summary>
        /// Simulates reading a double quoted string.
        /// </summary>
        public Expression ReadDoubleQuotedString() => Expression.Constant(true, typeof(bool));
        /// <summary>
        /// Simulates reading a quoted string.
        /// </summary>
        public Expression ReadQuotedString() => Expression.Constant(true, typeof(bool));
        /// <summary>
        /// Simulates reading a backtick string.
        /// </summary>
        public Expression ReadBacktickString() => Expression.Constant(true, typeof(bool));
        /// <summary>
        /// Records the expression argument passed to ReadCustomString.
        /// </summary>
        public Expression LastCustomStringArg { get; private set; }

        /// <summary>
        /// Simulates reading a custom string using expected characters.
        /// </summary>
        /// <param name = "expectedChars">The expected characters expression.</param>
        public Expression ReadCustomString(Expression expectedChars)
        {
            LastCustomStringArg = expectedChars;
            return Expression.Constant(true, typeof(bool));
        }

        /// <summary>
        /// Creates a fake compilation result.
        /// </summary>
        /// <typeparam name = "T">The type parameter.</typeparam>
        public FakeCompilationResult<T> CreateCompilationResult<T>()
        {
            var result = new FakeCompilationResult<T>
            {
                Success = Expression.Parameter(typeof(bool), "success")
            };
            return result;
        }

        /// <summary>
        /// Simulates the creation of a new TextSpan expression.
        /// </summary>
        /// <param name = "buffer">The buffer expression.</param>
        /// <param name = "offset">The offset expression.</param>
        /// <param name = "length">The length expression.</param>
        public Expression NewTextSpan(Expression buffer, Expression offset, Expression length)
        {
            // For testing purposes, we simply construct a NewExpression for the TextSpan struct.
            var constructor = typeof(TextSpan).GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });
            return Expression.New(constructor, buffer, offset, length);
        }
    }

    /// <summary>
    /// A minimal stub of the TextSpan struct used in compilation.
    /// </summary>
    public struct TextSpan
    {
        public string Buffer { get; }
        public int Offset { get; }
        public int Length { get; }

        public TextSpan(string buffer, int offset, int length)
        {
            Buffer = buffer;
            Offset = offset;
            Length = length;
        }
    }

    /// <summary>
    /// Tests that Compile builds a proper CompilationResult when using a single quoted string and DiscardResult is false.
    /// The test verifies that expected variables and body expressions are added and that the if-then block is constructed correctly.
    /// </summary>
//     [Fact] [Error] (641-44)CS1503 Argument 1: cannot convert from 'StringLiteralTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
//     public void Compile_WithSingleQuote_DiscardResultFalse_BuildsCompilationResultCorrectly()
//     {
//         // Arrange
//         var fakeContext = new FakeCompilationContext
//         {
//             DiscardResult = false
//         };
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Single);
//         // Act
//         var result = stringLiteral.Compile(fakeContext);
//         // Assert
//         // Verify that exactly one variable (the start variable) is added.
//         Assert.Single(result.Variables);
//         // Verify that two body expressions are present.
//         Assert.Equal(2, result.Body.Count);
//         // Verify the first body expression is an assignment expression to the start variable with a name starting with "start".
//         var assignExpr = result.Body[0] as BinaryExpression;
//         Assert.NotNull(assignExpr);
//         var variable = assignExpr.Left as ParameterExpression;
//         Assert.NotNull(variable);
//         Assert.StartsWith("start", variable.Name);
//         // Verify that the second body expression is a conditional (if-then) expression.
//         var ifThenExpr = result.Body[1] as ConditionalExpression;
//         Assert.NotNull(ifThenExpr);
//         // Verify that the test part of the conditional is the expression returned from ReadSingleQuotedString.
//         var testExpr = ifThenExpr.Test as ConstantExpression;
//         Assert.NotNull(testExpr);
//         Assert.Equal(true, testExpr.Value);
//         // Verify that the true branch is a block expression with three expressions.
//         var blockExpr = ifThenExpr.IfTrue as BlockExpression;
//         Assert.NotNull(blockExpr);
//         Assert.Equal(3, blockExpr.Expressions.Count);
//         // Since DiscardResult is false, the third expression in the block should be an assignment to the result.Value.
//         var thirdExpr = blockExpr.Expressions[2] as BinaryExpression;
//         Assert.NotNull(thirdExpr);
//         var methodCall = thirdExpr.Right as MethodCallExpression;
//         Assert.NotNull(methodCall);
//         Assert.NotNull(methodCall.Method);
//     }

    /// <summary>
    /// Tests that Compile builds a proper CompilationResult when using a single quoted string and DiscardResult is true.
    /// The test verifies that the if-then branch handles the discard scenario by including an empty expression.
    /// </summary>
//     [Fact] [Error] (686-44)CS1503 Argument 1: cannot convert from 'StringLiteralTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
//     public void Compile_WithSingleQuote_DiscardResultTrue_BuildsCompilationResultCorrectly()
//     {
//         // Arrange
//         var fakeContext = new FakeCompilationContext
//         {
//             DiscardResult = true
//         };
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Single);
//         // Act
//         var result = stringLiteral.Compile(fakeContext);
//         // Assert
//         // Verify that one variable is added.
//         Assert.Single(result.Variables);
//         // Verify that the result body has two expressions.
//         Assert.Equal(2, result.Body.Count);
//         // Extract the conditional expression from the body.
//         var ifThenExpr = result.Body[1] as ConditionalExpression;
//         Assert.NotNull(ifThenExpr);
//         // Extract the block in the true branch.
//         var blockExpr = ifThenExpr.IfTrue as BlockExpression;
//         Assert.NotNull(blockExpr);
//         // Since DiscardResult is true, the third expression in the block should be an empty (default) expression.
//         var thirdExpr = blockExpr.Expressions[2];
//         Assert.IsType<DefaultExpression>(thirdExpr);
//     }

    /// <summary>
    /// Tests that Compile builds a proper CompilationResult when using a custom quote.
    /// The test verifies that the ReadCustomString method is called and the expected argument is captured.
    /// </summary>
//     [Fact] [Error] (717-44)CS1503 Argument 1: cannot convert from 'StringLiteralTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
//     public void Compile_WithCustomQuote_DiscardResultFalse_CallsReadCustomString()
//     {
//         // Arrange
//         var fakeContext = new FakeCompilationContext
//         {
//             DiscardResult = false
//         };
//         var stringLiteral = new StringLiteral(StringLiteralQuotes.Custom);
//         // Act
//         var result = stringLiteral.Compile(fakeContext);
//         // Assert
//         // Verify that the ReadCustomString method was called by checking the captured argument.
//         Assert.NotNull(fakeContext.LastCustomStringArg);
//         var constExpr = fakeContext.LastCustomStringArg as ConstantExpression;
//         Assert.NotNull(constExpr);
//     }

    /// <summary>
    /// Tests that the constructor with a valid <see cref = "StringLiteralQuotes"/> value (Single) does not throw,
    /// sets the Name property, and assigns a non-null ExpectedChars array.
    /// </summary>
    [Fact]
    public void StringLiteral_Constructor_WithSingleQuote_ShouldSetNameAndExpectedChars()
    {
        // Arrange & Act
        var instance = new StringLiteral(StringLiteralQuotes.Single);
        // Assert
        Assert.NotNull(instance);
        Assert.Equal("StringLiteral", instance.Name);
        Assert.NotNull(instance.ExpectedChars);
    }

    /// <summary>
    /// Tests that the constructor with a valid <see cref = "StringLiteralQuotes"/> value (Double) does not throw,
    /// sets the Name property, and assigns a non-null ExpectedChars array.
    /// </summary>
    [Fact]
    public void StringLiteral_Constructor_WithDoubleQuote_ShouldSetNameAndExpectedChars()
    {
        // Arrange & Act
        var instance = new StringLiteral(StringLiteralQuotes.Double);
        // Assert
        Assert.NotNull(instance);
        Assert.Equal("StringLiteral", instance.Name);
        Assert.NotNull(instance.ExpectedChars);
    }

    /// <summary>
    /// Tests that the constructor with a valid <see cref = "StringLiteralQuotes"/> value (Backtick) does not throw,
    /// sets the Name property, and assigns a non-null ExpectedChars array.
    /// </summary>
    [Fact]
    public void StringLiteral_Constructor_WithBacktickQuote_ShouldSetNameAndExpectedChars()
    {
        // Arrange & Act
        var instance = new StringLiteral(StringLiteralQuotes.Backtick);
        // Assert
        Assert.NotNull(instance);
        Assert.Equal("StringLiteral", instance.Name);
        Assert.NotNull(instance.ExpectedChars);
    }

    /// <summary>
    /// Tests that the constructor with a valid <see cref = "StringLiteralQuotes"/> value (SingleOrDouble) does not throw,
    /// sets the Name property, and assigns a non-null ExpectedChars array.
    /// </summary>
    [Fact]
    public void StringLiteral_Constructor_WithSingleOrDoubleQuote_ShouldSetNameAndExpectedChars()
    {
        // Arrange & Act
        var instance = new StringLiteral(StringLiteralQuotes.SingleOrDouble);
        // Assert
        Assert.NotNull(instance);
        Assert.Equal("StringLiteral", instance.Name);
        Assert.NotNull(instance.ExpectedChars);
    }

    /// <summary>
    /// Tests that the constructor with an invalid <see cref = "StringLiteralQuotes"/> value (Custom) throws an InvalidOperationException.
    /// </summary>
    [Fact]
    public void StringLiteral_Constructor_WithCustomQuote_ThrowsInvalidOperationException()
    {
        // Arrange, Act & Assert
        Assert.Throws<InvalidOperationException>(() => new StringLiteral(StringLiteralQuotes.Custom));
    }

    /// <summary>
    /// Tests that the constructor accepting a char does not throw when provided with a typical quote character.
    /// </summary>
    [Fact]
    public void StringLiteral_Constructor_WithChar_DoesNotThrow()
    {
        // Arrange
        char testQuote = '\"';
        // Act
        var instance = new StringLiteral(testQuote);
        // Assert
        Assert.NotNull(instance);
    // Behavior for the char constructor is not defined in the snippet,
    // but the test asserts that an instance is successfully created.
    }

    /// <summary>
    /// Tests that the constructor accepting a <see cref = "StringLiteralQuotes"/> parameter
    /// creates a valid instance and sets the default properties.
    /// This test covers the happy path for the overload with the enum parameter.
    /// </summary>
    [Fact]
    public void Constructor_WithStringLiteralQuotesParameter_CreatesInstance()
    {
        // Arrange
        var quoteKind = StringLiteralQuotes.Single;
        // Act
        var stringLiteral = new StringLiteral(quoteKind);
        // Assert
        Assert.NotNull(stringLiteral);
        // Validate that CanSeek property is true as initialized by default.
        Assert.True(stringLiteral.CanSeek, "Expected CanSeek to be true.");
    }

    /// <summary>
    /// Tests that the constructor accepting a char parameter with a single quote
    /// initializes the instance with correct ExpectedChars, Name, and internal quote type.
    /// This test covers the happy path for the char overload when the quote is a single quote.
    /// </summary>
    [Fact]
    public void Constructor_WithSingleQuote_SetsExpectedCharsAndName()
    {
        // Arrange
        char singleQuote = '\'';
        // Act
        var stringLiteral = new StringLiteral(singleQuote);
        // Assert
        Assert.NotNull(stringLiteral);
        Assert.True(stringLiteral.CanSeek, "Expected CanSeek to be true.");
        Assert.NotNull(stringLiteral.ExpectedChars);
        Assert.Single(stringLiteral.ExpectedChars);
        Assert.Equal(singleQuote, stringLiteral.ExpectedChars[0]);
        // Since Name is set in the constructor, we expect it to be "StringLiteral"
        // Assuming the Name property is accessible from the base class.
        var nameProperty = stringLiteral.GetType().GetProperty("Name");
        Assert.NotNull(nameProperty);
        var nameValue = nameProperty.GetValue(stringLiteral) as string;
        Assert.Equal("StringLiteral", nameValue);
    }

    /// <summary>
    /// Tests that the constructor accepting a char parameter with a double quote
    /// initializes the instance with correct ExpectedChars and Name.
    /// This test covers the happy path for the char overload when the quote is a double quote.
    /// </summary>
    [Fact]
    public void Constructor_WithDoubleQuote_SetsExpectedCharsAndName()
    {
        // Arrange
        char doubleQuote = '\"';
        // Act
        var stringLiteral = new StringLiteral(doubleQuote);
        // Assert
        Assert.NotNull(stringLiteral);
        Assert.True(stringLiteral.CanSeek, "Expected CanSeek to be true.");
        Assert.NotNull(stringLiteral.ExpectedChars);
        Assert.Single(stringLiteral.ExpectedChars);
        Assert.Equal(doubleQuote, stringLiteral.ExpectedChars[0]);
        var nameProperty = stringLiteral.GetType().GetProperty("Name");
        Assert.NotNull(nameProperty);
        var nameValue = nameProperty.GetValue(stringLiteral) as string;
        Assert.Equal("StringLiteral", nameValue);
    }

    /// <summary>
    /// Tests that the constructor accepting a char parameter with a backtick
    /// initializes the instance with correct ExpectedChars and Name.
    /// This test covers the happy path for the char overload when the quote is a backtick.
    /// </summary>
    [Fact]
    public void Constructor_WithBacktick_SetsExpectedCharsAndName()
    {
        // Arrange
        char backtick = '`';
        // Act
        var stringLiteral = new StringLiteral(backtick);
        // Assert
        Assert.NotNull(stringLiteral);
        Assert.True(stringLiteral.CanSeek, "Expected CanSeek to be true.");
        Assert.NotNull(stringLiteral.ExpectedChars);
        Assert.Single(stringLiteral.ExpectedChars);
        Assert.Equal(backtick, stringLiteral.ExpectedChars[0]);
        var nameProperty = stringLiteral.GetType().GetProperty("Name");
        Assert.NotNull(nameProperty);
        var nameValue = nameProperty.GetValue(stringLiteral) as string;
        Assert.Equal("StringLiteral", nameValue);
    }

    /// <summary>
    /// Tests that the constructor accepting a char parameter with a non-standard quote character
    /// initializes the instance with quote type set to Custom, and sets ExpectedChars and Name correctly.
    /// This test covers the edge case for the char overload when the quote does not match standard quotes.
    /// </summary>
    [Fact]
    public void Constructor_WithCustomQuote_SetsExpectedCharsAndName()
    {
        // Arrange
        char customQuote = '*';
        // Act
        var stringLiteral = new StringLiteral(customQuote);
        // Assert
        Assert.NotNull(stringLiteral);
        Assert.True(stringLiteral.CanSeek, "Expected CanSeek to be true.");
        Assert.NotNull(stringLiteral.ExpectedChars);
        Assert.Single(stringLiteral.ExpectedChars);
        Assert.Equal(customQuote, stringLiteral.ExpectedChars[0]);
        var nameProperty = stringLiteral.GetType().GetProperty("Name");
        Assert.NotNull(nameProperty);
        var nameValue = nameProperty.GetValue(stringLiteral) as string;
        Assert.Equal("StringLiteral", nameValue);
    }

    /// <summary>
    /// Tests that the 'CanSeek' property returns true when the instance is created using the enum constructor.
    /// </summary>
    [Fact]
    public void CanSeek_EnumConstructor_ReturnsTrue()
    {
        // Arrange
        var stringLiteral = new StringLiteral(StringLiteralQuotes.Single);
        // Act
        bool canSeek = stringLiteral.CanSeek;
        // Assert
        Assert.True(canSeek, "Expected 'CanSeek' to be true when using the enum constructor.");
    }

    /// <summary>
    /// Tests that the 'CanSeek' property returns true when the instance is created using the char constructor.
    /// </summary>
    [Fact]
    public void CanSeek_CharConstructor_ReturnsTrue()
    {
        // Arrange
        var stringLiteral = new StringLiteral('\'');
        // Act
        bool canSeek = stringLiteral.CanSeek;
        // Assert
        Assert.True(canSeek, "Expected 'CanSeek' to be true when using the char constructor.");
    }

    /// <summary>
    /// Tests that the ExpectedChars property returns a non-null array when using the constructor that accepts a StringLiteralQuotes enum value.
    /// This test covers all valid enum values.
    /// </summary>
    /// <param name = "quoteValue">The integer value of the StringLiteralQuotes enum to test.</param>
    [Theory]
    [InlineData(0)] // StringLiteralQuotes.Single
    [InlineData(1)] // StringLiteralQuotes.Double
    [InlineData(2)] // StringLiteralQuotes.Backtick
    [InlineData(3)] // StringLiteralQuotes.SingleOrDouble
    [InlineData(4)] // StringLiteralQuotes.Custom
    public void ExpectedChars_Getter_ReturnsNonNullArray_ForEnumConstructor(int quoteValue)
    {
        // Arrange
        var quotes = (StringLiteralQuotes)quoteValue;
        var stringLiteral = new StringLiteral(quotes);
        // Act
        char[] result = stringLiteral.ExpectedChars;
        // Assert
        Assert.NotNull(result);
    }

    /// <summary>
    /// Tests that consecutive accesses to the ExpectedChars property return the same reference when using the constructor that accepts a StringLiteralQuotes enum value.
    /// This test ensures that the property is consistent across multiple calls.
    /// </summary>
    /// <param name = "quoteValue">The integer value of the StringLiteralQuotes enum to test.</param>
    [Theory]
    [InlineData(0)] // StringLiteralQuotes.Single
    [InlineData(1)] // StringLiteralQuotes.Double
    [InlineData(2)] // StringLiteralQuotes.Backtick
    [InlineData(3)] // StringLiteralQuotes.SingleOrDouble
    [InlineData(4)] // StringLiteralQuotes.Custom
    public void ExpectedChars_Getter_ReturnsSameReference_ForEnumConstructor(int quoteValue)
    {
        // Arrange
        var quotes = (StringLiteralQuotes)quoteValue;
        var stringLiteral = new StringLiteral(quotes);
        // Act
        char[] firstCall = stringLiteral.ExpectedChars;
        char[] secondCall = stringLiteral.ExpectedChars;
        // Assert
        Assert.Same(firstCall, secondCall);
    }

    /// <summary>
    /// Tests that the ExpectedChars property returns a non-null array when using the constructor that accepts a char value.
    /// </summary>
    [Fact]
    public void ExpectedChars_Getter_ReturnsNonNullArray_ForCharConstructor()
    {
        // Arrange
        char quote = '\'';
        var stringLiteral = new StringLiteral(quote);
        // Act
        char[] result = stringLiteral.ExpectedChars;
        // Assert
        Assert.NotNull(result);
    }

    /// <summary>
    /// Tests that consecutive accesses to the ExpectedChars property return the same reference when using the constructor that accepts a char value,
    /// ensuring that the property behaves consistently.
    /// </summary>
    [Fact]
    public void ExpectedChars_Getter_ReturnsSameReference_ForCharConstructor()
    {
        // Arrange
        char quote = '\"';
        var stringLiteral = new StringLiteral(quote);
        // Act
        char[] firstCall = stringLiteral.ExpectedChars;
        char[] secondCall = stringLiteral.ExpectedChars;
        // Assert
        Assert.Same(firstCall, secondCall);
    }

    /// <summary>
    /// Tests the <see cref = "StringLiteral.SkipWhitespace"/> property when an instance is constructed using the <see cref = "StringLiteral(StringLiteralQuotes)"/> constructor.
    /// The expected default value for SkipWhitespace is assumed to be false.
    /// </summary>
    [Fact]
    public void SkipWhitespace_UsingStringLiteralQuotesConstructor_ReturnsDefaultValue()
    {
        // Arrange
        var stringLiteral = new StringLiteral(StringLiteralQuotes.Single);
        // Act
        bool skipWhitespace = stringLiteral.SkipWhitespace;
        // Assert
        Assert.False(skipWhitespace);
    }

    /// <summary>
    /// Tests the <see cref = "StringLiteral.SkipWhitespace"/> property when an instance is constructed using the <see cref = "StringLiteral(char)"/> constructor.
    /// The expected default value for SkipWhitespace is assumed to be false.
    /// </summary>
    [Fact]
    public void SkipWhitespace_UsingCharConstructor_ReturnsDefaultValue()
    {
        // Arrange
        var stringLiteral = new StringLiteral('\'');
        // Act
        bool skipWhitespace = stringLiteral.SkipWhitespace;
        // Assert
        Assert.False(skipWhitespace);
    }
}