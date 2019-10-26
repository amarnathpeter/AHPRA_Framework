using Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.util;

namespace Core.Helpers
{
    public static class AllureEnvProperties
    {
        private static Properties props;

        public static Properties GetAllureEnvProperties()
        {
            return props;
        }

        public static void SetAllureEnvProperties(AppSettings _appSettings)
        {
            string root = Directory.GetParent(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName + "\\allure-results";
            string fileName = root + "\\environment.properties";
            if (props == null)
            {
                props = new Properties();

                try
                {
                    if (Directory.Exists(fileName))

                    {
                        Directory.Delete(fileName);
                    }
                    //}
                    DirectoryInfo di = Directory.CreateDirectory(root);
                  //  FileStream allureResDir = File.Create(fileName);
                    Dictionary<string, string> environmentDetails = new Dictionary<string, string>();
                    environmentDetails.Add("Browser :", _appSettings.environmentSettings.BrowserName);
                    environmentDetails.Add("SiteURL :", _appSettings.environmentSettings.ApplicationURL);
                    environmentDetails.Add("UserName :", Environment.MachineName);
                    environmentDetails.Add("Platform :", Environment.OSVersion.Platform.ToString());

                    using (StreamWriter file = new StreamWriter(fileName))
                        foreach (var entry in environmentDetails)
                            file.WriteLine("{0}{1}", entry.Key, entry.Value);
                }
                catch (IOException e)
                {
                    //e.printStackTrace();
                }
            }
        }
    }
}
