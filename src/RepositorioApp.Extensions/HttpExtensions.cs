using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
namespace RepositorioApp.Extensions
{
    public static class HttpExtensions
    {
        public static string ToRequestString(this HttpRequestMessage request)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Method: ");
            stringBuilder.Append(request.Method);
            stringBuilder.Append("\nRequestUri: ");
            stringBuilder.Append(request.RequestUri == null
                ? "<null>"
                : request.RequestUri.ToString());
            stringBuilder.Append("\nVersion: ");
            stringBuilder.Append(request.Version);
            stringBuilder.Append("\nContent: ");
            stringBuilder.Append(request.Content == null
                ? "<null>"
                : request.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            stringBuilder.Append("\nHeaders:\n");
            stringBuilder.Append(DumpHeaders(request.Headers,
                request.Content == null
                    ? null
                    : (HttpHeaders)request.Content.Headers));
            return stringBuilder.ToString();
        }

        public static string ToResponseString(this HttpResponseMessage response)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("StatusCode: ");
            stringBuilder.Append((int)response.StatusCode);
            stringBuilder.Append("\nReasonPhrase: ");
            stringBuilder.Append(response.ReasonPhrase ?? "<null>");
            stringBuilder.Append("\nVersion: ");
            stringBuilder.Append(response.Version);
            stringBuilder.Append("\nContent: ");
            stringBuilder.Append(response.Content == null
                ? "<null>"
                : response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            stringBuilder.Append("\nHeaders:\n");
            stringBuilder.Append(DumpHeaders(response.Headers,
                response.Content == null
                    ? null
                    : (HttpHeaders)response.Content.Headers));
            return stringBuilder.ToString();
        }

        public static string DumpHeaders(params HttpHeaders[] headers)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{\r\n");
            for (var index = 0; index < headers.Length; ++index)
            {
                if (headers[index] != null)
                {
                    foreach (var keyValuePair in headers[index])
                    {
                        foreach (var str in keyValuePair.Value)
                        {
                            stringBuilder.Append("  ");
                            stringBuilder.Append(keyValuePair.Key);
                            stringBuilder.Append(": ");
                            stringBuilder.Append(str);
                            stringBuilder.Append("\r\n");
                        }
                    }
                }
            }

            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }
    }
}
