using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{

    /// <summary>
    /// Random Functions
    /// </summary>
    public static class RandomFunctions
    {
        /// <summary>
        /// This will generate random string
        /// </summary>
        /// <param name="size"></param>
        /// <param name="lowerCase"></param>
        /// <returns></returns>
        public static string RandomString(this int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random randomvar = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomvar.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }     

        ///<summary>
        /// This will generate random alphabetic string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomAphabeticString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
