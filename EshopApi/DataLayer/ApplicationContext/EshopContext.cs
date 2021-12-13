using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.ApplicationContext
{
    public class EshopContext : DbContext
    {
        public EshopContext(DbContextOptions<EshopContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Slider> Sliders { get; set; }
    }
}
