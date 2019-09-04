using IdentityServerWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWebApp.Services.Interfaces
{
    public interface IUserInfoService
    {
        UserInfo GetByUserName(string userName);
    }
}
