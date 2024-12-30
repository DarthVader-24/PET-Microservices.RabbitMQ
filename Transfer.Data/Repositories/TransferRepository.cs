using Transfer.Data.Context;
using Transfer.Domain.Interfaces;
using Transfer.Domain.Models;

namespace Transfer.Data.Repositories;

public class TransferRepository(TransferDbContext context): ITransferRepository
{
    private readonly TransferDbContext _context = context;

    public IEnumerable<TransferLog> GetTransferLogs()
    {
        return _context.TransferLogs;
    }
}