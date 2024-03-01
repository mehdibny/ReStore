using e_commece_API_React.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commece_API_React.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
