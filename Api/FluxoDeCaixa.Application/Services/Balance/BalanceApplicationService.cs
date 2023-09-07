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
        //TODO: Ter um service domain?
        //TODO: Ou manter aqui e ter as regras / exceptions!
        return await _balanceRepository.Create(new Domain.Entities.Balance(createBalanceCommand.Name));
    }

    public async Task<IEnumerable<Domain.Entities.Balance>> Get()
    {
        return await _balanceRepository.Get();
    }
}