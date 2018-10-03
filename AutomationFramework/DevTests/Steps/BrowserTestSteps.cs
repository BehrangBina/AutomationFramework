using System;
using System.Linq;
using AutomationFramework.Online.Behrang;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AutomationFramework.DevTests.Steps
{
    [Binding]
    public class BrowserTestSteps
    {
        private Browser _browser;
        private IWebDriver _driver;

        [Given(@"I Have an instance of the browser")]
        public void GivenIHaveAnInstanceOfTheBrowser()
        {
            _browser = new Browser();

        }

        [Then(@"I Can lunch the browser with a url")]
        public void ThenICanLunchTheBrowserWithAUrl(Table table)
        {
            var rows = table.Rows;
            foreach (var row in rows)
            {
                var browserName = row.Values.ElementAt(0);
                var urlValue = row.Values.ElementAt(1);
                if (browserName == SupportedBrowsers.Chrome.ToString())
                {
                    _driver = _browser?.SetBrowserType(SupportedBrowsers.Chrome).Driver;
                }
                if (browserName == SupportedBrowsers.Edge.ToString())
                {
                    _driver = _browser?.SetBrowserType(SupportedBrowsers.Edge).Driver;
                }
                if (browserName == SupportedBrowsers.Firefox.ToString())
                {
                    _driver = _browser?.SetBrowserType(SupportedBrowsers.Firefox).Driver;
                }
                var uri = new Uri(urlValue);

                _driver?.Navigate().GoToUrl(uri);
                Console.WriteLine($"Browser: {browserName} | URL: {urlValue} | Title: {_driver?.Title}");
                _driver?.Dispose();
            }




        }



    }
}





















