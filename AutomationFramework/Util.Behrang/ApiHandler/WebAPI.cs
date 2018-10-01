using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutomationFramework.Util.Behrang.ApiHandler
{
    public class WebApi
    {
        public async Task<string> PostApiCallAndReturnResult(ApiContentType contentType, string url, ApiHeaders apiHeader,string postData,string jsonNodeKey)
        {
            try
            {
                Uri uri = new Uri(url);
                string contenttype = GetContentType(contentType);
                var httpClient = new HttpClient { BaseAddress = uri };
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contenttype));
                if (apiHeader.Authorization != null) httpClient.DefaultRequestHeaders.Add(nameof(apiHeader.Authorization), apiHeader.Authorization);
                if(apiHeader.ServicePassword!=null) httpClient.DefaultRequestHeaders.Add(nameof(apiHeader.ServicePassword),apiHeader.ServicePassword);
                if(apiHeader.ServiceUserName!=null) httpClient.DefaultRequestHeaders.Add(nameof(apiHeader.ServiceUserName),apiHeader.ServiceUserName);

                
                HttpContent bodyContent = new StringContent(postData);
                bodyContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = httpClient.PostAsync(uri, bodyContent).Result;
                Console.Out.WriteLine($"Debaug GetStringFromApiCall Reason: {response.ReasonPhrase} Request: {response.RequestMessage}");
                var content = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(content);
                Console.WriteLine($"Data: \r\n{data}");
                string valueOfJson = data[jsonNodeKey].Value<string>();
                return valueOfJson;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        private string GetContentType(ApiContentType contentType)
        {
            string type = string.Empty;

            switch (contentType)
            {

                case (ApiContentType.Json):
                    type = "application/json";
                    break;
                case (ApiContentType.Xml):
                    type = "application/xml";
                    break;
                case (ApiContentType.Html):
                case (ApiContentType.Text):
                    type = "text/html";
                    break;
            }
            return type;
        }
    }
}
