using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DelegatingHandlerRegistrationHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = _httpClientFactory.CreateClient("TestClient");

            var uri = new Uri("http://www.example.com");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            
            var response = await client.SendAsync(request);

            var hasProperty = response.RequestMessage.Properties.ContainsKey("handler-added-property"); // should be true

            return Ok();
        }
    }
}
