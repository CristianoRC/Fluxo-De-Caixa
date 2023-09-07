using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Services.BookEntry;

public class BookEntryService : IBookEntryService
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly IBookEntryRepository _bookEntryRepository;

    public BookEntryService(IBalanceRepository balanceRepository, IBookEntryRepository bookEntryRepository)
    {
        _balanceRepository = balanceRepository;
        _bookEntryRepository = bookEntryRepository;
    }

    public async Task<Aggregations.BookEntry> Create(CreateBookEntry command)
    {
        var entryBalance = await _balanceRepository.Get(command.EntryBalance);
        var offsetBalance = await _balanceRepository.Get(command.OffsetBalance);
        
        var transactionAmount = new TransactionAmount(command.Amount);
        var bookEntry = new Aggregations.BookEntry(transactionAmount, entryBalance, offsetBalance, command.TransactionType);
        if (bookEntry.Errors.Any())
            return bookEntry;

        await _bookEntryRepository.Save(bookEntry);
        return bookEntry;
    }
}