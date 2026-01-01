using System.Text.Json;
using System.Text.Json.Serialization;
using TurkishValidators.Json.Converters;
using Xunit;

namespace TurkishValidators.Tests.Json
{
    public class JsonConverterTests
    {
        private class TestModel
        {
            [JsonConverter(typeof(TcknMaskingConverter))]
            public string Tckn { get; set; } = string.Empty;

            [JsonConverter(typeof(IbanMaskingConverter))]
            public string Iban { get; set; } = string.Empty;
        }

        [Fact]
        public void Tckn_ShouldBeMasked_WhenSerialized()
        {
            var model = new TestModel { Tckn = "12345678901" };
            var json = JsonSerializer.Serialize(model);

            // Default masking: 123******01
            Assert.Contains("123******01", json);
            Assert.DoesNotContain("12345678901", json);
        }

        [Fact]
        public void Iban_ShouldBeMasked_WhenSerialized()
        {
            var model = new TestModel { Iban = "TR330006100519786457841326" };
            var json = JsonSerializer.Serialize(model);

            // Default masking: TR********************1326 (VisibleStart=2, VisibleEnd=4) 
            // Wait, IbanMasker default is TR (2) + Last 4.
            // TR330006100519786457841326 -> TR....................1326
            Assert.Contains("TR", json);
            Assert.Contains("1326", json);
            Assert.DoesNotContain("000610051978645784", json);
        }

        [Fact]
        public void ShouldDeserialize_AsOriginal()
        {
            // Note: Converters currently do NOT unmask (read returns string).
            // But if we receive masked data, we get masked data.
            // If we receive plain data, we get plain data.
            // The converter Read method implemented just reads string.
            
            var json = "{\"Tckn\": \"12345678901\"}";
            var model = JsonSerializer.Deserialize<TestModel>(json);

            Assert.Equal("12345678901", model!.Tckn);
        }
    }
}
