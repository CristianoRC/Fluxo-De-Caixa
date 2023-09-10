namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public class ReportQuery
{
    public ReportQuery(string date, string balanceId)
    {
        Date = DateOnly.Parse(date);
        BalanceId = Guid.Parse(balanceId);
    }

    public DateOnly Date { get; }
    public Guid BalanceId { get; }
}