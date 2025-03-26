using System;
using System.Numerics;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="NumberLiterals"/> class.
    /// </summary>
    public class NumberLiteralsTests
    {
        /// <summary>
        /// Tests that the CreateNumberLiteralParser method returns a non-null parser instance when called with int type and default parameters.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_WithInt_DefaultParameters_ReturnsNonNullParser()
        {
            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<int>();

            // Assert
            Assert.NotNull(parser);
        }

        /// <summary>
        /// Tests that the CreateNumberLiteralParser method returns a non-null parser instance when called with byte type and default parameters.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_WithByte_DefaultParameters_ReturnsNonNullParser()
        {
            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<byte>();

            // Assert
            Assert.NotNull(parser);
        }

        /// <summary>
        /// Tests that the CreateNumberLiteralParser method returns a non-null parser instance when called with double type and default parameters.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_WithDouble_DefaultParameters_ReturnsNonNullParser()
        {
            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<double>();

            // Assert
            Assert.NotNull(parser);
        }

        /// <summary>
        /// Tests that the CreateNumberLiteralParser method returns a non-null parser instance when called with BigInteger type and default parameters.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_WithBigInteger_DefaultParameters_ReturnsNonNullParser()
        {
            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<BigInteger>();

            // Assert
            Assert.NotNull(parser);
        }

#if NET6_0_OR_GREATER
        /// <summary>
        /// Tests that the CreateNumberLiteralParser method returns a non-null parser instance when called with Half type and default parameters.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_WithHalf_DefaultParameters_ReturnsNonNullParser()
        {
            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<Half>();

            // Assert
            Assert.NotNull(parser);
        }
#endif

        /// <summary>
        /// Tests that the CreateNumberLiteralParser method returns a non-null parser instance when called with custom decimal and group separators.
        /// </summary>
        [Theory]
        [InlineData('.', ',')]
        [InlineData(',', '.')]
        [InlineData('X', 'Y')]
        public void CreateNumberLiteralParser_WithCustomSeparators_ReturnsNonNullParser(char customDecimalSeparator, char customGroupSeparator)
        {
            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<int>(
                numberOptions: NumberOptions.Number, 
                decimalSeparator: customDecimalSeparator, 
                groupSeparator: customGroupSeparator);

            // Assert
            Assert.NotNull(parser);
        }

        /// <summary>
        /// Tests that the CreateNumberLiteralParser method throws a NotSupportedException when called with an unsupported type.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_WithUnsupportedType_ThrowsNotSupportedException()
        {
            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() => NumberLiterals.CreateNumberLiteralParser<string>());
            Assert.Contains("The type", exception.Message);
            Assert.Contains("is not supported", exception.Message);
        }
    }
}
