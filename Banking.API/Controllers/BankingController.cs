using Banking.Application.Interfaces;
using Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BankingController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;

    [HttpGet]
    public ActionResult<IEnumerable<Account>> GetBankAccounts()
    {
        return Ok(_accountService.GetAccounts());
    }
}