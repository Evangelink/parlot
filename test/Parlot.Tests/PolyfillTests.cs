using System.Collections.Generic;
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
        /// Tests the Append extension method with a valid source sequence.
        /// Expected behavior: Returns a new sequence with the appended element.
        /// </summary>
        [Fact]
        public void Append_WithValidSource_ReturnsNewSequenceWithAppendedElement()
        {
            // Arrange
            IEnumerable<int> source = new List<int> { 1, 2, 3 };
            int elementToAppend = 4;

            // Act
            IEnumerable<int> result = Polyfill.Append(source, elementToAppend);

            // Assert
            Assert.NotNull(result);
            var resultList = new List<int>(result);
            Assert.Equal(4, resultList.Count);
            Assert.Equal(new List<int> { 1, 2, 3, 4 }, resultList);
        }

        /// <summary>
        /// Tests the Append extension method with a null source.
        /// Expected behavior: Throws an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Append_WithNullSource_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> source = null;
            int elementToAppend = 5;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Polyfill.Append(source, elementToAppend));
        }

        /// <summary>
        /// Tests the Create extension method with valid parameters.
        /// Expected behavior: Delegate is executed to fill the char array and the constructed string is returned.
        /// </summary>
        [Fact]
        public void Create_WithValidLengthAndDelegate_ReturnsConstructedString()
        {
            // Arrange
            string dummy = "unused"; // The value is ignored by the extension method.
            int length = 5;
            char fillChar = 'A';
            Polyfill.SpanAction<char, char> action = (span, state) =>
            {
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = state;
                }
            };

            // Act
            string result = Polyfill.Create(dummy, length, fillChar, action);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(new string(fillChar, length), result);
        }

        /// <summary>
        /// Tests the Create extension method with a zero length.
        /// Expected behavior: Returns an empty string and the delegate is still invoked.
        /// </summary>
        [Fact]
        public void Create_WithZeroLength_ReturnsEmptyString()
        {
            // Arrange
            string dummy = "unused";
            int length = 0;
            char fillChar = 'B';
            bool delegateCalled = false;
            Polyfill.SpanAction<char, char> action = (span, state) =>
            {
                delegateCalled = true;
                for (int i = 0; i < span.Length; i++)
                {
                    span[i] = state;
                }
            };

            // Act
            string result = Polyfill.Create(dummy, length, fillChar, action);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
            Assert.True(delegateCalled);
        }

        /// <summary>
        /// Tests the Create extension method with a null delegate action.
        /// Expected behavior: Throws a NullReferenceException when attempting to invoke the null delegate.
        /// </summary>
        [Fact]
        public void Create_WithNullAction_ThrowsNullReferenceException()
        {
            // Arrange
            string dummy = "unused";
            int length = 3;
            char fillChar = 'C';
            Polyfill.SpanAction<char, char> action = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => Polyfill.Create(dummy, length, fillChar, action));
        }

        /// <summary>
        /// Tests the Create extension method with a negative length.
        /// Expected behavior: Throws an OverflowException due to invalid array length.
        /// </summary>
        [Fact]
        public void Create_WithNegativeLength_ThrowsOverflowException()
        {
            // Arrange
            string dummy = "unused";
            int negativeLength = -1;
            char fillChar = 'D';
            Polyfill.SpanAction<char, char> action = (span, state) => { };

            // Act & Assert
            Assert.Throws<OverflowException>(() => Polyfill.Create(dummy, negativeLength, fillChar, action));
        }
    }
}
