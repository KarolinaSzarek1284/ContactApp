
using ContactApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ContactApplication.Data
{
    public class ContactContext : DbContext
    {
        private string _connectionString =
            "Server=(localdb)\\local;Database=ContactDb;Trusted_Connection=True;";

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<Contact>()
                .Property(u => u.Password)
                .IsRequired();
            modelBuilder.Entity<Contact>()
                .Property(u => u.PhoneNumber)
                .IsRequired();
            modelBuilder.Entity<Contact>()
                .Property(u => u.FirstName)
                .IsRequired();
            modelBuilder.Entity<Contact>()
                .Property(u => u.LaseName)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
