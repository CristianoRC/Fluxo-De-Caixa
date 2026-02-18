namespace FluxoDeCaixa.Application.Services.Balance;

public record TransactionStatementItem(
    Guid Id,
    int Type,
    decimal Amount,
    decimal BalanceAfterTransaction,
    DateTimeOffset CreatedAt,
    string Origin
);
