using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Monitoring.ApplicationInsights.Availability;
using Slack;

namespace Monitoring.Features.HandleAvailabilityAlert
{
    public class HttpTrigger
    {
        private readonly ILogger _logger;
        private readonly ISlackApi _slackApi;

        public HttpTrigger(ILogger<HttpTrigger> logger, ISlackApi slackApi)
        {
            _logger = logger;
            _slackApi = slackApi;
        }

        [Function("HandleAvailabilityAlert")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "availability")] HttpRequestData req)
        {
            var alert = await req.MapTo<AvailabilityAlert>();

            await _slackApi.Send($":warning: New Availability alert for `{alert!.GetWebTestName()}`");

            return req.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
