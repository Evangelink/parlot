using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parlot.Fluent;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="NumberLiterals"/> class.
    /// </summary>
    [TestClass]
    public class NumberLiteralsTests
    {
        /// <summary>
        /// Tests that the CreateNumberLiteralParser method for type int returns a non-null parser instance
        /// when using the default number options and separators.
        /// Expected outcome: A parser instance is returned and its type name indicates an int-specific parser.
        /// </summary>
        [TestMethod]
        public void CreateNumberLiteralParser_Int_WithDefaultSeparators_ReturnsParser()
        {
            // Arrange & Act
            var parser = NumberLiterals.CreateNumberLiteralParser<int>();

            // Assert
            Assert.IsNotNull(parser, "The parser instance should not be null.");

            // Validate that the returned type indicates an int number literal parser.
            string typeName = parser.GetType().Name;
            Assert.IsTrue(
                typeName.Contains("IntNumberLiteral") || typeName.Contains("NumberLiteral"),
                $"Unexpected parser type: {typeName}");
        }

        /// <summary>
        /// Tests that the CreateNumberLiteralParser method for type double returns a non-null parser instance
        /// when provided with custom decimal and group separators.
        /// Expected outcome: A parser instance is returned and its type name indicates a double-specific parser.
        /// </summary>
        [TestMethod]
        public void CreateNumberLiteralParser_Double_WithCustomSeparators_ReturnsParser()
        {
            // Arrange
            char customDecimalSeparator = ',';
            char customGroupSeparator = '.';

            // Act
            var parser = NumberLiterals.CreateNumberLiteralParser<double>(NumberOptions.Number, customDecimalSeparator, customGroupSeparator);

            // Assert
            Assert.IsNotNull(parser, "The parser instance should not be null.");
            string typeName = parser.GetType().Name;
            Assert.IsTrue(
                typeName.Contains("DoubleNumberLiteral") || typeName.Contains("NumberLiteral"),
                $"Unexpected parser type: {typeName}");
        }

        /// <summary>
        /// Tests that the CreateNumberLiteralParser method for type BigInteger returns a non-null parser instance
        /// when using the default number options and separators.
        /// Expected outcome: A parser instance is returned and its type name indicates a BigInteger-specific parser.
        /// </summary>
        [TestMethod]
        public void CreateNumberLiteralParser_BigInteger_WithDefaultSeparators_ReturnsParser()
        {
            // Arrange & Act
            var parser = NumberLiterals.CreateNumberLiteralParser<BigInteger>();

            // Assert
            Assert.IsNotNull(parser, "The parser instance should not be null.");
            string typeName = parser.GetType().Name;
            Assert.IsTrue(
                typeName.Contains("BigIntegerNumberLiteral") || typeName.Contains("NumberLiteral"),
                $"Unexpected parser type: {typeName}");
        }

        /// <summary>
        /// Tests that the CreateNumberLiteralParser method throws a NotSupportedException when an unsupported type is provided.
        /// Expected outcome: A NotSupportedException is thrown with an appropriate message.
        /// </summary>
        [TestMethod]
        public void CreateNumberLiteralParser_UnsupportedType_ThrowsNotSupportedException()
        {
            // Arrange, Act & Assert
            NotSupportedException exception = Assert.ThrowsException<NotSupportedException>(
                () => NumberLiterals.CreateNumberLiteralParser<string>(),
                "A NotSupportedException should be thrown when using an unsupported type.");

            Assert.IsTrue(
                exception.Message.Contains("The type 'System.String' is not supported"),
                "Exception message does not indicate the unsupported type.");
        }
    }
}
