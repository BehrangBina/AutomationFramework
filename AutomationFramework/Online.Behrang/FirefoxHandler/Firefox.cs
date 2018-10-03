using System;
using System.IO;
using AutomationFramework.Util.Behrang.ConfigurationHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;

namespace AutomationFramework.Online.Behrang.FirefoxHandler
{
    public class Firefox:IBrowserHandler
    {
        private IWebDriver _driver;
        private const int ImplicitTimeWaitInSeconds = 30;
        private const int PageLoadTimeoutInSeconds = 30;
        public Firefox SetupFirefoxWithOption()
        {
            FirefoxProfile ffProfile = new FirefoxProfile();
            ffProfile.SetPreference("acceptInsecureCerts", true);
            ffProfile.SetPreference("security.enterprise_roots.enabled", true);
            var firefpoxOptions = new FirefoxOptions { Profile = ffProfile };
          

            var firingDriver = new EventFiringWebDriver(new FirefoxDriver(firefpoxOptions));
            firingDriver.ExceptionThrown += firingDriver_TakeScreenshotOnException;

            firingDriver
                .Manage()
                .Timeouts()
                .ImplicitWait = (TimeSpan.FromSeconds(ImplicitTimeWaitInSeconds));
            firingDriver
                .Manage()
                .Timeouts()
                .PageLoad = (TimeSpan.FromSeconds(PageLoadTimeoutInSeconds));
            _driver = firingDriver;
            _driver.Manage().Window.Maximize();
            return this;
        }

        public IWebDriver GetDriverInstance()
        {
            return _driver;
        }

        /// <summary>
        /// Handling exceptions occure in the webbrowser and 
        /// takes the screenshot and write the logs
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event exception</param>
        public void firingDriver_TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");

            var conf = new AppConfigHandler();
            var folderPath = conf.ReadFolderPathFromConfigurationFile(SolutionSubFolder.Logs);
            var imagePath = Path.Combine(folderPath, "FirefoxException-" + timestamp + ".png");
            _driver.TakeScreenshot().SaveAsFile(imagePath, ScreenshotImageFormat.Bmp);
            var filePath = Path.Combine(folderPath, "FirefoxException-" + timestamp + ".txt");
            File.WriteAllText(filePath, e.ThrownException.ToString());
        }
    }
}
