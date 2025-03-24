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
        /// Tests the ToNumberStyles extension method when no flags are set, expecting NumberStyles.None.
        /// </summary>
        [Fact]
        public void ToNumberStyles_NoOptions_ReturnsNone()
        {
            // Arrange
            NumberOptions options = 0; // No flags set.
            
            // Act
            NumberStyles result = options.ToNumberStyles();
            
            // Assert
            Assert.Equal(NumberStyles.None, result);
        }
        
        /// <summary>
        /// Tests the ToNumberStyles extension method with the AllowLeadingSign flag set, expecting NumberStyles.AllowLeadingSign.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllowLeadingSign_ReturnsAllowLeadingSign()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowLeadingSign;
            
            // Act
            NumberStyles result = options.ToNumberStyles();
            
            // Assert
            Assert.Equal(NumberStyles.AllowLeadingSign, result);
        }
        
        /// <summary>
        /// Tests the ToNumberStyles extension method with the AllowDecimalSeparator flag set, expecting NumberStyles.AllowDecimalPoint.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllowDecimalSeparator_ReturnsAllowDecimalPoint()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowDecimalSeparator;
            
            // Act
            NumberStyles result = options.ToNumberStyles();
            
            // Assert
            Assert.Equal(NumberStyles.AllowDecimalPoint, result);
        }
        
        /// <summary>
        /// Tests the ToNumberStyles extension method with the AllowGroupSeparators flag set, expecting NumberStyles.AllowThousands.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllowGroupSeparators_ReturnsAllowThousands()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowGroupSeparators;
            
            // Act
            NumberStyles result = options.ToNumberStyles();
            
            // Assert
            Assert.Equal(NumberStyles.AllowThousands, result);
        }
        
        /// <summary>
        /// Tests the ToNumberStyles extension method with the AllowExponent flag set, expecting NumberStyles.AllowExponent.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllowExponent_ReturnsAllowExponent()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowExponent;
            
            // Act
            NumberStyles result = options.ToNumberStyles();
            
            // Assert
            Assert.Equal(NumberStyles.AllowExponent, result);
        }
        
        /// <summary>
        /// Tests the ToNumberStyles extension method with all defined flags set, expecting the combination of the corresponding NumberStyles.
        /// </summary>
        [Fact]
        public void ToNumberStyles_AllOptions_ReturnsCombinedNumberStyles()
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
        /// Tests the ToNumberStyles extension method with an undefined flag; expects that undefined flags do not alter the outcome.
        /// </summary>
        [Fact]
        public void ToNumberStyles_UndefinedFlag_IgnoresFlagAndReturnsNone()
        {
            // Arrange
            // Choosing a flag value that does not correspond to any defined NumberOptions.
            NumberOptions options = (NumberOptions)16;
            
            // Act
            NumberStyles result = options.ToNumberStyles();
            
            // Assert
            Assert.Equal(NumberStyles.None, result);
        }
        
        /// <summary>
        /// Tests the ToNumberStyles extension method with a combination of a defined flag and an undefined flag.
        /// Only the defined flag should contribute to the result.
        /// </summary>
        [Fact]
        public void ToNumberStyles_DefinedAndUndefinedFlags_ReturnsOnlyDefinedFlagResult()
        {
            // Arrange
            NumberOptions options = NumberOptions.AllowLeadingSign | (NumberOptions)16;
            
            // Act
            NumberStyles result = options.ToNumberStyles();
            
            // Assert
            Assert.Equal(NumberStyles.AllowLeadingSign, result);
        }
    }
}
