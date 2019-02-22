using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SelectiveHandlerInvocation.Controllers
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

            var request1 = new HttpRequestMessage(HttpMethod.Get, uri);
            request1.SetNoValidate(); // explicitly skip validation

            var request2 = new HttpRequestMessage(HttpMethod.Get, uri); // missing required header

            var request3 = new HttpRequestMessage(HttpMethod.Get, uri); 
            request3.Headers.Add("required-header", "this-should-succeed"); // includes required header

            var response1 = await client.SendAsync(request1); // should be 200 because validation is skipped
            var response2 = await client.SendAsync(request2); // should be 400 because header is missing and validation is NOT skipped
            var response3 = await client.SendAsync(request3); // should be 200 because required header is present

            return Ok();
        }
    }
}
