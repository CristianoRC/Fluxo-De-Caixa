namespace FluxoDeCaixa.Application.Services.Balance;

public interface IBalanceApplicationService
{
    Task<Domain.Entities.Balance> Create(CreateBalanceCommand createBalanceCommand);
    Task<IEnumerable<Domain.Entities.Balance>> Get();
}