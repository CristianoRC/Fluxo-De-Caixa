using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoDeCaixa.Infra.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.BalanceAfterTransaction).HasConversion(x => x.Value, amount => new BalanceAmount(amount));
        builder.HasOne(x => x.Balance)
            .WithMany()
            .HasForeignKey(x => x.Id)
            .IsRequired();
    }
}