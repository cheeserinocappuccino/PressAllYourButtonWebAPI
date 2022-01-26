﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PressAllYourButtonWebApp;

#nullable disable

namespace PressAllYourButtonWebApp.Migrations
{
    [DbContext(typeof(PressAYBDbContext))]
    [Migration("20220126051555_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("datetime2");

                    b.Property<byte>("ActionType")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("ConnectionAudits");
                });
#pragma warning restore 612, 618
        }
    }
}
