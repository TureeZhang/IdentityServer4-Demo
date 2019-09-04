using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using BusinessWebsite.Commons.Interfaces;
using System.Net.Http;
using IdentityModel.Client;
using IdentityServer4.Models;

namespace BusinessWebsite.Controllers
{
    public class HomeController : Controller
    {
        private IHttpClientHelper _httpClientHelper;
        public HomeController(IHttpClientHelper httpClientHelper)
        {
            _httpClientHelper = httpClientHelper;
        }

        public async Task<IActionResult> IndexAsync()
        {


            HttpClient client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint ?? "http://localhost:5000/connect/token",
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "admin",
                Password = "123456",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            ViewData["api1Token"] = tokenResponse.AccessToken;

            #region 使用 Client 向受保护资源发起请求（现在已从前端进行 ajax 直接调用，Client 服务器无需介入）
            //向受保护资源发起请求
            //HttpClient rpcClient = new HttpClient();
            //rpcClient.SetBearerToken(tokenResponse.AccessToken);

            //Dictionary<string, string> pars = new Dictionary<string, string>();
            //pars.Add("a", "5");
            //pars.Add("b", "6");
            //HttpResponseMessage httpResponse = await rpcClient.PostAsync("http://localhost:5001/rpc/plus", new FormUrlEncodedContent(pars));

            //Console.WriteLine(httpResponse.Content.ReadAsStringAsync().Result);
            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> RemoteApiServerCallAsync()
        {
            Dictionary<string, string> pars = new Dictionary<string, string>();
            pars.Add("a", "5");
            pars.Add("a", "6");

            HttpResponseMessage httpResponse = await this._httpClientHelper.Post("http://localhost:5004/Rpc/Plus", pars);
            string responseStr = await httpResponse.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseStr))
                ViewData["RpcResponse"] = string.Empty;

            return View("/Index");
        }
    }
}
