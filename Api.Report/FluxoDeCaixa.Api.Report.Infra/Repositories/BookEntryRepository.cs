using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FluxoDeCaixa.Api.Report.Infra.Repositories;

public class BookEntryRepository : IBookEntryRepository
{
    private readonly IMongoCollection<Transaction> _transactionCollection;

    public BookEntryRepository(IMongoClient mongoClient, IConfiguration config)
    {
        var databaseName = config["database"];
        var database = mongoClient.GetDatabase(databaseName);
        _transactionCollection = database.GetCollection<Transaction>("transaction");
    }

    public async Task SaveTransaction(IEnumerable<Transaction> transactions)
    {
        await _transactionCollection.InsertManyAsync(transactions);
    }
}