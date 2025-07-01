using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using MyFirstAzureFunction.Models;

namespace MyFirstAzureFunction;

internal class HttpEndpoint(IRequestClient<Welcome> requestClient)
{
    [Function(nameof(TestIt))]
    public async Task<IActionResult> TestIt(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
        HttpRequest req)
    {
        var guidSent = Guid.NewGuid().ToString();
        var welcome = new Welcome
        {
            Name = guidSent
        };
        var response = await requestClient.GetResponse<Goodbye>(welcome);
        var msg = response.Message;
        var obj = new
        {
            guidSent,
            msg.InstanceId,
            msg.ReceivedName
        };

        return new OkObjectResult(obj);
    }
}