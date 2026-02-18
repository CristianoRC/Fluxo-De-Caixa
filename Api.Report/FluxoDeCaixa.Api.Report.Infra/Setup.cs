using FluxoDeCaixa.Api.Report.Domain.Repository;
using FluxoDeCaixa.Api.Report.Domain.Service.Report;
using FluxoDeCaixa.Api.Report.Infra.Http;
using FluxoDeCaixa.Api.Report.Infra.Report;
using FluxoDeCaixa.Api.Report.Infra.Repositories;
using Gotenberg.Sharp.API.Client.Domain.Settings;
using Gotenberg.Sharp.API.Client.Extensions;
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
        service.AddScoped<IReportBuilder, ReportBuilder>();

        ConfigureGotenbergClient(service, configuration);
        ConfigureMongoDb(service, configuration);
        return service;
    }

    private static void ConfigureGotenbergClient(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<GotenbergSharpClientOptions>()
            .Configure(x =>
            {
                var url = configuration["gotenberg"];
                x.ServiceUrl = new Uri(url);
                x.HealthCheckUrl = new Uri($"{url}/health");
                x.RetryPolicy = new RetryOptions()
                {
                    Enabled = true,
                    RetryCount = 5,
                    BackoffPower = 1.5,
                    LoggingEnabled = true
                };
            });
        services.AddGotenbergSharpClient();
    }
    
    private static void ConfigureMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        
        var objectDiscriminatorConvention = BsonSerializer.LookupDiscriminatorConvention(typeof(object));
        var objectSerializer = new ObjectSerializer(objectDiscriminatorConvention, GuidRepresentation.Standard);
        BsonSerializer.RegisterSerializer(objectSerializer);
        
        var client = new MongoClient(configuration["mongodb"]);
        services.AddSingleton<IMongoClient>(client);
    }
}