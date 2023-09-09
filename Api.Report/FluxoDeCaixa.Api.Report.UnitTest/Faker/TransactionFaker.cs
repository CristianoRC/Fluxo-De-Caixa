using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.UnitTest.Faker;

public static class TransactionFaker
{
    public static Transaction GenerateFakeTransaction()
    {
        var faker = new Bogus.Faker();
        return new Transaction()
        {
            Id = Guid.NewGuid(),
            CreatedAt = faker.Date.Past(),
            BalanceAfterTransaction = new Amount {Value = faker.Finance.Amount()},
            TransactionAmount = new Amount {Value = faker.Finance.Amount()},
            Type = faker.PickRandom(0, 1),
            Balance = GenerateValidBalance(faker),
            Description =  faker.Lorem.Word()
        };
    }
    
    private static Balance GenerateValidBalance(Bogus.Faker faker)
    {
        return new Balance
        {
            Id = Guid.NewGuid(),
            CreatedAt = faker.Date.Past(),
            Amount = new Amount {Value = faker.Finance.Amount()},
            Name = faker.Lorem.Word()
        };
    }
}