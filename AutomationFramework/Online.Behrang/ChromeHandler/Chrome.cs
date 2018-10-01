using System;
using System.Drawing.Imaging;
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
        private const int ImplicitTimeWaitInSeconds = 3;
        private const int PageLoadTimeoutInSeconds = 3;
        private ChromeOptions _options;

        private readonly string[] _chromeOptions = 
        {
            "--start-maximized",
            "--ignore-certificate-errors",
            "--disable-popup-blocking",
           // "--incognito"
        };
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
                .ImplicitlyWait(TimeSpan.FromSeconds(ImplicitTimeWaitInSeconds));
            firingDriver
                .Manage()
                .Timeouts()
                .SetPageLoadTimeout(TimeSpan.FromSeconds(PageLoadTimeoutInSeconds));
            _driver = firingDriver;
            return this;
        }

        private void firingDriver_TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
           
            var conf = new AppConfigHandler();
            var folderPath = conf.ReadFolderPathFromConfigurationFile(SolutionSubFolder.Logs);
            var imagePath = Path.Combine(folderPath, "Exception-" + timestamp + ".png");
            _driver.TakeScreenshot().SaveAsFile(imagePath, ImageFormat.Png);
            var filePath = Path.Combine(folderPath, "Exception-" + timestamp + ".txt");
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
