namespace FluxoDeCaixa.Domain.ValueObjects;

public record Amount : IValueObject
{
    public Amount(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; set; }
    public bool IsValid => decimal.IsPositive(Value) || Value == decimal.Zero;
}