using System;
using System.Linq;
using AutomationFramework.Online.Behrang;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace AutomationFramework.DevTests.Steps
{
    [Binding]
    public class BrowserTestSteps
    {
        private Browser _browser;
        private IWebDriver _driver = null;

        [Given(@"I Have an instance of the browser")]
        public void GivenIHaveAnInstanceOfTheBrowser()
        {
            _browser = new Browser();

        }

        [Then(@"I Can lunch the browser with a url")]
        public void ThenICanLunchTheBrowserWithAUrl(Table table)
        {
            var rows = table.Rows;
            var browserName =string.Empty;
            var urlValue = string.Empty;
            foreach (var row in rows)
            {
                 browserName = row.Values.ElementAt(0);
                 urlValue = row.Values.ElementAt(1);
                if (browserName == SupportedBrowsers.Chrome.ToString())
                {
                    _driver = _browser.SetBrowserType(SupportedBrowsers.Chrome).Driver;
                }
                if (browserName == SupportedBrowsers.Edge.ToString())
                {
                    _driver = _browser.SetBrowserType(SupportedBrowsers.Edge).Driver;
                }
                _driver.Navigate().GoToUrl(urlValue);
                _browser?.Dispose();
            }
            Console.WriteLine($"Browser: {browserName} | URL: {urlValue} | Title: {_driver.Title}");

             
           
        }


   
    }
}
