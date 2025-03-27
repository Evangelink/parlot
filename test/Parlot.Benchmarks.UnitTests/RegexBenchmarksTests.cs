using System;
using Parlot.Benchmarks;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="RegexBenchmarks"/> class.
    /// </summary>
    public class RegexBenchmarksTests
    {
        private const string ExpectedEmail = "sebastien.ros@gmail.com";
        private readonly RegexBenchmarks _regexBenchmarks;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexBenchmarksTests"/> class.
        /// </summary>
        public RegexBenchmarksTests()
        {
            _regexBenchmarks = new RegexBenchmarks();
        }

        /// <summary>
        /// Verifies that the Setup method completes without throwing an exception when the email is valid.
        /// </summary>
        [Fact]
        public void Setup_WithValidEmail_DoesNotThrow()
        {
            // Act & Assert
            var exception = Record.Exception(() => _regexBenchmarks.Setup());
            Assert.Null(exception);
        }

        /// <summary>
        /// Verifies that RegexEmailCompiled method returns the expected email address.
        /// </summary>
        [Fact]
        public void RegexEmailCompiled_WhenCalled_ReturnsExpectedEmail()
        {
            // Act
            string result = _regexBenchmarks.RegexEmailCompiled();
            // Assert
            Assert.Equal(ExpectedEmail, result);
        }

        /// <summary>
        /// Verifies that RegexEmail method returns the expected email address.
        /// </summary>
        [Fact]
        public void RegexEmail_WhenCalled_ReturnsExpectedEmail()
        {
            // Act
            string result = _regexBenchmarks.RegexEmail();
            // Assert
            Assert.Equal(ExpectedEmail, result);
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Verifies that RegexEmailGenerated method returns the expected email address when available.
        /// </summary>
        [Fact]
        public void RegexEmailGenerated_WhenCalled_ReturnsExpectedEmail()
        {
            // Act
            string result = _regexBenchmarks.RegexEmailGenerated();
            // Assert
            Assert.Equal(ExpectedEmail, result);
        }
#endif

        /// <summary>
        /// Verifies that ParlotEmailCompiled method returns a TextSpan that represents the expected email address.
        /// </summary>
        [Fact]
        public void ParlotEmailCompiled_WhenCalled_ReturnsExpectedEmail()
        {
            // Act
            var textSpan = _regexBenchmarks.ParlotEmailCompiled();
            // Assert
            Assert.Equal(ExpectedEmail, textSpan.ToString());
        }

        /// <summary>
        /// Verifies that ParlotEmail method returns a TextSpan that represents the expected email address.
        /// </summary>
        [Fact]
        public void ParlotEmail_WhenCalled_ReturnsExpectedEmail()
        {
            // Act
            var textSpan = _regexBenchmarks.ParlotEmail();
            // Assert
            Assert.Equal(ExpectedEmail, textSpan.ToString());
        }
    }
}
