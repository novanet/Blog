using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Monitoring.ApplicationInsights.Logs;
using Slack;

namespace Monitoring.Features.HandleError
{
    public class HttpTrigger
    {
        private readonly ISlackApi _slackApi;

        public HttpTrigger(ISlackApi slackApi) => _slackApi = slackApi;

        [Function("HandleError")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "errors")] HttpRequestData req)
        {
            var alert = await req.MapTo<LogAlert>();

            await _slackApi.Send($":no_entry: *{alert!.GetCloudRoleName()}*: <{alert!.GetLinkToSearchResults()}|{alert!.GetTracesMessage()}>");

            return req.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
