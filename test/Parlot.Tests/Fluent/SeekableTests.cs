using Moq;
using Parlot.Compilation;
using Parlot.Fluent;
using Parlot.Rewriting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Seekable{T}"/> class.
    /// </summary>
    [TestClass]
    public class SeekableTests
    {
        private readonly Random _random;

        public SeekableTests()
        {
            _random = new Random();
        }

        #region Constructor Tests

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException if a null parser is provided.
        /// </summary>
        [TestMethod]
        public void Constructor_NullParser_ThrowsArgumentNullException()
        {
            // Arrange
            Parser<int> nullParser = null;
            bool skipWhitespace = true;
            ReadOnlySpan<char> expectedChars = "abc".AsSpan();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var seekable = new Seekable<int>(nullParser, skipWhitespace, expectedChars);
            });
        }

        /// <summary>
        /// Tests that the constructor correctly initializes properties and deduplicates expected characters.
        /// </summary>
        [TestMethod]
        public void Constructor_ValidParameters_InitializesProperties()
        {
            // Arrange
            var mockParser = new Mock<Parser<int>>();
            bool skipWhitespace = _random.Next(0, 2) == 1;
            // Provide duplicate characters.
            ReadOnlySpan<char> expectedChars = "abbc".AsSpan();

            // Act
            var seekable = new Seekable<int>(mockParser.Object, skipWhitespace, expectedChars);

            // Assert
            Assert.IsNotNull(seekable.Parser);
            Assert.AreEqual(skipWhitespace, seekable.SkipWhitespace, "SkipWhitespace property is not set correctly.");
            Assert.IsTrue(seekable.CanSeek, "CanSeek should be true by default.");
            // Expected distinct characters: a, b, c
            CollectionAssert.AreEquivalent(new char[] { 'a', 'b', 'c' }, seekable.ExpectedChars, "ExpectedChars property did not deduplicate characters correctly.");
        }

        #endregion

        #region Parse Tests

        /// <summary>
        /// Tests that the Parse method calls the underlying parser's Parse method and tracks context entry and exit.
        /// </summary>
        [TestMethod]
        public void Parse_ValidContext_CallsUnderlyingParserAndTracksContext()
        {
            // Arrange
            bool expectedParseResult = true;
            var fakeResult = new ParseResult<int>();
            var fakeContext = new FakeParseContext();

            // Create a mock for the underlying parser.
            var mockParser = new Mock<Parser<int>>();
            mockParser.Setup(p => p.Parse(It.IsAny<ParseContext>(), ref It.Ref<ParseResult<int>>.IsAny))
                      .Returns((ParseContext ctx, ref ParseResult<int> res) =>
                      {
                          // Simulate setting a value in result.
                          res.Value = 42;
                          return expectedParseResult;
                      });

            var seekable = new Seekable<int>(mockParser.Object, false, "xyz".AsSpan());

            // Act
            bool actual = seekable.Parse(fakeContext, ref fakeResult);

            // Assert
            Assert.AreEqual(expectedParseResult, actual, "Parse method did not return expected result.");
            Assert.AreEqual(2, fakeContext.CallLog.Count, "ParseContext should have recorded exactly two calls.");
            Assert.AreEqual("Enter:" + seekable.ToString(), fakeContext.CallLog[0], "ParseContext did not record the EnterParser call correctly.");
            Assert.AreEqual("Exit:" + seekable.ToString(), fakeContext.CallLog[1], "ParseContext did not record the ExitParser call correctly.");
            Assert.AreEqual(42, fakeResult.Value, "ParseResult was not updated correctly by the underlying parser.");
        }

        #endregion

        #region Compile Tests

        /// <summary>
        /// Tests that the Compile method correctly builds a compilation result using the underlying parser's Build method.
        /// </summary>
        [TestMethod]
        public void Compile_ValidCompilationContext_ReturnsCompiledResultWithBlock()
        {
            // Arrange
            // Prepare a dummy parser compile result.
            var dummyVariables = new List<ParameterExpression> { Expression.Parameter(typeof(int), "dummy") };
            var dummyExpressions = new List<Expression> { Expression.Constant(100) };

            var fakeParserCompileResult = new FakeParserCompileResult
            {
                Variables = dummyVariables,
                Body = dummyExpressions
            };

            // Create a mock for the underlying parser with Build method.
            var mockParser = new Mock<Parser<int>>();
            mockParser.Setup(p => p.Build(It.IsAny<CompilationContext>(), true))
                      .Returns(fakeParserCompileResult);

            var seekable = new Seekable<int>(mockParser.Object, false, "abc".AsSpan());

            var fakeCompilationContext = new FakeCompilationContext();

            // Act
            var compilationResult = seekable.Compile(fakeCompilationContext);

            // Assert
            Assert.IsNotNull(compilationResult, "CompilationResult should not be null.");
            // The compilation result Body should have one block added.
            Assert.AreEqual(1, compilationResult.Body.Count, "CompilationResult.Body should contain exactly one block.");
            // Verify that the block contains the expected expressions.
            var blockExpression = compilationResult.Body.First() as BlockExpression;
            Assert.IsNotNull(blockExpression, "The added expression is not a BlockExpression.");
            CollectionAssert.AreEqual(dummyVariables, blockExpression.Variables.ToList(), "BlockExpression variables do not match expected variables.");
            CollectionAssert.AreEqual(dummyExpressions, blockExpression.Expressions.ToList(), "BlockExpression body expressions do not match expected expressions.");
        }

        #endregion

        #region ToString Tests

        /// <summary>
        /// Tests that the ToString method returns the underlying parser's string representation appended with " (Seekable)".
        /// </summary>
        [TestMethod]
        public void ToString_ReturnsUnderlyingParserToStringAppendedWithSeekable()
        {
            // Arrange
            var parserToString = "FakeParser";
            var mockParser = new Mock<Parser<int>>();
            mockParser.Setup(p => p.ToString()).Returns(parserToString);

            var seekable = new Seekable<int>(mockParser.Object, false, "def".AsSpan());

            // Act
            var result = seekable.ToString();

            // Assert
            Assert.AreEqual($"{parserToString} (Seekable)", result, "ToString did not return the expected string.");
        }

        #endregion

        #region Helper Classes

        /// <summary>
        /// A fake implementation of ParseContext to track parser entry and exit.
        /// </summary>
        private class FakeParseContext : ParseContext
        {
            public List<string> CallLog { get; } = new List<string>();

            public override void EnterParser(object parser)
            {
                CallLog.Add("Enter:" + parser.ToString());
            }

            public override void ExitParser(object parser)
            {
                CallLog.Add("Exit:" + parser.ToString());
            }
        }

        /// <summary>
        /// A fake implementation of CompilationContext for testing purposes.
        /// </summary>
        private class FakeCompilationContext : CompilationContext
        {
            public override CompilationResult CreateCompilationResult<T>(bool withResult)
            {
                return new FakeCompilationResult();
            }
        }

        /// <summary>
        /// A fake implementation of CompilationResult for testing purposes.
        /// </summary>
        private class FakeCompilationResult : CompilationResult
        {
            public List<Expression> Body { get; } = new List<Expression>();

            // Since CompilationResult is expected to have a Body property that is a List of Expressions,
            // we expose it through our FakeCompilationResult.
        }

        /// <summary>
        /// A fake implementation for the result returned by the Build method of a Parser.
        /// </summary>
        private class FakeParserCompileResult : ParserCompileResult
        {
            public override IEnumerable<ParameterExpression> Variables { get; set; }
            public override IEnumerable<Expression> Body { get; set; }
        }

        #endregion
    }

    #region Minimal Abstract Class Implementations

    // The following minimal abstract implementations are provided to support testing of Seekable<T>.
    // In an actual scenario, these would be part of the Parlot library.

    /// <summary>
    /// Minimal abstract implementation of Parser<T> to support Seekable tests.
    /// </summary>
    public abstract class Parser<T>
    {
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
        public abstract CompilationResult Compile(CompilationContext context);
        public virtual ParserCompileResult Build(CompilationContext context, bool requireResult)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Minimal abstract implementation of ParserCompileResult to support testing.
    /// </summary>
    public abstract class ParserCompileResult
    {
        public abstract IEnumerable<ParameterExpression> Variables { get; set; }
        public abstract IEnumerable<Expression> Body { get; set; }
    }

    /// <summary>
    /// Minimal abstract implementation of ParseContext to support testing.
    /// </summary>
    public abstract class ParseContext
    {
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
    }

    /// <summary>
    /// Minimal implementation of ParseResult to support testing.
    /// </summary>
    public class ParseResult<T>
    {
        public T Value { get; set; }
    }

    /// <summary>
    /// Minimal abstract implementation of CompilationContext to support testing.
    /// </summary>
    public abstract class CompilationContext
    {
        public abstract CompilationResult CreateCompilationResult<T>(bool withResult);
    }

    /// <summary>
    /// Minimal abstract implementation of CompilationResult to support testing.
    /// </summary>
    public abstract class CompilationResult
    {
        // For the sake of testing, we assume CompilationResult exposes a Body property.
        public abstract List<Expression> Body { get; }
    }

    #endregion
}
