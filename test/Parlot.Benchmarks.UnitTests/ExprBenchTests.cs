using Parlot.Benchmarks;
using Parlot.Benchmarks.PidginParsers;
using Parlot.Fluent;
using Parlot.Tests.Calc;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="ExprBench"/> class.
    /// </summary>
    public class ExprBenchTests
    {
        private readonly ExprBench _exprBench;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExprBenchTests"/> class.
        /// </summary>
        public ExprBenchTests()
        {
            _exprBench = new ExprBench();
        }

        /// <summary>
        /// Tests the ParlotRawSmall method to ensure it returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ParlotRawSmall_WhenCalled_ReturnsNonNullExpression()
        {
            // Act
            Expression result = _exprBench.ParlotRawSmall();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the ParlotCompiledSmall method to ensure it returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ParlotCompiledSmall_WhenCalled_ReturnsNonNullExpression()
        {
            // Act
            Expression result = _exprBench.ParlotCompiledSmall();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the ParlotFluentSmall method to ensure it returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ParlotFluentSmall_WhenCalled_ReturnsNonNullExpression()
        {
            // Act
            Expression result = _exprBench.ParlotFluentSmall();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the PidginSmall method to ensure it returns a non-null Expression.
        /// </summary>
        [Fact]
        public void PidginSmall_WhenCalled_ReturnsNonNullExpression()
        {
            // Act
            Expression result = _exprBench.PidginSmall();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the ParlotRawBig method to ensure it returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ParlotRawBig_WhenCalled_ReturnsNonNullExpression()
        {
            // Act
            Expression result = _exprBench.ParlotRawBig();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the ParlotCompiledBig method to ensure it returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ParlotCompiledBig_WhenCalled_ReturnsNonNullExpression()
        {
            // Act
            Expression result = _exprBench.ParlotCompiledBig();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the ParlotFluentBig method to ensure it returns a non-null Expression.
        /// </summary>
        [Fact]
        public void ParlotFluentBig_WhenCalled_ReturnsNonNullExpression()
        {
            // Act
            Expression result = _exprBench.ParlotFluentBig();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the PidginBig method to ensure it returns a non-null Expression.
        /// </summary>
        [Fact]
        public void PidginBig_WhenCalled_ReturnsNonNullExpression()
        {
            // Act
            Expression result = _exprBench.PidginBig();

            // Assert
            Assert.NotNull(result);
        }
    }
}
