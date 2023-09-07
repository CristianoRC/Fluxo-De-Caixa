using FluxoDeCaixa.Domain.Repositories;

namespace FluxoDeCaixa.Domain.Services.BookEntry;

public class BookEntryService
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly IBookEntryRepository _bookEntryRepository;

    public BookEntryService(IBalanceRepository balanceRepository, IBookEntryRepository bookEntryRepository)
    {
        _balanceRepository = balanceRepository;
        _bookEntryRepository = bookEntryRepository;
    }

    //TODO: IMPLEMENTAR ESSE CARA!
    //TODO: Precisamos atualizar também o amount dos dois balances!
    //TODO: Ter o lock nesse cara aqui!
}