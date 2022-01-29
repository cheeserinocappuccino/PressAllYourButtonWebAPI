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

            // For AES encrypted output, 1 character = 1 byte output
            // a maximum 31 char password needs 32byte
            // a maximum 32 char password needs 48byte
            // ( it append 16 byte at once each time input reaches limit)
            // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-6.0
            modelbuilder.Entity<UserInfo>().
                Property(u => u.Password)
                .HasColumnType("varbinary")
                .HasMaxLength(32);

            modelbuilder.Entity<UserInfo>()
                .Property(u => u.Iv)
                .HasColumnType("varbinary")
                .HasMaxLength(16);
                

            modelbuilder.Entity<UserInfo>().
                Property(u => u.Email)
                .HasMaxLength(50);

            modelbuilder.Entity<Device>()
                .HasOne(d => d.userinfo)
                .WithMany(u => u.devices)
                .HasForeignKey(d => d.Belong_User);

            modelbuilder.Entity<Device>()
                .HasOne(d => d.deviceType)
                .WithMany()
                .IsRequired()
                .HasForeignKey(d => d.DeviceType_id);

            modelbuilder.Entity<Device>()
                .Property(d => d.NicknameByUser)
                .HasMaxLength(30);

        }

    }
}
