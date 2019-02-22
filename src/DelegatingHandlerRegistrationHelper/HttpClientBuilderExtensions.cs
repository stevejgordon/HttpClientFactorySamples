using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DelegatingHandlerRegistrationHelper
{
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder RegisterAndAddHttpMessageHandler<T>(this IHttpClientBuilder builder)
            where T : DelegatingHandler
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.TryAddTransient<T>();

            if (!builder.Services.Any(sd => sd.ServiceType == typeof(T) && sd.Lifetime == ServiceLifetime.Transient))
            {
                throw new InvalidOperationException($"An IServiceCollection registration for '{typeof(T).Name}' with the required transient service lifetime was not found and cannot be added.");
            }

            builder.AddHttpMessageHandler<T>();

            return builder;
        }
    }
}
