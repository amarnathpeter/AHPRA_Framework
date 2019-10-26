using Allure.Commons;
using AllureCSharpCommons.Events;
using AventStack.ExtentReports;
using Core.Extensions;
using Core.Helpers;
using Demo.Factory;
using Demo.Pages.PageObjects;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Demo
{
    [Binding]
    public class ApplicationLoginSteps:Steps
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
        public ApplicationLoginSteps(IWebDriver driver, ExtentTest test, AllureCSharpCommons.Allure allureLifeCycle)
        {
            _driver = driver;
            _test = test;
            _lifecycle = allureLifeCycle;
            _objectFactory = new ObjectFactory();
        }

        [Given(@"URL Navigate sucessfully with page title ""(.*)""")]
        public void GivenURLNavigateSucessfullyWithPageTitle(string titleName)
        {
            _lifecycle.Fire(new StepStartedEvent(StepContext.StepInfo.Text + "Title : " + titleName));
            Shouldly.ShouldBe(_driver.Title, titleName);
            _lifecycle.Fire(new StepFinishedEvent());
        }
        [When(@"User Click on login Button")]
        public void WhenUserClickOnLoginButton()
        {
            _driver.FindElement(SharedObjects.LoginMenu).Click();
        }

        [When(@"Input the credentials (.*) and (.*)")]
        public void WhenInputTheCredentialsAnd(string userName, string password)
        {
            _lifecycle.Fire(new StepStartedEvent("UseName : " + userName));
            _driver.FindElement(SharedObjects.UserName).SendKeys(userName);
            _driver.FindElement(SharedObjects.Password).SendKeys(password);
            _lifecycle.Fire(new StepStartedEvent("DOB : " + "12/JUly/1993"));
            _driver.FindElement(SharedObjects.Date).SelectDropDown("12");
            _driver.FindElement(SharedObjects.Month).SelectDropDown("July");
            _driver.FindElement(SharedObjects.Year).SelectDropDown("1993");
            _lifecycle.Fire(new StepFinishedEvent());
        }

        [When(@"Click on ""(.*)""")]
        public void WhenClickOn(string btnName)
        {
            _lifecycle.Fire(new StepStartedEvent("click On Button : " + btnName));
            _driver.FindElement(SharedObjects.LoginButton).Click();
            _lifecycle.Fire(new StepFinishedEvent());
        }

        [Then(@"Verify user get correct result")]
        public void ThenVerifyUserGetCorrectResult()
        {
            _lifecycle.Fire(new StepStartedEvent("Verification For Error Message : " + _driver.FindElement(SharedObjects.CaptchaErrorMessage).Text));
            Shouldly.ShouldBe(_driver.FindElement(SharedObjects.CaptchaErrorMessage).Text,"reCAPTCHA Error. Please try again!");
            _lifecycle.Fire(new StepFinishedEvent());
        }

    }
}
