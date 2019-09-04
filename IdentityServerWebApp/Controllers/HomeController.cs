using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServerWebApp.Models;
using IdentityServerWebApp.Services.Interfaces;
using JetBrains.Annotations;
using IdentityServerWebApp.DataModels;
using Microsoft.AspNetCore.Http.Authentication;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServerWebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
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
    }
}
