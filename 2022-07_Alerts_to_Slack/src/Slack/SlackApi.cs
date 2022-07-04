using System.Text;
using System.Text.Json;

namespace Slack;
public interface ISlackApi
{
    Task Send(string text);
}

public class SlackApi : ISlackApi
{
    private readonly HttpClient _httpClient;

    public SlackApi(HttpClient httpClient) => _httpClient = httpClient;

    public async Task Send(string text)
    {
        try
        {
            await _httpClient.PostAsync(
                requestUri: "<relative_webhook_path>",
                content: new StringContent(JsonSerializer.Serialize(new Payload(text)), Encoding.UTF8, "application/json"));
        }
        catch (Exception)
        {
            // Ignore
        }
    }
}
