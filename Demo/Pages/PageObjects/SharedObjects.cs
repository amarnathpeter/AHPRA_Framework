using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Pages.PageObjects
{
    public class SharedObjects
    {
        public static By UserName = By.Id("content_0_contentcolumnmain_1_txtUser");
        public static By Password = By.Id("content_0_contentcolumnmain_1_txtPass");
        public static By LoginMenu = By.Id("ctl22_ctl00_hlLoggedinStatus");
        public static By Date = By.Id("content_0_contentcolumnmain_1_drpDD");
        public static By Month = By.Id("content_0_contentcolumnmain_1_drpMM");
        public static By Year = By.Id("content_0_contentcolumnmain_1_drpYY");
        public static By LoginButton = By.Id("content_0_contentcolumnmain_1_btnSubmit");
        public static By CaptchaErrorMessage = By.Id("content_0_contentcolumnmain_1_lblError");
    }
}
