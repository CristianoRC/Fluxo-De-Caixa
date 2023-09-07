namespace FluxoDeCaixa.Domain.ValueObjects;

public record BalanceAmount : IValueObject
{
    public BalanceAmount(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; } = decimal.Zero;
    public bool IsValid => true;
}