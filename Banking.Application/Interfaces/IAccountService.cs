using Banking.Application.Dtos;
using Banking.Domain.Models;

namespace Banking.Application.Interfaces;

public interface IAccountService
{
    IEnumerable<Account> GetAccounts();
    void Transfer(AccountTransferDto accountTransferDto);
}