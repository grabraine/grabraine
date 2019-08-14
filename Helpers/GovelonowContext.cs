using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class GovelonowContext : DbContext
    {
     

       public DbSet<Inquiry> Inquiry {get; set;}
         public GovelonowContext(DbContextOptions<GovelonowContext> options)
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