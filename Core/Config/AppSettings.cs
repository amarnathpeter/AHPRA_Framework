namespace Core.Configuration
{
    public class AppSettings
    {
        public EnvironmentSettings environmentSettings { get; set; }
        public ConnectionStrings connectionStrings { get; set; }
        public class EnvironmentSettings
        {
            public string BrowserName { get; set; }
            public string PlatformName { get; set; }
            public string ApplicationURL { get; set; }
        }
        public class ConnectionStrings
        {
        }

    }
}
