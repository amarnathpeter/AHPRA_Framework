using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Core.Extensions
{

    /// <summary>
    /// Web Element Extension
    /// </summary>
    public static class WebElementExtension  
    {

        /// <summary>
        /// This will Enter Text in text box
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void EnterText(this IWebElement element, string value)
        {
            try
            {
                element.Clear();
                element.SendKeys(value);
                LogHelper.Write("Entered the text in " + element);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// This will click a button, check box or radio button
        /// </summary>
        /// <param name="element"></param>
        public static void ClickElement(this IWebElement element)
        {
            try
            {
                element.Click();
                LogHelper.Write("Clicked the " + element);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// This will select a value from dropdown list using text
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SelectDropDown(this IWebElement element, string value)
        {
            try
            {
                SelectElement ddl = new SelectElement(element);
                ddl.SelectByText(value);
                LogHelper.Write("Selected the dropdown value from " + element);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// This will verify ccs class of element is same as expected
        /// </summary>
        /// <param name="element"></param>
        /// <param name="CssClass"></param>
        public static void VerifyCssClass(IWebElement element, string CssClass)
        {
            string className = element.GetAttributeValue("class");
            Assert.AreEqual(CssClass, className);
        }


        /// <summary>
        /// This will hover mouse on element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void Hover(this IWebDriver driver, IWebElement element)
        {
            try
            {
                Actions action = new Actions(driver);
                action.MoveToElement(element).Perform();
                action.MoveToElement(element).ToString();
                LogHelper.Write("Hover over action performed on " + element);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// This will hover mouse and click on element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="elementToHover"></param>
        /// <param name="elementToClick"></param>
        public static void HoverAndClick(this IWebDriver driver, IWebElement elementToHover, IWebElement elementToClick)
        {
            try
            {
                Actions action = new Actions(driver);
                action.MoveToElement(elementToHover).Build().Perform();
                action.MoveToElement(elementToClick).Click().Build().Perform();
                LogHelper.Write("Performed hover over on " + elementToHover + " and Click action on " + elementToClick);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Enter tab for any element
        /// </summary>
        /// <param name="element"></param>
        public static void TabControl(this IWebElement element)
        {
            try
            {
                element.SendKeys(Keys.Tab);
                LogHelper.Write(element + " is tabbed");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// This method is used to handle date picker
        /// </summary>
        /// <param name="element"></param>
        public static void TabDatePicker(this IWebElement element)
        {
            try
            {
                LogHelper.Write(element + " has default focus on mm");
                element.SendKeys(Keys.Tab);
                LogHelper.Write(element + " tabbed and focus in on dd");
                element.SendKeys(Keys.Tab);
                LogHelper.Write(element + " tabbed and focus in on yyyy");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// This method is used to get text from any element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetText(this IWebElement element)
        {
            try
            {
                return element.Text;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }

        }


        /// <summary>
        /// This will get Place holder text
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetPlaceHolderText(this IWebElement element)
        {
            try
            {
                return element.GetAttribute("placeholder");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Assert Tag text using textcontent attribute
        /// </summary>
        /// <param name="element"></param>
        /// <param name="expected"></param>
        public static void AssertTagText(this IWebElement element, string expected)
        {
            try
            {
                string actual = element.GetAttribute("textContent");
                Assert.AreEqual(expected, actual);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Get the hover element text
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetHoverText(this IWebDriver driver, IWebElement element)
        {
            try
            {
                Actions action = new Actions(driver);
                LogHelper.Write("Get hover text of " + element);
                return action.MoveToElement(element).ToString();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Select dropdown list value by its index
        /// </summary>
        /// <param name="element"></param>
        /// <param name="index"></param>
        public static void SelectDropDownByIndex(this IWebElement element, int index)
        {
            try
            {
                SelectElement ddl = new SelectElement(element);
                ddl.SelectByIndex(index);
                LogHelper.Write("Selected the dropdown value from " + element);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Get text of first element from drop down
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetSelectedDropDown(this IWebElement element)
        {
            try
            {
                SelectElement ddl = new SelectElement(element);
                LogHelper.Write("Selected Dropdown " + element);
                return ddl.AllSelectedOptions.First().ToString();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Get text of first element or default value from drop down
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetTextFromDropDown(this IWebElement element)
        {
            try
            {
                return new SelectElement(element).AllSelectedOptions.SingleOrDefault() != null ? new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text : null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Get all selected options from dropdown
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IList<IWebElement> GetSelectedListOptions(this IWebElement element)
        {
            try
            {
                SelectElement ddl = new SelectElement(element);
                return ddl.AllSelectedOptions;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Get all options from dropdown
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IList<IWebElement> GetOptions(this IWebElement element)
        {
            try
            {
                SelectElement ddl = new SelectElement(element);
                return ddl.Options;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Get any attribute value of any element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string GetAttributeValue(this IWebElement element, string attribute)
        {
            try
            {
                return element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Verify element is present
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static bool IsElementPresent(IWebElement element)
        {
            Thread.Sleep(1000);
            try
            {
                return element.Enabled;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return false;
            }
        }


        /// <summary>
        /// Verify Element is present
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementPresent(this IWebElement element)
        {
            try
            {
                if (IsElementPresent(element))
                {
                    LogHelper.Write("element exist");
                }
                else
                {
                    LogHelper.Write("element does not exist");
                    Assert.Fail("AssertElementNotPresent TagName:" + element.TagName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Verify Element is present with explicit wait
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// <param name="elementLoc"></param>
        public static void AssertElementPresent(this IWebDriver driver, IWebElement element, By elementLoc)
        {
            try
            {
                driver.WaitForObjectAvaialble(elementLoc);
                if (IsElementPresent(element))
                {
                    LogHelper.Write("element exist");
                }
                else
                {
                    LogHelper.Write("element does not exist");
                    Assert.Fail("AssertElementNotPresent TagName:" + element.TagName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Verify Element is not present
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementIsNotPresent(this IWebElement element)
        {
            try
            {
                if (IsElementPresent(element))
                {
                    LogHelper.Write("element exist");
                    Assert.Fail("AssertElementPresent");
                }
                else
                {
                    LogHelper.Write("element does not exist");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Verify Element is hidden
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementHidden(this IWebElement element)
        {
            try
            {
                if (IsElementPresent(element))
                {
                    LogHelper.Write("element exist");
                    Assert.Fail("AssertElementPresent");
                }
                else
                {
                    LogHelper.Write("element does not exist");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Verify element is enabled
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementEnabled(this IWebElement element)
        {
            try
            {
                if (element.Enabled)
                {
                    LogHelper.Write("element enabled");
                }
                else
                {
                    LogHelper.Write("element disabled");
                    Assert.Fail("AssertElementDisabled");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Verify Element is disabled
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementDisabled(this IWebElement element)
        {
            try
            {
                Thread.Sleep(1000);
                if (element.Enabled)
                {
                    LogHelper.Write("Element enabled");
                    Assert.Fail("AssertElementEnabled");
                }
                else
                {
                    LogHelper.Write("Element disabled");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// Verify Element text to be from given string array
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public static void AssertElementText(this IWebElement element, string[] text)
        {
            try
            {               
                for (int i = 0; i < text.Length; i++)
                {
                    StringAssert.Contains(text[i], element.GetText());
                }
            }
            catch (AssertionException ex)
            {
                LogHelper.LogException(ex);
                Assert.Fail("AssertElementText Failed");
            }
        }


        /// <summary>
        /// This will return true if element is colored
        /// </summary>
        /// <param name="element"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        private static bool IsHeaderAppearsColored(IWebElement element, string cssClass)
        {
            try
            {
                string actual = element.GetAttribute("class");
                string expected = cssClass;
                Assert.AreEqual(expected, actual);
                return true;
            }
            catch (AssertionException)
            {
                return false;
            }
        }


        /// <summary>
        /// This will verify given attribute value to be same as expected
        /// </summary>
        /// <param name="element"></param>
        /// <param name="expected"></param>
        /// <param name="attribute"></param>
        public static void AssertAttributeValue(this IWebElement element, string expected, string attribute)
        {
            try
            {
                string actual = element.GetAttribute(attribute);
                Assert.AreEqual(expected, actual);
            }
            catch (AssertionException)
            {
                Assert.Fail("AssertAttribute Failed");
            }
        }


        /// <summary>
        /// This will verify whether Header is colored or not
        /// </summary>
        /// <param name="element"></param>
        /// <param name="cssClass"></param>
        public static void AssertHeaderAppearsColored(this IWebElement element, string cssClass)
        {
            try
            {
                if (IsHeaderAppearsColored(element, cssClass))
                {
                    LogHelper.Write("Element Color Verified");
                }
                else
                {
                    LogHelper.Write("Element Color Not Verified");
                    Assert.Fail("AssertElementNotColored");
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// This will return true if element is clickable
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static bool IsElementClickable(IWebElement element)
        {
            try
            {
                return (element.Enabled);
            }
            catch (AssertionException)
            {
                return false;
            }
        }


        /// <summary>
        /// This will verify whether element is clickable,and add log in log file
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementClickable(this IWebElement element)
        {
            if (IsElementClickable(element))
            {
                LogHelper.Write("Element is clickable");
            }
            else
            {
                LogHelper.Write("Element is not clickable");
                Assert.Fail("AssertElementNotClickable");
            }
        }


        /// <summary>
        /// This will verify whether checkbox is checked
        /// </summary>
        /// <param name="element"></param>
        public static void AssertCheckBoxChecked(this IWebElement element)
        {
            if (element.Selected)
            {
                LogHelper.Write("Element is Selected");
                Assert.Pass("AssertElementSelected");
            }
            else
            {
                LogHelper.Write("Element is not Selected");
                Assert.Fail("AssertElementNotSelected");
            }
        }


        /// <summary>
        /// This will verify whether checkbox is unchecked
        /// </summary>
        /// <param name="element"></param>
        public static void AssertCheckBoxUnChecked(this IWebElement element)
        {
            if (!element.Selected)
            {
                LogHelper.Write("Element is not Selected");
                Assert.Pass("AssertElement not Selected");
            }
            else
            {
                LogHelper.Write("Element is Selected");
                Assert.Fail("AssertElementSelected");
            }
        }


        /// <summary>
        /// This will check and uncheck the checkbox as per given string
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetCheckBox(this IWebElement element, string value)
        {
            if (value.ToLower().Equals("uncheck") && element.Selected)
            {
                element.Click();
            }
            else if (value.ToLower().Equals("check") && !element.Selected)
            {
                element.Click();
            }
        }


        /// <summary>
        /// This will verify whether Tool Tip Text is same as expected
        /// </summary>
        /// <param name="element"></param>
        /// <param name="atribute"></param>
        /// <param name="ExpectedToolTipText"></param>
        public static void AsserttoolTipText(this IWebElement element, string atribute, string ExpectedToolTipText)
        {
            // Get tooltip text
            string ActualToolTipText = element.GetAttribute(atribute);
            // Compare toll tip text
            Assert.AreEqual(ExpectedToolTipText, ActualToolTipText);
        }


        /// <summary>
        /// This will verify element is not clickable
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementNotClickable(this IWebElement element)
        {
            if (!IsElementClickable(element))
            {
                LogHelper.Write("Element is mot clickable");
            }
            else
            {
                LogHelper.Write("Element is clickable");
                Assert.Fail("AssertElementClickable");
            }
        }


        /// <summary>
        /// This will verify if element present, which is not expected
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void AssertTrueIfelementPresent(this IWebDriver driver, By element)
        {
            IReadOnlyCollection<IWebElement> list = driver.FindElements(element);
            if (list.Count== 0)
            {
                LogHelper.Write("Element Not found As expected");
                Assert.IsTrue(true);
            }
            else
            {
                LogHelper.Write("Element Found but expected to be absent.");
                Assert.Fail("AssertElementClickable");
            }
        }

        /// <summary>
        /// This will refresh web page
        /// </summary>
        /// <param name="driver"></param>
        public static void RefreshPage(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        /// <summary>
        /// This will scroll web page till element height
        /// </summary>
        /// <param name="driver"></param>
        public static void ScrollTo(this IWebDriver driver)
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
        /// This will scroll web page till element present
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        public static void ScrollToElement(this IWebDriver driver, By by)
        {
            IWebElement element = driver.FindElement(by);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        /// This will verify if element text is present
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public static void AssertElementTextPresent(this IWebElement element, string text)
        {
            try
            {
                if (IsElementPresent(element))
                {
                    StringAssert.Contains(text, element.GetText().Trim());
                }
                else
                {
                    LogHelper.Write("Element does not exist");
                    Assert.Fail("AssertElementTextPresent TagName:" + element.TagName);
                }
            }
            catch (AssertionException ex)
            {
                LogHelper.LogException(ex);
                Assert.Fail("AssertElementTextPresent Failed");
            }
        }

        /// <summary>
        /// This will wait till web element is present
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="attempts"></param>
        public static void WaitForElementPresent(this IWebDriver driver, By by, int attempts)
        {
            int count = 0;
            int count1 = 1;
            while (count < 1 && count1 <= attempts)
            {
                Thread.Sleep(4000);
                count = driver.FindElements(by).Count;
                count1++;
            }
        }
    }
}

