using System;
using System.Threading;
using AutomationFramework.Online.Behrang.ChromeHandler;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using Protractor;

namespace AutomationFramework.Online.Behrang
{
    public class Browser:IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Browser));
        public IWebDriver Driver;
        public NgWebDriver NgDriver;

        /// <summary>
        /// Constructor initializing the driver, timeout, angular driver
        /// </summary>
        public Browser()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        public Browser SetBrowserType(SupportedBrowsers browser) 
        {
            switch (browser)
            {
                case (SupportedBrowsers.Chrome):
                    var chrome = new Chrome();
                   // var chromOptions = new []{ "--start-maximized" };
                    Driver = chrome
                        .SetExtention()
                        .SetupChromeWithOption()
                        .GetDriverInstance();
                    
                   
                    break;
                case (SupportedBrowsers.Edge):
                    Driver= new EdgeDriver();

                    break;
                    
            }
            NgDriver = new NgWebDriver(Driver);
            return this;
        }

 



        /// <summary>
        /// Close the browser 
        /// </summary>
        public void Dispose()
        {
            Thread.Sleep(1000);
            NgDriver?.Quit();
            Driver?.Quit();
        }
    }
}
