using OpenQA.Selenium;

namespace Core.Base
{

    /// <summary>
    /// Browser Class
    /// </summary>
    public class Browser
    {

        /// <summary>
        /// The driver
        /// </summary>
        private readonly IWebDriver _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="Browser"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public Browser(IWebDriver driver)
        {
            _driver = driver;
        }
    }   
}
