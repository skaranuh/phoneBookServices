using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PhoneBook.Api.DataContext;

namespace PhoneBook.Api
{
    public class PhoneBookContextFactory : IDesignTimeDbContextFactory<PhoneBookDataContext>
    {
        private const string _connectionName = "PhoneBookConnection";
        private const string _migrationAssembly = "PhoneBook.Api.DataContext";

        public PhoneBookDataContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var connectionString = configuration.GetConnectionString(_connectionName);

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<PhoneBookDataContext>()
                .UseNpgsql(connectionString, x => x.MigrationsAssembly(_migrationAssembly));

            return new PhoneBookDataContext(dbContextOptionsBuilder.Options);
        }
    }
}