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
                Entry = TransactionFaker.GenerateFakeTransaction(),
                Offset = TransactionFaker.GenerateFakeTransaction()
            }
        };
    }
}