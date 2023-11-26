using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace RepositorioApp.Jwt
{
    internal static class CustomJsonSerializer
    {
        private static JsonSerializerOptions _options;
        public static JsonSerializerOptions Options
        {
            get
            {
                if (_options != null) return _options;

                _options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = false,
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                };
                _options.Converters.Add(new JsonStringEnumConverter());
                _options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                return _options;
            }
        }

        public static string Serialize(object @object)
        {
            return JsonSerializer.Serialize(@object, Options);
        }
        public static string Serialize<T>(T @object)
        {
            return JsonSerializer.Serialize(@object, Options);
        }
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }

        public static object Deserialize(string json)
        {
            return JsonSerializer.Deserialize<object>(json, Options);
        }
    }
}
