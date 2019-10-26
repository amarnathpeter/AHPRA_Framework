using Core.Config;
using System;
using System.IO;


namespace Core.Helpers
{

    /// <summary>
    /// File Helper
    /// </summary>
    internal class FileHelper
    {

        /// <summary>
        /// The log file name
        /// </summary>
        private static string _logFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);

        /// <summary>
        /// The streamw
        /// </summary>
        private static StreamWriter _streamw = null;

        /// <summary>
        /// The folder path
        /// </summary>
        private readonly string _folderPath;

        /// <summary>
        /// The file name
        /// </summary>
        private string _fileName = string.Empty;

        public FileHelper(string folderPath)
        {
            _folderPath = folderPath;
        }

        /// <summary>
        /// This will write message in file
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logType"></param>
        /// <param name="filename"></param>
        internal void WriteToFile(String message, LogType logType, string filename = null)
        {
            try
            {
                GetFileName(logType, filename);

                if (Directory.Exists(_folderPath))
                {
                    _streamw = File.AppendText(_folderPath + "_" + _fileName + _logFileName + ".log");
                }
                else
                {
                    Directory.CreateDirectory(_folderPath);
                    _streamw = File.AppendText(_folderPath + "_" + _logFileName + ".log");
                }
                Write(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This will get File name
        /// </summary>
        /// <param name="logtype"></param>
        /// <param name="filename"></param>
        private void GetFileName(LogType logtype, string filename)
        {
            switch (logtype)
            {
                case LogType.Message:
                    _fileName = filename;
                    break;
                case LogType.Exception:
                    _fileName = "Exception";
                    break;
                default:
                    _fileName = string.Empty;
                    break;
            }
        }


        /// <summary>
        /// This will write log in log file
        /// </summary>
        /// <param name="logMessage"></param>
        private void Write(string logMessage)
        {
            _streamw = File.AppendText(_folderPath + _fileName + "_" + _logFileName + ".log");
            _streamw.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            _streamw.WriteLine("    {0}", logMessage);
            _streamw.Flush();
        }
    }
}
