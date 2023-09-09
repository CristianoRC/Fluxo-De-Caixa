using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FluxoDeCaixa.Api.Report;

public static class BookEntryTopicTrigger
{
    [Function("CreateCustomerTopicTrigger")]
    public static async Task Run([RabbitMQTrigger("CreateCustomerTopicTrigger", ConnectionStringSetting = "rabbitMq")]
        string message, FunctionContext context)
    {
        var bookEntryService = context.InstanceServices.GetService<IBookEntryService>();
        var createBookEntry = JsonConvert.DeserializeObject<BookEntry>(message);
        await bookEntryService.Create(createBookEntry);
    }
}