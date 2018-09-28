using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Behrang.Util.ApiHandler;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AutomationFramework.DevTests.Steps
{
    [Binding]
    public  class WebApiStepDef
    {
        private ApiHeaders apiHeaders;
        [Before]
        public void Init()
        {
            apiHeaders = new ApiHeaders();
        }
        [Given(@"I have Authorization Header")]
        public void GivenIHaveAuthorizationHeader()
        {
            var credentialBase64 = "T00uT25ib2FyZGluZzpvbmJvYXJkaW5n";
           apiHeaders.Authorization= $"Basic {credentialBase64}";
        }
        [Then(@"Autorization Header is ""(.*)""")]
        public void ThenAutorizationHeaderIs(string authtizationHeaderName)
        {
            Assert.AreEqual(nameof(apiHeaders.Authorization), authtizationHeaderName);

        }

        [Then(@"Authorization Header is ""(.*)""")]
        public void ThenAuthorizationHeaderIs(string authString)
        {
           Assert.AreEqual(apiHeaders.Authorization,authString);
           Console.WriteLine(apiHeaders.Authorization);
        }

    }
}

