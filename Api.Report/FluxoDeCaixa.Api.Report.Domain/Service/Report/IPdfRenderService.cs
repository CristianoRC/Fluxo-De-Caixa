namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public interface IPdfRenderService
{
    Task<byte[]> Render(string html);
}