using FluxoDeCaixa.Api.Report.Domain.Service.Report;

namespace FluxoDeCaixa.Api.Report.UnitTest.Service;

public class ReportQueryUnitTest : BaseUnitTest
{
    [Theory(DisplayName = "Deve ser inválido se for passado um Balance inválida")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("dfsfdg")]
    public void InvalidBalanceParse(string balance)
    {
        //Arrange/Act
        var query = new ReportQuery("09/09/2023", balance);

        //Assert
        query.IsValid.Should().BeFalse();
        query.Errors.First().Should().Be("Erro ao converter o id do balance");
    }

    [Theory(DisplayName = "Deve ser inválido se for passado uma data inválida")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("dfsfdg")]
    public void InvalidDateParse(string date)
    {
        //Arrange/Act
        var query = new ReportQuery(date, Guid.NewGuid().ToString());

        //Assert
        query.IsValid.Should().BeFalse();
        query.Errors.First().Should().Be("Erro ao converter a data do relatório");
    }

    [Theory(DisplayName = "Deve converter a data no fortamto pt-br com sucesso")]
    [InlineData("28/07/1998")]
    [InlineData("28-07-1998")]
    public void ValidDateFormat(string date)
    {
        //Arrange/Act
        var query = new ReportQuery(date, Guid.NewGuid().ToString());

        //Assert
        query.Date.Day.Should().Be(28);
        query.Date.Month.Should().Be(7);
        query.Date.Year.Should().Be(1998);
        query.IsValid.Should().BeTrue();
    }

    [Fact(DisplayName = "Deve converter o guid com sucesso")]
    public void ValidGuidFormat()
    {
        //Arrenge
        var id = Guid.NewGuid();
        //Arrange/Act
        var query = new ReportQuery("28/07/1998", id.ToString());

        //Assert
        query.BalanceId.Should().Be(id);
        query.IsValid.Should().BeTrue();
    }
}