using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OptShopAPI.Models;

namespace OptShopAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options) { }

      public DbSet<Product> products { get; set; }

        public DbSet<Customer> customers { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<Payment> payments { get; set; }
    }
}
