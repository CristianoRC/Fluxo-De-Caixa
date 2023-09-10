using System.Text;
using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public class ReportBuilder
{
    private readonly StringBuilder _htmlContent = new();

    public ReportBuilder()
    {
        var fileHtml = File.ReadAllText("./report.html", Encoding.UTF8);
        _htmlContent.Append(fileHtml);
    }

    public ReportBuilder InsertReportDate(DateOnly reportDate)
    {
        _htmlContent.Replace("#DATA", reportDate.ToLongDateString());
        return this;
    }


    public ReportBuilder InsertBalanceName(string balance)
    {
        _htmlContent.Replace("#BALANCE", balance);
        return this;
    }

    public ReportBuilder InsertDebitAmount(decimal amount)
    {
        _htmlContent.Replace("#DEBIT", $"R$ {amount}");
        return this;
    }

    public ReportBuilder InsertCreditAmount(decimal amount)
    {
        _htmlContent.Replace("#CREDIT", $"R$ {amount}");
        return this;
    }


    public ReportBuilder InsertTotalAmount(decimal amount)
    {
        _htmlContent.Replace("#AMOUNT", $"R$ {amount}");
        return this;
    }

    public ReportBuilder InsertTransactions(IEnumerable<ITransactionReport> transactions)
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
                        <strong>R$ {transaction.TransactionAmount}</strong>
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