using FluxoDeCaixa.Application.Services.Balance;
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

    [HttpGet("{id}/statement")]
    public async Task<IActionResult> GetStatement(Guid id)
    {
        try
        {
            var statement = await _service.GetStatement(id);
            return Ok(statement);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao buscar extrato do balance {BalanceId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}