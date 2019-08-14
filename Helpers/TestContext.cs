using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class TestContext : DbContext
    {
     

        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<Providers> Providers {get; set;}
        public DbSet<Payers> Payers {get; set;}
        public DbSet<ScheduleData> ScheduleData {get; set;}
        public DbSet<TimeReport> TimeReport {get; set;}
          public DbSet<Facilities> Facilities {get; set;}
         public DbSet<Sched> Sched {get; set;}
           public DbSet<Sched2> Sched2 {get; set;}
         public TestContext(DbContextOptions<TestContext> options)
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