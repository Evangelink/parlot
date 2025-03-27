using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Rewriting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Else{T}"/> class.
/// </summary>
// public class ElseTests [Error] (182-12)CS1520 Method must have a return type [Error] (184-45)CS0246 The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?)
// {
//     /// <summary>
//     /// Tests that the Parse method returns true and uses the result produced by the inner parser when it succeeds.
//     /// </summary>
//     [Fact] [Error] (33-46)CS1503 Argument 1: cannot convert from 'ElseTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (33-59)CS1503 Argument 2: cannot convert from 'ref ElseTests.FakeParseResult<int>' to 'ref Parlot.ParseResult<int>'
//     public void Parse_InnerParserSucceeds_ReturnsTrueAndUsesInnerParserResult()
//     {
//         // Arrange
//         int innerValue = 42;
//         int defaultValue = 100;
//         var fakeInnerParser = new FakeParser<int>(shouldSucceed: true, returnValue: innerValue);
//         var elseParser = new Else<int>(fakeInnerParser, defaultValue);
//         var context = new FakeParseContext();
//         var result = new FakeParseResult<int>
//         {
//             Start = 0,
//             End = 10
//         };
//         // Act
//         bool parseOutcome = elseParser.Parse(context, ref result);
//         // Assert
//         Assert.True(parseOutcome);
//         Assert.Equal(innerValue, result.Value);
//         Assert.Contains(elseParser, context.EnteredParsers);
//         Assert.Contains(elseParser, context.ExitedParsers);
//     }
// 
//     /// <summary>
//     /// Tests that the Parse method returns true and sets the default value when the inner parser fails.
//     /// </summary>
//     [Fact] [Error] (59-46)CS1503 Argument 1: cannot convert from 'ElseTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (59-59)CS1503 Argument 2: cannot convert from 'ref ElseTests.FakeParseResult<int>' to 'ref Parlot.ParseResult<int>'
//     public void Parse_InnerParserFails_ReturnsTrueAndSetsDefaultValue()
//     {
//         // Arrange
//         int defaultValue = 100;
//         var fakeInnerParser = new FakeParser<int>(shouldSucceed: false);
//         var elseParser = new Else<int>(fakeInnerParser, defaultValue);
//         var context = new FakeParseContext();
//         var result = new FakeParseResult<int>
//         {
//             Start = 5,
//             End = 15,
//             Value = 0
//         };
//         // Act
//         bool parseOutcome = elseParser.Parse(context, ref result);
//         // Assert
//         Assert.True(parseOutcome);
//         Assert.Equal(defaultValue, result.Value);
//         Assert.Contains(elseParser, context.EnteredParsers);
//         Assert.Contains(elseParser, context.ExitedParsers);
//     }
// 
//     /// <summary>
//     /// Tests that if the inner parser throws an exception, the exception propagates.
//     /// </summary>
//     [Fact] [Error] (83-73)CS1503 Argument 1: cannot convert from 'ElseTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (83-86)CS1503 Argument 2: cannot convert from 'ref ElseTests.FakeParseResult<int>' to 'ref Parlot.ParseResult<int>'
//     public void Parse_InnerParserThrows_ExceptionPropagates()
//     {
//         // Arrange
//         var throwingParser = new ThrowingParser<int>();
//         var elseParser = new Else<int>(throwingParser, 100);
//         var context = new FakeParseContext();
//         var result = new FakeParseResult<int>
//         {
//             Start = 0,
//             End = 0
//         };
//         // Act & Assert
//         Assert.Throws<InvalidOperationException>(() => elseParser.Parse(context, ref result));
//     }
// 
//     /// <summary>
//     /// Tests that if a null ParseContext is provided, a NullReferenceException is thrown.
//     /// </summary>
//     [Fact] [Error] (99-70)CS1503 Argument 1: cannot convert from 'ElseTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (99-83)CS1503 Argument 2: cannot convert from 'ref ElseTests.FakeParseResult<int>' to 'ref Parlot.ParseResult<int>'
//     public void Parse_NullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         int defaultValue = 100;
//         var fakeInnerParser = new FakeParser<int>(shouldSucceed: true, returnValue: 42);
//         var elseParser = new Else<int>(fakeInnerParser, defaultValue);
//         FakeParseContext context = null;
//         var result = new FakeParseResult<int>();
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => elseParser.Parse(context, ref result));
//     }
// 
//     // Helper fake implementations to support testing.
//     /// <summary>
//     /// A fake Parser implementation that can be configured to succeed or fail.
//     /// </summary>
//     /// <typeparam name = "T">The type of value being parsed.</typeparam>
//     private class FakeParser<T> : Parser<T> [Error] (107-19)CS0534 'ElseTests.FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//         private readonly bool _shouldSucceed;
//         private readonly T _returnValue;
//         public FakeParser(bool shouldSucceed, T returnValue = default)
//         {
//             _shouldSucceed = shouldSucceed;
//             _returnValue = returnValue;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (117-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             if (_shouldSucceed)
//             {
//                 result.Set(result.Start, result.End, _returnValue);
//                 return true;
//             }
// 
//             return false;
//         }
//     }
// 
//     /// <summary>
//     /// A fake Parser implementation that always throws an exception.
//     /// </summary>
//     /// <typeparam name = "T">The type of value being parsed.</typeparam>
//     private class ThrowingParser<T> : Parser<T> [Error] (133-19)CS0534 'ElseTests.ThrowingParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (135-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new InvalidOperationException("Simulated inner parser exception.");
//         }
//     }
// 
//     /// <summary>
//     /// A fake ParseContext for testing that records entered and exited parsers.
//     /// </summary>
//     private class FakeParseContext : ParseContext
//     {
//         public List<object> EnteredParsers { get; } = new List<object>();
//         public List<object> ExitedParsers { get; } = new List<object>();
// 
//         public override void EnterParser(object parser)
//         {
//             EnteredParsers.Add(parser);
//         }
// 
//         public override void ExitParser(object parser)
//         {
//             ExitedParsers.Add(parser);
//         }
//     }
// 
//     /// <summary>
//     /// A fake ParseResult implementation that holds parsed values.
//     /// </summary>
//     /// <typeparam name = "T">The type of value contained in the result.</typeparam>
//     private class FakeParseResult<T> : ParseResult<T> [Error] (164-40)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         public int Start { get; set; }
//         public int End { get; set; }
//         public T Value { get; set; }
// 
//         public override void Set(int start, int end, T value)
//         {
//             Start = start;
//             End = end;
//             Value = value;
//         }
//     }
// 
//     public List<Expression> Body { get; } = new List<Expression>();
//     public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();
//     public ParameterExpression Value { get; }
// 
//     public DummyCompilationResult()
//     {
//         Value = Expression.Parameter(typeof(T), "resultValue");
//     }
// 
//     /// <summary>
//     /// A fake parser used for testing which derives from Parser&lt;T&gt;.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
//     private class FakeParser<T> : Parser<T> [Error] (191-19)CS0102 The type 'ElseTests' already contains a definition for 'FakeParser'
//     {
//         private readonly string _representation;
//         public FakeParser(string representation)
//         {
//             _representation = representation;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (199-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (199-30)CS0111 Type 'ElseTests.FakeParser<T>' already defines a member called 'Parse' with the same parameter types
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString()
//         {
//             return _representation;
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the ToString method returns the expected representation when the inner parser's ToString returns a non-null value.
//     /// </summary>
//     [Fact]
//     public void ToString_WhenParserToStringReturnsValue_ReturnsParserRepresentationWithElseSuffix()
//     {
//         // Arrange
//         string parserRepresentation = "FakeParser";
//         var fakeParser = new FakeParser<string>(parserRepresentation);
//         var defaultValue = "default";
//         var elseParser = new Else<string>(fakeParser, defaultValue);
//         string expected = $"{parserRepresentation} (Else)";
//         // Act
//         string actual = elseParser.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that the ToString method returns the correct representation when the inner parser's ToString returns an empty string.
//     /// </summary>
//     [Fact]
//     public void ToString_WhenParserToStringReturnsEmpty_ReturnsElseSuffixOnly()
//     {
//         // Arrange
//         string parserRepresentation = string.Empty;
//         var fakeParser = new FakeParser<int>(parserRepresentation);
//         var defaultValue = 0;
//         var elseParser = new Else<int>(fakeParser, defaultValue);
//         string expected = $"{parserRepresentation} (Else)"; // expected to be " (Else)"
//         // Act
//         string actual = elseParser.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that the ToString method handles a null representation from the inner parser gracefully.
//     /// </summary>
//     [Fact]
//     public void ToString_WhenParserToStringReturnsNull_ReturnsElseSuffixOnly()
//     {
//         // Arrange
//         // Create a fake parser that returns null on ToString.
//         var fakeParser = new FakeParser<double>(null);
//         var defaultValue = 1.0;
//         var elseParser = new Else<double>(fakeParser, defaultValue);
//         // In C#, null in string interpolation is treated as empty string.
//         string expected = $"{(string)null} (Else)"; // which becomes " (Else)"
//         // Act
//         string actual = elseParser.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (265-58)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (265-70)CS0246 The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         result = default;
//         return false;
//     }
// 
//     public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (271-58)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         throw new NotImplementedException();
//     }
// 
//     /// <summary>
//     /// Tests that the ExpectedChars property returns an empty char array.
//     /// This test instantiates the Else class with a mocked parser and verifies that the ExpectedChars property returns an array that is not null and has no elements.
//     /// </summary>
//     [Fact]
//     public void ExpectedChars_WhenAccessed_ReturnsEmptyCharArray()
//     {
//         // Arrange
//         var dummyParserMock = new Mock<Parser<int>>();
//         var elseInstance = new Else<int>(dummyParserMock.Object, 42);
//         // Act
//         var result = elseInstance.ExpectedChars;
//         // Assert
//         Assert.NotNull(result);
//         Assert.Empty(result);
//     }
// 
//     /// <summary>
//     /// Tests that the SkipWhitespace property returns true when the inner parser's SkipWhitespace is true.
//     /// </summary>
//     [Fact]
//     public void SkipWhitespace_WhenInnerParserTrue_ReturnsTrue()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<int>(true);
//         var elseInstance = new Else<int>(fakeParser, 42);
//         // Act
//         bool result = elseInstance.SkipWhitespace;
//         // Assert
//         Assert.True(result, "SkipWhitespace should be true when the inner parser's SkipWhitespace is true.");
//     }
// 
//     /// <summary>
//     /// Tests that the SkipWhitespace property returns false when the inner parser's SkipWhitespace is false.
//     /// </summary>
//     [Fact]
//     public void SkipWhitespace_WhenInnerParserFalse_ReturnsFalse()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<int>(false);
//         var elseInstance = new Else<int>(fakeParser, 42);
//         // Act
//         bool result = elseInstance.SkipWhitespace;
//         // Assert
//         Assert.False(result, "SkipWhitespace should be false when the inner parser's SkipWhitespace is false.");
//     }
// }