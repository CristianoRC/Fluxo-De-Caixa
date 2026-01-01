using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoDeCaixa.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.Id);
                });

            // Create Transactions table as Ledger Append-Only
            // NOTA: Esta tabela foi criada manualmente usando SQL customizado porque o EF Core
            // não suporta nativamente a sintaxe WITH (LEDGER = ON (APPEND_ONLY = ON))
            // Ledger Tables garantem auditoria automática e histórico imutável de transações
            // APPEND_ONLY permite apenas INSERT (não permite UPDATE/DELETE)
            // IMPORTANTE: Ledger tables NÃO podem ter FK com CASCADE - deve usar NO ACTION
            migrationBuilder.Sql(@"
                CREATE TABLE [Transactions] (
                    [Id] uniqueidentifier NOT NULL,
                    [Type] int NOT NULL,
                    [TransactionAmount] decimal(18,2) NOT NULL,
                    [BalanceAfterTransaction] decimal(18,2) NOT NULL,
                    [BalanceId] uniqueidentifier NOT NULL,
                    [CreatedAt] datetimeoffset NOT NULL,
                    [Description] nvarchar(max) NOT NULL,
                    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
                    CONSTRAINT [FK_Transactions_Balances_BalanceId] FOREIGN KEY ([BalanceId])
                        REFERENCES [Balances] ([Id]) ON DELETE NO ACTION
                )
                WITH (LEDGER = ON (APPEND_ONLY = ON));
            ");

            // Create BookEntries table as Ledger Append-Only
            // NOTA: Esta tabela foi criada manualmente usando SQL customizado porque o EF Core
            // não suporta nativamente a sintaxe WITH (LEDGER = ON (APPEND_ONLY = ON))
            // Ledger Tables garantem auditoria automática e histórico imutável de lançamentos contábeis
            // APPEND_ONLY permite apenas INSERT (não permite UPDATE/DELETE)
            // IMPORTANTE: Ledger tables NÃO podem ter FK com CASCADE - todas devem usar NO ACTION
            migrationBuilder.Sql(@"
                CREATE TABLE [BookEntries] (
                    [Id] uniqueidentifier NOT NULL,
                    [EntryId] uniqueidentifier NOT NULL,
                    [OffsetId] uniqueidentifier NOT NULL,
                    [CreatedAt] datetimeoffset NOT NULL,
                    CONSTRAINT [PK_BookEntries] PRIMARY KEY ([Id]),
                    CONSTRAINT [FK_BookEntries_Transactions_EntryId] FOREIGN KEY ([EntryId])
                        REFERENCES [Transactions] ([Id]) ON DELETE NO ACTION,
                    CONSTRAINT [FK_BookEntries_Transactions_OffsetId] FOREIGN KEY ([OffsetId])
                        REFERENCES [Transactions] ([Id]) ON DELETE NO ACTION
                )
                WITH (LEDGER = ON (APPEND_ONLY = ON));
            ");

            migrationBuilder.CreateIndex(
                name: "IX_BookEntries_EntryId",
                table: "BookEntries",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookEntries_OffsetId",
                table: "BookEntries",
                column: "OffsetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BalanceId",
                table: "Transactions",
                column: "BalanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookEntries");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Balances");
        }
    }
}
