﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer
{
    public class AppDbContext : IdentityDbContext
    {
        //  Properties
        //  ==========

        public DbSet<Issue> Issues { get; set; } = null!;
        public DbSet<House> Houses { get; set; } = null!;

        //  Constructors
        //  ============

        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {

        }

        //  Methods
        //  =======

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Entity<House>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<House>()
                .HasMany(h => h.Issues)
                .WithOne(i => i.House);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = Abstractions.Roles.Tenant,
                    NormalizedName = Abstractions.Roles.Tenant.ToUpperInvariant()
                },
                new IdentityRole
                {
                    Name = Abstractions.Roles.Landlord,
                    NormalizedName = Abstractions.Roles.Landlord.ToUpperInvariant()
                });
        }
    }
}