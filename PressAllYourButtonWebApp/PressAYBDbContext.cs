using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PressAllYourButtonWebApp.Models;

namespace PressAllYourButtonWebApp
{
    public class PressAYBDbContext:DbContext
    {
        public DbSet<ConnectionAudit> ConnectionAudits { get; set; } = null!;

        public PressAYBDbContext(DbContextOptions<PressAYBDbContext> options)
            :base(options)
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ConnectionAudit>().
                Property(c => c.ActionTime).
                HasColumnType("DateTime");
        }

    }
}
