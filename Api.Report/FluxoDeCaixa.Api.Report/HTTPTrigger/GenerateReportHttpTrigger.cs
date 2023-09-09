using System.Net;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace FluxoDeCaixa.Api.Report.HTTPTrigger;

public static class GenerateReportHttpTrigger
{
    [Function("GenerateReportHTTPTrigger")]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "report")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger(nameof(GenerateReportHttpTrigger));

        var query = HttpUtility.ParseQueryString(req.Url.Query);
        var date = query["date"];
        var balance = query["balance"];
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new {date, balance});
        return response;
    }
}