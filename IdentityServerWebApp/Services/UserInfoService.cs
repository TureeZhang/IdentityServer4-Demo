using IdentityServerWebApp.DataModels;
using IdentityServerWebApp.Repository;
using IdentityServerWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWebApp.Services
{
    public class UserInfoService : IUserInfoService
    {
        private MyDbContext _db;

        public UserInfoService(MyDbContext db)
        {
            _db = db;
        }

        public UserInfo GetByUserName(string userName)
        {
            return this._db.UserInfoes.Where(user => user.UserName == userName).SingleOrDefault();
        }
    }
}
