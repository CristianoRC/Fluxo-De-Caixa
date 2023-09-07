using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.Repositories;

namespace FluxoDeCaixa.Infra.Repositories;

public class BalanceRepository : IBalanceRepository
{
    public Task<IEnumerable<Balance>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<Balance> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Balance> Create(Balance balance)
    {
        throw new NotImplementedException();
    }
}