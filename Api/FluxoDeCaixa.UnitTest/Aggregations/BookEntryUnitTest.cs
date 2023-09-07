using FluxoDeCaixa.Domain.Aggregations;
using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;
using FluxoDeCaixa.UnitTest.Fakers;

namespace FluxoDeCaixa.UnitTest.Aggregations;

public class BookEntryUnitTest : BaseUnitTest
{
    [Theory(DisplayName = "Ao criar um novo book entry válido, deve ser criado uma transaction de partida (entry) com com o tipo enviado por parâmetro")]
    [InlineData(TransactionType.Credit)]
    [InlineData(TransactionType.Debit)]
    public void Entry(TransactionType transactionType)
    {
        //Arrange
        var amount = new Amount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, transactionType);

        //Assert
        bookEntry.Entry.Type.Should().Be(transactionType);
    }
    
    [Theory(DisplayName = "Ao criar um novo book entry válido, deve ser criado uma transaction de contrapartida (offset) com com o tipo contrario ao enviado por parâmetro")]
    [InlineData(TransactionType.Credit,TransactionType.Debit)]
    [InlineData(TransactionType.Debit, TransactionType.Credit)]
    public void Offset(TransactionType transactionType, TransactionType offsetTransactionType)
    {
        //Arrange
        var amount = new Amount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, transactionType);

        //Assert
        bookEntry.Offset.Type.Should().Be(offsetTransactionType);
    }
    
    [Fact(DisplayName = "Ao criar um novo book entry válido, não deve conter erros na lista")]
    public void ValidBookentry()
    {
        //Arrange
        var amount = new Amount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType());

        //Assert
        bookEntry.Errors.Should().BeEmpty();
        bookEntry.Id.Should().NotBe(Guid.Empty);
        bookEntry.CreatedAt.Should().NotBe(default);
    }
    
    [Fact(DisplayName = "Ao criar um novo book entry válido, o entry e offset devem ter o mesmo ammount")]
    public void EntryAndOffsetAmount()
    {
        //Arrange
        var amount = new Amount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType());

        //Assert
        bookEntry.Entry.Amount.Should().Be(amount);
        bookEntry.Offset.Amount.Should().Be(amount);
    }

    [Fact(DisplayName = "Ao criar um novo book entry, caso seja passado o mesmo balance para partida e contrapartida, deve retornar erro")]
    public void TheSameBalance()
    {
        //Arrange
        var amount = new Amount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, entryBalance, TransactionTypeFaker.GenerateRandomTransactionType());

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Balance duplicado");
    }
    
    [Fact(DisplayName = "Ao criar um novo book entry, caso seja passado um balance invalido para partida, deve retornar erro")]
    public void InvalidBalanceEntry()
    {
        //Arrange
        var amount = new Amount(Faker.Finance.Amount());
        var entryBalance = new Balance(string.Empty);
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType());

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Balance de partida inválido");
    }
    
    [Fact(DisplayName = "Ao criar um novo book entry, caso seja passado um balance invalido para contrapartida, deve retornar erro")]
    public void InvalidBalanceOffset()
    {
        //Arrange
        var amount = new Amount(Faker.Finance.Amount());
        var entryBalance = new Balance(string.Empty);
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType());

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Balance de contrapartida inválido");
    }
    
    [Fact(DisplayName = "Ao criar um novo book entry, caso seja passado um amount invalido, deve retornar erro")]
    public void InvalidAmount()
    {
        //Arrange
        var amount = new Amount(decimal.MinusOne);
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType());

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Valor do amount inválido");
    }
}