using Core.Config;
using System;
using System.IO;
using Core.Exceptions;

namespace Core.Helpers
{

    /// <summary>
    /// Log Helper
    /// </summary>
    public static class LogHelper
    {
        //Global Declaration
        private static string _logFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);
        private static StreamWriter _streamw = null;

        /// <summary>
        /// Create a file which can store the log information
        /// </summary>
        /// <param name="TestName"></param>
        public static void CreateLogFile(String TestName)
        {
            if (Settings.LogPath == null)
            {
                ConfigReader.SetFrameworkSettings();
            }
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + Settings.LogPath;
            string dir = new Uri(finalpth).LocalPath;
            if (Directory.Exists(dir))
            {
                _streamw = File.AppendText(dir + TestName + "_" + _logFileName + ".log");
            }
            else
            {
                Directory.CreateDirectory(dir);
                _streamw = File.AppendText(dir + TestName + "_" + _logFileName + ".log");
            }
        }


        /// <summary>
        /// Create a method which can write the text in the log file
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Write(string logMessage)
        {
            _streamw.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            _streamw.WriteLine("    {0}", logMessage);
            _streamw.Flush();
        }

        /// <summary>
        /// This will write exception to log file
        /// </summary>
        /// <param name="exception"></param>
        public static void LogException(Exception exception)
        {
            ExceptionLogType exceptionLogType;
            Enum.TryParse<ExceptionLogType>(Settings.ExceptionDestinationType.ToString(), out exceptionLogType);
            switch (Convert.ToInt16(Settings.ExceptionDestinationType))//
            {
                case 1:
                    new ExceptionLogger().WriteExceptionToFile(Settings.ExceptionFilePath, exception);
                    break;
                case 0:
                default:
                    break;
            }
        }
    }
}
