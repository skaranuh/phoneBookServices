using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Entities;

namespace PhoneBook.Api.DataContext
{
    public class PhoneBookDataContext : DbContext
    {
        public PhoneBookDataContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
    }
}