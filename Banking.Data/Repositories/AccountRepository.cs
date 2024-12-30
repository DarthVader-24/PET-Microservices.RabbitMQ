using Banking.Data.Context;
using Banking.Domain.Interfaces;
using Banking.Domain.Models;

namespace Banking.Data.Repositories;

public class AccountRepository(BankingDbContext context) : IAccountRepository
{
    private readonly BankingDbContext _context = context;

    public IEnumerable<Account> GetAccounts()
    {
        return _context.Accounts;
    }
}