using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerWebApp
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customerProfile = new IdentityResource(
                name: "customer.profile",
                displayName: "Customer Profile",
                claimTypes: new[] { "RealName", "Roles", "DefaultStartPageId", "Status", "SearchingCoreSessionId" }
                );

            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                customerProfile
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes ={"api1" }
                }
            };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser> {
                new TestUser
                {
                     SubjectId="1",
                     Username = "admin",
                     Password = "123456"
                }
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","远程独立 API 服务器"){
                    UserClaims = {
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Subject,
                        "RealName", "Roles", "DefaultStartPageId", "Status","SearchingCoreSessionId"
                    }
                }
            };
        }



    }
}
