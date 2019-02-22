using System;
using System.Net.Http;

namespace PerRequestPolicyFiltering
{
    public static class HttpRequestMessageExtensions
    {
        private const string RetryExtraTimes = "RetryExtraTimes";

        public static void SetCustomRetries(this HttpRequestMessage request, int retries)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (retries < 0)
                throw new ArgumentOutOfRangeException(nameof(retries));

            request.Properties[RetryExtraTimes] = retries;
        }

        public static int ExtraRetries(this HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return request.Properties.TryGetValue(RetryExtraTimes, out var value) && value is int extraRetries
                ? extraRetries
                : -1; // will be the case if the property has not been added
        }
    }
}
