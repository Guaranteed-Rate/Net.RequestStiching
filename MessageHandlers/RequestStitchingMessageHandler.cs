namespace GuaranteedRate.Net.RequestStitching.MessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;

    public class RequestStitchingMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// Extracts X-Session-Id and X-Request-Id from request header and stores them in the current HTTP context. 
        /// Adds X-Session-Id and X-Request-Id to the response header.
        /// </summary>
        /// <remarks>Use RequestStitchingContext.SessionId and RequestStitchingContext.SessionId to retrieve the values.</remarks>
        /// <param name="generateIfMissing">Sends a Bad Request response if X-Request-Id and X-Session-Id are not include in the request header</param>
        public RequestStitchingMessageHandler(bool generateIfMissing)
        {
            _generateIfMissing = generateIfMissing;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestId = StitchRequestHeader(request, RequestHeader.RequestId);
            var sessionId = StitchRequestHeader(request, RequestHeader.SessionId);

            if (_errors.Count > 0)
            {
                var message = new ObjectContent(typeof (string[]), _errors, new JsonMediaTypeFormatter());
                return new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest, Content = message};
            }

            var response = await base.SendAsync(request, cancellationToken);

            SetResponseHeader(response, RequestHeader.RequestId, requestId);
            SetResponseHeader(response, RequestHeader.SessionId, sessionId);

            return response;
        }

        private readonly bool _generateIfMissing;

        private readonly List<string> _errors = new List<string>();

        private string StitchRequestHeader(HttpRequestMessage request, RequestHeader requestHeader)
        {
            string value = null;
            var name = RequestContext.RequestHeaders[requestHeader];

            if (request.Headers.Contains(name))
                value = request.Headers.GetValues(name).First();
            else if (_generateIfMissing)
                value = Guid.NewGuid().ToString();
            else
                _errors.Add(string.Format("{0} is missing from the request", name));

            RequestContext.Set(requestHeader, value);

            return value;
        }

        private void SetResponseHeader(HttpResponseMessage response, RequestHeader requestHeader, string value)
        {
            var name = RequestContext.RequestHeaders[requestHeader];
            response.Headers.TryAddWithoutValidation(name, value);
        }
    }
}