namespace Clients
{
    using System.Collections.Specialized;
    using GuaranteedRate.Net.RequestStitching.MessageHandlers;

    public class HttpClient : GuaranteedRate.Net.Http.HttpService.HttpClient
    {
        public HttpClient(string baseUrl, NameValueCollection defaultRequestHeaders = null)
            : base(baseUrl, defaultRequestHeaders)
        {
            var sessionId = RequestContext.SessionId;
            var requestId = RequestContext.RequestId;
            DefaultRequestHeaders.Add("X-Session-Id", sessionId);
            DefaultRequestHeaders.Add("X-Request-Id", requestId);
        }
    }
}