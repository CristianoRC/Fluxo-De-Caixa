using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;
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
}