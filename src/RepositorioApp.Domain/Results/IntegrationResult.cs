using Newtonsoft.Json;
namespace RepositorioApp.Domain.Results
{
    public class IntegrationResult
    {
        public bool Valid { get; set; }

        [JsonIgnore]
        public string Error { get; set; }

        [JsonIgnore]
        public string IntegrationCode { get; set; }
    }
}
