using FluxoDeCaixa.Domain.Aggregations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoDeCaixa.Infra.Configuration;

public class BookEntryConfiguration : IEntityTypeConfiguration<BookEntry>
{
    public void Configure(EntityTypeBuilder<BookEntry> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt);
        // Ledger tables não suportam FK com CASCADE - deve usar NoAction
        builder.HasOne(x => x.Entry)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Offset)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}