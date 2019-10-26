using OpenQA.Selenium;
using System;
using System.Threading;
using Core.Helpers;
using Core.Config;
using OpenQA.Selenium.Support.UI;

namespace Core.Extensions
{

    /// <summary>
    /// Web Driver Extension
    /// </summary>
    public static class WebDriverExtension
    {
        /// <summary>
        /// This method is used to wait for web page load completely 
        /// </summary>
        /// <param name="driver"></param>
        public static void WaitForPageLoad(this IWebDriver driver)
        {
            Thread.Sleep(6000);
            int waitTime = 240;
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            //Initially bellow given if condition will check ready state of page.
            if (js.ExecuteScript("return document.readyState").ToString().Equals("complete"))
            {
                return;
            }

            //This loop will rotate for 'waitTime' times to check If page Is ready after every 1 second.
            //You can replace your value with 'waitTime' If you wants to Increase or decrease wait time.
            for (int i = 0; i < waitTime; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e);
                }
                //To check page ready state.
                if (js.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Use this wait statement for all the editboxes & tables
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void WaitForObjectAvaialble(this IWebDriver driver, By element)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(60))
                       .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }



        /// <summary>
        /// Use this wait statement before clicking on any Buttons, links, Dropdowns, Checkboxes, column headers, images, icons 
        /// or on any element where you need to perform click operation
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void WaitForElementToBeClickable(this IWebDriver driver, By element)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(100))
                                  .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Use this wait statement if the objects are loaded with the iFrame
        /// </summary>
        /// <param name="driver"></param>
        public static void WaitForiFrameLoad(this IWebDriver driver)
        {
            IWebElement iFrameHost = new WebDriverWait(driver, TimeSpan.FromSeconds(100))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("iFrameHost")));
            driver.SwitchTo().DefaultContent(); // you are now outside both frames
            driver.SwitchTo().Frame(iFrameHost);
        }


        /// <summary>
        /// Use this wait statements if the page has to make ajax calls for certain objects to load
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="pageHasJQuery"></param>
        public static void WaitForAjaxLoad(this IWebDriver browser, bool pageHasJQuery = true)
        {
            while (true)
            {
                var ajaxIsComplete = false;

                if (pageHasJQuery)
                    ajaxIsComplete = (bool)(browser as IJavaScriptExecutor).ExecuteScript("if (!window.jQuery) { return false; } else { return jQuery.active == 0; }");
                else
                    ajaxIsComplete = (bool)(browser as IJavaScriptExecutor).ExecuteScript("return document.readyState == 'complete'");

                if (ajaxIsComplete)
                    break;

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// This method is used to wait for an element displayed and enabled
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        /// <param name="secondsToWait"></param>
        public static void WaitForElementPresentAndEnabled(this IWebDriver driver, By locator, int secondsToWait)
        {
           Thread.Sleep(4000);
            new WebDriverWait(driver, new TimeSpan(0, 0, secondsToWait))
               .Until(d => d.FindElement(locator).Enabled && d.FindElement(locator).Displayed) ;
        }


        /// <summary>
        /// This method is used to get current URL
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string GetCurrentURL(this IWebDriver driver)
        {
            return driver.Url;
        }


        /// <summary>
        /// This method is used to get Current Window Title
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string GetTitle(this IWebDriver driver)
        {
            return (driver.Title);
        }


        /// <summary>
        /// This is used to select locator type
        /// </summary>
        public enum Type
        {
            Id,
            CssSelector,
            XPath,
            ClassName,
            Name,
            LinkText,
            IWebElement
        }


        /// <summary>
        /// This method is used to create instance of JavaScript Executer
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        internal static object ExecuteJS(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }

        /// <summary>
        /// This method is stop processor for some time
        /// </summary>
        /// <param name="driver"></param>
        public static void ExtraWait(this IWebDriver driver)
        {
            Thread.Sleep(5000);
        }

        /// <summary>
        /// This method is used to wait for new window open
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="num"></param>
        /// <param name="timeout"></param>
        public static void WaitForDriverLoad(this IWebDriver driver, int num, int timeout)
        {
            Thread.Sleep(2000);
            new WebDriverWait(driver, new TimeSpan(0, 0, timeout))
            .Until(d => d.WindowHandles.Count == num);

        }

        /// <summary>
        /// This method is used to scroll web page till end
        /// </summary>
        /// <param name="driver"></param>
        public static void ScrollTillEnd(this IWebDriver driver)
        {
            long scrollHeight = 0;
            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(400);
                }
            } while (true);
        }

        /// <summary>
        /// This method is used to scroll a web page till specified element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="locator"></param>
        public static void ScrollTillElment(this IWebDriver driver, By locator)
        {
            IWebElement elem = driver.FindElement(locator);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(false);", elem);
        }
    }
}
