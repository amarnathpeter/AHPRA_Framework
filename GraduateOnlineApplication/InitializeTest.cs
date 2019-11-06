using Allure.Commons;
using AllureCSharpCommons.Events;
using AventStack.ExtentReports;
using BoDi;
using Core.Base;
using Core.Config;
using Core.Configuration;
using Core.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace GOA
{
    [Binding]
    public class InitializeTest :ConfigurationSettings
    {
        private static readonly AppSettings AppSettings = GetAppSettings();
        static AllureCSharpCommons.Allure _lifecycle = AllureCSharpCommons.Allure.Lifecycle;
        static string SuiteUid = "RegressionPack";
        /// <summary>
        /// The object test base
        /// </summary>
        static TestBase objTestBase = new TestBase();

        /// <summary>
        /// The browser
        /// </summary>
        private static string browser = "Chrome";
        AppProprties prop=new AppProprties();
        /// <summary>
        /// The test
        /// </summary>
        private ExtentTest _test;
        /// <summary>
        /// The object container1
        /// </summary>
        private readonly IObjectContainer _objectContainer1, _objectContainer2, _objectContainer3;
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeTest"/> class.
        /// </summary>
        /// <param name="objectContainer1">The object container1.</param>
        /// <param name="objectContainer2">The object container2.</param>
        public InitializeTest(IObjectContainer objectContainer1, IObjectContainer objectContainer2, IObjectContainer objectCOntainer3) : base(AppSettings)
        {
            _objectContainer1 = objectContainer1;
            _objectContainer2 = objectContainer2;
            _objectContainer3 = objectCOntainer3;
            AppProprties prop = new AppProprties();
        }
        /// <summary>
        /// Initials the set up.
        /// </summary>
        [BeforeTestRun]
        public static void InitialSetUp()
        {
            _lifecycle.Fire(new TestSuiteStartedEvent(SuiteUid, TestContext.CurrentContext.Test.FullName));
            if (Settings.Parallelizable == null)
            {
                ConfigReader.SetFrameworkSettings();
            }
            objTestBase.TestSetUp("Automation");
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        /// <summary>
        /// Creates the test set up.
        /// </summary>
        [BeforeScenario]
        public void CreateTestSetUp()
        {
            if (Settings.UserName == null)
            {
                ConfigReader.SetFrameworkSettings();
            }
            prop.AppProp._driver = objTestBase.StartTestExecution(_appSettings.environmentSettings.BrowserName, prop.AppProp._driver);
            _test = objTestBase.CreateExtentObjectParallel(_appSettings.environmentSettings.BrowserName, _test);
            _objectContainer1.RegisterInstanceAs<IWebDriver>(prop.AppProp._driver);
            _objectContainer2.RegisterInstanceAs<ExtentTest>(_test);
            _objectContainer3.RegisterInstanceAs<AllureCSharpCommons.Allure>(_lifecycle);
            prop.AppProp._driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            _lifecycle.Fire(new TestCaseStartedEvent(SuiteUid, ScenarioContext.Current.ScenarioInfo.Title));
            prop.AppProp._driver.Navigate().GoToUrl(_appSettings.environmentSettings.ApplicationURL);
        }
        /// <summary>
        /// Teardowns this instance.
        /// </summary>
        [AfterScenario]
        public void Teardown()
        {
            AllureEnvProperties.SetAllureEnvProperties(_appSettings);
            _lifecycle.Fire(new TestCaseFinishedEvent());
            DictionaryProperties.Details.Clear();
            objTestBase.StopReportParallel(prop.AppProp._driver, _test);        
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [AfterTestRun]
        public static void CleanUp()
        {
            objTestBase.CloseBrowser(browser);
            objTestBase.CloseCurrentDriverServer(browser);
            ExtentReportBase.extent.Flush();
        }
      
    }
}
