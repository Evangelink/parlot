// using Moq;
// using System;
// using System.Linq;
// using Xunit;
// using Parlot.Fluent;
// using Parlot.Fluent.UnitTests.Fakes;
// 
// namespace Parlot.Fluent.UnitTests
// {
//     /// <summary>
//     /// Unit tests for the <see cref="ListOfChars"/> class.
//     /// </summary>
// //     public class ListOfCharsTests [Error] (25-41)CS0122 'ListOfChars' is inaccessible due to its protection level [Error] (27-38)CS0122 'ListOfChars' is inaccessible due to its protection level
// //     {
// //         private readonly ListOfChars _parserWithoutNewLine; [Error] (15-26)CS0122 'ListOfChars' is inaccessible due to its protection level
// //         private readonly ListOfChars _parserWithNewLine; [Error] (16-26)CS0122 'ListOfChars' is inaccessible due to its protection level
// 
//         /// <summary>
//         /// Initializes a new instance of the <see cref="ListOfCharsTests"/> class.
//         /// Sets up parser instances for testing scenarios with and without newline characters.
//         /// </summary>
//         public ListOfCharsTests()
//         {
//             // Parser without newline characters.
//             _parserWithoutNewLine = new ListOfChars("abc", 1);
//             // Parser with a newline character in its set.
//             _parserWithNewLine = new ListOfChars("a\n", 1);
//         }
// 
//         /// <summary>
//         /// Tests that the constructor initializes ExpectedChars and CanSeek properties.
//         /// Due to the ordering in the constructor, ExpectedChars remains empty and CanSeek remains false.
//         /// </summary>
// //         [Fact] [Error] (42-30)CS0122 'ListOfChars' is inaccessible due to its protection level [Error] (47-33)CS0122 'ListOfChars.ExpectedChars' is inaccessible due to its protection level [Error] (48-33)CS0122 'ListOfChars.CanSeek' is inaccessible due to its protection level
// //         public void Constructor_WhenCalled_InitializesPropertiesAsExpected()
// //         {
// //             // Arrange
// //             string testValues = "xyz";
// //             int minSize = 1;
// // 
// //             // Act
// //             var parser = new ListOfChars(testValues, minSize);
// // 
// //             // Assert
// //             // ExpectedChars remains the empty array as initialized,
// //             // and CanSeek remains false because the assignment occurs after the check.
// //             Assert.Empty(parser.ExpectedChars);
// //             Assert.False(parser.CanSeek);
// //         }
// 
//         /// <summary>
//         /// Tests the Parse method on a happy path when input matches expected characters without newline.
//         /// Verifies that the parser consumes the correct number of characters and advances the cursor.
//         /// </summary>
// //         [Fact] [Error] (64-56)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (64-69)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (68-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (69-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (70-40)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value' [Error] (71-37)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner' [Error] (72-37)CS1061 'FakeParseContext' does not contain a definition for 'EnteredParserCount' and no accessible extension method 'EnteredParserCount' accepting a first argument of type 'FakeParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (73-37)CS1061 'FakeParseContext' does not contain a definition for 'ExitedParserCount' and no accessible extension method 'ExitedParserCount' accepting a first argument of type 'FakeParseContext' could be found (are you missing a using directive or an assembly reference?)
// //         public void Parse_WhenInputMatchesExpectedCharsWithoutNewLine_ReturnsTrueAndAdvancesCursor()
// //         {
// //             // Arrange
// //             string input = "abcd"; // "abc" should match and 'd' stops the match.
// //             var context = new FakeParseContext(input);
// //             var result = new FakeParseResult<TextSpan>();
// // 
// //             // Act
// //             bool success = _parserWithoutNewLine.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.True(success);
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(3, result.End);
// //             Assert.Equal("abc", result.Value.ToString());
// //             Assert.Equal(3, context.Scanner.Cursor.Offset);
// //             Assert.Equal(1, context.EnteredParserCount);
// //             Assert.Equal(1, context.ExitedParserCount);
// //         }
// 
//         /// <summary>
//         /// Tests the Parse method when the input does not meet the minimum size requirement.
//         /// Verifies that the parser returns false and does not advance the cursor.
//         /// </summary>
// //         [Fact] [Error] (85-30)CS0122 'ListOfChars' is inaccessible due to its protection level [Error] (92-41)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (92-54)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (97-37)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner' [Error] (99-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (100-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (101-32)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value' [Error] (102-37)CS1061 'FakeParseContext' does not contain a definition for 'EnteredParserCount' and no accessible extension method 'EnteredParserCount' accepting a first argument of type 'FakeParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (103-37)CS1061 'FakeParseContext' does not contain a definition for 'ExitedParserCount' and no accessible extension method 'ExitedParserCount' accepting a first argument of type 'FakeParseContext' could be found (are you missing a using directive or an assembly reference?)
// //         public void Parse_WhenInputInsufficient_ReturnsFalseAndDoesNotAdvanceCursor()
// //         {
// //             // Arrange
// //             // Create a parser with minSize set to 2, though due to constructor ordering it won't set ExpectedChars.
// //             var parser = new ListOfChars("abc", 2);
// //             // Input has only one matching character.
// //             string input = "axxx";
// //             var context = new FakeParseContext(input);
// //             var result = new FakeParseResult<TextSpan>();
// // 
// //             // Act
// //             bool success = parser.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.False(success);
// //             // Cursor is not advanced.
// //             Assert.Equal(0, context.Scanner.Cursor.Offset);
// //             // Result remains unassigned.
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(0, result.End);
// //             Assert.Null(result.Value);
// //             Assert.Equal(1, context.EnteredParserCount);
// //             Assert.Equal(1, context.ExitedParserCount);
// //         }
// 
//         /// <summary>
//         /// Tests the Parse method on a happy path when input includes a newline.
//         /// Verifies that the parser uses Advance (which can process newlines) and advances the cursor accordingly.
//         /// </summary>
// //         [Fact] [Error] (120-53)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (120-66)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (124-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (125-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (126-40)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value' [Error] (128-37)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner' [Error] (129-37)CS1061 'FakeParseContext' does not contain a definition for 'EnteredParserCount' and no accessible extension method 'EnteredParserCount' accepting a first argument of type 'FakeParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (130-37)CS1061 'FakeParseContext' does not contain a definition for 'ExitedParserCount' and no accessible extension method 'ExitedParserCount' accepting a first argument of type 'FakeParseContext' could be found (are you missing a using directive or an assembly reference?)
// //         public void Parse_WhenInputMatchesExpectedCharsWithNewLine_ReturnsTrueAndUsesAdvance()
// //         {
// //             // Arrange
// //             // _parserWithNewLine has 'a' and newline in its character set.
// //             string input = "a\nb";
// //             var context = new FakeParseContext(input);
// //             var result = new FakeParseResult<TextSpan>();
// // 
// //             // Act
// //             bool success = _parserWithNewLine.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.True(success);
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(2, result.End);
// //             Assert.Equal("a\n", result.Value.ToString());
// //             // Cursor advanced by 2 characters.
// //             Assert.Equal(2, context.Scanner.Cursor.Offset);
// //             Assert.Equal(1, context.EnteredParserCount);
// //             Assert.Equal(1, context.ExitedParserCount);
// //         }
// 
//         /// <summary>
//         /// Tests the Parse method when a maximum size limit is specified.
//         /// Verifies that the parser does not consume more characters than the specified maximum.
//         /// </summary>
// //         [Fact] [Error] (142-30)CS0122 'ListOfChars' is inaccessible due to its protection level [Error] (148-41)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (148-54)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<Parlot.Fluent.UnitTests.TextSpan>' to 'ref Parlot.ParseResult<Parlot.TextSpan>' [Error] (152-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Start' and 'FakeParseResult<TextSpan>.Start' [Error] (153-36)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.End' and 'FakeParseResult<TextSpan>.End' [Error] (154-39)CS0229 Ambiguity between 'FakeParseResult<TextSpan>.Value' and 'FakeParseResult<TextSpan>.Value' [Error] (155-37)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner' [Error] (156-37)CS1061 'FakeParseContext' does not contain a definition for 'EnteredParserCount' and no accessible extension method 'EnteredParserCount' accepting a first argument of type 'FakeParseContext' could be found (are you missing a using directive or an assembly reference?) [Error] (157-37)CS1061 'FakeParseContext' does not contain a definition for 'ExitedParserCount' and no accessible extension method 'ExitedParserCount' accepting a first argument of type 'FakeParseContext' could be found (are you missing a using directive or an assembly reference?)
// //         public void Parse_WhenMaxSizeLimitReached_ConsumesOnlyMaxSizeCharacters()
// //         {
// //             // Arrange
// //             // Using maxSize = 2, so only two characters should be consumed.
// //             var parser = new ListOfChars("abc", 1, 2);
// //             string input = "abxyz";
// //             var context = new FakeParseContext(input);
// //             var result = new FakeParseResult<TextSpan>();
// // 
// //             // Act
// //             bool success = parser.Parse(context, ref result);
// // 
// //             // Assert
// //             Assert.True(success);
// //             Assert.Equal(0, result.Start);
// //             Assert.Equal(2, result.End);
// //             Assert.Equal("ab", result.Value.ToString());
// //             Assert.Equal(2, context.Scanner.Cursor.Offset);
// //             Assert.Equal(1, context.EnteredParserCount);
// //             Assert.Equal(1, context.ExitedParserCount);
// //         }
//     }
// }
// 
// namespace Parlot.Fluent.UnitTests.Fakes
// {
//     /// <summary>
//     /// Fake implementation of ParseContext for testing purposes.
//     /// </summary>
//     internal class FakeParseContext
//     {
//         public FakeScanner Scanner { get; }
//         public int EnteredParserCount { get; private set; }
//         public int ExitedParserCount { get; private set; }
// 
//         /// <summary>
//         /// Initializes a new instance of the <see cref="FakeParseContext"/> class with the specified input buffer.
//         /// </summary>
//         /// <param name="buffer">The input string to parse.</param>
//         public FakeParseContext(string buffer)
//         {
//             Scanner = new FakeScanner(buffer);
//         }
// 
//         /// <summary>
//         /// Simulates entering a parser.
//         /// </summary>
//         /// <param name="parser">The parser being entered.</param>
//         public void EnterParser(object parser)
//         {
//             EnteredParserCount++;
//         }
// 
//         /// <summary>
//         /// Simulates exiting a parser.
//         /// </summary>
//         /// <param name="parser">The parser being exited.</param>
//         public void ExitParser(object parser)
//         {
//             ExitedParserCount++;
//         }
//     }
// 
//     /// <summary>
//     /// Fake implementation of ParseResult for testing purposes.
//     /// </summary>
//     /// <typeparam name="T">The type of the value held in the result.</typeparam>
//     internal class FakeParseResult<T>
//     {
//         public int Start { get; private set; }
//         public int End { get; private set; }
//         public T Value { get; private set; }
// 
//         /// <summary>
//         /// Sets the result with the start offset, end offset, and value.
//         /// </summary>
//         /// <param name="start">The start offset.</param>
//         /// <param name="end">The end offset.</param>
//         /// <param name="value">The parsed value.</param>
//         public void Set(int start, int end, T value)
//         {
//             Start = start;
//             End = end;
//             Value = value;
//         }
//     }
// 
//     /// <summary>
//     /// Fake implementation of Scanner for testing purposes.
//     /// </summary>
//     internal class FakeScanner
//     {
//         public FakeCursor Cursor { get; }
//         public string Buffer { get; }
// 
//         /// <summary>
//         /// Initializes a new instance of the <see cref="FakeScanner"/> class with the specified buffer.
//         /// </summary>
//         /// <param name="buffer">The input string buffer.</param>
//         public FakeScanner(string buffer)
//         {
//             Buffer = buffer;
//             Cursor = new FakeCursor(buffer);
//         }
//     }
// 
//     /// <summary>
//     /// Fake implementation of Cursor for testing purposes.
//     /// </summary>
//     internal class FakeCursor
//     {
//         private int _offset;
//         private readonly string _buffer;
// 
//         /// <summary>
//         /// Initializes a new instance of the <see cref="FakeCursor"/> class.
//         /// </summary>
//         /// <param name="buffer">The input string buffer.</param>
//         public FakeCursor(string buffer)
//         {
//             _buffer = buffer;
//             _offset = 0;
//         }
// 
//         /// <summary>
//         /// Gets the remaining characters as an array.
//         /// </summary>
//         public char[] Span => _buffer.Substring(_offset).ToCharArray();
// 
//         /// <summary>
//         /// Gets the current offset in the buffer.
//         /// </summary>
//         public int Offset => _offset;
// 
//         /// <summary>
//         /// Advances the cursor by the specified count.
//         /// </summary>
//         /// <param name="count">The number of characters to advance.</param>
//         public void Advance(int count)
//         {
//             _offset += count;
//         }
// 
//         /// <summary>
//         /// Advances the cursor by the specified count, ignoring newlines.
//         /// </summary>
//         /// <param name="count">The number of characters to advance.</param>
//         public void AdvanceNoNewLines(int count)
//         {
//             _offset += count;
//         }
//     }
// 
//     /// <summary>
//     /// Fake implementation of TextSpan for testing purposes.
//     /// </summary>
//     internal class TextSpan
//     {
//         public string Buffer { get; }
//         public int Start { get; }
//         public int Length { get; }
// 
//         /// <summary>
//         /// Initializes a new instance of the <see cref="TextSpan"/> class.
//         /// </summary>
//         /// <param name="buffer">The source buffer.</param>
//         /// <param name="start">The start offset.</param>
//         /// <param name="length">The length of the span.</param>
//         public TextSpan(string buffer, int start, int length)
//         {
//             Buffer = buffer;
//             Start = start;
//             Length = length;
//         }
// 
//         /// <summary>
//         /// Returns the substring represented by the text span.
//         /// </summary>
//         /// <returns>The substring from the buffer.</returns>
//         public override string ToString()
//         {
//             return Buffer.Substring(Start, Length);
//         }
//     }
// }
