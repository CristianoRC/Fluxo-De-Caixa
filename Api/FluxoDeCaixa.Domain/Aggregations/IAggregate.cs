namespace FluxoDeCaixa.Domain.Aggregations;

public interface IAggregate
{
    Guid Id { get; }
    public IEnumerable<string> Errors { get; }
}