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
#if !NET6_0_OR_GREATER
        #region Append Method Tests

        /// <summary>
        /// Tests the Append extension method with a non-empty source to verify that the element is added at the end.
        /// </summary>
        [Fact]
        public void Append_WithNonEmptySource_AppendsElement()
        {
            // Arrange
            IEnumerable<int> source = new List<int> { 1, 2, 3 };
            int elementToAppend = 4;
            
            // Act
            var result = source.Append(elementToAppend);
            
            // Assert
            var resultList = result.ToList();
            Assert.Equal(4, resultList.Count);
            Assert.Equal(new List<int> { 1, 2, 3, 4 }, resultList);
        }

        /// <summary>
        /// Tests the Append extension method with an empty source to verify that the element becomes the only item.
        /// </summary>
        [Fact]
        public void Append_WithEmptySource_AppendsElement()
        {
            // Arrange
            IEnumerable<string> source = new List<string>();
            string elementToAppend = "test";
            
            // Act
            var result = source.Append(elementToAppend);
            
            // Assert
            var resultList = result.ToList();
            Assert.Single(resultList);
            Assert.Equal("test", resultList[0]);
        }

        /// <summary>
        /// Tests the Append extension method when a null source is provided to verify that it throws an ArgumentNullException.
        /// </summary>
        [Fact]
        public void Append_WithNullSource_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> source = null;
            int elementToAppend = 3;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => source.Append(elementToAppend));
        }

        #endregion

        #region Create Method Tests

        /// <summary>
        /// Tests the Create extension method to verify that it correctly populates the char array and returns the expected string.
        /// </summary>
        [Fact]
        public void Create_WithValidParameters_ReturnsExpectedString()
        {
            // Arrange
            string ignored = "ignored";
            int length = 5;
            int state = 3;
            // Define an action that fills the array with the character corresponding to the state value.
            Polyfill.SpanAction<char, int> action = (array, s) =>
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = s.ToString()[0];
                }
            };
            
            // Act
            var result = ignored.Create(length, state, action);
            
            // Assert
            Assert.Equal(new string('3', length), result);
        }

        /// <summary>
        /// Tests the Create extension method with a length of zero to verify that it returns an empty string.
        /// </summary>
        [Fact]
        public void Create_WithZeroLength_ReturnsEmptyString()
        {
            // Arrange
            string ignored = "ignored";
            int length = 0;
            int state = 5;
            Polyfill.SpanAction<char, int> action = (array, s) =>
            {
                // Although action is invoked, no iteration will occur as length is zero.
            };
            
            // Act
            var result = ignored.Create(length, state, action);
            
            // Assert
            Assert.Equal(string.Empty, result);
        }

        /// <summary>
        /// Tests the Create extension method when a null action is provided to verify that it throws a NullReferenceException.
        /// </summary>
        [Fact]
        public void Create_WithNullAction_ThrowsNullReferenceException()
        {
            // Arrange
            string ignored = "ignored";
            int length = 5;
            int state = 0;
            Polyfill.SpanAction<char, int> action = null;
            
            // Act & Assert
            Assert.Throws<NullReferenceException>(() => ignored.Create(length, state, action));
        }

        #endregion
#endif
    }
}
