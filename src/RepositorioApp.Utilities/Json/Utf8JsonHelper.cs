using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Formatters;
using Utf8Json.Resolvers;
namespace RepositorioApp.Utilities.Json
{
    public static class Utf8JsonHelper
    {
        public static async Task<byte[]> SerializeAsync(this object @object)
        {
            await using var ms = new MemoryStream();
            await JsonSerializer.SerializeAsync(ms, @object, CustomtResolver.Instance);
            return ms.ToArray();
        }

        public static async Task<T> DeserializeAsync<T>(this byte[] buffer)
        {
            await using var ms = new MemoryStream(buffer);
            var data = await JsonSerializer.DeserializeAsync<T>(ms, CustomtResolver.Instance);
            return data;
        }

        public static async Task<object> DeserializeAsync(this byte[] buffer)
        {
            await using var ms = new MemoryStream(buffer);
            var data = await JsonSerializer.DeserializeAsync<object>(ms, CustomtResolver.Instance);
            return data;
        }

        public static byte[] Serialize(this object @object)
        {
            return JsonSerializer.Serialize(@object, CustomtResolver.Instance);
        }

        public static T Deserialize<T>(this byte[] buffer)
        {
            return JsonSerializer.Deserialize<T>(buffer, CustomtResolver.Instance);
        }

        public static object Deserialize(this byte[] buffer)
        {
            return JsonSerializer.Deserialize<object>(buffer, CustomtResolver.Instance);
        }
    }

    public class CustomtResolver : IJsonFormatterResolver
    {
        public static IJsonFormatterResolver Instance = new CustomtResolver();

        // configure your resolver and formatters.
        private static readonly IJsonFormatter[] formatters =
        {
            new DateTimeFormatter("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffZ"), new NullableDateTimeFormatter("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffZ")
        };

        private static readonly IJsonFormatterResolver[] resolvers =
        {
            EnumResolver.UnderlyingValue,
            StandardResolver.CamelCase,
            StandardResolver.ExcludeNull,
            StandardResolver.AllowPrivate,
            StandardResolver.AllowPrivateCamelCase,
            StandardResolver.ExcludeNullCamelCase,
            StandardResolver.ExcludeNullSnakeCase
        };

        private CustomtResolver()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        private static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                foreach (var item in formatters)
                {
                    foreach (var implInterface in item.GetType().GetTypeInfo().ImplementedInterfaces)
                    {
                        var ti = implInterface.GetTypeInfo();
                        if (ti.IsGenericType && ti.GenericTypeArguments[0] == typeof(T))
                        {
                            formatter = (IJsonFormatter<T>)item;
                            return;
                        }
                    }
                }

                foreach (var item in resolvers)
                {
                    var f = item.GetFormatter<T>();
                    if (f != null)
                    {
                        formatter = f;
                        return;
                    }
                }
            }
        }
    }
}
