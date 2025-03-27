// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Rewriting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Separated{U, T}"/> class focusing on the Parse method.
/// </summary>
// public class SeparatedTests [Error] (690-2)CS1513 } expected
// {
//     /// <summary>
//     /// Tests that Parse returns false if the first element parser fails.
//     /// </summary>
//     [Fact]
//     public void Parse_FirstElementParseFails_ReturnsFalse()
//     {
//         // Arrange
//         // Fake separator (should not be invoked because first element parse fails)
//         var fakeSeparator = new FakeParser<string>((context, result) => true);
//         // Fake element parser that fails on first attempt.
//         var fakeElement = new FakeParser<int>((context, result) => false);
//         var separated = new Separated<string, int>(fakeSeparator, fakeElement);
//         var context = new FakeParseContext();
//         var result = new ParseResult<IReadOnlyList<int>>();
//         // Act
//         bool parseResult = separated.Parse(context, ref result);
//         // Assert
//         Assert.False(parseResult);
//     }
// 
//     /// <summary>
//     /// Tests that Parse returns true and collects one element when the first element parses successfully and the separator fails immediately.
//     /// </summary>
//     [Fact]
//     public void Parse_SeparatorFailsAfterFirstElement_ReturnsTrueAndOneElement()
//     {
//         // Arrange
//         // Fake separator that fails on first call.
//         var fakeSeparator = new FakeParser<string>((context, result) => false);
//         // Fake element parser that succeeds on first call.
//         int elementValue = 42;
//         var fakeElement = new FakeParser<int>((context, result) =>
//         {
//             int start = context.Scanner.Cursor.Position.Offset;
//             // Simulate successful parse advancing the cursor by 10.
//             result.Set(start, start + 10, elementValue);
//             context.Scanner.Cursor.Position = new Position
//             {
//                 Offset = start + 10
//             };
//             return true;
//         });
//         var separated = new Separated<string, int>(fakeSeparator, fakeElement);
//         var context = new FakeParseContext();
//         var result = new ParseResult<IReadOnlyList<int>>();
//         // Act
//         bool parseOutcome = separated.Parse(context, ref result);
//         // Assert
//         Assert.True(parseOutcome);
//         Assert.NotNull(result.Value);
//         var list = result.Value;
//         Assert.Single(list);
//         Assert.Equal(elementValue, list[0]);
//     }
// 
//     /// <summary>
//     /// Tests that if the separator succeeds but the subsequent element parser fails after at least one successful parse,
//     /// the cursor is reset to the position prior to the failing parse and the method returns successfully with collected elements.
//     /// </summary>
//     [Fact]
//     public void Parse_ElementFailsAfterSeparator_ResetsCursorAndReturnsTrueWithCollectedElements()
//     {
//         // Arrange
//         // Fake separator that succeeds on its call.
//         var fakeSeparator = new FakeParser<string>((context, result) =>
//         {
//             int start = context.Scanner.Cursor.Position.Offset;
//             result.Set(start, start + 1, "sep");
//             // Advance cursor minimally.
//             context.Scanner.Cursor.Position = new Position
//             {
//                 Offset = start + 1
//             };
//             return true;
//         });
//         // Fake element parser: first call succeeds, second call fails.
//         bool firstCall = true;
//         int firstElementValue = 100;
//         var fakeElement = new FakeParser<int>((context, result) =>
//         {
//             if (firstCall)
//             {
//                 firstCall = false;
//                 int start = context.Scanner.Cursor.Position.Offset;
//                 result.Set(start, start + 5, firstElementValue);
//                 context.Scanner.Cursor.Position = new Position
//                 {
//                     Offset = start + 5
//                 };
//                 return true;
//             }
//             else
//             {
//                 // Simulate failure on second element
//                 return false;
//             }
//         });
//         var separated = new Separated<string, int>(fakeSeparator, fakeElement);
//         var context = new FakeParseContext();
//         // Set initial cursor offset to a known value.
//         context.Scanner.Cursor.Position = new Position
//         {
//             Offset = 0
//         };
//         var result = new ParseResult<IReadOnlyList<int>>();
//         // Act
//         bool parseOutcome = separated.Parse(context, ref result);
//         // Assert
//         // Expect parseOutcome to be true with one element collected.
//         Assert.True(parseOutcome);
//         Assert.NotNull(result.Value);
//         var list = result.Value;
//         Assert.Single(list);
//         Assert.Equal(firstElementValue, list[0]);
//         // The cursor should be reset to the end of the first parsed element.
//         Assert.Equal(5, context.Scanner.Cursor.Position.Offset);
//     }
// 
//     /// <summary>
//     /// Tests that Parse correctly collects multiple elements when both element and separator parsers succeed repeatedly,
//     /// and stops when the separator fails.
//     /// </summary>
//     [Fact]
//     public void Parse_MultipleElementsParsed_ReturnsTrueAndAllElementsCollected()
//     {
//         // Arrange
//         // Fake separator that succeeds for the first two invocations and then fails.
//         int separatorCallCount = 0;
//         var fakeSeparator = new FakeParser<string>((context, result) =>
//         {
//             separatorCallCount++;
//             // Succeed for first two calls, then fail on third.
//             if (separatorCallCount <= 2)
//             {
//                 int start = context.Scanner.Cursor.Position.Offset;
//                 result.Set(start, start + 1, "sep");
//                 context.Scanner.Cursor.Position = new Position
//                 {
//                     Offset = start + 1
//                 };
//                 return true;
//             }
// 
//             return false;
//         });
//         // Fake element parser that always succeeds.
//         int elementCallCount = 0;
//         var fakeElement = new FakeParser<int>((context, result) =>
//         {
//             elementCallCount++;
//             int start = context.Scanner.Cursor.Position.Offset;
//             int value = elementCallCount;
//             result.Set(start, start + 5, value);
//             context.Scanner.Cursor.Position = new Position
//             {
//                 Offset = start + 5
//             };
//             return true;
//         });
//         var separated = new Separated<string, int>(fakeSeparator, fakeElement);
//         var context = new FakeParseContext();
//         context.Scanner.Cursor.Position = new Position
//         {
//             Offset = 0
//         };
//         var result = new ParseResult<IReadOnlyList<int>>();
//         // Act
//         bool parseOutcome = separated.Parse(context, ref result);
//         // Assert
//         // Execution:
//         // First iteration: element parsed => value 1 (cursor: 0->5)
//         // Second iteration: separator parsed (cursor: 5->6), element parsed => value 2 (cursor: 6->11)
//         // Third iteration: separator parsed (cursor: 11->12) but then element parser is not called because separator will be attempted again
//         // Actually, logic: After two successful cycles, on third iteration, separator is called.
//         // Our fakeSeparator: on third call, returns false.
//         // So expected elements = [1,2]
//         Assert.True(parseOutcome);
//         Assert.NotNull(result.Value);
//         var list = result.Value;
//         Assert.Equal(2, list.Count);
//         Assert.Equal(1, list[0]);
//         Assert.Equal(2, list[1]);
//         // The final cursor position should remain at the end of the last successful parse.
//         Assert.Equal(11, context.Scanner.Cursor.Position.Offset);
//     }
// 
// #region Minimal Stub Classes for Parsing
//     // Minimal abstract Parser class definition.
//     public abstract class Parser<T>
//     {
//         public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
//     }
// 
//     // The Separated class under test.
//     public sealed class Separated<U, T> : Parser<IReadOnlyList<T>>, ICompilable, ISeekable
//     {
//         private readonly Parser<U> _separator;
//         private readonly Parser<T> _parser;
//         public Separated(Parser<U> separator, Parser<T> parser)
//         {
//             _separator = separator;
//             _parser = parser;
//         }
// 
//         public bool CanSeek => true;
//         public char[] ExpectedChars { get; } = new char[0];
//         public bool SkipWhitespace => true;
// 
//         public override bool Parse(ParseContext context, ref ParseResult<IReadOnlyList<T>> result)
//         {
//             context.EnterParser(this);
//             List<T> results = null;
//             var start = 0;
//             var end = context.Scanner.Cursor.Position;
//             var first = true;
//             var parsed = new ParseResult<T>();
//             var separatorResult = new ParseResult<U>();
//             while (true)
//             {
//                 if (!first)
//                 {
//                     if (!_separator.Parse(context, ref separatorResult))
//                     {
//                         break;
//                     }
//                 }
// 
//                 if (!_parser.Parse(context, ref parsed))
//                 {
//                     if (!first)
//                     {
//                         // A separator was found, but not followed by another value.
//                         // It's still successful if there was one value parsed, but we reset the cursor to before the separator
//                         context.Scanner.Cursor.ResetPosition(end);
//                         break;
//                     }
// 
//                     context.ExitParser(this);
//                     return false;
//                 }
//                 else
//                 {
//                     end = context.Scanner.Cursor.Position;
//                 }
// 
//                 if (first)
//                 {
//                     results = new List<T>();
//                     start = parsed.Start;
//                     first = false;
//                 }
// 
//                 results.Add(parsed.Value);
//             }
// 
//             result.Set(start, end.Offset, results ?? new List<T>());
//             context.ExitParser(this);
//             return true;
//         }
// 
//         public CompilationResult Compile(CompilationContext context)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => $"Separated({_separator}, {_parser})";
//     }
// 
//     // Minimal interface definitions for dependencies.
//     public interface ICompilable
//     {
//         CompilationResult Compile(CompilationContext context);
//     }
// 
//     public interface ISeekable
//     {
//         bool CanSeek { get; }
// 
//         char[] ExpectedChars { get; }
// 
//         bool SkipWhitespace { get; }
//     }
// 
//     public class CompilationResult
//     {
//     }
// 
//     public class CompilationContext
//     {
//     }
// 
//     // Minimal ParseContext and related classes.
//     public class ParseContext
//     {
//         public Scanner Scanner { get; } = new Scanner();
//         public List<object> EnteredParsers { get; } = new List<object>();
//         public List<object> ExitedParsers { get; } = new List<object>();
// 
//         public void EnterParser(object parser)
//         {
//             EnteredParsers.Add(parser);
//         }
// 
//         public void ExitParser(object parser)
//         {
//             ExitedParsers.Add(parser);
//         }
//     }
// 
//     public class FakeParseContext : ParseContext
//     {
//     }
// 
//     public class Scanner
//     {
//         public Cursor Cursor { get; } = new Cursor();
//     }
// 
//     public class Cursor
//     {
//         public Position Position { get; set; } = new Position
//         {
//             Offset = 0
//         };
// 
//         public void ResetPosition(Position pos)
//         {
//             Position = pos;
//         }
//     }
// 
//     public struct Position
//     {
//         public int Offset { get; set; }
//     }
// 
//     public class ParseResult<T>
//     {
//         public int Start { get; set; }
//         public T Value { get; set; }
//         public int EndOffset { get; set; }
// 
//         public void Set(int start, int endOffset, T value)
//         {
//             Start = start;
//             EndOffset = endOffset;
//             Value = value;
//         }
//     }
// 
//     // Fake parser to simulate parsing behavior.
//     public class FakeParser<T> : Parser<T>
//     {
//         private readonly Func<ParseContext, ParseResult<T>, bool> _parseFunc;
//         public FakeParser(Func<ParseContext, ParseResult<T>, bool> parseFunc)
//         {
//             _parseFunc = parseFunc;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<T> result)
//         {
//             return _parseFunc(context, result);
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method returns a CompilationResult with a non-empty body expression when given a valid CompilationContext.
//     /// </summary>
//     [Fact] [Error] (390-27)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (397-30)CS1729 'SeparatedTests.FakeParser<int>' does not contain a constructor that takes 2 arguments [Error] (398-33)CS1729 'SeparatedTests.FakeParser<char>' does not contain a constructor that takes 2 arguments [Error] (405-42)CS1061 'SeparatedTests.CompilationResult' does not contain a definition for 'Body' and no accessible extension method 'Body' accepting a first argument of type 'SeparatedTests.CompilationResult' could be found (are you missing a using directive or an assembly reference?) [Error] (406-39)CS1061 'SeparatedTests.CompilationResult' does not contain a definition for 'Body' and no accessible extension method 'Body' accepting a first argument of type 'SeparatedTests.CompilationResult' could be found (are you missing a using directive or an assembly reference?)
//     public void Compile_ValidContext_ReturnsCompilationResultWithBody()
//     {
//         // Arrange
//         // Set up a fake compilation context.
//         var context = new FakeCompilationContext();
//         // Since Separated<T, U> uses a static readonly _listAddMethodInfo field in its Compile method,
//         // we need to initialize it using reflection. For our test instance, T is int so we set it to List<int>.Add.
//         var listAddMethod = typeof(List<int>).GetMethod("Add");
//         var field = typeof(Separated<char, int>).GetField("_listAddMethodInfo", BindingFlags.NonPublic | BindingFlags.Static);
//         field.SetValue(null, listAddMethod);
//         // Create fake parser instances for _separator (U) and _parser (T)
//         var fakeParser = new FakeParser<int>(true, 42);
//         var fakeSeparator = new FakeParser<char>(true, 'x');
//         // Instantiate the Separated instance.
//         var separated = new Separated<char, int>(fakeSeparator, fakeParser);
//         // Act
//         var compilationResult = separated.Compile(context);
//         // Assert
//         Assert.NotNull(compilationResult);
//         Assert.NotNull(compilationResult.Body);
//         Assert.True(compilationResult.Body.Count > 0, "The compilation body should contain at least one expression.");
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method throws a NullReferenceException when a null CompilationContext is provided.
//     /// </summary>
//     [Fact] [Error] (416-30)CS1729 'SeparatedTests.FakeParser<int>' does not contain a constructor that takes 2 arguments [Error] (417-33)CS1729 'SeparatedTests.FakeParser<char>' does not contain a constructor that takes 2 arguments
//     public void Compile_NullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<int>(true, 42);
//         var fakeSeparator = new FakeParser<char>(true, 'x');
//         var separated = new Separated<char, int>(fakeSeparator, fakeParser);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => separated.Compile(null));
//     }
// 
//     /// <summary>
//     /// A fake parser implementation used for testing.
//     /// Inherits from Parser<T> and overrides ToString() to return a predetermined value.
//     /// The abstract Parse method is not used in these tests.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter for the parser.</typeparam>
//     private class FakeParser<T> : Parser<T> [Error] (429-19)CS0102 The type 'SeparatedTests' already contains a definition for 'FakeParser'
//     {
//         private readonly string _representation;
//         public FakeParser(string representation)
//         {
//             _representation = representation;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (437-30)CS0111 Type 'SeparatedTests.FakeParser<T>' already defines a member called 'Parse' with the same parameter types
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => _representation;
//     }
// 
//     /// <summary>
//     /// Tests the ToString method of Separated when the inner parsers return non-empty string representations.
//     /// Expected outcome is that the ToString method returns a string in the format "Separated({separator}, {parser})".
//     /// </summary>
//     [Fact]
//     public void ToString_WithNonEmptyParserRepresentations_ReturnsCorrectFormat()
//     {
//         // Arrange
//         string separatorRepresentation = "SEP";
//         string parserRepresentation = "PARSE";
//         var fakeSeparator = new FakeParser<char>(separatorRepresentation);
//         var fakeParser = new FakeParser<int>(parserRepresentation);
//         // Creating instance of Separated with type parameters U = char and T = int.
//         var separated = new Separated<char, int>(fakeSeparator, fakeParser);
//         string expected = $"Separated({separatorRepresentation}, {parserRepresentation})";
//         // Act
//         string actual = separated.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests the ToString method of Separated when the inner parsers return empty string representations.
//     /// Expected outcome is that the ToString method returns a string in the format "Separated(, )" when both representations are empty.
//     /// </summary>
//     [Fact]
//     public void ToString_WithEmptyParserRepresentations_ReturnsCorrectFormat()
//     {
//         // Arrange
//         string separatorRepresentation = string.Empty;
//         string parserRepresentation = string.Empty;
//         var fakeSeparator = new FakeParser<char>(separatorRepresentation);
//         var fakeParser = new FakeParser<int>(parserRepresentation);
//         // Creating instance of Separated with type parameters U = char and T = int.
//         var separated = new Separated<char, int>(fakeSeparator, fakeParser);
//         string expected = $"Separated({separatorRepresentation}, {parserRepresentation})";
//         // Act
//         string actual = separated.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor throws an ArgumentNullException when the separator parser is null.
//     /// </summary>
//     [Fact]
//     public void Separated_Constructor_NullSeparator_ThrowsArgumentNullException()
//     {
//         // Arrange
//         DummyParser<int> nonSeekableParser = new DummyParser<int>();
//         // Act & Assert
//         ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new Separated<char, int>(null, nonSeekableParser));
//         Assert.Equal("separator", exception.ParamName);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor throws an ArgumentNullException when the main parser is null.
//     /// </summary>
//     [Fact]
//     public void Separated_Constructor_NullParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         DummyParser<char> separatorParser = new DummyParser<char>();
//         // Act & Assert
//         ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new Separated<char, int>(separatorParser, null));
//         Assert.Equal("parser", exception.ParamName);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor successfully creates an instance when valid non-seekable parsers are provided.
//     /// </summary>
//     [Fact]
//     public void Separated_Constructor_WithNonSeekableParser_CreatesInstanceSuccessfully()
//     {
//         // Arrange
//         DummyParser<char> separatorParser = new DummyParser<char>();
//         DummyParser<int> nonSeekableParser = new DummyParser<int>();
//         // Act
//         Separated<char, int> instance = new Separated<char, int>(separatorParser, nonSeekableParser);
//         // Assert
//         Assert.NotNull(instance);
//         // Since the provided parser does not implement ISeekable, the properties should be default.
//         // We check that ExpectedChars is not null (as it is initialized) even if empty.
//         Assert.NotNull(instance.ExpectedChars);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor, when provided with a seekable parser, correctly sets the seekable properties.
//     /// </summary>
//     [Fact]
//     public void Separated_Constructor_WithSeekableParser_SetsPropertiesFromSeekableParser()
//     {
//         // Arrange
//         DummyParser<char> separatorParser = new DummyParser<char>();
//         DummySeekableParser seekableParser = new DummySeekableParser
//         {
//             CanSeek = true,
//             ExpectedChars = new char[]
//             {
//                 'a',
//                 'b',
//                 'c'
//             },
//             SkipWhitespace = true
//         };
//         // Act
//         Separated<char, int> instance = new Separated<char, int>(separatorParser, seekableParser);
//         // Assert
//         Assert.NotNull(instance);
//         Assert.Equal(seekableParser.CanSeek, instance.CanSeek);
//         Assert.Equal(seekableParser.ExpectedChars, instance.ExpectedChars);
//         Assert.Equal(seekableParser.SkipWhitespace, instance.SkipWhitespace);
//     }
// 
//     /// <summary>
//     /// A dummy implementation of the abstract Parser for testing purposes.
//     /// </summary>
//     /// <typeparam name = "T">The type of the parsing result.</typeparam>
//     private class DummyParser<T> : Parser<T>
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<T> result)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// A dummy implementation of a seekable parser for testing the assignment of seekable properties.
//     /// </summary>
//     private class DummySeekableParser : DummyParser<int>, ISeekable
//     {
//         public bool CanSeek { get; set; }
//         public char[] ExpectedChars { get; set; }
//         public bool SkipWhitespace { get; set; }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<int> result)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the CanSeek property returns true when the underlying parser's CanSeek is true.
//     /// </summary>
//     [Fact]
//     public void CanSeek_WhenUnderlyingParserIsTrue_ReturnsTrue()
//     {
//         // Arrange
//         bool expectedCanSeek = true;
//         var fakeSeparator = new FakeParser<string>(false); // The separator's CanSeek is irrelevant for this test.
//         var fakeParser = new FakeParser<int>(expectedCanSeek);
//         var separated = new Separated<string, int>(fakeSeparator, fakeParser);
//         // Act
//         bool actualCanSeek = separated.CanSeek;
//         // Assert
//         Assert.True(actualCanSeek);
//     }
// 
//     /// <summary>
//     /// Tests that the CanSeek property returns false when the underlying parser's CanSeek is false.
//     /// </summary>
//     [Fact]
//     public void CanSeek_WhenUnderlyingParserIsFalse_ReturnsFalse()
//     {
//         // Arrange
//         bool expectedCanSeek = false;
//         var fakeSeparator = new FakeParser<string>(true); // The separator's CanSeek is irrelevant for this test.
//         var fakeParser = new FakeParser<int>(expectedCanSeek);
//         var separated = new Separated<string, int>(fakeSeparator, fakeParser);
//         // Act
//         bool actualCanSeek = separated.CanSeek;
//         // Assert
//         Assert.False(actualCanSeek);
//     }
// 
//     /// <summary>
//     /// A minimal fake parser implementation used for testing. It extends Parser{T} and implements ISeekable.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter for the parser.</typeparam>
//     private class FakeParser<T> : Parser<T>, ISeekable [Error] (624-19)CS0102 The type 'SeparatedTests' already contains a definition for 'FakeParser' [Error] (624-46)CS0535 'SeparatedTests.FakeParser<T>' does not implement interface member 'SeparatedTests.ISeekable.ExpectedChars' [Error] (624-46)CS0535 'SeparatedTests.FakeParser<T>' does not implement interface member 'SeparatedTests.ISeekable.SkipWhitespace'
//     {
//         private readonly bool _canSeek;
//         /// <summary>
//         /// Initializes a new instance of the <see cref = "FakeParser{T}"/> class with a set CanSeek value.
//         /// </summary>
//         /// <param name = "canSeek">The value to be returned by the CanSeek property.</param>
//         public FakeParser(bool canSeek)
//         {
//             _canSeek = canSeek;
//         }
// 
//         /// <summary>
//         /// Gets a value indicating whether the parser supports seeking.
//         /// </summary>
//         public bool CanSeek => _canSeek;
// 
//         /// <summary>
//         /// A stub implementation of the abstract Parse method.
//         /// </summary>
//         /// <param name = "context">The parse context.</param>
//         /// <param name = "result">The parse result.</param>
//         /// <returns>This method always throws <see cref = "NotImplementedException"/>.</returns>
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (647-30)CS0111 Type 'SeparatedTests.FakeParser<T>' already defines a member called 'Parse' with the same parameter types
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the ExpectedChars property returns a non-null, empty char array.
//     /// </summary>
//     [Fact]
//     public void ExpectedChars_WhenAccessed_ReturnsEmptyArray()
//     {
//         // Arrange
//         // Create mock parser dependencies for the generic parameters.
//         var mockSeparator = new Mock<Parser<int>>();
//         var mockParser = new Mock<Parser<int>>();
//         // Instantiate the Separated class with the mocked dependencies.
//         var separated = new Separated<int, int>(mockSeparator.Object, mockParser.Object);
//         // Act
//         char[] result = separated.ExpectedChars;
//         // Assert
//         Assert.NotNull(result);
//         Assert.Empty(result);
//     }
// 
//     /// <summary>
//     /// Tests that the SkipWhitespace property returns the expected default value.
//     /// </summary>
//     [Fact]
//     public void SkipWhitespace_WhenInstanceCreated_ReturnsExpectedDefault()
//     {
//         // Arrange
//         // Create mocks for the separator and element parsers.
//         var mockSeparatorParser = new Mock<Parser<char>>();
//         var mockElementParser = new Mock<Parser<string>>();
//         // Act
//         // Instantiate the Separated parser with the mocked dependencies.
//         var separated = new Separated<char, string>(mockSeparatorParser.Object, mockElementParser.Object);
//         // Assert
//         // It is expected that SkipWhitespace returns the default value.
//         Assert.False(separated.SkipWhitespace, "Expected SkipWhitespace to be false by default.");
//     }
// #endregion
// }