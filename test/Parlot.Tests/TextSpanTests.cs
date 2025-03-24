using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parlot;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="TextSpan"/> struct.
    /// </summary>
    [TestClass]
    public class TextSpanTests
    {
        /// <summary>
        /// Tests that the parameterless constructor correctly initializes a TextSpan when a null string is provided.
        /// Expected: Buffer is null, Offset is 0, Length is 0 and Span is an empty span.
        /// </summary>
        [TestMethod]
        public void Ctor_WithNullString_ShouldInitializeWithZeroLength()
        {
            // Arrange
            string? input = null;

            // Act
            TextSpan textSpan = new TextSpan(input);

            // Assert
            Assert.IsNull(textSpan.Buffer, "Buffer should be null.");
            Assert.AreEqual(0, textSpan.Offset, "Offset should be 0 when constructed with a null string.");
            Assert.AreEqual(0, textSpan.Length, "Length should be 0 when constructed with a null string.");
            Assert.AreEqual(0, textSpan.Span.Length, "Span should be empty when Buffer is null.");
        }

        /// <summary>
        /// Tests that the parameterless constructor correctly initializes a TextSpan when a non-null string is provided.
        /// Expected: Buffer is set to the input string, Offset is 0, Length equals the input string's length.
        /// </summary>
        [TestMethod]
        public void Ctor_WithNonNullString_ShouldInitializeCorrectly()
        {
            // Arrange
            string input = "Hello";

            // Act
            TextSpan textSpan = new TextSpan(input);

            // Assert
            Assert.AreEqual(input, textSpan.Buffer, "Buffer should be equal to the input string.");
            Assert.AreEqual(0, textSpan.Offset, "Offset should be 0 when constructed with a non-null string.");
            Assert.AreEqual(input.Length, textSpan.Length, "Length should equal the length of the input string.");
            CollectionAssert.AreEqual(input.ToCharArray(), textSpan.Span.ToArray(), "Span should match the characters of the input string.");
        }

        /// <summary>
        /// Tests that the constructor with offset and count correctly initializes the TextSpan.
        /// Expected: Span and ToString return the substring defined by offset and count.
        /// </summary>
        [TestMethod]
        public void Ctor_WithOffsetAndCount_ShouldReturnCorrectSubstring()
        {
            // Arrange
            string input = "Hello World";
            int offset = 6;
            int count = 5;

            // Act
            TextSpan textSpan = new TextSpan(input, offset, count);

            // Assert
            Assert.AreEqual(input, textSpan.Buffer, "Buffer should equal the original input string.");
            Assert.AreEqual(offset, textSpan.Offset, "Offset should be as provided.");
            Assert.AreEqual(count, textSpan.Length, "Length should be as provided.");
            string expectedSubstring = "World";
            Assert.AreEqual(expectedSubstring, textSpan.ToString(), "ToString should return the correct substring.");
            CollectionAssert.AreEqual(expectedSubstring.ToCharArray(), textSpan.Span.ToArray(), "Span should return the correct characters for the substring.");
        }

        /// <summary>
        /// Tests the ToString method when the Buffer is null.
        /// Expected: ToString returns null.
        /// </summary>
        [TestMethod]
        public void ToString_WithNullBuffer_ShouldReturnNull()
        {
            // Arrange
            TextSpan textSpan = new TextSpan(null);

            // Act
            string? result = textSpan.ToString();

            // Assert
            Assert.IsNull(result, "ToString should return null when Buffer is null.");
        }

        /// <summary>
        /// Tests the Equals(string?) method when comparing with an equal string.
        /// Expected: Returns true when the content is the same.
        /// </summary>
        [TestMethod]
        public void Equals_String_WithEqualContent_ShouldReturnTrue()
        {
            // Arrange
            string value = "Test";
            TextSpan textSpan = new TextSpan(value);

            // Act
            bool result = textSpan.Equals(value);

            // Assert
            Assert.IsTrue(result, "Equals(string) should return true when the content is equal.");
        }

        /// <summary>
        /// Tests the Equals(string?) method when comparing with a non-equal string.
        /// Expected: Returns false when the content differs.
        /// </summary>
        [TestMethod]
        public void Equals_String_WithDifferentContent_ShouldReturnFalse()
        {
            // Arrange
            TextSpan textSpan = new TextSpan("Test");
            string differentValue = "Test1";

            // Act
            bool result = textSpan.Equals(differentValue);

            // Assert
            Assert.IsFalse(result, "Equals(string) should return false when the content is different.");
        }

        /// <summary>
        /// Tests the Equals(TextSpan) method when comparing two TextSpan instances with equal content.
        /// Expected: Returns true and operator == returns true.
        /// </summary>
        [TestMethod]
        public void Equals_TextSpan_WithEqualContent_ShouldReturnTrue()
        {
            // Arrange
            string value = "Content";
            TextSpan textSpan1 = new TextSpan(value);
            TextSpan textSpan2 = new TextSpan(value);

            // Act
            bool equalsMethod = textSpan1.Equals(textSpan2);
            bool equalityOperator = textSpan1 == textSpan2;

            // Assert
            Assert.IsTrue(equalsMethod, "Equals(TextSpan) should return true when both TextSpans have equal content.");
            Assert.IsTrue(equalityOperator, "Operator == should return true for equal TextSpans.");
        }

        /// <summary>
        /// Tests the Equals(TextSpan) method when comparing two TextSpan instances with different content.
        /// Expected: Returns false and operator != returns true.
        /// </summary>
        [TestMethod]
        public void Equals_TextSpan_WithDifferentContent_ShouldReturnFalse()
        {
            // Arrange
            TextSpan textSpan1 = new TextSpan("Content1");
            TextSpan textSpan2 = new TextSpan("Content2");

            // Act
            bool equalsMethod = textSpan1.Equals(textSpan2);
            bool notEqualOperator = textSpan1 != textSpan2;

            // Assert
            Assert.IsFalse(equalsMethod, "Equals(TextSpan) should return false when TextSpans have different content.");
            Assert.IsTrue(notEqualOperator, "Operator != should return true for TextSpans with different content.");
        }

        /// <summary>
        /// Tests the overridden Equals(object?) method with a TextSpan object.
        /// Expected: Returns true when the object is a TextSpan with equal content.
        /// </summary>
        [TestMethod]
        public void Equals_Object_WithTextSpan_ShouldReturnTrueForEqualContent()
        {
            // Arrange
            string value = "Sample";
            TextSpan textSpan = new TextSpan(value);
            object obj = new TextSpan(value);

            // Act
            bool result = textSpan.Equals(obj);

            // Assert
            Assert.IsTrue(result, "Equals(object) should return true when the object is a TextSpan with equal content.");
        }

        /// <summary>
        /// Tests the overridden Equals(object?) method with a non-TextSpan object.
        /// Expected: Returns false.
        /// </summary>
        [TestMethod]
        public void Equals_Object_WithNonTextSpan_ShouldReturnFalse()
        {
            // Arrange
            TextSpan textSpan = new TextSpan("Sample");
            object obj = "Sample";

            // Act
            bool result = textSpan.Equals(obj);

            // Assert
            Assert.IsFalse(result, "Equals(object) should return false when the object is not a TextSpan.");
        }

        /// <summary>
        /// Tests that GetHashCode returns the same value for two equal TextSpan instances.
        /// Expected: Equal contents produce equal hash codes.
        /// </summary>
        [TestMethod]
        public void GetHashCode_ForEqualTextSpans_ShouldReturnSameHashCode()
        {
            // Arrange
            string value = "HashTest";
            TextSpan textSpan1 = new TextSpan(value);
            TextSpan textSpan2 = new TextSpan(value);

            // Act
            int hash1 = textSpan1.GetHashCode();
            int hash2 = textSpan2.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2, "Equal TextSpan instances should have the same hash code.");
        }

        /// <summary>
        /// Tests the implicit operator conversion from string to TextSpan.
        /// Expected: The converted TextSpan should have the correct Buffer, Offset, and Length.
        /// </summary>
        [TestMethod]
        public void ImplicitOperatorConversion_FromString_ShouldConvertCorrectly()
        {
            // Arrange
            string value = "ImplicitConversion";

            // Act
            TextSpan textSpan = value; // Implicit conversion

            // Assert
            Assert.AreEqual(value, textSpan.Buffer, "Buffer should equal the original string after implicit conversion.");
            Assert.AreEqual(0, textSpan.Offset, "Offset should be 0 after implicit conversion.");
            Assert.AreEqual(value.Length, textSpan.Length, "Length should match the original string length after implicit conversion.");
        }
    }
}
