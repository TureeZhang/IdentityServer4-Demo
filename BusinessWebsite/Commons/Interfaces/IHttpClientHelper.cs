using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusinessWebsite.Commons.Interfaces
{
    public interface IHttpClientHelper
    {
        Task<HttpResponseMessage> Post([NotNull]string url, Dictionary<string, string> requestPars = null);
    }
}
