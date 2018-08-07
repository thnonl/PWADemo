using Microsoft.EntityFrameworkCore;

namespace React_Redux.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}