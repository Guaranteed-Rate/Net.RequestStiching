## Installing

1. [Install nuget package](https://www.nuget.org/packages/GuaranteedRate.Net.RequestStitching.MessageHandlers/):

		PM> Install-Package GuaranteedRate.Net.RequestStitching.MessageHandlers

## Configuration

1. Add to your WebApiConfig registration, the only constructor parameter is whether or not auto generate missing ids. Use whatever suits your needs:

		public static void Register(HttpConfiguration config)
		{
			...
		
			config.MessageHandlers.Add(new RequestStitchingMessageHandler(true));
		
			...
		}


## Usage

1. Retrieve SessionId and RequestId from the Request Stitchin RequestContext:

		string requestId = GuaranteedRate.Net.RequestStitching.MessageHandlers.RequestContext.RequestId;
		string sessionId = GuaranteedRate.Net.RequestStitching.MessageHandlers.RequestContext.SessionId

