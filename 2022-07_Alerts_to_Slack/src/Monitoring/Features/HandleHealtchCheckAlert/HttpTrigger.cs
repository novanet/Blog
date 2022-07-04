using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Monitoring.ApplicationInsights.Metrics;
using Slack;

namespace Monitoring.Features.HandleHealtchCheckAlert
{
    public class HttpTrigger
    {
        private readonly ISlackApi _slackApi;

        public HttpTrigger(ISlackApi slackApi) => _slackApi = slackApi;

        [Function("HandleHealtchCheckAlert")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "healthcheck")] HttpRequestData req)
        {
            var alert = await req.MapTo<MetricAlert>();

            await _slackApi.Send($":warning: Health check failed for `{alert!.GetHealthCheckResource()}`");

            return req.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
