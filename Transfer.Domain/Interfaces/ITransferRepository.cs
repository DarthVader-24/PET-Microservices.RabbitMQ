using Transfer.Domain.Models;

namespace Transfer.Domain.Interfaces;

public interface ITransferRepository
{
    IEnumerable<TransferLog> GetTransferLogs();
}