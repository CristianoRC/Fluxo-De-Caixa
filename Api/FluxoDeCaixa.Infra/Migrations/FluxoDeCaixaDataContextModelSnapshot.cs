﻿// <auto-generated />
using System;
using FluxoDeCaixa.Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FluxoDeCaixa.Infra.Migrations
{
    [DbContext(typeof(FluxoDeCaixaDataContext))]
    partial class FluxoDeCaixaDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FluxoDeCaixa.Domain.Aggregations.BookEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OffsetId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.HasIndex("OffsetId");

                    b.ToTable("BookEntries");
                });

            modelBuilder.Entity("FluxoDeCaixa.Domain.Entities.Balance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("FluxoDeCaixa.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("BalanceAfterTransaction")
                        .HasColumnType("numeric");

                    b.Property<Guid>("BalanceId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("numeric");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("FluxoDeCaixa.Domain.Aggregations.BookEntry", b =>
                {
                    b.HasOne("FluxoDeCaixa.Domain.Entities.Transaction", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FluxoDeCaixa.Domain.Entities.Transaction", "Offset")
                        .WithMany()
                        .HasForeignKey("OffsetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");

                    b.Navigation("Offset");
                });

            modelBuilder.Entity("FluxoDeCaixa.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("FluxoDeCaixa.Domain.Entities.Balance", "Balance")
                        .WithMany()
                        .HasForeignKey("BalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Balance");
                });
#pragma warning restore 612, 618
        }
    }
}
