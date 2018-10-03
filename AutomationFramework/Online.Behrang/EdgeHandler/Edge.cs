﻿using System;
using System.IO;
using AutomationFramework.Util.Behrang.ConfigurationHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;

namespace AutomationFramework.Online.Behrang.EdgeHandler
{
    class Edge:IBrowserHandler
    {
        private IWebDriver _driver;
        private const int ImplicitTimeWaitInSeconds = 30;
        private const int PageLoadTimeoutInSeconds = 30;
        private EdgeOptions _options;

        public Edge SetupEdge()
        {

            _options = new EdgeOptions();
            _options.AddAdditionalCapability("window", "--start-maximized");

            var firingDriver = new EventFiringWebDriver(new EdgeDriver(_options));

            firingDriver
                .Manage()
                .Timeouts()
                .ImplicitWait = (TimeSpan.FromSeconds(ImplicitTimeWaitInSeconds));
            firingDriver
                .Manage()
                .Timeouts()
                .PageLoad = (TimeSpan.FromSeconds(PageLoadTimeoutInSeconds));
            _driver = firingDriver;
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
            var imagePath = Path.Combine(folderPath, "EdgeException-" + timestamp + ".png");
            _driver.TakeScreenshot().SaveAsFile(imagePath, ScreenshotImageFormat.Bmp);
            var filePath = Path.Combine(folderPath, "EdgeException-" + timestamp + ".txt");
            File.WriteAllText(filePath, e.ThrownException.ToString());
        }
    }
}
