using FluxoDeCaixa.Domain.Entities;

namespace FluxoDeCaixa.Domain.Repositories;

public interface IBalanceRepository
{
    Task<IEnumerable<Balance>> GetBalances();
    Task<Balance> GetBalance(Guid id);
    Task CreateBalance(Balance balance);
}