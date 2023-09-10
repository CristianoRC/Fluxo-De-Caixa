using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Service.BookEntry;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FluxoDeCaixa.Api.Report.QueueTrigger;

public static class BookEntryTopicTrigger
{
    /*
    [Function("BookEntryTopicTrigger")]
    public static async Task Run(
        [RabbitMQTrigger("BookEntryTopicTrigger", ConnectionStringSetting = "rabbitMq")] string message,
        FunctionContext context)
    {
        var logger = context.GetLogger(nameof(BookEntryTopicTrigger));
        try
        {
            var bookEntryService = context.InstanceServices.GetRequiredService<IBookEntryService>();
            var createBookEntry = JsonConvert.DeserializeObject<BookEntry>(message);
            await bookEntryService.Create(createBookEntry);
        }
        catch
        {
            logger.LogError("Erro ao salvar o book entry. Message: {Message}", message);
            throw;
        }
    }*/
}