using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Rewriting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

/// <summary>
/// Minimal implementation of a parse context for testing purposes.
/// </summary>
// public class ParseContext [Error] (417-2)CS1038 #endregion directive expected
// {
//     public virtual void EnterParser(object parser)
//     {
//     }
// 
//     public virtual void ExitParser(object parser)
//     {
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method of ZeroOrMany returns a CompilationResult containing a Loop expression when using a valid CompilationContext.
//     /// The test arranges a fake parser that tracks if its Build method is invoked.
//     /// Expected outcome: The returned CompilationResult has a non-empty Body with at least one LoopExpression.
//     /// </summary>
//     [Fact] [Error] (35-46)CS1503 Argument 1: cannot convert from 'ParseContext.Parser<int>' to 'Parlot.Fluent.Parser<int>' [Error] (38-41)CS1503 Argument 1: cannot convert from 'ParseContext.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext'
//     public void Compile_WithValidContext_ReturnsCompilationResultContainingLoop()
//     {
//         // Arrange
//         var fakeParser = new FakeParserInt();
//         Parser<int> parser = fakeParser;
//         var zeroOrMany = new ZeroOrMany<int>(parser);
//         var context = new FakeCompilationContext();
//         // Act
//         var result = zeroOrMany.Compile(context);
//         // Assert
//         Assert.NotNull(result);
//         Assert.True(fakeParser.BuildInvoked, "Expected the parser's Build method to have been invoked.");
//         // Verify that the result.Body contains at least one LoopExpression.
//         bool containsLoop = result.Body.Any(exp => exp is LoopExpression);
//         Assert.True(containsLoop, "Expected the compilation result body to contain a Loop expression.");
//     }
// 
//     /// <summary>
//     /// Tests that the Compile method throws a NullReferenceException when provided with a null CompilationContext.
//     /// The test does not provide a valid context, so it expects an exception to be thrown.
//     /// </summary>
//     [Fact] [Error] (57-46)CS1503 Argument 1: cannot convert from 'ParseContext.Parser<int>' to 'Parlot.Fluent.Parser<int>'
//     public void Compile_WithNullContext_ThrowsNullReferenceException()
//     {
//         // Arrange
//         var fakeParser = new FakeParserInt();
//         Parser<int> parser = fakeParser;
//         var zeroOrMany = new ZeroOrMany<int>(parser);
//         // Act & Assert
//         Assert.Throws<NullReferenceException>(() => zeroOrMany.Compile(null));
//     }
// 
// #region Fake Implementations for Testing
//     /// <summary>
//     /// Minimal abstract definition of Parser&lt;T&gt; to support testing.
//     /// </summary>
//     public abstract class Parser<T>
//     {
//         public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
//         public virtual CompilationResult<T> Build(CompilationContext context)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// Minimal definition of ParseContext for testing purposes.
//     /// </summary>
//     public class ParseContext [Error] (78-18)CS0542 'ParseContext': member names cannot be the same as their enclosing type
//     {
//     }
// 
//     /// <summary>
//     /// Minimal definition of ParseResult&lt;T&gt; for testing purposes.
//     /// </summary>
//     public class ParseResult<T>
//     {
//     }
// 
//     /// <summary>
//     /// Minimal abstract definition for CompilationContext.
//     /// </summary>
//     public abstract class CompilationContext
//     {
//         public abstract int NextNumber { get; }
// 
//         public abstract CompilationResult<TResult> CreateCompilationResult<TResult>(bool success, Expression defaultValue);
//         public abstract Expression Eof();
//         public abstract Expression DiscardResult { get; }
//     }
// 
//     /// <summary>
//     /// Minimal abstract definition for CompilationResult&lt;TResult&gt;.
//     /// </summary>
//     public abstract class CompilationResult<TResult>
//     {
//         public List<Expression> Body { get; } = new List<Expression>();
//         public Expression Value { get; set; }
//         public abstract IEnumerable<ParameterExpression> Variables { get; }
// 
//         public abstract ParameterExpression DeclareVariable<TVar>(string name, Expression init = null);
//     }
// 
//     /// <summary>
//     /// Fake implementation of CompilationContext for testing purposes.
//     /// </summary>
//     public class FakeCompilationContext : CompilationContext
//     {
//         private int _counter = 1;
//         public override int NextNumber => _counter++;
// 
//         public override CompilationResult<TResult> CreateCompilationResult<TResult>(bool success, Expression defaultValue)
//         {
//             return new FakeCompilationResult<TResult>(success, defaultValue);
//         }
// 
//         public override Expression Eof()
//         {
//             return Expression.Constant(false);
//         }
// 
//         public override Expression DiscardResult => Expression.Empty();
//     }
// 
//     /// <summary>
//     /// Fake implementation of CompilationResult&lt;TResult&gt; for testing purposes.
//     /// </summary>
//     public class FakeCompilationResult<TResult> : CompilationResult<TResult>
//     {
//         private readonly List<ParameterExpression> _variables = new List<ParameterExpression>();
//         public bool Success { get; set; }
// 
//         public FakeCompilationResult(bool success, Expression defaultValue)
//         {
//             Success = success;
//             Value = defaultValue;
//         }
// 
//         public override IEnumerable<ParameterExpression> Variables => _variables;
// 
//         public override ParameterExpression DeclareVariable<TVar>(string name, Expression init = null)
//         {
//             var variable = Expression.Variable(typeof(TVar), name);
//             _variables.Add(variable);
//             return variable;
//         }
//     }
// 
//     /// <summary>
//     /// Fake implementation of Parser&lt;int&gt; that overrides the Build method.
//     /// It tracks whether Build is invoked and returns a fake CompilationResult.
//     /// </summary>
//     public class FakeParserInt : Parser<int>
//     {
//         public bool BuildInvoked { get; private set; }
// 
//         private readonly FakeCompilationResult<int> _fakeResult;
//         public FakeParserInt()
//         {
//             _fakeResult = new FakeCompilationResult<int>(true, Expression.Constant(42));
//         }
// 
//         public override CompilationResult<int> Build(CompilationContext context)
//         {
//             BuildInvoked = true;
//             // Simulate adding a dummy expression to the body.
//             _fakeResult.Body.Add(Expression.Constant(100));
//             return _fakeResult;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<int> result)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// A dummy parser implementation for testing purposes, where ToString returns a specified string.
//     /// </summary>
//     private class DummyParser : Parser<int>
//     {
//         private readonly string _representation;
//         public DummyParser(string representation)
//         {
//             _representation = representation;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<int> result)
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
//     /// Tests the ToString method when the underlying parser returns a non-null string.
//     /// Expected result is the parser's string representation appended with an asterisk.
//     /// </summary>
//     [Fact] [Error] (218-46)CS1503 Argument 1: cannot convert from 'ParseContext.DummyParser' to 'Parlot.Fluent.Parser<int>'
//     public void ToString_ParserReturnsNonNullString_ReturnsExpectedDescription()
//     {
//         // Arrange
//         string dummyRepresentation = "DummyParser";
//         var dummyParser = new DummyParser(dummyRepresentation);
//         var zeroOrMany = new ZeroOrMany<int>(dummyParser);
//         string expected = dummyRepresentation + "*";
//         // Act
//         string actual = zeroOrMany.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests the ToString method when the underlying parser's ToString method returns null.
//     /// Expected result is an asterisk, since null is treated as an empty string in string interpolation.
//     /// </summary>
//     [Fact] [Error] (236-46)CS1503 Argument 1: cannot convert from 'ParseContext.DummyParser' to 'Parlot.Fluent.Parser<int>'
//     public void ToString_ParserReturnsNull_ReturnsAsteriskOnly()
//     {
//         // Arrange
//         string dummyRepresentation = null;
//         var dummyParser = new DummyParser(dummyRepresentation);
//         var zeroOrMany = new ZeroOrMany<int>(dummyParser);
//         string expected = "*"; // null is treated as empty string
//         // Act
//         string actual = zeroOrMany.ToString();
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor throws an ArgumentNullException when a null parser is provided.
//     /// </summary>
//     [Fact] [Error] (253-99)CS1503 Argument 1: cannot convert from 'ParseContext.Parser<int>' to 'Parlot.Fluent.Parser<int>'
//     public void ZeroOrMany_NullParser_ThrowsArgumentNullException()
//     {
//         // Arrange
//         Parser<int> nullParser = null;
//         // Act & Assert
//         ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new ZeroOrMany<int>(nullParser));
//         Assert.Equal("parser", ex.ParamName);
//     }
// 
//     /// <summary>
//     /// Tests that the constructor correctly initializes an instance when provided with a non-seekable parser.
//     /// It also confirms that the ToString override returns the expected string.
//     /// </summary>
//     [Fact] [Error] (267-39)CS1503 Argument 1: cannot convert from 'ParseContext.FakeParser<int>' to 'Parlot.Fluent.Parser<int>'
//     public void ZeroOrMany_NonSeekableParser_DoesNotThrowAndReturnsExpectedToString()
//     {
//         // Arrange
//         var fakeParser = new FakeParser<int>();
//         // Act
//         var sut = new ZeroOrMany<int>(fakeParser);
//         // Assert
//         Assert.NotNull(sut);
//         // ToString is defined as $"{_parser}*". Our FakeParser.ToString returns "FakeParser".
//         Assert.Equal("FakeParser*", sut.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the constructor correctly assigns seekable properties from the provided ISeekable parser.
//     /// </summary>
//     [Fact] [Error] (290-39)CS1503 Argument 1: cannot convert from 'ParseContext.FakeSeekableParser<int>' to 'Parlot.Fluent.Parser<int>'
//     public void ZeroOrMany_SeekableParser_PropertiesAssignedCorrectly()
//     {
//         // Arrange
//         bool expectedCanSeek = true;
//         char[] expectedChars = new char[]
//         {
//             'a',
//             'b'
//         };
//         bool expectedSkipWhitespace = true;
//         var fakeSeekableParser = new FakeSeekableParser<int>(expectedCanSeek, expectedChars, expectedSkipWhitespace);
//         // Act
//         var sut = new ZeroOrMany<int>(fakeSeekableParser);
//         // Assert
//         Assert.NotNull(sut);
//         Assert.Equal(expectedCanSeek, sut.CanSeek);
//         Assert.Equal(expectedChars, sut.ExpectedChars);
//         Assert.Equal(expectedSkipWhitespace, sut.SkipWhitespace);
//         // ToString should reflect the underlying parser's ToString plus the "*" suffix.
//         Assert.Equal("FakeSeekableParser*", sut.ToString());
//     }
// 
//     /// <summary>
//     /// A fake parser used for testing non-seekable behavior.
//     /// Inherits from Parser and overrides ToString for predictable output.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
//     private class FakeParser<T> : Parser<T>
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) => throw new NotImplementedException();
//         public override string ToString() => "FakeParser";
//     }
// 
//     /// <summary>
//     /// A fake parser that implements ISeekable for testing seekable behavior.
//     /// Inherits from Parser and implements ISeekable properties.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter.</typeparam>
//     private class FakeSeekableParser<T> : Parser<T>, ISeekable
//     {
//         public bool CanSeek { get; }
//         public char[] ExpectedChars { get; }
//         public bool SkipWhitespace { get; }
// 
//         public FakeSeekableParser(bool canSeek, char[] expectedChars, bool skipWhitespace)
//         {
//             CanSeek = canSeek;
//             ExpectedChars = expectedChars;
//             SkipWhitespace = skipWhitespace;
//         }
// 
//         public override bool Parse(ParseContext context, ref ParseResult<T> result) => throw new NotImplementedException();
//         public override string ToString() => "FakeSeekableParser";
//     }
// 
//     /// <summary>
//     /// A dummy implementation of Parser<T> used for testing purposes.
//     /// This parser does not implement ISeekable.
//     /// The Parse method is not used in the tests.
//     /// </summary>
//     /// <typeparam name = "T">The type parameter for the parser.</typeparam>
//     private class DummyParser<T> : Parser<T>
//     {
//         public override bool Parse(ParseContext context, ref ParseResult<T> result)
//         {
//             throw new NotImplementedException();
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the CanSeek property returns false when the underlying parser does not implement ISeekable.
//     /// This confirms that, in absence of a seekable parser, ZeroOrMany's CanSeek defaults to false.
//     /// </summary>
//     [Fact] [Error] (357-46)CS1503 Argument 1: cannot convert from 'ParseContext.DummyParser<int>' to 'Parlot.Fluent.Parser<int>'
//     public void CanSeek_WhenUnderlyingParserIsNotSeekable_ReturnsFalse()
//     {
//         // Arrange
//         // Create a dummy parser that does not implement ISeekable.
//         var dummyParser = new DummyParser<int>();
//         var zeroOrMany = new ZeroOrMany<int>(dummyParser);
//         // Act
//         bool canSeek = zeroOrMany.CanSeek;
//         // Assert
//         Assert.False(canSeek, "Expected CanSeek to be false when the underlying parser is not seekable.");
//     }
// 
//     /// <summary>
//     /// Tests that the CanSeek property returns the value provided by an underlying seekable parser.
//     /// This uses Moq to simulate a parser that implements ISeekable.
//     /// </summary>
//     [Fact] [Error] (377-46)CS1503 Argument 1: cannot convert from 'ParseContext.Parser<int>' to 'Parlot.Fluent.Parser<int>'
//     public void CanSeek_WhenUnderlyingParserIsSeekable_ReturnsUnderlyingValue()
//     {
//         // Arrange
//         // Create a mock of Parser<int> and set it up to also implement ISeekable.
//         var mockParser = new Mock<Parser<int>>();
//         // Setup the ISeekable interface on the mock to return true.
//         mockParser.As<ISeekable>().SetupGet(p => p.CanSeek).Returns(true);
//         // Pass the mocked parser to ZeroOrMany.
//         var zeroOrMany = new ZeroOrMany<int>(mockParser.Object);
//         // Act
//         bool canSeek = zeroOrMany.CanSeek;
//         // Assert
//         // Assuming that ZeroOrMany assigns CanSeek based on whether the provided parser is seekable,
//         // then we expect the value to be true.
//         Assert.True(canSeek, "Expected CanSeek to be true when the underlying parser is seekable and returns true.");
//     }
// 
//     /// <summary>
//     /// Tests that the ExpectedChars property returns an empty character array.
//     /// </summary>
//     [Fact] [Error] (395-47)CS1503 Argument 1: cannot convert from 'ParseContext.Parser<char>' to 'Parlot.Fluent.Parser<char>'
//     public void ExpectedChars_WhenAccessed_ReturnsEmptyArray()
//     {
//         // Arrange: Create a mock for the required Parser<char> dependency.
//         var mockParser = new Mock<Parser<char>>();
//         // Act: Instantiate ZeroOrMany with the mocked parser and retrieve ExpectedChars.
//         var zeroOrMany = new ZeroOrMany<char>(mockParser.Object);
//         var actualExpectedChars = zeroOrMany.ExpectedChars;
//         // Assert: Validate that the ExpectedChars property is not null and is empty.
//         Assert.NotNull(actualExpectedChars);
//         Assert.Empty(actualExpectedChars);
//     }
// 
//     /// <summary>
//     /// Tests that the SkipWhitespace property returns its default value (false) when a ZeroOrMany instance is created.
//     /// This validates that the auto-property for SkipWhitespace is correctly initialized to false.
//     /// </summary>
//     [Fact] [Error] (411-50)CS1503 Argument 1: cannot convert from 'ParseContext.Parser<int>' to 'Parlot.Fluent.Parser<int>'
//     public void SkipWhitespace_Property_DefaultValue_ReturnsFalse()
//     {
//         // Arrange: Create a dummy parser using Moq.
//         var mockParser = new Mock<Parser<int>>();
//         var parserInstance = new ZeroOrMany<int>(mockParser.Object);
//         // Act: Retrieve the value of the SkipWhitespace property.
//         bool skipWhitespaceValue = parserInstance.SkipWhitespace;
//         // Assert: The default value of SkipWhitespace should be false.
//         Assert.False(skipWhitespaceValue);
//     }
// }