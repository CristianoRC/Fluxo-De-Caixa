using FluxoDeCaixa.Domain.Repositories;

namespace FluxoDeCaixa.Domain.Services.BookEntry;

public class BookEntryService
{
    private readonly IBalanceRepository _balanceRepository;

    public BookEntryService(IBalanceRepository balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }
}