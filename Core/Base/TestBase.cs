using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using AventStack.ExtentReports;
using Core.Config;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.IO;
using AllureCSharpCommons.Events;

namespace Core.Base
{

    /// <summary>
    /// Test Base
    /// </summary>
    /// <seealso cref="Core.Base.ExtentReportBase" />
    public class TestBase : ExtentReportBase
    {

        /// <summary>
        /// The element
        /// </summary>
        public static IReadOnlyCollection<IWebElement> _element;

        /// <summary>
        /// The snap shot file name
        /// </summary>
        private static string _snapShotFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);

        /// <summary>
        /// The browser instance
        /// </summary>
        IEnumerable<int> _browserInstance;

        /// <summary>
        /// Gets or sets the current browser.
        /// </summary>
        /// <value>
        /// The current browser.
        /// </value>
        public string _currentBrowser { get; set; }

        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        /// <value>
        /// The driver.
        /// </value>
        public IWebDriver _driver { get; set; }

        /// <summary>
        /// This method is used to initialized webdriver instance
        /// </summary>
        /// <param name="Browser"></param>
        /// <returns></returns>
        private IWebDriver OpenBrowser(String Browser)
        {
            switch (Browser.ToUpper())
            {
                case "IE":
                    if (Settings.Grid == "Yes")
                    {
                        InternetExplorerOptions options = new InternetExplorerOptions();
                        options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                        options.PlatformName = "windows";
                        _driver = new RemoteWebDriver(new Uri("http://192.168.10.56:4444/wd/hub"), options.ToCapabilities(), TimeSpan.FromSeconds(600));
                        IAllowsFileDetection allowsDetection = _driver as IAllowsFileDetection;
                        if (allowsDetection != null)
                        {
                            allowsDetection.FileDetector = new LocalFileDetector();
                        }
                    }
                    else
                    {
                        InternetExplorerOptions options = new InternetExplorerOptions();
                        options.IgnoreZoomLevel = true;
                        options.EnableNativeEvents = true;
                        options.EnablePersistentHover = true;
                        options.RequireWindowFocus = false;
                        options.BrowserAttachTimeout = TimeSpan.FromSeconds(120);
                        string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                        string finalpth = path.Substring(0, path.LastIndexOf("bin"));
                        string localpath = new Uri(finalpth).LocalPath;
                        _driver = new InternetExplorerDriver(localpath, options, TimeSpan.FromSeconds(140));
                    }
                    break;

                case "FIREFOX":
                    _driver = new FirefoxDriver();
                    break;

                case "CHROME":
                    {
                        ChromeOptions options = new ChromeOptions();
                        options.PlatformName = "Windows";
                        string rootPath = Directory.GetParent(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName;
                        _driver = new ChromeDriver(rootPath + "\\Driver", options, TimeSpan.FromSeconds(140));
                        break;
                    }
                case "SAFARI":
                    _driver = new SafariDriver();
                    break;

                case "EDGE":
                    _driver = new EdgeDriver();
                    break;

                case "OPERA":
                    _driver = new OperaDriver();
                    break;

                default:
                    break;
            }
            return _driver;
        }

        /// <summary>
        /// This method is used to create object of Extent Report and LogHelper
        /// </summary>
        /// <param name="TestScriptName"></param>
        /// <param name="useTestData"></param>
        public void TestSetUp(string TestScriptName, bool useTestData = true)
        {
            try
            {
                if (useTestData)
                {
                    StartReport();
                }
                LogHelper.CreateLogFile(TestScriptName);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// This method is used to Open and maximized browser
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public IWebDriver StartTestExecution(String browser, IWebDriver driver)
        {
            try
            {
                _currentBrowser = browser;
                _browserInstance = GetCurrentBrowserInstances(browser);
                driver = OpenBrowser(browser);
                MaximizeBrowser();
                DeleteAllCookies();
                return driver;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// This method is used to create instance of Extent Test in parallel execution
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public ExtentTest CreateExtentObjectParallel(String Browser, ExtentTest test)
        {
            var testname = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testname + "_" + Browser);
            test.Log(Status.Info, "Parallel Execution:");
            return test;
        }

        /// <summary>
        /// This method is used to create instance of Extent Test in sequential execution
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="test"></param>
        /// <param name="scenario"></param>
        /// <returns></returns>
        public ExtentTest CreateExtentObjectSequential(String Browser, ExtentTest test, String scenario)
        {
            try
            {
                var testname = scenario;
                test = extent.CreateTest(testname + "_" + Browser);
                return test;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw;
            }
        }


        /// <summary>
        /// This method is used to kill Firefox driver process
        /// </summary>
        public void TestCleanUp()
        {
            try
            {
                DeleteAllCookies();
                if (_driver != null)
                {
                    Process[] FirefoxDriverProcesses = Process.GetProcessesByName("firefox");

                    foreach (var FirefoxDriverProcess in FirefoxDriverProcesses)
                    {
                        FirefoxDriverProcess.Kill();
                    }
                    _driver.Quit();
                }
                LogHelper.Write("Closed the Browser");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }


        /// <summary>
        /// This method is used to kill any type driver process
        /// </summary>
        /// <param name="browser"></param>
        public void CloseBrowser(string browser)
        {
            IEnumerable<int> currentInstance;
            var currentBrowserInstances = GetCurrentBrowserInstances(browser);
            if (_browserInstance != null && _browserInstance.Any())
            {
                currentInstance = currentBrowserInstances.Except(_browserInstance);
            }
            else
            {
                currentInstance = currentBrowserInstances;
            }

            foreach (int instance in currentInstance)
            {
                Process.GetProcessById(instance).Kill();
            }
        }


        /// <summary>
        /// This method is used to kill IEDriver Server
        /// </summary>
        /// <param name="browser"></param>
        public void CloseCurrentDriverServer(string browser)
        {
            IEnumerable<int> currentInstance;
            var IEDriverInstances = GetCurrentDriverInstances(browser);
            if (_browserInstance != null && _browserInstance.Any())
            {
                currentInstance = IEDriverInstances.Except(_browserInstance);
            }
            else
            {
                currentInstance = IEDriverInstances;
            }

            foreach (int instance in currentInstance)
            {
                Process.GetProcessById(instance).Kill();
            }
        }


        /// <summary>
        /// This metthod is used to get current browser instances
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        private IEnumerable<int> GetCurrentBrowserInstances(string browser)
        {
            string processName = string.Empty;
            List<int> pIdList = null;
            switch (browser.ToUpper())
            {
                case "IE":
                    processName = "iexplore";
                    break;
                case "CHROME":
                    processName = "Chrome";
                    break;
                case "FIREFOX":
                    processName = "Firefox";
                    break;
            }
            if (!string.IsNullOrEmpty(processName))
            {
                Process[] processArray = Process.GetProcessesByName(processName);
                if (processArray != null && processArray.Length > 0)
                {
                    pIdList = new List<int>();
                    foreach (Process p in processArray)
                    {
                        pIdList.Add(p.Id);
                    }
                }
            }
            return pIdList;
        }

        /// <summary>
        /// This method is used to get current driver instances
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        private IEnumerable<int> GetCurrentDriverInstances(string browser)
        {
            string processName = string.Empty;
            List<int> pIdList = null;
            switch (browser.ToUpper())
            {
                case "IE":
                    processName = "IEDriverServer";
                    break;
                case "CHROME":
                    processName = "chromedriver";
                    break;
                case "FIREFOX":
                    processName = "Firefox";
                    break;
            }
            if (!string.IsNullOrEmpty(processName))
            {
                Process[] processArray = Process.GetProcessesByName(processName);
                if (processArray != null && processArray.Length > 0)
                {
                    pIdList = new List<int>();
                    foreach (Process p in processArray)
                    {
                        pIdList.Add(p.Id);
                    }
                }
            }
            return pIdList;
        }


        /// <summary>
        /// This method is used to Clear IE
        /// </summary>
        public void ClearIECache()
        {
            var options = new InternetExplorerOptions();
            options.EnsureCleanSession = true;
            _driver = new InternetExplorerDriver(options);
        }


        ///// <summary>
        ///// This method is used to launch Url
        ///// </summary>
        ///// <param name="_driver"></param>
        //public void NavigateToURL(IWebDriver _driver, AllureCSharpCommons.Allure _lifecycle)
        //{
        //    _lifecycle.Fire(new StepStartedEvent("URL : " + Settings.AUT));
        //    string AbsoluteURL = Settings.AbsoluteURL;
        //    string URL = Settings.AUT;
        //    URL = "https://" + URL;
        //    if ((!String.IsNullOrEmpty(AbsoluteURL)))
        //    {
        //        _driver.Navigate().GoToUrl(AbsoluteURL);
        //    }
        //    else
        //    {
        //        _driver.Navigate().GoToUrl(URL);
        //    }
        //    LogHelper.Write("Navigated to the URL");
        //    _lifecycle.Fire(new StepFinishedEvent());
        //}

        /// <summary>
        /// This method is used to set executing browser
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<String> BrowserToRunWith()
        {
            String[] browsers = null;
            if (Settings.ExecutingBrowser == null)
            {
                ConfigReader.SetFrameworkSettings();
            }
            browsers = Settings.ExecutingBrowser.Split(',');
            foreach (String b in browsers)
            {
                yield return b;
            }
        }


        /// <summary>
        /// This method is used to navigate forward
        /// </summary>
        public void NavigateForward()
        {
            _driver.Navigate().Forward();
        }


        /// <summary>
        /// This method is used to navigate back
        /// </summary>
        public void NavigateBack() => _driver.Navigate().Back();


        /// <summary>
        /// This method is used to refresh web page
        /// </summary>
        public void RefreshPage() => _driver.Navigate().Refresh();


        /// <summary>
        /// This method is used to maximized the browser
        /// </summary>
        public void MaximizeBrowser() => _driver.Manage().Window.Maximize();


        /// <summary>
        /// This method is used to delete cookies
        /// </summary>
        public void DeleteAllCookies() => _driver.Manage().Cookies.DeleteAllCookies();


        /// <summary>
        /// This method is used to resize browser size
        /// </summary>
        public void ResizeBrowserToTabSize() => ResizeBrowser(960, 640);


        /// <summary>
        /// This method is used to read data from excel
        /// </summary>
        /// <param name="testScripName"></param>
        public void PopulateExcelData(string testScripName)
        {
            if (Settings.TestDataPath == null)
            {
                ConfigReader.SetFrameworkSettings();
            }
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + Settings.TestDataPath + testScripName + ".xlsx";
            string testDatafileName = new Uri(finalpth).LocalPath;
            ExcelHelper.PopulateInCollection(testDatafileName);
        }


        /// <summary>
        /// This method is used to resize browser in desired size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void ResizeBrowser(int width, int height) => _driver.Manage().Window.Size = new System.Drawing.Size(width, height);

        /// <summary>
        /// This method is used to delete old Screensize and logs
        /// </summary>
        public void DeleteOldLogsAndScreenshots()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            if (Settings.ScreenShotLocation == null)
            {
                ConfigReader.SetFrameworkSettings();
            }
            string finalpth = path.Substring(0, path.LastIndexOf("bin")) + Settings.ScreenShotLocation;
            string localpath = new Uri(finalpth).LocalPath;
            if (Directory.Exists(localpath))
            {
                string[] files = Directory.GetFiles(localpath);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
                Directory.Delete(localpath);
            }

        }
    }
}
