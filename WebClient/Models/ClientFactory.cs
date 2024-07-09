using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebClient.Models
{
    public class ClientFactory : IDesignTimeDbContextFactory<ClientDbContext>
    {
        public ClientDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DB");
            var optionBuilder = new DbContextOptionsBuilder<ClientDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new ClientDbContext(optionBuilder.Options);
        }
    }
}
