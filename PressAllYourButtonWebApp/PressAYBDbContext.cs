using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PressAllYourButtonWebApp.Models;

namespace PressAllYourButtonWebApp
{
    public class PressAYBDbContext:DbContext
    {
        public DbSet<ConnectionAudit> ConnectionAudits { get; set; } = null!;
        public DbSet<UserInfo> UserInfos { get; set; } = null!;
        public DbSet<Device> Devices { get; set; } = null!;

        public DbSet<DeviceType> DeviceType { get; set; } = null!;

        public PressAYBDbContext(DbContextOptions<PressAYBDbContext> options)
            :base(options)
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ConnectionAudit>().
                Property(c => c.ActionTime).
                HasColumnType("DateTime");

            modelbuilder.Entity<UserInfo>().
                Property(u => u.UserName)
                .HasMaxLength(20);

            modelbuilder.Entity<UserInfo>().
                Property(u => u.Password)
                .HasMaxLength(20);

            modelbuilder.Entity<UserInfo>().
                Property(u => u.Email)
                .HasMaxLength(50);

            modelbuilder.Entity<Device>()
                .HasOne(d => d.userinfo)
                .WithMany(u => u.devices)
                .IsRequired()
                .HasForeignKey(d => d.Belong_User);

            modelbuilder.Entity<Device>()
                .HasOne(d => d.deviceType)
                .WithMany()
                .HasForeignKey(d => d.DeviceType_id);

        }

    }
}
