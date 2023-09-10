using System.Net;
using System.Web;
using FluxoDeCaixa.Api.Report.Domain.Service.Report;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Api.Report.HTTPTrigger;

public static class GenerateReportHttpTrigger
{
    [Function("GenerateReportHTTPTrigger")]
    public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "report")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        var service = executionContext.InstanceServices.GetRequiredService<IReportService>();
        var query = HttpUtility.ParseQueryString(req.Url.Query);
        var date = query["date"];
        var balance = query["balance"];

        var report = await service.GenerateReport(new ReportQuery(date, balance));
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteBytesAsync(report);
        response.Headers.Add("Content-Type", "text/html; charset=utf-8");
        return response;
    }
}