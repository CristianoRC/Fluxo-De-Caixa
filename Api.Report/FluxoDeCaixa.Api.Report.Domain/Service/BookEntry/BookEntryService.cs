using FluxoDeCaixa.Api.Report.Domain.Repository;

namespace FluxoDeCaixa.Api.Report.Domain.Service.BookEntry;

public class BookEntryService : IBookEntryService
{
    private readonly IIdempotencyService _idempotencyService;
    private readonly IBookEntryRepository _repository;

    public BookEntryService(IIdempotencyService idempotencyService, IBookEntryRepository repository)
    {
        _idempotencyService = idempotencyService;
        _repository = repository;
    }

    public async Task Create(Entities.BookEntry createBookEntry)
    {
        var alreadyProcess = await _idempotencyService.MessageAlreadyProcessed(createBookEntry);
        if (alreadyProcess)
            return;

        var transactions = new[] {createBookEntry.BookEntryData.Entry, createBookEntry.BookEntryData.Offset};
        await _repository.SaveTransaction(transactions);
    }
}