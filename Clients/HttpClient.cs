namespace GuaranteedRate.Net.RequestStitching.Clients
{
    using System.Collections.Specialized;
    using MessageHandlers;

    public class HttpClient : Http.HttpService.HttpClient
    {
        public HttpClient(string baseUrl = null, NameValueCollection defaultRequestHeaders = null)
            : base(baseUrl, defaultRequestHeaders)
        {
            var sessionId = RequestContext.SessionId;
            var requestId = RequestContext.RequestId;
            if (sessionId != null)
                DefaultRequestHeaders.Add("X-Session-Id", sessionId);
            if (requestId != null)
                DefaultRequestHeaders.Add("X-Request-Id", requestId);
        }
    }
}