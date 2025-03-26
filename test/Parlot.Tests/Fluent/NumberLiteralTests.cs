using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using Xunit;

namespace Parlot.Fluent.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="NumberLiteral{T}"/> class.
    /// </summary>
    public class NumberLiteralTests
    {
        private readonly NumberLiteral<int> _numberLiteralDefault;
        private readonly NumberLiteral<int> _numberLiteralWithSignAndDecimal;

        public NumberLiteralTests()
        {
            // Use default options (NumberOptions.Number) so that only digits are expected.
            _numberLiteralDefault = new NumberLiteral<int>();

            // Use options that allow leading sign and decimal separator.
            // For testing, we assume NumberOptions is a flags enum.
            // Here we simulate by using a value that has AllowLeadingSign and AllowDecimalSeparator set.
            // For the purpose of testing, we simply pass a combined options value (assuming bit flag values).
            int customOptions = (int)NumberOptions.AllowLeadingSign | (int)NumberOptions.AllowDecimalSeparator;
            _numberLiteralWithSignAndDecimal = new NumberLiteral<int>((NumberOptions)customOptions);
        }

        #region Parse Tests

        /// <summary>
        /// Tests the Parse method when a valid number is provided.
        /// The fake scanner returns a valid numeric string so that int.TryParse succeeds.
        /// Expected outcome: Parse returns true and the parse result is correctly set.
        /// </summary>
        [Fact]
        public void Parse_ValidInput_ReturnsTrueAndSetsResult()
        {
            // Arrange
            var fakeScanner = new FakeScanner();
            var expectedNumberString = "123";
            fakeScanner.ReadDecimalDelegate = (bool allowLeadingSign, bool allowDecimalSeparator, bool allowGroupSeparator, bool allowExponent, out string number, char decSep, char grpSep) =>
            {
                number = expectedNumberString;
                return true;
            };
            var fakeCursor = new FakeCursor(new FakePosition(0));
            fakeScanner.Cursor = fakeCursor;
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<int>();

            // Act
            bool parseSuccess = _numberLiteralDefault.Parse(fakeContext, ref result);

            // Assert
            Assert.True(parseSuccess);
            // int.TryParse("123", NumberStyles, culture, out value) should yield 123.
            Assert.Equal(123, result.Value);
            Assert.Equal(0, result.Start);
            Assert.Equal(expectedNumberString.Length, result.End);
        }

        /// <summary>
        /// Tests the Parse method when the scanner fails to read a number.
        /// Expected outcome: Parse returns false and the parse result remains unset.
        /// </summary>
        [Fact]
        public void Parse_InvalidInput_ReturnsFalse()
        {
            // Arrange
            var fakeScanner = new FakeScanner();
            fakeScanner.ReadDecimalDelegate = (bool allowLeadingSign, bool allowDecimalSeparator, bool allowGroupSeparator, bool allowExponent, out string number, char decSep, char grpSep) =>
            {
                number = string.Empty;
                return false;
            };
            var fakeCursor = new FakeCursor(new FakePosition(10));
            fakeScanner.Cursor = fakeCursor;
            var fakeContext = new FakeParseContext(fakeScanner);
            var result = new FakeParseResult<int>();

            // Act
            bool parseSuccess = _numberLiteralDefault.Parse(fakeContext, ref result);

            // Assert
            Assert.False(parseSuccess);
            // Since parsing failed, value should remain default (0) and start/end not set.
            Assert.Equal(0, result.Value);
            Assert.Equal(0, result.Start);
            Assert.Equal(0, result.End);
        }

        #endregion

        #region Compile Tests

        /// <summary>
        /// Tests the Compile method to ensure it returns a CompilationResult containing a valid list of expressions.
        /// Expected outcome: The CompilationResult's Body contains the expected expressions.
        /// </summary>
        [Fact]
        public void Compile_Always_ReturnsCompilationResultWithExpressions()
        {
            // Arrange
            var fakeCompilationContext = new FakeCompilationContext();

            // Act
            var compilationResult = _numberLiteralDefault.Compile(fakeCompilationContext);

            // Assert
            Assert.NotNull(compilationResult);
            Assert.NotEmpty(compilationResult.Body);
        }

        #endregion
    }

    #region Fake Classes for Parse Method Testing

    // Fake implementations to simulate the behavior of ParseContext, Scanner, Cursor, etc.
    internal class FakeParseContext
    {
        public FakeScanner Scanner { get; }

        public FakeParseContext(FakeScanner scanner)
        {
            Scanner = scanner;
        }

        public void EnterParser(object parser)
        {
            // No operation needed for fake.
        }

        public void ExitParser(object parser)
        {
            // No operation needed for fake.
        }
    }

    internal class FakeParseResult<T>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public T Value { get; private set; }

        public void Set(int start, int end, T value)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }

    internal class FakeScanner
    {
        public FakeCursor Cursor { get; set; }

        public delegate bool ReadDecimalDelegateType(bool allowLeadingSign, bool allowDecimalSeparator, bool allowGroupSeparator, bool allowExponent, out string number, char decimalSeparator, char groupSeparator);
        public ReadDecimalDelegateType ReadDecimalDelegate { get; set; }

        public bool ReadDecimal(bool allowLeadingSign, bool allowDecimalSeparator, bool allowGroupSeparator, bool allowExponent, out string number, char decimalSeparator, char groupSeparator)
        {
            if (ReadDecimalDelegate != null)
            {
                return ReadDecimalDelegate(allowLeadingSign, allowDecimalSeparator, allowGroupSeparator, allowExponent, out number, decimalSeparator, groupSeparator);
            }
            number = string.Empty;
            return false;
        }
    }

    internal class FakeCursor
    {
        public FakePosition Position { get; private set; }

        public FakeCursor(FakePosition position)
        {
            Position = position;
        }

        public void ResetPosition(FakePosition pos)
        {
            Position = pos;
        }
    }

    internal class FakePosition
    {
        public int Offset { get; set; }

        public FakePosition(int offset)
        {
            Offset = offset;
        }
    }

    #endregion

    #region Fake Classes for Compile Method Testing

    // Minimal fake implementations for CompilationContext and CompilationResult<T> to support testing of Compile method.
    internal class FakeCompilationContext : CompilationContext
    {
        private int _nextNumber = 1;

        public override int NextNumber => _nextNumber++;

        public override CompilationResult<T> CreateCompilationResult<T>()
        {
            return new FakeCompilationResult<T>();
        }

        public override Expression ResetPosition(object reset)
        {
            // For testing, return an empty expression.
            return Expression.Empty();
        }

        public override Expression Offset()
        {
            // Return a constant for testing.
            return Expression.Constant(0);
        }

        public override Expression ReadDecimal(Expression allowLeadingSign, Expression allowDecimalSeparator, Expression allowGroupSeparator, Expression allowExponent, ParameterExpression numberSpan, Expression decimalSeparator, Expression groupSeparator)
        {
            // Return a dummy expression that simulates the reading of a decimal.
            return Expression.Constant(true);
        }

        public override object DeclarePositionVariable<T>(CompilationResult<T> result)
        {
            // Return a dummy variable (could be a string representing a variable name).
            return "dummyPosition";
        }

        public override ParameterExpression DeclareVariable<T>(string name, Expression constant)
        {
            return Expression.Parameter(typeof(T), name);
        }

        public override ParameterExpression DeclareVariable(string name, Type type)
        {
            return Expression.Parameter(type, name);
        }
    }

    internal class FakeCompilationResult<T> : CompilationResult
    {
        public override List<Expression> Body { get; } = new List<Expression>();

        public override ParameterExpression Success { get; } = Expression.Variable(typeof(bool), "success");

        public override ParameterExpression Value { get; } = Expression.Variable(typeof(T), "value");
    }

    #endregion

    #region Minimal Abstract Classes to Support FakeCompilationContext

    // The following abstract classes are minimal representations to allow compilation of fake classes.
    // In the actual project, these would be part of the Parlot.Compilation namespace.

    public abstract class CompilationContext
    {
        public abstract int NextNumber { get; }
        public abstract CompilationResult<T> CreateCompilationResult<T>();
        public abstract Expression ResetPosition(object reset);
        public abstract Expression Offset();
        public abstract Expression ReadDecimal(Expression allowLeadingSign, Expression allowDecimalSeparator, Expression allowGroupSeparator, Expression allowExponent, ParameterExpression numberSpan, Expression decimalSeparator, Expression groupSeparator);
        public abstract object DeclarePositionVariable<T>(CompilationResult<T> result);
        public abstract ParameterExpression DeclareVariable<T>(string name, Expression constant);
        public abstract ParameterExpression DeclareVariable(string name, Type type);
    }

    public abstract class CompilationResult
    {
        public abstract List<Expression> Body { get; }
        public abstract ParameterExpression Success { get; }
        public abstract ParameterExpression Value { get; }
    }

    public class CompilationResult<T> : CompilationResult
    {
        private readonly List<Expression> _body = new List<Expression>();

        public override List<Expression> Body => _body;

        private readonly ParameterExpression _success = Expression.Variable(typeof(bool), "success");
        public override ParameterExpression Success => _success;

        private readonly ParameterExpression _value = Expression.Variable(typeof(T), "value");
        public override ParameterExpression Value => _value;
    }

    #endregion

    #region Minimal Enum and Options to Support Testing

    // Minimal represention of NumberOptions and NumberLiterals to allow testing.
    [Flags]
    public enum NumberOptions
    {
        Number = 0,
        AllowLeadingSign = 1,
        AllowDecimalSeparator = 2,
        AllowGroupSeparators = 4,
        AllowExponent = 8
    }

    public static class NumberLiterals
    {
        public const char DefaultDecimalSeparator = '.';
        public const char DefaultGroupSeparator = ',';
    }

    #endregion
}
