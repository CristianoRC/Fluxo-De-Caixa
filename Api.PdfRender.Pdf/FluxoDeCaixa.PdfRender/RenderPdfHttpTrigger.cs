using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FluxoDeCaixa.PdfRender;

public static class RenderPdfHttpTrigger
{
    [Function("RenderPdfHttpTrigger")]
    public static HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Render")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("RenderPdfHttpTrigger");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
    }
}