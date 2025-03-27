using Moq;
using Parlot.Fluent;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="TextLiteral"/> class.
    /// </summary>
    public class TextLiteralTests
    {
        /// <summary>
        /// Tests that the constructor of TextLiteral throws an ArgumentNullException when null is passed as text.
        /// </summary>
        [Fact]
        public void Constructor_NullText_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new TextLiteral(null, StringComparison.Ordinal));
            Assert.Equal("Value cannot be null. (Parameter 'text')", exception.Message);
        }

        /// <summary>
        /// Tests that when an empty string is passed to the constructor, CanSeek is false and ExpectedChars remains empty.
        /// </summary>
        [Fact]
        public void Constructor_EmptyText_ResultsInNoSeekAndEmptyExpectedChars()
        {
            // Arrange
            string text = string.Empty;

            // Act
            var textLiteral = new TextLiteral(text, StringComparison.Ordinal);

            // Assert
            Assert.False(textLiteral.CanSeek);
            Assert.Empty(textLiteral.ExpectedChars);
        }

        /// <summary>
        /// Tests that when a non-empty text is passed, the ExpectedChars property is set correctly for case-sensitive comparison.
        /// </summary>
        [Fact]
        public void Constructor_NonEmptyText_CaseSensitive_SetsExpectedCharsCorrectly()
        {
            // Arrange
            string text = "Abc";

            // Act
            var textLiteral = new TextLiteral(text, StringComparison.Ordinal);

            // Assert
            Assert.True(textLiteral.CanSeek);
            Assert.Single(textLiteral.ExpectedChars);
            Assert.Equal(text[0], textLiteral.ExpectedChars[0]);
        }

        /// <summary>
        /// Tests that when a non-empty text is passed with a case insensitive comparison, the ExpectedChars property is set to both upper and lower cases.
        /// </summary>
        [Fact]
        public void Constructor_NonEmptyText_CaseInsensitive_SetsExpectedCharsCorrectly()
        {
            // Arrange
            string text = "aBc";
            // Using an ignore case comparison
            var comparison = StringComparison.OrdinalIgnoreCase;

            // Act
            var textLiteral = new TextLiteral(text, comparison);

            // Assert
            Assert.True(textLiteral.CanSeek);
            Assert.Equal(2, textLiteral.ExpectedChars.Length);
            Assert.Contains(char.ToUpper(text[0]), textLiteral.ExpectedChars);
            Assert.Contains(char.ToLower(text[0]), textLiteral.ExpectedChars);
        }

        /// <summary>
        /// Tests the Parse method for a successful match scenario.
        /// </summary>
//         [Fact] [Error] (95-34)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(string)' and 'FakeCursor.FakeCursor(string)' [Error] (96-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeScanner.FakeScanner(FakeCursor)' and 'FakeScanner.FakeScanner(FakeCursor)' [Error] (97-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (101-46)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (101-63)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<string>' to 'ref Parlot.ParseResult<string>' [Error] (105-36)CS0229 Ambiguity between 'FakeParseResult<string>.Start' and 'FakeParseResult<string>.Start' [Error] (106-53)CS0229 Ambiguity between 'FakeParseResult<string>.End' and 'FakeParseResult<string>.End' [Error] (107-46)CS0229 Ambiguity between 'FakeParseResult<string>.Value' and 'FakeParseResult<string>.Value'
//         public void Parse_WhenTextMatches_ReturnsTrueAndSetsResult()
//         {
//             // Arrange
//             string literalText = "test";
//             var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);
//             // Create a fake parse context with a scanner that has input starting with literalText.
//             var input = literalText + " remaining";
//             var fakeCursor = new FakeCursor(input);
//             var fakeScanner = new FakeScanner(fakeCursor);
//             var fakeContext = new FakeParseContext(fakeScanner);
//             var result = new FakeParseResult<string>();
// 
//             // Act
//             bool success = textLiteral.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.True(success);
//             Assert.Equal(0, result.Start);
//             Assert.Equal(literalText.Length, result.End);
//             Assert.Equal(literalText, result.Value);
//         }

        /// <summary>
        /// Tests the Parse method for a failure scenario when the text does not match.
        /// </summary>
