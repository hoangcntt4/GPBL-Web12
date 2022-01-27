using System.Text.Json;
using System.Text.Json.Serialization;

namespace Misa.Amis.Core.Utils
{
  /// <summary>
  /// Custom Nullable Datetime converter
  /// </summary>
  public class DateTimeJsonConverter : JsonConverter<DateTime?>
  {
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var dateStr = reader.GetString();
      if (string.IsNullOrEmpty(dateStr)) return null;
      return DateTime.Parse(dateStr);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.Value);
    }
  }
}
