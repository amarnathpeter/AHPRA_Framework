using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class AppProprties
    {
        public IWebDriver _driver { get; set; }
        public string _currentBrowser { get; set; }
        public AppProprties _appProperties;
        public AppProprties AppProp
        {
            get
            {
                if (_appProperties == null)
                    _appProperties = new AppProprties();

                return _appProperties;
            }
        }
    }
}
