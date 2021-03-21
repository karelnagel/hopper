﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Hopper.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    internal class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Hopper.Models.ApplicationUser", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("int");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("Email")
                    .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                    .HasMaxLength(256);

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("tinyint(1)");

                b.Property<string>("FirstName")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<int>("Language")
                    .HasColumnType("int");

                b.Property<string>("LastName")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("tinyint(1)");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("datetime(6)");

                b.Property<string>("NormalizedEmail")
                    .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                    .HasMaxLength(256);

                b.Property<string>("NormalizedUserName")
                    .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                    .HasMaxLength(256);

                b.Property<string>("PasswordHash")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("tinyint(1)");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("tinyint(1)");

                b.Property<string>("UserName")
                    .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                    .HasMaxLength(256);

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasName("UserNameIndex");

                b.ToTable("AspNetUsers");
            });

            modelBuilder.Entity("Hopper.Models.Favorite", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.Property<string>("ApplicationUserId")
                    .IsRequired()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.Property<string>("SoundId")
                    .IsRequired()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.HasKey("Id");

                b.HasIndex("ApplicationUserId");

                b.HasIndex("SoundId");

                b.ToTable("Favorite");
            });

            modelBuilder.Entity("Hopper.Models.Role", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("Name")
                    .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                    .HasMaxLength(256);

                b.Property<string>("NormalizedName")
                    .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                    .HasMaxLength(256);

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasName("RoleNameIndex");

                b.ToTable("AspNetRoles");
            });

            modelBuilder.Entity("Hopper.Models.Sound", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.Property<string>("Address")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("ApplicationUserId")
                    .IsRequired()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.Property<string>("Author")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<int>("Language")
                    .HasColumnType("int");

                b.Property<string>("Title")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("Video")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.HasKey("Id");

                b.HasIndex("ApplicationUserId");

                b.ToTable("Sounds");
            });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
            {
                b.Property<string>("UserCode")
                    .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                    .HasMaxLength(200);

                b.Property<string>("ClientId")
                    .IsRequired()
                    .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                    .HasMaxLength(200);

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime(6)");

                b.Property<string>("Data")
                    .IsRequired()
                    .HasColumnType("longtext CHARACTER SET utf8mb4")
                    .HasMaxLength(50000);

                b.Property<string>("DeviceCode")
                    .IsRequired()
                    .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                    .HasMaxLength(200);

                b.Property<DateTime?>("Expiration")
                    .IsRequired()
                    .HasColumnType("datetime(6)");

                b.Property<string>("SubjectId")
                    .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                    .HasMaxLength(200);

                b.HasKey("UserCode");

                b.HasIndex("DeviceCode")
                    .IsUnique();

                b.HasIndex("Expiration");

                b.ToTable("DeviceCodes");
            });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
            {
                b.Property<string>("Key")
                    .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                    .HasMaxLength(200);

                b.Property<string>("ClientId")
                    .IsRequired()
                    .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                    .HasMaxLength(200);

                b.Property<DateTime>("CreationTime")
                    .HasColumnType("datetime(6)");

                b.Property<string>("Data")
                    .IsRequired()
                    .HasColumnType("longtext CHARACTER SET utf8mb4")
                    .HasMaxLength(50000);

                b.Property<DateTime?>("Expiration")
                    .HasColumnType("datetime(6)");

                b.Property<string>("SubjectId")
                    .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                    .HasMaxLength(200);

                b.Property<string>("Type")
                    .IsRequired()
                    .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                    .HasMaxLength(50);

                b.HasKey("Key");

                b.HasIndex("Expiration");

                b.HasIndex("SubjectId", "ClientId", "Type");

                b.ToTable("PersistedGrants");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("ClaimType")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("ClaimValue")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("ClaimType")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("ClaimValue")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                b.Property<string>("ProviderKey")
                    .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.Property<string>("RoleId")
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("varchar(36) CHARACTER SET utf8mb4");

                b.Property<string>("LoginProvider")
                    .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                b.Property<string>("Name")
                    .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                b.Property<string>("Value")
                    .HasColumnType("longtext CHARACTER SET utf8mb4");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens");
            });

            modelBuilder.Entity("Hopper.Models.Favorite", b =>
            {
                b.HasOne("Hopper.Models.ApplicationUser", "ApplicationUser")
                    .WithMany("Favorites")
                    .HasForeignKey("ApplicationUserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Hopper.Models.Sound", "Sound")
                    .WithMany("Favorites")
                    .HasForeignKey("SoundId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Hopper.Models.Sound", b =>
            {
                b.HasOne("Hopper.Models.ApplicationUser", null)
                    .WithMany("CreatedSounds")
                    .HasForeignKey("ApplicationUserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
            {
                b.HasOne("Hopper.Models.Role", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
            {
                b.HasOne("Hopper.Models.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
            {
                b.HasOne("Hopper.Models.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
            {
                b.HasOne("Hopper.Models.Role", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Hopper.Models.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
            {
                b.HasOne("Hopper.Models.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}