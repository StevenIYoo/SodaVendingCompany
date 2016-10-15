using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StevenYoo_SodaVendingCompany.Models
{
    public class UserContext : IdentityDbContext<User>
    {
        private IConfigurationRoot _config;

        public UserContext(IConfigurationRoot config, DbContextOptions options)
                :base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["Data:VendingCompanyConnection"]);
        }

    }
}
