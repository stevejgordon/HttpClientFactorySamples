using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PerRequestPolicyFiltering.Controllers
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

            var uri = new Uri("https://httpstat.us/503"); // always returns 503

            var request1 = new HttpRequestMessage(HttpMethod.Get, uri);
            request1.SetCustomRetries(10); // will retry 10 times

            var request2 = new HttpRequestMessage(HttpMethod.Get, uri); // will retry the default 3 times
            
            await client.SendAsync(request1);
            await client.SendAsync(request2);

            return Ok();
        }
    }
}
