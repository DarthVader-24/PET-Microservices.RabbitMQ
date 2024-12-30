using Banking.Application.Interfaces;
using Banking.Domain.Interfaces;
using Banking.Domain.Models;

namespace Banking.Application.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository;

    public IEnumerable<Account> GetAccounts()
    {
        return _accountRepository.GetAccounts();
    }
}