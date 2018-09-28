using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutomationFramework.Behrang.Util.ApiHandler;

namespace AutomationFramework.Util.Behrang.ApiHandler
{
    public class WebApi
    {
        public async Task<string> GetStringFromApiCall(ApiContentType contentType, string url, ApiHeaders apiHeader)
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



            return null;
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
