using System;
using AutomationFramework.Util.Behrang.ApiHandler;
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
            var credentialBase64 = "T3stAuth1r1zat1N";
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
        [Given(@"I have ServiceUserName in Header")]
        public void GivenIHaveServiceUserNameInHeader()
        {
            if (apiHeaders == null) throw new NullReferenceException("API HEADER NOT INITIATED");
        }

        [Given(@"ServiceUserName  Header is ""(.*)"" and ServiceUserName is ""(.*)""")]
        public void GivenServiceUserNameHeaderIsAndServiceUserNameIs(string serviceUserNameHeader, string serviceUserNameValue)
        {
            apiHeaders.ServiceUserName = serviceUserNameValue;
            Assert.AreEqual(nameof(apiHeaders.ServiceUserName), serviceUserNameHeader);

        }

        [Then(@"ServiceUserName header value will be ""(.*)""")]
        public void ThenServiceUserNameHeaderValueWillBe(string serviceUserNameValue)
        {
           Assert.AreEqual(apiHeaders.ServiceUserName, serviceUserNameValue);
        }

      




    }
}