//         [Fact] [Error] (120-34)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(string)' and 'FakeCursor.FakeCursor(string)' [Error] (121-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeScanner.FakeScanner(FakeCursor)' and 'FakeScanner.FakeScanner(FakeCursor)' [Error] (122-35)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (126-46)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeParseContext' to 'Parlot.Fluent.ParseContext' [Error] (126-63)CS1503 Argument 2: cannot convert from 'ref Parlot.Fluent.UnitTests.FakeParseResult<string>' to 'ref Parlot.ParseResult<string>' [Error] (131-36)CS0229 Ambiguity between 'FakeParseResult<string>.Start' and 'FakeParseResult<string>.Start' [Error] (132-36)CS0229 Ambiguity between 'FakeParseResult<string>.End' and 'FakeParseResult<string>.End' [Error] (133-32)CS0229 Ambiguity between 'FakeParseResult<string>.Value' and 'FakeParseResult<string>.Value'
//         public void Parse_WhenTextDoesNotMatch_ReturnsFalseAndLeavesResultUnchanged()
//         {
//             // Arrange
//             string literalText = "hello";
//             var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);
//             var input = "world";
//             var fakeCursor = new FakeCursor(input);
//             var fakeScanner = new FakeScanner(fakeCursor);
//             var fakeContext = new FakeParseContext(fakeScanner);
//             var result = new FakeParseResult<string>();
// 
//             // Act
//             bool success = textLiteral.Parse(fakeContext, ref result);
// 
//             // Assert
//             Assert.False(success);
//             // Result should not be set (Start remains default 0 and Value null)
//             Assert.Equal(0, result.Start);
//             Assert.Equal(0, result.End);
//             Assert.Null(result.Value);
//         }

        /// <summary>
        /// Tests that the ToString override returns the expected formatted string.
        /// </summary>
        [Fact]
        public void ToString_ReturnsFormattedText()
        {
            // Arrange
            string literalText = "example";
            var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);

            // Act
            string result = textLiteral.ToString();

            // Assert
            Assert.Equal($"Text(\"{literalText}\")", result);
        }

        /// <summary>
        /// Tests that the Compile method returns a compilation result with a non-empty body.
        /// </summary>
//         [Fact] [Error] (162-77)CS0121 The call is ambiguous between the following methods or properties: 'FakeCursor.FakeCursor(string)' and 'FakeCursor.FakeCursor(string)' [Error] (162-61)CS0121 The call is ambiguous between the following methods or properties: 'FakeScanner.FakeScanner(FakeCursor)' and 'FakeScanner.FakeScanner(FakeCursor)' [Error] (162-40)CS0121 The call is ambiguous between the following methods or properties: 'FakeParseContext.FakeParseContext(FakeScanner)' and 'FakeParseContext.FakeParseContext(FakeScanner)' [Error] (165-17)CS0229 Ambiguity between 'FakeCompilationContext.DiscardResult' and 'FakeCompilationContext.DiscardResult' [Error] (169-57)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.FakeCompilationContext' to 'Parlot.Compilation.CompilationContext' [Error] (175-89)CS0234 The type or namespace name 'IfThenExpression' does not exist in the namespace 'System.Linq.Expressions' (are you missing an assembly reference?)
//         public void Compile_ReturnsCompilationResultWithNonEmptyBody()
//         {
//             // Arrange
//             string literalText = "compile";
//             var textLiteral = new TextLiteral(literalText, StringComparison.Ordinal);
//             var fakeParseContext = new FakeParseContext(new FakeScanner(new FakeCursor(literalText)));
//             var fakeCompilationContext = new FakeCompilationContext(fakeParseContext)
//             {
//                 DiscardResult = false
//             };
// 
//             // Act
//             var compilationResult = textLiteral.Compile(fakeCompilationContext);
// 
//             // Assert
//             Assert.NotNull(compilationResult);
//             Assert.NotEmpty(compilationResult.Body);
//             // Check that the first expression is an IfThen expression as expected.
//             var ifThenExpression = compilationResult.Body[0] as System.Linq.Expressions.IfThenExpression;
//             Assert.NotNull(ifThenExpression);
//         }
    }

    #region Fake Classes for Parsing

    /// <summary>
    /// A fake cursor to simulate scanning behavior.
    /// </summary>
