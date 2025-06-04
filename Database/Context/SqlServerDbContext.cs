using Database.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Context
{
    public class SqlServerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database = OnlineShopм");
        }
        public SqlServerDbContext() 
        {
            Database.EnsureCreated();
            SeedDefaultUsers();
        }
        private void SeedDefaultUsers()
        {
            if (!Users.Any())
            {
                Users.AddRange(new List<User>
            {
                new User("admin", "admin@example.com", "admin123", "Admin"),
                new User("user1", "user1@example.com", "pass1", "Customer"),
                new User("user2", "user2@example.com", "pass2", "Customer")

            });

                SaveChanges();
            }
        }
    }
}
