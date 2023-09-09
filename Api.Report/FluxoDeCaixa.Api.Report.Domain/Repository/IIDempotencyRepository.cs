namespace FluxoDeCaixa.Api.Report.Domain.Repository;

public interface IIdempotencyRepository
{
    public Task<bool> AlreadyProcess(Guid key);
    Task MarkAsProcess(Guid key);
}