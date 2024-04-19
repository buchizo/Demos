using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace DocumentIntelligenceDemo;
public static class JsonExtension
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static string ToJson(this object source)
    {
        return JsonSerializer.Serialize(source, Options);
    }

    public static T? FromJson<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, Options);
    }
}
