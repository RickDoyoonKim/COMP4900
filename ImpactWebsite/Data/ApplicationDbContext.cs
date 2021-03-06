﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ImpactWebsite.Models;
using ImpactWebsite.Models.OrderModels;
using ImpactWebsite.Models.BillingModels;

namespace ImpactWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.RemovePluralizingTableNameConvention();
        }

        public DbSet<Investment> Investments { get; set; }
        public DbSet<OrderModule> Modules { get; set; }
        public DbSet<UnitPrice> UnitPrices { get; set; }
        public DbSet<NewsLetterUser> NewsLetterUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ImpactWebsite.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<BillingAddress> BillingAddresses { get; set; }        
    }
}
