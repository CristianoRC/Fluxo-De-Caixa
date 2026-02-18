using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;
using FluxoDeCaixa.Api.Report.Domain.Service.Report;
using FluxoDeCaixa.Api.Report.Infra.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FluxoDeCaixa.Api.Report.Infra.Repositories;

public class BookEntryRepository : IBookEntryRepository
{
    private readonly IMongoCollection<TransactionDbModel> _transactionCollection;

    public BookEntryRepository(IMongoClient mongoClient, IConfiguration config)
    {
        var databaseName = config["database"];
        var database = mongoClient.GetDatabase(databaseName);
        _transactionCollection = database.GetCollection<TransactionDbModel>("transaction");
    }

    public async Task SaveTransaction(IEnumerable<Transaction> transactions)
    {
        var transactionsConvertedToDbModel = transactions.Select(t => new TransactionDbModel(t));
        await _transactionCollection.InsertManyAsync(transactionsConvertedToDbModel);
    }

    public async Task<IEnumerable<ITransactionReport>> GetTransactions(ReportQuery reportQuery)
    {
        var startDate = reportQuery.Date.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc);
        var endDate = reportQuery.Date.ToDateTime(new TimeOnly(23, 59, 59), DateTimeKind.Utc);
        var transactions = await _transactionCollection
            .FindAsync(x => x.BalanceId == reportQuery.BalanceId
                            && x.CreatedAt >= startDate
                            && x.CreatedAt <= endDate);

        return await transactions.ToListAsync();
    }
}