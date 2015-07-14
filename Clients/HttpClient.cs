namespace GuaranteedRate.Net.RequestStitching.Clients
{
    using System.Collections.Specialized;
    using MessageHandlers;

    public class HttpClient : Http.HttpService.HttpClient
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