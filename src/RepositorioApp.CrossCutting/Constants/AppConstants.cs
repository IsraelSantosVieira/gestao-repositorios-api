using System.Collections.Generic;
using System.Linq;
namespace RepositorioApp.CrossCutting.Constants
{
    public static class AppConstants
    {
        public const string ApplicationName = "RepositorioApp";
        public const string JsonMediaType = "application/json";

        public static IReadOnlyCollection<string> ImagesContentTypes = new string[2]
        {
            "image/png", "image/jpeg"
        };

        private static IDictionary<string, ApiConfiguration> _apisConfiguration;

        public static IDictionary<string, ApiConfiguration> ApisConfiguration
        {
            get
            {
                if (_apisConfiguration?.Any() == true)
                {
                    return _apisConfiguration;
                }

                _apisConfiguration = new Dictionary<string, ApiConfiguration>
                {
                    {
                        IdentityApiV1Key, new ApiConfiguration(true, "identity", "Identity", "v1")
                    },
                    {
                        ClientApiV1Key, new ApiConfiguration(true, "client", "Client", "v1")
                    },
                    {
                        ManagementApiV1Key, new ApiConfiguration(true, "management", "Management", "v1")
                    },
                    {
                        PublicApiV1Key, new ApiConfiguration(true, "integration", "Integration", "v1")
                    }
                };

                return _apisConfiguration;
            }
        }

        public static class Emails
        {
            public static class Subjects
            {
                public static readonly string NewUser = $"Bem-vindo a {ApplicationName}";
                public static readonly string PasswordRecoverRequest = $"Recuperar senha de acesso {ApplicationName}";
            }

            public static class Style
            {
                public const string PrimaryColor = "#35007C";
                public const string WhiteColor = "#FFF";
                public const string SecondaryColor = "#4D0491";
                public const string Font = "Helvetica, Arial, sans serif";
                public const string PaddingStyle = "padding: 20px";
            }
        }

        #region ApiConfig V1 Keys

        public const string IdentityApiV1Key = "IdentityApiV1Key";
        public const string ClientApiV1Key = "ClientApiV1Key";
        public const string ManagementApiV1Key = "ManagementApiV1Key";
        public const string PublicApiV1Key = "IntegrationApiV1Key";

        #endregion
    }

    public class ApiConfiguration
    {
        private readonly string _key;
        private readonly string _name;
        
        public ApiConfiguration(
            bool exposeOnSwagger,
            string key,
            string name,
            string version,
            string description = null)
        {
            ExposeOnSwagger = exposeOnSwagger;
            _key = key;
            _name = name;
            Version = version;
            Description = description ?? Title;
        }
        
        public string Group => $"{_key}-{Version}";
        public string Title => $"{_name} API {Version}";
        public string Description { get; }
        public string Version { get; }
        public string SwaggerEndpoint => $"{Group}/swagger.json";
        public string Route => $"/{_key}/{Version}/";
        public bool ExposeOnSwagger { get; }
    }
}
