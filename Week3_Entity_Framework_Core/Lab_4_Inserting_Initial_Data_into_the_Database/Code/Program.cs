using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace RetailInventory
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=SOWMYA_SIVANI;Database=RetailInventoryDB;Integrated Security=true;TrustServerCertificate=true;");
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new AppDbContext();
            
            context.Database.EnsureCreated();
            Console.WriteLine("Database created successfully!");
            var electronics = new Category { Name = "Electronics" };
            var groceries = new Category { Name = "Groceries" };
            await context.Categories.AddRangeAsync(electronics, groceries);
            var product1 = new Product { Name = "Laptop", Price = 75000, Category = electronics };
            var product2 = new Product { Name = "Rice Bag", Price = 1200, Category = groceries };
            await context.Products.AddRangeAsync(product1, product2);
            await context.SaveChangesAsync();
            Console.WriteLine("Data inserted successfully!");
        }
    }
}
