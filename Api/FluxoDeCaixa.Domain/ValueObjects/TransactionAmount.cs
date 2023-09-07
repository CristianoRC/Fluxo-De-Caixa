namespace FluxoDeCaixa.Domain.ValueObjects;

public record TransactionAmount : IValueObject
{
    public TransactionAmount(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; set; }
    public bool IsValid => decimal.IsPositive(Value) || Value == decimal.Zero;
}