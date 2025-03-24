using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="NonWhiteSpaceLiteral"/> class.
    /// </summary>
    public class NonWhiteSpaceLiteralTests
    {
        /// <summary>
        /// Tests that the Parse method returns false when the scanner is at end-of-file.
        /// </summary>
        [Fact]
        public void Parse_WhenEof_ReturnsFalse()
        {
            // Arrange
            var parser = new NonWhiteSpaceLiteral();
            var fakeCursor = new FakeCursor
            {
                Offset = 0,
                Eof = true
            };
            var fakeScanner = new FakeScanner("dummy", fakeCursor);
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.Null(result.Value);
        }

        /// <summary>
        /// Tests that the Parse method successfully reads non-whitespace characters when available using ReadNonWhiteSpaceOrNewLine.
        /// </summary>
        [Fact]
        public void Parse_WhenLiteralFound_WithIncludeNewLines_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var parser = new NonWhiteSpaceLiteral(includeNewLines: true);
            // Set initial buffer with a non-whitespace literal at beginning.
            string buffer = "abc  def";
            var fakeCursor = new FakeCursor
            {
                Offset = 0,
                Eof = false
            };
            var fakeScanner = new FakeScanner(buffer, fakeCursor)
            {
                // Simulate reading non-whitespace or new line:
                OnReadNonWhiteSpaceOrNewLine = () =>
                {
                    // Advance until a whitespace or end of literal is encountered.
                    // For simplicity, assume literal is contiguous non-whitespace characters.
                    while (fakeCursor.Offset < buffer.Length && !char.IsWhiteSpace(buffer[fakeCursor.Offset]))
                    {
                        fakeCursor.Offset++;
                    }
                }
            };
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseResult);
            Assert.NotNull(result.Value);
            // The expected literal is "abc"
            Assert.Equal("abc", result.Value.ToString());
        }

        /// <summary>
        /// Tests that the Parse method returns false when no non-whitespace literal is read (offset not advanced).
        /// </summary>
        [Fact]
        public void Parse_WhenNoLiteralRead_ReturnsFalse()
        {
            // Arrange
            var parser = new NonWhiteSpaceLiteral(includeNewLines: false);
            string buffer = "   "; // only whitespace
            var fakeCursor = new FakeCursor
            {
                Offset = 0,
                Eof = false
            };
            var fakeScanner = new FakeScanner(buffer, fakeCursor)
            {
                // Simulate that ReadNonWhiteSpace does not advance because first char is whitespace.
                OnReadNonWhiteSpace = () =>
                {
                    // Do not change offset since there is no literal.
                }
            };
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<TextSpan>();

            // Act
            bool parseResult = parser.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseResult);
            Assert.Null(result.Value);
        }

        /// <summary>
        /// Tests that the Compile method returns a valid CompilationResult containing a non-empty body.
        /// </summary>
        [Fact]
        public void Compile_ReturnsCompilationResultWithValidBody()
        {
            // Arrange
            var parser = new NonWhiteSpaceLiteral(includeNewLines: true);
            var fakeCompilationContext = new FakeCompilationContext();

            // Act
            CompilationResult compilationResult = parser.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
        }
    }

    #region Fake Implementations for Parse Testing

    /// <summary>
    /// Fake implementation of a cursor used in scanning.
    /// </summary>
    internal class FakeCursor
    {
        public int Offset { get; set; }
        public bool Eof { get; set; }
    }

    /// <summary>
    /// Fake implementation of a scanner.
    /// </summary>
    internal class FakeScanner
    {
        public string Buffer { get; }
        public FakeCursor Cursor { get; }
        public Action? OnReadNonWhiteSpace { get; set; }
        public Action? OnReadNonWhiteSpaceOrNewLine { get; set; }

        public FakeScanner(string buffer, FakeCursor cursor)
        {
            Buffer = buffer;
            Cursor = cursor;
        }

        public void ReadNonWhiteSpace()
        {
            OnReadNonWhiteSpace?.Invoke();
        }

        public void ReadNonWhiteSpaceOrNewLine()
        {
            OnReadNonWhiteSpaceOrNewLine?.Invoke();
        }
    }

    /// <summary>
    /// Fake implementation of a parse context.
    /// </summary>
    internal class FakeParseContext : ParseContext
    {
        public FakeScanner Scanner { get; }

        public FakeParseContext(FakeScanner scanner)
        {
            Scanner = scanner;
            // Assign the fake scanner to the base Scanner property.
            base.Scanner = scanner;
        }

        public override void EnterParser(object parser)
        {
            // No-op for fake.
        }

        public override void ExitParser(object parser)
        {
            // No-op for fake.
        }
    }

    /// <summary>
    /// Fake implementation of a parse result.
    /// </summary>
    internal class FakeParseResult<T> : ParseResult<T>
    {
        public FakeParseResult() : base()
        {
        }

        public override void Set(int start, int end, T value)
        {
            this.Start = start;
            this.End = end;
            this.Value = value;
        }
    }

    #endregion

    #region Fake Implementations for Compilation Testing

    /// <summary>
    /// Fake implementation of a compilation result.
    /// </summary>
    internal class FakeCompilationResult<T> : CompilationResult
    {
        public List<Expression> BodyList { get; } = new List<Expression>();

        public override IList<Expression> Body => BodyList;

        public FakeCompilationResult()
        {
            // Create placeholders for Success and Value expressions.
            Success = Expression.Variable(typeof(bool), "success");
            Value = Expression.Variable(typeof(T), "value");
        }
    }

    /// <summary>
    /// Fake implementation of a compilation context.
    /// </summary>
    internal class FakeCompilationContext : CompilationContext
    {
        public override CompilationResult CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }

        public override Expression Eof()
        {
            // For compilation testing, assume we are not at EOF.
            return Expression.Constant(false);
        }

        public override Expression Offset()
        {
            // Return a dummy offset expression.
            return Expression.Constant(0);
        }

        public override Expression Buffer()
        {
            // Return a dummy buffer expression.
            return Expression.Constant("dummyBuffer");
        }

        public override Expression ReadNonWhiteSpace()
        {
            // Return an empty expression (as a stub).
            return Expression.Empty();
        }

        public override Expression ReadNonWhiteSpaceOrNewLine()
        {
            // Return an empty expression (as a stub).
            return Expression.Empty();
        }

        public override Expression NewTextSpan(Expression buffer, Expression start, Expression length)
        {
            // Create a new TextSpan using its constructor.
            var constructor = typeof(TextSpan).GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });
            if (constructor == null)
            {
                throw new InvalidOperationException("TextSpan constructor not found.");
            }
            return Expression.New(constructor, buffer, start, length);
        }

        public override Expression DiscardResult => Expression.Empty();
    }

    #endregion

    #region Minimal Abstract Base Classes for Compatibility

    // Minimal stubs to satisfy base classes since real implementations are not available in the test project.

    /// <summary>
    /// Minimal abstract implementation of ParseContext.
    /// </summary>
    public abstract class ParseContext
    {
        public FakeScanner? Scanner { get; set; }
        public abstract void EnterParser(object parser);
        public abstract void ExitParser(object parser);
    }

    /// <summary>
    /// Minimal abstract implementation of ParseResult.
    /// </summary>
    public abstract class ParseResult<T>
    {
        public int Start { get; protected set; }
        public int End { get; protected set; }
        public T? Value { get; protected set; }
        public abstract void Set(int start, int end, T value);
    }

    /// <summary>
    /// Minimal abstract implementation of CompilationResult.
    /// </summary>
    public abstract class CompilationResult
    {
        public abstract IList<Expression> Body { get; }
        public Expression? Success { get; set; }
        public Expression? Value { get; set; }
    }

    /// <summary>
    /// Minimal abstract implementation of CompilationContext.
    /// </summary>
    public abstract class CompilationContext
    {
        public abstract CompilationResult CreateCompilationResult<T>();
        public abstract Expression Eof();
        public abstract Expression Offset();
        public abstract Expression Buffer();
        public abstract Expression ReadNonWhiteSpace();
        public abstract Expression ReadNonWhiteSpaceOrNewLine();
        public abstract Expression NewTextSpan(Expression buffer, Expression start, Expression length);
        public abstract Expression DiscardResult { get; }
    }

    /// <summary>
    /// Minimal abstract parser class.
    /// </summary>
    public abstract class Parser<T>
    {
        public string Name { get; protected set; } = string.Empty;
        public abstract bool Parse(ParseContext context, ref ParseResult<T> result);
    }

    /// <summary>
    /// Minimal interface for compilable parsers.
    /// </summary>
    public interface ICompilable
    {
        CompilationResult Compile(CompilationContext context);
    }

    /// <summary>
    /// Minimal implementation of TextSpan.
    /// </summary>
    public class TextSpan
    {
        private readonly string _buffer;
        private readonly int _start;
        private readonly int _length;

        public TextSpan(string buffer, int start, int length)
        {
            _buffer = buffer;
            _start = start;
            _length = length;
        }

        public override string ToString()
        {
            if (_buffer == null || _start < 0 || _start + _length > _buffer.Length)
            {
                return string.Empty;
            }
            return _buffer.Substring(_start, _length);
        }
    }

    #endregion
}
