// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot.Fluent;
using System;
using System.Linq;
using Xunit;

/// <summary>
/// Unit tests for the Parsers extension methods 'Or'.
/// </summary>
// public class ParsersTests [Error] (302-2)CS1513 } expected [Error] (302-2)CS1038 #endregion directive expected
// {
//     /// <summary>
//     /// A dummy parser implementation for testing purposes.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
//     private class DummyParser<T> : Parser<T> [Error] (17-19)CS0534 'ParsersTests.DummyParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//     }
// 
//     /// <summary>
//     /// Tests that when the left parser is not a OneOf instance, calling Or returns a new OneOf instance containing both parsers.
//     /// </summary>
//     [Fact] [Error] (36-53)CS1061 'IReadOnlyList<Parser<int>>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<Parser<int>>' could be found (are you missing a using directive or an assembly reference?)
//     public void Or_NonOneOfParser_ReturnsOneOfWithBothParsers()
//     {
//         // Arrange
//         var leftParser = new DummyParser<int>();
//         var rightParser = new DummyParser<int>();
//         // Act
//         var result = leftParser.Or(rightParser);
//         // Assert
//         Assert.NotNull(result);
//         var oneOfResult = Assert.IsType<OneOf<int>>(result);
//         Assert.NotNull(oneOfResult.OriginalParsers);
//         Assert.Equal(2, oneOfResult.OriginalParsers.Length);
//         Assert.Same(leftParser, oneOfResult.OriginalParsers[0]);
//         Assert.Same(rightParser, oneOfResult.OriginalParsers[1]);
//     }
// 
//     /// <summary>
//     /// Tests that when the left parser is a OneOf instance, calling Or appends the provided parser to the existing list.
//     /// </summary>
//     [Fact] [Error] (57-53)CS1061 'IReadOnlyList<Parser<int>>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<Parser<int>>' could be found (are you missing a using directive or an assembly reference?)
//     public void Or_OneOfParser_AppendsParserToExistingOneOf()
//     {
//         // Arrange
//         var initialParser = new DummyParser<int>();
//         var additionalParser = new DummyParser<int>();
//         var oneOfParser = Parsers.OneOf(initialParser);
//         // Act
//         var result = oneOfParser.Or(additionalParser);
//         // Assert
//         Assert.NotNull(result);
//         var oneOfResult = Assert.IsType<OneOf<int>>(result);
//         Assert.NotNull(oneOfResult.OriginalParsers);
//         Assert.Equal(2, oneOfResult.OriginalParsers.Length);
//         Assert.Same(initialParser, oneOfResult.OriginalParsers[0]);
//         Assert.Same(additionalParser, oneOfResult.OriginalParsers[1]);
//     }
// 
//     /// <summary>
//     /// Tests that invoking Or on a null left parser throws a NullReferenceException.
//     /// </summary>
//     [Fact]
//     public void Or_NullLeftParser_ThrowsNullReferenceException()
//     {
//         // Arrange
//         DummyParser<int> leftParser = null;
//         var rightParser = new DummyParser<int>();
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => leftParser.Or(rightParser));
//     }
// 
//     /// <summary>
//     /// Tests that invoking Or with a null right parser returns a OneOf instance containing the left parser and a null entry.
//     /// </summary>
//     [Fact] [Error] (90-53)CS1061 'IReadOnlyList<Parser<int>>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<Parser<int>>' could be found (are you missing a using directive or an assembly reference?)
//     public void Or_RightParserNull_ReturnsOneOfContainingNull()
//     {
//         // Arrange
//         var leftParser = new DummyParser<int>();
//         DummyParser<int> rightParser = null;
//         // Act
//         var result = leftParser.Or(rightParser);
//         // Assert
//         Assert.NotNull(result);
//         var oneOfResult = Assert.IsType<OneOf<int>>(result);
//         Assert.NotNull(oneOfResult.OriginalParsers);
//         Assert.Equal(2, oneOfResult.OriginalParsers.Length);
//         Assert.Same(leftParser, oneOfResult.OriginalParsers[0]);
//         Assert.Null(oneOfResult.OriginalParsers[1]);
//     }
// 
//     /// <summary>
//     /// Tests the generic Or overload to ensure it returns a OneOf instance with both parsers when provided with matching types.
//     /// </summary>
//     [Fact] [Error] (110-53)CS1061 'IReadOnlyList<Parser<int>>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<Parser<int>>' could be found (are you missing a using directive or an assembly reference?)
//     public void Or_GenericOverload_ReturnsOneOfWithBothParsers()
//     {
//         // Arrange
//         var leftParser = new DummyParser<int>();
//         var rightParser = new DummyParser<int>();
//         // Act
//         var result = Parsers.Or<int, int, int>(leftParser, rightParser);
//         // Assert
//         Assert.NotNull(result);
//         var oneOfResult = Assert.IsType<OneOf<int>>(result);
//         Assert.NotNull(oneOfResult.OriginalParsers);
//         Assert.Equal(2, oneOfResult.OriginalParsers.Length);
//         Assert.Same(leftParser, oneOfResult.OriginalParsers[0]);
//         Assert.Same(rightParser, oneOfResult.OriginalParsers[1]);
//     }
// 
//     /// <summary>
//     /// A fake parser implementation for testing purposes.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter for the parser.</typeparam>
//     private class FakeParser<T> : Parser<T> [Error] (119-19)CS0534 'ParsersTests.FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//     // Minimal fake implementation; actual parsing logic is not required for these tests.
//     }
// 
// #region Tests for the Or<T>(this Parser<T>, Parser<T>) overload
//     /// <summary>
//     /// Tests the Or extension method (same type overload) with valid non-null parsers,
//     /// expecting a non-null combined parser.
//     /// </summary>
//     [Fact]
//     public void Or_SameTypeParsers_WithValidParsers_ReturnsNonNullParser()
//     {
//         // Arrange
//         var firstParser = new FakeParser<int>();
//         var secondParser = new FakeParser<int>();
//         // Act
//         var result = firstParser.Or(secondParser);
//         // Assert
//         Assert.NotNull(result);
//     }
// 
//     /// <summary>
//     /// Tests the Or extension method (same type overload) when the first parser is null,
//     /// expecting a NullReferenceException to be thrown.
//     /// </summary>
//     [Fact]
//     public void Or_SameTypeParsers_FirstParserNull_ThrowsNullReferenceException()
//     {
//         // Arrange
//         FakeParser<int> firstParser = null;
//         var secondParser = new FakeParser<int>();
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => firstParser.Or(secondParser));
//     }
// 
//     /// <summary>
//     /// Tests the Or extension method (same type overload) when the second parser is null,
//     /// expecting an ArgumentNullException to be thrown.
//     /// </summary>
//     [Fact]
//     public void Or_SameTypeParsers_SecondParserNull_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var firstParser = new FakeParser<int>();
//         FakeParser<int> secondParser = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => firstParser.Or(secondParser));
//     }
// 
// #endregion
// #region Tests for the Or<A, B, T>(this Parser<A>, Parser<B>) overload
//     /// <summary>
//     /// Tests the generic Or extension method (different type overload) with valid non-null parsers,
//     /// expecting the returned parser to be a combined instance represented by OneOf.
//     /// </summary>
//     [Fact]
//     public void Or_DifferentTypeParsers_WithValidParsers_ReturnsInstanceOfOneOf()
//     {
//         // Arrange
//         var firstParser = new FakeParser<int>();
//         var secondParser = new FakeParser<int>();
//         // Act
//         var result = firstParser.Or(secondParser);
//         // Assert
//         Assert.NotNull(result);
//         // Verify that the returned parser is of the expected OneOf type.
//         Assert.Equal(typeof(OneOf<int, int, int>), result.GetType());
//     }
// 
//     /// <summary>
//     /// Tests the generic Or extension method (different type overload) when the first parser is null,
//     /// expecting a NullReferenceException to be thrown.
//     /// </summary>
//     [Fact]
//     public void Or_DifferentTypeParsers_FirstParserNull_ThrowsNullReferenceException()
//     {
//         // Arrange
//         FakeParser<int> firstParser = null;
//         var secondParser = new FakeParser<int>();
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => firstParser.Or(secondParser));
//     }
// 
//     /// <summary>
//     /// Tests the generic Or extension method (different type overload) when the second parser is null,
//     /// expecting an ArgumentNullException to be thrown.
//     /// </summary>
//     [Fact]
//     public void Or_DifferentTypeParsers_SecondParserNull_ThrowsArgumentNullException()
//     {
//         // Arrange
//         var firstParser = new FakeParser<int>();
//         FakeParser<int> secondParser = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => firstParser.Or(secondParser));
//     }
// 
//     /// <summary>
//     /// A dummy parser delegate used for testing purposes.
//     /// Assumes that Parser<T> is a delegate that accepts any parameter and returns a T.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter of the parser.</typeparam>
//     /// <param name = "input">The input parameter (not used).</param>
//     /// <returns>Returns a default value for type T.</returns>
//     private static T DummyParser<T>(object input) => default; [Error] (224-22)CS0102 The type 'ParsersTests' already contains a definition for 'DummyParser'
//     /// <summary>
//     /// Tests that the OneOf method returns a OneOf instance when called with multiple valid parsers.
//     /// Expected outcome: The returned object is not null and is of type OneOf&lt;int&gt;.
//     /// </summary>
//     [Fact] [Error] (233-31)CS0428 Cannot convert method group 'DummyParser' to non-delegate type 'Parser<int>'. Did you intend to invoke the method? [Error] (234-31)CS0428 Cannot convert method group 'DummyParser' to non-delegate type 'Parser<int>'. Did you intend to invoke the method?
//     public void OneOf_WithMultipleValidParsers_ReturnsOneOfInstance()
//     {
//         // Arrange
//         Parser<int> parser1 = DummyParser<int>;
//         Parser<int> parser2 = DummyParser<int>;
//         // Act
//         Parser<int> result = Parsers.OneOf(parser1, parser2);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<OneOf<int>>(result);
//     }
// 
//     /// <summary>
//     /// Tests that the OneOf method returns a OneOf instance when called with a single valid parser.
//     /// Expected outcome: The returned object is not null and is of type OneOf&lt;int&gt;.
//     /// </summary>
//     [Fact] [Error] (250-31)CS0428 Cannot convert method group 'DummyParser' to non-delegate type 'Parser<int>'. Did you intend to invoke the method?
//     public void OneOf_WithSingleValidParser_ReturnsOneOfInstance()
//     {
//         // Arrange
//         Parser<int> parser1 = DummyParser<int>;
//         // Act
//         Parser<int> result = Parsers.OneOf(parser1);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<OneOf<int>>(result);
//     }
// 
//     /// <summary>
//     /// Tests that the OneOf method returns a OneOf instance when called with no parsers.
//     /// Expected outcome: The returned object is not null and is of type OneOf&lt;int&gt;.
//     /// </summary>
//     [Fact]
//     public void OneOf_WithNoParsers_ReturnsOneOfInstance()
//     {
//         // Act
//         Parser<int> result = Parsers.OneOf<int>();
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<OneOf<int>>(result);
//     }
// 
//     /// <summary>
//     /// Tests that the OneOf method returns a OneOf instance when an explicit null is passed as the parsers array.
//     /// Expected outcome: Since the implementation does not guard against a null array, the constructor is invoked with a null value.
//     /// This test verifies that the method call does not throw an exception and returns an instance of OneOf&lt;int&gt;.
//     /// </summary>
//     [Fact]
//     public void OneOf_WithExplicitNullArray_ReturnsOneOfInstance()
//     {
//         // Act
//         Parser<int> result = Parsers.OneOf<int>(null);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<OneOf<int>>(result);
//     }
// 
//     /// <summary>
//     /// Tests that the OneOf method returns a OneOf instance when the provided parsers array contains a null element.
//     /// Expected outcome: The returned object is not null and is of type OneOf&lt;int&gt;.
//     /// </summary>
//     [Fact] [Error] (295-31)CS0428 Cannot convert method group 'DummyParser' to non-delegate type 'Parser<int>'. Did you intend to invoke the method?
//     public void OneOf_WithNullElementInParsersArray_ReturnsOneOfInstance()
//     {
//         // Arrange
//         Parser<int> parser1 = DummyParser<int>;
//         // Act
//         Parser<int> result = Parsers.OneOf(parser1, null);
//         // Assert
//         Assert.NotNull(result);
//         Assert.IsType<OneOf<int>>(result);
//     }
// }