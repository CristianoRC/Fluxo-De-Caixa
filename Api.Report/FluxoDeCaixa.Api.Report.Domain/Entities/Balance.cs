namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public record Balance
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Amount Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsValid { get; set; }
}