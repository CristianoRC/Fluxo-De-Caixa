using FluxoDeCaixa.Api.Report.Domain.Service.Report;
using Gotenberg.Sharp.API.Client;
using Gotenberg.Sharp.API.Client.Domain.Builders;
using Gotenberg.Sharp.API.Client.Domain.Builders.Faceted;

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
        var builder = new HtmlRequestBuilder()
            .AddDocument(doc => doc.SetBody(html))
            .WithDimensions(dims =>
            {
                dims.SetPaperSize(PaperSizes.A4)
                    .SetMargins(Margins.None);
            });

        var req = await builder.BuildAsync();
        var reportPdfStream = await _gotenbergClient.HtmlToPdfAsync(req);
        var memoryStream = new MemoryStream();
        await reportPdfStream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}