//     internal class FakeCursor [Error] (185-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCursor' [Error] (191-16)CS0111 Type 'FakeCursor' already defines a member called 'FakeCursor' with the same parameter types [Error] (193-13)CS0229 Ambiguity between 'FakeCursor._input' and 'FakeCursor._input' [Error] (194-13)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
//     {
//         private readonly string _input; [Error] (187-33)CS0169 The field 'FakeCursor._input' is never used
// 
//         public int Offset { get; private set; }
// 
//         public FakeCursor(string input)
//         {
//             _input = input ?? throw new ArgumentNullException(nameof(input));
//             Offset = 0;
//         }
// 
//         /// <summary>
//         /// Attempts to match the provided span with the input starting from the current offset using the specified comparison.
//         /// </summary>
//         public bool Match(ReadOnlySpan<char> span, StringComparison comparison) [Error] (202-17)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset' [Error] (202-40)CS0229 Ambiguity between 'FakeCursor._input' and 'FakeCursor._input' [Error] (206-20)CS0229 Ambiguity between 'FakeCursor._input' and 'FakeCursor._input' [Error] (206-34)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
//         {
//             if (Offset + span.Length > _input.Length)
//             {
//                 return false;
//             }
//             return _input.AsSpan(Offset, span.Length).Equals(span, comparison);
//         }
// 
//         /// <summary>
//         /// Advances the cursor by the specified length.
//         /// </summary>
//         public void Advance(int length) [Error] (214-13)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
//         {
//             Offset += length;
//         }
// 
//         /// <summary>
//         /// Advances the cursor by the specified length without passing new line characters.
//         /// For the fake implementation, behaves the same as Advance.
//         /// </summary>
//         public void AdvanceNoNewLines(int length) [Error] (223-13)CS0229 Ambiguity between 'FakeCursor.Offset' and 'FakeCursor.Offset'
//         {
//             Offset += length;
//         }
//     }

    /// <summary>
    /// A fake scanner that holds a fake cursor.
    /// </summary>
//     internal class FakeScanner [Error] (230-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeScanner' [Error] (234-16)CS0111 Type 'FakeScanner' already defines a member called 'FakeScanner' with the same parameter types [Error] (236-13)CS0229 Ambiguity between 'FakeScanner.Cursor' and 'FakeScanner.Cursor'
//     {
//         public FakeCursor Cursor { get; }
// 
//         public FakeScanner(FakeCursor cursor)
//         {
//             Cursor = cursor;
//         }
//     }

    /// <summary>
    /// A fake parse context to simulate the parser environment.
    /// </summary>
//     internal class FakeParseContext [Error] (243-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseContext' [Error] (247-16)CS0111 Type 'FakeParseContext' already defines a member called 'FakeParseContext' with the same parameter types [Error] (249-13)CS0229 Ambiguity between 'FakeParseContext.Scanner' and 'FakeParseContext.Scanner'
//     {
//         public FakeScanner Scanner { get; } [Error] (245-28)CS0108 'FakeParseContext.Scanner' hides inherited member 'ParseContext.Scanner'. Use the new keyword if hiding was intended.
// 
//         public FakeParseContext(FakeScanner scanner)
//         {
//             Scanner = scanner;
//         }
// 
//         public void EnterParser(object parser) [Error] (252-21)CS0114 'FakeParseContext.EnterParser(object)' hides inherited member 'ParseContext.EnterParser(object)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (252-21)CS0111 Type 'FakeParseContext' already defines a member called 'EnterParser' with the same parameter types
//         {
//             // No operation for fake.
//         }
// 
//         public void ExitParser(object parser) [Error] (257-21)CS0114 'FakeParseContext.ExitParser(object)' hides inherited member 'ParseContext.ExitParser(object)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword. [Error] (257-21)CS0111 Type 'FakeParseContext' already defines a member called 'ExitParser' with the same parameter types
//         {
//             // No operation for fake.
//         }
//     }

    /// <summary>
    /// A fake parse result to hold parsing results.
    /// </summary>
    /// <typeparam name="T"></typeparam>
