using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4.ResponseHandling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RPCOnlineWebAPI.Controllers
{
    [Authorize]
    public class RpcController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Plus(string a, string b)
        {
            List<Claim> userClaims = GetUserClaims();

            if (userClaims != null && userClaims.Count > 0)
            {
                //handle userinfo with something goes here...
            }

            //再执行业务代码调用
            int num1 = Convert.ToInt32(a);
            int num2 = Convert.ToInt32(b);

            return Json(new { Result = num1 + num2 });
        }

        [NonAction]
        private List<Claim> GetUserClaims()
        {
            var claims = base.HttpContext.User.Claims.ToList();
            return claims;
        }
    }
}