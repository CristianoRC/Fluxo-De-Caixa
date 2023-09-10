using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Infra.Model;

namespace FluxoDeCaixa.Api.Report.UnitTest.Entity;

public class ConsolidatedStatementUnitTest : BaseUnitTest
{
    [Fact(DisplayName = "O valor de DebitAmount deve ser a soma de todas as transações de débito")]
    public void DebitAmountValue()
    {
        //Arrange
        var firstTransactionAmount = Faker.Finance.Amount();
        var secondTransactionAmount = Faker.Finance.Amount();
        var firstTransaction = new TransactionDbModel()
        {
            TransactionAmount = firstTransactionAmount,
            Id = Guid.NewGuid(),
            BalanceName = Faker.Lorem.Word(),
            TypeId = (int) TransactionType.Debit
        };
        var secondTransaction = new TransactionDbModel()
        {
            TransactionAmount = secondTransactionAmount,
            Id = Guid.NewGuid(),
            BalanceName = Faker.Lorem.Word(),
            TypeId = (int) TransactionType.Debit
        };

        //Act
        var consolidatedStatement = new ConsolidatedStatement(new[] {firstTransaction, secondTransaction});

        //Assert
        consolidatedStatement.DebitAmount.Should().Be(firstTransactionAmount + secondTransactionAmount);
    }

    [Fact(DisplayName = "O valor de DebitAmount deve ser a soma de todas as transações de débito")]
    public void CreditAmountValue()
    {
        //Arrange
        var firstTransactionAmount = Faker.Finance.Amount();
        var secondTransactionAmount = Faker.Finance.Amount();
        var firstTransaction = new TransactionDbModel()
        {
            TransactionAmount = firstTransactionAmount,
            Id = Guid.NewGuid(),
            BalanceName = Faker.Lorem.Word(),
            TypeId = (int) TransactionType.Credit
        };
        var secondTransaction = new TransactionDbModel()
        {
            TransactionAmount = secondTransactionAmount,
            Id = Guid.NewGuid(),
            BalanceName = Faker.Lorem.Word(),
            TypeId = (int) TransactionType.Credit
        };

        //Act
        var consolidatedStatement = new ConsolidatedStatement(new[] {firstTransaction, secondTransaction});

        //Assert
        consolidatedStatement.CreditAmount.Should().Be(firstTransactionAmount + secondTransactionAmount);
    }

    [Fact(DisplayName = "O valor de TotalAmount deve ser todos os creditos - todos os debitos")]
    public void TotalAmount()
    {
        //Arrange
        var debitTransactionAmount = Faker.Finance.Amount();
        var creditTransactionAmount = Faker.Finance.Amount();
        var debitTransaction = new TransactionDbModel()
        {
            TransactionAmount = decimal.Negate(debitTransactionAmount),
            Id = Guid.NewGuid(),
            BalanceName = Faker.Lorem.Word(),
            TypeId = (int) TransactionType.Debit
        };
        var creditTransaction = new TransactionDbModel()
        {
            TransactionAmount = creditTransactionAmount,
            Id = Guid.NewGuid(),
            BalanceName = Faker.Lorem.Word(),
            TypeId = (int) TransactionType.Credit
        };

        //Act
        var consolidatedStatement = new ConsolidatedStatement(new[] {debitTransaction, creditTransaction});

        //Assert
        consolidatedStatement.TotalAmount.Should().Be(creditTransactionAmount - debitTransactionAmount);
    }

    [Fact(DisplayName = "Balance Name deve conter o nome do balance da priemira transação")]
    public void BalanceName()
    {
        //Arrange
        var debitTransactionAmount = Faker.Finance.Amount();
        var creditTransactionAmount = Faker.Finance.Amount();
        var transaction = new TransactionDbModel()
        {
            TransactionAmount = debitTransactionAmount,
            Id = Guid.NewGuid(),
            BalanceName = Faker.Lorem.Word(),
            TypeId = (int) TransactionType.Debit
        };
        var otherTransaction = new TransactionDbModel()
        {
            TransactionAmount = creditTransactionAmount,
            Id = Guid.NewGuid(),
            BalanceName = Faker.Lorem.Word(),
            TypeId = (int) TransactionType.Credit
        };

        //Act
        var consolidatedStatement = new ConsolidatedStatement(new[] {transaction, otherTransaction});

        //Assert
        consolidatedStatement.BalanceName.Should().Be(transaction.BalanceName);
    }
    
    [Fact(DisplayName = "Balance Name deve ser stirng em branco caos não tenha nenhuma transação")]
    public void BalanceNameWithoutTransactions()
    {
        //Arrange/Act
        var consolidatedStatement = new ConsolidatedStatement(Enumerable.Empty<ITransactionReport>());

        //Assert
        consolidatedStatement.BalanceName.Should().Be(string.Empty);
    }
}