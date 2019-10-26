using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Core.Helpers
{

    /// <summary>
    /// Pop Up Helper
    /// </summary>
    public static class PopUpHelper
    {

        /// <summary>
        /// This will reject an alert
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void DismissAlert(this IWebDriver driver, IWebElement element)
        {
            element.Click();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            if (alert != null)
            {
                driver.SwitchTo().Alert();
                Thread.Sleep(3000);
                alert.Dismiss();
            }
        }


        /// <summary>
        /// This will accept an alert
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void AcceptAlert(this IWebDriver driver, IWebElement element)
        {
            element.Click();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            if (alert != null)
            {
                driver.SwitchTo().Alert();
                Thread.Sleep(1000);
                alert.Accept();
            }

        }


        /// <summary>
        /// This will switch and close new  pop up window
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void ClosePopUpWindow(this IWebDriver driver, IWebElement element)
        {
            // Get the current window handle so you can switch back later.
            string currentHandle = driver.CurrentWindowHandle;

            // Find the element that triggers the popup when clicked on.
            element = driver.FindElement(By.XPath("//*[@id='webtraffic_popup_start_button']"));

            // The Click method of the PopupWindowFinder class will click
            // the desired element, wait for the popup to appear, and return
            // the window handle to the popped-up browser window. Note that
            // you still need to switch to the window to manipulate the page
            // displayed by the popup window.
            PopupWindowFinder finder = new PopupWindowFinder(driver);
            string popupWindowHandle = finder.Click(element);

            driver.SwitchTo().Window(popupWindowHandle);

            // Do whatever you need to on the popup browser, then...
            driver.Close();
            driver.SwitchTo().Window(currentHandle);

        }


        /// <summary>
        /// This will verify whether alert text is same as expected
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public static void AlertTextVerify(this IWebDriver driver, IWebElement element, string text)
        {
            try
            {
                element.Click();
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                if (alert != null)
                {
                    driver.SwitchTo().Alert();
                    Thread.Sleep(2000);
                    Assert.IsTrue(alert.Text.Contains(text));
                    alert.Accept();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }


        }


        /// <summary>
        /// This will accept alert if alert is present
        /// </summary>
        /// <param name="driver"></param>
        public static void IsDialogPresent(this IWebDriver driver)
        {
            try
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
                IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                if (alert != null)
                {
                    driver.SwitchTo().Alert();
                    alert.Accept();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        /// <summary>
        /// This will verify Alert text
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="text"></param>
        public static void VerifyAlertText(this IWebDriver driver, string text)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            if (alert != null)
            {
                driver.SwitchTo().Alert();
                Thread.Sleep(1000);
                Console.WriteLine("Text: " + alert.Text);
                Assert.IsTrue(alert.Text.Contains(text));
                alert.Accept();
            }
        }

        /// <summary>
        /// This will handle unexpected alert
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static Boolean IsAlertPresent(this IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
