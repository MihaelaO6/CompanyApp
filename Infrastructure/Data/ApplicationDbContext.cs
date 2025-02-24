using CompanyApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(c => c.CompanyId);
            modelBuilder.Entity<Contact>().HasKey(c => c.ContactId);
            modelBuilder.Entity<Country>().HasKey(c => c.CountryId);

            // Company - Contact (1-M)
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Contacts)
                .WithOne(co => co.Company)
                .HasForeignKey(co => co.CompanyId);
            // Country - Contact (1-M)
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Contacts)
                .WithOne(co => co.Country)
                .HasForeignKey(co => co.CountryId);

        }
    }
}
