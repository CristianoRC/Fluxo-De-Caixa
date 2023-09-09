using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;
using FluxoDeCaixa.Api.Report.Infra.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace FluxoDeCaixa.Api.Report.Infra.Repositories;

public class IdempotencyRepository : IIdempotencyRepository
{
    private readonly IMongoCollection<IdempotencyDbModel> _idempotencyCollection;

    public IdempotencyRepository(IMongoClient mongoClient, IConfiguration config)
    {
        var databaseName = config["database"];
        var database = mongoClient.GetDatabase(databaseName);
        _idempotencyCollection = database.GetCollection<IdempotencyDbModel>("Idempotency");
    }

    public async Task<bool> AlreadyProcess(Guid key)
    {
        var quantity = await _idempotencyCollection.CountDocumentsAsync(x => x.IdempotencyKey == key);
        return quantity > 0;
    }

    public async Task MarkAsProcess(Guid key)
    {
        await _idempotencyCollection.InsertOneAsync(new IdempotencyDbModel(key));
    }
}