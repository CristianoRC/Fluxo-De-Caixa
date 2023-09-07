using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;
using FluxoDeCaixa.UnitTest.Fakers;

namespace FluxoDeCaixa.UnitTest.Entity;

public class TransactionUnitTest : BaseUnitTest
{
    [Fact(DisplayName = "Deve ser inválido se o valor de amount for inválido")]
    public void InvalidAmount()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount(max: decimal.MinusOne));
        var transactionType = TransactionTypeFaker.GenerateRandomTransactionType();
        var balance = BalanceFaker.GenerateValidBalance();

        //Act
        var transaction = new Transaction(transactionType, amount, balance);

        //Assert
        transaction.IsValid.Should().BeFalse();
    }

    [Fact(DisplayName = "Deve ser inválido se o balance for inválido")]
    public void InvalidBalance()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var transactionType = TransactionTypeFaker.GenerateRandomTransactionType();
        var balance = new Balance(string.Empty);

        //Act
        var transaction = new Transaction(transactionType, amount, balance);

        //Assert
        transaction.IsValid.Should().BeFalse();
    }

    [Fact(DisplayName = "Deve ser válido se o balance e o amount forem passados corretamente")]
    public void ValidBalance()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var transactionType = TransactionTypeFaker.GenerateRandomTransactionType();
        var balance = BalanceFaker.GenerateValidBalance();

        //Act
        var transaction = new Transaction(transactionType, amount, balance);

        //Assert
        transaction.IsValid.Should().BeTrue();
        transaction.Id.Should().NotBe(Guid.Empty);
        transaction.CreatedAt.Should().NotBe(default);
        transaction.Type.Should().Be(transactionType);
        transaction.TransactionAmount.Should().Be(amount);
        transaction.Balance.Should().Be(balance);
    }
}