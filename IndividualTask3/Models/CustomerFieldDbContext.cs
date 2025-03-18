using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace IndividualTask3.Models
{
    public class CustomerFieldDbContext : DbContext
    {
        public DbSet<CustomerField> CustomerFields { get; set; }

        public CustomerFieldDbContext(DbContextOptions<CustomerFieldDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerField>().HasKey(c => c.AccountNumber);
            modelBuilder.Entity<CustomerField>().HasData(
                new CustomerField { AccountNumber = "1234567890", Industry = "Manufacturing", Fields = new() { "Invoice Number", "Quantity", "Delivery Address" } },
                new CustomerField { AccountNumber = "2345678901", Industry = "Education", Fields = new() { "Matric Number", "Level", "Course" } },
                new CustomerField { AccountNumber = "3456789012", Industry = "Telecom", Fields = new() { "GSM Number", "Network", "Residential Address" } }
            );
        }
    }
}
