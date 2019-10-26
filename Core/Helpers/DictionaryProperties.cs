using System.Collections.Generic;

namespace Core.Helpers
{

    /// <summary>
    /// DictionaryProperties
    /// </summary>
    public static class DictionaryProperties
    {

        /// <summary>
        /// The details
        /// </summary>
        private static Dictionary<string, string> _Details = new Dictionary<string, string>();

        public static Dictionary<string, string> Details
        {
            get { return _Details; }
        }


        /// <summary>
        /// The handles
        /// </summary>
        private static Dictionary<string, string> Handles = new Dictionary<string, string>();

        /// <summary>
        /// Gets the handle.
        /// </summary>
        /// <param name="handleName">Name of the handle.</param>
        /// <returns></returns>
        public static string GetHandle(string handleName)
        {
            string retVal = string.Empty;

            if (Handles.ContainsKey(handleName))
                retVal = Handles[handleName];
            return retVal;
        }

        /// <summary>
        /// Sets the handle.
        /// </summary>
        /// <param name="handleName">Name of the handle.</param>
        /// <param name="handleValue">The handle value.</param>
        public static void SetHandle(string handleName, string handleValue)
        {
            if (!Handles.ContainsKey(handleName))
                Handles.Add(handleName, handleValue);
            else
                Handles[handleName] = handleValue;
        }
    }

   
}
