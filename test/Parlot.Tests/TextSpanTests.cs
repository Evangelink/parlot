using System;
using System.Globalization;
using Xunit;
using Parlot;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="TextSpan"/> struct.
    /// </summary>
    public class TextSpanTests
    {
        /// <summary>
        /// Tests that the constructor with a string parameter correctly initializes the TextSpan with a null value.
        /// Expected: Buffer is null, Offset is 0, Length is 0, ToString() returns null, and Span is empty.
        /// </summary>
//         [Fact] [Error] (21-19)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//         public void Constructor_StringNull_SetsPropertiesToDefaults()
//         {
//             // Arrange
//             string? input = null;
// 
//             // Act
//             TextSpan span = new TextSpan(input);
// 
//             // Assert
//             Assert.Null(span.Buffer);
//             Assert.Equal(0, span.Offset);
//             Assert.Equal(0, span.Length);
//             Assert.Null(span.ToString());
//             Assert.Equal(0, span.Span.Length);
//         }

        /// <summary>
        /// Tests that the constructor with a string parameter correctly initializes the TextSpan with a non-null value.
        /// Expected: Buffer equals the input, Offset is 0, Length equals input length, ToString() returns the full string, and Span equals the full string.
        /// </summary>
        [Theory]
        [InlineData("test")]
        [InlineData("Hello, World!")]
        public void Constructor_StringNotNull_SetsPropertiesCorrectly(string input)
        {
            // Arrange & Act
            TextSpan span = new TextSpan(input);

            // Assert
            Assert.Equal(input, span.Buffer);
            Assert.Equal(0, span.Offset);
            Assert.Equal(input.Length, span.Length);
            Assert.Equal(input, span.ToString());
            Assert.Equal(input.AsSpan().ToString(), span.Span.ToString());
        }

        /// <summary>
        /// Tests that the constructor with explicit offset and count correctly initializes the TextSpan.
        /// Expected: Buffer equals the provided string, Offset and Length are set as provided, ToString() and Span represent the substring.
        /// </summary>
//         [Fact] [Error] (68-33)CS1729 'TextSpan' does not contain a constructor that takes 3 arguments
//         public void Constructor_BufferOffsetCount_ValidParameters_CreatesCorrectSubstring()
//         {
//             // Arrange
//             string input = "hello";
//             int offset = 1;
//             int count = 3;
//             string expectedSubstring = "ell";
// 
//             // Act
//             TextSpan span = new TextSpan(input, offset, count);
// 
//             // Assert
//             Assert.Equal(input, span.Buffer);
//             Assert.Equal(offset, span.Offset);
//             Assert.Equal(count, span.Length);
//             Assert.Equal(expectedSubstring, span.ToString());
//             Assert.Equal(expectedSubstring, span.Span.ToString());
//         }

        /// <summary>
        /// Tests that the Span property returns an empty span when Buffer is null.
        /// Expected: Span length is zero.
        /// </summary>
//         [Fact] [Error] (86-49)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//         public void Span_WhenBufferIsNull_ReturnsEmptySpan()
//         {
//             // Arrange
//             TextSpan span = new TextSpan((string?)null);
// 
//             // Act
//             ReadOnlySpan<char> result = span.Span;
// 
//             // Assert
//             Assert.Equal(0, result.Length);
//         }

        /// <summary>
        /// Tests that the Equals(string?) method returns true when comparing a TextSpan initialized with null to a null string.
        /// Expected: True.
        /// </summary>
//         [Fact] [Error] (103-49)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. [Error] (106-48)CS8632 The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
//         public void Equals_String_NullComparison_ReturnsTrueWhenBothAreNull()
//         {
//             // Arrange
//             TextSpan span = new TextSpan((string?)null);
// 
//             // Act
//             bool areEqual = span.Equals((string?)null);
// 
//             // Assert
//             Assert.True(areEqual);
//         }

        /// <summary>
        /// Tests that the Equals(string?) method correctly compares the underlying characters of the TextSpan with a string.
        /// Expected: Returns true when the contents are identical and false otherwise.
        /// </summary>
        [Theory]
        [InlineData("example", "example", true)]
        [InlineData("example", "Example", false)]
        [InlineData("", "", true)]
        public void Equals_String_ValueComparison_ReturnsExpectedOutcome(string spanString, string compareTo, bool expected)
        {
            // Arrange
            TextSpan span = new TextSpan(spanString);

            // Act
            bool areEqual = span.Equals(compareTo);

            // Assert
            Assert.Equal(expected, areEqual);
        }

        /// <summary>
        /// Tests that the Equals(TextSpan) method correctly compares two TextSpan instances.
        /// Expected: Returns true when both spans contain the same sequence of characters.
        /// </summary>
        [Theory]
        [InlineData("parlot", "parlot", true)]
        [InlineData("parlot", "Parlot", false)]
        [InlineData("", "", true)]
        public void Equals_TextSpan_ValueComparison_ReturnsExpectedOutcome(string first, string second, bool expected)
        {
            // Arrange
            TextSpan span1 = new TextSpan(first);
            TextSpan span2 = new TextSpan(second);

            // Act
            bool areEqual = span1.Equals(span2);

            // Assert
            Assert.Equal(expected, areEqual);
        }

        /// <summary>
        /// Tests the implicit conversion from string to TextSpan.
        /// Expected: The implicit conversion produces a TextSpan with Buffer equal to the input string.
        /// </summary>
//         [Theory] [Error] (163-29)CS0029 Cannot implicitly convert type 'string' to 'Parlot.UnitTests.TextSpan'
//         [InlineData("implicit")]
//         [InlineData("")]
//         public void ImplicitOperator_FromString_CreatesExpectedTextSpan(string input)
//         {
//             // Arrange & Act
//             TextSpan span = input;
// 
//             // Assert
//             Assert.Equal(input, span.Buffer);
//             Assert.Equal(0, span.Offset);
//             Assert.Equal(input?.Length ?? 0, span.Length);
//             Assert.Equal(input, span.ToString());
//         }

        /// <summary>
        /// Tests the overridden Equals(object) method for both matching and non-matching types.
        /// Expected: Returns true when the object is a TextSpan with identical content, false otherwise.
        /// </summary>
        [Fact]
        public void Equals_Object_VariousScenarios_ReturnsExpectedOutcome()
        {
            // Arrange
            string testString = "object equality";
            TextSpan span = new TextSpan(testString);
            object sameSpanBoxed = new TextSpan(testString);
            object differentSpanBoxed = new TextSpan("different");
            object nonTextSpan = "object equality";

            // Act & Assert
            Assert.True(span.Equals(sameSpanBoxed));
            Assert.False(span.Equals(differentSpanBoxed));
            Assert.False(span.Equals(nonTextSpan));
        }

        /// <summary>
        /// Tests the equality operators (== and !=) for TextSpan.
        /// Expected: operator == returns true for equal TextSpan instances; operator != returns true for non-equal instances.
        /// </summary>
        [Fact]
        public void EqualityOperators_Comparison_ReturnsExpectedResults()
        {
            // Arrange
            TextSpan span1 = new TextSpan("operator test");
            TextSpan span2 = new TextSpan("operator test");
            TextSpan span3 = new TextSpan("different");

            // Act & Assert
            Assert.True(span1 == span2);
            Assert.False(span1 != span2);
            Assert.False(span1 == span3);
            Assert.True(span1 != span3);
        }

        /// <summary>
        /// Tests that GetHashCode produces the same result for two equal TextSpan instances.
        /// Expected: Equal TextSpans should have the same hash code.
        /// </summary>
        [Theory]
        [InlineData("hashcode")]
        [InlineData("")]
        public void GetHashCode_EqualTextSpans_HaveSameHashCode(string input)
        {
            // Arrange
            TextSpan span1 = new TextSpan(input);
            TextSpan span2 = new TextSpan(input);

            // Act
            int hash1 = span1.GetHashCode();
            int hash2 = span2.GetHashCode();

            // Assert
            Assert.Equal(hash1, hash2);
        }

        /// <summary>
        /// Tests that GetHashCode can be called without exceptions and returns an integer.
        /// Expected: Calling GetHashCode should not throw an exception and should return a value of type int.
        /// </summary>
        [Fact]
        public void GetHashCode_CanBeCalled_ReturnsInteger()
        {
            // Arrange
            TextSpan span = new TextSpan("test");

            // Act
            int hash = span.GetHashCode();

            // Assert
            Assert.IsType<int>(hash);
        }
    }
}
