using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Parlot.Benchmarks;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="JsonBench"/> class.
    /// </summary>
    public class JsonBenchTests
    {
        private readonly JsonBench _jsonBench;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonBenchTests"/> class and invokes Setup on JsonBench.
        /// </summary>
        public JsonBenchTests()
        {
            _jsonBench = new JsonBench();
            _jsonBench.Setup();
        }

        /// <summary>
        /// Tests that the Setup method initializes fields correctly by validating that subsequent parsing methods return non-null results.
        /// </summary>
        [Fact]
        public void Setup_WhenCalled_InitializesParserAndJsonStrings()
        {
            // Act
            var bigResult = _jsonBench.BigJson_Parlot();
            var longResult = _jsonBench.LongJson_Parlot();
            var deepResult = _jsonBench.DeepJson_Parlot();
            var wideResult = _jsonBench.WideJson_Parlot();

            // Assert
            Assert.NotNull(bigResult);
            Assert.NotNull(longResult);
            Assert.NotNull(deepResult);
            Assert.NotNull(wideResult);
        }

        /// <summary>
        /// Tests the BigJson_ParlotCompiled method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void BigJson_ParlotCompiled_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.BigJson_ParlotCompiled();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the BigJson_Parlot method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void BigJson_Parlot_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.BigJson_Parlot();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the BigJson_Pidgin method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void BigJson_Pidgin_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.BigJson_Pidgin();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the BigJson_Newtonsoft method to ensure it returns a valid JToken with children.
        /// </summary>
        [Fact]
        public void BigJson_Newtonsoft_WhenCalled_ReturnsValidJToken()
        {
            // Act
            JToken result = _jsonBench.BigJson_Newtonsoft();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasValues, "Expected the JToken to contain values.");
        }

        /// <summary>
        /// Tests the BigJson_SystemTextJson method to ensure it returns a valid JsonDocument.
        /// </summary>
        [Fact]
        public void BigJson_SystemTextJson_WhenCalled_ReturnsValidJsonDocument()
        {
            // Act
            using JsonDocument doc = _jsonBench.BigJson_SystemTextJson();

            // Assert
            Assert.NotNull(doc);
            Assert.True(doc.RootElement.ValueKind != JsonValueKind.Undefined, "Expected a defined root element.");
        }

        /// <summary>
        /// Tests the BigJson_Sprache method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void BigJson_Sprache_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.BigJson_Sprache();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the BigJson_Superpower method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void BigJson_Superpower_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.BigJson_Superpower();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the LongJson_ParlotCompiled method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void LongJson_ParlotCompiled_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.LongJson_ParlotCompiled();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the LongJson_Parlot method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void LongJson_Parlot_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.LongJson_Parlot();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the LongJson_Pidgin method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void LongJson_Pidgin_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.LongJson_Pidgin();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the LongJson_Newtonsoft method to ensure it returns a valid JToken with children.
        /// </summary>
        [Fact]
        public void LongJson_Newtonsoft_WhenCalled_ReturnsValidJToken()
        {
            // Act
            JToken result = _jsonBench.LongJson_Newtonsoft();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasValues, "Expected the JToken to contain values.");
        }

        /// <summary>
        /// Tests the LongJson_SystemTextJson method to ensure it returns a valid JsonDocument.
        /// </summary>
        [Fact]
        public void LongJson_SystemTextJson_WhenCalled_ReturnsValidJsonDocument()
        {
            // Act
            using JsonDocument doc = _jsonBench.LongJson_SystemTextJson();

            // Assert
            Assert.NotNull(doc);
            Assert.True(doc.RootElement.ValueKind != JsonValueKind.Undefined, "Expected a defined root element.");
        }

        /// <summary>
        /// Tests the LongJson_Sprache method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void LongJson_Sprache_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.LongJson_Sprache();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the LongJson_Superpower method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void LongJson_Superpower_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.LongJson_Superpower();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the DeepJson_ParlotCompiled method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void DeepJson_ParlotCompiled_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.DeepJson_ParlotCompiled();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the DeepJson_Parlot method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void DeepJson_Parlot_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.DeepJson_Parlot();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the DeepJson_Pidgin method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void DeepJson_Pidgin_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.DeepJson_Pidgin();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the DeepJson_Newtonsoft method to ensure it returns a valid JToken with children.
        /// </summary>
        [Fact]
        public void DeepJson_Newtonsoft_WhenCalled_ReturnsValidJToken()
        {
            // Act
            JToken result = _jsonBench.DeepJson_Newtonsoft();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasValues, "Expected the JToken to contain values.");
        }

        /// <summary>
        /// Tests the DeepJson_SystemTextJson method to ensure it returns a valid JsonDocument.
        /// </summary>
        [Fact]
        public void DeepJson_SystemTextJson_WhenCalled_ReturnsValidJsonDocument()
        {
            // Act
            using JsonDocument doc = _jsonBench.DeepJson_SystemTextJson();

            // Assert
            Assert.NotNull(doc);
            Assert.True(doc.RootElement.ValueKind != JsonValueKind.Undefined, "Expected a defined root element.");
        }

        /// <summary>
        /// Tests the DeepJson_Sprache method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void DeepJson_Sprache_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.DeepJson_Sprache();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the WideJson_ParlotCompiled method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void WideJson_ParlotCompiled_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.WideJson_ParlotCompiled();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the WideJson_Parlot method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void WideJson_Parlot_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.WideJson_Parlot();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the WideJson_Pidgin method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void WideJson_Pidgin_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.WideJson_Pidgin();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the WideJson_Newtonsoft method to ensure it returns a valid JToken with children.
        /// </summary>
        [Fact]
        public void WideJson_Newtonsoft_WhenCalled_ReturnsValidJToken()
        {
            // Act
            JToken result = _jsonBench.WideJson_Newtonsoft();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasValues, "Expected the JToken to contain values.");
        }

        /// <summary>
        /// Tests the WideJson_Sprache method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void WideJson_Sprache_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.WideJson_Sprache();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the WideJson_Superpower method to ensure it returns a valid IJson object.
        /// </summary>
        [Fact]
        public void WideJson_Superpower_WhenCalled_ReturnsNonNullIJson()
        {
            // Act
            var result = _jsonBench.WideJson_Superpower();

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the RandomString static method to ensure it returns a string of the specified positive length.
        /// </summary>
        [Fact]
        public void RandomString_WhenCalledWithPositiveLength_ReturnsStringOfThatLength()
        {
            // Arrange
            int length = 10;

            // Act
            string result = JsonBench.RandomString(length);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(length, result.Length);
        }

        /// <summary>
        /// Tests the RandomString static method to ensure it returns an empty string when zero is provided.
        /// </summary>
        [Fact]
        public void RandomString_WhenCalledWithZeroLength_ReturnsEmptyString()
        {
            // Arrange
            int length = 0;

            // Act
            string result = JsonBench.RandomString(length);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);
        }

        /// <summary>
        /// Tests the RandomString static method to ensure it throws an ArgumentOutOfRangeException when a negative length is provided.
        /// </summary>
        [Fact]
        public void RandomString_WhenCalledWithNegativeLength_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int negativeLength = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => JsonBench.RandomString(negativeLength));
        }
    }
}
