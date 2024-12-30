using Banking.Application.Dtos;
using Banking.Application.Interfaces;
using Banking.Domain.Commands;
using Banking.Domain.Interfaces;
using Banking.Domain.Models;
using Domain.Core.Bus;

namespace Banking.Application.Services;

public class AccountService(IAccountRepository accountRepository, IEventBus eventBus) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IEventBus _eventBus = eventBus;
    public IEnumerable<Account> GetAccounts()
    {
        return _accountRepository.GetAccounts();
    }

    public void Transfer(AccountTransferDto accountTransferDto)
    {
        var createTransferCommand = new CreateTransferCommand(accountTransferDto.AccountSourceId,
            accountTransferDto.AccountDestinationId, accountTransferDto.TransferAmount);
        
        _eventBus.SendCommand(createTransferCommand);
    }
}