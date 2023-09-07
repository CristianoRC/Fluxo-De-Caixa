using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.UnitTest.Fakers;

public static class TransactionTypeFaker
{
    public static TransactionType GenerateRandomTransactionType()
    {
        var faker = new Bogus.Faker();
        return faker.PickRandom(TransactionType.Credit, TransactionType.Debit);
    }
}