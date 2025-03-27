using System;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Cursor"/> class.
    /// </summary>
    public class CursorTests
    {
        /// <summary>
        /// Tests that the constructor with a non-empty buffer and a specified TextPosition sets the current character correctly,
        /// while initializing the cursor position to the start.
        /// </summary>
        [Fact]
        public void Constructor_NonEmptyBufferWithPosition_SetsCurrentBasedOnGivenPosition()
        {
            // Arrange
            string buffer = "Hello";
            // Assuming TextPosition has a constructor TextPosition(int offset, int line, int column)
            // and that TextPosition.Start equals new TextPosition(0, 1, 1).
            var customPosition = new TextPosition(1, 99, 99);
            // Act
            var cursor = new Cursor(buffer, in customPosition);
            // Assert
            // Despite the provided TextPosition having offset 1, the cursor's Offset is initialized to 0.
            // However, Current is set based on the provided position (i.e. buffer[1]).
            Assert.Equal(buffer[1], cursor.Current);
            Assert.Equal(0, cursor.Offset);
            Assert.False(cursor.Eof);
            // Position property returns the internally tracked position (Offset, _line, _column) which should be (0, 1, 1)
            var pos = cursor.Position;
            Assert.Equal(0, pos.Offset);
            Assert.Equal(1, pos.Line);
            Assert.Equal(1, pos.Column);
        }

        /// <summary>
        /// Tests that the constructor with an empty buffer sets Eof to true and Current to NullChar.
        /// </summary>
        [Fact]
        public void Constructor_EmptyBuffer_SetsEofTrueAndCurrentNullChar()
        {
            // Arrange
            string buffer = "";
            // Act
            var cursor = new Cursor(buffer);
            // Assert
            Assert.True(cursor.Eof);
            Assert.Equal(Cursor.NullChar, cursor.Current);
            Assert.Equal(0, cursor.Offset);
        }

        /// <summary>
        /// Tests the Advance() method for a normal single-character advancement without a newline.
        /// </summary>
        [Fact]
        public void Advance_SingleStep_NoNewLine_UpdatesOffsetAndCurrentCorrectly()
        {
            // Arrange
            string buffer = "abc";
            var cursor = new Cursor(buffer);
            // Initially, Current should be 'a'
            Assert.Equal('a', cursor.Current);
            // Act
            cursor.Advance();
            // Assert
            // After one advance, Offset should be 1 and current character should be 'b'
            Assert.Equal(1, cursor.Offset);
            Assert.Equal('b', cursor.Current);
            // Since no newline was encountered, column should have been incremented.
            var pos = cursor.Position;
            Assert.Equal(2, pos.Column);
            Assert.Equal(1, pos.Line);
        }

        /// <summary>
        /// Tests the Advance() method when encountering a newline, ensuring that line and column counters update appropriately.
        /// </summary>
        [Fact]
        public void Advance_OnNewLine_UpdatesLineAndResetsColumn()
        {
            // Arrange
            // buffer with a newline character in the middle.
            string buffer = "a\nb";
            var cursor = new Cursor(buffer);
            // First advance: from 'a' to '\n'
            cursor.Advance();
            // Act
            // Second advance: from '\n' to 'b'. Expect that _line is incremented and column reset.
            cursor.Advance();
            // Assert
            Assert.Equal(2, cursor.Offset);
            Assert.Equal('b', cursor.Current);
            var pos = cursor.Position;
            // After encountering newline, line increments and column resets to 1 in that advance.
            Assert.Equal(2, pos.Line);
            Assert.Equal(1, pos.Column);
            Assert.False(cursor.Eof);
        }

        /// <summary>
        /// Tests the Advance(int count) method for normal advancement within the buffer.
        /// </summary>
        [Fact]
        public void AdvanceIntCount_NormalAdvance_UpdatesPositionCorrectly()
        {
            // Arrange
            string buffer = "abcdef";
            var cursor = new Cursor(buffer);
            // Act
            cursor.Advance(3);
            // Assert
            // Offset should be advanced by 3 and current should be character at position 3.
            Assert.Equal(3, cursor.Offset);
            Assert.Equal(buffer[3], cursor.Current);
            // Since there are no newline characters in "abcdef", column should increase by count.
            var pos = cursor.Position;
            Assert.Equal(1 + 3, pos.Column);
            Assert.Equal(1, pos.Line);
            Assert.False(cursor.Eof);
        }

        /// <summary>
        /// Tests that Advance(int count) exceeding the buffer length sets Eof to true and Current to NullChar.
        /// </summary>
        [Fact]
        public void AdvanceIntCount_OverAdvance_SetsEofAndNullChar()
        {
            // Arrange
            string buffer = "abc";
            var cursor = new Cursor(buffer);
            // Act
            cursor.Advance(10);
            // Assert
            // When advanced past the end, Eof should be true, Offset set to buffer.Length, Current set to NullChar,
            // and column should be incremented by the extra advancement.
            Assert.True(cursor.Eof);
            Assert.Equal(buffer.Length, cursor.Offset);
            Assert.Equal(Cursor.NullChar, cursor.Current);
        }

        /// <summary>
        /// Tests the AdvanceNoNewLines(int offset) method for normal advancement when no newline is present.
        /// </summary>
        [Fact]
        public void AdvanceNoNewLines_NormalAdvance_UpdatesOffsetAndColumn()
        {
            // Arrange
            string buffer = "abcdef";
            var cursor = new Cursor(buffer);
            // Act
            cursor.AdvanceNoNewLines(3);
            // Assert
            Assert.Equal(3, cursor.Offset);
            Assert.Equal(buffer[3], cursor.Current);
            var pos = cursor.Position;
            Assert.Equal(1 + 3, pos.Column);
            Assert.Equal(1, pos.Line);
            Assert.False(cursor.Eof);
        }

        /// <summary>
        /// Tests that AdvanceNoNewLines(int offset) beyond the end of the buffer sets Eof to true and Current to NullChar.
        /// </summary>
        [Fact]
        public void AdvanceNoNewLines_OverAdvance_SetsEofAndNullChar()
        {
            // Arrange
            string buffer = "abc";
            var cursor = new Cursor(buffer);
            // Act
            cursor.AdvanceNoNewLines(5);
            // Assert
            Assert.True(cursor.Eof);
            Assert.Equal(buffer.Length, cursor.Offset);
            Assert.Equal(Cursor.NullChar, cursor.Current);
        }

        /// <summary>
        /// Tests that ResetPosition(in TextPosition) correctly updates the cursor's internal state to the specified position.
        /// </summary>
        [Fact]
        public void ResetPosition_ValidPosition_UpdatesCursorState()
        {
            // Arrange
            string buffer = "abcdef";
            var cursor = new Cursor(buffer);
            // First advance so that initial internal offset is different.
            cursor.Advance(2);
            // New target position: choose an offset within buffer, and arbitrary line and column.
            var newPosition = new TextPosition(4, 10, 20);
            // Act
            cursor.ResetPosition(in newPosition);
            // Assert
            Assert.Equal(newPosition.Offset, cursor.Offset);
            // Current should reflect the character at index 4.
            Assert.Equal(buffer[4], cursor.Current);
            // The Position property should return the provided line and column.
            var pos = cursor.Position;
            Assert.Equal(newPosition.Line, pos.Line);
            Assert.Equal(newPosition.Column, pos.Column);
        }

        /// <summary>
        /// Tests the PeekNext(int) method when a valid next character exists.
        /// </summary>
        [Fact]
        public void PeekNext_WithinRange_ReturnsNextCharacter()
        {
            // Arrange
            string buffer = "abc";
            var cursor = new Cursor(buffer);
            // Act
            char peeked = cursor.PeekNext();
            // Assert
            // Initially, Offset is 0 so PeekNext should return the character at index 1.
            Assert.Equal(buffer[1], peeked);
        }

        /// <summary>
        /// Tests the PeekNext(int) method when the next index is out of the buffer's range.
        /// </summary>
        [Fact]
        public void PeekNext_OutOfRange_ReturnsNullChar()
        {
            // Arrange
            string buffer = "abc";
            var cursor = new Cursor(buffer);
            // Move cursor to near the end.
            cursor.Advance(2);
            // Act
            char peeked = cursor.PeekNext();
            // Assert
            // With Offset at 2, the next index (3) is equal to the buffer length, so should return NullChar.
            Assert.Equal(Cursor.NullChar, peeked);
        }

        /// <summary>
        /// Tests that the Span property returns the correct ReadOnlySpan starting at the current offset.
        /// </summary>
        [Fact]
        public void Span_AfterAdvancement_ReturnsCorrectSliceOfBuffer()
        {
            // Arrange
            string buffer = "abcdef";
            var cursor = new Cursor(buffer);
            cursor.AdvanceNoNewLines(2);
            // Act
            ReadOnlySpan<char> span = cursor.Span;
            // Assert
            Assert.Equal(buffer.Substring(cursor.Offset), span.ToString());
        }

        /// <summary>
        /// Tests the Match(char) method returning true when the current character matches.
        /// </summary>
        [Fact]
        public void Match_Char_MatchingCharacter_ReturnsTrue()
        {
            // Arrange
            string buffer = "abc";
            var cursor = new Cursor(buffer);
            // Act & Assert
            Assert.True(cursor.Match('a'));
        }

        /// <summary>
        /// Tests the Match(char) method returning false when the current character does not match.
        /// </summary>
        [Fact]
        public void Match_Char_NonMatchingCharacter_ReturnsFalse()
        {
            // Arrange
            string buffer = "abc";
            var cursor = new Cursor(buffer);
            // Act & Assert
            Assert.False(cursor.Match('z'));
        }

        /// <summary>
        /// Tests the MatchAnyOf(ReadOnlySpan{char}) method returning true when the current character is in the provided span.
        /// </summary>
        [Fact]
        public void MatchAnyOf_WithMatchingCharacter_ReturnsTrue()
        {
            // Arrange
            string buffer = "abc";
            var cursor = new Cursor(buffer);
            ReadOnlySpan<char> span = "xyzabc";
            // Act & Assert
            Assert.True(cursor.MatchAnyOf(span));
        }

        /// <summary>
        /// Tests the MatchAnyOf(ReadOnlySpan{char}) method returning false when the current character is not in the provided span.
        /// </summary>
        [Fact]
        public void MatchAnyOf_WithNonMatchingCharacter_ReturnsFalse()
        {
            // Arrange
            string buffer = "d";
            var cursor = new Cursor(buffer);
            ReadOnlySpan<char> span = "abc";
            // Act & Assert
            Assert.False(cursor.MatchAnyOf(span));
        }

        /// <summary>
        /// Tests the Match(ReadOnlySpan{char}) method returning true when the buffer starting at current offset matches the given span.
        /// </summary>
        [Fact]
        public void Match_StringSpan_Matching_ReturnsTrue()
        {
            // Arrange
            string buffer = "abcdef";
            var cursor = new Cursor(buffer);
            ReadOnlySpan<char> matchSpan = "abc";
            // Act & Assert
            Assert.True(cursor.Match(matchSpan));
        }

        /// <summary>
        /// Tests the Match(ReadOnlySpan{char}) method returning false when the buffer does not have enough characters to match.
        /// </summary>
        [Fact]
        public void Match_StringSpan_InsufficientLength_ReturnsFalse()
        {
            // Arrange
            string buffer = "ab";
            var cursor = new Cursor(buffer);
            ReadOnlySpan<char> matchSpan = "abc";
            // Act & Assert
            Assert.False(cursor.Match(matchSpan));
        }

        /// <summary>
        /// Tests the Match(ReadOnlySpan{char}, StringComparison) method using a case-insensitive comparison.
        /// </summary>
        [Fact]
        public void Match_StringSpanWithComparison_CaseInsensitiveMatching_ReturnsTrue()
        {
            // Arrange
            string buffer = "AbCdEf";
            var cursor = new Cursor(buffer);
            ReadOnlySpan<char> matchSpan = "abc";
            // Act
            bool result = cursor.Match(matchSpan, StringComparison.OrdinalIgnoreCase);
            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Tests the Match(ReadOnlySpan{char}, StringComparison) method returning false when the characters do not match even with comparison.
        /// </summary>
        [Fact]
        public void Match_StringSpanWithComparison_NonMatching_ReturnsFalse()
        {
            // Arrange
            string buffer = "AbCdEf";
            var cursor = new Cursor(buffer);
            ReadOnlySpan<char> matchSpan = "xyz";
            // Act
            bool result = cursor.Match(matchSpan, StringComparison.OrdinalIgnoreCase);
            // Assert
            Assert.False(result);
        }
    }
}
