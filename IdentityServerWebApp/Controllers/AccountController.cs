using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServerWebApp.DataModels;
using IdentityServerWebApp.Services.Interfaces;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerWebApp.Controllers
{
    public class AccountController : Controller
    {

        private IUserInfoService _userInfoService { get; set; }
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public AccountController(IUserInfoService userInfoService,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
        IAuthenticationSchemeProvider schemeProvider,
        IEventService events)
        {
            this._userInfoService = userInfoService;
            this._interaction = interaction;
            this._clientStore = clientStore;
            this._schemeProvider = schemeProvider;
            this._events = events;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([NotNull]string userName, [NotNull]string password, string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            UserInfo userInfo = this._userInfoService.GetByUserName(userName);
            if (userInfo == null || userInfo.Password != password)
                return View();

            Microsoft.AspNetCore.Authentication.AuthenticationProperties props = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(1))
            };
            await base.HttpContext.SignInAsync(new IdentityServerUser(userInfo.Id.ToString()) { DisplayName = userInfo.UserName, AuthenticationTime = DateTime.Now }, props);
            return Redirect(returnUrl);
        }


        public async Task<IActionResult> Logout(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);
            await AuthenticationHttpContextExtensions.SignOutAsync(Request.HttpContext);
            if (!string.IsNullOrEmpty(logout.PostLogoutRedirectUri))
            {
                return Redirect(logout.PostLogoutRedirectUri);
            }
            var refererUrl = Request.Headers["Referer"].ToString();
            return Redirect(refererUrl);
        }
    }
}