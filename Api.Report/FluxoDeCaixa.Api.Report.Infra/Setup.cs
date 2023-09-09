using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace FluxoDeCaixa.Api.Report.Infra;

public static class Setup
{
    public static IServiceCollection AddInfra(this IServiceCollection service, IConfiguration configuration)
    {
        ConfigureMongoDb(service, configuration);
        return service;
    }
    
    private static void ConfigureMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDb"]);
        services.AddSingleton<IMongoClient>(client);
        
        var objectDiscriminatorConvention = BsonSerializer.LookupDiscriminatorConvention(typeof(object));
        var objectSerializer = new ObjectSerializer(objectDiscriminatorConvention, GuidRepresentation.Standard);
        BsonSerializer.RegisterSerializer(objectSerializer);
    }
}