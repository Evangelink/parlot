using Parlot;
using System;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Character"/> class.
/// </summary>
public class CharacterTests
{
    /// <summary>
    /// Tests the IsDecimalDigit method with valid digit characters.
    /// This test confirms that IsDecimalDigit returns true for characters '0' through '9'.
    /// </summary>
    /// <param name = "digit">A valid digit character.</param>
    [Theory]
    [InlineData('0')]
    [InlineData('1')]
    [InlineData('2')]
    [InlineData('3')]
    [InlineData('4')]
    [InlineData('5')]
    [InlineData('6')]
    [InlineData('7')]
    [InlineData('8')]
    [InlineData('9')]
    public void IsDecimalDigit_ValidDigit_ReturnsTrue(char digit)
    {
        // Act
        bool result = Character.IsDecimalDigit(digit);
        // Assert
        Assert.True(result, $"Expected IsDecimalDigit to return true for digit '{digit}' but it returned false.");
    }

    /// <summary>
    /// Tests the IsDecimalDigit method with non-digit characters.
    /// This test confirms that IsDecimalDigit returns false for characters that are not between '0' and '9'.
    /// </summary>
    /// <param name = "nonDigit">A character that is not a decimal digit.</param>
    [Theory]
    [InlineData('a')]
    [InlineData('Z')]
    [InlineData(' ')]
    [InlineData('/')]
    [InlineData(':')]
    [InlineData('\0')]
    [InlineData('-')]
    [InlineData('+')]
    public void IsDecimalDigit_NonDigit_ReturnsFalse(char nonDigit)
    {
        // Act
        bool result = Character.IsDecimalDigit(nonDigit);
        // Assert
        Assert.False(result, $"Expected IsDecimalDigit to return false for non-digit '{nonDigit}' but it returned true.");
    }

    /// <summary>
    /// Tests that IsIdentifierStart returns true for alphabetical characters typically valid as identifier starts.
    /// </summary>
    /// <param name = "input">A character expected to be a valid identifier start.</param>
    [Theory]
    [InlineData('A')]
    [InlineData('Z')]
    [InlineData('a')]
    [InlineData('z')]
    public void IsIdentifierStart_WithLetterCharacter_ReturnsTrue(char input)
    {
        // Act
        bool result = Character.IsIdentifierStart(input);
        // Assert
        Assert.True(result, $"Expected IsIdentifierStart to return true for letter '{input}'.");
    }

    /// <summary>
    /// Tests that IsIdentifierStart returns false for characters not valid as identifier starts such as digits and common punctuation.
    /// </summary>
    /// <param name = "input">A character expected not to be a valid identifier start.</param>
    [Theory]
    [InlineData('0')]
    [InlineData('9')]
    [InlineData('+')]
    [InlineData('-')]
    [InlineData(' ')]
    public void IsIdentifierStart_WithNonLetterCharacter_ReturnsFalse(char input)
    {
        // Act
        bool result = Character.IsIdentifierStart(input);
        // Assert
        Assert.False(result, $"Expected IsIdentifierStart to return false for non-letter character '{input}'.");
    }

    /// <summary>
    /// Tests that IsIdentifierStart handles boundary characters like char.MinValue and char.MaxValue.
    /// The expected outcome is assumed to be false if the character is not flagged as a valid identifier start.
    /// </summary>
    /// <param name = "input">A boundary character value.</param>
    [Theory]
    [InlineData(char.MinValue)]
    [InlineData(char.MaxValue)]
    public void IsIdentifierStart_WithBoundaryCharacters_ReturnsFalse(char input)
    {
        // Act
        bool result = Character.IsIdentifierStart(input);
        // Assert
        Assert.False(result, $"Expected IsIdentifierStart to return false for boundary character '{(int)input}'.");
    }

    /// <summary>
    /// Tests the <see cref = "Character.IsIdentifierPart(char)"/> method to ensure it returns true for valid identifier part characters.
    /// This test verifies that letters, digits, and underscore are recognized as valid identifier parts.
    /// </summary>
    /// <param name = "ch">The input character to evaluate.</param>
    [Theory]
    [InlineData('a')]
    [InlineData('Z')]
    [InlineData('0')]
    [InlineData('_')]
    public void IsIdentifierPart_WhenCharacterIsValidIdentifierPart_ReturnsTrue(char ch)
    {
        // Act
        bool result = Character.IsIdentifierPart(ch);
        // Assert
        Assert.True(result, $"Expected Character.IsIdentifierPart('{ch}') to return true.");
    }

    /// <summary>
    /// Tests the <see cref = "Character.IsIdentifierPart(char)"/> method to verify it returns false for characters that are not valid identifier parts.
    /// This test considers whitespace, punctuation, and boundary characters that should not be marked as identifier parts.
    /// </summary>
    /// <param name = "ch">The input character to evaluate.</param>
    [Theory]
    [InlineData(' ')]
    [InlineData('#')]
    [InlineData('\0')]
    [InlineData('\uffff')]
    public void IsIdentifierPart_WhenCharacterIsNotValidIdentifierPart_ReturnsFalse(char ch)
    {
        // Act
        bool result = Character.IsIdentifierPart(ch);
        // Assert
        Assert.False(result, $"Expected Character.IsIdentifierPart('{ch}') to return false.");
    }

    /// <summary>
    /// Tests that IsHexDigit returns true for all valid hexadecimal characters.
    /// </summary>
    /// <param name = "input">A valid hexadecimal character.</param>
    [Theory]
    [InlineData('0')]
    [InlineData('1')]
    [InlineData('2')]
    [InlineData('3')]
    [InlineData('4')]
    [InlineData('5')]
    [InlineData('6')]
    [InlineData('7')]
    [InlineData('8')]
    [InlineData('9')]
    [InlineData('A')]
    [InlineData('B')]
    [InlineData('C')]
    [InlineData('D')]
    [InlineData('E')]
    [InlineData('F')]
    [InlineData('a')]
    [InlineData('b')]
    [InlineData('c')]
    [InlineData('d')]
    [InlineData('e')]
    [InlineData('f')]
    public void IsHexDigit_ValidHexDigits_ReturnsTrue(char input)
    {
        // Act
        bool result = Character.IsHexDigit(input);
        // Assert
        Assert.True(result, $"Expected IsHexDigit to return true for valid hex character '{input}', but it returned false.");
    }

    /// <summary>
    /// Tests that IsHexDigit returns false for characters that are not valid hexadecimal digits.
    /// </summary>
    /// <param name = "input">A non-hexadecimal character.</param>
    [Theory]
    [InlineData('G')]
    [InlineData('z')]
    [InlineData('/')]
    [InlineData(':')]
    [InlineData(' ')]
    [InlineData('\n')]
    [InlineData('!')]
    [InlineData('\0')]
    public void IsHexDigit_InvalidHexDigits_ReturnsFalse(char input)
    {
        // Act
        bool result = Character.IsHexDigit(input);
        // Assert
        Assert.False(result, $"Expected IsHexDigit to return false for non-hex character '{input}', but it returned true.");
    }
}