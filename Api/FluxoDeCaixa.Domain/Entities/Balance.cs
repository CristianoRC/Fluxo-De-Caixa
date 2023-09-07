namespace FluxoDeCaixa.Domain.Entities;

public class Balance : IEntity
{
    public Balance(Guid id, string name, DateTimeOffset createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }

    public Balance(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public static bool operator ==(Balance firstBalance, Balance secondBalance)
    {
        return firstBalance.Id == secondBalance.Id;
    }

    public static bool operator !=(Balance firstBalance, Balance secondBalance)
    {
        return firstBalance.Id != secondBalance.Id;
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