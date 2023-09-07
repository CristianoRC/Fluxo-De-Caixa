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
        return await _balanceRepository.Create(new Domain.Entities.Balance(createBalanceCommand.Name));
    }

    public async Task<IEnumerable<Domain.Entities.Balance>> Get()
    {
        return await _balanceRepository.Get();
    }
}