namespace FluxoDeCaixa.Domain.Entities;

public class Balance : IEntity
{
    public Balance(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; }
    public string Name { get; }
    public DateTimeOffset CreatedAt { get; }

    public bool IsValid
    {
        get
        {
            var idIsValid = Id != Guid.Empty;
            var nameIsValid = string.IsNullOrEmpty(Name) is false;
            var createdAtIsValid = CreatedAt != default;
            return idIsValid && nameIsValid && createdAtIsValid;
        }
    }
}