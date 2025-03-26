using System;
using System.Globalization;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Helpers"/> class.
    /// </summary>
    public class HelpersTests
    {
        /// <summary>
        /// Tests that ToNumberStyles returns NumberStyles.None when no flags are set in NumberOptions.
        /// </summary>
        [Fact]
        public void ToNumberStyles_NoFlagsSet_ReturnsNone()
        {
            // Arrange
            // Assuming default value (0) represents no flags.
            NumberOptions options = 0;

            // Act
            NumberStyles result = options.ToNumberStyles();

            // Assert
            Assert.Equal(NumberStyles.None, result);
        }

        /// <summary>
        /// Tests that ToNumberStyles returns NumberStyles.AllowLeadingSign when the AllowLeadingSign flag is set.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllowLeadingSignSet_ReturnsAllowLeadingSign()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowLeadingSign;

            // Act
            NumberStyles result = options.ToNumberStyles();

            // Assert
            Assert.Equal(NumberStyles.AllowLeadingSign, result);
        }

        /// <summary>
        /// Tests that ToNumberStyles returns NumberStyles.AllowDecimalPoint when the AllowDecimalSeparator flag is set.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllowDecimalSeparatorSet_ReturnsAllowDecimalPoint()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowDecimalSeparator;

            // Act
            NumberStyles result = options.ToNumberStyles();

            // Assert
            Assert.Equal(NumberStyles.AllowDecimalPoint, result);
        }

        /// <summary>
        /// Tests that ToNumberStyles returns NumberStyles.AllowThousands when the AllowGroupSeparators flag is set.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllowGroupSeparatorsSet_ReturnsAllowThousands()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowGroupSeparators;

            // Act
            NumberStyles result = options.ToNumberStyles();

            // Assert
            Assert.Equal(NumberStyles.AllowThousands, result);
        }

        /// <summary>
        /// Tests that ToNumberStyles returns NumberStyles.AllowExponent when the AllowExponent flag is set.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllowExponentSet_ReturnsAllowExponent()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowExponent;

            // Act
            NumberStyles result = options.ToNumberStyles();

            // Assert
            Assert.Equal(NumberStyles.AllowExponent, result);
        }

        /// <summary>
        /// Tests that ToNumberStyles returns the correct combination of NumberStyles when multiple flags are set.
        /// </summary>
        [Fact]
        public void ToNumberStyles_MultipleFlagsSet_ReturnsCombinedNumberStyles()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowLeadingSign 
                                   | NumberOptions.AllowDecimalSeparator 
                                   | NumberOptions.AllowGroupSeparators
                                   | NumberOptions.AllowExponent;
            NumberStyles expected = NumberStyles.AllowLeadingSign
                                  | NumberStyles.AllowDecimalPoint
                                  | NumberStyles.AllowThousands
                                  | NumberStyles.AllowExponent;

            // Act
            NumberStyles result = options.ToNumberStyles();

            // Assert
            Assert.Equal(expected, result);
        }
        
        /// <summary>
        /// Tests that ToNumberStyles returns the correct NumberStyles when a subset of flags is set.
        /// </summary>
        [Fact]
        public void ToNumberStyles_SubsetOfFlagsSet_ReturnsCombinedNumberStyles()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowLeadingSign | NumberOptions.AllowGroupSeparators;
            NumberStyles expected = NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands;

            // Act
            NumberStyles result = options.ToNumberStyles();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
