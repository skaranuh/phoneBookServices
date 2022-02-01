using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Entities.Entities;

namespace PhoneBook.Api.DataContext
{
    public class PhoneBookDataContext : DbContext
    {
        public PhoneBookDataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ContactInfo>()
               .HasIndex(i => new { i.ContactInfoType, i.ContactPersonId, i.Value })
               .IsUnique();
        }
    }
}