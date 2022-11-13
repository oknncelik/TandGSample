using Microsoft.EntityFrameworkCore;

namespace Product.Data.Contexts
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {

        }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Entities.Entities.Product> Products { get; set; }
    }
}
