using FluxoDeCaixa.Application.Services.BookEntry;
using FluxoDeCaixa.Domain.Services.BookEntry;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeCaixa.Api.Controllers;

[Route("/api/[controller]")]
public class BookEntryController : Controller
{
    private readonly IBookEntryApplicationService _service;
    private readonly ILogger<BookEntryController> _logger;

    public BookEntryController(IBookEntryApplicationService service, ILogger<BookEntryController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookEntry command)
    {
        try
        {
            var bookEntry = await _service.Create(command);
            if (bookEntry.Errors.Any())
                return BadRequest(bookEntry);
            return Created(string.Empty, bookEntry);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new {Error = e.Message});
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro ao criar um novo book entry");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}