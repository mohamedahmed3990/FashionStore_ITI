using FashionStore.DAL.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets for Entities
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ParentCategory> ParentCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(
           typeof(AppDbContext).Assembly
       );
        }


    }
}
