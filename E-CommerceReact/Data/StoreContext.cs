using E_CommerceReact.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceReact.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
    }
}
