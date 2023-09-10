using System.Globalization;

namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public class ReportQuery
{
    private readonly List<string> _errors = new List<string>();

    public ReportQuery(string date, string balanceId)
    {
        var dateHasBeenConverted = DateOnly.TryParse(date, new CultureInfo("pt-BR"), out var dateConverted);
        if (dateHasBeenConverted)
            Date = dateConverted;
        else
            _errors.Add("Erro ao converter a data do relatório");
        
        var balanceHasBeenConverted = Guid.TryParse(balanceId, out var balanceIdConverted);
        if (balanceHasBeenConverted)
            BalanceId = balanceIdConverted;
        else
            _errors.Add("Erro ao converter o id do balance");
    }

    public DateOnly Date { get; }
    public Guid BalanceId { get; set; }
    public bool IsValid => _errors.Any() is false;
    public IEnumerable<string> Errors => _errors;
}