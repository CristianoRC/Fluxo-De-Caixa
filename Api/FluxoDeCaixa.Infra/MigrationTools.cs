using FluxoDeCaixa.Infra.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Infra;

public static class MigrationTools
{
    public static void RunMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FluxoDeCaixaDataContext>();
        dbContext.Database.Migrate();
    }
}