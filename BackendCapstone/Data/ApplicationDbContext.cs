using System;
using System.Collections.Generic;
using System.Text;
using BackendCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendCapstone.Data
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

            modelBuilder.Entity<BarterItem>().HasData(
                new BarterItem()
                {
                    BarterItemId = 1,
                    BarterTypeId = 3,
                    AppUserId = user.Id,
                    Description = "keep your kids entertained",
                    Title = "Dance Lessons",
                    Quantity = 100,
                    Value = 2
                },
                   new BarterItem()
                   {
                       BarterItemId = 2,
                       BarterTypeId = 2,
                       AppUserId = user.Id,
                       Description = "It dips low",
                       Title = "Boat",
                       Quantity = 100,
                       Value = 3
                   },
                   new BarterItem()
                   {
                       BarterItemId = 3,
                       BarterTypeId = 1,
                       AppUserId = user.Id,
                       Description = "feed your family",
                       Title = "Potatoe",
                       Quantity = 100,
                       Value = 4
                   },
                  new BarterItem()
                  {
                      BarterItemId = 4,
                      BarterTypeId = 4,
                      AppUserId = user.Id,
                      Description = "It flies high",
                      Title = "Kite",
                      Quantity = 100,
                      Value = 5
                  }
            );


        }
    }
}