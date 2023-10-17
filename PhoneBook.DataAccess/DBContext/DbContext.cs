using Microsoft.EntityFrameworkCore;
using PhoneBook.Entities.Models;

namespace PhoneBook.DataAccess.DBContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<User>()
            .HasMany(b => b.PhoneNumbers)
            .WithOne(p => p.User);

            builder.Entity<User>()
            .HasMany(b => b.Notes)
            .WithOne(p => p.User);
        }

        public ApplicationDbContext() { }

        public DbSet<Login> Logins { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    }
}



