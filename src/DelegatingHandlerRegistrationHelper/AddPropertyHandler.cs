using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DelegatingHandlerRegistrationHelper
{
    public class AddPropertyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Properties.Add("handler-added-property", "true");

            return base.SendAsync(request, cancellationToken);
        }
    }
}