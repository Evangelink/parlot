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
        /// Tests that CreateNumberLiteralParser returns a non-null parser for the type int using default separators.
        /// This validates the happy path for a common numeric type.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_IntWithDefaultSeparators_ReturnsParser()
        {
            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<int>();

            // Assert
            Assert.NotNull(parser);
        }

        /// <summary>
        /// Tests that CreateNumberLiteralParser returns a non-null parser for the type double when custom decimal and group separators are provided.
        /// This ensures that custom formatting options are accepted.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_DoubleWithCustomSeparators_ReturnsParser()
        {
            // Arrange
            char customDecimalSeparator = ',';
            char customGroupSeparator = ' ';

            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<double>(NumberOptions.Number, customDecimalSeparator, customGroupSeparator);

            // Assert
            Assert.NotNull(parser);
        }

#if !NET8_0_OR_GREATER
        /// <summary>
        /// Tests that CreateNumberLiteralParser throws a NotSupportedException when invoked with an unsupported type (e.g. string) in builds prior to .NET8.0.
        /// This validates that only numeric types are allowed.
        /// </summary>
        [Fact]
        public void CreateNumberLiteralParser_UnsupportedType_ThrowsNotSupportedException()
        {
            // Act & Assert
            var exception = Assert.Throws<NotSupportedException>(() => NumberLiterals.CreateNumberLiteralParser<string>());
            Assert.Contains("is not supported", exception.Message);
        }
#endif

        /// <summary>
        /// Tests that CreateNumberLiteralParser returns a valid parser for a variety of numeric types.
        /// This includes testing all supported numeric types by invoking the generic method via reflection.
        /// </summary>
        /// <param name="numericType">The numeric type to test.</param>
        [Theory]
        [InlineData(typeof(byte))]
        [InlineData(typeof(sbyte))]
        [InlineData(typeof(int))]
        [InlineData(typeof(uint))]
        [InlineData(typeof(long))]
        [InlineData(typeof(ulong))]
        [InlineData(typeof(short))]
        [InlineData(typeof(ushort))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(double))]
        [InlineData(typeof(float))]
#if NET6_0_OR_GREATER && !NET8_0_OR_GREATER
        [InlineData(typeof(Half))]
#endif
        [InlineData(typeof(BigInteger))]
        public void CreateNumberLiteralParser_VariousNumericTypes_ReturnsParser(Type numericType)
        {
            // Arrange
            var methodInfo = typeof(NumberLiterals).GetMethod("CreateNumberLiteralParser")
                                                   .MakeGenericMethod(numericType);
            object[] parameters = new object[] { NumberOptions.Number, NumberLiterals.DefaultDecimalSeparator, NumberLiterals.DefaultGroupSeparator };

            // Act
            var parser = methodInfo.Invoke(null, parameters);

            // Assert
            Assert.NotNull(parser);
        }
    }
}
