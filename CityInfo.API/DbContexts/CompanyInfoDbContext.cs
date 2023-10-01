using CompanyInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyInfo.API.DbContexts
{
    public class CompanyInfoDbContext : DbContext
    {
        public CompanyInfoDbContext(DbContextOptions<CompanyInfoDbContext> options)
            : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<CarModel> CarModels { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasData
                (
                   new Company("Hyundai")
                   {
                       ID = 1,
                       Description = "This is Hyundai"
                   },
                   new Company("Jac")
                   {
                       ID = 2,
                       Description = "This is Jac"
                   },
                   new Company("Volvo")
                   {
                       ID = 3,
                       Description = "This is Volvo"
                   }
                );
            modelBuilder.Entity<CarModel>()
                .HasData(
                new CarModel("Elantra")
                {
                    ID = 1,
                    CompanyID = 1,
                    Description = "This is Elantra"
                },
                new CarModel("Kona")
                {
                    ID = 2,
                    CompanyID = 1,
                    Description = "This is Kona"
                },
                new CarModel("A30")
                {
                    ID = 3,
                    CompanyID = 2,
                    Description = "This is A30"
                },
                new CarModel("Heyue RS")
                {
                    ID = 4,
                    CompanyID = 2,
                    Description = "This is Heyue RS"
                },
                new CarModel("XC90")
                {
                    ID = 5,
                    CompanyID = 3,
                    Description = "This is XC90"
                },
                new CarModel("V60")
                {
                    ID = 6,
                    CompanyID = 3,
                    Description = "This is V60"
                }
                );
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite();
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
