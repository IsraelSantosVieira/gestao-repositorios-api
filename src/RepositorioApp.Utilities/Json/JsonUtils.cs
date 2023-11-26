using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
namespace RepositorioApp.Utilities.Json
{
    public static class JsonUtils
    {
        public static JsonSerializerSettings GetSettings(bool indent = false)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = indent ? Formatting.Indented : Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetSettings());
        }

        public static T DeserializeyteArray<T>(byte[] buffer)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(buffer), GetSettings());
        }

        public static T DeserializeIgnoreRoot<T>(string json)
        {
            var data = JObject.Parse(json)?.Properties()?.FirstOrDefault()?.Value;

            return data == null ? default : Deserialize<T>(Serialize(data));
        }

        public static string Serialize(object value, bool indent = false, string root = null)
        {
            if (!string.IsNullOrEmpty(root))
            {
                var data = new ExpandoObject() as IDictionary<string, object>;
                ;
                data.Add(root, value);
                return JsonConvert.SerializeObject(data, GetSettings(indent));
            }

            return JsonConvert.SerializeObject(value, GetSettings(indent));
        }

        public static byte[] SerializeByteArray(object value, string root = null)
        {
            if (!string.IsNullOrEmpty(root))
            {
                var data = new ExpandoObject() as IDictionary<string, object>;
                ;
                data.Add(root, value);
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, GetSettings()));
            }

            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value, GetSettings()));
        }

        public static T CastObject<T>(object obj)
        {
            var jsonSeralizer = new JsonSerializer
            {
                ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            var data = ((JObject)obj).ToObject<T>(jsonSeralizer);
            return data;
        }

        public static JsonTextWriter GetJsonTextWriter(StringWriter sw)
        {
            var writer = new JsonTextWriter(sw)
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                Formatting = Formatting.None
            };
            return writer;
        }
    }

    public class ParseStringConverter : JsonConverter
    {

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
        public override bool CanConvert(Type t)
        {
            return t == typeof(long) || t == typeof(long?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (long.TryParse(value, out var l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
        }
    }

    public class PrivateSetterCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.Writable)
                return jProperty;

            jProperty.Writable = member.IsPropertyWithSetter();

            return jProperty;
        }
    }

    internal static class MemberInfoExtensions
    {
        internal static bool IsPropertyWithSetter(this MemberInfo member)
        {
            var property = member as PropertyInfo;

            return property?.GetSetMethod(true) != null;
        }
    }
}
