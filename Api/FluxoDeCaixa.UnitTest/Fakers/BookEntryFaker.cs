using Bogus;
using FluxoDeCaixa.Domain.Aggregations;
using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.UnitTest.Fakers;

public static class BookEntryFaker
{
    public static BookEntry GenerateValidBookEntry()
    {
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        return GenerateBookEntry(entryBalance, offsetBalance);
    }

    public static BookEntry GenerateInvalidBookEntry()
    {
        var balance = BalanceFaker.GenerateValidBalance();
        return GenerateBookEntry(balance, balance);
    }

    private static BookEntry GenerateBookEntry(Balance entryBalance, Balance offsetBalance)
    {
        var faker = new Faker();
        var amount = new TransactionAmount(faker.Finance.Amount());
        return new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType(), faker.Lorem.Word());
    }
}