using System.Text;
using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Service.Report;

namespace FluxoDeCaixa.Api.Report.Infra.Report;

public class ReportBuilder : IReportBuilder
{
    private readonly StringBuilder _htmlContent = new();

    public ReportBuilder()
    {
        var fileHtml = File.ReadAllText("./report.html", Encoding.UTF8);
        _htmlContent.Append(fileHtml);
    }

    public IReportBuilder InsertReportDate(DateOnly reportDate)
    {
        _htmlContent.Replace("#DATA", reportDate.ToLongDateString());
        return this;
    }


    public IReportBuilder InsertBalanceName(string balance)
    {
        _htmlContent.Replace("#BALANCE", balance);
        return this;
    }

    public IReportBuilder InsertDebitAmount(decimal amount)
    {
        _htmlContent.Replace("#DEBIT", $"R$ {amount:0.00}");
        return this;
    }

    public IReportBuilder InsertCreditAmount(decimal amount)
    {
        _htmlContent.Replace("#CREDIT", $"R$ {amount:0.00}");
        return this;
    }


    public IReportBuilder InsertTotalAmount(decimal amount)
    {
        _htmlContent.Replace("#AMOUNT", $"R$ {amount:0.00}");
        return this;
    }

    public IReportBuilder InsertTransactions(IEnumerable<ITransactionReport> transactions)
    {
        _htmlContent.Replace("#TRANSACTION", GenerateTransactionsHtml(transactions));
        return this;
    }

    private static string GenerateTransactionsHtml(IEnumerable<ITransactionReport> transactions)
    {
        var stringBuilder = new StringBuilder();
        foreach (var transaction in transactions)
        {
            var transactionClass = transaction.TypeId == (int) TransactionType.Credit ? "credit" : "debit";
            stringBuilder.Append(@$"
                <tr class=""item"">
				    <td>{transaction.Description}</td>
				    <td class=""{transactionClass}"">
                        R$ {transaction.TransactionAmount:0.00}
                    </td>
                </tr>");
        }

        return stringBuilder.ToString();
    }

    public string Build()
    {
        return _htmlContent.ToString();
    }
}