using FluxoDeCaixa.Domain.Aggregations;
using FluxoDeCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Infra.Configuration;

public class FluxoDeCaixaDataContext : DbContext
{
    public FluxoDeCaixaDataContext()
    {
    }
    
    public FluxoDeCaixaDataContext(DbContextOptions<FluxoDeCaixaDataContext> options) : base(options)
    {
    }

    public DbSet<Balance> Balances { get; set; }
    public DbSet<BookEntry> BookEntries { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FluxoDeCaixaDataContext).Assembly);
    }
}