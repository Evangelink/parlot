using Moq;
using Parlot;
using Parlot.Fluent;
using System;
using System.Reflection;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Scanner"/> class focusing on the SkipWhiteSpaceOrNewLine method.
/// </summary>
// public class ScannerTests [Error] (4069-2)CS1038 #endregion directive expected
// {
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine returns false and does not advance the cursor when the first character is not whitespace.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLine_FirstCharNonWhiteSpace_ReturnsFalseAndCursorUnchanged()
//     {
//         // Arrange
//         var input = "abc";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpaceOrNewLine();
//         // Assert
//         Assert.False(result);
//         // Assuming Cursor.Span returns the remaining text from the current cursor position.
//         Assert.Equal("abc", scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine returns true and advances the cursor past the leading whitespace characters.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLine_LeadingWhiteSpace_ReturnsTrueAndAdvancesCursorToFirstNonWhiteSpace()
//     {
//         // Arrange
//         var input = "   abc";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpaceOrNewLine();
//         // Assert
//         Assert.True(result);
//         // The cursor should now point to the first non-whitespace character "a" of "abc".
//         Assert.Equal("abc", scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine returns true when the leading whitespace includes newline characters and advances the cursor correctly.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLine_LeadingNewLine_ReturnsTrueAndAdvancesCursorToFirstNonWhiteSpace()
//     {
//         // Arrange
//         var input = "\nabc";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpaceOrNewLine();
//         // Assert
//         Assert.True(result);
//         // The cursor should now be positioned at "a" in "abc".
//         Assert.Equal("abc", scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine returns true when the entire buffer is whitespace and advances the cursor to the end.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLine_OnlyWhiteSpace_ReturnsTrueAndAdvancesCursorToEnd()
//     {
//         // Arrange
//         var input = "    ";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpaceOrNewLine();
//         // Assert
//         Assert.True(result);
//         // After skipping all whitespace the cursor should have an empty span.
//         Assert.Equal(string.Empty, scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that SkipWhiteSpaceOrNewLine returns false for an empty input buffer.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpaceOrNewLine_EmptyBuffer_ReturnsFalseAndCursorRemainsEmpty()
//     {
//         // Arrange
//         var input = string.Empty;
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpaceOrNewLine();
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace method when the input does not start with a whitespace character.
//     /// Expected behavior: the method should return false and the cursor position should not advance.
//     /// </summary>
//     [Theory]
//     [InlineData("abc", false, "abc")]
//     public void SkipWhiteSpace_NoLeadingWhitespace_ReturnsFalseAndDoesNotAdvance(string input, bool expectedResult, string expectedRemaining)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpace();
//         // Assert
//         Assert.Equal(expectedResult, result);
//         Assert.Equal(expectedRemaining, scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace method when the input starts with a single whitespace character.
//     /// Expected behavior: the method should return true and advance the cursor past the whitespace.
//     /// </summary>
//     [Theory]
//     [InlineData(" abc", true, "abc")]
//     public void SkipWhiteSpace_SingleLeadingWhitespace_ReturnsTrueAndAdvancesCursor(string input, bool expectedResult, string expectedRemaining)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpace();
//         // Assert
//         Assert.Equal(expectedResult, result);
//         Assert.Equal(expectedRemaining, scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace method when the input starts with multiple whitespace characters followed by non-whitespace.
//     /// Expected behavior: the method should return true and advance the cursor past all consecutive whitespace characters.
//     /// </summary>
//     [Theory]
//     [InlineData("  abc", true, "abc")]
//     [InlineData("   def", true, "def")]
//     public void SkipWhiteSpace_MultipleLeadingWhitespaces_ReturnsTrueAndAdvancesCursor(string input, bool expectedResult, string expectedRemaining)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpace();
//         // Assert
//         Assert.Equal(expectedResult, result);
//         Assert.Equal(expectedRemaining, scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace method when the input consists solely of whitespace characters.
//     /// Expected behavior: the method should return true and advance the cursor to the end resulting in an empty span.
//     /// </summary>
//     [Theory]
//     [InlineData("   ", true, "")]
//     [InlineData(" \t\n", true, "")]
//     public void SkipWhiteSpace_AllWhitespaces_ReturnsTrueAndExhaustsInput(string input, bool expectedResult, string expectedRemaining)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpace();
//         // Assert
//         Assert.Equal(expectedResult, result);
//         Assert.Equal(expectedRemaining, scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the SkipWhiteSpace method when the input is an empty string.
//     /// Expected behavior: the method should return false and the cursor span remains empty.
//     /// </summary>
//     [Fact]
//     public void SkipWhiteSpace_EmptyString_ReturnsFalseAndCursorSpanRemainsEmpty()
//     {
//         // Arrange
//         var input = "";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.SkipWhiteSpace();
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, scanner.Cursor.Span.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadFirstThenOthers returns true and the correct span when the input matches both the first and others predicates.
//     /// </summary>
//     [Fact]
//     public void ReadFirstThenOthers_ValidInput_ReturnsTrueAndCorrectSpan()
//     {
//         // Arrange
//         string input = "abc";
//         var scanner = new Scanner(input);
//         Func<char, bool> firstPredicate = c => c == 'a';
//         Func<char, bool> otherPredicate = c => char.IsLower(c);
//         // Act
//         bool result = scanner.ReadFirstThenOthers(firstPredicate, otherPredicate, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("abc", output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadFirstThenOthers returns false and an empty span when the first character does not satisfy the first predicate.
//     /// </summary>
//     [Fact]
//     public void ReadFirstThenOthers_InvalidFirstCharacter_ReturnsFalse()
//     {
//         // Arrange
//         string input = "bbc";
//         var scanner = new Scanner(input);
//         Func<char, bool> firstPredicate = c => c == 'a';
//         Func<char, bool> otherPredicate = c => char.IsLower(c);
//         // Act
//         bool result = scanner.ReadFirstThenOthers(firstPredicate, otherPredicate, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(result);
//         Assert.True(output.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadFirstThenOthers returns false and an empty span when the buffer is empty.
//     /// </summary>
//     [Fact]
//     public void ReadFirstThenOthers_EmptyBuffer_ReturnsFalse()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         Func<char, bool> firstPredicate = c => true;
//         Func<char, bool> otherPredicate = c => true;
//         // Act
//         bool result = scanner.ReadFirstThenOthers(firstPredicate, otherPredicate, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(result);
//         Assert.True(output.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadFirstThenOthers returns true and outputs a single character when only the first character satisfies the predicate.
//     /// </summary>
//     [Fact]
//     public void ReadFirstThenOthers_SingleMatchingCharacter_ReturnsTrueAndOutputSingleCharacter()
//     {
//         // Arrange
//         string input = "a1";
//         var scanner = new Scanner(input);
//         Func<char, bool> firstPredicate = c => c == 'a';
//         // The other predicate fails for '1'
//         Func<char, bool> otherPredicate = c => char.IsLetter(c);
//         // Act
//         bool result = scanner.ReadFirstThenOthers(firstPredicate, otherPredicate, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("a", output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the overload of ReadFirstThenOthers without the out parameter returns true for valid input.
//     /// </summary>
//     [Fact]
//     public void ReadFirstThenOthers_WithoutOutParameter_ValidInput_ReturnsTrue()
//     {
//         // Arrange
//         string input = "abcdef";
//         var scanner = new Scanner(input);
//         Func<char, bool> firstPredicate = c => c == 'a';
//         Func<char, bool> otherPredicate = c => char.IsLower(c);
//         // Act
//         bool result = scanner.ReadFirstThenOthers(firstPredicate, otherPredicate);
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that ReadFirstThenOthers returns false and provides an empty result when the first predicate fails.
//     /// </summary>
//     [Fact]
//     public void ReadFirstThenOthers_FirstPredicateFails_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = "abc";
//         var scanner = new Scanner(input);
//         // Define a first predicate that always fails.
//         Func<char, bool> firstPredicate = c => false;
//         // Other predicate is irrelevant in this scenario.
//         Func<char, bool> otherPredicate = c => true;
//         // Act
//         bool success = scanner.ReadFirstThenOthers(firstPredicate, otherPredicate, out ReadOnlySpan<char> resultSpan);
//         // Assert
//         Assert.False(success);
//         Assert.True(resultSpan.IsEmpty, "Expected result to be empty when the first predicate fails.");
//     }
// 
//     /// <summary>
//     /// Tests that ReadFirstThenOthers returns true and extracts only the first character when the other predicate always fails.
//     /// </summary>
//     [Fact]
//     public void ReadFirstThenOthers_OtherPredicateAlwaysFalse_ReturnsFirstCharOnly()
//     {
//         // Arrange
//         string input = "abc";
//         var scanner = new Scanner(input);
//         // Define a first predicate that always succeeds.
//         Func<char, bool> firstPredicate = c => true;
//         // Define an other predicate that always fails, so no further characters are consumed.
//         Func<char, bool> otherPredicate = c => false;
//         // Act
//         bool success = scanner.ReadFirstThenOthers(firstPredicate, otherPredicate, out ReadOnlySpan<char> resultSpan);
//         // Assert
//         Assert.True(success);
//         // After the first character is accepted and the cursor is advanced, only one character should be read.
//         Assert.Equal(input.Substring(0, 1), new string (resultSpan));
//     }
// 
//     /// <summary>
//     /// Tests that ReadFirstThenOthers returns true and correctly extracts a multi-character token when the other predicate accepts subsequent characters.
//     /// </summary>
//     [Fact]
//     public void ReadFirstThenOthers_OtherPredicateReturnsTrue_UntilConditionFails_ReturnsCorrectSubstring()
//     {
//         // Arrange
//         // The input buffer contains a sequence of letters followed by a digit.
//         string input = "abc1def";
//         var scanner = new Scanner(input);
//         // Define a first predicate that accepts any letter.
//         Func<char, bool> firstPredicate = c => char.IsLetter(c);
//         // Define an other predicate that accepts letters.
//         Func<char, bool> otherPredicate = c => char.IsLetter(c);
//         // Act
//         bool success = scanner.ReadFirstThenOthers(firstPredicate, otherPredicate, out ReadOnlySpan<char> resultSpan);
//         // Assert
//         Assert.True(success);
//         // Expected to read "abc" because after reading these letters, the next character is a digit which fails the predicate.
//         Assert.Equal("abc", new string (resultSpan));
//     }
// 
//     /// <summary>
//     /// Tests the ReadIdentifier(out ReadOnlySpan<char>) method with a valid identifier.
//     /// Expected outcome: the method returns true and outputs the correct identifier.
//     /// </summary>
//     [Fact]
//     public void ReadIdentifier_WithValidIdentifier_ReturnsTrueAndOutputsIdentifier()
//     {
//         // Arrange
//         string input = "abc123 def";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadIdentifier(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(success);
//         // The expected identifier is "abc123" until the first non-identifier char which is likely a space.
//         Assert.Equal("abc123", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadIdentifier(out ReadOnlySpan<char>) method when the input does not start with a valid identifier character.
//     /// Expected outcome: the method returns false and outputs an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadIdentifier_WithInvalidStartCharacter_ReturnsFalseAndOutputsEmptySpan()
//     {
//         // Arrange
//         string input = "1abc";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadIdentifier(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(success);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadIdentifier(out ReadOnlySpan<char>) method with an empty input string.
//     /// Expected outcome: the method returns false and outputs an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadIdentifier_WithEmptyInput_ReturnsFalseAndOutputsEmptySpan()
//     {
//         // Arrange
//         string input = string.Empty;
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadIdentifier(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(success);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadIdentifier() method (without out parameter) with a valid identifier.
//     /// Expected outcome: the method returns true.
//     /// </summary>
//     [Fact]
//     public void ReadIdentifier_NoOutParameter_WithValidIdentifier_ReturnsTrue()
//     {
//         // Arrange
//         string input = "_validIdentifier ";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadIdentifier();
//         // Assert
//         Assert.True(success);
//     }
// 
//     /// <summary>
//     /// Tests the ReadIdentifier(out ReadOnlySpan&lt;char&gt;) method with valid identifiers.
//     /// Expected to return true and output the complete identifier.
//     /// </summary>
//     /// <param name = "input">The input string to scan.</param>
//     /// <param name = "expectedIdentifier">The expected identifier string read.</param>
//     [Theory]
//     [InlineData("abc", "abc")]
//     [InlineData("abc123", "abc123")]
//     [InlineData("_identifier", "_identifier")]
//     public void ReadIdentifier_ValidIdentifier_ReturnsTrueAndCorrectIdentifier(string input, string expectedIdentifier)
//     {
//         // Arrange
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadIdentifier(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(expectedIdentifier, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadIdentifier(out ReadOnlySpan&lt;char&gt;) method when the first character is invalid.
//     /// Expected to return false and output an empty ReadOnlySpan.
//     /// </summary>
//     [Fact]
//     public void ReadIdentifier_InvalidStartCharacter_ReturnsFalseAndEmptyIdentifier()
//     {
//         // Arrange
//         // '1' is not a valid starting character for an identifier.
//         string input = "1abc";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadIdentifier(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty, "Expected the result span to be empty when identifier reading fails.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadIdentifier(out ReadOnlySpan&lt;char&gt;) method with an empty input.
//     /// Expected to return false and output an empty ReadOnlySpan.
//     /// </summary>
//     [Fact]
//     public void ReadIdentifier_EmptyInput_ReturnsFalseAndEmptyIdentifier()
//     {
//         // Arrange
//         string input = "";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadIdentifier(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty, "Expected the result span to be empty when input is empty.");
//     }
// 
//     /// <summary>
//     /// Tests the parameterless ReadIdentifier() method under a valid identifier input scenario.
//     /// Expected to return true.
//     /// </summary>
//     [Theory]
//     [InlineData("xyz")]
//     [InlineData("a1_b2")]
//     public void ReadIdentifier_NoOutParameter_ValidIdentifier_ReturnsTrue(string input)
//     {
//         // Arrange
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadIdentifier();
//         // Assert
//         Assert.True(success);
//     }
// 
//     /// <summary>
//     /// Tests the default ReadDecimal() method overload with a valid integer value.
//     /// Expected to return true and the parsed token "123".
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_Default_WithValidInteger_ReturnsParsedNumber()
//     {
//         // Arrange
//         var scanner = new Scanner("123");
//         // Act
//         bool result = scanner.ReadDecimal(out var token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("123", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the default ReadDecimal() method overload with an input that does not start with a digit.
//     /// Expected to return false and an empty token.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_Default_WithInvalidInput_ReturnsFalse()
//     {
//         // Arrange
//         var scanner = new Scanner("abc");
//         // Act
//         bool result = scanner.ReadDecimal(out var token);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadDecimal(NumberOptions, out ReadOnlySpan<char>, char, char) overload with a valid decimal number.
//     /// Expected to return true and the parsed token "123.45".
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_WithNumberOptions_ReturnsParsedDecimal()
//     {
//         // Arrange
//         var scanner = new Scanner("123.45");
//         // Assuming default number options when no flags are set are represented by 0.
//         NumberOptions options = 0;
//         // Act
//         bool result = scanner.ReadDecimal(options, out var token, '.', ',');
//         // Assert
//         Assert.True(result);
//         Assert.Equal("123.45", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadDecimal(bool, bool, bool, bool, out ReadOnlySpan<char>, char, char) overload with custom options that allow group separators.
//     /// Expected to return true and the parsed token "1,234".
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_WithCustomOptions_AllowsGroupSeparator_ReturnsParsedNumber()
//     {
//         // Arrange
//         var scanner = new Scanner("1,234");
//         // Act
//         bool result = scanner.ReadDecimal(true, true, true, false, out var token, '.', ',');
//         // Assert
//         Assert.True(result);
//         Assert.Equal("1,234", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadDecimal(bool, bool, bool, bool, out ReadOnlySpan<char>, char, char) overload with custom options disallowing a decimal separator.
//     /// Expected to return true and only parse the integer part "123" from the input "123.45".
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_WithCustomOptions_DisallowsDecimalSeparator_ParsesIntegerPartOnly()
//     {
//         // Arrange
//         var scanner = new Scanner("123.45");
//         // Act
//         bool result = scanner.ReadDecimal(true, false, false, true, out var token, '.', ',');
//         // Assert
//         Assert.True(result);
//         Assert.Equal("123", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadDecimal method returns true for a valid decimal input.
//     /// This test initializes a Scanner with a valid decimal string and calls ReadDecimal().
//     /// The expected outcome is a true result indicating that a valid decimal token was read.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_Parameterless_ValidInput_ReturnsTrue()
//     {
//         // Arrange
//         string input = "123";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadDecimal method returns false for an invalid decimal input.
//     /// This test initializes a Scanner with a non-decimal string and calls ReadDecimal().
//     /// The expected outcome is a false result indicating that no valid decimal token was read.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_Parameterless_InvalidInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "abc";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal();
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests that ReadDecimal with an out parameter correctly reads valid decimal tokens.
//     /// The test provides several valid decimal formats and validates that the returned token matches the expected value.
//     /// </summary>
//     /// <param name = "input">The input string containing the decimal token.</param>
//     /// <param name = "expectedToken">The expected token extracted from the input.</param>
//     [Theory]
//     [InlineData("123", "123")]
//     [InlineData("-456", "-456")]
//     [InlineData("789.01", "789.01")]
//     [InlineData("1e3", "1e3")]
//     public void ReadDecimal_OutParam_ValidInput_ReturnsTrueAndCorrectToken(string input, string expectedToken)
//     {
//         // Arrange
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expectedToken, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadDecimal with an out parameter returns false and produces an empty token for invalid decimal inputs.
//     /// The test examines various invalid inputs such as empty strings, non-decimal text and malformed decimals.
//     /// </summary>
//     /// <param name = "input">The invalid input string.</param>
//     [Theory]
//     [InlineData("")]
//     [InlineData("abc")]
//     [InlineData("12.34.56")]
//     public void ReadDecimal_OutParam_InvalidInput_ReturnsFalseAndEmptyToken(string input)
//     {
//         // Arrange
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that the ReadDecimal overload accepting NumberOptions returns true and produces the correct token for valid input.
//     /// This test assumes a default NumberOptions value (cast from zero) and validates the correct token extraction.
//     /// </summary>
//     /// <param name = "input">The input string containing the decimal token.</param>
//     /// <param name = "expectedToken">The expected token extracted from the input.</param>
//     [Theory]
//     [InlineData("123.45", "123.45")]
//     [InlineData("-678.90", "-678.90")]
//     public void ReadDecimal_NumberOptions_ValidInput_ReturnsTrueAndCorrectToken(string input, string expectedToken)
//     {
//         // Arrange
//         Scanner scanner = new Scanner(input);
//         NumberOptions options = (NumberOptions)0;
//         // Act
//         bool result = scanner.ReadDecimal(options, out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expectedToken, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the ReadDecimal overload accepting NumberOptions returns false and produces an empty token for invalid input.
//     /// The test covers cases where the input string does not represent a valid decimal token.
//     /// </summary>
//     /// <param name = "input">The invalid input string.</param>
//     [Theory]
//     [InlineData("abc")]
//     [InlineData("12.34.56")]
//     public void ReadDecimal_NumberOptions_InvalidInput_ReturnsFalseAndEmptyToken(string input)
//     {
//         // Arrange
//         Scanner scanner = new Scanner(input);
//         NumberOptions options = (NumberOptions)0;
//         // Act
//         bool result = scanner.ReadDecimal(options, out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that the boolean overload of ReadDecimal returns false when the decimal separator is not allowed.
//     /// This test uses input with a decimal point while explicitly disallowing the decimal separator, expecting failure.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_BooleanOverload_DisallowDecimalSeparator_InvalidInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "123.45";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign: true, allowDecimalSeparator: false, allowGroupSeparator: false, allowExponent: true, out ReadOnlySpan<char> token, decimalSeparator: '.', groupSeparator: ',');
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that the boolean overload of ReadDecimal returns true and produces the correct token when all options are allowed.
//     /// This test validates acceptance of decimal input including signs, decimal separators and exponent parts.
//     /// </summary>
//     /// <param name = "input">The input string containing the decimal token.</param>
//     /// <param name = "expectedToken">The expected token extracted from the input.</param>
//     [Theory]
//     [InlineData("123.45", "123.45")]
//     [InlineData("-123.45e6", "-123.45e6")]
//     public void ReadDecimal_BooleanOverload_AllAllowed_ValidInput_ReturnsTrueAndCorrectToken(string input, string expectedToken)
//     {
//         // Arrange
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign: true, allowDecimalSeparator: true, allowGroupSeparator: true, allowExponent: true, out ReadOnlySpan<char> token, decimalSeparator: '.', groupSeparator: ',');
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expectedToken, token.ToString());
//     }
// 
// #region ReadDecimal() tests (no parameters)
//     /// <summary>
//     /// Tests that ReadDecimal() returns true and consumes a valid decimal number when the input buffer starts with a valid decimal.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_NoParams_ValidDecimal_ReturnsTrue()
//     {
//         // Arrange
//         string input = "123.45 rest of text";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that ReadDecimal() returns false when the input does not start with a valid decimal number.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_NoParams_InvalidInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "abc123";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal();
//         // Assert
//         Assert.False(result);
//     }
// 
// #endregion
// #region ReadDecimal(out ReadOnlySpan<char>) tests
//     /// <summary>
//     /// Tests that ReadDecimal(out ReadOnlySpan<char>) returns true and outputs the expected decimal token for a positive number.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_OutParam_ValidPositiveDecimal_ReturnsToken()
//     {
//         // Arrange
//         string input = "789.01 extra";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("789.01", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadDecimal(out ReadOnlySpan<char>) returns true and outputs the expected token for a negative decimal.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_OutParam_ValidNegativeDecimal_ReturnsToken()
//     {
//         // Arrange
//         string input = "-456.78 trailing";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("-456.78", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadDecimal(out ReadOnlySpan<char>) returns false when the input buffer is empty.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_OutParam_EmptyInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
// #endregion
// #region ReadDecimal(NumberOptions, out ReadOnlySpan<char>, char, char) tests
//     /// <summary>
//     /// Tests that ReadDecimal(NumberOptions, out ReadOnlySpan<char>, char, char) correctly reads a decimal with group separators when allowed.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_NumberOptions_WithGroupSeparator_ReturnsCompleteToken()
//     {
//         // Arrange
//         // Assuming NumberOptions enum flags: AllowLeadingSign = 1, AllowDecimalSeparator = 2, AllowGroupSeparators = 4, AllowExponent = 8.
//         // Combined value to allow all parts.
//         NumberOptions options = (NumberOptions)(1 | 2 | 4 | 8);
//         string input = "1,234.56 remaining";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(options, out ReadOnlySpan<char> token, '.', ',');
//         // Assert
//         Assert.True(result);
//         Assert.Equal("1,234.56", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadDecimal(NumberOptions, out ReadOnlySpan<char>, char, char) returns false when the input does not start with a decimal token.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_NumberOptions_InvalidInput_ReturnsFalse()
//     {
//         // Arrange
//         NumberOptions options = (NumberOptions)(1 | 2 | 4 | 8);
//         string input = "noNumber123";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(options, out ReadOnlySpan<char> token, '.', ',');
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
// #endregion
// #region ReadDecimal(bool, bool, bool, bool, out ReadOnlySpan<char>, char, char) tests
//     /// <summary>
//     /// Tests that ReadDecimal(bool, bool, bool, bool, out ReadOnlySpan<char>, char, char) returns only the integer part when decimal separator is disallowed.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_CustomParameters_DisallowDecimalSeparator_ReturnsIntegerPartOnly()
//     {
//         // Arrange
//         // Here, disallow decimal separator so it should only read integer part even if decimal exists.
//         string input = "123.456 extra";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign: true, allowDecimalSeparator: false, allowGroupSeparator: false, allowExponent: true, out ReadOnlySpan<char> token, decimalSeparator: '.', groupSeparator: ',');
//         // Assert
//         Assert.True(result);
//         // Expected behavior: stops at the decimal separator.
//         Assert.Equal("123", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadDecimal(bool, bool, bool, bool, out ReadOnlySpan<char>, char, char) returns the full token with exponent when allowed.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_CustomParameters_AllowExponent_ReturnsTokenWithExponent()
//     {
//         // Arrange
//         string input = "3.14e10 and more";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign: true, allowDecimalSeparator: true, allowGroupSeparator: false, allowExponent: true, out ReadOnlySpan<char> token, decimalSeparator: '.', groupSeparator: ',');
//         // Assert
//         Assert.True(result);
//         Assert.Equal("3.14e10", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadDecimal(bool, bool, bool, bool, out ReadOnlySpan<char>, char, char) returns false when no valid digits are present at the start.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_CustomParameters_NonDigitStart_ReturnsFalse()
//     {
//         // Arrange
//         string input = "xyz123";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign: true, allowDecimalSeparator: true, allowGroupSeparator: true, allowExponent: true, out ReadOnlySpan<char> token, decimalSeparator: '.', groupSeparator: ',');
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
// #region Helpers
//     /// <summary>
//     /// Converts a ReadOnlySpan&lt;char&gt; to string.
//     /// </summary>
//     /// <param name = "span">The span instance.</param>
//     /// <returns>A string constructed from the given span.</returns>
//     private static string SpanToString(ReadOnlySpan<char> span) => new string (span);
// #endregion
// #region Tests for ReadDecimal() and ReadDecimal(out ReadOnlySpan<char>)
//     /// <summary>
//     /// Tests the default ReadDecimal() method with valid integer input.
//     /// Expected to return true and output the entire integer token.
//     /// </summary>
//     [Theory]
//     [InlineData("123", "123")]
//     [InlineData("-123", "-123")]
//     public void ReadDecimal_Default_ValidInteger_ReturnsToken(string input, string expected)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expected, tokenStr);
//     }
// 
//     /// <summary>
//     /// Tests the default ReadDecimal() method with valid decimal input.
//     /// Expected to return true and output the full decimal token.
//     /// </summary>
//     [Theory]
//     [InlineData("123.456", "123.456")]
//     [InlineData(".456", ".456")]
//     public void ReadDecimal_Default_ValidDecimal_ReturnsToken(string input, string expected)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expected, tokenStr);
//     }
// 
//     /// <summary>
//     /// Tests the default ReadDecimal() method with valid exponent part.
//     /// Expected to return true and output the full token including exponent.
//     /// </summary>
//     [Theory]
//     [InlineData("123e10", "123e10")]
//     [InlineData("123E+5", "123E+5")]
//     [InlineData("-123e-10", "-123e-10")]
//     public void ReadDecimal_Default_ValidExponent_ReturnsToken(string input, string expected)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expected, tokenStr);
//     }
// 
//     /// <summary>
//     /// Tests the default ReadDecimal() method with an invalid exponent sequence.
//     /// Expected to return false and leave the scanned part unchanged.
//     /// </summary>
//     [Theory]
//     [InlineData("123e", "123")]
//     [InlineData("123e-", "123")]
//     public void ReadDecimal_Default_InvalidExponent_ReturnsFalse(string input, string expectedPartial)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.False(result);
//         // In failure, the scanner resets to the initial position.
//         // Therefore, the token should not include the attempted number.
//         // Depending on implementation, it may either be empty or the partial valid token.
//         // Here, we expect that the scanner did not consume the invalid exponent.
//         Assert.Equal(expectedPartial, tokenStr);
//     }
// 
//     /// <summary>
//     /// Tests the default ReadDecimal() method with empty input.
//     /// Expected to return false.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_Default_EmptyInput_ReturnsFalse()
//     {
//         // Arrange
//         var scanner = new Scanner(string.Empty);
//         // Act
//         bool result = scanner.ReadDecimal(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, SpanToString(token));
//     }
// 
// #endregion
// #region Tests for ReadDecimal(NumberOptions, out ReadOnlySpan<char>, char, char)
//     /// <summary>
//     /// Tests the ReadDecimal overload that takes NumberOptions with a valid integer input.
//     /// Expected to return true and output the integer token.
//     /// </summary>
//     [Theory]
//     [InlineData("456", "456")]
//     public void ReadDecimal_NumberOptions_ValidInteger_ReturnsToken(string input, string expected)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // As NumberOptions details are not provided, using default value.
//         var numberOptions = default(NumberOptions);
//         // Act
//         bool result = scanner.ReadDecimal(numberOptions, out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expected, tokenStr);
//     }
// 
//     /// <summary>
//     /// Tests the ReadDecimal overload that takes NumberOptions with valid decimal input.
//     /// Expected to return true and output the full decimal token.
//     /// </summary>
//     [Theory]
//     [InlineData("789.01", "789.01")]
//     public void ReadDecimal_NumberOptions_ValidDecimal_ReturnsToken(string input, string expected)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         var numberOptions = default(NumberOptions);
//         // Act
//         bool result = scanner.ReadDecimal(numberOptions, out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expected, tokenStr);
//     }
// 
// #endregion
// #region Tests for ReadDecimal(bool, bool, bool, bool, out ReadOnlySpan<char>, char, char)
//     /// <summary>
//     /// Tests the full parameter ReadDecimal overload with group separator enabled.
//     /// Expected to return true and include the group separator in the token.
//     /// </summary>
//     [Theory]
//     [InlineData("1,234", "1,234")]
//     [InlineData("+1,234.56", "+1,234.56")]
//     public void ReadDecimal_FullParameters_GroupSeparatorEnabled_ReturnsToken(string input, string expected)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Enable group separator processing.
//         bool allowLeadingSign = true;
//         bool allowDecimalSeparator = true;
//         bool allowGroupSeparator = true;
//         bool allowExponent = true;
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign, allowDecimalSeparator, allowGroupSeparator, allowExponent, out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expected, tokenStr);
//     }
// 
//     /// <summary>
//     /// Tests the full parameter ReadDecimal overload with exponent invalid.
//     /// Expected to return false due to missing exponent digits.
//     /// </summary>
//     [Theory]
//     [InlineData("1,234e", "1,234")]
//     [InlineData("-1,234e-", "-1,234")]
//     public void ReadDecimal_FullParameters_InvalidExponent_ReturnsFalse(string input, string expectedPartial)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         bool allowLeadingSign = true;
//         bool allowDecimalSeparator = true;
//         bool allowGroupSeparator = true;
//         bool allowExponent = true;
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign, allowDecimalSeparator, allowGroupSeparator, allowExponent, out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(expectedPartial, tokenStr);
//     }
// 
//     /// <summary>
//     /// Tests the full parameter ReadDecimal overload with input that has a leading sign and a decimal part.
//     /// Expected to return true and output the complete token.
//     /// </summary>
//     [Theory]
//     [InlineData("-0.789", "-0.789")]
//     [InlineData("+0.789", "+0.789")]
//     public void ReadDecimal_FullParameters_SignedDecimal_ReturnsToken(string input, string expected)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         bool allowLeadingSign = true;
//         bool allowDecimalSeparator = true;
//         bool allowGroupSeparator = true;
//         bool allowExponent = true;
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign, allowDecimalSeparator, allowGroupSeparator, allowExponent, out ReadOnlySpan<char> token);
//         string tokenStr = SpanToString(token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expected, tokenStr);
//     }
// 
//     /// <summary>
//     /// Tests the full parameter ReadDecimal overload with empty input.
//     /// Expected to return false.
//     /// </summary>
//     [Fact]
//     public void ReadDecimal_FullParameters_EmptyInput_ReturnsFalse()
//     {
//         // Arrange
//         var scanner = new Scanner(string.Empty);
//         bool allowLeadingSign = true;
//         bool allowDecimalSeparator = true;
//         bool allowGroupSeparator = true;
//         bool allowExponent = true;
//         // Act
//         bool result = scanner.ReadDecimal(allowLeadingSign, allowDecimalSeparator, allowGroupSeparator, allowExponent, out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, SpanToString(token));
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger(out ReadOnlySpan<char>) method when the input begins with digits followed by non-digit characters.
//     /// Expects the method to successfully parse the leading integer and leave the rest unread.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_WithDigitsFollowedByLetters_ReturnsLeadingDigits()
//     {
//         // Arrange
//         // Input: digits followed by letters.
//         string input = "12345abc";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadInteger(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("12345", result.ToString());
//         // Attempting to read integer again should fail because the next char is non-digit.
//         bool secondAttempt = scanner.ReadInteger(out ReadOnlySpan<char> secondResult);
//         Assert.False(secondAttempt);
//         Assert.True(secondResult.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger(out ReadOnlySpan<char>) method when the input consists entirely of digits.
//     /// Expects the method to successfully parse the entire input as an integer.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_WithAllDigits_ReturnsEntireInput()
//     {
//         // Arrange
//         string input = "67890";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadInteger(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("67890", result.ToString());
//         // Since the entire input is consumed, a subsequent call should fail.
//         bool secondAttempt = scanner.ReadInteger(out ReadOnlySpan<char> secondResult);
//         Assert.False(secondAttempt);
//         Assert.True(secondResult.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger(out ReadOnlySpan<char>) method when the input starts with a non-digit character.
//     /// Expects the method to return false with an empty result.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_WithLeadingNonDigit_ReturnsFalse()
//     {
//         // Arrange
//         string input = "abc123";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadInteger(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger(out ReadOnlySpan<char>) method when the input buffer is empty.
//     /// Expects the method to fail and return an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_WithEmptyInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadInteger(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger() overload that does not require an out parameter.
//     /// Uses an input with digits followed by letters, expecting a successful read on the first call and failure on the subsequent call.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_WithoutOutParameter_BehavesLikeOverload()
//     {
//         // Arrange
//         string input = "98765xyz";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool firstCallResult = scanner.ReadInteger();
//         // Assert
//         Assert.True(firstCallResult);
//         // After digits are read, further call should fail as next char is not a digit.
//         bool secondCallResult = scanner.ReadInteger();
//         Assert.False(secondCallResult);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger(out ReadOnlySpan<char> result) method when the input starts with a valid integer.
//     /// Expected behavior: Returns true, outputs the integer read, and advances the cursor by the number of digits.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_WithValidInteger_ReturnsTrueAndOutputsInteger()
//     {
//         // Arrange
//         string input = "123abc";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadInteger(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("123", result.ToString());
//         // Assuming the Cursor exposes an Offset property representing the current reading position.
//         Assert.Equal(3, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger() method (without the out parameter) when the input starts with a valid integer.
//     /// Expected behavior: Returns true and advances the cursor appropriately.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_NoOutParameter_WithValidInteger_ReturnsTrueAndAdvancesCursor()
//     {
//         // Arrange
//         string input = "456def";
//         var scanner = new Scanner(input);
//         int initialOffset = scanner.Cursor.Offset;
//         // Act
//         bool success = scanner.ReadInteger();
//         // Assert
//         Assert.True(success);
//         // Verify that the cursor has advanced by the number of digit characters read (3 in this case).
//         Assert.Equal(initialOffset + 3, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger(out ReadOnlySpan<char> result) method when the input does not begin with a digit.
//     /// Expected behavior: Returns false, outputs an empty span, and does not advance the cursor.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_WithNonDigitStart_ReturnsFalseAndDoesNotAdvanceCursor()
//     {
//         // Arrange
//         string input = "abc123";
//         var scanner = new Scanner(input);
//         int initialOffset = scanner.Cursor.Offset;
//         // Act
//         bool success = scanner.ReadInteger(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty, "Expected an empty result span when no integer is read.");
//         Assert.Equal(initialOffset, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger(out ReadOnlySpan<char> result) method when given an empty string.
//     /// Expected behavior: Returns false, outputs an empty span, and leaves the cursor unadvanced.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_WithEmptyInput_ReturnsFalseAndDoesNotAdvanceCursor()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         int initialOffset = scanner.Cursor.Offset;
//         // Act
//         bool success = scanner.ReadInteger(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty, "Expected an empty result span for an empty input.");
//         Assert.Equal(initialOffset, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the ReadInteger(out ReadOnlySpan<char> result) method when the entire input is a valid integer.
//     /// Expected behavior: Returns true, outputs the entire input as the integer, and advances the cursor to the end.
//     /// </summary>
//     [Fact]
//     public void ReadInteger_FullInputInteger_ReturnsTrueAndConsumesAllCharacters()
//     {
//         // Arrange
//         string input = "7890";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadInteger(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("7890", result.ToString());
//         Assert.Equal(input.Length, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests that ReadWhile(Func<char , bool>, out ReadOnlySpan&lt;char&gt;) reads matching characters from the beginning of the buffer.
//     /// Given a buffer where the first several characters satisfy the predicate,
//     /// the method should return true and output the continuous sequence satisfying the predicate.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_WithMatchingCharacters_ReturnsTrueAndCorrectResult()
//     {
//         // Arrange
//         string buffer = "aaabbb";
//         // Predicate: returns true only for 'a'
//         Func<char, bool> predicate = c => c == 'a';
//         var scanner = new Scanner(buffer);
//         // Act
//         bool result = scanner.ReadWhile(predicate, out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         // Expect to read "aaa" until first non-'a' encountered.
//         Assert.True(token.SequenceEqual("aaa"));
//     }
// 
//     /// <summary>
//     /// Tests that ReadWhile(Func<char , bool>, out ReadOnlySpan&lt;char&gt;) returns false and an empty result
//     /// when the first character in the buffer does not satisfy the predicate.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_WithNoMatchingCharacterAtStart_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "bbbccc";
//         // Predicate: returns true only for 'a'
//         Func<char, bool> predicate = c => c == 'a';
//         var scanner = new Scanner(buffer);
//         // Act
//         bool result = scanner.ReadWhile(predicate, out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadWhile(Func<char , bool>, out ReadOnlySpan&lt;char&gt;) handles an empty buffer correctly.
//     /// Given an empty buffer, the method should return false and yield an empty result.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_WithEmptyBuffer_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string buffer = string.Empty;
//         // Predicate: any predicate (e.g., letter check)
//         Func<char, bool> predicate = char.IsLetter;
//         var scanner = new Scanner(buffer);
//         // Act
//         bool result = scanner.ReadWhile(predicate, out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadWhile(Func<char , bool>, out ReadOnlySpan&lt;char&gt;) throws an ArgumentNullException
//     /// when the provided predicate is null.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_WithNullPredicate_ThrowsArgumentNullException()
//     {
//         // Arrange
//         string buffer = "test";
//         Func<char, bool> predicate = null;
//         var scanner = new Scanner(buffer);
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => scanner.ReadWhile(predicate, out _));
//     }
// 
//     /// <summary>
//     /// Tests the overload ReadWhile(Func&lt;char, bool&gt;) that does not return the token.
//     /// Given a buffer with matching characters at the start, it should return true.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_NoOutParameter_WithMatchingCharacters_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "111222";
//         // Predicate: returns true only for '1'
//         Func<char, bool> predicate = c => c == '1';
//         var scanner = new Scanner(buffer);
//         // Act
//         bool result = scanner.ReadWhile(predicate);
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the overload ReadWhile(Func&lt;char, bool&gt;) that does not return the token.
//     /// Given a buffer whose first character does not satisfy the predicate, it should return false.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_NoOutParameter_WithNoMatchingCharacterAtStart_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "222111";
//         // Predicate: returns true only for '1'
//         Func<char, bool> predicate = c => c == '1';
//         var scanner = new Scanner(buffer);
//         // Act
//         bool result = scanner.ReadWhile(predicate);
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests that ReadWhile returns false and an empty span when the buffer is empty.
//     /// This verifies the boundary condition when no characters are available for consumption.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_EmptyBuffer_ReturnsFalseAndEmptySpan()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadWhile(c => true, out ReadOnlySpan<char> span);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, new string (span));
//     }
// 
//     /// <summary>
//     /// Tests that ReadWhile returns false and an empty span when the first character does not satisfy the predicate.
//     /// This verifies that the method correctly handles cases where no matching prefix exists.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_FirstCharDoesNotMatch_ReturnsFalseAndEmptySpan()
//     {
//         // Arrange
//         string input = "123abc";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadWhile(c => char.IsLetter(c), out ReadOnlySpan<char> span);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, new string (span));
//     }
// 
//     /// <summary>
//     /// Tests that ReadWhile returns true and reads the entire input when all characters satisfy the predicate.
//     /// This verifies the happy path scenario.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_AllCharactersMatch_ReturnsTrueAndFullSpan()
//     {
//         // Arrange
//         string input = "abcdef";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadWhile(c => char.IsLetter(c), out ReadOnlySpan<char> span);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("abcdef", new string (span));
//     }
// 
//     /// <summary>
//     /// Tests that ReadWhile returns true and reads only the matching prefix when only part of the input satisfies the predicate.
//     /// This verifies that the method stops reading upon encountering a character that does not match the predicate.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_PartialMatch_ReturnsTrueAndPartialSpan()
//     {
//         // Arrange
//         string input = "123XYZ";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadWhile(c => char.IsDigit(c), out ReadOnlySpan<char> span);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("123", new string (span));
//     }
// 
//     /// <summary>
//     /// Tests the non-out overload of ReadWhile to ensure it returns the same boolean outcome as the out overload.
//     /// This verifies that both overloads internally perform the same matching logic.
//     /// </summary>
//     [Fact]
//     public void ReadWhile_NonOutOverload_ReturnsSameBooleanAsOutOverload()
//     {
//         // Arrange
//         string input = "hello123";
//         var scanner = new Scanner(input);
//         // Act using the out overload
//         bool resultWithOut = scanner.ReadWhile(c => char.IsLetter(c), out ReadOnlySpan<char> spanOut);
//         // Reinitialize scanner since the previous call advances its internal cursor.
//         scanner = new Scanner(input);
//         bool resultWithoutOut = scanner.ReadWhile(c => char.IsLetter(c));
//         // Assert
//         Assert.True(resultWithOut);
//         Assert.True(resultWithoutOut);
//         Assert.Equal("hello", new string (spanOut));
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpace(out ReadOnlySpan<char> result) returns true and extracts a token when the input starts with non-white-space characters.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_WithNonWhiteSpace_StartsTokenExtraction()
//     {
//         // Arrange
//         string input = "abc def";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result, "Expected ReadNonWhiteSpace to return true when input starts with non-white-space characters.");
//         Assert.Equal("abc", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpace(out ReadOnlySpan<char> result) returns false and an empty token when the input starts with white-space characters.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_WithLeadingWhiteSpace_ReturnsFalseAndEmptyToken()
//     {
//         // Arrange
//         string input = "   abc";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result, "Expected ReadNonWhiteSpace to return false when input starts with white-space characters.");
//         Assert.True(token.IsEmpty, "Expected token to be empty when no non-white-space characters are read at the beginning.");
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpace(out ReadOnlySpan<char> result) returns false and an empty token when the input is empty.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_WithEmptyInput_ReturnsFalseAndEmptyToken()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result, "Expected ReadNonWhiteSpace to return false for an empty input.");
//         Assert.True(token.IsEmpty, "Expected token to be empty for an empty input.");
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadNonWhiteSpace method returns true when non-white-space characters are present.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_Parameterless_WithNonWhiteSpace_ReturnsTrue()
//     {
//         // Arrange
//         string input = "xyz ";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace();
//         // Assert
//         Assert.True(result, "Expected parameterless ReadNonWhiteSpace to return true when non-white-space characters are present.");
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadNonWhiteSpace method returns false when the input starts with white-space characters.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_Parameterless_WithLeadingWhiteSpace_ReturnsFalse()
//     {
//         // Arrange
//         string input = "  ";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace();
//         // Assert
//         Assert.False(result, "Expected parameterless ReadNonWhiteSpace to return false when input starts with white-space.");
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpace(out ReadOnlySpan<char> result) correctly reads a single non-white-space character token.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_SingleCharacterToken_ReturnsToken()
//     {
//         // Arrange
//         string input = "a ";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result, "Expected ReadNonWhiteSpace to return true for a single non-white-space character.");
//         Assert.Equal("a", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpace(out ReadOnlySpan<char>) returns true and outputs the correct token when the buffer starts with non-white space characters.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_WithLeadingNonWhitespace_ReturnsToken()
//     {
//         // Arrange
//         string input = "Hello World";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("Hello", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpace(out ReadOnlySpan<char>) returns false and outputs an empty token when the buffer starts with white space.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_WithLeadingWhiteSpace_ReturnsFalse()
//     {
//         // Arrange
//         string input = "   LeadingSpace";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty, "Expected token to be empty since the buffer starts with white space.");
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpace(out ReadOnlySpan<char>) returns false and outputs an empty token when the buffer is empty.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_WithEmptyBuffer_ReturnsFalse()
//     {
//         // Arrange
//         string input = string.Empty;
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty, "Expected token to be empty for an empty buffer.");
//     }
// 
//     /// <summary>
//     /// Tests that the overload ReadNonWhiteSpace() without an out parameter returns true when non-white space token is read.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpace_NoOutParameter_WithLeadingNonWhitespace_ReturnsTrue()
//     {
//         // Arrange
//         string input = "SampleText remaining";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpace();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadNonWhiteSpaceOrNewLine method returns true when the buffer starts with a non-whitespace and non-newline character.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_NoOutParam_WithTokenPresent_ReturnsTrue()
//     {
//         // Arrange
//         // Assuming that tokens are read until a whitespace is encountered.
//         // "abc def" should yield a token "abc"
//         string input = "abc def";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadNonWhiteSpaceOrNewLine();
//         // Assert
//         // Expect true because the first character 'a' is not whitespace or newline.
//         Assert.True(success);
//     }
// 
//     /// <summary>
//     /// Tests that the overload ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan{char}) returns the correct token when the buffer starts with a non-whitespace character.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_WithOutParam_WithTokenPresent_ReturnsExpectedToken()
//     {
//         // Arrange
//         // "hello world" should yield "hello" as the token.
//         string input = "hello world";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("hello", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpaceOrNewLine returns false and outputs an empty token when the buffer starts with a whitespace.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_WithLeadingWhiteSpace_ReturnsFalseAndEmptyToken()
//     {
//         // Arrange
//         // Input starting with spaces should not form a token.
//         string input = "  hello";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpaceOrNewLine returns false and outputs an empty token when the buffer starts with a newline character.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_WithLeadingNewLine_ReturnsFalseAndEmptyToken()
//     {
//         // Arrange
//         // Input starting with a newline should result in failure to read a non-whitespace token.
//         string input = "\nhello";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpaceOrNewLine returns false and outputs an empty token when the buffer is empty.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_WithEmptyBuffer_ReturnsFalseAndEmptyToken()
//     {
//         // Arrange
//         string input = "";
//         Scanner scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> result) returns a contiguous token when the input starts with non-whitespace characters.
//     /// Expected outcome: The method returns true and the token contains the expected non-whitespace sequence.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_WithNonWhitespaceInput_ReturnsToken()
//     {
//         // Arrange
//         string input = "abc def";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result, "Expected the method to return true when a non-whitespace token is present.");
//         Assert.Equal("abc", new string (token));
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> result) returns false when the input starts with a whitespace character.
//     /// Expected outcome: The method returns false and the token is empty.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_WithLeadingWhitespace_ReturnsFalse()
//     {
//         // Arrange
//         string input = " def";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result, "Expected the method to return false when the input starts with whitespace.");
//         Assert.Equal(0, token.Length);
//     }
// 
//     /// <summary>
//     /// Tests that ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> result) returns false when the input is empty.
//     /// Expected outcome: The method returns false and the token is empty.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_WithEmptyInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpaceOrNewLine(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result, "Expected the method to return false when the input is empty.");
//         Assert.Equal(0, token.Length);
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadNonWhiteSpaceOrNewLine() method returns true when the input begins with a non-whitespace token.
//     /// Expected outcome: The method returns true even though the token is not captured.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_Parameterless_WithNonWhitespaceInput_ReturnsTrue()
//     {
//         // Arrange
//         string input = "xyz\nmore";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpaceOrNewLine();
//         // Assert
//         Assert.True(result, "Expected the parameterless method to return true when a token is present.");
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadNonWhiteSpaceOrNewLine() method returns false when the input starts with a whitespace.
//     /// Expected outcome: The method returns false.
//     /// </summary>
//     [Fact]
//     public void ReadNonWhiteSpaceOrNewLine_Parameterless_WithLeadingWhitespace_ReturnsFalse()
//     {
//         // Arrange
//         string input = " leadingToken";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadNonWhiteSpaceOrNewLine();
//         // Assert
//         Assert.False(result, "Expected the parameterless method to return false when input starts with whitespace.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char) method when the current character matches the expected character.
//     /// Expected outcome: The method returns true and advances the cursor.
//     /// </summary>
//     [Fact] [Error] (1848-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?)
//     public void ReadChar_SingleChar_MatchingChar_ReturnsTrueAndAdvancesCursor()
//     {
//         // Arrange
//         string input = "a";
//         var scanner = new Scanner(input);
//         var fakeCursor = new FakeCursor(input);
//         // Inject fake cursor into scanner using reflection.
//         FieldInfo cursorField = typeof(Scanner).GetField("Cursor", BindingFlags.Public | BindingFlags.Instance);
//         cursorField.SetValue(scanner, fakeCursor);
//         // Act
//         bool result = scanner.ReadChar('a');
//         // Assert
//         Assert.True(result);
//         Assert.True(fakeCursor.AdvanceCalled);
//         Assert.Equal(1, fakeCursor.Position);
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char) method when the current character does not match the expected character.
//     /// Expected outcome: The method returns false and does not advance the cursor.
//     /// </summary>
//     [Fact] [Error] (1870-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?)
//     public void ReadChar_SingleChar_NonMatchingChar_ReturnsFalseAndDoesNotAdvanceCursor()
//     {
//         // Arrange
//         string input = "b";
//         var scanner = new Scanner(input);
//         var fakeCursor = new FakeCursor(input);
//         FieldInfo cursorField = typeof(Scanner).GetField("Cursor", BindingFlags.Public | BindingFlags.Instance);
//         cursorField.SetValue(scanner, fakeCursor);
//         // Act
//         bool result = scanner.ReadChar('a');
//         // Assert
//         Assert.False(result);
//         Assert.False(fakeCursor.AdvanceCalled);
//         Assert.Equal(0, fakeCursor.Position);
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char, out ReadOnlySpan<char>) method when the current character matches the expected character.
//     /// Expected outcome: The method returns true, outputs a span containing the matched character, and advances the cursor.
//     /// </summary>
//     [Fact] [Error] (1891-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?)
//     public void ReadChar_WithOutParameter_MatchingChar_ReturnsTrueAndOutputsCharAndAdvancesCursor()
//     {
//         // Arrange
//         string input = "a";
//         var scanner = new Scanner(input);
//         var fakeCursor = new FakeCursor(input);
//         FieldInfo cursorField = typeof(Scanner).GetField("Cursor", BindingFlags.Public | BindingFlags.Instance);
//         cursorField.SetValue(scanner, fakeCursor);
//         ReadOnlySpan<char> resultSpan;
//         // Act
//         bool result = scanner.ReadChar('a', out resultSpan);
//         // Assert
//         Assert.True(result);
//         Assert.True(fakeCursor.AdvanceCalled);
//         Assert.Equal(1, fakeCursor.Position);
//         Assert.Equal("a", new string (resultSpan));
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char, out ReadOnlySpan<char>) method when the current character does not match the expected character.
//     /// Expected outcome: The method returns false, outputs an empty span, and does not advance the cursor.
//     /// </summary>
//     [Fact] [Error] (1914-30)CS0246 The type or namespace name 'FakeCursor' could not be found (are you missing a using directive or an assembly reference?)
//     public void ReadChar_WithOutParameter_NonMatchingChar_ReturnsFalseAndOutputsEmptySpanAndDoesNotAdvanceCursor()
//     {
//         // Arrange
//         string input = "b";
//         var scanner = new Scanner(input);
//         var fakeCursor = new FakeCursor(input);
//         FieldInfo cursorField = typeof(Scanner).GetField("Cursor", BindingFlags.Public | BindingFlags.Instance);
//         cursorField.SetValue(scanner, fakeCursor);
//         ReadOnlySpan<char> resultSpan;
//         // Act
//         bool result = scanner.ReadChar('a', out resultSpan);
//         // Assert
//         Assert.False(result);
//         Assert.False(fakeCursor.AdvanceCalled);
//         Assert.Equal(0, fakeCursor.Position);
//         Assert.True(resultSpan.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char, out ReadOnlySpan<char> result) method when the expected character is at the current cursor position.
//     /// Expected outcome: The method returns true, output result equals the matched character, and the cursor advances.
//     /// </summary>
//     [Fact]
//     public void ReadChar_WithMatchingCharacter_ReturnsTrueAndOutputSpan()
//     {
//         // Arrange
//         string input = "abc";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadChar('a', out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("a", output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char, out ReadOnlySpan<char> result) method when the expected character does not match the character at the current cursor position.
//     /// Expected outcome: The method returns false and the output span is empty.
//     /// </summary>
//     [Fact]
//     public void ReadChar_WithNonMatchingCharacter_ReturnsFalseAndEmptySpan()
//     {
//         // Arrange
//         string input = "abc";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadChar('x', out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(result);
//         Assert.True(output.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char, out ReadOnlySpan<char> result) method when the scanner has an empty buffer.
//     /// Expected outcome: The method returns false and the output span is empty.
//     /// </summary>
//     [Fact]
//     public void ReadChar_WithEmptyBuffer_ReturnsFalseAndEmptySpan()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadChar('a', out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(result);
//         Assert.True(output.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that the cursor position advances after a successful ReadChar invocation.
//     /// Expected outcome: After reading a matching character, subsequent call reads the next character.
//     /// </summary>
//     [Fact]
//     public void ReadChar_AdvancesCursorPositionProperly()
//     {
//         // Arrange
//         string input = "ab";
//         var scanner = new Scanner(input);
//         // Act
//         bool firstResult = scanner.ReadChar('a', out ReadOnlySpan<char> firstOutput);
//         bool secondResult = scanner.ReadChar('b', out ReadOnlySpan<char> secondOutput);
//         bool thirdResult = scanner.ReadChar('c', out ReadOnlySpan<char> thirdOutput);
//         // Assert
//         Assert.True(firstResult);
//         Assert.Equal("a", firstOutput.ToString());
//         Assert.True(secondResult);
//         Assert.Equal("b", secondOutput.ToString());
//         // Expect false since the scanner has only two chars and third read attempt should fail.
//         Assert.False(thirdResult);
//         Assert.True(thirdOutput.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char) overload (without out parameter) when the expected character matches.
//     /// Expected outcome: The method returns true.
//     /// </summary>
//     [Fact]
//     public void ReadChar_NonOutOverload_WithMatchingCharacter_ReturnsTrue()
//     {
//         // Arrange
//         string input = "z";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadChar('z');
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the ReadChar(char) overload (without out parameter) when the expected character does not match.
//     /// Expected outcome: The method returns false.
//     /// </summary>
//     [Fact]
//     public void ReadChar_NonOutOverload_WithNonMatchingCharacter_ReturnsFalse()
//     {
//         // Arrange
//         string input = "z";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadChar('a');
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, StringComparison comparisonType, out ReadOnlySpan<char> result)
//     /// method when the expected text matches exactly and verifies cursor advancement.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithComparison_MatchingText_ReturnsTrueAndAdvancesCursor()
//     {
//         // Arrange
//         string buffer = "HelloWorld";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> result;
//         // Act
//         bool firstCall = scanner.ReadText("Hello".AsSpan(), StringComparison.Ordinal, out result);
//         bool secondCall = scanner.ReadText("World".AsSpan(), StringComparison.Ordinal, out var result2);
//         bool thirdCall = scanner.ReadText("!".AsSpan(), StringComparison.Ordinal, out var result3);
//         // Assert
//         Assert.True(firstCall);
//         Assert.Equal("Hello", result.ToString());
//         Assert.True(secondCall);
//         Assert.Equal("World", result2.ToString());
//         Assert.False(thirdCall);
//         Assert.Equal(string.Empty, result3.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, StringComparison comparisonType)
//     /// method when the expected text does not match the beginning of the buffer.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithComparison_NonMatchingText_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "Parlot";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool result = scanner.ReadText("XYZ".AsSpan(), StringComparison.Ordinal);
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text) method which uses an Ordinal comparison by default,
//     /// ensuring that it returns true when the expected text is found.
//     /// </summary>
//     [Fact]
//     public void ReadText_DefaultComparison_MatchingText_ReturnsTrueAndAdvancesCursor()
//     {
//         // Arrange
//         string buffer = "TestBuffer";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> result;
//         // Act
//         bool firstCall = scanner.ReadText("Test".AsSpan(), out result);
//         bool secondCall = scanner.ReadText("Buffer".AsSpan(), out var result2);
//         // Assert
//         Assert.True(firstCall);
//         Assert.Equal("Test", result.ToString());
//         Assert.True(secondCall);
//         Assert.Equal("Buffer", result2.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, out ReadOnlySpan<char> result)
//     /// method when the provided expected text does not match, ensuring it returns false and an empty result.
//     /// </summary>
//     [Fact]
//     public void ReadText_OutParameter_NonMatchingText_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "SampleText";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> result;
//         // Act
//         bool callResult = scanner.ReadText("NotSample".AsSpan(), out result);
//         // Assert
//         Assert.False(callResult);
//         Assert.Equal(string.Empty, result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that providing an empty expected text to the ReadText methods returns true with an empty result,
//     /// and does not advance the cursor.
//     /// </summary>
//     [Fact]
//     public void ReadText_EmptyExpectedText_ReturnsTrueAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "Data";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> result;
//         // Act
//         bool callResultOut = scanner.ReadText("".AsSpan(), out result);
//         bool callResultComparison = scanner.ReadText("".AsSpan(), StringComparison.Ordinal, out var result2);
//         // Assert
//         Assert.True(callResultOut);
//         Assert.Equal(string.Empty, result.ToString());
//         Assert.True(callResultComparison);
//         Assert.Equal(string.Empty, result2.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadText method with a case-insensitive comparison to confirm that matching occurs regardless of case.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithIgnoreCase_MatchingText_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "caseTest";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> result;
//         // Act
//         bool callResult = scanner.ReadText("CASE".AsSpan(), StringComparison.OrdinalIgnoreCase, out result);
//         // Assert
//         Assert.True(callResult);
//         Assert.Equal("CASE", result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that subsequent calls to the ReadText methods properly reflect the advanced cursor position.
//     /// </summary>
//     [Fact]
//     public void ReadText_SubsequentCalls_ReflectCursorAdvance()
//     {
//         // Arrange
//         string buffer = "12345";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> result;
//         // Act & Assert
//         bool first = scanner.ReadText("12".AsSpan(), out result);
//         Assert.True(first);
//         Assert.Equal("12", result.ToString());
//         bool second = scanner.ReadText("345".AsSpan(), out var result2);
//         Assert.True(second);
//         Assert.Equal("345", result2.ToString());
//         bool third = scanner.ReadText("0".AsSpan(), out var result3);
//         Assert.False(third);
//         Assert.Equal(string.Empty, result3.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadText with a StringComparison parameter returns true and the expected text when the buffer begins with the expected text.
//     /// This verifies that the method correctly identifies matching text and advances the cursor.
//     /// </summary>
//     [Fact] [Error] (2178-17)CS0111 Type 'ScannerTests' already defines a member called 'ReadText_WithComparison_MatchingText_ReturnsTrueAndAdvancesCursor' with the same parameter types
//     public void ReadText_WithComparison_MatchingText_ReturnsTrueAndAdvancesCursor()
//     {
//         // Arrange
//         string buffer = "Hello World";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> expectedText = "Hello".AsSpan();
//         // Act
//         bool success = scanner.ReadText(expectedText, StringComparison.Ordinal, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("Hello", new string (result));
//     }
// 
//     /// <summary>
//     /// Tests that ReadText with a StringComparison parameter returns false and an empty result when the expected text is not at the current cursor position.
//     /// This verifies that the method does not advance the cursor on failure.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithComparison_NonMatchingText_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "Hello World";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> nonMatchingText = "Bye".AsSpan();
//         // Act
//         bool success = scanner.ReadText(nonMatchingText, StringComparison.Ordinal, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadText without specifying a StringComparison returns true and the expected text when the buffer exactly matches the expected text.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithoutComparison_MatchingText_ReturnsTrueAndAdvancesCursor()
//     {
//         // Arrange
//         string buffer = "Test";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> expectedText = "Test".AsSpan();
//         // Act
//         bool success = scanner.ReadText(expectedText, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("Test", new string (result));
//     }
// 
//     /// <summary>
//     /// Tests that ReadText without specifying a StringComparison returns false and an empty result when the expected text does not match the buffer content.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithoutComparison_NonMatchingText_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "Test";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> nonMatchingText = "Demo".AsSpan();
//         // Act
//         bool success = scanner.ReadText(nonMatchingText, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadText with a StringComparison parameter correctly handles an empty expected text by returning true and an empty result.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithComparison_EmptyExpectedText_ReturnsTrueAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "Any text";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> emptyText = "".AsSpan();
//         // Act
//         bool success = scanner.ReadText(emptyText, StringComparison.Ordinal, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(string.Empty, new string (result));
//     }
// 
//     /// <summary>
//     /// Tests that ReadText without specifying a StringComparison returns false and an empty result when the buffer is empty but the expected text is non-empty.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithoutComparison_EmptyBufferNonEmptyExpectedText_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> expectedText = "NonEmpty".AsSpan();
//         // Act
//         bool success = scanner.ReadText(expectedText, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//     }
// 
// #region Obsolete Overload: ReadAnyOf(ReadOnlySpan<char>, StringComparison, out ReadOnlySpan<char>)
//     /// <summary>
//     /// Tests that the obsolete ReadAnyOf method returns true, advances the cursor, and sets the result when the current character is found in the provided span.
//     /// </summary>
//     [Fact] [Error] (2291-24)CS0618 'Scanner.ReadAnyOf(ReadOnlySpan<char>, StringComparison, out ReadOnlySpan<char>)' is obsolete: 'Prefer bool ReadAnyOf(ReadOnlySpan<char>, out ReadOnlySpan<char>)'
//     public void ReadAnyOf_WithMatchingCharacterUsingComparison_ReturnsTrueAndAdvancesCursor()
//     {
//         // Arrange
//         // Buffer starts with 'a'. The provided span contains 'abc' so 'a' is found at index 0.
//         string input = "aXYZ";
//         var scanner = new Scanner(input);
//         int initialOffset = scanner.Cursor.Offset;
//         ReadOnlySpan<char> expectedResult = input.AsSpan(0, 1);
//         // Act
//         bool success = scanner.ReadAnyOf("abc".AsSpan(), StringComparison.Ordinal, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(expectedResult.ToString(), result.ToString());
//         Assert.Equal(initialOffset + 1, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests that the obsolete ReadAnyOf method returns false, does not advance the cursor, and returns an empty result when the current character is not in the provided span.
//     /// </summary>
//     [Fact] [Error] (2310-24)CS0618 'Scanner.ReadAnyOf(ReadOnlySpan<char>, StringComparison, out ReadOnlySpan<char>)' is obsolete: 'Prefer bool ReadAnyOf(ReadOnlySpan<char>, out ReadOnlySpan<char>)'
//     public void ReadAnyOf_WithNonMatchingCharacterUsingComparison_ReturnsFalseAndDoesNotAdvanceCursor()
//     {
//         // Arrange
//         // Buffer starts with 'x' which is not present in "abc".
//         string input = "xYZ";
//         var scanner = new Scanner(input);
//         int initialOffset = scanner.Cursor.Offset;
//         // Act
//         bool success = scanner.ReadAnyOf("abc".AsSpan(), StringComparison.Ordinal, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//         Assert.Equal(initialOffset, scanner.Cursor.Offset);
//     }
// 
// #endregion
// #region Overload: ReadAnyOf(ReadOnlySpan<char>, out ReadOnlySpan<char>)
//     /// <summary>
//     /// Tests that the non-obsolete ReadAnyOf overload returns true, advances the cursor, and sets the result when the current character is contained in the provided span.
//     /// </summary>
//     [Fact]
//     public void ReadAnyOf_Span_WithMatchingCharacter_ReturnsTrueAndAdvancesCursor()
//     {
//         // Arrange
//         // Buffer starts with 'b'. The provided span "bcd" contains 'b'.
//         string input = "bHello";
//         var scanner = new Scanner(input);
//         int initialOffset = scanner.Cursor.Offset;
//         ReadOnlySpan<char> expectedResult = input.AsSpan(0, 1);
//         // Act
//         bool success = scanner.ReadAnyOf("bcd".AsSpan(), out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(expectedResult.ToString(), result.ToString());
//         Assert.Equal(initialOffset + 1, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests that the non-obsolete ReadAnyOf overload returns false, does not advance the cursor, and returns an empty result when the current character is not contained in the provided span.
//     /// </summary>
//     [Fact]
//     public void ReadAnyOf_Span_WithNonMatchingCharacter_ReturnsFalseAndDoesNotAdvanceCursor()
//     {
//         // Arrange
//         // Buffer starts with 'z'. The provided span "abc" does not contain 'z'.
//         string input = "zTest";
//         var scanner = new Scanner(input);
//         int initialOffset = scanner.Cursor.Offset;
//         // Act
//         bool success = scanner.ReadAnyOf("abc".AsSpan(), out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//         Assert.Equal(initialOffset, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the ReadAnyOf(ReadOnlySpan<char>, out ReadOnlySpan<char>) method when the initial sequence of characters
//     /// matches the allowed set until a non-allowed character is reached.
//     /// Buffer: "aaaX", Allowed set: "a".
//     /// Expected: Method returns true, extracted span is "aaa", and the cursor offset is advanced by 3.
//     /// </summary>
//     [Fact]
//     public void ReadAnyOf_ReadOnlySpan_MatchingSequence_ReturnsExtractedSpan()
//     {
//         // Arrange
//         string buffer = "aaaX";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> allowedChars = "a".AsSpan();
//         // Act
//         bool success = scanner.ReadAnyOf(allowedChars, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("aaa", result.ToString());
//         // The cursor should have advanced past the matched characters.
//         Assert.Equal(3, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the ReadAnyOf(ReadOnlySpan<char>, out ReadOnlySpan<char>) method when the first character
//     /// does not match the allowed set.
//     /// Buffer: "Xaaa", Allowed set: "a".
//     /// Expected: Method returns false, extracted span is empty, and the cursor offset remains unchanged.
//     /// </summary>
//     [Fact]
//     public void ReadAnyOf_ReadOnlySpan_NonMatchingFirstCharacter_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "Xaaa";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> allowedChars = "a".AsSpan();
//         int initialOffset = scanner.Cursor.Offset;
//         // Act
//         bool success = scanner.ReadAnyOf(allowedChars, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//         Assert.Equal(initialOffset, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the ReadAnyOf(ReadOnlySpan<char>, out ReadOnlySpan<char>) method when the entire buffer
//     /// consists of allowed characters, leading to an EOF condition.
//     /// Buffer: "aaa", Allowed set: "a".
//     /// Expected: Method returns false due to EOF, extracted span is empty, and the cursor offset equals the buffer's length.
//     /// </summary>
//     [Fact]
//     public void ReadAnyOf_ReadOnlySpan_AllCharactersMatch_EOF_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "aaa";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> allowedChars = "a".AsSpan();
//         // Act
//         bool success = scanner.ReadAnyOf(allowedChars, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//         Assert.Equal(buffer.Length, scanner.Cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text) overload when the expected text matches exactly at the start.
//     /// Expected outcome: The method returns true.
//     /// </summary>
//     [Fact]
//     public void ReadText_NoOut_WithExactMatch_ReturnsTrue()
//     {
//         // Arrange
//         var buffer = "hello world";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("hello".AsSpan());
//         // Assert
//         Assert.True(success, "Expected ReadText to return true when the beginning of the buffer matches the input text.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text) overload when the expected text does not match.
//     /// Expected outcome: The method returns false.
//     /// </summary>
//     [Fact]
//     public void ReadText_NoOut_WithNonMatch_ReturnsFalse()
//     {
//         // Arrange
//         var buffer = "hello world";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("world".AsSpan());
//         // Assert
//         Assert.False(success, "Expected ReadText to return false when the beginning of the buffer does not match the input text.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, out ReadOnlySpan<char> result) overload when the expected text matches.
//     /// Expected outcome: The method returns true and sets the out parameter to the matched text.
//     /// </summary>
//     [Fact]
//     public void ReadText_Out_WithExactMatch_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         var buffer = "hello world";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("hello".AsSpan(), out ReadOnlySpan<char> readSpan);
//         // Assert
//         Assert.True(success, "Expected ReadText to return true when the input text matches the beginning of the buffer.");
//         Assert.Equal("hello", readSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, out ReadOnlySpan<char> result) overload when the expected text does not match.
//     /// Expected outcome: The method returns false and the out parameter is an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadText_Out_WithNonMatch_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         var buffer = "hello world";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("world".AsSpan(), out ReadOnlySpan<char> readSpan);
//         // Assert
//         Assert.False(success, "Expected ReadText to return false when the input text does not match the beginning of the buffer.");
//         Assert.True(readSpan.IsEmpty, "Expected the result span to be empty when the text does not match.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, StringComparison comparisonType, out ReadOnlySpan<char> result) overload
//     /// when a case-insensitive match is expected.
//     /// Expected outcome: The method returns true and outputs the text in its original case.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithComparison_Out_WithExactMatch_ReturnsTrueAndSetsResult()
//     {
//         // Arrange
//         var buffer = "HeLLo world";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("hello".AsSpan(), StringComparison.OrdinalIgnoreCase, out ReadOnlySpan<char> readSpan);
//         // Assert
//         Assert.True(success, "Expected ReadText with case-insensitive comparison to return true for a matching text.");
//         // Verify that the returned span has the length equal to the expected part of the buffer.
//         Assert.Equal(5, readSpan.Length);
//         Assert.Equal("HeLLo", readSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, StringComparison comparisonType, out ReadOnlySpan<char> result) overload
//     /// when a case-sensitive mismatch occurs.
//     /// Expected outcome: The method returns false and produces an empty result.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithComparison_Out_WithNonMatch_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         var buffer = "Hello world";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("hello".AsSpan(), StringComparison.Ordinal, out ReadOnlySpan<char> readSpan);
//         // Assert
//         Assert.False(success, "Expected ReadText with case-sensitive comparison to return false when the case does not match.");
//         Assert.True(readSpan.IsEmpty, "Expected the result span to be empty when the text does not match.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, StringComparison comparisonType) overload when the expected text matches.
//     /// Expected outcome: The method returns true.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithComparison_NoOut_WithExactMatch_ReturnsTrue()
//     {
//         // Arrange
//         var buffer = "TestTextRest";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("TestText".AsSpan(), StringComparison.Ordinal);
//         // Assert
//         Assert.True(success, "Expected ReadText with comparison to return true when the text matches exactly.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, StringComparison comparisonType) overload when the expected text does not match.
//     /// Expected outcome: The method returns false.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithComparison_NoOut_WithNonMatch_ReturnsFalse()
//     {
//         // Arrange
//         var buffer = "TestTextRest";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("WrongText".AsSpan(), StringComparison.Ordinal);
//         // Assert
//         Assert.False(success, "Expected ReadText with comparison to return false when the text does not match.");
//     }
// 
//     /// <summary>
//     /// Tests that after a successful ReadText call with out parameter, the Scanner advances its cursor,
//     /// preventing the same text from being re-read.
//     /// Expected outcome: Subsequent ReadText calls read the following parts of the buffer.
//     /// </summary>
//     [Fact]
//     public void ReadText_Out_AfterSuccessfulCall_BufferIsAdvanced()
//     {
//         // Arrange
//         var buffer = "hello world";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool firstSuccess = scanner.ReadText("hello".AsSpan(), out ReadOnlySpan<char> firstRead);
//         bool secondSuccess = scanner.ReadText(" world".AsSpan(), out ReadOnlySpan<char> secondRead);
//         // Assert
//         Assert.True(firstSuccess, "Expected the first ReadText call to succeed.");
//         Assert.Equal("hello", firstRead.ToString());
//         Assert.True(secondSuccess, "Expected the second ReadText call to succeed after advancing the cursor.");
//         Assert.Equal(" world", secondRead.ToString());
//     }
// 
//     /// <summary>
//     /// Tests ReadText(ReadOnlySpan<char> text, StringComparison comparisonType, out ReadOnlySpan<char> result) when the expected text matches the beginning of the buffer.
//     /// Expected to return true and output the matching text.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithMatchingTextAndComparison_ReturnsTrueAndOutputMatches()
//     {
//         // Arrange
//         string buffer = "Hello, world!";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> expected = "Hello".AsSpan();
//         // Act
//         bool success = scanner.ReadText("Hello".AsSpan(), StringComparison.Ordinal, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(expected.ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests ReadText(ReadOnlySpan<char> text, StringComparison comparisonType, out ReadOnlySpan<char> result) when the expected text does not match the beginning of the buffer.
//     /// Expected to return false and output an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithNonMatchingTextAndComparison_ReturnsFalseAndEmptyOutput()
//     {
//         // Arrange
//         string buffer = "Hello, world!";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("World".AsSpan(), StringComparison.Ordinal, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(0, result.Length);
//     }
// 
//     /// <summary>
//     /// Tests the default ReadText(ReadOnlySpan<char> text) overload when the expected text matches the beginning of the buffer using ordinal comparison.
//     /// Expected to return true.
//     /// </summary>
//     [Fact]
//     public void ReadText_DefaultOverload_WithMatchingText_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "TestString";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("Test".AsSpan());
//         // Assert
//         Assert.True(success);
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, out ReadOnlySpan<char> result) overload when the expected text matches the beginning of the buffer.
//     /// Expected to return true and output the matching text.
//     /// </summary>
//     [Fact]
//     public void ReadText_OutParameterOverload_WithMatchingText_ReturnsTrueAndOutputMatches()
//     {
//         // Arrange
//         string buffer = "SampleData";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> expected = "Sample".AsSpan();
//         // Act
//         bool success = scanner.ReadText("Sample".AsSpan(), out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(expected.ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadText(ReadOnlySpan<char> text, out ReadOnlySpan<char> result) overload when the expected text does not match the beginning of the buffer.
//     /// Expected to return false and output an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadText_OutParameterOverload_WithNonMatchingText_ReturnsFalseAndEmptyOutput()
//     {
//         // Arrange
//         string buffer = "AnotherTest";
//         var scanner = new Scanner(buffer);
//         // Act
//         bool success = scanner.ReadText("Mismatch".AsSpan(), out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(0, result.Length);
//     }
// 
//     /// <summary>
//     /// Tests ReadText methods when provided with an empty text.
//     /// Expected to return true and output an empty span since there is nothing to match.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithEmptyText_ReturnsTrueAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "NonEmptyBuffer";
//         var scanner = new Scanner(buffer);
//         // Act & Assert for overload with comparison
//         bool success1 = scanner.ReadText(ReadOnlySpan<char>.Empty, StringComparison.Ordinal, out ReadOnlySpan<char> result1);
//         Assert.True(success1);
//         Assert.Equal(0, result1.Length);
//         // Act & Assert for default overload without out parameter
//         bool success2 = scanner.ReadText(ReadOnlySpan<char>.Empty);
//         Assert.True(success2);
//         // Act & Assert for overload with out parameter using default comparison
//         bool success3 = scanner.ReadText(ReadOnlySpan<char>.Empty, out ReadOnlySpan<char> result3);
//         Assert.True(success3);
//         Assert.Equal(0, result3.Length);
//     }
// 
//     /// <summary>
//     /// Tests ReadText methods when the provided text is longer than the available buffer.
//     /// Expected to return false and output an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadText_WithTextLongerThanBuffer_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string buffer = "Short";
//         var scanner = new Scanner(buffer);
//         ReadOnlySpan<char> longText = "ThisTextIsLongerThanBuffer".AsSpan();
//         // Act & Assert for overload with comparison
//         bool success1 = scanner.ReadText(longText, StringComparison.Ordinal, out ReadOnlySpan<char> result1);
//         Assert.False(success1);
//         Assert.Equal(0, result1.Length);
//         // Act & Assert for overload with out parameter using default comparison
//         bool success2 = scanner.ReadText(longText, out ReadOnlySpan<char> result2);
//         Assert.False(success2);
//         Assert.Equal(0, result2.Length);
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString(out ReadOnlySpan&lt;char&gt;) returns true and outputs the correct token 
//     /// when a valid single-quoted string is provided.
//     /// </summary>
//     /// <param name = "input">The input buffer containing a valid single-quoted string.</param>
//     /// <param name = "expected">The expected token including the surrounding single quotes.</param>
//     [Theory]
//     [InlineData("'hello'", "'hello'")]
//     [InlineData("''", "''")]
//     [InlineData("'a'", "'a'")]
//     public void ReadSingleQuotedString_WithValidInput_ReturnsTrueAndCorrectToken(string input, string expected)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(expected, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString(out ReadOnlySpan&lt;char&gt;) returns false and outputs an empty token 
//     /// when the input is missing a closing single quote.
//     /// </summary>
//     /// <param name = "input">The input buffer missing the closing quote.</param>
//     [Theory]
//     [InlineData("'hello")]
//     [InlineData("'test")]
//     public void ReadSingleQuotedString_MissingClosingQuote_ReturnsFalseAndEmptyToken(string input)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString(out ReadOnlySpan&lt;char&gt;) returns false and outputs an empty token 
//     /// when the input does not begin with a single quote.
//     /// </summary>
//     /// <param name = "input">The input buffer that does not start with a single quote.</param>
//     [Theory]
//     [InlineData("hello'")]
//     [InlineData("noquote")]
//     public void ReadSingleQuotedString_InputDoesNotStartWithQuote_ReturnsFalseAndEmptyToken(string input)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the overload ReadSingleQuotedString() (without an out parameter) returns true when provided 
//     /// with a valid single-quoted string.
//     /// </summary>
//     /// <param name = "input">The input buffer containing a valid single-quoted string.</param>
//     [Theory]
//     [InlineData("'valid'")]
//     [InlineData("'example'")]
//     public void ReadSingleQuotedString_OverloadWithoutOutParameter_ValidInput_ReturnsTrue(string input)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that the overload ReadSingleQuotedString() (without an out parameter) returns false when provided 
//     /// with an invalid input.
//     /// </summary>
//     /// <param name = "input">The input buffer that does not represent a valid single-quoted string.</param>
//     [Theory]
//     [InlineData("invalid")]
//     [InlineData("'invalid")]
//     public void ReadSingleQuotedString_OverloadWithoutOutParameter_InvalidInput_ReturnsFalse(string input)
//     {
//         // Arrange
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString();
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString returns true and correctly outputs an empty quoted string when the input is two single quotes.
//     /// </summary>
//     [Fact]
//     public void ReadSingleQuotedString_EmptyQuotes_ReturnsQuotedEmptyString()
//     {
//         // Arrange
//         string input = "''";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("''", output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString returns true and correctly outputs the quoted string when the input starts with a valid single quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadSingleQuotedString_ValidQuotedString_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "'hello world'";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(result);
//         Assert.Equal("'hello world'", output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString (overload without out parameter) returns true when a valid single quoted string is present.
//     /// </summary>
//     [Fact]
//     public void ReadSingleQuotedString_ValidQuotedStringWithoutOutParameter_ReturnsTrue()
//     {
//         // Arrange
//         string input = "'test'";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString returns false when the input does not start with a single quote.
//     /// </summary>
//     [Fact]
//     public void ReadSingleQuotedString_InputWithoutStartingQuote_ReturnsFalse()
//     {
//         // Arrange
//         string input = "no quotes here";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString returns false when a starting quote is present but the closing single quote is missing.
//     /// </summary>
//     [Fact]
//     public void ReadSingleQuotedString_InputMissingClosingQuote_ReturnsFalse()
//     {
//         // Arrange
//         string input = "'unterminated string";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadSingleQuotedString returns false when the input is empty.
//     /// </summary>
//     [Fact]
//     public void ReadSingleQuotedString_EmptyInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = string.Empty;
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadSingleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(result);
//         Assert.Equal(string.Empty, output.ToString());
//     }
// 
//     /// <summary>
//     /// Verifies that ReadDoubleQuotedString returns true and outputs the expected quoted string
//     /// when the input starts with a valid double quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_ValidDoubleQuotedInput_ReturnsTrueAndOutputsQuotedString()
//     {
//         // Arrange
//         // The input starts with a properly closed double quoted string followed by other text.
//         string input = "\"Hello, World!\" remaining text";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadDoubleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(success);
//         // Expect the result to be the entire quoted string including the quotes.
//         Assert.Equal("\"Hello, World!\"", output.ToString());
//     }
// 
//     /// <summary>
//     /// Verifies that ReadDoubleQuotedString returns false and outputs an empty span
//     /// when the input does not start with a double quote.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_InputDoesNotStartWithDoubleQuote_ReturnsFalseAndEmptyOutput()
//     {
//         // Arrange
//         string input = "No quotes at beginning";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadDoubleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, output.ToString());
//     }
// 
//     /// <summary>
//     /// Verifies that ReadDoubleQuotedString returns false and outputs an empty span
//     /// when an empty buffer is provided.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_EmptyBuffer_ReturnsFalseAndEmptyOutput()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadDoubleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, output.ToString());
//     }
// 
//     /// <summary>
//     /// Verifies that ReadDoubleQuotedString returns false and outputs an empty span
//     /// when the input starts with a double quote but is missing the closing double quote.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_MissingClosingDoubleQuote_ReturnsFalseAndEmptyOutput()
//     {
//         // Arrange
//         string input = "\"Unfinished string with no end";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadDoubleQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, output.ToString());
//     }
// 
//     /// <summary>
//     /// Verifies that the parameterless overload of ReadDoubleQuotedString returns true
//     /// for valid input containing a properly formatted double quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_ParameterlessValidInput_ReturnsTrue()
//     {
//         // Arrange
//         string input = "\"Example\" additional content";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadDoubleQuotedString();
//         // Assert
//         Assert.True(success);
//     }
// 
//     /// <summary>
//     /// Verifies that the parameterless overload of ReadDoubleQuotedString returns false
//     /// when the input is not a valid double quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_ParameterlessInvalidInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "Invalid input without quotes";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadDoubleQuotedString();
//         // Assert
//         Assert.False(success);
//     }
// 
//     /// <summary>
//     /// Tests that ReadDoubleQuotedString (with out parameter) returns true and outputs the correct token when given a valid double-quoted string.
//     /// The token should include the original double quote characters.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_WithValidDoubleQuotedString_ReturnsTrueAndOutputsToken()
//     {
//         // Arrange
//         // A valid double quoted string with extra text afterwards.
//         string input = "\"Hello, World!\" extra text";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDoubleQuotedString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         // The expected token includes the surrounding quotes.
//         string expectedToken = "\"Hello, World!\"";
//         Assert.Equal(expectedToken, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless overload of ReadDoubleQuotedString returns true when a valid double-quoted string exists.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_Parameterless_WithValidDoubleQuotedString_ReturnsTrue()
//     {
//         // Arrange
//         string input = "\"Data\" following text";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDoubleQuotedString();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that ReadDoubleQuotedString returns false when the input does not start with a double quote.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_InputWithoutStartingDoubleQuote_ReturnsFalse()
//     {
//         // Arrange
//         string input = "No starting quote";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDoubleQuotedString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         // Expect the token to be empty.
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadDoubleQuotedString returns false when the input is an empty string.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_EmptyInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDoubleQuotedString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadDoubleQuotedString returns false when the input has a starting double quote but no closing double quote.
//     /// This simulates an incomplete quoted string scenario.
//     /// </summary>
//     [Fact]
//     public void ReadDoubleQuotedString_IncompleteDoubleQuotedString_ReturnsFalse()
//     {
//         // Arrange
//         string input = "\"Unfinished string with no end";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadDoubleQuotedString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString(out ReadOnlySpan<char> result) returns true and outputs the entire backtick string when the input starts with a valid backtick-enclosed string.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickString_WithValidBacktickEnclosedString_ReturnsTrueAndOutputsToken()
//     {
//         // Arrange
//         // Input begins with a valid backtick string followed by additional text.
//         string input = "`hello world` additional text";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadBacktickString(out ReadOnlySpan<char> token);
//         // Assert
//         // Expect success and that the token exactly equals the backtick string including the quotes.
//         Assert.True(success);
//         Assert.Equal("`hello world`", token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString() (the overload without output parameter) returns true when the input starts with a valid backtick-enclosed string.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickString_WithoutOutParameter_WithValidBacktickEnclosedString_ReturnsTrue()
//     {
//         // Arrange
//         string input = "`test string` remaining";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadBacktickString();
//         // Assert
//         Assert.True(success);
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString(out ReadOnlySpan<char> result) returns false and outputs an empty span when the input does not start with a backtick.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickString_InputDoesNotStartWithBacktick_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = "no backtick at start";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadBacktickString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString(out ReadOnlySpan<char> result) returns false and outputs an empty span when the backtick-enclosed string is incomplete.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickString_IncompleteBacktickString_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         // The input starts with a backtick but lacks a closing backtick.
//         string input = "`incomplete string without closing backtick";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadBacktickString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString(out ReadOnlySpan<char> result) returns false and outputs an empty span when the input is empty.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickString_EmptyInput_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = string.Empty;
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadBacktickString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(success);
//         Assert.Equal(string.Empty, token.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString(out ReadOnlySpan&lt;char&gt;) returns true and outputs the expected result 
//     /// when the input starts with a valid backtick-quoted string.
//     /// The expected result includes the backtick characters.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickStringOut_WithValidBacktickString_ReturnsTrueAndExpectedResult()
//     {
//         // Arrange
//         // For a valid backtick string, we assume that the Scanner will detect a quoted string starting and ending with '`'
//         // e.g. "`hello`" is valid and should be returned with the quotes.
//         string input = "`hello` some other text";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadBacktickString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.True(result);
//         // Expected token should include the backticks as per the summary of ReadQuotedString.
//         string tokenString = token.ToString();
//         Assert.Equal("`hello`", tokenString);
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString(out ReadOnlySpan&lt;char&gt;) returns false and outputs an empty span 
//     /// when the input does not start with a backtick.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickStringOut_WithInvalidStart_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         // Input does not start with a backtick so the method should not match.
//         string input = "hello without backtick";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadBacktickString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty, "Expected token to be empty when no valid backtick string is found.");
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadBacktickString() overload returns true 
//     /// when the input starts with a valid backtick-quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickString_NoOut_WithValidBacktickString_ReturnsTrue()
//     {
//         // Arrange
//         string input = "`world` extra text";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadBacktickString();
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests that the parameterless ReadBacktickString() overload returns false 
//     /// when the input does not start with a backtick.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickString_NoOut_WithInvalidStart_ReturnsFalse()
//     {
//         // Arrange
//         string input = "no backtick at start";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadBacktickString();
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString(out ReadOnlySpan&lt;char&gt;) returns false and an empty span 
//     /// when provided an empty input string.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickStringOut_WithEmptyInput_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = string.Empty;
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadBacktickString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty, "Expected token to be empty for an empty input.");
//     }
// 
//     /// <summary>
//     /// Tests that ReadBacktickString(out ReadOnlySpan&lt;char&gt;) returns false when the input starts with an 
//     /// opening backtick but does not contain a matching closing backtick.
//     /// </summary>
//     [Fact]
//     public void ReadBacktickStringOut_WithUnterminatedBacktickString_ReturnsFalse()
//     {
//         // Arrange
//         // Input with an opening backtick but no closing backtick.
//         string input = "`incomplete string without closing delimiter";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadBacktickString(out ReadOnlySpan<char> token);
//         // Assert
//         Assert.False(result);
//         Assert.True(token.IsEmpty, "Expected token to be empty when backtick string is unterminated.");
//     }
// 
//     /// <summary>
//     /// Tests the parameterless ReadQuotedString() method when the input starts with a valid single quoted string.
//     /// Expected outcome: the method returns true.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_NoParameters_ValidSingleQuotedString_ReturnsTrue()
//     {
//         // Arrange
//         string input = "'Hello World'";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString();
//         // Assert
//         Assert.True(result, "Expected ReadQuotedString() to return true when a valid single quoted string is present.");
//     }
// 
//     /// <summary>
//     /// Tests the parameterless ReadQuotedString() method when the input starts with a valid double quoted string.
//     /// Expected outcome: the method returns true.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_NoParameters_ValidDoubleQuotedString_ReturnsTrue()
//     {
//         // Arrange
//         string input = "\"Hello World\"";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString();
//         // Assert
//         Assert.True(result, "Expected ReadQuotedString() to return true when a valid double quoted string is present.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(out ReadOnlySpan<char> result) method with a valid single quoted string.
//     /// Expected outcome: the method returns true and outputs the complete quoted token.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_OutResult_ValidSingleQuotedString_ReturnsQuotedSpan()
//     {
//         // Arrange
//         string input = "'TestString'";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> quotedSpan);
//         // Assert
//         Assert.True(success, "Expected ReadQuotedString(out result) to return true for a valid single quoted string.");
//         Assert.Equal("'TestString'", quotedSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(out ReadOnlySpan<char> result) method with a valid double quoted string.
//     /// Expected outcome: the method returns true and outputs the complete quoted token.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_OutResult_ValidDoubleQuotedString_ReturnsQuotedSpan()
//     {
//         // Arrange
//         string input = "\"AnotherTest\"";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> quotedSpan);
//         // Assert
//         Assert.True(success, "Expected ReadQuotedString(out result) to return true for a valid double quoted string.");
//         Assert.Equal("\"AnotherTest\"", quotedSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(char quoteChar, out ReadOnlySpan<char> result) method with a valid quoted string using a specific quote character.
//     /// Expected outcome: the method returns true and outputs the complete quoted token.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_WithSpecifiedQuoteChar_ValidQuotedString_ReturnsQuotedSpan()
//     {
//         // Arrange
//         string input = "#CustomQuoted#";
//         var scanner = new Scanner(input);
//         char quoteChar = '#';
//         // Act
//         bool success = scanner.ReadQuotedString(quoteChar, out ReadOnlySpan<char> quotedSpan);
//         // Assert
//         Assert.True(success, "Expected ReadQuotedString(quoteChar, out result) to return true for a valid custom quoted string.");
//         Assert.Equal("#CustomQuoted#", quotedSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(char[] quoteChar) method when the input does not begin with any of the specified quote characters.
//     /// Expected outcome: the method returns false.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_WithQuoteArray_InvalidInput_ReturnsFalse()
//     {
//         // Arrange
//         string input = "NoQuotesHere";
//         var scanner = new Scanner(input);
//         char[] quotes = new char[]
//         {
//             '\'',
//             '\"'
//         };
//         // Act
//         bool result = scanner.ReadQuotedString(quotes);
//         // Assert
//         Assert.False(result, "Expected ReadQuotedString(char[] quoteChar) to return false when no valid quote character is present at the start.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(char[] quoteChar, out ReadOnlySpan<char> result) method with an incomplete quoted string.
//     /// Expected outcome: the method returns false and outputs an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_WithQuoteArray_IncompleteQuotedString_ReturnsFalse()
//     {
//         // Arrange
//         string input = "\"Incomplete";
//         var scanner = new Scanner(input);
//         char[] quotes = new char[]
//         {
//             '\"'
//         };
//         // Act
//         bool result = scanner.ReadQuotedString(quotes, out ReadOnlySpan<char> quotedSpan);
//         // Assert
//         Assert.False(result, "Expected ReadQuotedString(char[] quoteChar, out result) to return false for an incomplete quoted string.");
//         Assert.Equal(string.Empty, quotedSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString() method (no parameters) returns a valid double-quoted string.
//     /// The test provides input starting with a double-quoted token and verifies the method returns the full quoted portion.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_NoParams_WithValidDoubleQuotedString_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "\"Hello World\" extra";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success, "Expected ReadQuotedString to succeed on a valid double-quoted string.");
//         Assert.Equal("\"Hello World\"", result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString() method (no parameters) returns a valid single-quoted string.
//     /// The test provides input starting with a single-quoted token and verifies the returned string.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_NoParams_WithValidSingleQuotedString_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "'Hello World' extra";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success, "Expected ReadQuotedString to succeed on a valid single-quoted string.");
//         Assert.Equal("'Hello World'", result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString() method (no parameters) when the quoted string is missing its closing quote.
//     /// It is expected to return false and an empty result.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_NoParams_WithMissingClosingQuote_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = "\"Unterminated string";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success, "Expected ReadQuotedString to fail when the closing quote is missing.");
//         Assert.True(result.IsEmpty, "Expected the result to be empty when reading fails.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(char[] quoteChar, out ReadOnlySpan<char> result) overload with custom quote characters.
//     /// The test provides a custom set of quote characters and input that matches those, verifying the returned string.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharArrayParam_WithValidCustomQuotes_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "[CustomQuoted] remaining";
//         var scanner = new Scanner(input);
//         char[] customQuotes = new char[]
//         {
//             '[',
//             ']'
//         };
//         // Act
//         bool success = scanner.ReadQuotedString(customQuotes, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success, "Expected ReadQuotedString to succeed with valid custom quotes.");
//         Assert.Equal("[CustomQuoted]", result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(out ReadOnlySpan<char> result) overload.
//     /// This verifies that the overload without parameters behaves identically when valid input is provided.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_OutParam_WithValidQuotedString_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "\"TestString\" extra data";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success, "Expected ReadQuotedString to succeed on a valid quoted string using out parameter overload.");
//         Assert.Equal("\"TestString\"", result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(char quoteChar, out ReadOnlySpan<char> result) overload
//     /// when the input starts with the expected quote character. The method should return the full quoted token.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharParam_WithValidQuote_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "`BacktickString` extra";
//         var scanner = new Scanner(input);
//         char quoteChar = '`';
//         // Act
//         bool success = scanner.ReadQuotedString(quoteChar, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success, "Expected ReadQuotedString to succeed when the input starts with the expected quote character.");
//         Assert.Equal("`BacktickString`", result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(char quoteChar, out ReadOnlySpan<char> result) overload
//     /// when the input does not start with the expected quote character. The method should return false and an empty result.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharParam_WithMismatchedQuote_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = "\"Mismatched quotes' extra";
//         var scanner = new Scanner(input);
//         char expectedQuoteChar = '\'';
//         // Act
//         bool success = scanner.ReadQuotedString(expectedQuoteChar, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success, "Expected ReadQuotedString to fail when the starting quote does not match the expected quote character.");
//         Assert.True(result.IsEmpty, "Expected an empty result when reading fails due to mismatched quote.");
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString(char[] quoteChar, out ReadOnlySpan<char> result) overload when the input does not
//     /// start with any of the specified quote characters. The method should return false and an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharArrayParam_WithInvalidStartingCharacter_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = "NoQuotes here";
//         var scanner = new Scanner(input);
//         char[] validQuotes = new char[]
//         {
//             '\'',
//             '\"'
//         };
//         // Act
//         bool success = scanner.ReadQuotedString(validQuotes, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success, "Expected ReadQuotedString to fail when the input does not start with a valid quote.");
//         Assert.True(result.IsEmpty, "Expected an empty result when no valid quoted string is found.");
//     }
// 
//     /// <summary>
//     /// Tests ReadQuotedString(out ReadOnlySpan<char>) with a valid string starting with a single quote.
//     /// Expected to return true and the result containing the full quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_Out_WithValidSingleQuote_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "'abc'";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(input.AsSpan().ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests ReadQuotedString(out ReadOnlySpan<char>) with a valid string starting with a double quote.
//     /// Expected to return true and the result containing the full quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_Out_WithValidDoubleQuote_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "\"abc\"";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(input.AsSpan().ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests ReadQuotedString(out ReadOnlySpan<char>) with an input that does not start with a valid quote.
//     /// Expected to return false and the result to be an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_Out_WithInvalidStart_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = "noQuotes";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests ReadQuotedString(char[] quoteChar, out ReadOnlySpan<char>) with a valid string starting with a single quote.
//     /// Expected to return true and the result containing the full quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharArray_WithValidSingleQuote_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "'def'";
//         var scanner = new Scanner(input);
//         char[] validQuotes = new char[]
//         {
//             '\'',
//             '"'
//         };
//         // Act
//         bool success = scanner.ReadQuotedString(validQuotes, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(input.AsSpan().ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests ReadQuotedString(char[] quoteChar, out ReadOnlySpan<char>) with an input that does not start with a valid quote.
//     /// Expected to return false and the result to be an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharArray_WithInvalidStart_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = "xyz";
//         var scanner = new Scanner(input);
//         char[] validQuotes = new char[]
//         {
//             '\'',
//             '"'
//         };
//         // Act
//         bool success = scanner.ReadQuotedString(validQuotes, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests ReadQuotedString(char, out ReadOnlySpan<char>) with a valid matching quote.
//     /// Expected to return true and the result containing the full quoted string.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_Char_WithValidQuote_ReturnsQuotedString()
//     {
//         // Arrange
//         string input = "'ghi'";
//         var scanner = new Scanner(input);
//         char quote = '\'';
//         // Act
//         bool success = scanner.ReadQuotedString(quote, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.True(success);
//         Assert.Equal(input.AsSpan().ToString(), result.ToString());
//     }
// 
//     /// <summary>
//     /// Tests ReadQuotedString(char, out ReadOnlySpan<char>) with a non-matching quote.
//     /// Expected to return false and the result to be an empty span.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_Char_WithInvalidQuote_ReturnsFalseAndEmptyResult()
//     {
//         // Arrange
//         string input = "\"ghi\"";
//         var scanner = new Scanner(input);
//         char quote = '\'';
//         // Act
//         bool success = scanner.ReadQuotedString(quote, out ReadOnlySpan<char> result);
//         // Assert
//         Assert.False(success);
//         Assert.True(result.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the parameterless ReadQuotedString() method (which delegates to the out overload) with a valid quoted string.
//     /// Expected to return true.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_NoParameter_WithValidQuote_ReturnsTrue()
//     {
//         // Arrange
//         string input = "'jkl'";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString();
//         // Assert
//         Assert.True(success);
//     }
// 
//     /// <summary>
//     /// Tests the parameterless ReadQuotedString() method with an input that does not start with a valid quote.
//     /// Expected to return false.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_NoParameter_WithInvalidStart_ReturnsFalse()
//     {
//         // Arrange
//         string input = "jkl";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString();
//         // Assert
//         Assert.False(success);
//     }
// 
//     /// <summary>
//     /// Tests the parameterless ReadQuotedString method with a valid quoted string using the default quotes.
//     /// Expected to return true.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_Parameterless_ValidQuotedString_ReturnsTrue()
//     {
//         // Arrange
//         string input = "'hello'";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString();
//         // Assert
//         Assert.True(success);
//     }
// 
//     /// <summary>
//     /// Tests the parameterless ReadQuotedString method with an invalid quoted string missing its closing quote.
//     /// Expected to return false.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_Parameterless_MissingClosingQuote_ReturnsFalse()
//     {
//         // Arrange
//         string input = "'hello";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString();
//         // Assert
//         Assert.False(success);
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString method with an out parameter using default quotes.
//     /// Provides a valid quoted string and verifies the output result matches the original input.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_OutParameter_ValidQuotedString_ReturnsTrueAndOutput()
//     {
//         // Arrange
//         string input = "\"world\"";
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("\"world\"", output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString method with an out parameter using default quotes when the input is invalid.
//     /// Expected to return false and produce an empty output.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_OutParameter_InvalidQuotedString_ReturnsFalseAndEmptyOutput()
//     {
//         // Arrange
//         string input = "\"world"; // Missing closing quote.
//         var scanner = new Scanner(input);
//         // Act
//         bool success = scanner.ReadQuotedString(out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(success);
//         Assert.True(output.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString method that accepts a char array of quote characters.
//     /// Provides a valid quoted string with custom quote characters and verifies the output.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_WithCharArray_ValidQuotedString_ReturnsTrueAndOutput()
//     {
//         // Arrange
//         string input = "#custom#";
//         var scanner = new Scanner(input);
//         char[] quotes = new char[]
//         {
//             '#'
//         };
//         // Act
//         bool success = scanner.ReadQuotedString(quotes, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("#custom#", output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString method that accepts a char array of quote characters when the starting character is not valid.
//     /// Expected to return false and produce an empty output.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_WithCharArray_InvalidQuotedString_ReturnsFalseAndEmptyOutput()
//     {
//         // Arrange
//         string input = "'notcustom'";
//         var scanner = new Scanner(input);
//         char[] quotes = new char[]
//         {
//             '#'
//         };
//         // Act
//         bool success = scanner.ReadQuotedString(quotes, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(success);
//         Assert.True(output.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString method that accepts a single quote character with an out parameter.
//     /// Provides a valid quoted string and verifies that the output matches the input.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_WithSingleQuoteCharacter_ValidQuotedString_ReturnsTrueAndOutput()
//     {
//         // Arrange
//         string input = "!data!";
//         var scanner = new Scanner(input);
//         char quoteChar = '!';
//         // Act
//         bool success = scanner.ReadQuotedString(quoteChar, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("!data!", output.ToString());
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString method that accepts a single quote character with an out parameter for an invalid quoted string.
//     /// Expected to return false and produce an empty output when the closing quote is missing.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_WithSingleQuoteCharacter_MissingClosingQuote_ReturnsFalseAndEmptyOutput()
//     {
//         // Arrange
//         string input = "!data";
//         var scanner = new Scanner(input);
//         char quoteChar = '!';
//         // Act
//         bool success = scanner.ReadQuotedString(quoteChar, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.False(success);
//         Assert.True(output.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Tests the ReadQuotedString method with a char array overload using default allowed quotes.
//     /// Provides a valid quoted string and verifies that the output is as expected.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_WithCharArray_DefaultQuotes_ValidQuotedString_ReturnsTrueAndOutput()
//     {
//         // Arrange
//         string input = "'example'";
//         var scanner = new Scanner(input);
//         char[] quotes = new char[]
//         {
//             '\'',
//             '\"'
//         };
//         // Act
//         bool success = scanner.ReadQuotedString(quotes, out ReadOnlySpan<char> output);
//         // Assert
//         Assert.True(success);
//         Assert.Equal("'example'", output.ToString());
//     }
// 
// #region Tests for ReadQuotedString(char quoteChar, out ReadOnlySpan<char> result)
//     /// <summary>
//     /// Verifies that ReadQuotedString(char, out ReadOnlySpan<char>) returns false when the current character does not match the expected quote.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharInvalidStartingCharacter_ReturnsFalse()
//     {
//         // Arrange
//         var input = "Hello"; // Does not start with a quote
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString('\'', out ReadOnlySpan<char> quoted);
//         // Assert
//         Assert.False(result);
//         Assert.True(quoted.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Verifies that ReadQuotedString(char, out ReadOnlySpan<char>) successfully reads a properly quoted string without any escape sequences.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharValidSimple_ReturnsQuotedContent()
//     {
//         // Arrange
//         var input = "'Hello'";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString('\'', out ReadOnlySpan<char> quoted);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(input, quoted.ToString());
//     }
// 
//     /// <summary>
//     /// Verifies that ReadQuotedString(char, out ReadOnlySpan<char>) returns false when the quoted string is missing the ending quote.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharMissingEndingQuote_ReturnsFalse()
//     {
//         // Arrange
//         var input = "'Unfinished";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString('\'', out ReadOnlySpan<char> quoted);
//         // Assert
//         Assert.False(result);
//         Assert.True(quoted.IsEmpty);
//     }
// 
//     /// <summary>
//     /// Verifies that ReadQuotedString(char, out ReadOnlySpan<char>) correctly processes a quoted string that contains a valid escape sequence.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharValidEscapeSequence_ReturnsQuotedContent()
//     {
//         // Arrange
//         // The string contains an escaped quote: It\'s
//         var input = "'It\\'s'";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString('\'', out ReadOnlySpan<char> quoted);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(input, quoted.ToString());
//     }
// 
//     /// <summary>
//     /// Verifies that ReadQuotedString(char, out ReadOnlySpan<char>) returns false when encountering an invalid escape sequence.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharInvalidEscapeSequence_ReturnsFalse()
//     {
//         // Arrange
//         // The escape sequence \q is invalid.
//         var input = "'abc\\qdef'";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString('\'', out ReadOnlySpan<char> quoted);
//         // Assert
//         Assert.False(result);
//         Assert.True(quoted.IsEmpty);
//     }
// 
// #endregion
// #region Tests for ReadQuotedString() [Parameterless]
//     /// <summary>
//     /// Verifies that the parameterless ReadQuotedString method returns true when the input starts with a valid default quote (either single or double).
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_ParameterlessValidDoubleQuote_ReturnsTrue()
//     {
//         // Arrange
//         var input = "\"Hello World\"";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString();
//         // Assert
//         Assert.True(result);
//     }
// 
// #endregion
// #region Tests for ReadQuotedString(out ReadOnlySpan<char> result)
//     /// <summary>
//     /// Verifies that ReadQuotedString(out ReadOnlySpan<char>) correctly reads a quoted string using default quote characters.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_OutOverloadValidSingleQuote_ReturnsQuotedContent()
//     {
//         // Arrange
//         var input = "'Sample Text'";
//         var scanner = new Scanner(input);
//         // Act
//         bool result = scanner.ReadQuotedString(out ReadOnlySpan<char> quoted);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(input, quoted.ToString());
//     }
// 
// #endregion
// #region Tests for ReadQuotedString(char[] quoteChar) and ReadQuotedString(char[] quoteChar, out ReadOnlySpan<char> result)
//     /// <summary>
//     /// Verifies that ReadQuotedString(char[] quoteChar) returns true when the input starts with one of the expected quote characters.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharArrayOverloadValidQuote_ReturnsTrue()
//     {
//         // Arrange
//         var input = "\"Test Case\"";
//         var scanner = new Scanner(input);
//         char[] validQuotes = new char[]
//         {
//             '\"',
//             '\''
//         };
//         // Act
//         bool result = scanner.ReadQuotedString(validQuotes);
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Verifies that ReadQuotedString(char[] quoteChar, out ReadOnlySpan<char> result) correctly reads a quoted string when provided with an array of valid quote characters.
//     /// </summary>
//     [Fact]
//     public void ReadQuotedString_CharArrayOutOverloadValidQuote_ReturnsQuotedContent()
//     {
//         // Arrange
//         var input = "'Another Test'";
//         var scanner = new Scanner(input);
//         char[] validQuotes = new char[]
//         {
//             '\"',
//             '\''
//         };
//         // Act
//         bool result = scanner.ReadQuotedString(validQuotes, out ReadOnlySpan<char> quoted);
//         // Assert
//         Assert.True(result);
//         Assert.Equal(input, quoted.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the Scanner constructor correctly initializes the instance with a valid buffer.
//     /// The test verifies that the Buffer property matches the input and that the Cursor property is not null.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithValidBuffer_SetsBufferAndInitializesCursor()
//     {
//         // Arrange
//         string inputBuffer = "Test input";
//         // Act
//         Scanner scanner = new Scanner(inputBuffer);
//         // Assert
//         Assert.Equal(inputBuffer, scanner.Buffer);
//         Assert.NotNull(scanner.Cursor);
//     }
// 
//     /// <summary>
//     /// Tests that the Scanner constructor throws an ArgumentNullException when a null buffer is provided.
//     /// The test verifies that the exception parameter name is "buffer".
//     /// </summary>
//     [Fact]
//     public void Constructor_WithNullBuffer_ThrowsArgumentNullException()
//     {
//         // Arrange
//         string inputBuffer = null;
//         // Act & Assert
//         ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new Scanner(inputBuffer));
//         Assert.Equal("buffer", exception.ParamName);
//     }
// }