using Microsoft.EntityFrameworkCore;
using SwaggerWebAPIDemo.Models;

namespace SwaggerWebAPIDemo.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Department).HasMaxLength(50);
                entity.Property(e => e.Salary).HasPrecision(18, 2);
            });

            // Seed data
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "John Doe", Email = "john@company.com", Department = "IT", Salary = 75000, JoinDate = DateTime.Now.AddYears(-2) },
                new Employee { Id = 2, Name = "Jane Smith", Email = "jane@company.com", Department = "HR", Salary = 65000, JoinDate = DateTime.Now.AddYears(-1) },
                new Employee { Id = 3, Name = "Bob Johnson", Email = "bob@company.com", Department = "Finance", Salary = 80000, JoinDate = DateTime.Now.AddMonths(-6) },
                new Employee { Id = 4, Name = "Alice Brown", Email = "alice@company.com", Department = "Marketing", Salary = 60000, JoinDate = DateTime.Now.AddMonths(-3) }
            );
        }
    }
}
