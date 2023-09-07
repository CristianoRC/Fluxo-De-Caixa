using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Infra.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Infra.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly FluxoDeCaixaDataContext _dataContext;

    public BalanceRepository(FluxoDeCaixaDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<Balance>> Get()
    {
        return await _dataContext.Balances.AsNoTracking().ToListAsync();
    }

    public async Task<Balance?> Get(Guid id)
    {
        return await _dataContext.Balances.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Balance> Create(Balance balance)
    {
        await _dataContext.Balances.AddAsync(balance);
        await _dataContext.SaveChangesAsync();
        return balance;
    }
}