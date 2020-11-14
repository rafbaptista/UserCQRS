using Microsoft.EntityFrameworkCore;
using UserCQRS.Domain.Entities;
using UserCQRS.Infra.Data.Mappings;

namespace UserCQRS.Infra.Data.Context
{
    public class UserCQRSContext : DbContext
    {
        public UserCQRSContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMapping());
        }
    }
}
