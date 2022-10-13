using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RealEstateSystem.ViewModels;

namespace RealEstateSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Models.Property> Properties { get; set; }
        public DbSet<Commerial> Commerials { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<EarthType> EarthTypes { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyStatus> PropertyStatuses { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyUser> PropertyUsers { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public string WebRootPath { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>().Property(x => x.lat).HasPrecision(18, 10);
            modelBuilder.Entity<Property>().Property(x => x.lng).HasPrecision(18, 10);
        }


        // public DbSet<RealEstateSystem.ViewModels.PropertyViewModel> PropertyViewModel { get; set; } الفيو موديل ما منضيفها عال context

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(u => u.Properties)
        //        .WithMany(p => p.Users)
        //        .UsingEntity<PropertyUser>(
        //        j => j
        //            .HasOne(pu => pu.Property)
        //            .WithMany(p => p.PropertyUsers)
        //            .HasForeignKey(pu => pu.PropertyId),

        //         j => j
        //            .HasOne(pu => pu.ApplicationUser)
        //            .WithMany(p => p.PropertyUsers)
        //            .HasForeignKey(pu => pu.ApplicationUserId),

        //         j =>
        //         {
        //             j.Property(pu => pu.PuplishDate).HasDefaultValueSql("GETDATE");
        //             j.HasKey(p => new { p.ApplicationUserId, p.PropertyId });
        //         }


        //        );
        //}


        //public ApplicationUser GetApplicationUserData()
        //{
        //    var user = new ApplicationUser();
        //    if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
        //    {
        //        var prinicpal = _httpContextAccessor.HttpContext.User;

        //        var x = prinicpal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).ToArray();
        //        if (prinicpal != null)
        //            user = new ApplicationUserData
        //            {
        //                Email = prinicpal.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault(),
        //                UserId = prinicpal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault(),
        //                FullName = prinicpal.Claims.Where(c => c.Type == "FullName").Select(c => c.Value).FirstOrDefault(),
        //                UserName = prinicpal.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).FirstOrDefault(),
        //                Roles = prinicpal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray(),
        //                //JobRole = prinicpal.Claims.FirstOrDefault(c => c.Type == "JobRole").ToString()
        //                //Roles = customIdentity.Roles,
        //                // SessionId = customIdentity.SessionId,
        //                //UserName = prinicpal.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault()
        //            };
        //        var jobRole = prinicpal.Claims.FirstOrDefault(c => c.Type == "JobRole");

        //        if (jobRole != null)
        //            user.JobRole = jobRole.Value.ToString();

        //        var VendorId = prinicpal.Claims.FirstOrDefault(c => c.Type == "VendorId");

        //        if (!string.IsNullOrWhiteSpace(VendorId.Value))
        //            user.VendorId = Convert.ToInt32(VendorId.Value.ToString());

        //    }
        //    else if (applicationUserData != null)
        //        user = applicationUserData;

        //    return user;
        //}
    }
}
