// using  < global  namespace >; [Error] (1-8)CS1001 Identifier expected [Error] (1-8)CS1002 ; expected [Error] (1-8)CS1525 Invalid expression term '<' [Error] (1-18)CS1002 ; expected [Error] (1-28)CS1001 Identifier expected [Error] (1-28)CS1514 { expected [Error] (1-29)CS1022 Type or namespace definition, or end-of-file expected [Error] (1-10)CS0103 The name 'global' does not exist in the current context
using Moq;
using Parlot;
using Parlot.Compilation;
using Parlot.Fluent;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

/// <summary>
/// Unit tests for the <see cref = "Cursor"/> class focusing on the Advance methods.
/// </summary>
// public class CursorTests [Error] (1332-2)CS1513 } expected [Error] (1332-2)CS1038 #endregion directive expected
// {
//     /// <summary>
//     /// Tests the Advance() method when advancing within the buffer.
//     /// Expected outcome: The cursor's Offset is incremented, Current is updated to the next character, and Eof remains false.
//     /// </summary>
//     [Fact]
//     public void Advance_WhenWithinBuffer_UpdatesCurrentAndOffset()
//     {
//         // Arrange
//         string buffer = "abc";
//         // Create cursor instance with initial TextPosition.Start.
//         var cursor = new Cursor(buffer);
//         // Act
//         // Assuming initial state: Offset = 0 and Current equals buffer[0] = 'a'
//         cursor.Advance();
//         // Assert
//         // After one advance, offset should be 1 and Current should be 'b'
//         Assert.Equal(1, cursor.Offset);
//         Assert.Equal('b', cursor.Current);
//         Assert.False(cursor.Eof);
//     }
// 
//     /// <summary>
//     /// Tests the Advance() method when the cursor reaches the end of the buffer.
//     /// Expected outcome: Eof is set to true and Current is set to NullChar.
//     /// </summary>
//     [Fact]
//     public void Advance_WhenAtEndOfBuffer_SetsEofAndNullChar()
//     {
//         // Arrange
//         string buffer = "a";
//         var cursor = new Cursor(buffer);
//         // Act
//         // First Advance() should set the cursor to the only character.
//         // Second Advance() should move beyond the end.
//         cursor.Advance();
//         cursor.Advance();
//         // Assert
//         // After advancing past the end, Eof should be true, and Current should equal the defined NullChar.
//         Assert.True(cursor.Eof);
//         Assert.Equal(Cursor.NullChar, cursor.Current);
//         Assert.True(cursor.Offset >= buffer.Length);
//     }
// 
//     /// <summary>
//     /// Tests the Advance() method when a newline is encountered.
//     /// Expected outcome: When the previous character is a newline, the cursor's line number is incremented and the column resets.
//     /// </summary>
//     [Fact]
//     public void Advance_WhenPreviousCharacterIsNewLine_ResetsColumnAndIncrementsLine()
//     {
//         // Arrange
//         // Buffer contains a newline character at index 1.
//         string buffer = "a\nb";
//         var cursor = new Cursor(buffer);
//         // Act
//         // First Advance: from 'a' to '\n'
//         cursor.Advance();
//         // Second Advance: from '\n' to 'b'; newline condition should trigger line increment and column reset
//         cursor.Advance();
//         // Assert
//         // Verify that after the newline, the current character is 'b'
//         Assert.Equal('b', cursor.Current);
//         // Verify that the cursor indicates it is not at the end of file.
//         Assert.False(cursor.Eof);
//         // Validate the tracked position if supported by the Position property.
//         // Assuming initial position is (Line = 1, Column = 1), after the first advance:
//         // Column is incremented to 2 for non-newline, then after the newline is encountered, on next advance,
//         // line should be 2 and column reset to 1.
//         var position = cursor.Position;
//         Assert.Equal(2, position.Line);
//         Assert.Equal(1, position.Column);
//     }
// 
//     /// <summary>
//     /// Tests the Advance(int count) method when advancing within the buffer.
//     /// Expected outcome: The cursor advances 'count' times, updating Offset and Current correctly.
//     /// </summary>
//     [Fact]
//     public void AdvanceInt_WhenWithinBuffer_UpdatesCurrentAndOffset()
//     {
//         // Arrange
//         string buffer = "abcdef";
//         var cursor = new Cursor(buffer);
//         // Act
//         // Advance the cursor by 3 positions. 
//         cursor.Advance(3);
//         // Assert
//         // Expected: Offset equals 3 and Current equals the character at index 3 ('d').
//         Assert.Equal(3, cursor.Offset);
//         Assert.Equal('d', cursor.Current);
//         Assert.False(cursor.Eof);
//     }
// 
//     /// <summary>
//     /// Tests the Advance(int count) method when the provided count exceeds the remaining characters.
//     /// Expected outcome: The cursor moves to the end, Eof is true, and Current is set to NullChar.
//     /// </summary>
//     [Fact]
//     public void AdvanceInt_WhenCountExceedsBuffer_SetsEofAndNullChar()
//     {
//         // Arrange
//         string buffer = "abc";
//         var cursor = new Cursor(buffer);
//         // Act
//         // Advance by a count greater than the length of the buffer.
//         cursor.Advance(5);
//         // Assert
//         // Expected: Cursor is at or past the end of the buffer.
//         Assert.True(cursor.Eof);
//         Assert.True(cursor.Offset >= buffer.Length);
//         Assert.Equal(Cursor.NullChar, cursor.Current);
//     }
// 
//     /// <summary>
//     /// Tests the parameterless Advance method to ensure it advances the cursor by one character.
//     /// Assumes that the parameterless Advance behaves equivalently to Advance(1).
//     /// </summary>
//     [Fact]
//     public void Advance_Parameterless_WithValidBuffer_AdvancesCursorByOne()
//     {
//         // Arrange
//         string buffer = "abc";
//         // Create a new Cursor with a known buffer.
//         // Assumption: The cursor starts at offset 0, current character equals buffer[0],
//         // and its position starts at line 1, column 1.
//         var cursor = new Cursor(buffer);
//         char initialCurrent = cursor.Current;
//         int initialOffset = cursor.Offset;
//         var initialPosition = cursor.Position; // Expected: Line = 1, Column = 1
//         // Act
//         cursor.Advance();
//         // Assert
//         // After advancing one character, expect the offset to increment by 1.
//         Assert.Equal(initialOffset + 1, cursor.Offset);
//         // The current character should now be the character at the new offset.
//         if (cursor.Offset < buffer.Length)
//         {
//             Assert.Equal(buffer[cursor.Offset], cursor.Current);
//         }
// 
//         // Validate that the position has advanced by one column (if no new line was encountered).
//         Assert.Equal(initialPosition.Line, cursor.Position.Line);
//         Assert.Equal(initialPosition.Column + 1, cursor.Position.Column);
//     }
// 
//     /// <summary>
//     /// Tests the Advance(int count) method for a count within the bounds of the buffer.
//     /// Verifies that the cursor's offset, current character, and position are updated correctly.
//     /// </summary>
//     [Fact]
//     public void AdvanceInt_WhenCountWithinRange_AdvancesCorrectly()
//     {
//         // Arrange
//         string buffer = "abcd";
//         var cursor = new Cursor(buffer);
//         int initialOffset = cursor.Offset;
//         var initialPosition = cursor.Position; // Expected: Line = 1, Column = 1
//         int advanceCount = 2;
//         // Act
//         cursor.Advance(advanceCount);
//         // Assert
//         // The offset should be incremented by the count.
//         Assert.Equal(initialOffset + advanceCount, cursor.Offset);
//         // Since the buffer has no newline, the current character is the one at the new offset.
//         if (cursor.Offset < buffer.Length)
//         {
//             Assert.Equal(buffer[cursor.Offset], cursor.Current);
//         }
// 
//         // Without newline characters, the column should increase by the number of characters advanced.
//         Assert.Equal(initialPosition.Line, cursor.Position.Line);
//         Assert.Equal(initialPosition.Column + advanceCount, cursor.Position.Column);
//     }
// 
//     /// <summary>
//     /// Tests the Advance(int count) method when the count exceeds the remaining characters in the buffer.
//     /// Verifies that End of File (Eof) is set, the offset equals the buffer length, and current becomes NullChar.
//     /// </summary>
//     [Fact]
//     public void AdvanceInt_WhenCountExceedsBuffer_SetsEofAndOffset()
//     {
//         // Arrange
//         string buffer = "abc";
//         var cursor = new Cursor(buffer);
//         // Use a count that exceeds the available characters.
//         int advanceCount = 3; // typical buffer length is 3, and based on the logic Eof will be set.
//         // Act
//         cursor.Advance(advanceCount);
//         // Assert
//         // The cursor should be at End of File.
//         Assert.True(cursor.Eof);
//         // The offset is set to the buffer length.
//         Assert.Equal(buffer.Length, cursor.Offset);
//         // The current character should now be the declared NullChar.
//         Assert.Equal(Cursor.NullChar, cursor.Current);
//         // The column in position is expected to have been incremented one extra time at the end.
//         // Starting column is assumed to be 1; with 2 iterations (since max valid offset is buffer.Length - 1),
//         // column would be 1 + 2, and then increased by one due to Eof adjustment.
//         int expectedColumn = 1 + (buffer.Length - 1) + 1;
//         Assert.Equal(expectedColumn, cursor.Position.Column);
//     }
// 
//     /// <summary>
//     /// Tests the Advance(int count) method when called on a cursor that is already at End of File (Eof).
//     /// Verifies that no further advancement occurs.
//     /// </summary>
//     [Fact]
//     public void AdvanceInt_WhenCalledOnCursorAtEof_NoOp()
//     {
//         // Arrange
//         string buffer = "ab";
//         var cursor = new Cursor(buffer);
//         // Advance beyond the buffer to trigger Eof.
//         cursor.Advance(3);
//         int offsetAtEof = cursor.Offset;
//         char currentAtEof = cursor.Current;
//         var positionAtEof = cursor.Position;
//         // Act
//         cursor.Advance(1);
//         // Assert
//         // The cursor's state should remain unchanged.
//         Assert.True(cursor.Eof);
//         Assert.Equal(offsetAtEof, cursor.Offset);
//         Assert.Equal(currentAtEof, cursor.Current);
//         Assert.Equal(positionAtEof.Line, cursor.Position.Line);
//         Assert.Equal(positionAtEof.Column, cursor.Position.Column);
//     }
// 
//     /// <summary>
//     /// Tests the parameterless Advance method when the cursor is at End of File (Eof),
//     /// ensuring that invoking it does not change the state.
//     /// </summary>
//     [Fact]
//     public void Advance_Parameterless_WhenCalledAtEof_NoOp()
//     {
//         // Arrange
//         string buffer = "ab";
//         var cursor = new Cursor(buffer);
//         // Advance to set Eof.
//         cursor.Advance(3);
//         int offsetAtEof = cursor.Offset;
//         char currentAtEof = cursor.Current;
//         var positionAtEof = cursor.Position;
//         // Act
//         cursor.Advance();
//         // Assert
//         // The cursor's state should remain unchanged when already at Eof.
//         Assert.True(cursor.Eof);
//         Assert.Equal(offsetAtEof, cursor.Offset);
//         Assert.Equal(currentAtEof, cursor.Current);
//         Assert.Equal(positionAtEof.Line, cursor.Position.Line);
//         Assert.Equal(positionAtEof.Column, cursor.Position.Column);
//     }
// 
//     /// <summary>
//     /// Tests that AdvanceNoNewLines with a valid positive offset updates the cursor's state correctly.
//     /// It verifies that the method advances to the correct character, updates the offset, and does not set Eof.
//     /// </summary>
//     [Fact]
//     public void AdvanceNoNewLines_ValidAdvance_UpdatesCursorCorrectly()
//     {
//         // Arrange
//         string buffer = "abcdef";
//         // Use the constructor that takes just the buffer; assumes TextPosition.Start is properly defined.
//         var cursor = new Cursor(buffer);
//         // Pre-assert: initial state should be at offset 0 with first character.
//         Assert.Equal(0, cursor.Offset);
//         Assert.Equal(buffer[0], cursor.Current);
//         Assert.False(cursor.Eof);
//         int advanceAmount = 3;
//         int expectedOffset = cursor.Offset + advanceAmount; // expected = 3 if starting 0
//         char expectedChar = buffer[expectedOffset];
//         // Act
//         cursor.AdvanceNoNewLines(advanceAmount);
//         // Assert
//         Assert.Equal(expectedOffset, cursor.Offset);
//         Assert.Equal(expectedChar, cursor.Current);
//         Assert.False(cursor.Eof);
//     }
// 
//     /// <summary>
//     /// Tests that AdvanceNoNewLines with zero offset does not alter the cursor's state.
//     /// It verifies that the offset, current character, and Eof flag remain unchanged.
//     /// </summary>
//     [Fact]
//     public void AdvanceNoNewLines_ZeroAdvance_NoStateChange()
//     {
//         // Arrange
//         string buffer = "abcdef";
//         var cursor = new Cursor(buffer);
//         int initialOffset = cursor.Offset;
//         char initialChar = cursor.Current;
//         bool initialEof = cursor.Eof;
//         // Act
//         cursor.AdvanceNoNewLines(0);
//         // Assert
//         Assert.Equal(initialOffset, cursor.Offset);
//         Assert.Equal(initialChar, cursor.Current);
//         Assert.Equal(initialEof, cursor.Eof);
//     }
// 
//     /// <summary>
//     /// Tests that AdvanceNoNewLines when advancing beyond the end of the buffer sets the Eof flag,
//     /// adjusts the offset to the length of the buffer, and sets the current character to NullChar.
//     /// </summary>
//     [Fact]
//     public void AdvanceNoNewLines_AdvanceBeyondEnd_SetsEofAndMovesToBufferEnd()
//     {
//         // Arrange
//         string buffer = "abcdef";
//         var cursor = new Cursor(buffer);
//         // _textLength is buffer length; valid indices are 0 to Length-1.
//         // Determine an offset that will move the cursor beyond the last valid index.
//         int advanceAmount = buffer.Length + 3; // This will certainly overshoot.
//         int expectedOffset = buffer.Length; // As specified in method.
//         char expectedChar = Cursor.NullChar; // Expected current char when EOF reached.
//         // Act
//         cursor.AdvanceNoNewLines(advanceAmount);
//         // Assert
//         Assert.Equal(expectedOffset, cursor.Offset);
//         Assert.Equal(expectedChar, cursor.Current);
//         Assert.True(cursor.Eof);
//     }
// 
//     /// <summary>
//     /// Tests that AdvanceNoNewLines when called with a negative offset results in an exception.
//     /// This validates that the method does not support negative advances and correctly fails.
//     /// </summary>
//     [Fact]
//     public void AdvanceNoNewLines_NegativeAdvance_ThrowsException()
//     {
//         // Arrange
//         string buffer = "abcdef";
//         var cursor = new Cursor(buffer);
//         int negativeAdvance = -1;
//         // Act & Assert
//         Assert.Throws<IndexOutOfRangeException>(() => cursor.AdvanceNoNewLines(negativeAdvance));
//     }
// 
//     /// <summary>
//     /// Tests that sequential calls to AdvanceNoNewLines correctly update the cursor state.
//     /// First it advances within the bounds, then attempts another advance that overshoots,
//     /// finally verifying the cumulative effect on the cursor's state.
//     /// </summary>
//     [Fact]
//     public void AdvanceNoNewLines_SequentialAdvance_UpdatesCursorStateAsExpected()
//     {
//         // Arrange
//         string buffer = "abcdefghij";
//         var cursor = new Cursor(buffer);
//         // First valid advance
//         int firstAdvance = 4;
//         int expectedOffsetAfterFirst = cursor.Offset + firstAdvance;
//         char expectedCharAfterFirst = buffer[expectedOffsetAfterFirst];
//         // Act - first advance
//         cursor.AdvanceNoNewLines(firstAdvance);
//         // Assert after first advance
//         Assert.Equal(expectedOffsetAfterFirst, cursor.Offset);
//         Assert.Equal(expectedCharAfterFirst, cursor.Current);
//         Assert.False(cursor.Eof);
//         // Arrange - second advance overshooting end
//         int secondAdvance = buffer.Length; // This will overshoot from current offset.
//         int expectedFinalOffset = buffer.Length;
//         char expectedFinalChar = Cursor.NullChar;
//         // Act - second advance
//         cursor.AdvanceNoNewLines(secondAdvance);
//         // Assert after second advance
//         Assert.Equal(expectedFinalOffset, cursor.Offset);
//         Assert.Equal(expectedFinalChar, cursor.Current);
//         Assert.True(cursor.Eof);
//     }
// 
//     /// <summary>
//     /// Tests that calling ResetPosition with a TextPosition having the same offset as the current cursor
//     /// does not alter the cursor's position.
//     /// </summary>
//     [Fact]
//     public void ResetPosition_WithSameOffset_DoesNotChangePosition()
//     {
//         // Arrange
//         string buffer = "Hello, world!";
//         // Initialize the cursor using the default start position (assumed to be TextPosition.Start).
//         Cursor cursor = new Cursor(buffer);
//         TextPosition initialPosition = cursor.Position;
//         // Act
//         cursor.ResetPosition(initialPosition);
//         // Assert
//         // The current position of the cursor should remain unchanged.
//         Assert.Equal(initialPosition.Offset, cursor.Offset);
//         Assert.Equal(initialPosition.Line, cursor.Position.Line);
//         Assert.Equal(initialPosition.Column, cursor.Position.Column);
//     }
// 
//     /// <summary>
//     /// Tests that calling ResetPosition with a TextPosition that has a different offset updates
//     /// the cursor's position accordingly.
//     /// </summary>
//     [Fact]
//     public void ResetPosition_WithDifferentOffset_UpdatesPosition()
//     {
//         // Arrange
//         string buffer = "Hello, world!";
//         // Initialize the cursor with the default start position.
//         Cursor cursor = new Cursor(buffer);
//         // Create a new TextPosition with an offset different from the current one.
//         // Assumes that TextPosition has a constructor with parameters: offset, line, column.
//         // For example, setting offset = 5, line = 1, and column = 6.
//         TextPosition newPosition = new TextPosition(5, 1, 6);
//         // Act
//         cursor.ResetPosition(newPosition);
//         // Assert
//         // The cursor's offset and tracked position should now match those of newPosition.
//         Assert.Equal(newPosition.Offset, cursor.Offset);
//         Assert.Equal(newPosition.Line, cursor.Position.Line);
//         Assert.Equal(newPosition.Column, cursor.Position.Column);
//     }
// 
//     /// <summary>
//     /// Helper method to invoke the private ResetPositionNotInlined method via reflection.
//     /// </summary>
//     /// <param name = "cursor">The instance of Cursor.</param>
//     /// <param name = "textPosition">The TextPosition value to set.</param>
//     private static void InvokeResetPositionNotInlined(Cursor cursor, in TextPosition textPosition)
//     {
//         MethodInfo method = typeof(Cursor).GetMethod("ResetPositionNotInlined", BindingFlags.NonPublic | BindingFlags.Instance);
//         if (method == null)
//         {
//             throw new InvalidOperationException("Unable to find method ResetPositionNotInlined via reflection.");
//         }
// 
//         // Invoke the method with the textPosition parameter.
//         method.Invoke(cursor, new object[] { textPosition });
//     }
// 
//     /// <summary>
//     /// Tests that ResetPositionNotInlined correctly updates the state when the new position is within the buffer bounds.
//     /// Expected Outcome: Cursor.Offset is updated, Current reflects the character at the new offset, and Eof is false.
//     /// </summary>
//     [Fact]
//     public void ResetPositionNotInlined_ValidPositionWithinBuffer_UpdatesStateCorrectly()
//     {
//         // Arrange
//         string buffer = "Hello";
//         // Start with initial position offset 0
//         Cursor cursor = new Cursor(buffer);
//         // Create a new TextPosition with an offset within the buffer (offset = 1, line = 1, column = 2)
//         TextPosition newPosition = new TextPosition(1, 1, 2);
//         // Act
//         InvokeResetPositionNotInlined(cursor, newPosition);
//         // Assert
//         Assert.Equal(1, cursor.Offset);
//         Assert.False(cursor.Eof);
//         Assert.Equal(buffer[1], cursor.Current);
//     }
// 
//     /// <summary>
//     /// Tests that ResetPositionNotInlined sets the Eof flag to true and the Current character to NullChar when the new position is at or beyond the buffer length.
//     /// Expected Outcome: Cursor.Offset is updated, Eof is true, and Current equals Cursor.NullChar.
//     /// </summary>
//     [Fact]
//     public void ResetPositionNotInlined_PositionAtBufferEnd_SetsEofTrueAndCurrentToNullChar()
//     {
//         // Arrange
//         string buffer = "Hello";
//         Cursor cursor = new Cursor(buffer);
//         // Create a new TextPosition with an offset equal to the buffer length.
//         TextPosition newPosition = new TextPosition(buffer.Length, 2, 1);
//         // Act
//         InvokeResetPositionNotInlined(cursor, newPosition);
//         // Assert
//         Assert.Equal(buffer.Length, cursor.Offset);
//         Assert.True(cursor.Eof);
//         Assert.Equal(Cursor.NullChar, cursor.Current);
//     }
// 
//     /// <summary>
//     /// Tests that ResetPositionNotInlined throws an IndexOutOfRangeException when the new position has a negative offset.
//     /// Expected Outcome: An IndexOutOfRangeException is thrown due to accessing the buffer with an invalid index.
//     /// </summary>
//     [Fact]
//     public void ResetPositionNotInlined_NegativeOffset_ThrowsIndexOutOfRangeException()
//     {
//         // Arrange
//         string buffer = "Hello";
//         Cursor cursor = new Cursor(buffer);
//         // Create a new TextPosition with a negative offset.
//         TextPosition newPosition = new TextPosition(-1, 0, 0);
//         // Act & Assert
//         Assert.Throws<IndexOutOfRangeException>(() => InvokeResetPositionNotInlined(cursor, newPosition));
//     }
// 
//     /// <summary>
//     /// Tests that PeekNext with the default index returns the character immediately after the current offset.
//     /// Assumes initial offset to be zero, so for a buffer "abc", PeekNext() should return 'b'.
//     /// </summary>
//     [Fact]
//     public void PeekNext_WithDefaultIndex_ReturnsNextCharacter()
//     {
//         // Arrange
//         string buffer = "abc";
//         var cursor = new Cursor(buffer);
//         // Act
//         char result = cursor.PeekNext();
//         // Assert
//         Assert.Equal(buffer[1], result);
//     }
// 
//     /// <summary>
//     /// Tests that PeekNext returns NullChar when the calculated index is equal to or exceeds buffer length.
//     /// For a buffer "abc", calling PeekNext(3) should return NullChar.
//     /// </summary>
//     [Fact]
//     public void PeekNext_WithIndexExceedingBounds_ReturnsNullChar()
//     {
//         // Arrange
//         string buffer = "abc";
//         var cursor = new Cursor(buffer);
//         // Act
//         char result = cursor.PeekNext(3);
//         // Assert
//         Assert.Equal(Cursor.NullChar, result);
//     }
// 
//     /// <summary>
//     /// Tests that PeekNext returns NullChar when a negative index is provided.
//     /// Since the computed index becomes negative, it should return NullChar.
//     /// </summary>
//     [Fact]
//     public void PeekNext_WithNegativeIndex_ReturnsNullChar()
//     {
//         // Arrange
//         string buffer = "abc";
//         var cursor = new Cursor(buffer);
//         // Act
//         char result = cursor.PeekNext(-1);
//         // Assert
//         Assert.Equal(Cursor.NullChar, result);
//     }
// 
//     /// <summary>
//     /// Tests that PeekNext returns the correct character for a valid custom index.
//     /// For a buffer "abcdef", PeekNext(2) should return 'c' (assuming offset starts at zero).
//     /// </summary>
//     [Fact]
//     public void PeekNext_WithCustomValidIndex_ReturnsCorrectCharacter()
//     {
//         // Arrange
//         string buffer = "abcdef";
//         var cursor = new Cursor(buffer);
//         // Act
//         char result = cursor.PeekNext(2);
//         // Assert
//         Assert.Equal(buffer[2], result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(char) method to verify it returns the expected result when comparing the current character.
//     /// </summary>
//     /// <param name = "buffer">The input buffer used to initialize the cursor.</param>
//     /// <param name = "charToMatch">The character to compare with the current character.</param>
//     /// <param name = "expected">The expected boolean result from the Match method.</param>
//     [Theory]
//     [InlineData("abc", 'a', true)]
//     [InlineData("abc", 'b', false)]
//     [InlineData("", '\0', true)]
//     [InlineData("", 'x', false)]
//     public void Match_Char_CurrentCharacterComparison_ReturnsExpectedResult(string buffer, char charToMatch, bool expected)
//     {
//         // Arrange
//         var cursor = new Cursor(buffer);
//         // Act
//         bool result = cursor.Match(charToMatch);
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan&lt;char&gt;) method to verify it returns the expected result when matching a span at the current position.
//     /// </summary>
//     /// <param name = "buffer">The input buffer used to initialize the cursor.</param>
//     /// <param name = "spanToMatch">The string (as a span) expected to match the beginning of the buffer.</param>
//     /// <param name = "expected">The expected boolean result from the Match method.</param>
//     [Theory]
//     [InlineData("abcdef", "abc", true)]
//     [InlineData("abcdef", "abd", false)]
//     [InlineData("abc", "abcdef", false)]
//     [InlineData("abcdef", "", true)]
//     public void Match_Span_AtCurrentPosition_ReturnsExpectedResult(string buffer, string spanToMatch, bool expected)
//     {
//         // Arrange
//         var cursor = new Cursor(buffer);
//         ReadOnlySpan<char> span = spanToMatch.AsSpan();
//         // Act
//         bool result = cursor.Match(span);
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan&lt;char&gt;, StringComparison) method to verify it returns the expected result 
//     /// when matching a span at the current position using a specified string comparison.
//     /// </summary>
//     /// <param name = "buffer">The input buffer used to initialize the cursor.</param>
//     /// <param name = "spanToMatch">The string (as a span) expected to match the beginning of the buffer.</param>
//     /// <param name = "comparisonInt">An integer representing the StringComparison enum value.</param>
//     /// <param name = "expected">The expected boolean result from the Match method.</param>
//     [Theory]
//     [InlineData("abcdef", "abc", (int)StringComparison.Ordinal, true)]
//     [InlineData("abcdef", "Abc", (int)StringComparison.Ordinal, false)]
//     [InlineData("abcdef", "Abc", (int)StringComparison.OrdinalIgnoreCase, true)]
//     [InlineData("abcdef", "", (int)StringComparison.Ordinal, true)]
//     public void Match_SpanWithComparison_UsingSpecifiedComparison_ReturnsExpectedResult(string buffer, string spanToMatch, int comparisonInt, bool expected)
//     {
//         // Arrange
//         var cursor = new Cursor(buffer);
//         ReadOnlySpan<char> span = spanToMatch.AsSpan();
//         StringComparison comparison = (StringComparison)comparisonInt;
//         // Act
//         bool result = cursor.Match(span, comparison);
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests the MatchAnyOf method when the cursor's Eof property is true.
//     /// Expected to return false regardless of the input span.
//     /// </summary>
//     [Fact]
//     public void MatchAnyOf_WhenEofIsTrue_ReturnsFalse()
//     {
//         // Arrange
//         // An empty buffer simulates the Eof condition.
//         var cursor = new Cursor(string.Empty);
//         // Act
//         // Even with a non-empty span, the method should return false as Eof is true.
//         bool result = cursor.MatchAnyOf("abc".AsSpan());
//         // Assert
//         Assert.False(result, "MatchAnyOf should return false when the cursor is at end-of-file.");
//     }
// 
//     /// <summary>
//     /// Tests the MatchAnyOf method when the provided span is empty and the cursor is not at end-of-file.
//     /// Expected to return true as per method logic.
//     /// </summary>
//     [Fact]
//     public void MatchAnyOf_WhenSpanIsEmptyAndEofIsFalse_ReturnsTrue()
//     {
//         // Arrange
//         // A non-empty buffer ensures the cursor is not at end-of-file.
//         var cursor = new Cursor("abc");
//         // Act
//         // An empty span should return true when the cursor is not at Eof.
//         bool result = cursor.MatchAnyOf(ReadOnlySpan<char>.Empty);
//         // Assert
//         Assert.True(result, "MatchAnyOf should return true when passed an empty span and the cursor is not at end-of-file.");
//     }
// 
//     /// <summary>
//     /// Tests the MatchAnyOf method with non-empty spans when the cursor is not at end-of-file.
//     /// The result should depend on whether the current character is found within the span.
//     /// </summary>
//     /// <param name = "buffer">The buffer used to initialize the cursor. The first character is considered the current character.</param>
//     /// <param name = "spanInput">The string to be converted to a ReadOnlySpan for matching.</param>
//     /// <param name = "expectedResult">The expected boolean outcome of the match.</param>
//     [Theory]
//     [InlineData("cat", "act", true)]
//     [InlineData("dog", "abc", false)]
//     [InlineData("M", "AM", true)]
//     [InlineData("X", "YZ", false)]
//     public void MatchAnyOf_WithNonEmptySpan_ReturnsExpectedResult(string buffer, string spanInput, bool expectedResult)
//     {
//         // Arrange
//         var cursor = new Cursor(buffer);
//         // Act
//         bool result = cursor.MatchAnyOf(spanInput.AsSpan());
//         // Assert
//         Assert.Equal(expectedResult, result);
//     }
// 
// #region Match(ReadOnlySpan<char> s) tests
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan&lt;char&gt;) method when the provided span exactly matches the beginning of the buffer.
//     /// Expected outcome: returns true.
//     /// </summary>
//     /// <param name = "buffer">The string buffer for the cursor.</param>
//     /// <param name = "spanToMatch">The string to match as a ReadOnlySpan&lt;char&gt;.</param>
//     /// <param name = "expected">The expected boolean result.</param>
//     [Theory]
//     [InlineData("Hello World", "Hello", true)]
//     [InlineData("Hello", "World", false)]
//     [InlineData("Hi", "Hello", false)]
//     [InlineData("Any", "", true)]
//     public void Match_ReadOnlySpan_SimpleScenarios_ReturnExpected(string buffer, string spanToMatch, bool expected)
//     {
//         // Arrange
//         var cursor = new Cursor(buffer);
//         // Act
//         bool result = cursor.Match(spanToMatch.AsSpan());
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
// #endregion
// #region Match(char c) tests
//     /// <summary>
//     /// Tests the Match(char) method when the current character in the buffer matches the provided character.
//     /// Expected outcome: returns true.
//     /// </summary>
//     [Fact]
//     public void Match_Char_CurrentCharMatches_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "Hello";
//         var cursor = new Cursor(buffer);
//         // Act
//         bool result = cursor.Match(buffer[0]);
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(char) method when the current character in the buffer does not match the provided character.
//     /// Expected outcome: returns false.
//     /// </summary>
//     [Fact]
//     public void Match_Char_CurrentCharDoesNotMatch_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "Hello";
//         var cursor = new Cursor(buffer);
//         // The first character is 'H'; testing with a different character.
//         char nonMatchingChar = 'e';
//         // Act
//         bool result = cursor.Match(nonMatchingChar);
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(char) method when the buffer is empty.
//     /// Expected outcome: returns false since there is no current character.
//     /// </summary>
//     [Fact]
//     public void Match_Char_EmptyBuffer_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "";
//         var cursor = new Cursor(buffer);
//         // Act
//         bool result = cursor.Match('a');
//         // Assert
//         Assert.False(result);
//     }
// 
// #endregion
// #region Match(ReadOnlySpan<char>, StringComparison) tests
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan&lt;char&gt;, StringComparison) method using ordinal comparison when the span matches the beginning of the buffer.
//     /// Expected outcome: returns true.
//     /// </summary>
//     [Theory]
//     [InlineData("Hello World", "Hell", StringComparison.Ordinal, true)]
//     [InlineData("Hello World", "World", StringComparison.Ordinal, false)]
//     [InlineData("Hi", "Hello", StringComparison.Ordinal, false)]
//     [InlineData("Any", "", StringComparison.Ordinal, true)]
//     public void Match_WithComparison_OrdinalScenarios_ReturnExpected(string buffer, string spanToMatch, StringComparison comparison, bool expected)
//     {
//         // Arrange
//         var cursor = new Cursor(buffer);
//         // Act
//         bool result = cursor.Match(spanToMatch.AsSpan(), comparison);
//         // Assert
//         Assert.Equal(expected, result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan&lt;char&gt;, StringComparison) method using OrdinalIgnoreCase comparison when the span matches the beginning of the buffer regardless of case.
//     /// Expected outcome: returns true.
//     /// </summary>
//     [Fact]
//     public void Match_WithComparison_IgnoreCase_Matching_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "Hello";
//         var cursor = new Cursor(buffer);
//         string spanToMatch = "hello";
//         // Act
//         bool result = cursor.Match(spanToMatch.AsSpan(), StringComparison.OrdinalIgnoreCase);
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan&lt;char&gt;, StringComparison) method using ordinal comparison where casing differences cause a mismatch.
//     /// Expected outcome: returns false.
//     /// </summary>
//     [Fact]
//     public void Match_WithComparison_Ordinal_CaseSensitiveMismatch_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "Hello";
//         var cursor = new Cursor(buffer);
//         string spanToMatch = "hello"; // Lowercase does not match in Ordinal
//         // Act
//         bool result = cursor.Match(spanToMatch.AsSpan(), StringComparison.Ordinal);
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(char) method when the current character matches the provided character.
//     /// Expected outcome: returns true.
//     /// </summary>
//     [Fact]
//     public void Match_Char_WhenMatchingCharacter_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "HelloWorld";
//         var cursor = new Cursor(buffer);
//         // The current character is assumed to be the first character of the buffer: 'H'
//         // Act
//         bool result = cursor.Match('H');
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(char) method when the current character does not match the provided character.
//     /// Expected outcome: returns false.
//     /// </summary>
//     [Fact]
//     public void Match_Char_WhenNonMatchingCharacter_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "HelloWorld";
//         var cursor = new Cursor(buffer);
//         // The current character is assumed to be 'H', so comparing with 'X' should fail.
//         // Act
//         bool result = cursor.Match('X');
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan<char>) method when the span at the current position matches the provided span.
//     /// Expected outcome: returns true.
//     /// </summary>
//     [Fact]
//     public void Match_String_WhenMatchingPrefix_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "HelloWorld";
//         var cursor = new Cursor(buffer);
//         // Testing with a span that represents the prefix "Hello"
//         ReadOnlySpan<char> matchSpan = "Hello".AsSpan();
//         // Act
//         bool result = cursor.Match(matchSpan);
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan<char>) method when the span at the current position does not match the provided span.
//     /// Expected outcome: returns false.
//     /// </summary>
//     [Fact]
//     public void Match_String_WhenNonMatchingPrefix_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "HelloWorld";
//         var cursor = new Cursor(buffer);
//         // Testing with a span that does not match the beginning of the buffer.
//         ReadOnlySpan<char> matchSpan = "World".AsSpan();
//         // Act
//         bool result = cursor.Match(matchSpan);
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan<char>, StringComparison) method when the buffer is long enough and the span matches using ordinal comparison.
//     /// Expected outcome: returns true.
//     /// </summary>
//     [Fact]
//     public void Match_StringComparison_WhenMatchingWithOrdinal_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "HelloWorld";
//         var cursor = new Cursor(buffer);
//         ReadOnlySpan<char> matchSpan = "Hello".AsSpan();
//         // Act
//         bool result = cursor.Match(matchSpan, StringComparison.Ordinal);
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan<char>, StringComparison) method when the buffer is not long enough for the given span.
//     /// Expected outcome: returns false.
//     /// </summary>
//     [Fact]
//     public void Match_StringComparison_WhenBufferTooShort_ReturnsFalse()
//     {
//         // Arrange
//         string buffer = "Hi";
//         var cursor = new Cursor(buffer);
//         // The span length exceeds the remaining buffer length.
//         ReadOnlySpan<char> matchSpan = "Hello".AsSpan();
//         // Act
//         bool result = cursor.Match(matchSpan, StringComparison.Ordinal);
//         // Assert
//         Assert.False(result);
//     }
// 
//     /// <summary>
//     /// Tests the Match(ReadOnlySpan<char>, StringComparison) method using a case-insensitive comparison.
//     /// Expected outcome: returns true when texts match irrespective of case.
//     /// </summary>
//     [Fact]
//     public void Match_StringComparison_WhenMatchingIgnoringCase_ReturnsTrue()
//     {
//         // Arrange
//         string buffer = "helloWorld";
//         var cursor = new Cursor(buffer);
//         // The provided span compares "HELLO" with a case-insensitive comparison.
//         ReadOnlySpan<char> matchSpan = "HELLO".AsSpan();
//         // Act
//         bool result = cursor.Match(matchSpan, StringComparison.OrdinalIgnoreCase);
//         // Assert
//         Assert.True(result);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "Cursor(string, in TextPosition)"/> constructor with a non-empty buffer and a custom <see cref = "TextPosition"/>.
//     /// Expected outcome: The Buffer property is set to the provided value, Eof is false, Current is set to the character at the specified offset, and Offset is 0.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithNonEmptyBufferAndCustomTextPosition_SetsInitialValues()
//     {
//         // Arrange
//         string testBuffer = "Hello";
//         // Assuming TextPosition has a public constructor taking (offset, line, column).
//         // Here, offset 1 should select the second character ('e').
//         var position = new TextPosition(1, 1, 2);
//         char expectedCurrent = testBuffer[1];
//         // Act
//         var cursor = new Cursor(testBuffer, position);
//         // Assert
//         Assert.Equal(testBuffer, cursor.Buffer);
//         Assert.False(cursor.Eof);
//         Assert.Equal(expectedCurrent, cursor.Current);
//         Assert.Equal(0, cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "Cursor(string, in TextPosition)"/> constructor with an empty buffer.
//     /// Expected outcome: The Buffer property is set to an empty string, Eof is true, Current is set to <see cref = "Cursor.NullChar"/>, and Offset is 0.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithEmptyBufferAndAnyTextPosition_SetsEofAndNullChar()
//     {
//         // Arrange
//         string testBuffer = "";
//         // The specific TextPosition is irrelevant here since the buffer is empty.
//         var position = new TextPosition(0, 1, 1);
//         // Act
//         var cursor = new Cursor(testBuffer, position);
//         // Assert
//         Assert.Equal(testBuffer, cursor.Buffer);
//         Assert.True(cursor.Eof);
//         Assert.Equal(Cursor.NullChar, cursor.Current);
//         Assert.Equal(0, cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests the <see cref = "Cursor(string)"/> constructor which uses <see cref = "TextPosition.Start"/>.
//     /// Expected outcome: The Buffer property is set to the provided value, Eof is false, Current is set using <see cref = "TextPosition.Start"/>'s offset (assumed 0), and Offset is 0.
//     /// </summary>
//     [Fact]
//     public void Constructor_WithNonEmptyBuffer_UsingDefaultStartPosition_SetsInitialValues()
//     {
//         // Arrange
//         string testBuffer = "Hello";
//         // Assuming TextPosition.Start initializes the offset to 0.
//         char expectedCurrent = testBuffer[0];
//         // Act
//         var cursor = new Cursor(testBuffer);
//         // Assert
//         Assert.Equal(testBuffer, cursor.Buffer);
//         Assert.False(cursor.Eof);
//         Assert.Equal(expectedCurrent, cursor.Current);
//         Assert.Equal(0, cursor.Offset);
//     }
// 
//     /// <summary>
//     /// Tests that the Cursor(string) constructor initializes correctly with a valid non-null buffer.
//     /// Expected: Cursor instance is created with Buffer matching input and Eof is false when buffer is not empty.
//     /// </summary>
//     [Fact]
//     public void CursorConstructor_WithValidBuffer_ShouldInitializeProperties()
//     {
//         // Arrange
//         var buffer = "Test buffer content";
//         // Act
//         var cursor = new Cursor(buffer);
//         // Assert
//         Assert.Equal(buffer, cursor.Buffer);
//         if (buffer.Length > 0)
//         {
//             Assert.False(cursor.Eof);
//         }
//     }
// 
//     /// <summary>
//     /// Tests that the Cursor(string, TextPosition) constructor initializes correctly with a valid non-null buffer and a specified text position.
//     /// Expected: Cursor instance is created with Buffer matching input and Position equal to the provided text position.
//     /// </summary>
//     [Fact]
//     public void CursorConstructor_WithValidBufferAndPosition_ShouldInitializeProperties()
//     {
//         // Arrange
//         var buffer = "Another test buffer";
//         var textPosition = TextPosition.Start; // Assuming TextPosition.Start is defined and represents the start position.
//         // Act
//         var cursor = new Cursor(buffer, textPosition);
//         // Assert
//         Assert.Equal(buffer, cursor.Buffer);
//         Assert.Equal(textPosition, cursor.Position);
//     }
// 
//     /// <summary>
//     /// Tests that the constructors correctly handle an empty buffer.
//     /// Expected: Cursor is created with an empty Buffer and Eof is true.
//     /// </summary>
//     [Fact]
//     public void CursorConstructor_WithEmptyBuffer_ShouldSetEofTrue()
//     {
//         // Arrange
//         var buffer = string.Empty;
//         // Act
//         var cursor = new Cursor(buffer);
//         // Assert
//         Assert.Equal(buffer, cursor.Buffer);
//         Assert.True(cursor.Eof);
//     }
// 
//     /// <summary>
//     /// Tests that the Cursor(string) constructor throws an ArgumentNullException when a null buffer is provided.
//     /// Expected: An ArgumentNullException is thrown.
//     /// </summary>
//     [Fact]
//     public void CursorConstructor_NullBufferWithoutPosition_ShouldThrowArgumentNullException()
//     {
//         // Arrange
//         string buffer = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Cursor(buffer));
//     }
// 
//     /// <summary>
//     /// Tests that the Cursor(string, TextPosition) constructor throws an ArgumentNullException when a null buffer is provided.
//     /// Expected: An ArgumentNullException is thrown.
//     /// </summary>
//     [Fact]
//     public void CursorConstructor_NullBufferWithPosition_ShouldThrowArgumentNullException()
//     {
//         // Arrange
//         string buffer = null;
//         var textPosition = TextPosition.Start;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Cursor(buffer, textPosition));
//     }
// 
//     /// <summary>
//     /// Tests that the Position property returns the default starting position when using the default constructor.
//     /// </summary>
//     [Fact]
//     public void Position_WithDefaultConstructor_ReturnsStartPosition()
//     {
//         // Arrange
//         string buffer = "abcdef";
//         // Assuming TextPosition.Start returns the default text position.
//         TextPosition expected = TextPosition.Start;
//         // Act
//         Cursor cursor = new Cursor(buffer);
//         TextPosition actual = cursor.Position;
//         // Assert
//         Assert.Equal(expected.Offset, actual.Offset);
//         Assert.Equal(expected.Line, actual.Line);
//         Assert.Equal(expected.Column, actual.Column);
//     }
// 
//     /// <summary>
//     /// Tests that the Position property returns the custom starting position provided to the constructor.
//     /// </summary>
//     [Fact]
//     public void Position_WithCustomPosition_ReturnsProvidedPosition()
//     {
//         // Arrange
//         string buffer = "example";
//         // Creating a custom text position. Assuming TextPosition has a constructor that takes an offset, line, and column.
//         TextPosition customPosition = new TextPosition(2, 3, 4);
//         // Act
//         Cursor cursor = new Cursor(buffer, in customPosition);
//         TextPosition actual = cursor.Position;
//         // Assert
//         Assert.Equal(customPosition.Offset, actual.Offset);
//         Assert.Equal(customPosition.Line, actual.Line);
//         Assert.Equal(customPosition.Column, actual.Column);
//     }
// 
//     /// <summary>
//     /// Tests that the Span property returns the entire buffer as a ReadOnlySpan when the cursor is at its initial position.
//     /// This verifies that the Span property correctly calls Buffer.AsSpan with the starting Offset.
//     /// </summary>
//     [Fact]
//     public void Span_InitialCursor_ReturnsFullBufferSpan()
//     {
//         // Arrange
//         string buffer = "Hello, World!";
//         // Creating cursor with the default constructor which uses TextPosition.Start (assumed offset 0).
//         Cursor cursor = new Cursor(buffer);
//         ReadOnlySpan<char> expectedSpan = buffer.AsSpan(0);
//         // Act
//         ReadOnlySpan<char> actualSpan = cursor.Span;
//         // Assert
//         Assert.Equal(expectedSpan.ToString(), actualSpan.ToString());
//     }
// 
//     /// <summary>
//     /// Tests that the Span property returns an empty ReadOnlySpan when an empty buffer is provided.
//     /// Expected outcome: The returned span is empty.
//     /// </summary>
//     [Fact]
//     public void Span_WithEmptyBuffer_ReturnsEmptySpan()
//     {
//         // Arrange
//         string buffer = string.Empty;
//         Cursor cursor = new Cursor(buffer);
//         ReadOnlySpan<char> expectedSpan = buffer.AsSpan(0);
//         // Act
//         ReadOnlySpan<char> actualSpan = cursor.Span;
//         // Assert
//         Assert.Equal(expectedSpan.ToString(), actualSpan.ToString());
//         Assert.Equal(0, actualSpan.Length);
//     }
// 
//     /// <summary>
//     /// Validates that the Current property returns the first character of the buffer for a non-empty string.
//     /// Functional steps:
//     /// 1. Create a Cursor instance with a non-empty string buffer.
//     /// 2. Retrieve the Current property.
//     /// Expected outcome: The Current property equals the first character of the input buffer.
//     /// </summary>
//     [Fact]
//     public void Current_WithNonEmptyBuffer_ReturnsFirstCharacter()
//     {
//         // Arrange
//         string buffer = "hello";
//         char expected = buffer[0];
//         // Act
//         Cursor cursor = new Cursor(buffer);
//         char actual = cursor.Current;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Validates that the Current property returns the null character when an empty string buffer is provided.
//     /// Functional steps:
//     /// 1. Create a Cursor instance with an empty string.
//     /// 2. Retrieve the Current property.
//     /// Expected outcome: The Current property equals Cursor.NullChar, indicating nothing is present at the position.
//     /// </summary>
//     [Fact]
//     public void Current_WithEmptyBuffer_ReturnsNullChar()
//     {
//         // Arrange
//         string buffer = string.Empty;
//         char expected = Cursor.NullChar;
//         // Act
//         Cursor cursor = new Cursor(buffer);
//         char actual = cursor.Current;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Validates that the Current property returns the only character available for a single-character buffer.
//     /// Functional steps:
//     /// 1. Create a Cursor instance with a single-character string.
//     /// 2. Retrieve the Current property.
//     /// Expected outcome: The Current property equals the only character in the buffer.
//     /// </summary>
//     [Fact]
//     public void Current_WithSingleCharacterBuffer_ReturnsThatCharacter()
//     {
//         // Arrange
//         string buffer = "Z";
//         char expected = 'Z';
//         // Act
//         Cursor cursor = new Cursor(buffer);
//         char actual = cursor.Current;
//         // Assert
//         Assert.Equal(expected, actual);
//     }
// 
//     /// <summary>
//     /// Tests that the 'Offset' property is initialized to zero when a Cursor is created with a non-empty buffer.
//     /// This verifies the expected default behavior of the Offset property immediately following instantiation.
//     /// </summary>
//     [Fact]
//     public void Offset_InitializedWithNonEmptyBuffer_ShouldBeZero()
//     {
//         // Arrange
//         string buffer = "Sample text for Cursor initialization";
//         // Act
//         var cursor = new Cursor(buffer);
//         int actualOffset = cursor.Offset;
//         // Assert
//         Assert.Equal(0, actualOffset);
//     }
// 
//     /// <summary>
//     /// Tests that the 'Offset' property is initialized to zero when a Cursor is created with an empty buffer.
//     /// Even with an empty string as a buffer, the starting offset should be zero.
//     /// </summary>
//     [Fact]
//     public void Offset_InitializedWithEmptyBuffer_ShouldBeZero()
//     {
//         // Arrange
//         string buffer = string.Empty;
//         // Act
//         var cursor = new Cursor(buffer);
//         int actualOffset = cursor.Offset;
//         // Assert
//         Assert.Equal(0, actualOffset);
//     }
// 
//     /// <summary>
//     /// Verifies that initializing the Cursor with an empty buffer sets the Eof property to true.
//     /// This test arranges an empty string as the input buffer, constructs the Cursor, and asserts that Eof is true.
//     /// </summary>
//     [Fact]
//     public void Eof_EmptyBuffer_ReturnsTrue()
//     {
//         // Arrange
//         string emptyBuffer = string.Empty;
//         // Act
//         var cursor = new Cursor(emptyBuffer);
//         // Assert
//         Assert.True(cursor.Eof, "Expected Eof to be true when the buffer is empty.");
//     }
// 
//     /// <summary>
//     /// Verifies that initializing the Cursor with a non-empty buffer sets the Eof property to false.
//     /// This test arranges a non-empty string as the input buffer, constructs the Cursor, and asserts that Eof is false.
//     /// </summary>
//     [Fact]
//     public void Eof_NonEmptyBuffer_ReturnsFalse()
//     {
//         // Arrange
//         string nonEmptyBuffer = "abc";
//         // Act
//         var cursor = new Cursor(nonEmptyBuffer);
//         // Assert
//         Assert.False(cursor.Eof, "Expected Eof to be false when the buffer is non-empty.");
//     }
// 
//     /// <summary>
//     /// Verifies that the Buffer property returns the same non-empty string provided during construction.
//     /// Arrange: Provide a non-empty buffer string.
//     /// Act: Construct a Cursor instance.
//     /// Assert: The Buffer property value is equal to the input string.
//     /// </summary>
//     [Fact]
//     public void Buffer_WhenConstructedWithNonEmptyString_ReturnsSameString()
//     {
//         // Arrange
//         string expectedBuffer = "This is a test buffer.";
//         // Act
//         Cursor cursor = new Cursor(expectedBuffer);
//         // Assert
//         Assert.Equal(expectedBuffer, cursor.Buffer);
//     }
// 
//     /// <summary>
//     /// Verifies that the Buffer property returns an empty string when an empty string is provided during construction.
//     /// Arrange: Provide an empty buffer string.
//     /// Act: Construct a Cursor instance.
//     /// Assert: The Buffer property value is an empty string.
//     /// </summary>
//     [Fact]
//     public void Buffer_WhenConstructedWithEmptyString_ReturnsEmptyString()
//     {
//         // Arrange
//         string expectedBuffer = string.Empty;
//         // Act
//         Cursor cursor = new Cursor(expectedBuffer);
//         // Assert
//         Assert.Equal(expectedBuffer, cursor.Buffer);
//     }
// 
//     /// <summary>
//     /// Verifies that constructing a Cursor with a null buffer throws an ArgumentNullException.
//     /// Arrange: Provide a null buffer string.
//     /// Act & Assert: Expect an ArgumentNullException during construction.
//     /// </summary>
//     [Fact]
//     public void Buffer_WhenConstructedWithNullString_ThrowsArgumentNullException()
//     {
//         // Arrange
//         string nullBuffer = null;
//         // Act & Assert
//         Assert.Throws<ArgumentNullException>(() => new Cursor(nullBuffer));
//     }
// }