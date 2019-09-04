using IdentityServer4.Models;
using IdentityServer4.Validation;
using IdentityServerWebApp.DataModels;
using IdentityServerWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerWebApp.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

        private IUserInfoService _userInfoService;

        public ResourceOwnerPasswordValidator(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrEmpty(context.Password))
            {
                InvalidCustomerCredential(ref context);
                return;
            }

            UserInfo userInfo = this._userInfoService.GetByUserName(context.UserName);
            if (userInfo == null)
            {
                InvalidCustomerCredential(ref context);
                return;
            }

            if (userInfo.Password != context.Password)
            {
                InvalidCustomerCredential(ref context);
                return;
            }

            ValidateSuccess(ref context, context.UserName);
            return;
        }

        private void InvalidCustomerCredential(ref ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidClient,
                "invalid customer credential"
            );
        }

        private void ValidateSuccess(ref ResourceOwnerPasswordValidationContext context, string subjectId)
        {
            string sessionId = Guid.NewGuid().ToString();
            context.Result = new GrantValidationResult(
                subject: context.UserName,
                authenticationMethod: "customer",
                claims: new List<Claim>() { new Claim("SearchingCoreSessionId", sessionId) }
                );
        }
    }
}