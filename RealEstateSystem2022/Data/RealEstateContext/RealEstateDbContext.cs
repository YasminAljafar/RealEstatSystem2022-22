using Microsoft.EntityFrameworkCore;
using RealEstateSystem2022.Models;

namespace RealEstateSystem2022.Data.RealEstateContext
{
    public class RealEstateDbContext:DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options):base(options)
        {
        }

        //public DbSet<ApplicationUser> User { get; set; }
    }
}
