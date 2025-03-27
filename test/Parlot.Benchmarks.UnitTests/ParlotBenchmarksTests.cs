using Moq;
using Parlot.Benchmarks;
using Parlot.Fluent;
using Parlot.Tests.Calc;
using Parlot.Tests.Json;
using System;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ParlotBenchmarks"/> class.
    /// </summary>
    public class ParlotBenchmarksTests
    {
        private readonly ParlotBenchmarks _benchmarks;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParlotBenchmarksTests"/> class.
        /// Calls the Setup method to initialize benchmark dependencies.
        /// </summary>
        public ParlotBenchmarksTests()
        {
            _benchmarks = new ParlotBenchmarks();
            _benchmarks.Setup();
        }

        /// <summary>
        /// Tests that CreateCompiledSmallParser returns a compiled parser that correctly parses an accepted character.
        /// </summary>
        [Fact]
        public void CreateCompiledSmallParser_ValidInput_ReturnsParserThatParsesExpectedValue()
        {
            // Act
            var parser = _benchmarks.CreateCompiledSmallParser();
            var result = parser.Parse("a");

            // Assert
            Assert.Equal('a', result);
        }

        /// <summary>
        /// Tests that CreateCompiledExpressionParser returns a non-null compiled expression parser.
        /// </summary>
        [Fact]
        public void CreateCompiledExpressionParser_ValidInput_ReturnsNonNullParser()
        {
            // Act
            var parser = _benchmarks.CreateCompiledExpressionParser();

            // Assert
            Assert.NotNull(parser);
        }

        /// <summary>
        /// Tests that CursorMatchHello correctly matches the string "hello".
        /// </summary>
        [Fact]
        public void CursorMatchHello_ValidHelloInput_ReturnsHello()
        {
            // Act
            var result = _benchmarks.CursorMatchHello();

            // Assert
            Assert.Equal("hello", result);
        }

        /// <summary>
        /// Tests that CursorMatchGoodbye correctly matches the string "goodbye".
        /// </summary>
        [Fact]
        public void CursorMatchGoodbye_ValidGoodbyeInput_ReturnsGoodbye()
        {
            // Act
            var result = _benchmarks.CursorMatchGoodbye();

            // Assert
            Assert.Equal("goodbye", result);
        }

        /// <summary>
        /// Tests that CursorMatchNone returns null when the input does not match any expected string.
        /// </summary>
        [Fact]
        public void CursorMatchNone_InvalidInput_ReturnsNull()
        {
            // Act
            var result = _benchmarks.CursorMatchNone();

            // Assert
            // Assuming the parser returns null for non-matching input.
            Assert.Null(result);
        }

        /// <summary>
        /// Tests that Lookup returns the expected character when parsing a valid input.
        /// </summary>
        [Fact]
        public void Lookup_ValidInput_ReturnsExpectedCharacter()
        {
            // Act
            var result = _benchmarks.Lookup();

            // Assert
            Assert.Equal('d', result);
        }

        /// <summary>
        /// Tests that SkipWhiteSpace_0 correctly parses input without leading whitespace.
        /// </summary>
        [Fact]
        public void SkipWhiteSpace_0_NoLeadingWhitespace_ReturnsExpectedCharacter()
        {
            // Act
            var result = _benchmarks.SkipWhiteSpace_0();

            // Assert
            Assert.Equal('a', result);
        }

        /// <summary>
        /// Tests that SkipWhiteSpace_1 correctly parses input with one leading whitespace.
        /// </summary>
        [Fact]
        public void SkipWhiteSpace_1_OneLeadingWhitespace_ReturnsExpectedCharacter()
        {
            // Act
            var result = _benchmarks.SkipWhiteSpace_1();

            // Assert
            Assert.Equal('a', result);
        }

        /// <summary>
        /// Tests that SkipWhiteSpace_10 correctly parses input with multiple leading whitespaces.
        /// </summary>
        [Fact]
        public void SkipWhiteSpace_10_MultipleLeadingWhitespaces_ReturnsExpectedCharacter()
        {
            // Act
            var result = _benchmarks.SkipWhiteSpace_10();

            // Assert
            Assert.Equal('a', result);
        }

        /// <summary>
        /// Tests that DecodeStringWithoutEscapes returns a TextSpan with the expected decoded text for a string without escape sequences.
        /// </summary>
        [Fact]
        public void DecodeStringWithoutEscapes_ValidInput_ReturnsDecodedTextSpan()
        {
            // Act
            var result = _benchmarks.DecodeStringWithoutEscapes();

            // Assert
            // The expected output should reflect the actual newline and tab characters.
            string expected = "This is a new line \n \t and a tab and some \xa0";
            Assert.Equal(expected, result.ToString());
        }

        /// <summary>
        /// Tests that DecodeStringWithEscapes returns a TextSpan with the expected decoded text for a string with escape sequences.
        /// </summary>
        [Fact]
        public void DecodeStringWithEscapes_ValidInput_ReturnsDecodedTextSpan()
        {
            // Act
            var result = _benchmarks.DecodeStringWithEscapes();

            // Assert
            // The expected output preserves the literal backslashes and letters,
            // because the field contains double-escaped characters.
            string expected = "This is a new line \\n \\t and a tab and some \\xa0";
            Assert.Equal(expected, result.ToString());
        }

        /// <summary>
        /// Tests that ExpressionRawSmall returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ExpressionRawSmall_ValidInput_ReturnsNonNullExpression()
        {
            // Act
            var expr = _benchmarks.ExpressionRawSmall();

            // Assert
            Assert.NotNull(expr);
        }

        /// <summary>
        /// Tests that ExpressionCompiledSmall returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ExpressionCompiledSmall_ValidInput_ReturnsNonNullExpression()
        {
            // Act
            var expr = _benchmarks.ExpressionCompiledSmall();

            // Assert
            Assert.NotNull(expr);
        }

        /// <summary>
        /// Tests that ExpressionFluentSmall returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ExpressionFluentSmall_ValidInput_ReturnsNonNullExpression()
        {
            // Act
            var expr = _benchmarks.ExpressionFluentSmall();

            // Assert
            Assert.NotNull(expr);
        }

        /// <summary>
        /// Tests that ExpressionRawBig returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ExpressionRawBig_ValidInput_ReturnsNonNullExpression()
        {
            // Act
            var expr = _benchmarks.ExpressionRawBig();

            // Assert
            Assert.NotNull(expr);
        }

        /// <summary>
        /// Tests that ExpressionCompiledBig returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ExpressionCompiledBig_ValidInput_ReturnsNonNullExpression()
        {
            // Act
            var expr = _benchmarks.ExpressionCompiledBig();

            // Assert
            Assert.NotNull(expr);
        }

        /// <summary>
        /// Tests that ExpressionFluentBig returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ExpressionFluentBig_ValidInput_ReturnsNonNullExpression()
        {
            // Act
            var expr = _benchmarks.ExpressionFluentBig();

            // Assert
            Assert.NotNull(expr);
        }

        /// <summary>
        /// Tests that BigJson returns a non-null IJson.
        /// </summary>
        [Fact]
        public void BigJson_ValidInput_ReturnsNonNullIJson()
        {
            // Act
            var json = _benchmarks.BigJson();

            // Assert
            Assert.NotNull(json);
        }

        /// <summary>
        /// Tests that BigJsonCompiled returns a non-null IJson.
        /// </summary>
        [Fact]
        public void BigJsonCompiled_ValidInput_ReturnsNonNullIJson()
        {
            // Act
            var json = _benchmarks.BigJsonCompiled();

            // Assert
            Assert.NotNull(json);
        }

        /// <summary>
        /// Tests that DeepJson returns a non-null IJson.
        /// </summary>
        [Fact]
        public void DeepJson_ValidInput_ReturnsNonNullIJson()
        {
            // Act
            var json = _benchmarks.DeepJson();

            // Assert
            Assert.NotNull(json);
        }

        /// <summary>
        /// Tests that DeepJsonCompiled returns a non-null IJson.
        /// </summary>
        [Fact]
        public void DeepJsonCompiled_ValidInput_ReturnsNonNullIJson()
        {
            // Act
            var json = _benchmarks.DeepJsonCompiled();

            // Assert
            Assert.NotNull(json);
        }

        /// <summary>
        /// Tests that LongJson returns a non-null IJson.
        /// </summary>
        [Fact]
        public void LongJson_ValidInput_ReturnsNonNullIJson()
        {
            // Act
            var json = _benchmarks.LongJson();

            // Assert
            Assert.NotNull(json);
        }

        /// <summary>
        /// Tests that LongJsonCompiled returns a non-null IJson.
        /// </summary>
        [Fact]
        public void LongJsonCompiled_ValidInput_ReturnsNonNullIJson()
        {
            // Act
            var json = _benchmarks.LongJsonCompiled();

            // Assert
            Assert.NotNull(json);
        }

        /// <summary>
        /// Tests that WideJson returns a non-null IJson.
        /// </summary>
        [Fact]
        public void WideJson_ValidInput_ReturnsNonNullIJson()
        {
            // Act
            var json = _benchmarks.WideJson();

            // Assert
            Assert.NotNull(json);
        }

        /// <summary>
        /// Tests that WideJsonCompiled returns a non-null IJson.
        /// </summary>
        [Fact]
        public void WideJsonCompiled_ValidInput_ReturnsNonNullIJson()
        {
            // Act
            var json = _benchmarks.WideJsonCompiled();

            // Assert
            Assert.NotNull(json);
        }

        /// <summary>
        /// Tests that CursorCtor creates a Cursor initialized with "hello" and a starting text position.
        /// </summary>
        [Fact]
        public void CursorCtor_ValidParameters_ReturnsCursorWithExpectedValues()
        {
            // Act
            var cursor = _benchmarks.CursorCtor();

            // Assert
            Assert.NotNull(cursor);
            // Assuming the Cursor's ToString() contains the initial text.
            Assert.Contains("hello", cursor.ToString());
        }

        /// <summary>
        /// Tests that ScannerCtor creates a Scanner initialized with "hello".
        /// </summary>
        [Fact]
        public void ScannerCtor_ValidParameters_ReturnsNonNullScanner()
        {
            // Act
            var scanner = _benchmarks.ScannerCtor();

            // Assert
            Assert.NotNull(scanner);
            // Assuming the Scanner's state is reflected in its string representation.
            Assert.Contains("hello", scanner.ToString());
        }

        /// <summary>
        /// Tests that ParseContextCtor creates a ParseContext initialized with a Scanner containing "hello".
        /// </summary>
        [Fact]
        public void ParseContextCtor_ValidParameters_ReturnsNonNullParseContext()
        {
            // Act
            var context = _benchmarks.ParseContextCtor();

            // Assert
            Assert.NotNull(context);
            // Assuming that the ParseContext exposes a property named "Scanner".
            var scannerProperty = context.GetType().GetProperty("Scanner");
            Assert.NotNull(scannerProperty);
            var scannerValue = scannerProperty.GetValue(context)?.ToString();
            Assert.NotNull(scannerValue);
            Assert.Contains("hello", scannerValue);
        }
    }
}
