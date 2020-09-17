using System;
using System.Net.Http;

namespace IranPost.Net.AspNet
{
    public class IranPostConfiguration
    {
        public IranPostConnectionProtocol ConnectionProtocol { get; set; } = IranPostConnectionProtocol.Http;
        
        internal RetryBuilder RetryBuilder { get; private set; }

        public TimeSpan Timeout { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public Func<IServiceProvider, HttpClient> HttpClientFactory { get; set; }
        public Uri RemoteServiceUri { get; set; }


        public RetryBuilder AddRetry()
        {
            RetryBuilder = new RetryBuilder();
            return RetryBuilder;
        }
    }
}