using System;
using System.Collections.Generic;
using System.Linq;
using Parlot;
using Xunit;

namespace Parlot.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Polyfill"/> class.
    /// </summary>
    public class PolyfillTests
    {
        /// <summary>
        /// Tests the Append extension method with a non-empty collection.
        /// Functional steps: Uses a non-empty list, appends an element, and verifies that the output collection contains the original elements followed by the appended element.
        /// Expected outcome: The sequence returns all elements in the original order with the new element at the end.
        /// </summary>
        [Fact]
        public void Append_WithNonEmptyCollection_AppendsElement()
        {
            // Arrange
            IEnumerable<int> source = new List<int> { 1, 2, 3 };
            int elementToAppend = 4;
            var expected = new List<int> { 1, 2, 3, 4 };

            // Act
            var result = Polyfill.Append(source, elementToAppend);

            // Assert
            Assert.Equal(expected, result.ToList());
        }

        /// <summary>
        /// Tests the Append extension method with an empty collection.
        /// Functional steps: Starts with an empty collection, appends an element, and verifies that the output contains only the new element.
        /// Expected outcome: The returned collection contains exactly one element, the appended element.
        /// </summary>
        [Fact]
        public void Append_WithEmptyCollection_ReturnsSingleElement()
        {
            // Arrange
            IEnumerable<string> source = new List<string>();
            string elementToAppend = "test";
            var expected = new List<string> { "test" };

            // Act
            var result = Polyfill.Append(source, elementToAppend);

            // Assert
            Assert.Equal(expected, result.ToList());
        }

        /// <summary>
        /// Tests the Append extension method when the source is null.
        /// Functional steps: Calls the method with a null source and expects an ArgumentNullException to be thrown.
        /// Expected outcome: The method should throw an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Append_WithNullSource_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<double> source = null;
            double elementToAppend = 1.23;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Polyfill.Append(source, elementToAppend));
        }

        /// <summary>
        /// Tests the Create extension method with a valid length and a delegate that fills the span with a specified character.
        /// Functional steps: Invokes Create with a predetermined length and a delegate that fills each element with a given character, and then compares the result against the expected string.
        /// Expected outcome: Returns a string composed of the specified repeated character.
        /// </summary>
        [Fact]
        public void Create_WithValidLengthAndAction_ReturnsFilledString()
        {
            // Arrange
            string dummy = "ignored";
            int length = 5;
            char fillChar = 'x';
            string expected = new string(fillChar, length);

            // Act
            string result = dummy.Create(length, fillChar, (span, state) =>
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = state;
                }
            });

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the Create extension method with a length of zero.
        /// Functional steps: Invokes Create with zero as the length. The delegate, even if provided, will process an empty span.
        /// Expected outcome: Returns an empty string.
        /// </summary>
        [Fact]
        public void Create_WithZeroLength_ReturnsEmptyString()
        {
            // Arrange
            string dummy = "ignored";
            int length = 0;
            char fillChar = 'y';
            string expected = string.Empty;

            // Act
            string result = dummy.Create(length, fillChar, (span, state) =>
            {
                // No iteration occurs as the span length is zero.
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = state;
                }
            });

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests the Create extension method with a negative length.
        /// Functional steps: Invokes Create with a negative length which leads to an exception during array allocation.
        /// Expected outcome: Throws an exception (either ArgumentOutOfRangeException or OverflowException).
        /// </summary>
        [Fact]
        public void Create_WithNegativeLength_ThrowsException()
        {
            // Arrange
            string dummy = "ignored";
            int length = -1;
            char fillChar = 'z';

            // Act & Assert
            Assert.ThrowsAny<Exception>(() => dummy.Create(length, fillChar, (span, state) =>
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = state;
                }
            }));
        }

        /// <summary>
        /// Tests the Create extension method when a null action delegate is provided.
        /// Functional steps: Invokes Create with a null delegate, expecting a NullReferenceException when the method attempts to invoke the delegate.
        /// Expected outcome: Throws a NullReferenceException.
        /// </summary>
        [Fact]
        public void Create_WithNullAction_ThrowsNullReferenceException()
        {
            // Arrange
            string dummy = "ignored";
            int length = 3;
            char fillChar = 'a';

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => dummy.Create(length, fillChar, null));
        }
    }
}
