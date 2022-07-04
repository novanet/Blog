using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json;

namespace Monitoring.Features;

public static class HttpRequestDataExtensions
{
    public static async Task<T> MapTo<T>(this HttpRequestData req)
        => JsonSerializer.Deserialize<T>(await new StreamReader(req.Body).ReadToEndAsync())!;
}
