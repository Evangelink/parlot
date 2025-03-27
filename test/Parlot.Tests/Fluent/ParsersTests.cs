using Moq;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the members of the <see cref = "Parsers"/> class.
/// </summary>
// public class ParsersTests [Error] (1878-2)CS1027 #endif directive expected
// {
//     /// <summary>
//     /// Tests that Separated returns a non-null parser instance when provided with valid separator and element parsers.
//     /// </summary>
//     [Fact]
//     public void Separated_ValidParsers_ReturnsNonNullParser()
//     {
//         // Arrange
//         var separatorMock = new Mock<Parser<char>>();
//         var elementMock = new Mock<Parser<int>>();
//         // Act
//         Parser<IReadOnlyList<int>> result = Parsers.Separated<char, int>(separatorMock.Object, elementMock.Object);
//         // Assert
//         Assert.NotNull(result);
//     }
// 
//     /// <summary>
//     /// Tests that Separated throws an ArgumentNullException when the separator parser is null.
//     /// </summary>
//     [Fact]
//     public void Separated_NullSeparator_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Parser<char> nullSeparator = null;
//         var elementMock = new Mock<Parser<int>>();
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.Separated<char, int>(nullSeparator, elementMock.Object));
//     }
// 
//     /// <summary>
//     /// Tests that Separated throws an ArgumentNullException when the element parser is null.
//     /// </summary>
//     [Fact]
//     public void Separated_NullElementParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var separatorMock = new Mock<Parser<char>>();
//         Parser<int> nullElementParser = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.Separated<char, int>(separatorMock.Object, nullElementParser));
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace returns a non-null wrapped parser when provided with a valid parser.
//     /// The test uses a mocked parser to simulate the underlying parser dependency.
//     /// Expected outcome: The returned parser is not null and its type name includes "SkipWhiteSpace".
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_ValidParser_ReturnsWrappedParser()
//     {
//         // Arrange
//         var mockParser = new Mock<Parser<string>>();
//         // Act
//         var wrappedParser = Parsers.SkipWhiteSpace(mockParser.Object);
//         // Assert
//         Assert.NotNull(wrappedParser);
//         Assert.Contains("SkipWhiteSpace", wrappedParser.GetType().Name, StringComparison.OrdinalIgnoreCase);
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpace throws an ArgumentNullException when a null parser is provided.
//     /// Expected outcome: An ArgumentNullException is thrown.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_NullParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Parser<string> nullParser = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.SkipWhiteSpace(nullParser));
//     }
// 
//     /// <summary>
//     /// Tests the ZeroOrOne overload that accepts a default value.
//     /// Verifies that a non-null parser is returned and that its type name contains "ZeroOrOne".
//     /// </summary>
//     [Fact] [Error] (93-35)CS0246 The type or namespace name 'FakeParser<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void ZeroOrOne_WithDefaultValue_ReturnsParserOfExpectedType()
//     {
//         // Arrange
//         // Create a mock of a Parser<int>. Using Moq to simulate a dummy parser.
//         var mockParser = new Mock<FakeParser<int>>();
//         // Define a dummy default value.
//         int defaultValue = 42;
//         // Act
//         var result = Parsers.ZeroOrOne(mockParser.Object, defaultValue);
//         // Assert
//         Assert.NotNull(result);
//         // Verify the returned object's type name contains "ZeroOrOne".
//         Assert.Contains("ZeroOrOne", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests the ZeroOrOne generic overload without default value.
//     /// Verifies that a non-null parser is returned and that its type name contains "ZeroOrOne".
//     /// </summary>
//     [Fact] [Error] (113-35)CS0246 The type or namespace name 'FakeParser<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void ZeroOrOne_WithoutDefaultValue_ReturnsParserOfExpectedType()
//     {
//         // Arrange
//         // Create a mock of a Parser<string>. Using Moq to simulate a dummy parser.
//         var mockParser = new Mock<FakeParser<string>>();
//         // Act
//         var result = Parsers.ZeroOrOne(mockParser.Object);
//         // Assert
//         Assert.NotNull(result);
//         // Verify the returned object's type name contains "ZeroOrOne".
//         Assert.Contains("ZeroOrOne", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that ZeroOrOne(parser, defaultValue) returns the inner parser's result when the inner parser succeeds.
//     /// </summary>
//     [Fact] [Error] (133-71)CS1510 A ref or out value must be an assignable variable [Error] (142-72)CS1510 A ref or out value must be an assignable variable
//     public void ZeroOrOne_WithDefaultValue_InnerParserSucceeds_ReturnsInnerValue()
//     {
//         // Arrange
//         const int innerResult = 42;
//         const int providedDefault = 99;
//         var innerParserMock = new Mock<Parser<int>>();
//         // Setup the inner parser to succeed and return innerResult.
//         innerParserMock.Setup(p => p.TryParse(It.IsAny<string>(), out innerResult)).Returns(true);
//         // Create a combinator parser using the overload that accepts a default value.
//         Parser<int> zeroOrOneParser = Parsers.ZeroOrOne(innerParserMock.Object, providedDefault);
//         // Act
//         bool parseSucceeded = zeroOrOneParser.TryParse("dummy input", out int actualResult);
//         // Assert
//         Assert.True(parseSucceeded);
//         Assert.Equal(innerResult, actualResult);
//         // Verify that the underlying parser was invoked.
//         innerParserMock.Verify(p => p.TryParse(It.IsAny<string>(), out innerResult), Times.Once);
//     }
// 
//     /// <summary>
//     /// Tests that ZeroOrOne(parser, defaultValue) returns the provided default value when the inner parser fails.
//     /// </summary>
//     [Fact]
//     public void ZeroOrOne_WithDefaultValue_InnerParserFails_ReturnsProvidedDefault()
//     {
//         // Arrange
//         const int providedDefault = 99;
//         int dummyOut = default;
//         var innerParserMock = new Mock<Parser<int>>();
//         // Setup the inner parser to fail.
//         innerParserMock.Setup(p => p.TryParse(It.IsAny<string>(), out dummyOut)).Returns(false);
//         // Create a combinator parser using the overload that accepts a default value.
//         Parser<int> zeroOrOneParser = Parsers.ZeroOrOne(innerParserMock.Object, providedDefault);
//         // Act
//         bool parseSucceeded = zeroOrOneParser.TryParse("dummy input", out int actualResult);
//         // Assert
//         // Even if inner parser fails, ZeroOrOne should succeed by returning the provided default.
//         Assert.True(parseSucceeded);
//         Assert.Equal(providedDefault, actualResult);
//         // Verify that the underlying parser was invoked.
//         innerParserMock.Verify(p => p.TryParse(It.IsAny<string>(), out dummyOut), Times.Once);
//     }
// 
//     /// <summary>
//     /// Tests that ZeroOrOne(parser) returns the inner parser's result when the inner parser succeeds.
//     /// </summary>
//     [Fact] [Error] (179-71)CS1510 A ref or out value must be an assignable variable [Error] (188-72)CS1510 A ref or out value must be an assignable variable
//     public void ZeroOrOne_WithoutDefaultValue_InnerParserSucceeds_ReturnsInnerValue()
//     {
//         // Arrange
//         const int innerResult = 42;
//         var innerParserMock = new Mock<Parser<int>>();
//         // Setup the inner parser to succeed and return innerResult.
//         innerParserMock.Setup(p => p.TryParse(It.IsAny<string>(), out innerResult)).Returns(true);
//         // Create a combinator parser using the overload without a provided default.
//         Parser<int> zeroOrOneParser = Parsers.ZeroOrOne(innerParserMock.Object);
//         // Act
//         bool parseSucceeded = zeroOrOneParser.TryParse("dummy input", out int actualResult);
//         // Assert
//         Assert.True(parseSucceeded);
//         Assert.Equal(innerResult, actualResult);
//         // Verify that the underlying parser was invoked.
//         innerParserMock.Verify(p => p.TryParse(It.IsAny<string>(), out innerResult), Times.Once);
//     }
// 
//     /// <summary>
//     /// Tests that ZeroOrOne(parser) returns the default value of T when the inner parser fails.
//     /// </summary>
//     [Fact]
//     public void ZeroOrOne_WithoutDefaultValue_InnerParserFails_ReturnsDefaultValue()
//     {
//         // Arrange
//         int expectedDefault = default; // For int, default is 0.
//         int dummyOut = default;
//         var innerParserMock = new Mock<Parser<int>>();
//         // Setup the inner parser to fail.
//         innerParserMock.Setup(p => p.TryParse(It.IsAny<string>(), out dummyOut)).Returns(false);
//         // Create a combinator parser using the overload without a provided default.
//         Parser<int> zeroOrOneParser = Parsers.ZeroOrOne(innerParserMock.Object);
//         // Act
//         bool parseSucceeded = zeroOrOneParser.TryParse("dummy input", out int actualResult);
//         // Assert
//         // Even if inner parser fails, ZeroOrOne should succeed by returning the default value of T.
//         Assert.True(parseSucceeded);
//         Assert.Equal(expectedDefault, actualResult);
//         // Verify that the underlying parser was invoked.
//         innerParserMock.Verify(p => p.TryParse(It.IsAny<string>(), out dummyOut), Times.Once);
//     }
// 
//     /// <summary>
//     /// Tests that ZeroOrMany with a valid parser returns a non-null instance of type Parser&lt;IReadOnlyList&lt;T&gt; &gt;.
//     /// This test follows the Arrange-Act-Assert pattern. A fake parser is provided via Moq to simulate the input parser.
//     /// The expected outcome is that the factory method returns a valid parser instance.
//     /// </summary>
//     [Fact]
//     public void ZeroOrMany_WithValidParser_ReturnsNonNullInstance()
//     {
//         // Arrange
//         var fakeParser = new Mock<Parser<int>>().Object;
//         // Act
//         var result = Parsers.ZeroOrMany<int>(fakeParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsAssignableFrom<Parser<IReadOnlyList<int>>>(result);
//     }
// 
//     /// <summary>
//     /// Tests that ZeroOrMany throws an ArgumentNullException when a null parser is provided.
//     /// This test verifies that input validation is enforced when calling the method.
//     /// </summary>
//     [Fact]
//     public void ZeroOrMany_NullParser_ThrowsArgumentNullException()
//     {
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.ZeroOrMany<int>(null));
//     }
// 
//     /// <summary>
//     /// Tests that consecutive calls to ZeroOrMany with the same input parser yield distinct parser instances.
//     /// This test ensures that the method does not return a cached instance inadvertently.
//     /// </summary>
//     [Fact]
//     public void ZeroOrMany_CalledMultipleTimes_ReturnsDistinctInstances()
//     {
//         // Arrange
//         var fakeParser = new Mock<Parser<string>>().Object;
//         // Act
//         var result1 = Parsers.ZeroOrMany<string>(fakeParser);
//         var result2 = Parsers.ZeroOrMany<string>(fakeParser);
//         // Assert
//         Assert.NotSame(result1, result2);
//     }
// 
//     /// <summary>
//     /// Tests that the OneOrMany method returns a non-null parser instance when provided with a valid parser.
//     /// This test creates a mock parser for type int and verifies that the returned parser instance appears to be
//     /// of the expected OneOrMany type.
//     /// </summary>
//     [Fact]
//     public void OneOrMany_ValidParser_ReturnsNonNullParserInstance()
//     {
//         // Arrange: Create a mock parser for int using Moq.
//         var mockParser = new Mock<Parser<int>>();
//         // Act: Call OneOrMany with the mocked parser.
//         var result = Parsers.OneOrMany<int>(mockParser.Object);
//         // Assert: Verify that the result is not null.
//         Assert.NotNull(result);
//         // Additionally, check that the type name of result indicates that it is a OneOrMany parser.
//         // This is based on the expectation that the constructed type's name contains "OneOrMany".
//         Assert.Contains("OneOrMany", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that the OneOrMany method throws an ArgumentNullException when passed a null parser.
//     /// This verifies the method's defensive programming against null arguments.
//     /// </summary>
//     [Fact]
//     public void OneOrMany_NullParser_ThrowsArgumentNullException()
//     {
//         // Arrange, Act & Assert: Pass a null value and verify that an ArgumentNullException is thrown.
//         Assert.Throws<ArgumentNullException>(() => Parsers.OneOrMany<int>(null));
//     }
// 
//     /// <summary>
//     /// Tests that Parsers.Not returns a Not parser instance when provided with a valid inner parser.
//     /// </summary>
//     [Fact]
//     public void Not_ValidParser_ReturnsNotParserInstance()
//     {
//         // Arrange
//         // Create a dummy parser instance using Moq.
//         var dummyParserMock = new Mock<Parser<int>>();
//         // Act
//         var notParser = Parsers.Not(dummyParserMock.Object);
//         // Assert
//         Assert.NotNull(notParser);
//         // Verify that the returned parser is of a type whose name starts with "Not" to indicate it is the Not parser.
//         Assert.StartsWith("Not", notParser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that Parsers.Not throws an ArgumentNullException when a null parser is provided.
//     /// </summary>
//     [Fact]
//     public void Not_NullParser_ThrowsArgumentNullException()
//     {
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.Not<int>(null));
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;C, S, T&gt;(Func&lt;C, S?, bool&gt; predicate, S? state, Parser&lt;T&gt; parser) overload.
//     /// Verifies that a non-null parser is returned and its type name indicates an "If" parser.
//     /// </summary>
//     [Fact] [Error] (324-31)CS1729 'ParsersTests.DummyParser<string>' does not contain a constructor that takes 1 arguments [Error] (331-30)CS0311 The type 'ParseContext' cannot be used as type parameter 'C' in the generic type or method 'Parsers.If<C, S, T>(Func<C, S?, bool>, S?, Parser<T>)'. There is no implicit reference conversion from 'ParseContext' to 'Parlot.Fluent.ParseContext'.
//     public void If_WithGenericPredicateAndState_ReturnsIfParser()
//     {
//         // Arrange
//         var dummyParser = new DummyParser<string>("dummy");
//         string state = "testState";
//         bool Predicate(ParseContext context, string s)
//         {
//             return true;
//         };
//         // Act
//         var result = Parsers.If<ParseContext, string, string>(Predicate, state, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name, StringComparison.Ordinal);
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;S, T&gt;(Func&lt;ParseContext, S?, bool&gt; predicate, S? state, Parser&lt;T&gt; parser) overload.
//     /// Verifies that a non-null parser is returned and its type name indicates an "If" parser.
//     /// </summary>
//     [Fact] [Error] (345-31)CS1729 'ParsersTests.DummyParser<string>' does not contain a constructor that takes 1 arguments [Error] (352-49)CS1503 Argument 1: cannot convert from 'method group' to 'System.Func<Parlot.Fluent.ParseContext, string?, bool>'
//     public void If_WithParseContextPredicateAndState_ReturnsIfParser()
//     {
//         // Arrange
//         var dummyParser = new DummyParser<string>("dummy");
//         string state = "testState";
//         bool Predicate(ParseContext context, string s)
//         {
//             return true;
//         };
//         // Act
//         var result = Parsers.If<string, string>(Predicate, state, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name, StringComparison.Ordinal);
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;C, T&gt;(Func&lt;C, bool&gt; predicate, Parser&lt;T&gt; parser) overload.
//     /// Verifies that a non-null parser is returned and its type name indicates an "If" parser.
//     /// </summary>
//     [Fact] [Error] (366-31)CS1729 'ParsersTests.DummyParser<string>' does not contain a constructor that takes 1 arguments [Error] (372-30)CS0311 The type 'ParseContext' cannot be used as type parameter 'C' in the generic type or method 'Parsers.If<C, T>(Func<C, bool>, Parser<T>)'. There is no implicit reference conversion from 'ParseContext' to 'Parlot.Fluent.ParseContext'.
//     public void If_WithGenericPredicate_ReturnsIfParser()
//     {
//         // Arrange
//         var dummyParser = new DummyParser<string>("dummy");
//         bool Predicate(ParseContext context)
//         {
//             return true;
//         };
//         // Act
//         var result = Parsers.If<ParseContext, string>(Predicate, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name, StringComparison.Ordinal);
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;T&gt;(Func&lt;ParseContext, bool&gt; predicate, Parser&lt;T&gt; parser) overload.
//     /// Verifies that a non-null parser is returned and its type name indicates an "If" parser.
//     /// </summary>
//     [Fact] [Error] (386-31)CS1729 'ParsersTests.DummyParser<string>' does not contain a constructor that takes 1 arguments [Error] (392-41)CS1503 Argument 1: cannot convert from 'method group' to 'System.Func<Parlot.Fluent.ParseContext, bool>'
//     public void If_WithParseContextPredicate_ReturnsIfParser()
//     {
//         // Arrange
//         var dummyParser = new DummyParser<string>("dummy");
//         bool Predicate(ParseContext context)
//         {
//             return true;
//         };
//         // Act
//         var result = Parsers.If<string>(Predicate, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name, StringComparison.Ordinal);
//     }
// 
//     /// <summary>
//     /// Dummy implementation of ParseContext for testing purposes.
//     /// </summary>
//     private class DummyParseContext : ParseContext
//     {
//     }
// 
//     /// <summary>
//     /// Dummy implementation of Parser for testing purposes.
//     /// </summary>
//     /// <typeparam name = "T">The type of result produced by the parser.</typeparam>
//     private class DummyParser<T> : Parser<T> [Error] (409-19)CS0534 'ParsersTests.DummyParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;C, S, T&gt; overload with a valid predicate and state, expecting a non-null parser instance.
//     /// </summary>
//     [Fact] [Error] (424-30)CS0311 The type 'ParsersTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'Parsers.If<C, S, T>(Func<C, S?, bool>, S?, Parser<T>)'. There is no implicit reference conversion from 'ParsersTests.DummyParseContext' to 'Parlot.Fluent.ParseContext'.
//     public void If_GenericPredicateWithState_ReturnsNonNullParser()
//     {
//         // Arrange
//         Func<DummyParseContext, string, bool> predicate = (ctx, state) => true;
//         string stateValue = "dummyState";
//         var dummyParser = new DummyParser<int>();
//         // Act
//         var result = Parsers.If<DummyParseContext, string, int>(predicate, stateValue, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;S, T&gt; overload with a valid predicate and state, expecting a non-null parser instance.
//     /// </summary>
//     [Fact] [Error] (441-46)CS1503 Argument 1: cannot convert from 'System.Func<ParseContext, string, bool>' to 'System.Func<Parlot.Fluent.ParseContext, string?, bool>'
//     public void If_ParseContextPredicateWithState_ReturnsNonNullParser()
//     {
//         // Arrange
//         Func<ParseContext, string, bool> predicate = (ctx, state) => true;
//         string stateValue = "dummyState";
//         var dummyParser = new DummyParser<int>();
//         // Act
//         var result = Parsers.If<string, int>(predicate, stateValue, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;C, T&gt; overload with a predicate that returns true, expecting a non-null parser instance.
//     /// </summary>
//     [Fact] [Error] (457-30)CS0311 The type 'ParsersTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'Parsers.If<C, T>(Func<C, bool>, Parser<T>)'. There is no implicit reference conversion from 'ParsersTests.DummyParseContext' to 'Parlot.Fluent.ParseContext'.
//     public void If_GenericPredicateWithoutState_ReturnsNonNullParser()
//     {
//         // Arrange
//         Func<DummyParseContext, bool> predicate = (ctx) => true;
//         var dummyParser = new DummyParser<int>();
//         // Act
//         var result = Parsers.If<DummyParseContext, int>(predicate, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;T&gt; overload with a predicate that returns true, expecting a non-null parser instance.
//     /// </summary>
//     [Fact] [Error] (473-38)CS1503 Argument 1: cannot convert from 'System.Func<ParseContext, bool>' to 'System.Func<Parlot.Fluent.ParseContext, bool>'
//     public void If_ParseContextPredicateWithoutState_ReturnsNonNullParser()
//     {
//         // Arrange
//         Func<ParseContext, bool> predicate = (ctx) => true;
//         var dummyParser = new DummyParser<int>();
//         // Act
//         var result = Parsers.If<int>(predicate, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;C, T&gt; overload with a predicate that returns false, expecting a non-null parser instance.
//     /// </summary>
//     [Fact] [Error] (489-30)CS0311 The type 'ParsersTests.DummyParseContext' cannot be used as type parameter 'C' in the generic type or method 'Parsers.If<C, T>(Func<C, bool>, Parser<T>)'. There is no implicit reference conversion from 'ParsersTests.DummyParseContext' to 'Parlot.Fluent.ParseContext'.
//     public void If_GenericPredicateReturningFalse_ReturnsNonNullParser()
//     {
//         // Arrange
//         Func<DummyParseContext, bool> predicate = (ctx) => false;
//         var dummyParser = new DummyParser<int>();
//         // Act
//         var result = Parsers.If<DummyParseContext, int>(predicate, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests the If&lt;T&gt; overload with a predicate that returns false, expecting a non-null parser instance.
//     /// </summary>
//     [Fact] [Error] (505-38)CS1503 Argument 1: cannot convert from 'System.Func<ParseContext, bool>' to 'System.Func<Parlot.Fluent.ParseContext, bool>'
//     public void If_ParseContextPredicateReturningFalse_ReturnsNonNullParser()
//     {
//         // Arrange
//         Func<ParseContext, bool> predicate = (ctx) => false;
//         var dummyParser = new DummyParser<int>();
//         // Act
//         var result = Parsers.If<int>(predicate, dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("If", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that the Deferred method returns a non-null instance when invoked with a value type.
//     /// </summary>
//     [Fact]
//     public void Deferred_WithValueType_ReturnsNonNullInstance()
//     {
//         // Arrange & Act
//         var deferred = Parsers.Deferred<int>();
//         // Assert
//         Assert.NotNull(deferred);
//         Assert.IsType<Deferred<int>>(deferred);
//     }
// 
//     /// <summary>
//     /// Tests that the Deferred method returns a non-null instance when invoked with a reference type.
//     /// </summary>
//     [Fact]
//     public void Deferred_WithReferenceType_ReturnsNonNullInstance()
//     {
//         // Arrange & Act
//         var deferred = Parsers.Deferred<string>();
//         // Assert
//         Assert.NotNull(deferred);
//         Assert.IsType<Deferred<string>>(deferred);
//     }
// 
//     /// <summary>
//     /// Tests that multiple invocations of the Deferred method produce distinct instances.
//     /// </summary>
//     [Fact]
//     public void Deferred_MultipleCalls_ReturnDistinctInstances()
//     {
//         // Arrange & Act
//         var deferredOne = Parsers.Deferred<int>();
//         var deferredTwo = Parsers.Deferred<int>();
//         // Assert
//         Assert.NotSame(deferredOne, deferredTwo);
//     }
// 
//     /// <summary>
//     /// Tests that the Recursive method returns a non-null Deferred instance when provided with a valid lambda.
//     /// The lambda is expected to be invoked exactly once.
//     /// </summary>
//     [Fact] [Error] (563-24)CS1729 'ParsersTests.DummyParser<int>' does not contain a constructor that takes 1 arguments
//     public void Recursive_WithValidLambda_ReturnsNonNullDeferred()
//     {
//         // Arrange
//         int lambdaInvocationCount = 0;
//         // Act
//         Deferred<int> deferred = Parsers.Recursive<int>(d =>
//         {
//             lambdaInvocationCount++;
//             return new DummyParser<int>(42);
//         });
//         // Assert
//         Assert.NotNull(deferred);
//         Assert.Equal(1, lambdaInvocationCount);
//     }
// 
//     /// <summary>
//     /// Tests that the Recursive method throws an ArgumentNullException when provided a null lambda.
//     /// </summary>
//     [Fact]
//     public void Recursive_WithNullLambda_ThrowsArgumentNullException()
//     {
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.Recursive<int>(null));
//     }
// 
//     /// <summary>
//     /// A dummy implementation of Parser<T> to use in tests.
//     /// </summary>
//     /// <typeparam name = "T">The type of the parser result.</typeparam>
//     private class DummyParser<T> : Parser<T> [Error] (584-19)CS0102 The type 'ParsersTests' already contains a definition for 'DummyParser'
//     {
//     // No-op implementation for testing purposes.
//     }
// 
//     /// <summary>
//     /// Tests the Between method with valid non-null parsers to ensure it returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void Between_HappyPath_ReturnsBetweenParser()
//     {
//         // Arrange
//         var beforeParser = new DummyParser<string>();
//         var mainParser = new DummyParser<int>();
//         var afterParser = new DummyParser<double>();
//         // Act
//         var result = Parsers.Between(beforeParser, mainParser, afterParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("Between", result.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests the Between method when the 'before' parser is null and expects an ArgumentNullException.
//     /// </summary>
//     [Fact]
//     public void Between_NullBeforeParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         DummyParser<string> beforeParser = null;
//         var mainParser = new DummyParser<int>();
//         var afterParser = new DummyParser<double>();
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.Between(beforeParser, mainParser, afterParser));
//     }
// 
//     /// <summary>
//     /// Tests the Between method when the primary parser is null and expects an ArgumentNullException.
//     /// </summary>
//     [Fact]
//     public void Between_NullMainParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var beforeParser = new DummyParser<string>();
//         DummyParser<int> mainParser = null;
//         var afterParser = new DummyParser<double>();
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.Between(beforeParser, mainParser, afterParser));
//     }
// 
//     /// <summary>
//     /// Tests the Between method when the 'after' parser is null and expects an ArgumentNullException.
//     /// </summary>
//     [Fact]
//     public void Between_NullAfterParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var beforeParser = new DummyParser<string>();
//         var mainParser = new DummyParser<int>();
//         DummyParser<double> afterParser = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.Between(beforeParser, mainParser, afterParser));
//     }
// 
//     /// <summary>
//     /// Tests the AnyCharBefore method using default parameter values.
//     /// Verifies that the returned parser is of type TextBefore and its boolean flags are set to false.
//     /// </summary>
//     [Fact]
//     public void AnyCharBefore_DefaultParameters_ReturnsTextBeforeParser()
//     {
//         // Arrange
//         var dummyParserMock = new Mock<Parser<int>>();
//         var dummyParser = dummyParserMock.Object;
//         // Act
//         var result = Parsers.AnyCharBefore(dummyParser);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal("TextBefore`1", result.GetType().Name);
//         var type = result.GetType();
//         bool? canBeEmpty = GetBooleanField(type, result, new[] { "canBeEmpty", "_canBeEmpty" });
//         bool? failOnEof = GetBooleanField(type, result, new[] { "failOnEof", "_failOnEof" });
//         bool? consumeDelimiter = GetBooleanField(type, result, new[] { "consumeDelimiter", "_consumeDelimiter" });
//         Assert.True(canBeEmpty.HasValue && canBeEmpty.Value == false, "Expected default canBeEmpty to be false.");
//         Assert.True(failOnEof.HasValue && failOnEof.Value == false, "Expected default failOnEof to be false.");
//         Assert.True(consumeDelimiter.HasValue && consumeDelimiter.Value == false, "Expected default consumeDelimiter to be false.");
//     }
// 
//     /// <summary>
//     /// Tests the AnyCharBefore method with custom boolean parameter values.
//     /// Verifies that the returned parser is of type TextBefore and its internal boolean flags match the specified values.
//     /// </summary>
//     [Fact]
//     public void AnyCharBefore_CustomParameters_ReturnsTextBeforeParser()
//     {
//         // Arrange
//         var dummyParserMock = new Mock<Parser<int>>();
//         var dummyParser = dummyParserMock.Object;
//         bool canBeEmpty = true;
//         bool failOnEof = true;
//         bool consumeDelimiter = true;
//         // Act
//         var result = Parsers.AnyCharBefore(dummyParser, canBeEmpty, failOnEof, consumeDelimiter);
//         // Assert
//         Assert.NotNull(result);
//         Assert.Equal("TextBefore`1", result.GetType().Name);
//         var type = result.GetType();
//         bool? actualCanBeEmpty = GetBooleanField(type, result, new[] { "canBeEmpty", "_canBeEmpty" });
//         bool? actualFailOnEof = GetBooleanField(type, result, new[] { "failOnEof", "_failOnEof" });
//         bool? actualConsumeDelimiter = GetBooleanField(type, result, new[] { "consumeDelimiter", "_consumeDelimiter" });
//         Assert.True(actualCanBeEmpty.HasValue && actualCanBeEmpty.Value == canBeEmpty, $"Expected canBeEmpty to be {canBeEmpty}.");
//         Assert.True(actualFailOnEof.HasValue && actualFailOnEof.Value == failOnEof, $"Expected failOnEof to be {failOnEof}.");
//         Assert.True(actualConsumeDelimiter.HasValue && actualConsumeDelimiter.Value == consumeDelimiter, $"Expected consumeDelimiter to be {consumeDelimiter}.");
//     }
// 
//     /// <summary>
//     /// Tests the AnyCharBefore method to ensure that providing a null parser argument throws an ArgumentNullException.
//     /// </summary>
//     [Fact]
//     public void AnyCharBefore_NullParser_ThrowsArgumentNullException()
//     {
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => Parsers.AnyCharBefore<int>(null !));
//     }
// 
//     /// <summary>
//     /// Helper method that retrieves a boolean field's value via reflection.
//     /// Tries a set of possible field names.
//     /// </summary>
//     /// <param name = "type">The type of the instance.</param>
//     /// <param name = "instance">The object instance from which to retrieve the field.</param>
//     /// <param name = "fieldNames">An array of possible field names.</param>
//     /// <returns>The boolean value if found; otherwise, null.</returns>
//     private static bool? GetBooleanField(Type type, object instance, string[] fieldNames)
//     {
//         foreach (var name in fieldNames)
//         {
//             var fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
//             if (fieldInfo != null && fieldInfo.FieldType == typeof(bool))
//             {
//                 return (bool)fieldInfo.GetValue(instance);
//             }
//         }
// 
//         return null;
//     }
// 
//     /// <summary>
//     /// Tests the non-generic Always() method to ensure it returns a parser that always produces null.
//     /// This test assumes that the returned parser has a Parse method that accepts a string input and returns a value.
//     /// </summary>
//     [Fact]
//     public void Always_NonGeneric_ReturnsNullOnParse()
//     {
//         // Arrange
//         var parser = Parsers.Always();
//         // Act
//         // We use an empty string as dummy input.
//         var result = parser.Parse(string.Empty);
//         // Assert
//         Assert.Null(result);
//     }
// 
//     /// <summary>
//     /// Tests the generic Always&lt;T&gt;() method to ensure it returns a parser that always produces the default value of T.
//     /// In this test, T is int so the default value should be 0.
//     /// </summary>
//     [Fact]
//     public void Always_GenericWithoutValue_ReturnsDefaultValueOnParse()
//     {
//         // Arrange
//         var parser = Parsers.Always<int>();
//         // Act
//         var result = parser.Parse(string.Empty);
//         // Assert
//         Assert.Equal(default(int), result);
//     }
// 
//     /// <summary>
//     /// Tests the generic Always&lt;T&gt;(T value) method to ensure it returns a parser that always produces the provided value.
//     /// In this test, we provide a string value and expect the parser to always return it.
//     /// </summary>
//     [Fact]
//     public void Always_GenericWithValue_ReturnsProvidedValueOnParse()
//     {
//         // Arrange
//         string expectedValue = "test";
//         var parser = Parsers.Always(expectedValue);
//         // Act
//         var result = parser.Parse(string.Empty);
//         // Assert
//         Assert.Equal(expectedValue, result);
//     }
// 
//     /// <summary>
//     /// Tests that the non-generic Always() method returns a parser that always provides the default null value.
//     /// </summary>
//     [Fact] [Error] (789-21)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void Always_NoGeneric_ReturnsParserWithDefaultNull()
//     {
//         // Arrange & Act
//         var parser = Parsers.Always();
//         // Assert
//         Assert.NotNull(parser);
//         // Use reflection to get the internal "Value" property to ensure it holds the expected default (null)
//         PropertyInfo? valueProperty = parser.GetType().GetProperty("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//         Assert.NotNull(valueProperty);
//         var value = valueProperty!.GetValue(parser);
//         Assert.Null(value);
//     }
// 
//     /// <summary>
//     /// Tests that the generic Always&lt;T&gt;() method returns a parser that always provides the default value for the type.
//     /// </summary>
//     [Fact] [Error] (806-21)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void Always_GenericWithoutArgument_ReturnsParserWithDefaultValue()
//     {
//         // Arrange & Act
//         var parser = Parsers.Always<int>();
//         // Assert
//         Assert.NotNull(parser);
//         // Retrieve the internal "Value" property using reflection and assert that it equals default(int) (which is 0)
//         PropertyInfo? valueProperty = parser.GetType().GetProperty("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//         Assert.NotNull(valueProperty);
//         var value = valueProperty!.GetValue(parser);
//         Assert.Equal(default(int), value);
//     }
// 
//     /// <summary>
//     /// Tests that the generic Always&lt;T&gt;(T value) method returns a parser that always provides the provided value.
//     /// </summary>
//     [Fact] [Error] (825-21)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//     public void Always_GenericWithArgument_ReturnsParserWithProvidedValue()
//     {
//         // Arrange
//         int expectedValue = 5;
//         // Act
//         var parser = Parsers.Always(expectedValue);
//         // Assert
//         Assert.NotNull(parser);
//         // Retrieve the internal "Value" property using reflection and assert that it equals the provided value.
//         PropertyInfo? valueProperty = parser.GetType().GetProperty("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//         Assert.NotNull(valueProperty);
//         var value = valueProperty!.GetValue(parser);
//         Assert.Equal(expectedValue, value);
//     }
// 
//     /// <summary>
//     /// Tests the Always() method (non-generic) to ensure it returns a parser that always succeeds and returns the default object value.
//     /// This test uses dynamic invocation assuming that the parser implements a Parse method which ignores the input and returns default value.
//     /// </summary>
//     [Fact]
//     public void Always_NoGeneric_ReturnsParserThatAlwaysSucceeds()
//     {
//         // Arrange & Act
//         var parser = Parsers.Always();
//         // Assert
//         Assert.NotNull(parser);
//         // Using dynamic invocation to simulate a parse call.
//         // We assume the parser has a method "Parse" that takes an input string.
//         dynamic dynParser = parser;
//         var result = dynParser.Parse("");
//         // Expecting the default value for object? which is null.
//         Assert.Null(result);
//     }
// 
//     /// <summary>
//     /// Tests the Always<T>() method (generic with default value) to ensure it returns a parser that always succeeds and returns the default value of type T.
//     /// This test uses dynamic invocation assuming that the parser implements a Parse method.
//     /// </summary>
//     [Fact]
//     public void Always_Generic_Default_ReturnsParserThatAlwaysSucceedsWithDefaultValue()
//     {
//         // Arrange & Act
//         var parser = Parsers.Always<int>();
//         // Assert
//         Assert.NotNull(parser);
//         dynamic dynParser = parser;
//         var result = dynParser.Parse("");
//         // default(int) is 0.
//         Assert.Equal(0, result);
//     }
// 
//     /// <summary>
//     /// Tests the Always<T>(T value) method to ensure it returns a parser that always succeeds and returns the provided constant value.
//     /// This test uses dynamic invocation to simulate the parser's parsing behavior.
//     /// </summary>
//     [Fact]
//     public void Always_Generic_WithValue_ReturnsParserThatAlwaysSucceedsWithSpecifiedValue()
//     {
//         // Arrange
//         int expectedValue = 42;
//         // Act
//         var parser = Parsers.Always(expectedValue);
//         // Assert
//         Assert.NotNull(parser);
//         dynamic dynParser = parser;
//         var result = dynParser.Parse("");
//         Assert.Equal(expectedValue, result);
//     }
// 
//     /// <summary>
//     /// Tests that the WhiteSpace method without parameters returns a non-null parser instance 
//     /// of type "WhiteSpaceLiteral" with the default setting of IncludeNewLines set to false.
//     /// </summary>
//     [Fact]
//     public void WhiteSpace_DefaultParameter_ReturnsParserWithIncludeNewLinesFalse()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act
//         var parser = builder.WhiteSpace();
//         // Assert
//         Assert.NotNull(parser);
//         var parserType = parser.GetType();
//         Assert.Equal("WhiteSpaceLiteral", parserType.Name);
//         // Attempt to verify that the IncludeNewLines property is false if such property exists.
//         var includeNewLinesProperty = parserType.GetProperty("IncludeNewLines", BindingFlags.Public | BindingFlags.Instance);
//         if (includeNewLinesProperty != null)
//         {
//             var value = includeNewLinesProperty.GetValue(parser);
//             Assert.IsType<bool>(value);
//             Assert.False((bool)value, "Expected IncludeNewLines to be false by default.");
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the WhiteSpace method with includeNewLines set to true returns a non-null parser instance 
//     /// of type "WhiteSpaceLiteral" with IncludeNewLines set to true.
//     /// </summary>
//     [Fact]
//     public void WhiteSpace_ExplicitIncludeNewLinesTrue_ReturnsParserWithIncludeNewLinesTrue()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act
//         var parser = builder.WhiteSpace(true);
//         // Assert
//         Assert.NotNull(parser);
//         var parserType = parser.GetType();
//         Assert.Equal("WhiteSpaceLiteral", parserType.Name);
//         // Attempt to verify that the IncludeNewLines property is true if such property exists.
//         var includeNewLinesProperty = parserType.GetProperty("IncludeNewLines", BindingFlags.Public | BindingFlags.Instance);
//         if (includeNewLinesProperty != null)
//         {
//             var value = includeNewLinesProperty.GetValue(parser);
//             Assert.IsType<bool>(value);
//             Assert.True((bool)value, "Expected IncludeNewLines to be true when explicitly set.");
//         }
//     }
// 
//     /// <summary>
//     /// Tests that multiple calls to the WhiteSpace method yield distinct parser instances.
//     /// </summary>
//     [Fact]
//     public void WhiteSpace_MultipleCalls_ReturnsDistinctParserInstances()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act
//         var parser1 = builder.WhiteSpace();
//         var parser2 = builder.WhiteSpace();
//         // Assert
//         Assert.NotSame(parser1, parser2);
//     }
// 
//     /// <summary>
//     /// Tests that the NonWhiteSpace method of LiteralBuilder returns a NonWhiteSpaceLiteral parser when includeNewLines is true.
//     /// The test verifies that the returned parser is not null and its type name matches the expected implementation.
//     /// </summary>
//     [Fact]
//     public void NonWhiteSpace_WithIncludeNewLinesTrue_ReturnsNonWhiteSpaceLiteral()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         bool includeNewLines = true;
//         // Act
//         var parser = literalBuilder.NonWhiteSpace(includeNewLines);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Equal("NonWhiteSpaceLiteral", parser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that the NonWhiteSpace method of LiteralBuilder returns a NonWhiteSpaceLiteral parser when includeNewLines is false.
//     /// The test verifies that the returned parser is not null and its type name matches the expected implementation.
//     /// </summary>
//     [Fact]
//     public void NonWhiteSpace_WithIncludeNewLinesFalse_ReturnsNonWhiteSpaceLiteral()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         bool includeNewLines = false;
//         // Act
//         var parser = literalBuilder.NonWhiteSpace(includeNewLines);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Equal("NonWhiteSpaceLiteral", parser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that the NonWhiteSpace method of LiteralBuilder returns a NonWhiteSpaceLiteral parser when using the default parameter.
//     /// The test verifies that the returned parser is not null and its type name matches the expected implementation.
//     /// </summary>
//     [Fact]
//     public void NonWhiteSpace_DefaultParameter_ReturnsNonWhiteSpaceLiteral()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.NonWhiteSpace();
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Equal("NonWhiteSpaceLiteral", parser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that the Text method returns a TextLiteral-based parser when provided with valid non-empty input.
//     /// </summary>
//     /// <param name = "inputText">The string to be used as input.</param>
//     /// <param name = "caseInsensitive">Flag indicating if the comparison should be case-insensitive.</param>
//     [Theory]
//     [InlineData("SampleText", false)]
//     [InlineData("DifferentText", true)]
//     [InlineData("", false)]
//     public void Text_WithValidInput_ReturnsTextLiteralParser(string inputText, bool caseInsensitive)
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act
//         var parser = builder.Text(inputText, caseInsensitive);
//         // Assert
//         Assert.NotNull(parser);
//         // The returned parser should be based on TextLiteral. Since the implementation is hidden,
//         // we check that the type name contains "TextLiteral".
//         Assert.Contains("TextLiteral", parser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that the Text method handles an empty string input appropriately.
//     /// </summary>
//     [Fact]
//     public void Text_WithEmptyString_ReturnsTextLiteralParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         string inputText = string.Empty;
//         // Act
//         var parser = builder.Text(inputText);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Contains("TextLiteral", parser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that the Text method throws an ArgumentNullException when provided with a null string.
//     /// This assumes that the implementation validates the input parameter.
//     /// </summary>
//     [Fact]
//     public void Text_WithNullInput_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         string inputText = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => builder.Text(inputText));
//     }
// 
//     /// <summary>
//     /// Verifies that the Char method returns a non-null parser instance when called with a valid character.
//     /// </summary>
//     /// <param name = "input">The character input for the parser.</param>
//     [Theory]
//     [InlineData('a')]
//     [InlineData(' ')]
//     [InlineData('#')]
//     public void Char_WhenCalledWithValidCharacter_ReturnsParserInstance(char input)
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.Char(input);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests the Parsers.Number method with default parameters to ensure it returns a non-null parser.
//     /// </summary>
//     [Fact] [Error] (1077-30)CS0117 'Parsers' does not contain a definition for 'Number'
//     public void Number_WithDefaultParameters_ReturnsNonNull()
//     {
//         // Arrange & Act
//         var parser = Parsers.Number<double>();
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests the Parsers.Number method with custom number options and separator characters to ensure it returns a non-null parser.
//     /// </summary>
//     [Fact] [Error] (1093-30)CS0117 'Parsers' does not contain a definition for 'Number'
//     public void Number_WithCustomSeparators_ReturnsNonNull()
//     {
//         // Arrange
//         var customNumberOptions = NumberOptions.Float;
//         char customDecimalSeparator = ',';
//         char customGroupSeparator = '.';
//         // Act
//         var parser = Parsers.Number<double>(customNumberOptions, customDecimalSeparator, customGroupSeparator);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Integer method in LiteralBuilder returns a non-null instance of Parser&lt;long&gt; using default NumberOptions.
//     /// </summary>
//     [Fact]
//     public void Integer_DefaultOptions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act
//         Parser<long> parser = builder.Integer();
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<long>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Integer method in LiteralBuilder returns a non-null instance of Parser&lt;long&gt; when a custom NumberOptions is provided.
//     /// </summary>
//     [Fact]
//     public void Integer_CustomOptions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Using NumberOptions.Integer as custom option for demonstration (assuming other options may exist).
//         var customOption = NumberOptions.Integer;
//         // Act
//         Parser<long> parser = builder.Integer(customOption);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<long>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that Parsers.Decimal with the default parameter returns a non-null parser of type Parser&lt;decimal&gt;.
//     /// </summary>
//     [Fact] [Error] (1137-30)CS0117 'Parsers' does not contain a definition for 'Decimal'
//     public void Decimal_DefaultParameter_ReturnsNonNullParser()
//     {
//         // Act
//         var parser = Parsers.Decimal();
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<decimal>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that Parsers.Decimal with a custom NumberOptions returns a non-null parser of type Parser&lt;decimal&gt;.
//     /// </summary>
//     [Fact] [Error] (1152-30)CS0117 'Parsers' does not contain a definition for 'Decimal'
//     public void Decimal_CustomNumberOptions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var customOption = NumberOptions.Integer;
//         // Act
//         var parser = Parsers.Decimal(customOption);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<decimal>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Float method returns a non-null parser when called with the default NumberOptions.
//     /// </summary>
//     [Fact] [Error] (1167-22)CS0618 'LiteralBuilder.Float(NumberOptions)' is obsolete: 'Prefer Number<float>(NumberOptions.Float) instead.'
//     public void Float_DefaultOptions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.Float();
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Float method returns a non-null parser when called with a custom NumberOptions.
//     /// </summary>
//     [Fact] [Error] (1183-22)CS0618 'LiteralBuilder.Float(NumberOptions)' is obsolete: 'Prefer Number<float>(NumberOptions.Float) instead.'
//     public void Float_CustomNumberOptions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Using NumberOptions.Integer as an alternative to the default NumberOptions.Float.
//         var customOption = NumberOptions.Integer;
//         // Act
//         var parser = literalBuilder.Float(customOption);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests the Double method with default NumberOptions to ensure it returns a non-null Parser{double}.
//     /// </summary>
//     [Fact] [Error] (1197-33)CS0618 'LiteralBuilder.Double(NumberOptions)' is obsolete: 'Prefer Number<double>(NumberOptions.Float) instead.'
//     public void Double_DefaultNumberOptions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         Parser<double> result = literalBuilder.Double();
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsAssignableFrom<Parser<double>>(result);
//     }
// 
//     /// <summary>
//     /// Tests the Double method with specified NumberOptions values to ensure it returns a non-null Parser{double}.
//     /// </summary>
//     /// <param name = "option">An instance of NumberOptions to use for the test.</param>
//     [Theory] [Error] (1216-33)CS0618 'LiteralBuilder.Double(NumberOptions)' is obsolete: 'Prefer Number<double>(NumberOptions.Float) instead.'
//     [InlineData(NumberOptions.Float)]
//     [InlineData(NumberOptions.Integer)]
//     [InlineData(NumberOptions.Number)]
//     public void Double_WithSpecifiedNumberOptions_ReturnsNonNullParser(NumberOptions option)
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         Parser<double> result = literalBuilder.Double(option);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsAssignableFrom<Parser<double>>(result);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "LiteralBuilder.String(StringLiteralQuotes)"/> method with the default parameter to ensure it returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void String_DefaultParameter_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.String();
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "LiteralBuilder.String(StringLiteralQuotes)"/> method with an explicit parameter and verifies it returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void String_WithParameter_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         var expectedQuotes = StringLiteralQuotes.SingleOrDouble;
//         // Act
//         var parser = literalBuilder.String(expectedQuotes);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests the Identifier method (Func overload) when called with null predicates returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void Identifier_FuncParams_Null_ReturnsNonNullParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act
//         var parser = builder.Identifier(null, null);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Contains("Identifier", parser.GetType().Name, StringComparison.OrdinalIgnoreCase);
//     }
// 
//     /// <summary>
//     /// Tests the Identifier method (Func overload) when called with valid predicate functions returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void Identifier_FuncParams_WithPredicates_ReturnsNonNullParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         Func<char, bool> extraStart = c => char.IsLetter(c);
//         Func<char, bool> extraPart = c => char.IsDigit(c);
//         // Act
//         var parser = builder.Identifier(extraStart, extraPart);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Contains("Identifier", parser.GetType().Name, StringComparison.OrdinalIgnoreCase);
//     }
// 
//     /// <summary>
//     /// Tests that Pattern with default parameters returns a PatternLiteral with the expected default values.
//     /// </summary>
//     [Fact]
//     public void Pattern_WithValidPredicateDefaultValues_ReturnsValidPatternLiteral()
//     {
//         // Arrange
//         LiteralBuilder builder = new LiteralBuilder();
//         Func<char, bool> predicate = c => char.IsDigit(c);
//         // Act
//         var parser = builder.Pattern(predicate);
//         // Assert
//         Assert.NotNull(parser);
//         // Verify underlying PatternLiteral values via reflection.
//         // Since builder.Pattern creates a new PatternLiteral, we expect the type name to be "PatternLiteral".
//         Type parserType = parser.GetType();
//         Assert.Equal("PatternLiteral", parserType.Name);
//         // Retrieve the field "minSize"
//         FieldInfo minSizeField = parserType.GetField("minSize", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(minSizeField);
//         int minSizeValue = (int)minSizeField.GetValue(parser);
//         Assert.Equal(1, minSizeValue);
//         // Retrieve the field "maxSize"
//         FieldInfo maxSizeField = parserType.GetField("maxSize", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(maxSizeField);
//         int maxSizeValue = (int)maxSizeField.GetValue(parser);
//         Assert.Equal(0, maxSizeValue);
//         // Retrieve the field "predicate"
//         FieldInfo predicateField = parserType.GetField("predicate", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(predicateField);
//         var storedPredicate = predicateField.GetValue(parser) as Func<char, bool>;
//         Assert.NotNull(storedPredicate);
//         // Validate the predicate behavior.
//         Assert.True(storedPredicate('5'));
//         Assert.False(storedPredicate('a'));
//     }
// 
//     /// <summary>
//     /// Tests that Pattern with custom minSize and maxSize values returns a PatternLiteral with those values.
//     /// </summary>
//     [Fact]
//     public void Pattern_WithCustomMinSizeAndMaxSize_ReturnsValidPatternLiteral()
//     {
//         // Arrange
//         LiteralBuilder builder = new LiteralBuilder();
//         Func<char, bool> predicate = c => char.IsLetter(c);
//         int customMinSize = 3;
//         int customMaxSize = 5;
//         // Act
//         var parser = builder.Pattern(predicate, customMinSize, customMaxSize);
//         // Assert
//         Assert.NotNull(parser);
//         Type parserType = parser.GetType();
//         Assert.Equal("PatternLiteral", parserType.Name);
//         FieldInfo minSizeField = parserType.GetField("minSize", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(minSizeField);
//         int minSizeValue = (int)minSizeField.GetValue(parser);
//         Assert.Equal(customMinSize, minSizeValue);
//         FieldInfo maxSizeField = parserType.GetField("maxSize", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(maxSizeField);
//         int maxSizeValue = (int)maxSizeField.GetValue(parser);
//         Assert.Equal(customMaxSize, maxSizeValue);
//         FieldInfo predicateField = parserType.GetField("predicate", BindingFlags.Instance | BindingFlags.NonPublic);
//         Assert.NotNull(predicateField);
//         var storedPredicate = predicateField.GetValue(parser) as Func<char, bool>;
//         Assert.NotNull(storedPredicate);
//         // Validate the predicate behavior.
//         Assert.True(storedPredicate('a'));
//         Assert.False(storedPredicate('1'));
//     }
// 
// #if NET8_0_OR_GREATER
//         /// <summary>
//         /// Tests that the AnyOf method with a SearchValues parameter returns a non-null parser.
//         /// </summary>
//         [Fact]
//         public void AnyOf_WithSearchValues_ValidInput_ReturnsNonNullParser()
//         {
//             // Arrange
//             var builder = new LiteralBuilder();
//             // Create a SearchValues<char> instance using a ReadOnlySpan<char> from a sample string.
//             var searchValues = SearchValues.Create("abc".AsSpan());
//             int minSize = 2;
//             int maxSize = 5;
// 
//             // Act
//             var parser = builder.AnyOf(searchValues, minSize, maxSize);
// 
//             // Assert
//             Assert.NotNull(parser);
//         }
// 
//         /// <summary>
//         /// Tests that the AnyOf method with a ReadOnlySpan<char> parameter returns a non-null parser.
//         /// </summary>
//         [Fact]
//         public void AnyOf_WithReadOnlySpan_ValidInput_ReturnsNonNullParser()
//         {
//             // Arrange
//             var builder = new LiteralBuilder();
//             ReadOnlySpan<char> values = "xyz".ToCharArray();
//             int minSize = 1;
//             int maxSize = 3;
// 
//             // Act
//             var parser = builder.AnyOf(values, minSize, maxSize);
// 
//             // Assert
//             Assert.NotNull(parser);
//         }
// #else
//     /// <summary>
//     /// Tests that the AnyOf method with a string parameter returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void AnyOf_WithString_ValidInput_ReturnsNonNullParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         string values = "def";
//         int minSize = 1;
//         int maxSize = 0;
//         // Act
//         var parser = builder.AnyOf(values, minSize, maxSize);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the NonWhiteSpace method with the default parameter returns a non-null parser instance.
//     /// </summary>
//     [Fact]
//     public void NonWhiteSpace_DefaultParameter_ReturnsNonNullParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act
//         var parser = builder.NonWhiteSpace();
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the NonWhiteSpace method with includeNewLines set to false returns a non-null parser instance.
//     /// </summary>
//     [Fact]
//     public void NonWhiteSpace_IncludeNewLinesFalse_ReturnsNonNullParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act
//         var parser = builder.NonWhiteSpace(includeNewLines: false);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that LiteralBuilder.Text returns a non-null TextLiteral parser when provided with a valid text and default case sensitivity.
//     /// </summary>
//     [Fact]
//     public void Text_WithValidText_DefaultCaseSensitivity_ReturnsTextLiteralParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         string inputText = "hello";
//         // Act
//         var parser = literalBuilder.Text(inputText);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Equal("TextLiteral", parser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that LiteralBuilder.Text returns a non-null TextLiteral parser when provided with an empty string.
//     /// </summary>
//     [Fact] [Error] (1459-17)CS0111 Type 'ParsersTests' already defines a member called 'Text_WithEmptyString_ReturnsTextLiteralParser' with the same parameter types
//     public void Text_WithEmptyString_ReturnsTextLiteralParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         string inputText = string.Empty;
//         // Act
//         var parser = literalBuilder.Text(inputText);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Equal("TextLiteral", parser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that LiteralBuilder.Text returns a TextLiteral parser with case-insensitive comparison when specified.
//     /// </summary>
//     [Fact]
//     public void Text_WithCaseInsensitiveTrue_ReturnsTextLiteralParserWithIgnoreCase()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         string inputText = "world";
//         // Act
//         var parser = literalBuilder.Text(inputText, caseInsensitive: true);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.Equal("TextLiteral", parser.GetType().Name);
//     // Note: The internal configuration (e.g., StringComparison) is not directly accessible.
//     }
// 
//     /// <summary>
//     /// Indicates whether parsing was successful.
//     /// </summary>
//     public bool Success { get; set; }
//     /// <summary>
//     /// The parsed value.
//     /// </summary>
//     public T Value { get; set; } [Error] (1495-12)CS0246 The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?)
//     /// <summary>
//     /// The new position in the input after parsing.
//     /// </summary>
//     public int Position { get; set; }
// 
//     /// <summary>
//     /// Tests that the Number method with default parameters returns a non-null Parser instance for type long.
//     /// </summary>
//     [Fact]
//     public void Number_Default_ReturnsNonNullParser_Long()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.Number<long>();
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<long>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Number method with custom number options and separators returns a non-null Parser instance for type decimal.
//     /// </summary>
//     [Fact]
//     public void Number_CustomParameters_ReturnsNonNullParser_Decimal()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         var customNumberOptions = NumberOptions.Float;
//         char customDecimalSeparator = ',';
//         char customGroupSeparator = '.';
//         // Act
//         var parser = literalBuilder.Number<decimal>(customNumberOptions, customDecimalSeparator, customGroupSeparator);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<decimal>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that Parsers.Integer with default options returns a valid parser instance.
//     /// </summary>
//     [Fact] [Error] (1541-39)CS0117 'Parsers' does not contain a definition for 'Integer'
//     public void Integer_DefaultOptions_ReturnsParserInstance()
//     {
//         // Act
//         Parser<long> parser = Parsers.Integer();
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<long>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that Parsers.Integer with an explicit NumberOptions.Integer returns a valid parser instance.
//     /// </summary>
//     [Fact] [Error] (1556-39)CS0117 'Parsers' does not contain a definition for 'Integer'
//     public void Integer_WithIntegerOption_ReturnsParserInstance()
//     {
//         // Arrange
//         NumberOptions numberOptions = NumberOptions.Integer;
//         // Act
//         Parser<long> parser = Parsers.Integer(numberOptions);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<long>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that Parsers.Integer with a different valid NumberOptions (Number) returns a valid parser instance.
//     /// This test ensures the method handles non-standard options gracefully.
//     /// </summary>
//     [Fact] [Error] (1572-39)CS0117 'Parsers' does not contain a definition for 'Integer'
//     public void Integer_WithDifferentValidOption_ReturnsParserInstance()
//     {
//         // Arrange
//         NumberOptions numberOptions = NumberOptions.Number;
//         // Act
//         Parser<long> parser = Parsers.Integer(numberOptions);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<long>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Decimal method returns a non-null Parser&lt;decimal&gt; using the default NumberOptions.
//     /// </summary>
//     [Fact] [Error] (1585-30)CS0117 'Parsers' does not contain a definition for 'Decimal'
//     public void Decimal_WithDefaultOption_ReturnsNonNullParser()
//     {
//         // Act
//         var parser = Parsers.Decimal();
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<decimal>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Decimal method returns a non-null Parser&lt;decimal&gt; when specifying NumberOptions.Integer.
//     /// </summary>
//     [Fact] [Error] (1600-30)CS0117 'Parsers' does not contain a definition for 'Decimal'
//     public void Decimal_WithIntegerOption_ReturnsNonNullParser()
//     {
//         // Arrange
//         var numberOption = NumberOptions.Integer;
//         // Act
//         var parser = Parsers.Decimal(numberOption);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<decimal>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests the Float method with the default parameter, ensuring it returns a non-null Parser&lt;float&gt;.
//     /// </summary>
//     [Fact] [Error] (1615-32)CS0618 'LiteralBuilder.Float(NumberOptions)' is obsolete: 'Prefer Number<float>(NumberOptions.Float) instead.'
//     public void Float_DefaultOption_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         Parser<float> parser = literalBuilder.Float();
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<float>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests the Float method with a custom NumberOptions parameter, ensuring it returns a non-null Parser&lt;float&gt;.
//     /// </summary>
//     [Theory] [Error] (1632-32)CS0618 'LiteralBuilder.Float(NumberOptions)' is obsolete: 'Prefer Number<float>(NumberOptions.Float) instead.'
//     [InlineData(NumberOptions.Float)]
//     [InlineData(NumberOptions.Integer)]
//     public void Float_CustomOption_ReturnsNonNullParser(NumberOptions numberOptions)
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         Parser<float> parser = literalBuilder.Float(numberOptions);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<float>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Double method of LiteralBuilder returns a non-null Parser&lt;double&gt; when called with default options.
//     /// </summary>
//     [Fact] [Error] (1647-22)CS0618 'LiteralBuilder.Double(NumberOptions)' is obsolete: 'Prefer Number<double>(NumberOptions.Float) instead.'
//     public void Double_DefaultOptions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.Double();
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<double>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Double method of LiteralBuilder returns a non-null Parser&lt;double&gt; when called with explicit NumberOptions.Float.
//     /// </summary>
//     [Fact] [Error] (1662-22)CS0618 'LiteralBuilder.Double(NumberOptions)' is obsolete: 'Prefer Number<double>(NumberOptions.Float) instead.'
//     public void Double_WithFloatNumberOptions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.Double(NumberOptions.Float);
//         // Assert
//         Assert.NotNull(parser);
//         Assert.IsAssignableFrom<Parser<double>>(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the String method with default parameters returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void String_Default_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.String();
//         // Assert
//         Assert.NotNull(parser);
//         // Verify that the returned parser is created using StringLiteral (by type name inspection).
//         Assert.Contains("StringLiteral", parser.GetType().Name, StringComparison.OrdinalIgnoreCase);
//     }
// 
//     /// <summary>
//     /// Tests that the String method with a custom quotes parameter returns a parser configured with the expected quotes.
//     /// </summary>
//     [Fact]
//     public void String_WithCustomQuotes_ReturnsParserWithExpectedQuotes()
//     {
//         // Arrange
//         var expectedQuotes = StringLiteralQuotes.Double;
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.String(expectedQuotes);
//         // Assert
//         Assert.NotNull(parser);
//         // Use reflection to check if the parser has a property called "Quotes" and that it matches the expected value.
//         var quotesProperty = parser.GetType().GetProperty("Quotes", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
//         Assert.NotNull(quotesProperty);
//         var actualQuotes = quotesProperty.GetValue(parser);
//         Assert.Equal(expectedQuotes, actualQuotes);
//     }
// 
//     /// <summary>
//     /// Tests that Identifier(Func<char , bool>?, Func<char , bool>?) with null arguments returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void Identifier_NullArgs_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         // Act
//         var parser = literalBuilder.Identifier(null, null);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that Identifier(Func<char , bool>?, Func<char , bool>?) with non-null extraStart and extraPart returns a non-null parser.
//     /// </summary>
//     [Fact]
//     public void Identifier_WithExtraFunctions_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         Func<char, bool> extraStart = c => char.IsLetter(c);
//         Func<char, bool> extraPart = c => char.IsDigit(c);
//         // Act
//         var parser = literalBuilder.Identifier(extraStart, extraPart);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Pattern method returns a non-null parser instance when provided with a valid predicate.
//     /// </summary>
//     [Fact]
//     public void Pattern_ValidPredicate_ReturnsNonNullPatternParser()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         Func<char, bool> predicate = c => char.IsDigit(c);
//         int minSize = 2;
//         int maxSize = 5;
//         // Act
//         var parser = builder.Pattern(predicate, minSize, maxSize);
//         // Assert
//         Assert.NotNull(parser);
//         // Since the Pattern method returns an instance of PatternLiteral wrapped in its own implementation,
//         // we validate that the returned object's type name contains "PatternLiteral".
//         Assert.Contains("PatternLiteral", parser.GetType().Name);
//     }
// 
//     /// <summary>
//     /// Tests that the Pattern method throws an ArgumentNullException when provided with a null predicate.
//     /// </summary>
//     [Fact]
//     public void Pattern_NullPredicate_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var builder = new LiteralBuilder();
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => builder.Pattern(null, 1, 0));
//     }
// 
// #if NET8_0_OR_GREATER
//         /// <summary>
//         /// Tests the AnyOf(SearchValues&lt;char&gt;) method with valid input.
//         /// Verifies that a non-null parser is returned when passing a valid SearchValues<char> instance.
//         /// </summary>
//         [Fact]
//         public void AnyOf_SearchValues_WithValidInput_ReturnsNonNullParser()
//         {
//             // Arrange
//             var literalBuilder = new LiteralBuilder();
//             var searchValues = SearchValues.Create(new char[] { 'a', 'b', 'c' });
//             int minSize = 2;
//             int maxSize = 5;
// 
//             // Act
//             var parser = literalBuilder.AnyOf(searchValues, minSize, maxSize);
// 
//             // Assert
//             Assert.NotNull(parser);
//         }
// 
//         /// <summary>
//         /// Tests the AnyOf(ReadOnlySpan&lt;char&gt;) method with valid input.
//         /// Verifies that a non-null parser is returned when passing a valid ReadOnlySpan<char>.
//         /// </summary>
//         [Fact]
//         public void AnyOf_ReadOnlySpan_WithValidInput_ReturnsNonNullParser()
//         {
//             // Arrange
//             var literalBuilder = new LiteralBuilder();
//             ReadOnlySpan<char> values = "abc";
//             int minSize = 2;
//             int maxSize = 5;
// 
//             // Act
//             var parser = literalBuilder.AnyOf(values, minSize, maxSize);
// 
//             // Assert
//             Assert.NotNull(parser);
//         }
// #else
//     /// <summary>
//     /// Tests the AnyOf(string) method with valid input (non NET8_0_OR_GREATER scenario).
//     /// Verifies that a non-null parser is returned when passing a valid string.
//     /// </summary>
//     [Fact]
//     public void AnyOf_String_WithValidInput_ReturnsNonNullParser()
//     {
//         // Arrange
//         var literalBuilder = new LiteralBuilder();
//         string values = "abc";
//         int minSize = 2;
//         int maxSize = 5;
//         // Act
//         var parser = literalBuilder.AnyOf(values, minSize, maxSize);
//         // Assert
//         Assert.NotNull(parser);
//     }
// 
//     /// <summary>
//     /// Tests that the Literals property returns a non-null instance of LiteralBuilder.
//     /// This test verifies the basic behavior of the property.
//     /// </summary>
//     [Fact]
//     public void Literals_Get_ReturnsNonNullLiteralBuilder()
//     {
//         // Act
//         LiteralBuilder result = Parsers.Literals;
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<LiteralBuilder>(result);
//     }
// 
//     /// <summary>
//     /// Tests that each access of the Literals property returns a new distinct instance.
//     /// This ensures that the property is not caching or reusing instances.
//     /// </summary>
//     [Fact]
//     public void Literals_Get_ReturnsNewInstanceEachTime()
//     {
//         // Act
//         LiteralBuilder firstInstance = Parsers.Literals;
//         LiteralBuilder secondInstance = Parsers.Literals;
//         // Assert
//         Assert.NotSame(firstInstance, secondInstance);
//     }
// 
//     /// <summary>
//     /// Tests that the Terms property returns a non-null instance of TermBuilder.
//     /// </summary>
//     [Fact]
//     public void Terms_WhenAccessed_ReturnsNonNullInstance()
//     {
//         // Act
//         TermBuilder result = Parsers.Terms;
//         // Assert
//         Assert.NotNull(result);
//     }
// 
//     /// <summary>
//     /// Tests that the Terms property returns a new instance of TermBuilder on each access.
//     /// This ensures that state is not shared between different uses.
//     /// </summary>
//     [Fact]
//     public void Terms_WhenAccessedMultipleTimes_ReturnsDifferentInstances()
//     {
//         // Act
//         TermBuilder firstInstance = Parsers.Terms;
//         TermBuilder secondInstance = Parsers.Terms;
//         // Assert
//         Assert.NotSame(firstInstance, secondInstance);
//     }
// }