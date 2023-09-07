using FluxoDeCaixa.Domain.Entities;

namespace FluxoDeCaixa.Domain.Repositories;

public interface IBalanceRepository
{
    Task<IEnumerable<Balance>> Get();
    Task<Balance> Get(Guid id);
    Task<Balance> Create(Balance balance);
}