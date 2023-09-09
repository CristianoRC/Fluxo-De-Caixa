using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.UnitTest.Faker;

public static class BookEntryFaker
{
    public static BookEntry GenerateValidBookEntry()
    {
        var faker = new Bogus.Faker();

        return new BookEntry()
        {
            IdempotencyKey = Guid.NewGuid(),
            CorrelationId = Guid.NewGuid(),
            Data = new BookEntryData()
            {
                Id = Guid.NewGuid(),
                CreatedAt = faker.Date.Past(),
                Entry = GenerateFakeTransaction(faker),
                Offset = GenerateFakeTransaction(faker)
            }
        };
    }

    private static Transaction GenerateFakeTransaction(Bogus.Faker faker)
    {
        return new Transaction()
        {
            Id = Guid.NewGuid(),
            CreatedAt = faker.Date.Past(),
            BalanceAfterTransaction = new Amount {Value = faker.Finance.Amount()},
            TransactionAmount = new Amount {Value = faker.Finance.Amount()},
            Type = faker.PickRandom(0, 1),
            Balance = GenerateValidBalance(faker)
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