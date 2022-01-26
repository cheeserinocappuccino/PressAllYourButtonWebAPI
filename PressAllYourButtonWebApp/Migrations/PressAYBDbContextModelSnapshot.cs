﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PressAllYourButtonWebApp;

#nullable disable

namespace PressAllYourButtonWebApp.Migrations
{
    [DbContext(typeof(PressAYBDbContext))]
    partial class PressAYBDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PressAllYourButtonWebApp.Models.ConnectionAudit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("ActionTime")
                        .HasColumnType("DateTime");

                    b.Property<byte>("ActionType")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("ConnectionAudits");
                });
#pragma warning restore 612, 618
        }
    }
}
