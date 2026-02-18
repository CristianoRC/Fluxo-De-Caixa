using FluxoDeCaixa.Domain.Repositories;

namespace FluxoDeCaixa.Application.Services.Balance;

public class BalanceApplicationService : IBalanceApplicationService
{
    private readonly IBalanceRepository _balanceRepository;

    public BalanceApplicationService(IBalanceRepository balanceRepository)
    {
        _balanceRepository = balanceRepository;
    }

    public async Task<Domain.Entities.Balance> Create(CreateBalanceCommand createBalanceCommand)
    {
        var balance = new Domain.Entities.Balance(createBalanceCommand.Name);
        if (balance.IsValid)
            await _balanceRepository.Create(balance);
        return balance;
    }

    public async Task<IEnumerable<Domain.Entities.Balance>> Get()
    {
        return await _balanceRepository.Get();
    }

    public async Task<IEnumerable<TransactionStatementItem>> GetStatement(Guid balanceId)
    {
        var bookEntries = await _balanceRepository.GetBookEntries(balanceId);
        return bookEntries.Select(be =>
        {
            var isEntry = be.Entry.Balance.Id == balanceId;
            var transaction = isEntry ? be.Entry : be.Offset;
            var counterpart = isEntry ? be.Offset : be.Entry;
            return new TransactionStatementItem(
                transaction.Id,
                (int)transaction.Type,
                transaction.TransactionAmount.Value,
                transaction.BalanceAfterTransaction.Value,
                transaction.CreatedAt,
                counterpart.Balance.Name
            );
        });
    }
}