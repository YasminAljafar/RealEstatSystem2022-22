using Microsoft.EntityFrameworkCore;
using RealEstateSystem2022.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace RealEstateSystem2022.Data.RealEstateContext
{
    public class RealEstateDbContext: DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ApplicationUser>()
            //    .HasMany(u => u.UserTypes)
            //    .WithOne();
        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
    }
}
