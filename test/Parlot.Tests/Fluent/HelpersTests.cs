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
        /// Tests that when no flags are set, the ToNumberStyles extension method returns NumberStyles.None.
        /// </summary>
//         [Fact] [Error] (25-43)CS1061 'NumberOptions' does not contain a definition for 'ToNumberStyles' and no accessible extension method 'ToNumberStyles' accepting a first argument of type 'NumberOptions' could be found (are you missing a using directive or an assembly reference?)
//         public void ToNumberStyles_NoFlags_ReturnsNone()
//         {
//             // Arrange
//             // Assuming that the underlying NumberOptions is a [Flags] enum,
//             // using (NumberOptions)0 represents no flags set.
//             NumberOptions options = 0;
// 
//             // Act
//             NumberStyles result = options.ToNumberStyles();
// 
//             // Assert
//             Assert.Equal(NumberStyles.None, result);
//         }

        /// <summary>
        /// Tests that when only the AllowLeadingSign flag is set, the ToNumberStyles extension method returns NumberStyles.AllowLeadingSign.
        /// </summary>
//         [Fact] [Error] (41-43)CS1061 'NumberOptions' does not contain a definition for 'ToNumberStyles' and no accessible extension method 'ToNumberStyles' accepting a first argument of type 'NumberOptions' could be found (are you missing a using directive or an assembly reference?)
//         public void ToNumberStyles_AllowLeadingSignOnly_ReturnsAllowLeadingSign()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowLeadingSign;
// 
//             // Act
//             NumberStyles result = options.ToNumberStyles();
// 
//             // Assert
//             Assert.Equal(NumberStyles.AllowLeadingSign, result);
//         }

        /// <summary>
        /// Tests that when only the AllowDecimalSeparator flag is set, the ToNumberStyles extension method returns NumberStyles.AllowDecimalPoint.
        /// </summary>
//         [Fact] [Error] (57-43)CS1061 'NumberOptions' does not contain a definition for 'ToNumberStyles' and no accessible extension method 'ToNumberStyles' accepting a first argument of type 'NumberOptions' could be found (are you missing a using directive or an assembly reference?)
//         public void ToNumberStyles_AllowDecimalSeparatorOnly_ReturnsAllowDecimalPoint()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowDecimalSeparator;
// 
//             // Act
//             NumberStyles result = options.ToNumberStyles();
// 
//             // Assert
//             Assert.Equal(NumberStyles.AllowDecimalPoint, result);
//         }

        /// <summary>
        /// Tests that when only the AllowGroupSeparators flag is set, the ToNumberStyles extension method returns NumberStyles.AllowThousands.
        /// </summary>
//         [Fact] [Error] (73-43)CS1061 'NumberOptions' does not contain a definition for 'ToNumberStyles' and no accessible extension method 'ToNumberStyles' accepting a first argument of type 'NumberOptions' could be found (are you missing a using directive or an assembly reference?)
//         public void ToNumberStyles_AllowGroupSeparatorsOnly_ReturnsAllowThousands()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowGroupSeparators;
// 
//             // Act
//             NumberStyles result = options.ToNumberStyles();
// 
//             // Assert
//             Assert.Equal(NumberStyles.AllowThousands, result);
//         }

        /// <summary>
        /// Tests that when only the AllowExponent flag is set, the ToNumberStyles extension method returns NumberStyles.AllowExponent.
        /// </summary>
//         [Fact] [Error] (89-43)CS1061 'NumberOptions' does not contain a definition for 'ToNumberStyles' and no accessible extension method 'ToNumberStyles' accepting a first argument of type 'NumberOptions' could be found (are you missing a using directive or an assembly reference?)
//         public void ToNumberStyles_AllowExponentOnly_ReturnsAllowExponent()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowExponent;
// 
//             // Act
//             NumberStyles result = options.ToNumberStyles();
// 
//             // Assert
//             Assert.Equal(NumberStyles.AllowExponent, result);
//         }

        /// <summary>
        /// Tests that when a combination of flags (AllowLeadingSign and AllowDecimalSeparator) are set,
        /// the ToNumberStyles extension method returns the combined NumberStyles.
        /// </summary>
//         [Fact] [Error] (106-43)CS1061 'NumberOptions' does not contain a definition for 'ToNumberStyles' and no accessible extension method 'ToNumberStyles' accepting a first argument of type 'NumberOptions' could be found (are you missing a using directive or an assembly reference?)
//         public void ToNumberStyles_CombinedFlags_ReturnsCombinedNumberStyles()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowLeadingSign | NumberOptions.AllowDecimalSeparator;
// 
//             // Act
//             NumberStyles result = options.ToNumberStyles();
// 
//             // Assert
//             NumberStyles expected = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
//             Assert.Equal(expected, result);
//         }

        /// <summary>
        /// Tests that when all flags are set, the ToNumberStyles extension method returns
        /// the combination of all corresponding NumberStyles.
        /// </summary>
//         [Fact] [Error] (127-43)CS1061 'NumberOptions' does not contain a definition for 'ToNumberStyles' and no accessible extension method 'ToNumberStyles' accepting a first argument of type 'NumberOptions' could be found (are you missing a using directive or an assembly reference?)
//         public void ToNumberStyles_AllFlagsSet_ReturnsAllCombinedNumberStyles()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.AllowLeadingSign
//                                     | NumberOptions.AllowDecimalSeparator
//                                     | NumberOptions.AllowGroupSeparators
//                                     | NumberOptions.AllowExponent;
// 
//             // Act
//             NumberStyles result = options.ToNumberStyles();
// 
//             // Assert
//             NumberStyles expected = NumberStyles.AllowLeadingSign
//                                     | NumberStyles.AllowDecimalPoint
//                                     | NumberStyles.AllowThousands
//                                     | NumberStyles.AllowExponent;
//             Assert.Equal(expected, result);
//         }
    }
}
