using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace AutomationFramework.DevTests.Steps
{
    [TestFixture()]
    class TestFirefox
    {
        [TestCase()]
        public void TestFirefoxwithSecurityprofile()
        {
           // var location1 = @"C:\Users\ben\AppData\Local\Mozilla\Firefox\Profiles\l7wny2h5.default";
          //  var locaoton2 = @"C:\Users\ben\AppData\Local\Mozilla\Firefox\Profiles\lty7m019.Automation";
            var ffProfile = new FirefoxProfile();
            ffProfile.SetPreference("acceptInsecureCerts", true);
            ffProfile.SetPreference("security.enterprise_roots.enabled",true);
        /*    ffProfile.AcceptUntrustedCertificates = true;
            
            ffProfile.AssumeUntrustedCertificateIssuer = false;*/

            var firefpoxOptions = new FirefoxOptions {Profile = ffProfile};
            var driver = new FirefoxDriver(firefpoxOptions);
            driver.Navigate().GoToUrl("https://www.google.com");

        }
    }
}
