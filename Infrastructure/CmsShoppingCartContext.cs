using SmartCityApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityApp.Infrastructure
{
    public class SmartCityAppContext : IdentityDbContext<AppUser>
    {
        public SmartCityAppContext(DbContextOptions<SmartCityAppContext> options)
            :base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}