using System;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="TextSpan"/> struct.
    /// </summary>
    public class TextSpanTests
    {
        /// <summary>
        /// Tests that the constructor TextSpan(string? value) correctly sets properties when given a null value.
        /// Expected outcome: Buffer is null, Offset is 0, Length is 0, Span returns an empty span, and ToString returns null.
        /// </summary>
        [Fact]
        public void Constructor_StringValueIsNull_PropertiesSetCorrectly()
        {
            // Arrange & Act
            var textSpan = new TextSpan((string?)null);

            // Assert
            Assert.Null(textSpan.Buffer);
            Assert.Equal(0, textSpan.Offset);
            Assert.Equal(0, textSpan.Length);
            Assert.True(textSpan.Span.Length == 0, "Expected empty span when Buffer is null.");
            Assert.Null(textSpan.ToString());
        }

        /// <summary>
        /// Tests that the constructor TextSpan(string? value) correctly sets properties when given a non-null string.
        /// Expected outcome: Buffer equals input string, Offset is 0, Length equals the string's length, and Span returns the full string.
        /// </summary>
        [Theory]
        [InlineData("hello")]
        [InlineData("world")]
        [InlineData("")]
        public void Constructor_StringValueIsNotNull_PropertiesSetCorrectly(string value)
        {
            // Arrange & Act
            var textSpan = new TextSpan(value);

            // Assert
            Assert.Equal(value, textSpan.Buffer);
            Assert.Equal(0, textSpan.Offset);
            int expectedLength = value?.Length ?? 0;
            Assert.Equal(expectedLength, textSpan.Length);
            if (value != null)
            {
                string spanString = new string(textSpan.Span);
                Assert.Equal(value, spanString);
                Assert.Equal(value, textSpan.ToString());
            }
            else
            {
                Assert.Equal(0, textSpan.Span.Length);
                Assert.Null(textSpan.ToString());
            }
        }

        /// <summary>
        /// Tests that the constructor TextSpan(string? buffer, int offset, int count) correctly sets properties for valid input.
        /// Expected outcome: Span and ToString return the expected substring.
        /// </summary>
        [Theory]
        [InlineData("Parlot", 1, 3, "arl")]
        [InlineData("UnitTests", 0, 5, "UnitT")]
        [InlineData("Boundary", 7, 1, "y")]
        public void Constructor_BufferAndOffsets_ValidInput_ReturnsExpectedSubstring(string buffer, int offset, int count, string expectedSubstring)
        {
            // Arrange & Act
            var textSpan = new TextSpan(buffer, offset, count);

            // Assert
            Assert.Equal(buffer, textSpan.Buffer);
            Assert.Equal(offset, textSpan.Offset);
            Assert.Equal(count, textSpan.Length);
            string spanString = new string(textSpan.Span);
            Assert.Equal(expectedSubstring, spanString);
            Assert.Equal(expectedSubstring, textSpan.ToString());
        }

        /// <summary>
        /// Tests that accessing Span property throws an exception when offset and count are invalid.
        /// Expected outcome: ArgumentOutOfRangeException is thrown.
        /// </summary>
        [Theory]
        [InlineData("error", -1, 3)]
        [InlineData("error", 2, 10)]
        public void Span_InvalidOffsetOrCount_ThrowsArgumentOutOfRangeException(string buffer, int offset, int count)
        {
            // Arrange
            var textSpan = new TextSpan(buffer, offset, count);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                // Accessing Span will trigger the exception from Buffer.AsSpan if parameters are invalid.
                var _ = textSpan.Span;
            });
        }

        /// <summary>
        /// Tests the Equals(string?) method for matching and non-matching cases.
        /// Expected outcome: Returns true when the span matches the provided string, false otherwise.
        /// </summary>
        [Theory]
        [InlineData("example", "example", true)]
        [InlineData("example", "Example", false)]
        [InlineData("Test", "TestTest", false)]
        public void Equals_String_Condition_ReturnsExpectedOutcome(string source, string compareTo, bool expectedResult)
        {
            // Arrange
            var textSpan = new TextSpan(source);

            // Act
            bool result = textSpan.Equals(compareTo);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Tests the Equals(string?) method when both the TextSpan and the string are null.
        /// Expected outcome: Returns true.
        /// </summary>
        [Fact]
        public void Equals_String_BothNull_ReturnsTrue()
        {
            // Arrange
            var textSpan = new TextSpan((string?)null);

            // Act
            bool result = textSpan.Equals((string?)null);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Tests the Equals(TextSpan) method for matching and non-matching TextSpan instances.
        /// Expected outcome: Returns true when spans are equal, false otherwise.
        /// </summary>
        [Theory]
        [InlineData("match", "match", true)]
        [InlineData("match", "Match", false)]
        [InlineData("hello", "hello world", false)]
        public void Equals_TextSpan_Condition_ReturnsExpectedOutcome(string value1, string value2, bool expectedResult)
        {
            // Arrange
            var span1 = new TextSpan(value1);
            var span2 = new TextSpan(value2);

            // Act
            bool result = span1.Equals(span2);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        /// <summary>
        /// Tests the object.Equals override for comparing a TextSpan with another object.
        /// Expected outcome: Returns true when comparing two equal TextSpans and false for different types.
        /// </summary>
        [Fact]
        public void Equals_Object_MixedTypes_ReturnsExpectedOutcome()
        {
            // Arrange
            var span1 = new TextSpan("objectTest");
            var span2 = new TextSpan("objectTest");
            object objSpan = new TextSpan("objectTest");
            object nonSpan = "objectTest";

            // Act & Assert
            Assert.True(span1.Equals(objSpan));
            Assert.False(span1.Equals(nonSpan)); // nonSpan is not a TextSpan instance.
            Assert.False(span1.Equals(null));
        }

        /// <summary>
        /// Tests the implicit conversion from string to TextSpan.
        /// Expected outcome: The converted TextSpan has the same Buffer, Offset, Length, and Span as derived from the string.
        /// </summary>
        [Theory]
        [InlineData("implicit")]
        [InlineData("")]
        public void ImplicitConversion_FromString_SetsPropertiesCorrectly(string value)
        {
            // Arrange & Act
            TextSpan textSpan = value; // implicit conversion

            // Assert
            Assert.Equal(value, textSpan.Buffer);
            Assert.Equal(0, textSpan.Offset);
            int expectedLength = value?.Length ?? 0;
            Assert.Equal(expectedLength, textSpan.Length);
            if (value != null)
            {
                string spanString = new string(textSpan.Span);
                Assert.Equal(value, spanString);
            }
            else
            {
                Assert.Equal(0, textSpan.Span.Length);
            }
        }

        /// <summary>
        /// Tests that GetHashCode returns the same hash code for two equal TextSpan instances.
        /// Expected outcome: Hash codes are identical for equal spans.
        /// </summary>
        [Theory]
        [InlineData("hashcode")]
        [InlineData("TestHash")]
        [InlineData("")]
        public void GetHashCode_EqualTextSpans_ReturnSameHashCode(string value)
        {
            // Arrange
            var span1 = new TextSpan(value);
            var span2 = new TextSpan(value);

            // Act
            int hash1 = span1.GetHashCode();
            int hash2 = span2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        /// <summary>
        /// Tests the equality (==) and inequality (!=) operators for TextSpan.
        /// Expected outcome: Returns true for equal spans using == and false for !=; opposite for non-equal spans.
        /// </summary>
        [Theory]
        [InlineData("operator", "operator", true)]
        [InlineData("operator", "Operator", false)]
        public void Operators_EqualityAndInequality_ReturnExpectedResults(string value1, string value2, bool expectedEqual)
        {
            // Arrange
            var span1 = new TextSpan(value1);
            var span2 = new TextSpan(value2);

            // Act
            bool areEqual = span1 == span2;
            bool areNotEqual = span1 != span2;

            // Assert
            Assert.Equal(expectedEqual, areEqual);
            Assert.Equal(!expectedEqual, areNotEqual);
        }
    }
}
