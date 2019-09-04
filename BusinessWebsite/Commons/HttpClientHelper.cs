using BusinessWebsite.Commons.Interfaces;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessWebsite.Commons
{
    public class HttpClientHelper : CommonHelperBase, IHttpClientHelper
    {
        private string url { get; set; }
        private Dictionary<string, string> RequestParameters { get; set; } = new Dictionary<string, string>();


        public async Task<HttpResponseMessage> Post([NotNull]string url, Dictionary<string, string> requestPars = null)
        {
            this.url = url;

            HttpClient httpClient = new HttpClient();
            //httpClient.SetBearerToken();
            HttpResponseMessage response = await httpClient.PostAsync(this.url, new FormUrlEncodedContent(requestPars));
            return response;
        }

    }
}