//     internal class FakeParseResult<T> : ParseResult<T> [Error] (267-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeParseResult'
//     {
//         public int StartValue { get; private set; }
//         public int EndValue { get; private set; }
//         public T ParsedValue { get; private set; }
// 
//         public FakeParseResult()
//         {
//             // Initialize with default values.
//             StartValue = 0;
//             EndValue = 0;
//             ParsedValue = default;
//         }
// 
//         public override void Set(int start, int end, T value) [Error] (281-30)CS0462 The inherited members 'ParseResult<T>.Set(int, int, T)' and 'ParseResult<T>.Set(int, int, T)' have the same signature in type 'FakeParseResult<T>', so they cannot be overridden [Error] (281-30)CS0111 Type 'FakeParseResult<T>' already defines a member called 'Set' with the same parameter types
//         {
//             StartValue = start;
//             EndValue = end;
//             ParsedValue = value;
//         }
// 
//         public int Start => StartValue; [Error] (288-20)CS0108 'FakeParseResult<T>.Start' hides inherited member 'ParseResult<T>.Start'. Use the new keyword if hiding was intended.
//         public int End => EndValue; [Error] (289-20)CS0108 'FakeParseResult<T>.End' hides inherited member 'ParseResult<T>.End'. Use the new keyword if hiding was intended.
//         public T Value => ParsedValue; [Error] (290-18)CS0108 'FakeParseResult<T>.Value' hides inherited member 'ParseResult<T>.Value'. Use the new keyword if hiding was intended.
//     }

    #endregion

    #region Fake Classes for Compilation

    /// <summary>
    /// A fake compilation context to simulate the compilation environment.
    /// </summary>
//     internal class FakeCompilationContext : CompilationContext [Error] (300-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationContext'
//     {
//         public FakeParseContext ParseContextInstance { get; }
// 
//         public override Expression ParseContext => Expression.Constant(ParseContextInstance); [Error] (304-36)CS1715 'FakeCompilationContext.ParseContext': type must be 'ParameterExpression' to match overridden member 'CompilationContext.ParseContext'
// 
//         public bool DiscardResult { get; set; } [Error] (306-21)CS0114 'FakeCompilationContext.DiscardResult' hides inherited member 'CompilationContext.DiscardResult'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
// 
//         public FakeCompilationContext(FakeParseContext parseContext)
//         {
//             ParseContextInstance = parseContext;
//         }
// 
//         public override CompilationResult<T> CreateCompilationResult<T>() [Error] (313-46)CS0111 Type 'FakeCompilationContext' already defines a member called 'CreateCompilationResult' with the same parameter types [Error] (315-24)CS0121 The call is ambiguous between the following methods or properties: 'FakeCompilationResult<T>.FakeCompilationResult()' and 'FakeCompilationResult<T>.FakeCompilationResult()'
//         {
//             return new FakeCompilationResult<T>();
//         }
//     }

    /// <summary>
    /// A fake compilation result to store compilation expressions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
//     internal class FakeCompilationResult<T> : CompilationResult<T> [Error] (323-20)CS0101 The namespace 'Parlot.Fluent.UnitTests' already contains a definition for 'FakeCompilationResult' [Error] (325-16)CS0111 Type 'FakeCompilationResult<T>' already defines a member called 'FakeCompilationResult' with the same parameter types [Error] (327-13)CS0229 Ambiguity between 'FakeCompilationResult<T>.Body' and 'FakeCompilationResult<T>.Body' [Error] (328-13)CS0229 Ambiguity between 'FakeCompilationResult<T>.Success' and 'FakeCompilationResult<T>.Success' [Error] (329-13)CS0229 Ambiguity between 'FakeCompilationResult<T>.Value' and 'FakeCompilationResult<T>.Value'
//     {
//         public FakeCompilationResult()
//         {
//             Body = new List<Expression>();
//             Success = Expression.Variable(typeof(bool), "success");
//             Value = Expression.Variable(typeof(T), "value");
//         }
//     }

    #endregion
}
