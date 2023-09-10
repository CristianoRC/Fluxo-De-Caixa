using FluxoDeCaixa.Api.Report.Domain.Service.Report;
using Gotenberg.Sharp.API.Client;
using Microsoft.Extensions.Configuration;

namespace FluxoDeCaixa.Api.Report.Infra.Http;

public class PdfRenderService : IPdfRenderService
{
    private readonly GotenbergSharpClient _gotenbergClient;

    public PdfRenderService(GotenbergSharpClient gotenbergClient)
    {
        _gotenbergClient = gotenbergClient;
    }
    
    public async Task<byte[]> Render(string html)
    {
        throw new NotImplementedException();
    }
}