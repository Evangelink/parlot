using System;
using System.Globalization;
using System.Numerics;
using Parlot.Fluent;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ByteNumberLiteral"/> class.
    /// </summary>
    public class ByteNumberLiteralTests
    {
        private readonly ByteNumberLiteral _parser;

        public ByteNumberLiteralTests()
        {
            _parser = new ByteNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid input and the correct byte value.
        /// </summary>
        [Theory]
        [InlineData("123", (byte)123)]
        [InlineData("0", (byte)0)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, byte expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out byte result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid input.
        /// </summary>
        [Theory]
        [InlineData("300")]
        [InlineData("-1")]
        [InlineData("abc")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out byte result);

            // Assert
            Assert.False(success);
        }

        /// <summary>
        /// Tests that the default ExpectedChars contains digit characters.
        /// </summary>
        [Fact]
        public void Constructor_Default_ExpectedCharsContainsDigits()
        {
            // Arrange
            string expectedDigits = "0123456789";

            // Assert
            foreach (char digit in expectedDigits)
            {
                Assert.Contains(digit, _parser.ExpectedChars);
            }
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="SByteNumberLiteral"/> class.
    /// </summary>
    public class SByteNumberLiteralTests
    {
        private readonly SByteNumberLiteral _parser;

        public SByteNumberLiteralTests()
        {
            _parser = new SByteNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid input and the correct sbyte value.
        /// </summary>
        [Theory]
        [InlineData("127", (sbyte)127)]
        [InlineData("-128", (sbyte)-128)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, sbyte expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out sbyte result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid input.
        /// </summary>
        [Theory]
        [InlineData("128")]
        [InlineData("-129")]
        [InlineData("abc")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out sbyte result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="IntNumberLiteral"/> class.
    /// </summary>
    public class IntNumberLiteralTests
    {
        private readonly IntNumberLiteral _parser;

        public IntNumberLiteralTests()
        {
            _parser = new IntNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid input and the correct int value.
        /// </summary>
        [Theory]
        [InlineData("12345", 12345)]
        [InlineData("-100", -100)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, int expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid input.
        /// </summary>
        [Theory]
        [InlineData("abc")]
        [InlineData("999999999999999999999")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="UIntNumberLiteral"/> class.
    /// </summary>
    public class UIntNumberLiteralTests
    {
        private readonly UIntNumberLiteral _parser;

        public UIntNumberLiteralTests()
        {
            _parser = new UIntNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid input and the correct uint value.
        /// </summary>
        [Theory]
        [InlineData("12345", 12345u)]
        [InlineData("0", 0u)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, uint expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out uint result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid input.
        /// </summary>
        [Theory]
        [InlineData("-1")]
        [InlineData("abc")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out uint result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="LongNumberLiteral"/> class.
    /// </summary>
    public class LongNumberLiteralTests
    {
        private readonly LongNumberLiteral _parser;

        public LongNumberLiteralTests()
        {
            _parser = new LongNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid input and the correct long value.
        /// </summary>
        [Theory]
        [InlineData("123456789", 123456789L)]
        [InlineData("-1000000000", -1000000000L)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, long expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out long result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid input.
        /// </summary>
        [Theory]
        [InlineData("abc")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out long result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="ULongNumberLiteral"/> class.
    /// </summary>
    public class ULongNumberLiteralTests
    {
        private readonly ULongNumberLiteral _parser;

        public ULongNumberLiteralTests()
        {
            _parser = new ULongNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid input and the correct ulong value.
        /// </summary>
        [Theory]
        [InlineData("123456789", 123456789ul)]
        [InlineData("0", 0ul)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, ulong expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid input.
        /// </summary>
        [Theory]
        [InlineData("-1")]
        [InlineData("abc")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="ShortNumberLiteral"/> class.
    /// </summary>
    public class ShortNumberLiteralTests
    {
        private readonly ShortNumberLiteral _parser;

        public ShortNumberLiteralTests()
        {
            _parser = new ShortNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid input and the correct short value.
        /// </summary>
        [Theory]
        [InlineData("12345", (short)12345)]
        [InlineData("-32768", short.MinValue)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, short expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out short result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid input.
        /// </summary>
        [Theory]
        [InlineData("40000")]
        [InlineData("abc")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out short result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="UShortNumberLiteral"/> class.
    /// </summary>
    public class UShortNumberLiteralTests
    {
        private readonly UShortNumberLiteral _parser;

        public UShortNumberLiteralTests()
        {
            _parser = new UShortNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid input and the correct ushort value.
        /// </summary>
        [Theory]
        [InlineData("60000", (ushort)60000)]
        [InlineData("0", (ushort)0)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, ushort expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out ushort result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid input.
        /// </summary>
        [Theory]
        [InlineData("-1")]
        [InlineData("abc")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out ushort result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="DecimalNumberLiteral"/> class.
    /// </summary>
    public class DecimalNumberLiteralTests
    {
        private readonly DecimalNumberLiteral _parser;

        public DecimalNumberLiteralTests()
        {
            _parser = new DecimalNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid decimal input.
        /// </summary>
        [Theory]
        [InlineData("123.45", "123.45")]
        [InlineData("-100.00", "-100.00")]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, string expectedString)
        {
            // Arrange
            var expected = decimal.Parse(expectedString, CultureInfo.InvariantCulture);

            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid decimal input.
        /// </summary>
        [Theory]
        [InlineData("abc")]
        [InlineData("123,45")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="DoubleNumberLiteral"/> class.
    /// </summary>
    public class DoubleNumberLiteralTests
    {
        private readonly DoubleNumberLiteral _parser;

        public DoubleNumberLiteralTests()
        {
            _parser = new DoubleNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid double input.
        /// </summary>
        [Theory]
        [InlineData("123.45", 123.45)]
        [InlineData("-100.00", -100.00)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, double expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Number, CultureInfo.InvariantCulture, out double result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result, 5);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid double input.
        /// </summary>
        [Theory]
        [InlineData("abc")]
        [InlineData("123,45")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Number, CultureInfo.InvariantCulture, out double result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="FloatNumberLiteral"/> class.
    /// </summary>
    public class FloatNumberLiteralTests
    {
        private readonly FloatNumberLiteral _parser;

        public FloatNumberLiteralTests()
        {
            _parser = new FloatNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid float input.
        /// </summary>
        [Theory]
        [InlineData("123.45", 123.45f)]
        [InlineData("-100.00", -100.00f)]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, float expected)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Number, CultureInfo.InvariantCulture, out float result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result, 5);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid float input.
        /// </summary>
        [Theory]
        [InlineData("abc")]
        [InlineData("123,45")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Number, CultureInfo.InvariantCulture, out float result);

            // Assert
            Assert.False(success);
        }
    }

    /// <summary>
    /// Unit tests for the <see cref="BigIntegerNumberLiteral"/> class.
    /// </summary>
    public class BigIntegerNumberLiteralTests
    {
        private readonly BigIntegerNumberLiteral _parser;

        public BigIntegerNumberLiteralTests()
        {
            _parser = new BigIntegerNumberLiteral();
        }

        /// <summary>
        /// Tests that TryParseNumber returns true with valid BigInteger input.
        /// </summary>
        [Theory]
        [InlineData("12345678901234567890", "12345678901234567890")]
        [InlineData("0", "0")]
        public void TryParseNumber_ValidInput_ReturnsTrue(string input, string expectedString)
        {
            // Arrange
            BigInteger expected = BigInteger.Parse(expectedString, CultureInfo.InvariantCulture);

            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger result);

            // Assert
            Assert.True(success);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests that TryParseNumber returns false with invalid BigInteger input.
        /// </summary>
        [Theory]
        [InlineData("abc")]
        public void TryParseNumber_InvalidInput_ReturnsFalse(string input)
        {
            // Act
            bool success = _parser.TryParseNumber(input.AsSpan(), NumberStyles.Integer, CultureInfo.InvariantCulture, out BigInteger result);

            // Assert
            Assert.False(success);
        }
    }
}
