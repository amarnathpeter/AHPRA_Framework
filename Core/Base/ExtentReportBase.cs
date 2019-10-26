using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Core.Config;
using Core.Helpers;
using System;
using System.Net.Mail;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.IO;
using Core.Configuration;

namespace Core.Base
{

    /// <summary>
    /// Extent Report Base
    /// </summary>
    public class ExtentReportBase
    {       

        /// <summary>
        /// The extent
        /// </summary>
        public static ExtentReports extent;

        /// <summary>
        /// The test
        /// </summary>
        public ExtentTest test;

        /// <summary>
        /// Getting the Project Path
        /// </summary>
        public static string ProjectPath
        {
            get
            {
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = path.Substring(0, path.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                return projectPath;
            }
        }

        /// <summary>
        /// Getting the Report Path
        /// </summary>
        public static string ReportName
        {
            get
            {
                string reportPath = ProjectPath + "_ExtentReport.html";
                return reportPath;
            }
        }

        /// <summary>
        /// This method is used to initialized Extent Report
        /// </summary>
        public void StartReport()
        {
            if (Settings.IEVersion == null)
            {
                ConfigReader.SetFrameworkSettings();
            }
            var htmlReporter = new ExtentHtmlReporter(ReportName);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        /// <summary>
        /// This method is used to Flush Extent Report in Sequential execution
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="test"></param>
        /// <param name="stepName"></param>
        /// <param name="scenarioTitle"></param>
        public void StopReportSequential(IWebDriver driver, ExtentTest test, string stepName, string scenarioTitle)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            var errorMessage = TestContext.CurrentContext.Result.Message;

            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            if (status == TestStatus.Failed)
            {
                string screenShotPath = TakeScreenShot(driver);
                test.Log(Status.Info, "Valid APP number entered in search box");
                test.Log(logstatus, "Test Step **" + stepName + "** !!" + logstatus + "!! In Scenario **" + scenarioTitle + "** " + stacktrace + errorMessage);
                test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
            }
            else
            {
                test.Log(logstatus, "Test scenario **" + scenarioTitle + "** ended with !!" + logstatus + "!!" + stacktrace + errorMessage);
            }

            if (driver != null)
            {
                driver.Quit();
            }
            extent.Flush();
        }

        /// <summary>
        /// This method is used to Flush extent Report in Parallel execution
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="test"></param>
        public void StopReportParallel(IWebDriver driver, ExtentTest test)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var scenarioname = TestContext.CurrentContext.Test.Name;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            var errorMessage = TestContext.CurrentContext.Result.Message;
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            if (status == TestStatus.Failed)
            {
                string screenShotPath = TakeScreenShot(driver);
                test.Log(logstatus, "Test Scenario **" + scenarioname + "** ended with !!" + logstatus + "!!" + stacktrace + errorMessage);
                test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
            }
            else
            {
                test.Log(logstatus, "Test scenario **" + scenarioname + "** ended with !!" + logstatus + "!!" + stacktrace + errorMessage);
            }

            if (driver != null)
            {
                driver.Quit();
            }
            extent.Flush();
        }

        /// <summary>
        /// This method is used to take Screenshot 
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string TakeScreenShot(IWebDriver driver)
        {
            try
            {
                string localPath = "";
                string screenshotName = "";
                string strdatetime = DateTime.Now.ToString("yyyy_dd_M_HH_mm_ss");
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string stepName = TestContext.CurrentContext.Test.Name;
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string finalpth = path.Substring(0, path.LastIndexOf("bin")) + Settings.ScreenShotLocation;
                string dir = new Uri(finalpth).LocalPath;
                if (Directory.Exists(dir))
                {
                    screenshotName = path.Substring(0, path.LastIndexOf("bin")) + Settings.ScreenShotLocation + stepName + strdatetime + ".png";
                    localPath = new Uri(screenshotName).LocalPath;
                    ss.SaveAsFile(localPath);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                    screenshotName = path.Substring(0, path.LastIndexOf("bin")) + Settings.ScreenShotLocation + stepName + strdatetime + ".png";
                    localPath = new Uri(screenshotName).LocalPath;
                    ss.SaveAsFile(localPath);
                }

                return localPath;
            }
            catch (Exception e)
            {
                LogHelper.LogException(e);
                throw;
            }
        }

        /// <summary>
        /// This method is used to Email test report
        /// </summary>
        public static void EmailReport()
        {
            try
            {
                if (Settings.SendEmailReport)
                {
                    SmtpClient mailClient = new SmtpClient();
                    mailClient.Port = Settings.SMTPPort;
                    mailClient.Host = Settings.SMTPHost;
                    mailClient.EnableSsl = Settings.SMTPEnableSSL;
                    mailClient.Timeout = Settings.SMTPTimeout;
                    mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mailClient.UseDefaultCredentials = false;
                    mailClient.Credentials = new System.Net.NetworkCredential(Settings.SMTPUserName, Settings.SMTPPassword);
                    MailMessage mail = new MailMessage(Settings.EmailFrom, Settings.EmailGroup);
                    mail.Subject = Settings.EmailSubject;
                    mail.Attachments.Add(new System.Net.Mail.Attachment(ReportName));
                    mail.Body = Settings.EmailBody;
                    mail.BodyEncoding = UTF8Encoding.UTF8;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    mailClient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        /// <summary>
        /// This method is used to add screenshot in extent report
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="test"></param>
        public void AddAssertionScreenshot(IWebDriver driver, ExtentTest test)
        {
            string screenShotPath = TakeScreenShot(driver);
            test.Log(Status.Info, "Assert Screenshot: " + TestContext.CurrentContext.Test.Name + ".png" + test.AddScreenCaptureFromPath(screenShotPath));
        }   



    }
}
