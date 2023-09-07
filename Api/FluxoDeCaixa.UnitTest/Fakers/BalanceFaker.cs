using FluxoDeCaixa.Domain.Entities;

namespace FluxoDeCaixa.UnitTest.Fakers;

public static class BalanceFaker
{
    public static Balance GenerateValidBalance()
    {
        var balanceName = new Bogus.Faker().Finance.AccountName();
        return new Balance(balanceName);
    }
}