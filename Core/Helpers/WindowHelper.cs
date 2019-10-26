using OpenQA.Selenium;
using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{

    /// <summary>
    /// Window Helper
    /// </summary>
    /// <seealso cref="Core.Base.TestBase" />
    public class WindowHelper : TestBase
    {
        /// <summary>
        /// This will switch to child window
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static string SwitchToChildWindow(IWebDriver driver)
        {
            string main_window = driver.CurrentWindowHandle;
            string child_handle="";
            var handles = driver.WindowHandles.GetEnumerator();
            while (handles.MoveNext())
            {
                if (handles.Current != main_window)
                {
                    child_handle = handles.Current;
                    driver.SwitchTo().Window(child_handle);
                    break;
                }
            }
            return main_window;
        }

        /// <summary>
        /// This will switch driver to third window
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="Mainwindowhandle"></param>
        /// <param name="Secondwindowhandle"></param>
        public static void SwitchToThirdChildWindow(IWebDriver driver, string Mainwindowhandle, string Secondwindowhandle)
        {

            string child_handle = "";
            var handles = driver.WindowHandles.GetEnumerator();
            while (handles.MoveNext())
            {
                if (handles.Current != Mainwindowhandle && handles.Current != Secondwindowhandle)
                {
                    child_handle = handles.Current;
                    driver.SwitchTo().Window(child_handle);
                    break;
                }
            }
        }


        /// <summary>
        /// This will switch back to parent window
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="main_window"></param>
        public static void SwitchToMainWindow(IWebDriver driver, String main_window)
        {
            driver.SwitchTo().Window(main_window);
        }

        /// <summary>
        /// This will close all windows except main window
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="main_window"></param>
        public static void CloseAllWindowsExceptMain(IWebDriver driver, string main_window)
        {
            var handles = driver.WindowHandles.GetEnumerator();
            while (handles.MoveNext())
            {
                if (handles.Current != main_window)
                {
                    driver.SwitchTo().Window(handles.Current);
                    driver.Close();
                }
            }
        }
    }
}
