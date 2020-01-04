using Microsoft.EntityFrameworkCore;

namespace DellChallenge.D1.Api.Dal
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
    }
}
