// using Moq;
// using Parlot.Fluent;
// using Parlot.Rewriting;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using Xunit;
// 
// /// <summary>
// /// Unit tests for the <see cref = "OneOf{T}"/> class focusing on the Parse method.
// /// </summary>
// public class OneOfTests
// {
//     /// <summary>
//     /// Tests that when the first parser in the OneOf parsers array succeeds, the Parse method returns true,
//     /// the result is set accordingly, and subsequent parsers are not invoked.
//     /// </summary>
// //     [Fact] [Error] (33-56)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<int>' [Error] (33-69)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<int>' [Error] (35-35)CS1503 Argument 1: cannot convert from 'OneOfTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (35-48)CS1503 Argument 2: cannot convert from 'ref OneOfTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>'
// //     public void Parse_WhenFirstParserSucceeds_ReturnsTrueAndSkipsSubsequentParsers()
// //     {
// //         // Arrange
// //         // Create fake cursor, scanner and parse context.
// //         var initialPosition = 10;
// //         var fakeCursor = new FakeCursor(initialPosition, 'a');
// //         var fakeScanner = new FakeScanner(fakeCursor);
// //         var context = new FakeParseContext(fakeScanner);
// //         var parseResult = new ParseResult<int>();
// //         // Create two fake parsers, where the first succeeds.
// //         var firstParser = new FakeParser(true, 42);
// //         var secondParser = new FakeParser(false, 100); // Should not be invoked
// //         // Create OneOf instance with the two parsers.
// //         var oneOf = new OneOf<int>(new Parser<int>[] { firstParser, secondParser });
// //         // Act
// //         bool result = oneOf.Parse(context, ref parseResult);
// //         // Assert
// //         Assert.True(result);
// //         Assert.True(parseResult.Success);
// //         Assert.Equal(42, parseResult.Value);
// //         Assert.Equal(1, firstParser.CallCount);
// //         Assert.Equal(0, secondParser.CallCount);
// //         // Verify that the OneOf parser has called EnterParser and ExitParser exactly once.
// //         Assert.Contains(nameof(OneOf<int>), context.EnteredParsers);
// //         Assert.Contains(nameof(OneOf<int>), context.ExitedParsers);
// //     }
// 
//     /// <summary>
//     /// Tests that when the first parser fails and a later parser succeeds, the Parse method returns true
//     /// and sets the result from the succeeding parser.
//     /// </summary>
// //     [Fact] [Error] (63-56)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<int>' [Error] (63-69)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<int>' [Error] (65-35)CS1503 Argument 1: cannot convert from 'OneOfTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (65-48)CS1503 Argument 2: cannot convert from 'ref OneOfTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>'
// //     public void Parse_WhenLaterParserSucceeds_ReturnsTrueAndProcessesAllParsersInOrder()
// //     {
// //         // Arrange
// //         var initialPosition = 5;
// //         var fakeCursor = new FakeCursor(initialPosition, 'b');
// //         var fakeScanner = new FakeScanner(fakeCursor);
// //         var context = new FakeParseContext(fakeScanner);
// //         var parseResult = new ParseResult<int>();
// //         // Create two fake parsers, where the first fails and the second succeeds.
// //         var firstParser = new FakeParser(false, 10);
// //         var secondParser = new FakeParser(true, 100);
// //         var oneOf = new OneOf<int>(new Parser<int>[] { firstParser, secondParser });
// //         // Act
// //         bool result = oneOf.Parse(context, ref parseResult);
// //         // Assert
// //         Assert.True(result);
// //         Assert.True(parseResult.Success);
// //         Assert.Equal(100, parseResult.Value);
// //         Assert.Equal(1, firstParser.CallCount);
// //         Assert.Equal(1, secondParser.CallCount);
// //         Assert.Contains(nameof(OneOf<int>), context.EnteredParsers);
// //         Assert.Contains(nameof(OneOf<int>), context.ExitedParsers);
// //     }
// 
//     /// <summary>
//     /// Tests that when all parsers fail, the Parse method returns false,
//     /// and if whitespace skipping logic were present, the cursor position would be reset.
//     /// </summary>
// //     [Fact] [Error] (92-56)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<int>' [Error] (92-69)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<int>' [Error] (94-35)CS1503 Argument 1: cannot convert from 'OneOfTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (94-48)CS1503 Argument 2: cannot convert from 'ref OneOfTests.ParseResult<int>' to 'ref Parlot.ParseResult<int>'
// //     public void Parse_WhenNoParserSucceeds_ReturnsFalseAndResetsPositionIfSkipWhitespace()
// //     {
// //         // Arrange
// //         var initialPosition = 20;
// //         var fakeCursor = new FakeCursor(initialPosition, 'c');
// //         var fakeScanner = new FakeScanner(fakeCursor);
// //         var context = new FakeParseContext(fakeScanner);
// //         var parseResult = new ParseResult<int>();
// //         // Create two fake parsers that always fail.
// //         var firstParser = new FakeParser(false, 0);
// //         var secondParser = new FakeParser(false, 0);
// //         var oneOf = new OneOf<int>(new Parser<int>[] { firstParser, secondParser });
// //         // Act
// //         bool result = oneOf.Parse(context, ref parseResult);
// //         // Assert
// //         Assert.False(result);
// //         Assert.False(parseResult.Success);
// //         Assert.Equal(1, firstParser.CallCount);
// //         Assert.Equal(1, secondParser.CallCount);
// //         // If SkipWhitespace were true, the cursor position should be reset to initialPosition.
// //         // Since our fake OneOf does not have SkipWhitespace behavior exposed, verify the position remains unchanged.
// //         Assert.Equal(initialPosition, fakeCursor.Position);
// //         Assert.Contains(nameof(OneOf<int>), context.EnteredParsers);
// //         Assert.Contains(nameof(OneOf<int>), context.ExitedParsers);
// //     }
// 
// #region Fake Classes for Testing
//     /// <summary>
//     /// A fake implementation of the abstract Parser for testing purposes.
//     /// </summary>
// //     private class FakeParser : Parser<int> [Error] (111-19)CS0263 Partial declarations of 'OneOfTests.FakeParser' must not specify different base classes
// //     {
// //         public bool ShouldSucceed { get; }
// //         public int ReturnValue { get; }
// //         public int CallCount { get; private set; }
// // 
// //         public FakeParser(bool shouldSucceed, int returnValue)
// //         {
// //             ShouldSucceed = shouldSucceed;
// //             ReturnValue = returnValue;
// //         }
// // 
// //         public override bool Parse(ParseContext context, ref ParseResult<int> result)
// //         {
// //             CallCount++;
// //             if (ShouldSucceed)
// //             {
// //                 result.Success = true;
// //                 result.Value = ReturnValue;
// //                 return true;
// //             }
// // 
// //             return false;
// //         }
// //     }
// 
//     /// <summary>
//     /// Minimal implementation of ParseResult used for testing.
//     /// </summary>
//     private class ParseResult<T>
//     {
//         public bool Success { get; set; }
//         public T Value { get; set; }
//     }
// 
//     /// <summary>
//     /// Minimal fake implementation of a cursor.
//     /// </summary>
//     private class FakeCursor
//     {
//         public int Position { get; set; }
//         public char Current { get; set; }
// 
//         public FakeCursor(int position, char current)
//         {
//             Position = position;
//             Current = current;
//         }
// 
//         public void ResetPosition(int position)
//         {
//             Position = position;
//         }
//     }
// 
//     /// <summary>
//     /// Minimal fake implementation of a scanner.
//     /// </summary>
//     private class FakeScanner
//     {
//         public FakeCursor Cursor { get; }
// 
//         public FakeScanner(FakeCursor cursor)
//         {
//             Cursor = cursor;
//         }
//     }
// 
//     /// <summary>
//     /// Minimal fake implementation of ParseContext for testing purposes.
//     /// </summary>
//     private class FakeParseContext : ParseContext
//     {
//         public List<string> EnteredParsers { get; } = new List<string>();
//         public List<string> ExitedParsers { get; } = new List<string>();
// 
//         public FakeParseContext(FakeScanner scanner) : base(new WrapperScanner(scanner))
//         {
//         }
// 
//         public override void EnterParser(object parser)
//         {
//             EnteredParsers.Add(parser.GetType().Name);
//         }
// 
//         public override void ExitParser(object parser)
//         {
//             ExitedParsers.Add(parser.GetType().Name);
//         }
//     }
// 
//     /// <summary>
//     /// Base ParseContext class as required by the OneOf class.
//     /// </summary>
//     public abstract class ParseContext
//     {
//         public Scanner Scanner { get; }
// 
//         protected ParseContext(Scanner scanner)
//         {
//             Scanner = scanner;
//         }
// 
//         public virtual void SkipWhiteSpace()
//         {
//         }
// 
//         public virtual void EnterParser(object parser)
//         {
//         }
// 
//         public virtual void ExitParser(object parser)
//         {
//         }
//     }
// 
//     /// <summary>
//     /// Minimal wrapper for Scanner to adapt FakeScanner to the required Scanner type.
//     /// </summary>
//     private class WrapperScanner : Scanner
//     {
//         public new FakeCursor Cursor { get; }
// 
//         public WrapperScanner(FakeScanner fakeScanner) : base(null)
//         {
//             Cursor = fakeScanner.Cursor;
//         }
//     }
// 
//     /// <summary>
//     /// Minimal Scanner base class as required by ParseContext.
//     /// </summary>
// //     public class Scanner [Error] (247-16)CS0051 Inconsistent accessibility: parameter type 'OneOfTests.FakeCursor' is less accessible than method 'OneOfTests.Scanner.Scanner(OneOfTests.FakeCursor)'
// //     {
// //         public virtual FakeCursor Cursor { get; } [Error] (245-35)CS0053 Inconsistent accessibility: property type 'OneOfTests.FakeCursor' is less accessible than property 'OneOfTests.Scanner.Cursor'
// 
//         public Scanner(FakeCursor cursor)
//         {
//             Cursor = cursor;
//         }
//     }
// 
//     /// <summary>
//     /// A fake parser for testing purposes.
//     /// Overrides ToString to return a custom representation.
//     /// Other abstract members throw NotImplementedException.
//     /// </summary>
// //     private sealed class FakeParser : Parser<string> [Error] (258-26)CS0102 The type 'OneOfTests' already contains a definition for 'FakeParser'
// //     {
// //         private readonly string _representation;
// //         public FakeParser(string representation)
// //         {
// //             _representation = representation;
// //         }
// // 
// //         /// <summary>
// //         /// Returns the custom string representation of the fake parser.
// //         /// </summary>
// //         /// <returns>The custom string representation.</returns>
// //         public override string ToString() => _representation;
// //         /// <summary>
// //         /// Not implemented. This method is not needed for ToString tests.
// //         /// </summary>
// //         public override bool Parse(ParseContext context, ref ParseResult<string> result)
// //         {
// //             throw new NotImplementedException();
// //         }
// //     }
// 
//     /// <summary>
//     /// Tests that ToString returns the expected format when there are non-empty parsers and the ExpectedChars is empty.
//     /// Expected behavior: The output is the joined string representations of the parsers followed by ") on []".
//     /// </summary>
// //     [Fact] [Error] (290-13)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<string>' [Error] (291-13)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<string>'
// //     public void ToString_NonEmptyParsersEmptyExpectedChars_ReturnsCorrectString()
// //     {
// //         // Arrange
// //         var fakeParsers = new Parser<string>[]
// //         {
// //             new FakeParser("A"),
// //             new FakeParser("B")
// //         };
// //         var oneOf = new OneOf<string>(fakeParsers);
// //         // Act
// //         var result = oneOf.ToString();
// //         // Assert
// //         Assert.Equal("A | B) on []", result);
// //     }
// 
//     /// <summary>
//     /// Tests that ToString returns the expected format when the parsers list is empty and ExpectedChars is empty.
//     /// Expected behavior: The output is ") on []" when no parsers are provided.
//     /// </summary>
//     [Fact]
//     public void ToString_EmptyParsersEmptyExpectedChars_ReturnsCorrectString()
//     {
//         // Arrange
//         var fakeParsers = Array.Empty<Parser<string>>();
//         var oneOf = new OneOf<string>(fakeParsers);
//         // Act
//         var result = oneOf.ToString();
//         // Assert
//         Assert.Equal(") on []", result);
//     }
// 
//     /// <summary>
//     /// Tests that ToString returns the updated format after modifying the ExpectedChars property using reflection.
//     /// Expected behavior: The output includes the custom expected characters in the format "[x y]".
//     /// </summary>
// //     [Fact] [Error] (326-13)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<string>' [Error] (327-13)CS0029 Cannot implicitly convert type 'OneOfTests.FakeParser' to 'Parlot.Fluent.Parser<string>'
// //     public void ToString_WithCustomExpectedChars_ReturnsUpdatedString()
// //     {
// //         // Arrange
// //         var fakeParsers = new Parser<string>[]
// //         {
// //             new FakeParser("A"),
// //             new FakeParser("B")
// //         };
// //         var oneOf = new OneOf<string>(fakeParsers);
// //         // Use reflection to set the backing field for the ExpectedChars auto-property.
// //         // The expected backing field name is in the format "<PropertyName>k__BackingField".
// //         var expectedCharsField = typeof(OneOf<string>).GetField("<ExpectedChars>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
// //         Assert.NotNull(expectedCharsField);
// //         expectedCharsField.SetValue(oneOf, new char[] { 'x', 'y' });
// //         // Act
// //         var result = oneOf.ToString();
// //         // Assert
// //         Assert.Equal("A | B) on [x y]", result);
// //     }
// 
//     /// <summary>
//     /// A dummy implementation of the Parser&lt;T&gt; abstract class.
//     /// This dummy parser does not implement any parsing logic.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
// //     private class DummyParser<T> : Parser<T> [Error] (346-19)CS0534 'OneOfTests.DummyParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
// //     {
// //         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (348-30)CS0115 'OneOfTests.DummyParser<T>.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<T>)': no suitable method found to override
// //         {
// //             return false;
// //         }
// 
//         public override string ToString() => "DummyParser";
//     }
// 
//     /// <summary>
//     /// A dummy implementation of a seekable parser.
//     /// Implements ISeekable interface members.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
// //     private class DummySeekableParser<T> : Parser<T>, ISeekable [Error] (361-19)CS0534 'OneOfTests.DummySeekableParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
// //     {
// //         public bool CanSeek { get; set; }
// //         public char[] ExpectedChars { get; set; }
// //         public bool SkipWhitespace { get; set; }
// // 
// //         public DummySeekableParser(bool canSeek, char[] expectedChars, bool skipWhitespace)
// //         {
// //             CanSeek = canSeek;
// //             ExpectedChars = expectedChars;
// //             SkipWhitespace = skipWhitespace;
// //         }
// // 
// //         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (374-30)CS0115 'OneOfTests.DummySeekableParser<T>.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<T>)': no suitable method found to override
// //         {
// //             return false;
// //         }
// 
//         public override string ToString() => $"DummySeekableParser({string.Join(",", ExpectedChars)})";
//     }
// 
//     /// <summary>
//     /// Tests that constructing a OneOf with a null parsers array throws an ArgumentNullException.
//     /// </summary>
//     [Fact]
//     public void OneOfConstructor_NullParsersArray_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Parser<string>[] nullParsers = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new OneOf<string>(nullParsers));
//     }
// 
//     /// <summary>
//     /// Tests that constructing a OneOf with a single parser does not attempt to build any lookup table.
//     /// Validates that the OriginalParsers and Parsers properties match the input.
//     /// </summary>
//     [Fact]
//     public void OneOfConstructor_SingleParser_PopulatesOriginalAndParsersCorrectly()
//     {
//         // Arrange
//         var dummyParser = new DummyParser<string>();
//         var parsers = new Parser<string>[]
//         {
//             dummyParser
//         };
//         // Act
//         var oneOf = new OneOf<string>(parsers);
//         // Assert
//         Assert.Equal(parsers, oneOf.OriginalParsers);
//         Assert.Equal(parsers, oneOf.Parsers);
//         // Since only one parser is provided, the lookup table is not built so ExpectedChars remains empty.
//         Assert.Empty(oneOf.ExpectedChars);
//     }
// 
//     /// <summary>
//     /// Tests that constructing a OneOf with multiple non-seekable parsers initializes correctly.
//     /// Validates that the OriginalParsers and Parsers properties contain all provided parsers.
//     /// </summary>
//     [Fact]
//     public void OneOfConstructor_MultipleNonSeekableParsers_PopulatesOriginalAndParsersCorrectly()
//     {
//         // Arrange
//         var dummyParser1 = new DummyParser<string>();
//         var dummyParser2 = new DummyParser<string>();
//         var parsers = new Parser<string>[]
//         {
//             dummyParser1,
//             dummyParser2
//         };
//         // Act
//         var oneOf = new OneOf<string>(parsers);
//         // Assert
//         Assert.Equal(parsers, oneOf.OriginalParsers);
//         Assert.Equal(parsers, oneOf.Parsers);
//         // As non-seekable parsers, the lookup table is not built hence ExpectedChars remains empty.
//         Assert.Empty(oneOf.ExpectedChars);
//     }
// 
//     /// <summary>
//     /// Tests that constructing a OneOf with a mix of seekable and non-seekable parsers initializes correctly.
//     /// Uses Moq to simulate a seekable parser and a non-seekable parser.
//     /// Validates that the OriginalParsers property contains all provided parsers.
//     /// </summary>
//     [Fact]
//     public void OneOfConstructor_MixedParsers_PopulatesOriginalParsersCorrectly()
//     {
//         // Arrange
//         // Create a mock for a seekable parser.
//         var mockSeekable = new Mock<Parser<string>>();
//         mockSeekable.As<ISeekable>().SetupGet(x => x.CanSeek).Returns(true);
//         mockSeekable.As<ISeekable>().SetupGet(x => x.ExpectedChars).Returns(new char[] { 'a' });
//         mockSeekable.As<ISeekable>().SetupGet(x => x.SkipWhitespace).Returns(false);
//         mockSeekable.Setup(x => x.ToString()).Returns("MockSeekableParser");
//         // Create a non-seekable parser.
//         var dummyParser = new DummyParser<string>();
//         var parsers = new Parser<string>[]
//         {
//             mockSeekable.Object,
//             dummyParser
//         };
//         // Act
//         var oneOf = new OneOf<string>(parsers);
//         // Assert
//         Assert.Equal(parsers, oneOf.OriginalParsers);
//         Assert.Equal(parsers, oneOf.Parsers);
//         // When a mix is provided, lookup table creation might be skipped due to inconsistencies.
//         // Therefore, ExpectedChars may be empty or contain a special character.
//         // We assert that ExpectedChars is not null.
//         Assert.NotNull(oneOf.ExpectedChars);
//     }
// 
//     /// <summary>
//     /// Tests that the ToString method returns a non-empty string that reflects the Parsers and ExpectedChars.
//     /// This indirectly validates part of the constructor's setup.
//     /// </summary>
//     [Fact]
//     public void OneOfConstructor_ToString_ReturnsExpectedFormat()
//     {
//         // Arrange
//         var dummyParser = new DummyParser<string>();
//         var parsers = new Parser<string>[]
//         {
//             dummyParser
//         };
//         var oneOf = new OneOf<string>(parsers);
//         // Act
//         var result = oneOf.ToString();
//         // Assert
//         Assert.NotNull(result);
//         Assert.Contains("DummyParser", result);
//     }
// 
//     /// <summary>
//     /// A fake parser that always indicates it can seek.
//     /// </summary>
// //     private class FakeParserTrue : Parser<string>, ISeekable [Error] (497-19)CS0534 'OneOfTests.FakeParserTrue' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)' [Error] (497-52)CS0535 'OneOfTests.FakeParserTrue' does not implement interface member 'ISeekable.ExpectedChars' [Error] (497-52)CS0535 'OneOfTests.FakeParserTrue' does not implement interface member 'ISeekable.SkipWhitespace'
// //     {
// //         public override bool Parse(ParseContext context, ref ParseResult<string> result) [Error] (499-30)CS0115 'OneOfTests.FakeParserTrue.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<string>)': no suitable method found to override
// //         {
// //             // Return false as parsing is not needed for the CanSeek tests.
// //             return false;
// //         }
// 
//         // ISeekable implementation returning true.
//         public bool CanSeek => true;
//     }
// 
//     /// <summary>
//     /// A fake parser that always indicates it cannot seek.
//     /// </summary>
// //     private class FakeParserFalse : Parser<string>, ISeekable [Error] (512-19)CS0534 'OneOfTests.FakeParserFalse' does not implement inherited abstract member 'Parser<string>.Parse(ParseContext, ref ParseResult<string>)' [Error] (512-53)CS0535 'OneOfTests.FakeParserFalse' does not implement interface member 'ISeekable.ExpectedChars' [Error] (512-53)CS0535 'OneOfTests.FakeParserFalse' does not implement interface member 'ISeekable.SkipWhitespace'
// //     {
// //         public override bool Parse(ParseContext context, ref ParseResult<string> result) [Error] (514-30)CS0115 'OneOfTests.FakeParserFalse.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<string>)': no suitable method found to override
// //         {
// //             // Return false as parsing is not needed for the CanSeek tests.
// //             return false;
// //         }
// 
//         // ISeekable implementation returning false.
//         public bool CanSeek => false;
//     }
// 
//     /// <summary>
//     /// Tests that the CanSeek property returns false when the OneOf instance is constructed with an empty parser array.
//     /// </summary>
//     [Fact]
//     public void CanSeek_WithEmptyParsers_ReturnsFalse()
//     {
//         // Arrange: Create an instance of OneOf<string> with an empty parser array.
//         var oneOf = new OneOf<string>(new Parser<string>[0]);
//         // Act: Retrieve the CanSeek property value.
//         bool canSeekFirstCall = oneOf.CanSeek;
//         bool canSeekSecondCall = oneOf.CanSeek;
//         // Assert: The property should consistently return false.
//         Assert.False(canSeekFirstCall);
//         Assert.False(canSeekSecondCall);
//         Assert.Equal(canSeekFirstCall, canSeekSecondCall);
//     }
// 
//     /// <summary>
//     /// Tests that the CanSeek property returns true when all inner parsers indicate they can seek.
//     /// </summary>
//     [Fact]
//     public void CanSeek_WithAllSeekableParsers_ReturnsTrue()
//     {
//         // Arrange: Create an array of parsers that all report as seekable.
//         var parsers = new Parser<string>[]
//         {
//             new FakeParserTrue(),
//             new FakeParserTrue()
//         };
//         var oneOf = new OneOf<string>(parsers);
//         // Act: Retrieve the CanSeek property value.
//         bool canSeek = oneOf.CanSeek;
//         // Assert: Expect the OneOf instance to be seekable when all inner parsers are seekable.
//         Assert.True(canSeek);
//     }
// 
//     /// <summary>
//     /// Tests that the CanSeek property returns false when at least one inner parser indicates it cannot seek.
//     /// </summary>
//     [Fact]
//     public void CanSeek_WithNonSeekableParser_ReturnsFalse()
//     {
//         // Arrange: Create an array containing a mix of seekable and non-seekable parsers.
//         var parsers = new Parser<string>[]
//         {
//             new FakeParserTrue(),
//             new FakeParserFalse()
//         };
//         var oneOf = new OneOf<string>(parsers);
//         // Act: Retrieve the CanSeek property value.
//         bool canSeek = oneOf.CanSeek;
//         // Assert: Expect the OneOf instance to be non-seekable if any inner parser is not seekable.
//         Assert.False(canSeek);
//     }
// 
//     /// <summary>
//     /// Tests that multiple accesses to the CanSeek property return the same result.
//     /// </summary>
//     [Fact]
//     public void CanSeek_MultipleCalls_ReturnSameValue()
//     {
//         // Arrange: Use a mix of parsers (all seekable for this test).
//         var parsers = new Parser<string>[]
//         {
//             new FakeParserTrue(),
//             new FakeParserTrue()
//         };
//         var oneOf = new OneOf<string>(parsers);
//         // Act: Retrieve the CanSeek property value multiple times.
//         bool firstCall = oneOf.CanSeek;
//         bool secondCall = oneOf.CanSeek;
//         bool thirdCall = oneOf.CanSeek;
//         // Assert: All calls must return the same value.
//         Assert.Equal(firstCall, secondCall);
//         Assert.Equal(secondCall, thirdCall);
//     }
// 
//     /// <summary>
//     /// A dummy implementation of the Parse method that always returns false.
//     /// </summary>
// //     public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (604-26)CS0051 Inconsistent accessibility: parameter type 'OneOfTests.ParseResult<int>' is less accessible than method 'OneOfTests.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<int>)' [Error] (604-26)CS0115 'OneOfTests.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<int>)': no suitable method found to override
// //     {
// //         return false;
// //     }
// 
//     /// <summary>
//     /// Returns a string representing the fake parser.
//     /// </summary>
//     public override string ToString()
//     {
//         return "FakeParser";
//     }
// 
//     /// <summary>
//     /// A dummy Parse method that always returns false.
//     /// </summary>
//     /// <param name = "context">The parse context.</param>
//     /// <param name = "result">The parse result.</param>
//     /// <returns>False always.</returns>
// //     public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (623-70)CS0246 The type or namespace name 'T' could not be found (are you missing a using directive or an assembly reference?) [Error] (623-26)CS0051 Inconsistent accessibility: parameter type 'OneOfTests.ParseResult<T>' is less accessible than method 'OneOfTests.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<T>)'
// //     {
// //         return false;
// //     }
// 
//     /// <summary>
//     /// Minimal implementation of Parse method that always returns false.
//     /// </summary>
// //     public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (631-26)CS0051 Inconsistent accessibility: parameter type 'OneOfTests.ParseResult<int>' is less accessible than method 'OneOfTests.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<int>)' [Error] (631-26)CS0115 'OneOfTests.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<int>)': no suitable method found to override [Error] (631-26)CS0111 Type 'OneOfTests' already defines a member called 'Parse' with the same parameter types
// //     {
// //         return false;
// //     }
// 
//     /// <summary>
//     /// Overridden ToString method.
//     /// </summary>
// //     public override string ToString() [Error] (639-28)CS0111 Type 'OneOfTests' already defines a member called 'ToString' with the same parameter types
// //     {
// //         return "DummyIntParser";
// //     }
// 
//     /// <summary>
//     /// A dummy implementation of <see cref = "Parser{T}"/> for testing purposes.
//     /// </summary>
// //     private class DummyIntParser : Parser<int> [Error] (647-19)CS0534 'OneOfTests.DummyIntParser' does not implement inherited abstract member 'Parser<int>.Parse(ParseContext, ref ParseResult<int>)'
// //     {
// //         /// <summary>
// //         /// Dummy parse method implementation that always returns false.
// //         /// </summary>
// //         /// <param name = "context">The parse context.</param>
// //         /// <param name = "result">The parse result.</param>
// //         /// <returns>Always returns false.</returns>
// //         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (655-30)CS0115 'OneOfTests.DummyIntParser.Parse(OneOfTests.ParseContext, ref OneOfTests.ParseResult<int>)': no suitable method found to override
// //         {
// //             return false;
// //         }
// 
//         /// <summary>
//         /// Returns a fixed string representation.
//         /// </summary>
//         /// <returns>A string representing the dummy parser.</returns>
//         public override string ToString() => "DummyIntParser";
//     }
// 
//     /// <summary>
//     /// Tests that the Parsers property returns the same sequence of parsers that are provided via the constructor.
//     /// Expected outcome: The Parsers property should contain the same elements in the same order.
//     /// </summary>
//     [Fact]
//     public void Parsers_WithValidParsers_ReturnsSameSequence()
//     {
//         // Arrange
//         var parser1 = new DummyIntParser();
//         var parser2 = new DummyIntParser();
//         Parser<int>[] inputParsers = new Parser<int>[]
//         {
//             parser1,
//             parser2
//         };
//         var oneOf = new OneOf<int>(inputParsers);
//         // Act
//         IReadOnlyList<Parser<int>> actualParsers = oneOf.Parsers;
//         // Assert
//         Assert.NotNull(actualParsers);
//         Assert.Equal(inputParsers.Length, actualParsers.Count);
//         for (int i = 0; i < inputParsers.Length; i++)
//         {
//             Assert.Same(inputParsers[i], actualParsers[i]);
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the Parsers property returns an empty list when the OneOf instance is initialized with an empty array.
//     /// Expected outcome: Parsers property should not be null and its count should be zero.
//     /// </summary>
//     [Fact]
//     public void Parsers_WithEmptyArray_ReturnsEmptyList()
//     {
//         // Arrange
//         Parser<int>[] emptyParsers = new Parser<int>[0];
//         var oneOf = new OneOf<int>(emptyParsers);
//         // Act
//         IReadOnlyList<Parser<int>> actualParsers = oneOf.Parsers;
//         // Assert
//         Assert.NotNull(actualParsers);
//         Assert.Empty(actualParsers);
//     }
// 
//     /// <summary>
//     /// Tests that constructing a OneOf instance with a null array of parsers throws an ArgumentNullException.
//     /// Expected outcome: An ArgumentNullException should be thrown.
//     /// </summary>
//     [Fact]
//     public void Constructor_NullParsers_ThrowsArgumentNullException()
//     {
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new OneOf<int>(null));
//     }
// #endregion
// }
