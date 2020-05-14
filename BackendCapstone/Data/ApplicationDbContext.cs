using System;
using System.Collections.Generic;
using System.Text;
using SwapShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SwapShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Trade> Trade { get; set; }
        public DbSet<BarterItem> BarterItem { get; set; }
        public DbSet<BarterTrade> BarterTrade { get; set; }
        public DbSet<BarterType> BarterType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<Trade>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Trade>()
               .Property(b => b.DateCompleted)
               .HasDefaultValueSql("GETDATE()");

            // Restrict deletion of related order when OrderBarters entry is removed
            modelBuilder.Entity<Trade>()
                .HasMany(o => o.BarterTrades)
                .WithOne(l => l.Trade)
                .OnDelete(DeleteBehavior.Restrict);

            // Restrict deletion of related product when OrderBarters entry is removed
            modelBuilder.Entity<BarterItem>()
                .HasMany(o => o.AssociatedTrades)
                .WithOne(l => l.BarterItem)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BarterItem>()
                .HasMany(o => o.AssociatedTrades)
                .WithOne(l => l.BarterItem)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trade>()
                .HasMany(o => o.BarterTrades)
                .WithOne(l => l.Trade)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.ReceivedTrades)
                .WithOne(t => t.Receiver)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
               .HasMany(au => au.SentTrades)
               .WithOne(t => t.Sender)
               .OnDelete(DeleteBehavior.Cascade);


            ApplicationUser user = new ApplicationUser
            {
                TagName = "Ayejah",
                Location = "Nashville, TN",
                ImagePath = " ",
                UserName = "aja@barter.com",
                NormalizedUserName = "aja@barter.com",
                Email = "aja@barter.com",
                NormalizedEmail = "aja@barter.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "APPaw8");
            modelBuilder.Entity<ApplicationUser>().HasData(user);


            modelBuilder.Entity<BarterType>().HasData(
                new BarterType()
                {
                    BarterTypeId = 1,
                    Title = "Food"

                },
                new BarterType()
                {
                    BarterTypeId = 2,
                    Title = "Goods"
                },
                new BarterType()
                {
                    BarterTypeId = 3,
                    Title = "Services"
                },
                new BarterType()
                {
                    BarterTypeId = 4,
                    Title = "Commissions"
                }

            );

        }
    }
}