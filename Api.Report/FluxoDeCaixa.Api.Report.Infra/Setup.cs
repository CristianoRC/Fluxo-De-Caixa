using FluxoDeCaixa.Api.Report.Domain.Repository;
using FluxoDeCaixa.Api.Report.Domain.Service.Report;
using FluxoDeCaixa.Api.Report.Infra.Http;
using FluxoDeCaixa.Api.Report.Infra.Model;
using FluxoDeCaixa.Api.Report.Infra.Repositories;
using Gotenberg.Sharp.API.Client.Domain.Settings;
using Gotenberg.Sharp.API.Client.Extensions;
using Microsoft.Extensions.Azure;
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
        service.AddTransient<IIdempotencyRepository, IdempotencyRepository>();
        service.AddTransient<IBookEntryRepository, BookEntryRepository>();
        service.AddTransient<IPdfRenderService, PdfRenderService>();

        service.AddOptions<GotenbergSharpClientOptions>("gotenberg");
        service.AddGotenbergSharpClient();

        ConfigureMongoDb(service, configuration);
        ConfigureBlobStorage(service, configuration);
        return service;
    }

    private static void ConfigureBlobStorage(IServiceCollection services, IConfiguration configuration)
    {
        /*services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(new Uri(configuration["blobStorage"]));
        });*/
    }

    private static void ConfigureMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        var client = new MongoClient(configuration["mongoDb"]);
        services.AddSingleton<IMongoClient>(client);

        var objectDiscriminatorConvention = BsonSerializer.LookupDiscriminatorConvention(typeof(object));
        var objectSerializer = new ObjectSerializer(objectDiscriminatorConvention, GuidRepresentation.Standard);
        BsonSerializer.RegisterSerializer(objectSerializer);
    }
}