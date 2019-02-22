using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SelectiveHandlerInvocation
{
    public class ValidationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.RequiresValidation() && !request.Headers.Contains("required-header"))
            {
                // validation of a required header has failed so return a bad request

                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("The required-header is missing")
                };

                return Task.FromResult(response);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
