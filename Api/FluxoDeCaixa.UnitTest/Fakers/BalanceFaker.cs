using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.UnitTest.Fakers;

public static class BalanceFaker
{
    public static Balance GenerateValidBalance()
    {
        var balanceName = new Bogus.Faker().Finance.AccountName();
        return new Balance(balanceName);
    }

    public static Balance GenerateValidBalance(decimal amount)
    {
        var faker = new Bogus.Faker();
        var balanceName = faker.Finance.AccountName();
        return new Balance(Guid.NewGuid(), balanceName, faker.Date.PastOffset(), new BalanceAmount(amount));
    }
}