
namespace Core.Config
{
    /// <summary>
    /// Settings
    /// </summary>
    public static class Settings
    {
        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string Parallelizable { get; set; }

        public static string Grid { get; set; }

        public static string AUT { get; set; }

        public static string AbsoluteURL { get; set; }

        public static string BuildName { get; set; }

        public static string DBName { get; set; }
        
        public static string LogPath { get; set; }

        public static string TestDataPath { get; set; }

        public static string ExecutingBrowser { get; set; }

        public static string ScreenShotLocation { get; set; }

        public static string ExceptionDestinationType { get; set; }

        public static string ExceptionFilePath { get; set; }

        public static string iDashXPath { get; set; }

        public static string MLEHRBuildVersion { get; set; }

        public static string MLMobileBuildVersion { get; set; }

        public static string Environment { get; set; }

        public static string IEVersion { get; set; }

        public static string FFVersion { get; set; }

        public static string ChromeVersion { get; set; }

        public static string TestReportName { get; set; }

        public static string EmailGroup { get; set; }

        public static string DBConnectionString { get; set; }

        public static string MasterDBConnectionString { get; set; }

        public static string SMTPHost { get; set; }

        public static int SMTPPort { get; set; }

        public static string SMTPUserName { get; set; }

        public static string SMTPPassword { get; set; }

        public static int SMTPTimeout { get; set; }

        public static bool SMTPEnableSSL { get; set; }

        public static string EmailSubject { get; set; }

        public static bool SendEmailReport { get; set; }

        public static string EmailFrom { get; set; }

        public static string EmailBody { get; set; }

        public static bool EnableDBSnapshot { get; set; }

        public static bool ReplaceExistingTestResult { get; set; }

        public static string Title { get; set; }

        public static string DependenciesLocation { get; set; }

        public static string DownloadsLocation { get; set; }

    }
}
