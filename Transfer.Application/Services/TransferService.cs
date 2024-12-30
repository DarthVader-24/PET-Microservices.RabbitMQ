using Transfer.Application.Interfaces;
using Transfer.Domain.Interfaces;
using Transfer.Domain.Models;

namespace Transfer.Application.Services;

public class TransferService(ITransferRepository transferRepository) : ITransferService
{
    private readonly ITransferRepository _transferRepository = transferRepository;

    public IEnumerable<TransferLog> GetTransferLogs()
    {
        return _transferRepository.GetTransferLogs();
    }
}