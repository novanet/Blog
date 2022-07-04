using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Monitoring.ApplicationInsights.Metrics;
using Slack;

namespace Monitoring.Features.HandleDlqAlert
{
    public class HttpTrigger
    {
        private readonly ISlackApi _slackApi;

        public HttpTrigger(ISlackApi slackApi) => _slackApi = slackApi;

        [Function("HandleDlqAlert")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "dlq")] HttpRequestData req)
        {
            var alert = await req.MapTo<MetricAlert>();

            await _slackApi.Send($":radioactive_sign: New messages in DLQ for `{alert!.GetDlqEntityName()}`");

            return req.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
