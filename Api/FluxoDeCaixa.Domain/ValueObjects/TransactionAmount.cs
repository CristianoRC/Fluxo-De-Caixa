namespace FluxoDeCaixa.Domain.ValueObjects;

public readonly record struct TransactionAmount(decimal Value) : IValueObject
{
    public bool IsValid => decimal.IsPositive(Value) || Value == decimal.Zero;
}