using AllureCSharpCommons.Events;
using AventStack.ExtentReports;
using Demo.Factory;
using OpenQA.Selenium;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace Demo.StepDefination
{
    [Binding]
    public class FindAllBrokenLinkSteps:Steps
    {
        /// <summary>
        /// The object factory
        /// </summary>
        private readonly ObjectFactory _objectFactory;

        /// <summary>
        /// The driver
        /// </summary>
        private readonly IWebDriver _driver;

        /// <summary>
        /// The test
        /// </summary>
        private readonly ExtentTest _test;
        /// <summary>
        /// The test
        /// </summary>
        private readonly AllureCSharpCommons.Allure _lifecycle;
        public FindAllBrokenLinkSteps(IWebDriver driver, ExtentTest test, AllureCSharpCommons.Allure allureLifeCycle)
        {
            _driver = driver;
            _test = test;
            _lifecycle = allureLifeCycle;
            _objectFactory = new ObjectFactory();
        }

        [When(@"Find links on home page")]
        public void WhenFindLinksOnHomePage()
        {
            HttpWebRequest re = null;
            var urls = _driver.FindElements(By.TagName("a"));
            foreach (var url in urls)
            {
                //Get the url
                re = (HttpWebRequest)WebRequest.Create(url.GetAttribute("href"));
                try
                {
                    _lifecycle.Fire(new StepStartedEvent(StepContext.StepInfo.Text + "Url : " + url.GetAttribute("href") + " " + url.Text));
                    var response = (HttpWebResponse)re.GetResponse();
                    System.Console.WriteLine($"URL: {url.GetAttribute("href")} status is :{response.StatusCode}");
                }
                catch (WebException e)
                {
                    _lifecycle.Fire(new StepStartedEvent(StepContext.StepInfo.Text + "Url : " + url.GetAttribute("href")+" "+url.Text));
                    var errorResponse = (HttpWebResponse)e.Response;
                    continue;
                }
            }
             _lifecycle.Fire(new StepFinishedEvent());
            
        }
    }
}
