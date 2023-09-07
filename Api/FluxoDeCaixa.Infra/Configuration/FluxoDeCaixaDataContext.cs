using FluxoDeCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Infra.Configuration;

internal class FluxoDeCaixaDataContext : DbContext
{
    public FluxoDeCaixaDataContext(DbContextOptions<FluxoDeCaixaDataContext> options) : base(options)
    {
    }
    
    public DbSet<Balance> Balances { get; set; }
    //public DbSet<BookEntry> BookEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FluxoDeCaixaDataContext).Assembly);
    }
}