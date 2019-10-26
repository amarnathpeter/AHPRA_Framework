using System;

namespace Core.Exceptions
{

    /// <summary>
    /// Exception Logger
    /// </summary>
    internal class ExceptionLogger
    {
        /// <summary>
        /// Write exception in Log file
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="e"></param>
        public void WriteExceptionToFile(string folderPath, Exception e)
        {
            string message = null;
            new Helpers.FileHelper(folderPath).WriteToFile(message, Helpers.LogType.Exception);
        }
    }
}
