using System;
using System.IO;
using AutomationFramework.Util.Behrang.ConfigurationHelper;
using AutomationFramework.Util.Behrang.FileHandler;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;

namespace AutomationFramework.Online.Behrang.ChromeHandler
{
    public class Chrome
    {
        private IWebDriver _driver;
        private const int ImplicitTimeWaitInSeconds = 30;
        private const int PageLoadTimeoutInSeconds = 30;
        private ChromeOptions _options;
        /// <summary>
        ///  Array of string for chrome Options
        /// </summary>
        private readonly string[] _chromeOptions = 
        {
            "--start-maximized",
            "--ignore-certificate-errors",
            "--disable-popup-blocking",
           // "--incognito"
        };
        /// <summary>
        /// Setup Chrome with options
        /// </summary>
        /// <returns>this class instance</returns>
        public Chrome SetupChromeWithOption()
        {
                foreach (var opt in _chromeOptions)
                {
                    _options.AddArguments(opt);
                }
            var firingDriver = new EventFiringWebDriver(new ChromeDriver(_options));
            firingDriver.ExceptionThrown += firingDriver_TakeScreenshotOnException;

            firingDriver
                .Manage()
                .Timeouts()
                .ImplicitWait=(TimeSpan.FromSeconds(ImplicitTimeWaitInSeconds));
            firingDriver
                .Manage()
                .Timeouts()
                .PageLoad=(TimeSpan.FromSeconds(PageLoadTimeoutInSeconds));
            _driver = firingDriver;
            return this;
        }

        /// <summary>
        /// Handling exceptions occure in the webbrowser and 
        /// takes the screenshot and write the logs
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event exception</param>
        private void firingDriver_TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
           
            var conf = new AppConfigHandler();
            var folderPath = conf.ReadFolderPathFromConfigurationFile(SolutionSubFolder.Logs);
            var imagePath = Path.Combine(folderPath, "ChromeException-" + timestamp + ".png");
            _driver.TakeScreenshot().SaveAsFile(imagePath, ScreenshotImageFormat.Bmp);
            var filePath = Path.Combine(folderPath, "ChromeException-" + timestamp + ".txt");
            File.WriteAllText(filePath,e.ThrownException.ToString());
        }
        /// <summary>
        /// Set Proxy
        ///  i.e. "myhttpproxy:3337"
        /// </summary>
        /// <returns>chrome instance</returns>
        public Chrome SetUpProxy(string proxyStr)
        {
            ChromeOptions options = new ChromeOptions();
            var proxy = new Proxy();
            proxy.Kind = ProxyKind.Manual;
            proxy.IsAutoDetect = false;
            proxy.HttpProxy = proxyStr;
            proxy.SslProxy = proxyStr;
            options.Proxy = proxy;
            return this;
        }
        /// <summary>
        /// Set Proxy
        ///  i.e. "--proxy-server=http://user:password@yourProxyServer.com:8080" with credntial
        /// </summary>
        /// <returns>chrome instance</returns>
        public Chrome SetupProxyWithCredentials(string proxyString)
        {
            _options.AddArguments(proxyString);
            return this;
        }

        public Chrome SetExtention()
        {
            var dir = FileAndFolderHandler.GetSolutionDirectory();
            var extensionPath = Path.Combine(dir, "Resources", "ChromeExtensions");
            var files = Directory.GetFiles(extensionPath);
            foreach (var extension in files)
            {
                var extPath = Path.Combine(extensionPath, extension);
                if(_options==null) _options=new ChromeOptions();
                _options.AddExtension(extPath);
            }
            return this;
        }
        public IWebDriver GetDriverInstance()
        {
            return _driver;
        }

    }
}
