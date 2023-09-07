using FluxoDeCaixa.Infra.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Api;

public static class MigrationTools
{
    public static void RunMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FluxoDeCaixaDataContext>();
        dbContext.Database.Migrate();
    }
}