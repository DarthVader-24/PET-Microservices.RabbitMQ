using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Transfer.Data.Context;

public class BankingDbContextFactory : IDesignTimeDbContextFactory<TransferDbContext>
{
    public TransferDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Transfer.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TransferDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("TransferDbConnection"));

        return new TransferDbContext(optionsBuilder.Options);
    }
}