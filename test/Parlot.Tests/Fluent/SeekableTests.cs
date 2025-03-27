// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-8)CS8802 Only one compilation unit can have top-level statements. [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Seekable{T}"/> class.
/// </summary>
// public class SeekableTests [Error] (452-2)CS1513 } expected [Error] (186-12)CS1520 Method must have a return type
// {
//     /// <summary>
//     /// A fake parser to simulate the behavior of an inner parser.
//     /// </summary>
//     private class FakeParser : Parser<int> [Error] (26-46)CS1073 Unexpected token 'ref' [Error] (26-50)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (19-19)CS0534 'SeekableTests.FakeParser' does not implement inherited abstract member 'Parser<int>.Parse(ParseContext, ref ParseResult<int>)'
//     {
//         private readonly Func<ParseContext, ref ParseResult<int>, bool> _parseFunc; [Error] (21-45)CS1073 Unexpected token 'ref' [Error] (21-49)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         /// <summary>
//         /// Initializes a new instance of the <see cref = "FakeParser"/> class with a delegate for parsing.
//         /// </summary>
//         /// <param name = "parseFunc">A delegate that determines the behavior of the Parse method.</param>
//         public FakeParser(Func<ParseContext, ref ParseResult<int>, bool> parseFunc)
//         {
//             _parseFunc = parseFunc;
//         }
// 
//         /// <summary>
//         /// Parses the input using the provided delegate.
//         /// </summary>
//         /// <param name = "context">The parse context.</param>
//         /// <param name = "result">The parse result.</param>
//         /// <returns>A boolean indicating whether parsing was successful.</returns>
//         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (37-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             return _parseFunc(context, ref result);
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the Parse method correctly calls EnterParser and ExitParser and returns true when the inner parser succeeds.
//     /// </summary>
//     [Fact] [Error] (57-60)CS0748 Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit [Error] (57-60)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (59-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (61-35)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenInnerParserReturnsTrue_CallsEnterAndExitAndReturnsTrue()
//     {
//         // Arrange
//         // Create a mock for ParseContext and setup methods to do nothing.
//         var mockContext = new Mock<ParseContext>();
//         // We use callbacks to record the order of calls.
//         var callOrder = new System.Collections.Generic.List<string>();
//         mockContext.Setup(x => x.EnterParser(It.IsAny<Parser<int>>())).Callback<Parser<int>>(p => callOrder.Add("EnterParser"));
//         mockContext.Setup(x => x.ExitParser(It.IsAny<Parser<int>>())).Callback<Parser<int>>(p => callOrder.Add("ExitParser"));
//         // Create a fake inner parser that returns true.
//         var fakeInnerParser = new FakeParser((context, ref ParseResult<int> res) => true);
//         // Create an instance of Seekable with the fake inner parser.
//         var seekable = new Seekable<int>(fakeInnerParser, skipWhiteSpace: false, expectedChars: "abc".AsSpan());
//         // Prepare a default ParseResult.
//         var parseResult = default(ParseResult<int>);
//         // Act
//         bool result = seekable.Parse(mockContext.Object, ref parseResult);
//         // Assert
//         Assert.True(result);
//         // Verify that EnterParser and ExitParser were called exactly once in the correct order.
//         mockContext.Verify(x => x.EnterParser(seekable), Times.Once);
//         mockContext.Verify(x => x.ExitParser(seekable), Times.Once);
//         Assert.Equal(2, callOrder.Count);
//         Assert.Equal("EnterParser", callOrder[0]);
//         Assert.Equal("ExitParser", callOrder[1]);
//     }
// 
//     /// <summary>
//     /// Tests that the Parse method correctly calls EnterParser and ExitParser and returns false when the inner parser fails.
//     /// </summary>
//     [Fact] [Error] (86-60)CS0748 Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit [Error] (86-60)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (87-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (88-35)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     public void Parse_WhenInnerParserReturnsFalse_CallsEnterAndExitAndReturnsFalse()
//     {
//         // Arrange
//         var mockContext = new Mock<ParseContext>();
//         var callOrder = new System.Collections.Generic.List<string>();
//         mockContext.Setup(x => x.EnterParser(It.IsAny<Parser<int>>())).Callback<Parser<int>>(p => callOrder.Add("EnterParser"));
//         mockContext.Setup(x => x.ExitParser(It.IsAny<Parser<int>>())).Callback<Parser<int>>(p => callOrder.Add("ExitParser"));
//         // Create a fake inner parser that returns false.
//         var fakeInnerParser = new FakeParser((context, ref ParseResult<int> res) => false);
//         var seekable = new Seekable<int>(fakeInnerParser, skipWhiteSpace: false, expectedChars: "xyz".AsSpan());
//         var parseResult = default(ParseResult<int>);
//         // Act
//         bool result = seekable.Parse(mockContext.Object, ref parseResult);
//         // Assert
//         Assert.False(result);
//         // Verify that EnterParser and ExitParser were called exactly once.
//         mockContext.Verify(x => x.EnterParser(seekable), Times.Once);
//         mockContext.Verify(x => x.ExitParser(seekable), Times.Once);
//         Assert.Equal(2, callOrder.Count);
//         Assert.Equal("EnterParser", callOrder[0]);
//         Assert.Equal("ExitParser", callOrder[1]);
//     }
// 
//     /// <summary>
//     /// Tests that the Parse method propagates exceptions thrown by the inner parser and does not call ExitParser.
//     /// </summary>
//     [Fact] [Error] (113-60)CS0748 Inconsistent lambda parameter usage; parameter types must be all explicit or all implicit [Error] (113-60)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (114-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (115-35)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (117-25)CS0619 'Assert.Throws<T>(Func<Task>)' is obsolete: 'You must call Assert.ThrowsAsync<T> (and await the result) when testing async code.'
//     public void Parse_WhenInnerParserThrows_ExceptionPropagatesAndExitNotCalled()
//     {
//         // Arrange
//         var mockContext = new Mock<ParseContext>();
//         var callOrder = new System.Collections.Generic.List<string>();
//         mockContext.Setup(x => x.EnterParser(It.IsAny<Parser<int>>())).Callback<Parser<int>>(p => callOrder.Add("EnterParser"));
//         mockContext.Setup(x => x.ExitParser(It.IsAny<Parser<int>>())).Callback<Parser<int>>(p => callOrder.Add("ExitParser"));
//         // Create a fake inner parser that throws an exception during parsing.
//         var fakeInnerParser = new FakeParser((context, ref ParseResult<int> res) => throw new InvalidOperationException("Inner parser failure."));
//         var seekable = new Seekable<int>(fakeInnerParser, skipWhiteSpace: true, expectedChars: "def".AsSpan());
//         var parseResult = default(ParseResult<int>);
//         // Act & Assert
//         var exception = Assert.Throws<InvalidOperationException>(() => seekable.Parse(mockContext.Object, ref parseResult));
//         Assert.Equal("Inner parser failure.", exception.Message);
//         // Verify that EnterParser was called once.
//         mockContext.Verify(x => x.EnterParser(seekable), Times.Once);
//         // Verify that ExitParser was never called due to exception.
//         mockContext.Verify(x => x.ExitParser(seekable), Times.Never);
//         Assert.Single(callOrder);
//         Assert.Equal("EnterParser", callOrder[0]);
//     }
// 
//     /// <summary>
//     /// Tests the Compile method with a valid CompilationContext to ensure it returns a CompilationResult
//     /// whose Body contains a BlockExpression built from the parser's compile result.
//     /// The BlockExpression should contain the expected variables and body expression.
//     /// </summary>
//     [Fact] [Error] (144-30)CS7036 There is no argument given that corresponds to the required parameter 'parseFunc' of 'SeekableTests.FakeParser.FakeParser(Func<ParseContext, ParseResult<int>, bool>)' [Error] (146-13)CS0117 'SeekableTests.FakeParser' does not contain a definition for 'ExpectedVariables' [Error] (147-13)CS0117 'SeekableTests.FakeParser' does not contain a definition for 'ExpectedBody' [Error] (150-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (152-31)CS0246 The type or namespace name 'FakeCompilationContext' could not be found (are you missing a using directive or an assembly reference?) [Error] (157-42)CS1061 'Parser<T>' does not contain a definition for 'Body' and no accessible extension method 'Body' accepting a first argument of type 'Parser<T>' could be found (are you missing a using directive or an assembly reference?) [Error] (158-41)CS1061 'Parser<T>' does not contain a definition for 'Body' and no accessible extension method 'Body' accepting a first argument of type 'Parser<T>' could be found (are you missing a using directive or an assembly reference?) [Error] (160-49)CS1061 'Parser<T>' does not contain a definition for 'Body' and no accessible extension method 'Body' accepting a first argument of type 'Parser<T>' could be found (are you missing a using directive or an assembly reference?)
//     public void Compile_WithValidContext_ReturnsCompilationResultWithBlockContainingParserResult()
//     {
//         // Arrange
//         // Define expected variables and a body expression.
//         var expectedVariables = new List<ParameterExpression>
//         {
//             Expression.Parameter(typeof(int), "var1"),
//             Expression.Parameter(typeof(int), "var2")
//         };
//         var expectedBody = Expression.Constant(42);
//         // Create a fake parser that returns a controlled compile result.
//         var fakeParser = new FakeParser
//         {
//             ExpectedVariables = expectedVariables,
//             ExpectedBody = expectedBody
//         };
//         // Instantiate the Seekable parser using the fake parser.
//         var seekable = new Seekable<int>(fakeParser, skipWhiteSpace: false);
//         // Create a fake compilation context.
//         var fakeContext = new FakeCompilationContext();
//         // Act
//         var compilationResult = seekable.Compile(fakeContext);
//         // Assert
//         Assert.NotNull(compilationResult);
//         Assert.NotNull(compilationResult.Body);
//         Assert.Single(compilationResult.Body);
//         // Retrieve the block expression added in the Compile method.
//         var blockExpression = compilationResult.Body[0] as BlockExpression;
//         Assert.NotNull(blockExpression);
//         Assert.Equal(expectedVariables, blockExpression.Variables);
//         Assert.Single(blockExpression.Expressions);
//         Assert.Equal(expectedBody, blockExpression.Expressions[0]);
//     }
// 
//     /// <summary>
//     /// Tests the Compile method by passing a null CompilationContext.
//     /// This ensures that a NullReferenceException is thrown when the context is missing.
//     /// </summary>
//     [Fact] [Error] (175-30)CS7036 There is no argument given that corresponds to the required parameter 'parseFunc' of 'SeekableTests.FakeParser.FakeParser(Func<ParseContext, ParseResult<int>, bool>)' [Error] (177-13)CS0117 'SeekableTests.FakeParser' does not contain a definition for 'ExpectedVariables' [Error] (178-13)CS0117 'SeekableTests.FakeParser' does not contain a definition for 'ExpectedBody' [Error] (180-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (182-62)CS1501 No overload for method 'Compile' takes 1 arguments
//     public void Compile_WithNullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var fakeParser = new FakeParser
//         {
//             ExpectedVariables = new List<ParameterExpression>(),
//             ExpectedBody = Expression.Empty()
//         };
//         var seekable = new Seekable<int>(fakeParser, skipWhiteSpace: false);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => seekable.Compile(null));
//     }
// 
//     private readonly string _toStringValue;
//     public DummyParser(string toStringValue)
//     {
//         _toStringValue = toStringValue;
//     }
// 
//     public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (191-58)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//     {
//         throw new NotImplementedException();
//     }
// 
//     public override string ToString() => _toStringValue;
//     /// <summary>
//     /// A dummy parser implementation of <see cref = "Parser{T}"/> for testing purposes.
//     /// </summary>
//     private class DummyParser : Parser<int> [Error] (200-19)CS0534 'SeekableTests.DummyParser' does not implement inherited abstract member 'Parser<int>.Parse(ParseContext, ref ParseResult<int>)'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (202-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString() => "DummyParser";
//     }
// 
//     /// <summary>
//     /// Tests that the constructor throws an <see cref = "ArgumentNullException"/> when a null parser is provided.
//     /// </summary>
//     [Fact] [Error] (219-72)CS0122 'Seekable<T>' is inaccessible due to its protection level
//     public void Constructor_NullParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Parser<int> nullParser = null;
//         // Act & Assert
//         var exception = Assert.Throws<ArgumentNullException>(() => new Seekable<int>(nullParser, true, "abc".AsSpan()));
//         Assert.Equal("parser", exception.ParamName);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor correctly sets properties when no expected characters are provided.
//     /// </summary>
//     [Fact] [Error] (233-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (235-44)CS0122 'Seekable<T>.Parser' is inaccessible due to its protection level [Error] (236-47)CS0122 'Seekable<T>.SkipWhitespace' is inaccessible due to its protection level [Error] (237-30)CS0122 'Seekable<T>.CanSeek' is inaccessible due to its protection level [Error] (238-31)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level
//     public void Constructor_NoExpectedChars_SetsPropertiesCorrectly()
//     {
//         // Arrange
//         var dummyParser = new DummyParser();
//         bool skipWhitespace = false;
//         // Act
//         var seekable = new Seekable<int>(dummyParser, skipWhitespace);
//         // Assert
//         Assert.Equal(dummyParser, seekable.Parser);
//         Assert.Equal(skipWhitespace, seekable.SkipWhitespace);
//         Assert.True(seekable.CanSeek);
//         Assert.Empty(seekable.ExpectedChars);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor extracts distinct expected characters from a single span.
//     /// </summary>
//     [Fact] [Error] (253-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (255-44)CS0122 'Seekable<T>.Parser' is inaccessible due to its protection level [Error] (256-47)CS0122 'Seekable<T>.SkipWhitespace' is inaccessible due to its protection level [Error] (257-30)CS0122 'Seekable<T>.CanSeek' is inaccessible due to its protection level [Error] (263-65)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level
//     public void Constructor_SingleSpanDistinctCalculation_SetsExpectedCharsCorrectly()
//     {
//         // Arrange
//         var dummyParser = new DummyParser();
//         bool skipWhitespace = true;
//         // "abba" contains duplicate 'b' and 'a', distinct expected characters are ['a', 'b']
//         var inputSpan = "abba".AsSpan();
//         // Act
//         var seekable = new Seekable<int>(dummyParser, skipWhitespace, inputSpan);
//         // Assert
//         Assert.Equal(dummyParser, seekable.Parser);
//         Assert.Equal(skipWhitespace, seekable.SkipWhitespace);
//         Assert.True(seekable.CanSeek);
//         var expectedDistinct = new[]
//         {
//             'a',
//             'b'
//         };
//         Assert.Equal(expectedDistinct.OrderBy(c => c), seekable.ExpectedChars.OrderBy(c => c));
//     }
// 
//     /// <summary>
//     /// Tests that the constructor extracts distinct expected characters when multiple spans are provided.
//     /// </summary>
//     [Fact] [Error] (278-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (280-44)CS0122 'Seekable<T>.Parser' is inaccessible due to its protection level [Error] (281-47)CS0122 'Seekable<T>.SkipWhitespace' is inaccessible due to its protection level [Error] (282-30)CS0122 'Seekable<T>.CanSeek' is inaccessible due to its protection level [Error] (291-65)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level
//     public void Constructor_MultipleSpansDistinctCalculation_SetsExpectedCharsCorrectly()
//     {
//         // Arrange
//         var dummyParser = new DummyParser();
//         bool skipWhitespace = false;
//         var span1 = "abc".AsSpan();
//         var span2 = "cda".AsSpan(); // Overlap: 'a' and 'c'
//         // Act
//         var seekable = new Seekable<int>(dummyParser, skipWhitespace, span1, span2);
//         // Assert
//         Assert.Equal(dummyParser, seekable.Parser);
//         Assert.Equal(skipWhitespace, seekable.SkipWhitespace);
//         Assert.True(seekable.CanSeek);
//         // Expected distinct characters: 'a', 'b', 'c', 'd'
//         var expectedDistinct = new[]
//         {
//             'a',
//             'b',
//             'c',
//             'd'
//         };
//         Assert.Equal(expectedDistinct.OrderBy(c => c), seekable.ExpectedChars.OrderBy(c => c));
//     }
// 
//     /// <summary>
//     /// A fake parser implementation for testing purposes.
//     /// </summary>
//     private class FakeParser : Parser<int> [Error] (297-19)CS0102 The type 'SeekableTests' already contains a definition for 'FakeParser'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (299-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (299-30)CS0111 Type 'SeekableTests.FakeParser' already defines a member called 'Parse' with the same parameter types
//         {
//             result = default;
//             return false;
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the default value of the CanSeek property is false upon initialization.
//     /// </summary>
//     [Fact] [Error] (313-30)CS7036 There is no argument given that corresponds to the required parameter 'parseFunc' of 'SeekableTests.FakeParser.FakeParser(Func<ParseContext, ParseResult<int>, bool>)' [Error] (316-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (318-38)CS0122 'Seekable<T>.CanSeek' is inaccessible due to its protection level
//     public void CanSeek_GetProperty_DefaultValueIsFalse()
//     {
//         // Arrange
//         var fakeParser = new FakeParser();
//         // Creating a dummy expected characters span.
//         ReadOnlySpan<char> expected = "abc";
//         var seekable = new Seekable<int>(fakeParser, false, expected);
//         // Act
//         bool initialValue = seekable.CanSeek;
//         // Assert
//         Assert.False(initialValue);
//     }
// 
//     /// <summary>
//     /// Tests that setting the CanSeek property updates the property correctly.
//     /// </summary>
//     [Fact] [Error] (330-30)CS7036 There is no argument given that corresponds to the required parameter 'parseFunc' of 'SeekableTests.FakeParser.FakeParser(Func<ParseContext, ParseResult<int>, bool>)' [Error] (332-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (334-18)CS0122 'Seekable<T>.CanSeek' is inaccessible due to its protection level [Error] (335-30)CS0122 'Seekable<T>.CanSeek' is inaccessible due to its protection level [Error] (336-18)CS0122 'Seekable<T>.CanSeek' is inaccessible due to its protection level [Error] (337-31)CS0122 'Seekable<T>.CanSeek' is inaccessible due to its protection level
//     public void CanSeek_SetProperty_ChangesValue()
//     {
//         // Arrange
//         var fakeParser = new FakeParser();
//         ReadOnlySpan<char> expected = "xyz";
//         var seekable = new Seekable<int>(fakeParser, true, expected);
//         // Act & Assert
//         seekable.CanSeek = true;
//         Assert.True(seekable.CanSeek);
//         seekable.CanSeek = false;
//         Assert.False(seekable.CanSeek);
//     }
// 
//     /// <summary>
//     /// Dummy implementation of Parser<int> to facilitate testing of Seekable.
//     /// </summary>
//     private class DummyParser : Parser<int> [Error] (343-19)CS0102 The type 'SeekableTests' already contains a definition for 'DummyParser'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (345-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (345-30)CS0111 Type 'SeekableTests.DummyParser' already defines a member called 'Parse' with the same parameter types
//         {
//             // Dummy implementation always returns false.
//             return false;
//         }
// 
//         public override string ToString() => "DummyParser"; [Error] (351-32)CS0111 Type 'SeekableTests.DummyParser' already defines a member called 'ToString' with the same parameter types
//     }
// 
//     /// <summary>
//     /// Tests that the ExpectedChars property correctly returns a non-empty char array after being set.
//     /// </summary>
//     [Fact] [Error] (369-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (371-18)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level [Error] (372-31)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level
//     public void ExpectedChars_Property_WhenSetWithNonEmptyArray_ReturnsSameArray()
//     {
//         // Arrange
//         char[] expected = new char[]
//         {
//             'a',
//             'b',
//             'c'
//         };
//         var dummyParser = new DummyParser();
//         // Use an empty expectedChars span for constructor; property will be set later.
//         var seekable = new Seekable<int>(dummyParser, false, ReadOnlySpan<char>.Empty);
//         // Act
//         seekable.ExpectedChars = expected;
//         var actual = seekable.ExpectedChars;
//         // Assert
//         Assert.NotNull(actual);
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that the ExpectedChars property correctly returns an empty char array after being set.
//     /// </summary>
//     [Fact] [Error] (389-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (391-18)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level [Error] (392-31)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level
//     public void ExpectedChars_Property_WhenSetWithEmptyArray_ReturnsEmptyArray()
//     {
//         // Arrange
//         char[] expected = new char[]
//         {
//         };
//         var dummyParser = new DummyParser();
//         var seekable = new Seekable<int>(dummyParser, false, ReadOnlySpan<char>.Empty);
//         // Act
//         seekable.ExpectedChars = expected;
//         var actual = seekable.ExpectedChars;
//         // Assert
//         Assert.NotNull(actual);
//         Assert.Empty(actual);
//     }
// 
//     /// <summary>
//     /// Tests that the ExpectedChars property returns null after being set to null.
//     /// </summary>
//     [Fact] [Error] (406-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (408-18)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level [Error] (409-31)CS0122 'Seekable<T>.ExpectedChars' is inaccessible due to its protection level
//     public void ExpectedChars_Property_WhenSetToNull_ReturnsNull()
//     {
//         // Arrange
//         var dummyParser = new DummyParser();
//         var seekable = new Seekable<int>(dummyParser, false, ReadOnlySpan<char>.Empty);
//         // Act
//         seekable.ExpectedChars = null;
//         var actual = seekable.ExpectedChars;
//         // Assert
//         Assert.Null(actual);
//     }
// 
//     public override bool Parse(ParseContext context, ref ParseResult<int> result) [Error] (414-58)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?) [Error] (414-26)CS0111 Type 'SeekableTests' already defines a member called 'Parse' with the same parameter types
//     {
//         throw new NotImplementedException();
//     }
// 
//     public override string ToString() => "FakeIntParser"; [Error] (419-28)CS0111 Type 'SeekableTests' already defines a member called 'ToString' with the same parameter types
//     /// <summary>
//     /// Tests that the 'Parser' property returns the same parser instance provided during construction.
//     /// </summary>
//     [Fact] [Error] (430-28)CS0122 'Seekable<T>' is inaccessible due to its protection level [Error] (432-37)CS0122 'Seekable<T>.Parser' is inaccessible due to its protection level
//     public void Parser_PropertyGetter_ReturnsProvidedParserInstance()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<int>();
//         bool skipWhitespace = true;
//         ReadOnlySpan<char> expectedChars = "abc".AsSpan();
//         var seekable = new Seekable<int>(fakeParser, skipWhitespace, expectedChars);
//         // Act
//         var actualParser = seekable.Parser;
//         // Assert
//         Assert.Same(fakeParser, actualParser);
//     }
// 
//     /// <summary>
//     /// A minimal fake parser implementation for testing purposes.
//     /// </summary>
//     private class FakeParser<T> : Parser<T> [Error] (440-19)CS0534 'SeekableTests.FakeParser<T>' does not implement inherited abstract member 'Parser<T>.Parse(ParseContext, ref ParseResult<T>)'
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) [Error] (442-62)CS0246 The type or namespace name 'ParseResult<>' could not be found (are you missing a using directive or an assembly reference?)
//         {
//             throw new NotImplementedException();
//         }
// 
//         public override string ToString()
//         {
//             return "FakeParser";
//         }
//     }
// }