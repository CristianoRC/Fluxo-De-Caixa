using FluxoDeCaixa.Application.Services.Balance;
using FluxoDeCaixa.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeCaixa.Api.Controllers;

[Route("/api/[controller]")]
public class BalanceController : Controller
{
    private readonly IBalanceApplicationService _service;
    private readonly ILogger<BalanceController> _logger;

    public BalanceController(IBalanceApplicationService service, ILogger<BalanceController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBalanceCommand command)
    {
        try
        {
            var balance = await _service.Create(command);
            if (balance.IsValid)
                return Created(string.Empty, balance);
            return BadRequest(balance);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao criar um novo balance");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var balances = await _service.Get();
        return Ok(balances);
    }
}