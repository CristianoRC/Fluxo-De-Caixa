namespace FluxoDeCaixa.Domain.Entities;

public interface IEntity
{
    public Guid Id { get; }
    public bool IsValid { get; }
}