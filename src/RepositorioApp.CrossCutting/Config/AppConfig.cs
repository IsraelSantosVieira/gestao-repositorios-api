namespace RepositorioApp.CrossCutting.Config
{
    public class AppConfig
    {
        public string Logger { get; set; }

        public string Domain { get; set; }
        public string BaseUrl { get; set; }

        public string ContactEmailAddress { get; set; }

        public bool RequireHttps { get; set; }

        public string AppUrl { get; set; }

        public string BasePath { get; set; } = "/";

        public string FirebaseCredentials { get; set; }
    }
}
