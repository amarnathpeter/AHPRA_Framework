using System;
using Faker;

namespace Core.Helpers
{
    /// <summary>
    /// Fake Data Class
    /// </summary>
    class Faker
    {
        /// <summary>
        /// Fakes the name.
        /// </summary>
        /// <returns></returns>
        public string FakeName()
        {
            return NameFaker.Name();
        }

        /// <summary>
        /// Fakes the first name.
        /// </summary>
        /// <returns></returns>
        public string FakeFirstName()
        {
            return NameFaker.FirstName();
        }

        /// <summary>
        /// Fakes the last name.
        /// </summary>
        /// <returns></returns>
        public string FakeLastName()
        {
            return NameFaker.LastName();
        }

        /// <summary>
        /// Fakes the phone number.
        /// </summary>
        /// <returns></returns>
        public string FakePhoneNumber()
        {
            return PhoneFaker.Phone();
        }

        /// <summary>
        /// Fakes the email.
        /// </summary>
        /// <returns></returns>
        public string FakeEmail()
        {
            Random random = new Random();
            string builder = null;

            for (int i = 0; i < 10; ++i)
            {
                builder = string.Format("qa{0:0000}@test.com", random.Next(10000));
            }
            return builder;
        }

        /// <summary>
        /// Fakes the number.
        /// </summary>
        /// <returns></returns>
        public int FakeNumber()
        {
            return NumberFaker.Number();
        }

        /// <summary>
        /// Fakes the password.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public string FakePassword(int length)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random(); 
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return (new string(chars));
        }

        /// <summary>
        /// Fakes the sentence.
        /// </summary>
        /// <returns></returns>
        public string FakeSentence()
        {
            return TextFaker.Sentence();
        }
    }
}
