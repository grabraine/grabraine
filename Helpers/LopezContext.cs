using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class LopezContext : DbContext
    {
     

        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<Providers> Providers {get; set;}
        public DbSet<Payers> Payers {get; set;}
         public DbSet<Sched> Sched {get; set;}
         public LopezContext(DbContextOptions<LopezContext> options)
            : base(options)
        {
            
        }
          protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
      
    }
}