namespace RequestStitching.MessageHandlers
{
    using System.Collections.Generic;
    using System.Web;

    public static class RequestStitchingContext
    {
        private static string Get(RequestHeader requestHeader)
        {
            var name = RequestHeaders[requestHeader];
            var value = HttpContext.Current.Items[name];
            return value != null ? value.ToString() : null;
        }

        public static string RequestId
        {
            get { return Get(RequestHeader.RequestId); }
        }

        public static string SessionId
        {
            get { return Get(RequestHeader.SessionId); }
        }

        internal static void Set(RequestHeader requestHeader, string value)
        {
            var name = RequestHeaders[requestHeader];
            HttpContext.Current.Items[name] = value;
        }

        internal static readonly IReadOnlyDictionary<RequestHeader, string> RequestHeaders = new Dictionary<RequestHeader, string>
        {
            {RequestHeader.RequestId, "X-Request-Id"},
            {RequestHeader.SessionId, "X-Session-Id"}
        };
    }
}