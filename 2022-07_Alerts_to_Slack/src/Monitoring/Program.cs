using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Slack;

await new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((_, config) =>
    {
        config.AddJsonFile("local.settings.json", true);
    })
    .ConfigureServices((builder, services) =>
    {
        services.AddSlack();
    })
    .Build()
    .RunAsync();