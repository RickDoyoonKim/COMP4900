﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ImpactWebsite.Data;
using ImpactWebsite.Models.OrderModels;

namespace ImpactWebsite.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170516182318_week4")]
    partial class week4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImpactWebsite.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(160);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(160);

                    b.Property<bool>("IsTempUser");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(160);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<bool>("NewsletterRequired");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("ImpactWebsite.Models.Investment", b =>
                {
                    b.Property<long>("InvestmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("EstimateValue");

                    b.Property<string>("ISIN")
                        .HasMaxLength(160);

                    b.Property<string>("InvestmentName")
                        .IsRequired()
                        .HasMaxLength(160);

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("ShareNumber");

                    b.HasKey("InvestmentId");

                    b.ToTable("Investment");
                });

            modelBuilder.Entity("ImpactWebsite.Models.NewsLetterUser", b =>
                {
                    b.Property<long>("NewsLetterUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<bool>("isSubscribed");

                    b.HasKey("NewsLetterUserId");

                    b.ToTable("NewsLetterUser");
                });

            modelBuilder.Entity("ImpactWebsite.Models.OrderModels.OrderHeader", b =>
                {
                    b.Property<int>("OrderHeaderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DeliveredDate");

                    b.Property<string>("NoteFromAdmin");

                    b.Property<string>("NoteFromUser");

                    b.Property<int>("OrderNum");

                    b.Property<int>("OrderStatus");

                    b.Property<DateTime>("OrderedDate");

                    b.Property<string>("SalesRep")
                        .HasMaxLength(160);

                    b.Property<int>("TotalAmount");

                    b.Property<string>("UserEmail")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("OrderHeaderId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("OrderHeader");
                });

            modelBuilder.Entity("ImpactWebsite.Models.OrderModels.OrderLine", b =>
                {
                    b.Property<int>("OrderLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("ModuleId");

                    b.Property<string>("ModuleName");

                    b.Property<int>("OrderHeaderId");

                    b.HasKey("OrderLineId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("OrderHeaderId");

                    b.ToTable("OrderLine");
                });

            modelBuilder.Entity("ImpactWebsite.Models.OrderModels.OrderModule", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeliveryDays");

                    b.Property<string>("Description");

                    b.Property<string>("LongDescription");

                    b.Property<string>("ModuleName");

                    b.Property<int>("UnitPriceId");

                    b.HasKey("ModuleId");

                    b.HasIndex("UnitPriceId");

                    b.ToTable("OrderModule");
                });

            modelBuilder.Entity("ImpactWebsite.Models.OrderModels.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<string>("Description");

                    b.Property<decimal>("DiscountRate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("PromotionName")
                        .HasMaxLength(160);

                    b.HasKey("PromotionId");

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("ImpactWebsite.Models.OrderModels.UnitPrice", b =>
                {
                    b.Property<int>("UnitPriceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateEffectFrom");

                    b.Property<DateTime>("DateEffectTo");

                    b.Property<int>("Price");

                    b.HasKey("UnitPriceId");

                    b.ToTable("UnitPrice");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityRoleClaim<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserClaim<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("IdentityUserLogin<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("IdentityUserRole<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("IdentityUserToken<string>");
                });

            modelBuilder.Entity("ImpactWebsite.Models.OrderModels.OrderHeader", b =>
                {
                    b.HasOne("ImpactWebsite.Models.ApplicationUser")
                        .WithMany("Orders")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("ImpactWebsite.Models.OrderModels.OrderLine", b =>
                {
                    b.HasOne("ImpactWebsite.Models.OrderModels.OrderModule", "Module")
                        .WithMany("OrderLines")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ImpactWebsite.Models.OrderModels.OrderHeader", "OrderHeader")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ImpactWebsite.Models.OrderModels.OrderModule", b =>
                {
                    b.HasOne("ImpactWebsite.Models.OrderModels.UnitPrice", "UnitPrice")
                        .WithMany()
                        .HasForeignKey("UnitPriceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ImpactWebsite.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ImpactWebsite.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ImpactWebsite.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
