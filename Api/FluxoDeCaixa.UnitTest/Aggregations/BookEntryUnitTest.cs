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
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, transactionType, description);

        //Assert
        bookEntry.Entry.Type.Should().Be(transactionType);
    }

    [Theory(DisplayName = "Ao criar um novo book entry válido, deve ser criado uma transaction de contrapartida (offset) com com o tipo contrario ao enviado por parâmetro")]
    [InlineData(TransactionType.Credit, TransactionType.Debit)]
    [InlineData(TransactionType.Debit, TransactionType.Credit)]
    public void Offset(TransactionType transactionType, TransactionType offsetTransactionType)
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, transactionType, description);

        //Assert
        bookEntry.Offset.Type.Should().Be(offsetTransactionType);
    }

    [Fact(DisplayName = "Ao criar um novo book entry válido, não deve conter erros na lista")]
    public void ValidBookEntry()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Errors.Should().BeEmpty();
        bookEntry.Id.Should().NotBe(Guid.Empty);
        bookEntry.CreatedAt.Should().NotBe(default);
    }

    [Fact(DisplayName = "Ao criar um novo book entry válido, o entry e offset devem ter o mesmo ammount")]
    public void EntryAndOffsetAmount()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Entry.TransactionAmount.Should().Be(amount);
        bookEntry.Offset.TransactionAmount.Should().Be(amount);
    }
    
    [Fact(DisplayName = "Ao criar um novo book entry, caso seja passado o mesmo balance para partida e contrapartida, deve retornar erro")]
    public void TheSameBalance()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, entryBalance, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Balance duplicado");
    }

    [Fact(DisplayName = "Ao criar um novo book entry, caso seja passado um balance invalido para partida, deve retornar erro")]
    public void InvalidBalanceEntry()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = new Balance(string.Empty);
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Balance de partida inválido");
    }

    [Fact(DisplayName = "Ao criar um novo book entry, caso seja passado um balance invalido para contrapartida, deve retornar erro")]
    public void InvalidBalanceOffset()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = new Balance(string.Empty);
        var description = Faker.Lorem.Word();
        
        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Balance de contrapartida inválido");
    }

    [Fact(DisplayName = "Ao criar um novo book entry, caso seja passado um amount invalido, deve retornar erro")]
    public void InvalidAmount()
    {
        //Arrange
        var amount = new TransactionAmount(decimal.MinusOne);
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Valor do amount inválido");
    }

    [Fact(DisplayName = "Ao criar um novo book entry, deve retornar erro caso o amount seja nulo")]
    public void NullAmount()
    {
        //Arrange
        var amount = new TransactionAmount(decimal.MinusOne);
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Valor do amount inválido");
    }
    
    [Fact(DisplayName = "Ao criar um novo book entry, deve retornar erro caso o balance da partida seja nulo")]
    public void NullEntryBalance()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, null!, offsetBalance, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Balance de partida inválido");
    }
    
    [Fact(DisplayName = "Ao criar um novo book entry, deve retornar erro caso o balance da contrapartida seja nulo")]
    public void NullOffsetBalance()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, null!, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Errors.Count().Should().Be(1);
        var error = bookEntry.Errors.First();
        error.Should().Be("Balance de contrapartida inválido");
    }

    [Fact(DisplayName = "Ao criar um novo book entry, não deve criar as transações caso tenha algum erro")]
    public void ShouldNotCreateTransaction()
    {
        //Arrange
        var amount = new TransactionAmount(Faker.Finance.Amount());
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var description = Faker.Lorem.Word();

        //Act
        var bookEntry = new BookEntry(amount, entryBalance, null!, TransactionTypeFaker.GenerateRandomTransactionType(), description);

        //Assert
        bookEntry.Entry.Should().BeNull();
        bookEntry.Offset.Should().BeNull();
    }
}