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
    [Migration("20220129024040_AddPasswordColumnTobyteArrayInUserInfos")]
    partial class AddPasswordColumnTobyteArrayInUserInfos
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
                        .HasColumnType("DateTime");

                    b.Property<byte>("ActionType")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("ConnectionAudits");
                });

            modelBuilder.Entity("PressAllYourButtonWebApp.Models.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Belong_User")
                        .HasColumnType("int");

                    b.Property<int?>("DeviceType_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Manufacture_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("NicknameByUser")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Belong_User");

                    b.HasIndex("DeviceType_id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("PressAllYourButtonWebApp.Models.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeviceType");
                });

            modelBuilder.Entity("PressAllYourButtonWebApp.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varbinary(32)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("PressAllYourButtonWebApp.Models.Device", b =>
                {
                    b.HasOne("PressAllYourButtonWebApp.Models.UserInfo", "userinfo")
                        .WithMany("devices")
                        .HasForeignKey("Belong_User");

                    b.HasOne("PressAllYourButtonWebApp.Models.DeviceType", "deviceType")
                        .WithMany()
                        .HasForeignKey("DeviceType_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("deviceType");

                    b.Navigation("userinfo");
                });

            modelBuilder.Entity("PressAllYourButtonWebApp.Models.UserInfo", b =>
                {
                    b.Navigation("devices");
                });
#pragma warning restore 612, 618
        }
    }
}