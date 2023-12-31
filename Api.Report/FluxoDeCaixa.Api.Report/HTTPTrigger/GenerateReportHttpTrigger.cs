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
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "report")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        var service = executionContext.InstanceServices.GetRequiredService<IReportService>();
        
        var reportQuery = GenerateReportQuery(req);
        if (reportQuery.IsValid is false)
            return await GenerateInvalidQueryResponse(req, reportQuery);

        var report = await service.GenerateReport(reportQuery);
        return await GenerateReportResponse(req, reportQuery, report);
    }

    private static ReportQuery GenerateReportQuery(HttpRequestData request)
    {
        var query = HttpUtility.ParseQueryString(request.Url.Query);
        var date = query["date"];
        var balance = query["balance"];
        return new ReportQuery(date, balance);
    }

    private static async Task<HttpResponseData> GenerateReportResponse(HttpRequestData request, ReportQuery query, byte[] report)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteBytesAsync(report);
        response.Headers.Add("Content-Type", "application/pdf");
        response.Headers.Add("Access-Control-Allow-Origin", "*");
        return response;
    }

    private static async Task<HttpResponseData> GenerateInvalidQueryResponse(HttpRequestData request, ReportQuery query)
    {
        var response = request.CreateResponse(HttpStatusCode.BadRequest);
        await response.WriteAsJsonAsync(query);
        response.Headers.Add("Access-Control-Allow-Origin", "*");
        return response;
    }
}