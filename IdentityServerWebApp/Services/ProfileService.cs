using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServerWebApp.DataModels;
using IdentityServerWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerWebApp.Services
{
    public class ProfileService : IProfileService
    {

        private IUserInfoService _userInfoService;

        public ProfileService(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            UserInfo userInfo = null;
            if (!string.IsNullOrEmpty(context.Subject.Identity.Name) || !string.IsNullOrEmpty(context.Subject.Claims.ToList().Find(c => c.Type == "sub").Value))
            {
                userInfo = this._userInfoService.GetByUserName(context.Subject.Identity.Name ?? context.Subject.Claims.ToList().Find(c => c.Type == "sub").Value);
                if (userInfo == null)
                    return;
            }

            // ----------- ↓↓↓↓ 从其他位置获取 ----------- ↓↓↓↓
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("id", context.Subject.Identity.Name ?? context.Subject.Claims.ToList().Find(c => c.Type == "sub").Value));
            claims.Add(new Claim("RealName", "安德玛"));
            claims.Add(new Claim("Roles", "administrator,developer"));
            claims.Add(new Claim("DefaultStartPageId", "ProjectManagePage"));
            claims.Add(new Claim("Status", "Normal"));
            claims.Add(new Claim("SearchingCoreSessionId", context.Subject.Claims.ToList().Find(c => c.Type == "SearchingCoreSessionId").Value));
            // ----------- ↑↑↑↑ 从其他位置获取 ----------- ↑↑↑↑

            context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();    //RequestedClaimTypes 取决于访问来源。源自 IdentityReousrce 则取决于 IdentityResource，来自于 ApiResouce 则取决于 New ApiResouce 时赋予的 UserClaims
            return;
        }



        public async Task IsActiveAsync(IsActiveContext context)
        {
            return;
        }
    }
}

