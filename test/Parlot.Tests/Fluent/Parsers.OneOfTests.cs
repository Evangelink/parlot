// using Parlot.Fluent;
// using Parlot.UnitTests;
// using System;
// using System.Linq;
// using System.Linq.Expressions;
// using Xunit;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the <see cref = "Parsers"/> static class.
//     /// </summary>
//     public class ParsersTests
//     {
//         private readonly FakeParser<int> _intParser1;
//         private readonly FakeParser<int> _intParser2;
//         private readonly FakeParser<int> _intParser3;
//         /// <summary>
//         /// Initializes test dependencies.
//         /// </summary>
//         public ParsersTests()
//         {
//             _intParser1 = new FakeParser<int>(1);
//             _intParser2 = new FakeParser<int>(2);
//             _intParser3 = new FakeParser<int>(3);
//         }
// 
// #region Tests for Or<T>(this Parser<T> parser, Parser<T> or)
//         /// <summary>
//         /// Tests that calling Or on a parser that is not already a OneOf returns a OneOf containing both provided parsers.
//         /// </summary>
// //         [Fact] [Error] (39-41)CS1061 'Parser<int>' does not contain a definition for 'Or' and no accessible extension method 'Or' accepting a first argument of type 'Parser<int>' could be found (are you missing a using directive or an assembly reference?) [Error] (42-51)CS1061 'IReadOnlyList<Parser<int>>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<Parser<int>>' could be found (are you missing a using directive or an assembly reference?)
// //         public void Or_WhenCalledOnRegularParser_ReturnsOneOfWithBothParsers()
// //         {
// //             // Arrange
// //             Parser<int> parser = _intParser1;
// //             Parser<int> parserToOr = _intParser2;
// //             // Act
// //             Parser<int> result = parser.Or(parserToOr);
// //             // Assert
// //             var oneOf = Assert.IsType<OneOf<int>>(result);
// //             Assert.Equal(2, oneOf.OriginalParsers.Length);
// //             Assert.Same(_intParser1, oneOf.OriginalParsers[0]);
// //             Assert.Same(_intParser2, oneOf.OriginalParsers[1]);
// //         }
// 
//         /// <summary>
//         /// Tests that calling Or on a parser that is already a OneOf appends the new parser to the existing parsers.
//         /// </summary>
// //         [Fact] [Error] (55-43)CS1729 'OneOf<int>' does not contain a constructor that takes 2 arguments [Error] (58-50)CS1503 Argument 2: cannot convert from 'Parlot.Fluent.UnitTests.Parser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (61-51)CS1061 'IReadOnlyList<Parser<int>>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<Parser<int>>' could be found (are you missing a using directive or an assembly reference?)
// //         public void Or_WhenCalledOnExistingOneOf_AppendsParserToOriginalParsers()
// //         {
// //             // Arrange
// //             // Create a OneOf with two parsers.
// //             OneOf<int> initialOneOf = new OneOf<int>(_intParser1, _intParser2);
// //             Parser<int> newParser = _intParser3;
// //             // Act
// //             Parser<int> result = initialOneOf.Or(newParser);
// //             // Assert
// //             var oneOf = Assert.IsType<OneOf<int>>(result);
// //             Assert.Equal(3, oneOf.OriginalParsers.Length);
// //             Assert.Same(_intParser1, oneOf.OriginalParsers[0]);
// //             Assert.Same(_intParser2, oneOf.OriginalParsers[1]);
// //             Assert.Same(_intParser3, oneOf.OriginalParsers[2]);
// //         }
// 
//         /// <summary>
//         /// Tests that calling the Or extension method with a null 'or' parser allows null to be part of the OneOf.
//         /// </summary>
// //         [Fact] [Error] (77-41)CS1061 'Parser<int>' does not contain a definition for 'Or' and no accessible extension method 'Or' accepting a first argument of type 'Parser<int>' could be found (are you missing a using directive or an assembly reference?) [Error] (80-51)CS1061 'IReadOnlyList<Parser<int>>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<Parser<int>>' could be found (are you missing a using directive or an assembly reference?)
// //         public void Or_WhenCalledWithNullOrParser_AllowsNullInParsersArray()
// //         {
// //             // Arrange
// //             Parser<int> parser = _intParser1;
// //             Parser<int> nullParser = null;
// //             // Act
// //             Parser<int> result = parser.Or(nullParser);
// //             // Assert
// //             var oneOf = Assert.IsType<OneOf<int>>(result);
// //             Assert.Equal(2, oneOf.OriginalParsers.Length);
// //             Assert.Same(_intParser1, oneOf.OriginalParsers[0]);
// //             Assert.Null(oneOf.OriginalParsers[1]);
// //         }
// 
//         /// <summary>
//         /// Tests that calling the Or extension method on a null 'this' parser throws a NullReferenceException.
//         /// </summary>
// //         [Fact] [Error] (94-68)CS1061 'Parser<int>' does not contain a definition for 'Or' and no accessible extension method 'Or' accepting a first argument of type 'Parser<int>' could be found (are you missing a using directive or an assembly reference?)
// //         public void Or_WhenCalledOnNullThisParser_ThrowsNullReferenceException()
// //         {
// //             // Arrange
// //             Parser<int> nullParser = null;
// //             // Act & Assert
// //             Assert.Throws<NullReferenceException>(() => nullParser.Or(_intParser2));
// //         }
// 
// #endregion
// #region Tests for Or<A, B, T>(this Parser<A> parser, Parser<B> or)
//         /// <summary>
//         /// Tests that the generic Or extension method correctly creates a OneOf instance for compatible types.
//         /// </summary>
// //         [Fact] [Error] (110-37)CS1929 'FakeParser<int>' does not contain a definition for 'Or' and the best extension method overload 'Parsers.Or<int, string, object>(Parser<int>, Parser<string>)' requires a receiver of type 'Parlot.Fluent.Parser<int>' [Error] (113-35)CS1061 'OneOf<int, string, object>' does not contain a definition for 'OriginalParsers' and no accessible extension method 'OriginalParsers' accepting a first argument of type 'OneOf<int, string, object>' could be found (are you missing a using directive or an assembly reference?) [Error] (114-42)CS1061 'OneOf<int, string, object>' does not contain a definition for 'OriginalParsers' and no accessible extension method 'OriginalParsers' accepting a first argument of type 'OneOf<int, string, object>' could be found (are you missing a using directive or an assembly reference?) [Error] (115-45)CS1061 'OneOf<int, string, object>' does not contain a definition for 'OriginalParsers' and no accessible extension method 'OriginalParsers' accepting a first argument of type 'OneOf<int, string, object>' could be found (are you missing a using directive or an assembly reference?)
// //         public void Or_Generic_WhenCalled_ReturnsOneOfWithBothParsers()
// //         {
// //             // Arrange
// //             // For this test, T is object, A is int, B is string.
// //             var intParser = new FakeParser<int>(10);
// //             var stringParser = new FakeParser<string>("test");
// //             // Act
// //             Parser<object> result = intParser.Or<int, string, object>(stringParser);
// //             // Assert
// //             var oneOf = Assert.IsType<OneOf<int, string, object>>(result);
// //             Assert.Equal(2, oneOf.OriginalParsers.Length);
// //             Assert.Same(intParser, oneOf.OriginalParsers[0]);
// //             Assert.Same(stringParser, oneOf.OriginalParsers[1]);
// //         }
// 
//         /// <summary>
//         /// Tests that calling the generic Or extension method with a null second parser allows null in the OneOf.
//         /// </summary>
// //         [Fact] [Error] (128-37)CS1929 'FakeParser<int>' does not contain a definition for 'Or' and the best extension method overload 'Parsers.Or<int, string, object>(Parser<int>, Parser<string>)' requires a receiver of type 'Parlot.Fluent.Parser<int>' [Error] (131-35)CS1061 'OneOf<int, string, object>' does not contain a definition for 'OriginalParsers' and no accessible extension method 'OriginalParsers' accepting a first argument of type 'OneOf<int, string, object>' could be found (are you missing a using directive or an assembly reference?) [Error] (132-42)CS1061 'OneOf<int, string, object>' does not contain a definition for 'OriginalParsers' and no accessible extension method 'OriginalParsers' accepting a first argument of type 'OneOf<int, string, object>' could be found (are you missing a using directive or an assembly reference?) [Error] (133-31)CS1061 'OneOf<int, string, object>' does not contain a definition for 'OriginalParsers' and no accessible extension method 'OriginalParsers' accepting a first argument of type 'OneOf<int, string, object>' could be found (are you missing a using directive or an assembly reference?)
// //         public void Or_Generic_WhenCalledWithNullSecondParser_AllowsNullInParsersArray()
// //         {
// //             // Arrange
// //             var intParser = new FakeParser<int>(20);
// //             Parser<string> nullStringParser = null;
// //             // Act
// //             Parser<object> result = intParser.Or<int, string, object>(nullStringParser);
// //             // Assert
// //             var oneOf = Assert.IsType<OneOf<int, string, object>>(result);
// //             Assert.Equal(2, oneOf.OriginalParsers.Length);
// //             Assert.Same(intParser, oneOf.OriginalParsers[0]);
// //             Assert.Null(oneOf.OriginalParsers[1]);
// //         }
// 
//         /// <summary>
//         /// Tests that calling the generic Or extension method on a null 'this' parser throws a NullReferenceException.
//         /// </summary>
// //         [Fact] [Error] (146-57)CS1929 'FakeParser<int>' does not contain a definition for 'Or' and the best extension method overload 'Parsers.Or<int, string, object>(Parser<int>, Parser<string>)' requires a receiver of type 'Parlot.Fluent.Parser<int>'
// //         public void Or_Generic_WhenCalledOnNullThisParser_ThrowsNullReferenceException()
// //         {
// //             // Arrange
// //             FakeParser<int> nullIntParser = null;
// //             var stringParser = new FakeParser<string>("test");
// //             // Act & Assert
// //             Assert.Throws<NullReferenceException>(() => nullIntParser.Or<int, string, object>(stringParser));
// //         }
// 
// #endregion
// #region Tests for OneOf<T>(params Parser<T>[] parsers)
//         /// <summary>
//         /// Tests that the OneOf method returns a OneOf instance containing all provided parsers.
//         /// </summary>
// //         [Fact] [Error] (162-42)CS0411 The type arguments for method 'Parsers.OneOf<T>(params Parser<T>[])' cannot be inferred from the usage. Try specifying the type arguments explicitly. [Error] (165-51)CS1061 'IReadOnlyList<Parser<int>>' does not contain a definition for 'Length' and no accessible extension method 'Length' accepting a first argument of type 'IReadOnlyList<Parser<int>>' could be found (are you missing a using directive or an assembly reference?)
// //         public void OneOf_WithMultipleParsers_ReturnsOneOfContainingAllParsers()
// //         {
// //             // Arrange
// //             var parser1 = _intParser1;
// //             var parser2 = _intParser2;
// //             var parser3 = _intParser3;
// //             // Act
// //             Parser<int> result = Parsers.OneOf(parser1, parser2, parser3);
// //             // Assert
// //             var oneOf = Assert.IsType<OneOf<int>>(result);
// //             Assert.Equal(3, oneOf.OriginalParsers.Length);
// //             Assert.Same(parser1, oneOf.OriginalParsers[0]);
// //             Assert.Same(parser2, oneOf.OriginalParsers[1]);
// //             Assert.Same(parser3, oneOf.OriginalParsers[2]);
// //         }
// 
//         /// <summary>
//         /// Tests that the OneOf method works correctly when provided with an empty array of parsers.
//         /// </summary>
// //         [Fact] [Error] (178-34)CS0029 Cannot implicitly convert type 'Parlot.Fluent.Parser<int>' to 'Parlot.Fluent.UnitTests.Parser<int>'
// //         public void OneOf_WithEmptyArray_ReturnsOneOfWithEmptyOriginalParsers()
// //         {
// //             // Act
// //             Parser<int> result = Parsers.OneOf<int>();
// //             // Assert
// //             var oneOf = Assert.IsType<OneOf<int>>(result);
// //             Assert.Empty(oneOf.OriginalParsers);
// //         }
// #endregion
//     }
// 
//     /// <summary>
//     /// A fake implementation of Parser<T> for unit testing purposes.
//     /// </summary>
//     /// <typeparam name = "T">The result type of the parser.</typeparam>
// //     internal class FakeParser<T> : Parser<T> [Error] (190-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParser'
// //     {
// //         /// <summary>
// //         /// A value to help identify this FakeParser instance.
// //         /// </summary>
// //         public T Value { get; }
// // 
// //         /// <summary>
// //         /// Initializes a new instance of the FakeParser class with the provided value.
// //         /// </summary>
// //         /// <param name = "value">The value for identification purposes.</param>
// //         public FakeParser(T value)
// //         {
// //             Value = value;
// //         }
// //     }
// }
