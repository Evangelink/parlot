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
        /// Tests that calling ToNumberStyles with no options returns NumberStyles.None.
        /// </summary>
//         [Fact] [Error] (23-35)CS0122 'Helpers' is inaccessible due to its protection level
//         public void ToNumberStyles_NoFlags_ReturnsNone()
//         {
//             // Arrange
//             NumberOptions options = (NumberOptions)0; // No flags set
// 
//             // Act
//             NumberStyles result = Helpers.ToNumberStyles(options);
// 
//             // Assert
//             Assert.Equal(NumberStyles.None, result);
//         }

        /// <summary>
        /// Tests that calling ToNumberStyles with only AllowLeadingSign returns NumberStyles.AllowLeadingSign.
        /// </summary>
//         [Fact] [Error] (39-35)CS0122 'Helpers' is inaccessible due to its protection level
//         public void ToNumberStyles_AllowLeadingSignOnly_ReturnsAllowLeadingSign()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowLeadingSign;
// 
//             // Act
//             NumberStyles result = Helpers.ToNumberStyles(options);
// 
//             // Assert
//             Assert.Equal(NumberStyles.AllowLeadingSign, result);
//         }

        /// <summary>
        /// Tests that calling ToNumberStyles with only AllowDecimalSeparator returns NumberStyles.AllowDecimalPoint.
        /// </summary>
//         [Fact] [Error] (55-35)CS0122 'Helpers' is inaccessible due to its protection level
//         public void ToNumberStyles_AllowDecimalSeparatorOnly_ReturnsAllowDecimalPoint()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowDecimalSeparator;
// 
//             // Act
//             NumberStyles result = Helpers.ToNumberStyles(options);
// 
//             // Assert
//             Assert.Equal(NumberStyles.AllowDecimalPoint, result);
//         }

        /// <summary>
        /// Tests that calling ToNumberStyles with only AllowGroupSeparators returns NumberStyles.AllowThousands.
        /// </summary>
//         [Fact] [Error] (71-35)CS0122 'Helpers' is inaccessible due to its protection level
//         public void ToNumberStyles_AllowGroupSeparatorsOnly_ReturnsAllowThousands()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowGroupSeparators;
// 
//             // Act
//             NumberStyles result = Helpers.ToNumberStyles(options);
// 
//             // Assert
//             Assert.Equal(NumberStyles.AllowThousands, result);
//         }

        /// <summary>
        /// Tests that calling ToNumberStyles with only AllowExponent returns NumberStyles.AllowExponent.
        /// </summary>
//         [Fact] [Error] (87-35)CS0122 'Helpers' is inaccessible due to its protection level
//         public void ToNumberStyles_AllowExponentOnly_ReturnsAllowExponent()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowExponent;
// 
//             // Act
//             NumberStyles result = Helpers.ToNumberStyles(options);
// 
//             // Assert
//             Assert.Equal(NumberStyles.AllowExponent, result);
//         }

        /// <summary>
        /// Tests that calling ToNumberStyles with a combination of AllowLeadingSign and AllowDecimalSeparator returns the combined NumberStyles.
        /// </summary>
//         [Fact] [Error] (104-35)CS0122 'Helpers' is inaccessible due to its protection level
//         public void ToNumberStyles_CombinationOfLeadingSignAndDecimalSeparator_ReturnsCombinedNumberStyles()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowLeadingSign | NumberOptions.AllowDecimalSeparator;
//             NumberStyles expected = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
// 
//             // Act
//             NumberStyles result = Helpers.ToNumberStyles(options);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }

        /// <summary>
        /// Tests that calling ToNumberStyles with all available flags returns the combined NumberStyles.
        /// </summary>
//         [Fact] [Error] (127-35)CS0122 'Helpers' is inaccessible due to its protection level
//         public void ToNumberStyles_AllFlags_ReturnsAllCombinedNumberStyles()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowLeadingSign 
//                                    | NumberOptions.AllowDecimalSeparator 
//                                    | NumberOptions.AllowGroupSeparators 
//                                    | NumberOptions.AllowExponent;
//             NumberStyles expected = NumberStyles.AllowLeadingSign 
//                                   | NumberStyles.AllowDecimalPoint 
//                                   | NumberStyles.AllowThousands 
//                                   | NumberStyles.AllowExponent;
// 
//             // Act
//             NumberStyles result = Helpers.ToNumberStyles(options);
// 
//             // Assert
//             Assert.Equal(expected, result);
//         }
    }

    // Since NumberOptions is not provided in the test code context, 
    // we define it here for the purpose of compiling and testing. 
    // In the actual project, this enum should be defined in the production code.
    [Flags]
    internal enum NumberOptions
    {
        None = 0,
        AllowLeadingSign = 1,
        AllowDecimalSeparator = 2,
        AllowGroupSeparators = 4,
        AllowExponent = 8
    }
}
