// using Moq;
// using Parlot.Compilation;
// using Parlot.Fluent;
// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "Switch{T, U}"/> class.
// /// </summary>
// public class SwitchTests
// {
//     /// <summary>
//     /// A fake implementation of ParseContext for testing purposes.
//     /// </summary>
//     private class FakeParseContext : ParseContext
//     {
//         public override void EnterParser(object parser)
//         {
//         // No-op for testing.
//         }
// 
//         public override void ExitParser(object parser)
//         {
//         // No-op for testing.
//         }
//     }
// 
//     /// <summary>
//     /// A fake implementation of Parser&lt;T&gt; for testing purposes.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
// //     private class FakeParser<T> : Parser<T> [Error] (34-19)CS0534 'SwitchTests.FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
// //     {
// //         private readonly bool _shouldSucceed;
// //         private readonly T _value;
// //         private readonly int _start;
// //         private readonly int _end;
// //         /// <summary>
// //         /// Initializes a new instance of the <see cref = "FakeParser{T}"/> class.
// //         /// </summary>
// //         /// <param name = "shouldSucceed">Determines if Parse should succeed.</param>
// //         /// <param name = "value">The value to set in the parse result.</param>
// //         /// <param name = "start">The start position to set in the parse result.</param>
// //         /// <param name = "end">The end position to set in the parse result.</param>
// //         public FakeParser(bool shouldSucceed, T value, int start = 10, int end = 20)
// //         {
// //             _shouldSucceed = shouldSucceed;
// //             _value = value;
// //             _start = start;
// //             _end = end;
// //         }
// // 
// //         /// <summary>
// //         /// Simulates parsing by setting the result if configured to succeed.
// //         /// </summary>
// //         /// <param name = "context">The parse context.</param>
// //         /// <param name = "result">The parse result (passed by reference).</param>
// //         /// <returns>True if parsing succeeds; otherwise, false.</returns>
// //         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (61-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
// //         {
// //             if (_shouldSucceed)
// //             {
// //                 result.Set(_start, _end, _value);
// //                 return true;
// //             }
// // 
// //             return false;
// //         }
//     }
// 
//     /// <summary>
//     /// Tests the Parse method when the previous parser fails.
//     /// Expected to return false and not invoke the action delegate.
//     /// </summary>
// //     [Fact] [Error] (88-68)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>' [Error] (90-26)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
// //     public void Parse_PreviousParserFails_ReturnsFalse()
// //     {
// //         // Arrange
// //         var fakePrevParser = new FakeParser<int>(shouldSucceed: false, value: 0);
// //         bool actionCalled = false;
// //         Func<ParseContext, int, Parser<string>> action = (ctx, val) =>
// //         {
// //             actionCalled = true;
// //             return null;
// //         };
// //         var switchParser = new Switch<int, string>(fakePrevParser, action);
// //         var fakeContext = new FakeParseContext();
// //         var result = new ParseResult<string>();
// //         // Act
// //         bool parseOutcome = switchParser.Parse(fakeContext, ref result);
// //         // Assert
// //         Assert.False(parseOutcome);
// //         Assert.False(actionCalled);
// //     }
// 
//     /// <summary>
//     /// Tests the Parse method when the action delegate returns null.
//     /// Expected to return false.
//     /// </summary>
// //     [Fact] [Error] (108-68)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>' [Error] (110-26)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
// //     public void Parse_ActionReturnsNull_ReturnsFalse()
// //     {
// //         // Arrange
// //         var fakePrevParser = new FakeParser<int>(shouldSucceed: true, value: 42);
// //         Func<ParseContext, int, Parser<string>> action = (ctx, val) => null;
// //         var switchParser = new Switch<int, string>(fakePrevParser, action);
// //         var fakeContext = new FakeParseContext();
// //         var result = new ParseResult<string>();
// //         // Act
// //         bool parseOutcome = switchParser.Parse(fakeContext, ref result);
// //         // Assert
// //         Assert.False(parseOutcome);
// //     }
// 
//     /// <summary>
//     /// Tests the Parse method when the next parser (returned by the action delegate) fails.
//     /// Expected to return false.
//     /// </summary>
// //     [Fact] [Error] (128-68)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>' [Error] (130-26)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
// //     public void Parse_NextParserFails_ReturnsFalse()
// //     {
// //         // Arrange
// //         var fakePrevParser = new FakeParser<int>(shouldSucceed: true, value: 42);
// //         // Action returns a fake parser that will fail.
// //         Func<ParseContext, int, Parser<string>> action = (ctx, val) => new FakeParser<string>(shouldSucceed: false, value: null);
// //         var switchParser = new Switch<int, string>(fakePrevParser, action);
// //         var fakeContext = new FakeParseContext();
// //         var result = new ParseResult<string>();
// //         // Act
// //         bool parseOutcome = switchParser.Parse(fakeContext, ref result);
// //         // Assert
// //         Assert.False(parseOutcome);
// //     }
// 
//     /// <summary>
//     /// Tests the Parse method when both the previous and next parsers succeed.
//     /// Expected to set the result from the next parser and return true.
//     /// </summary>
// //     [Fact] [Error] (157-68)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>' [Error] (159-26)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
// //     public void Parse_NextParserSucceeds_SetsResultAndReturnsTrue()
// //     {
// //         // Arrange
// //         int previousValue = 42;
// //         var fakePrevParser = new FakeParser<int>(shouldSucceed: true, value: previousValue, start: 5, end: 15);
// //         string nextValue = "Success";
// //         var fakeNextParser = new FakeParser<string>(shouldSucceed: true, value: nextValue, start: 15, end: 25);
// //         bool actionCalled = false;
// //         Func<ParseContext, int, Parser<string>> action = (ctx, val) =>
// //         {
// //             actionCalled = true;
// //             // Validate that the previous parser's value is passed correctly.
// //             Assert.Equal(previousValue, val);
// //             return fakeNextParser;
// //         };
// //         var switchParser = new Switch<int, string>(fakePrevParser, action);
// //         var fakeContext = new FakeParseContext();
// //         var result = new ParseResult<string>();
// //         // Act
// //         bool parseOutcome = switchParser.Parse(fakeContext, ref result);
// //         // Assert
// //         Assert.True(parseOutcome);
// //         Assert.True(actionCalled);
// //         Assert.Equal(15, result.Start);
// //         Assert.Equal(25, result.End);
// //         Assert.Equal(nextValue, result.Value);
// //     }
// 
//     /// <summary>
//     /// Gets or sets the parse context.
//     /// </summary>
// //     public abstract ParseContext ParseContext { get; set; } [Error] (173-49)CS0513 'SwitchTests.ParseContext.get' is abstract but it is contained in non-abstract type 'SwitchTests' [Error] (173-54)CS0513 'SwitchTests.ParseContext.set' is abstract but it is contained in non-abstract type 'SwitchTests'
//     /// <summary>
//     /// Gets or sets a value indicating whether the result should be discarded.
//     /// </summary>
// //     public abstract bool DiscardResult { get; set; } [Error] (177-42)CS0513 'SwitchTests.DiscardResult.get' is abstract but it is contained in non-abstract type 'SwitchTests' [Error] (177-47)CS0513 'SwitchTests.DiscardResult.set' is abstract but it is contained in non-abstract type 'SwitchTests'
//     /// <summary>
//     /// Gets the next unique number.
//     /// </summary>
// //     public abstract int NextNumber { get; } [Error] (181-38)CS0513 'SwitchTests.NextNumber.get' is abstract but it is contained in non-abstract type 'SwitchTests'
// 
//     /// <summary>
//     /// Creates a new compilation result for the specified type.
//     /// </summary>
//     /// <typeparam name = "T">The type for the compilation result.</typeparam>
//     /// <returns>A new instance of <see cref = "CompilationResult"/>.</returns>
// //     public abstract CompilationResult CreateCompilationResult<T>(); [Error] (188-39)CS0513 'SwitchTests.CreateCompilationResult<T>()' is abstract but it is contained in non-abstract type 'SwitchTests'
//     /// <summary>
//     /// Dummy implementation of the Parser class used to simulate behavior for testing ToString.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter for the parser.</typeparam>
// //     private class DummyParser<T> : Parser<T> [Error] (193-19)CS0534 'SwitchTests.DummyParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
// //     {
// //         private readonly string _toStringResult;
// //         public DummyParser(string toStringResult)
// //         {
// //             _toStringResult = toStringResult;
// //         }
// // 
// //         /// <summary>
// //         /// This implementation is not used in tests; it throws a NotImplementedException.
// //         /// </summary>
// //         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (204-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
// //         {
// //             throw new NotImplementedException();
// //         }
// 
//         /// <summary>
//         /// Returns the pre-defined string representation.
//         /// </summary>
//         public override string ToString() => _toStringResult;
//     }
// 
//     /// <summary>
//     /// Tests the ToString method of Switch when the previous parser is non-null.
//     /// It verifies that the returned string concatenates the DummyParser's string representation with " (Switch)".
//     /// </summary>
// //     [Fact] [Error] (225-67)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>'
// //     public void ToString_WhenPreviousParserNotNull_ReturnsConcatenatedString()
// //     {
// //         // Arrange
// //         var dummyParser = new DummyParser<int>("DummyParser");
// //         Func<ParseContext, int, Parser<string>> dummyAction = (ctx, value) => new DummyParser<string>("Ignored");
// //         var switchInstance = new Switch<int, string>(dummyParser, dummyAction);
// //         // Act
// //         string result = switchInstance.ToString();
// //         // Assert
// //         Assert.Equal("DummyParser (Switch)", result);
// //     }
// 
//     /// <summary>
//     /// Tests the ToString method of Switch when the previous parser is null.
//     /// It verifies that the returned string still appends " (Switch)" even if the previous parser is null.
//     /// </summary>
// //     [Fact] [Error] (242-66)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>'
// //     public void ToString_WhenPreviousParserIsNull_ReturnsStringWithSwitchOnly()
// //     {
// //         // Arrange
// //         Parser<int> nullParser = null;
// //         Func<ParseContext, int, Parser<string>> dummyAction = (ctx, value) => new DummyParser<string>("Ignored");
// //         var switchInstance = new Switch<int, string>(nullParser, dummyAction);
// //         // Act
// //         string result = switchInstance.ToString();
// //         // Assert
// //         Assert.Equal(" (Switch)", result);
// //     }
// 
//     /// <summary>
//     /// Tests the ToString method of Switch when the previous parser's ToString returns an empty string.
//     /// It verifies that the resulting string properly appends " (Switch)" to the empty string.
//     /// </summary>
// //     [Fact] [Error] (259-67)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>'
// //     public void ToString_WhenPreviousParserReturnsEmpty_ReturnsSwitchSuffixOnly()
// //     {
// //         // Arrange
// //         var dummyParser = new DummyParser<int>(string.Empty);
// //         Func<ParseContext, int, Parser<string>> dummyAction = (ctx, value) => new DummyParser<string>("Ignored");
// //         var switchInstance = new Switch<int, string>(dummyParser, dummyAction);
// //         // Act
// //         string result = switchInstance.ToString();
// //         // Assert
// //         Assert.Equal(" (Switch)", result);
// //     }
// 
//     /// <summary>
//     /// Dummy parser implementation for testing purposes.
//     /// Inherits from the abstract <see cref = "Parser{T}"/> class.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
// //     private class DummyParser<T> : Parser<T> [Error] (271-19)CS0102 The type 'SwitchTests' already contains a definition for 'DummyParser'
// //     {
// //         /// <summary>
// //         /// Dummy implementation of the Parse method.
// //         /// </summary>
// //         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (276-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (276-30)CS0111 Type 'SwitchTests.DummyParser<T>' already defines a member called 'Parse' with the same parameter types
// //         {
// //             throw new NotImplementedException();
// //         }
// // 
// //         /// <summary>
// //         /// Overrides ToString to return a fixed identifier.
// //         /// </summary>
// //         public override string ToString() [Error] (284-32)CS0111 Type 'SwitchTests.DummyParser<T>' already defines a member called 'ToString' with the same parameter types
// //         {
// //             return "DummyParser";
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests the constructor to ensure that a valid instance of <see cref = "Switch{T, U}"/> is created when provided with valid parameters.
//     /// </summary>
// //     [Fact] [Error] (297-34)CS7036 There is no argument given that corresponds to the required parameter 'toStringResult' of 'SwitchTests.DummyParser<int>.DummyParser(string)' [Error] (298-82)CS7036 There is no argument given that corresponds to the required parameter 'toStringResult' of 'SwitchTests.DummyParser<string>.DummyParser(string)' [Error] (300-64)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>'
// //     public void Constructor_WithValidParameters_ShouldCreateInstance()
// //     {
// //         // Arrange
// //         var previousParser = new DummyParser<int>();
// //         Func<ParseContext, int, Parser<string>> action = (context, value) => new DummyParser<string>();
// //         // Act
// //         var instance = new Switch<int, string>(previousParser, action);
// //         // Assert
// //         Assert.NotNull(instance);
// //         // The ToString method returns the previous parser's ToString appended with " (Switch)"
// //         Assert.Equal("DummyParser (Switch)", instance.ToString());
// //     }
// 
//     /// <summary>
//     /// Tests the constructor to ensure that it throws an <see cref = "ArgumentNullException"/>
//     /// when the previousParser parameter is null.
//     /// </summary>
// //     [Fact] [Error] (316-82)CS7036 There is no argument given that corresponds to the required parameter 'toStringResult' of 'SwitchTests.DummyParser<string>.DummyParser(string)' [Error] (318-104)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>'
// //     public void Constructor_NullPreviousParser_ShouldThrowArgumentNullException()
// //     {
// //         // Arrange
// //         Parser<int> nullParser = null;
// //         Func<ParseContext, int, Parser<string>> action = (context, value) => new DummyParser<string>();
// //         // Act & Assert
// //         var exception = Assert.Throws<ArgumentNullException>(() => new Switch<int, string>(nullParser, action));
// //         Assert.Equal("previousParser", exception.ParamName);
// //     }
// 
//     /// <summary>
//     /// Tests the constructor to ensure that it throws an <see cref = "ArgumentNullException"/>
//     /// when the action parameter is null.
//     /// </summary>
// //     [Fact] [Error] (330-34)CS7036 There is no argument given that corresponds to the required parameter 'toStringResult' of 'SwitchTests.DummyParser<int>.DummyParser(string)' [Error] (333-108)CS1503 Argument 2: cannot convert from 'System.Func<ParseContext, int, Parlot.Fluent.Parser<string>>' to 'System.Func<Parlot.Fluent.ParseContext, int, Parlot.Fluent.Parser<string>>'
// //     public void Constructor_NullAction_ShouldThrowArgumentNullException()
// //     {
// //         // Arrange
// //         var previousParser = new DummyParser<int>();
// //         Func<ParseContext, int, Parser<string>> nullAction = null;
// //         // Act & Assert
// //         var exception = Assert.Throws<ArgumentNullException>(() => new Switch<int, string>(previousParser, nullAction));
// //         Assert.Equal("action", exception.ParamName);
// //     }
// }
