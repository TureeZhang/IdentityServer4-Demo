using IdentityServerWebApp.DataModels;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWebApp.Repository
{
    public class MyDbContext : DbContext
    {
        public DbSet<UserInfo> UserInfoes { get; set; }

        public MyDbContext()
        {
        }

        public MyDbContext([NotNull]DbContextOptions options) : base(options)
        {
        }
    }
}
