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
}