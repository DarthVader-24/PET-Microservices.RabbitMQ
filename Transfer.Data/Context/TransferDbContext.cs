using Microsoft.EntityFrameworkCore;
using Transfer.Domain.Models;

namespace Transfer.Data.Context;

public class TransferDbContext(DbContextOptions<TransferDbContext> options) : DbContext(options)
{
    public DbSet<TransferLog> TransferLogs { get; init; }
}