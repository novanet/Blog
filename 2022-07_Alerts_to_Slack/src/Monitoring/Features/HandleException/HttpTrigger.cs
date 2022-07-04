using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Monitoring.ApplicationInsights.Logs;
using Slack;

namespace Monitoring.Features.HandleException
{
    public class HttpTrigger
    {
        private readonly ISlackApi _slackApi;

        public HttpTrigger(ISlackApi slackApi) => _slackApi = slackApi;

        [Function("HandleException")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "exceptions")] HttpRequestData req)
        {
            var alert = await req.MapTo<LogAlert>();

            await _slackApi.Send($":no_entry: *{alert!.GetCloudRoleName()}*: <{alert!.GetLinkToSearchResults()}|{alert!.GetExceptionMessage()}>");

            return req.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
