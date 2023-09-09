using FluxoDeCaixa.Api.Report.Infra.Model;
using FluxoDeCaixa.Api.Report.UnitTest.Faker;

namespace FluxoDeCaixa.Api.Report.UnitTest.DbModel;

public class TransactionDbModelUnitTest : BaseUnitTest
{
    [Fact(DisplayName = "Ao converter uma transação em DbModel, se o typeId for 1, deve ser Crédito")]
    public void Credit()
    {
        //Arrange
        var transaction = TransactionFaker.GenerateFakeTransaction();
        transaction.Type = 1;
        
        //Act
        var transactionDbModel = new TransactionDbModel(transaction);

        //Assert
        transactionDbModel.Type.Should().Be("Crédito");
        transactionDbModel.TypeId.Should().Be(1);
        
        transactionDbModel.Id.Should().Be(transaction.Id);
        transactionDbModel.Description.Should().Be(transaction.Description);
        transactionDbModel.CreatedAt.Should().Be(transaction.CreatedAt);
    }
    
    [Fact(DisplayName = "Ao converter uma transação em DbModel, se o typeId for 0, deve ser Débito")]
    public void Debit()
    {
        //Arrange
        var transaction = TransactionFaker.GenerateFakeTransaction();
        transaction.Type = 0;
        
        //Act
        var transactionDbModel = new TransactionDbModel(transaction);

        //Assert
        transactionDbModel.Type.Should().Be("Débito");
        transactionDbModel.TypeId.Should().Be(0);
        
        transactionDbModel.Id.Should().Be(transaction.Id);
        transactionDbModel.Description.Should().Be(transaction.Description);
        transactionDbModel.CreatedAt.Should().Be(transaction.CreatedAt);
    }

    [Fact(DisplayName = "Ao converter uma transação em DbModel, deve converter os dados de financeiros corretamente")]
    public void BalanceData()
    {
        //Arrange
        var transaction = TransactionFaker.GenerateFakeTransaction();
        
        //Act
        var transactionDbModel = new TransactionDbModel(transaction);

        //Assert
        transactionDbModel.TransactionAmount.Should().Be(transaction.TransactionAmount.Value);
        transactionDbModel.BalanceAfterTransaction.Should().Be(transaction.BalanceAfterTransaction.Value);
        transactionDbModel.BalanceName.Should().Be(transaction.Balance.Name);
        transactionDbModel.BalanceId.Should().Be(transaction.Balance.Id);
    }
}