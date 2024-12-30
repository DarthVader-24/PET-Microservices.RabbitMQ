using Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Data.Context;

public class BankingDbContext(DbContextOptions<BankingDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; init; }
}