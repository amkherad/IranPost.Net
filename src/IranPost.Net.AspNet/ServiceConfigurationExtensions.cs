using System;
using System.Net.Http;
using IranPost.Net;
using IranPost.Net.AspNet;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddIranPost(
            this IServiceCollection serviceCollection,
            Action<IranPostConfiguration> config
        )
        {
            if (serviceCollection is null) throw new ArgumentNullException(nameof(serviceCollection));
            if (config is null) throw new ArgumentNullException(nameof(config));

            var settings = new IranPostConfiguration();

            config(settings);

            serviceCollection.AddSingleton(settings);

            serviceCollection.AddScoped<IIranPostClient>(IranPostClientFactory);
        }

        private static IIranPostClient IranPostClientFactory(
            IServiceProvider sp
        )
        {
            var config = sp.GetRequiredService<IranPostConfiguration>();
            if (config is null)
            {
                throw new InvalidOperationException();
            }


            var httpClient = config.HttpClientFactory?.Invoke(sp) ?? new HttpClient
            {
                Timeout = config.Timeout,
            };

            var retryBuilder = config.RetryBuilder;

            var protocol = config.ConnectionProtocol;

            switch (protocol)
            {
                case IranPostConnectionProtocol.Http:
                {
                    return new IranPostRestClient(
                        config.RemoteServiceUri ?? new Uri("http://gateway.post.ir/Gateway/"),
                        httpClient,
                        new AuthInfo
                        {
                            Username = config.Username,
                            Password = config.Password
                        },
                        retryBuilder.CreateHandler()
                    );
                }
                case IranPostConnectionProtocol.Soap:
                {
                    return new IranPostSoapClient(
                        new AuthInfo
                        {
                            Username = config.Username,
                            Password = config.Password
                        },
                        retryBuilder.CreateHandler()
                    );
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}