using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AutomationFramework.Behrang.Util.ApiHandler
{
    public class WebApi
    {
        public async Task<string> GetStringFromApiCall(ApiContentType contentType, string url ,ApiHeaders[] ApiHeader)
        {
            try
            {
                Uri uri = new Uri(url);
                string contenttype = GetContentType(contentType);
                var httpClient = new HttpClient { BaseAddress = uri };
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contenttype));
  
                foreach (var apiHeader in ApiHeader)
                {
                    if(apiHeader.Authorization!=null)  httpClient.DefaultRequestHeaders.Add("Authorization",apiHeader.Authorization);
                }
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
