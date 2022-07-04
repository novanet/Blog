using Microsoft.Extensions.DependencyInjection;

namespace Slack;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSlack(this IServiceCollection services)
    {
        services.AddHttpClient<ISlackApi, SlackApi>(client =>
        {
            client.BaseAddress = new Uri("https://hooks.slack.com");
        });

        return services;
    }
}
