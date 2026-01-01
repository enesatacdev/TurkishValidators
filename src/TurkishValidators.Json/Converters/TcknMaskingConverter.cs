using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TurkishValidators.Masking;

namespace TurkishValidators.Json.Converters
{
    /// <summary>
    /// TC Kimlik Numarasını JSON serileştirme sırasında otomatik maskeler.
    /// </summary>
    public class TcknMaskingConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() ?? string.Empty;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (string.IsNullOrEmpty(value))
            {
                writer.WriteStringValue(value);
                return;
            }

            // Varsayılan maskeleme kullanılır
            var masked = TcKimlikNoMasker.Mask(value);
            writer.WriteStringValue(masked);
        }
    }
}
