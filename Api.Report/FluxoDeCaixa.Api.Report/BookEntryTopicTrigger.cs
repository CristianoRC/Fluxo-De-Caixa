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
        var bookEntryUseCase = context.InstanceServices.GetService<ICreateBookEntryUseCase>();
        var createBookEntry = JsonConvert.DeserializeObject<CreateBookEntry>(message);
        await bookEntryUseCase.Execute(createBookEntry);
    }
}