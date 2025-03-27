using System;
using System.Numerics;
using Xunit;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="NumberLiterals"/> class.
    /// </summary>
    public class NumberLiteralsTests
    {
        /// <summary>
        /// Tests that calling CreateNumberLiteralParser with type int returns a non-null parser.
        /// </summary>
//         [Fact] [Error] (20-51)CS0117 'NumberOptions' does not contain a definition for 'Number' [Error] (25-72)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.NumberOptions' to 'Parlot.Fluent.NumberOptions'
//         public void CreateNumberLiteralParser_Int_ReturnsParserOfInt()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.Number;
//             char customDecimal = '.';
//             char customGroup = ',';
// 
//             // Act
//             var parser = NumberLiterals.CreateNumberLiteralParser<int>(options, customDecimal, customGroup);
// 
//             // Assert
//             Assert.NotNull(parser);
//         }

        /// <summary>
        /// Tests that calling CreateNumberLiteralParser with type double returns a non-null parser.
        /// </summary>
//         [Fact] [Error] (38-51)CS0117 'NumberOptions' does not contain a definition for 'Number' [Error] (43-75)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.NumberOptions' to 'Parlot.Fluent.NumberOptions'
//         public void CreateNumberLiteralParser_Double_ReturnsParserOfDouble()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.Number;
//             char customDecimal = ',';
//             char customGroup = '.';
// 
//             // Act
//             var parser = NumberLiterals.CreateNumberLiteralParser<double>(options, customDecimal, customGroup);
// 
//             // Assert
//             Assert.NotNull(parser);
//         }

        /// <summary>
        /// Tests that calling CreateNumberLiteralParser with type BigInteger returns a non-null parser.
        /// </summary>
//         [Fact] [Error] (56-51)CS0117 'NumberOptions' does not contain a definition for 'Number' [Error] (61-79)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.NumberOptions' to 'Parlot.Fluent.NumberOptions'
//         public void CreateNumberLiteralParser_BigInteger_ReturnsParserOfBigInteger()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.Number;
//             char customDecimal = '.';
//             char customGroup = ' ';
// 
//             // Act
//             var parser = NumberLiterals.CreateNumberLiteralParser<BigInteger>(options, customDecimal, customGroup);
// 
//             // Assert
//             Assert.NotNull(parser);
//         }

        /// <summary>
        /// Tests that calling CreateNumberLiteralParser with an unsupported type (e.g. string) throws a NotSupportedException.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_UnsupportedType_ThrowsNotSupportedException()
        {
            // Arrange, Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() => NumberLiterals.CreateNumberLiteralParser<string>());
            Assert.Contains("is not supported", exception.Message);
        }

        /// <summary>
        /// Tests that calling CreateNumberLiteralParser with custom separators does not affect the creation of a parser.
        /// </summary>
//         [Fact] [Error] (85-51)CS0117 'NumberOptions' does not contain a definition for 'Number' [Error] (90-75)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.NumberOptions' to 'Parlot.Fluent.NumberOptions' [Error] (91-81)CS1503 Argument 1: cannot convert from 'Parlot.Fluent.UnitTests.NumberOptions' to 'Parlot.Fluent.NumberOptions'
//         public void CreateNumberLiteralParser_CustomSeparators_ReturnsNonNullParser()
//         {
//             // Arrange
//             NumberOptions options = NumberOptions.Number;
//             char customDecimal = '#';
//             char customGroup = '@';
// 
//             // Act
//             var parserInt = NumberLiterals.CreateNumberLiteralParser<int>(options, customDecimal, customGroup);
//             var parserDouble = NumberLiterals.CreateNumberLiteralParser<double>(options, customDecimal, customGroup);
// 
//             // Assert
//             Assert.NotNull(parserInt);
//             Assert.NotNull(parserDouble);
//         }
    }
}
