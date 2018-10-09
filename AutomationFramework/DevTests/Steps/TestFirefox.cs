using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AutomationFramework.DevTests.Steps
{
    [TestFixture()]
    class TestFirefox
    {
        [TestCase()]
        public void TestFirefoxwithSecurityprofile()
        {
          var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://bing.com");
            driver.FindElement(By.Name("q")).SendKeys("David");
            

        }
    }
}
