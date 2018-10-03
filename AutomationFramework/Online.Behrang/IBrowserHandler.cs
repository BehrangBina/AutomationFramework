using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace AutomationFramework.Online.Behrang
{
    interface IBrowserHandler
    {

        /// <summary>
        /// Handling exceptions occure in the webbrowser and 
        /// takes the screenshot and write the logs
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event exception</param>
        void firingDriver_TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e);
        IWebDriver GetDriverInstance();
    }
}
