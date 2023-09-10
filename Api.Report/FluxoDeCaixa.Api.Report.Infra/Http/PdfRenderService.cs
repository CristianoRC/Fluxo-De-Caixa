using FluxoDeCaixa.Api.Report.Domain.Service.Report;

namespace FluxoDeCaixa.Api.Report.Infra.Http;

public class PdfRenderService : IPdfRenderService
{
    public async Task<byte[]> Render(string html)
    {
        throw new NotImplementedException();
    }
}