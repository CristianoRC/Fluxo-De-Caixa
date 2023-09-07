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

    [Fact(DisplayName = "Ao criar uma transação de credito, o balance after transaction deve ter o valor atual do balance + transaction amount")]
    public void CreditBalanceAfterTransaction()
    {
        //Arrange
        var balanceAmount = Faker.Finance.Amount(-99999);
        var transactionAmount = new TransactionAmount(Faker.Finance.Amount());
        var transactionType = TransactionType.Credit;
        var balance = BalanceFaker.GenerateValidBalance(balanceAmount);
        var expectedBalanceAfterTransaction = balanceAmount + transactionAmount.Value;
        
        //Act
        var transaction = new Transaction(transactionType, transactionAmount, balance);

        //Assert
        transaction.BalanceAfterTransaction.Value.Should().Be(expectedBalanceAfterTransaction);
    }
    
    [Fact(DisplayName = "Ao criar uma transação de debito, o balance after transaction deve ter o valor atual do balance - transaction amount")]
    public void DebitBalanceAfterTransaction()
    {
        //Arrange
        var balanceAmount = Faker.Finance.Amount(-99999);
        var transactionAmount = new TransactionAmount(Faker.Finance.Amount());
        var transactionType = TransactionType.Debit;
        var balance = BalanceFaker.GenerateValidBalance(balanceAmount);
        var expectedBalanceAfterTransaction = balanceAmount - transactionAmount.Value;
        
        //Act
        var transaction = new Transaction(transactionType, transactionAmount, balance);

        //Assert
        transaction.BalanceAfterTransaction.Value.Should().Be(expectedBalanceAfterTransaction);
    }

    [Fact(DisplayName = "Ao criar uma transação, deve atualizar também o amount do balance")]
    public void UpdateBalanceAmount()
    {
        //Arrange
        var balanceAmount = Faker.Finance.Amount(-99999);
        var transactionAmount = new TransactionAmount(Faker.Finance.Amount());
        var transactionType = TransactionType.Debit;
        var balance = BalanceFaker.GenerateValidBalance(balanceAmount);
        var expectedBalanceAfterTransaction = balanceAmount - transactionAmount.Value;
        
        //Act
        var transaction = new Transaction(transactionType, transactionAmount, balance);

        //Assert
        transaction.Balance.Amount.Value.Should().Be(expectedBalanceAfterTransaction);
    }
}
