using Core.Base;
using NUnit.Framework;
using System;

namespace Core.Helpers
{

    /// <summary>
    /// Shouldly
    /// </summary>
    /// <seealso cref="Core.Base.TestBase" />
    public class Shouldly:TestBase
    {
        /// <summary>
        /// This will verify string to be equal
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static void ShouldBe(string actual,string expected)
        {
            Assert.IsTrue(string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// This will verify string not equal
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static void ShouldNotBe(string actual, string expected)
        {
            Assert.IsFalse(string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// This will verify boolean value
        /// </summary>
        /// <param name="expected"></param>
        public static void IsTrue(bool expected)
        {
            Assert.IsTrue(expected);
        }     

        /// <summary>
        /// This will verify given condition is true
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="Text"></param>
        public static void IsCondtionTrue(bool expected, string Text)
        {
            try
            { 
                Assert.IsTrue(expected);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This will verify given condition is false
        /// </summary>
        /// <param name="expected"></param>
        public static void IsFalse(bool expected)
        {
            Assert.IsFalse(expected);
        }
    }
}
