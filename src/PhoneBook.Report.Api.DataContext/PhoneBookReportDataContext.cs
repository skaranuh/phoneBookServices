using Microsoft.EntityFrameworkCore;
using PhoneBook.Report.Api.Entities.Entities;
namespace PhoneBook.Report.Api.DataContext
{
    public class PhoneBookReportDataContext : DbContext
    {
        public PhoneBookReportDataContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<ReportEntity> Reports { get; set; }
    }
}