using System;
using System.Net.Http;

namespace SelectiveHandlerInvocation
{
    public static class HttpRequestMessageExtensions
    {
        private const string SkipValidationKey = "SkipValidation";

        public static void SetNoValidate(this HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            request.Properties[SkipValidationKey] = true;
        }

        public static bool RequiresValidation(this HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Properties.TryGetValue(SkipValidationKey, out var value) && value is bool shouldSkipValidation)
            {
                return !shouldSkipValidation;
            }

            return true;
        }
    }
}