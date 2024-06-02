using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HelloWorldAppFunction;
public class HelloWorldFunctionApp
{
    private readonly ILogger<HelloWorldFunctionApp> _logger;

    public HelloWorldFunctionApp(ILogger<HelloWorldFunctionApp> logger)
    {
        _logger = logger;
    }

    [Function("HelloWorld")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        string name = req.Query["name"];

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        name = name ?? data?.name;

        string responseMessage = "Built at Fri May 31 16:29:29 2024 "+(string.IsNullOrEmpty(name)
                                  ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                                  : $"Hello, {name}. This HTTP triggered function executed successfully.")
            + DateTime.Now.ToString("yyyy MMM dd hh:mm:ss.fff ttt (zzz)");

        _logger.LogInformation("C# HTTP trigger function processed a request. {responseMessage}", responseMessage);

        return new OkObjectResult(responseMessage);
    }
}
