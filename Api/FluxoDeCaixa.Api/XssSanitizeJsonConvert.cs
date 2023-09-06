using System.Text.Json;
using System.Text.Json.Serialization;
using Ganss.Xss;

namespace FluxoDeCaixa.Api;

public class XssSanitizeJsonConvert : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var raw = reader.GetString();
        if (raw is null)
            return null;

        var sanitiser = new HtmlSanitizer();
        return sanitiser.Sanitize(raw);
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}