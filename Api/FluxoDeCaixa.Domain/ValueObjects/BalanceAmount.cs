namespace FluxoDeCaixa.Domain.ValueObjects;

public readonly record struct BalanceAmount(decimal Value) : IValueObject
{
    public bool IsValid => true;
}