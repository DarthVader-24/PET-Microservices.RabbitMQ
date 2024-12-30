using Microsoft.AspNetCore.Mvc;
using Transfer.Application.Interfaces;
using Transfer.Domain.Models;

namespace Transfer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransferController(ITransferService transferService) : ControllerBase
{
    private readonly ITransferService _transferService = transferService;

    [HttpGet]
    public ActionResult<IEnumerable<TransferLog>> GetTransferLogs()
    {
        return Ok(_transferService.GetTransferLogs());
    }
}