using System.Collections.Generic;
using System.Net.Http;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaprController : ControllerBase
    {
        public DaprController()
        {
        }

        // 通过HttpClient调用BackEnd
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            using var httpClient = DaprClient.CreateInvokeHttpClient();
            var result = await httpClient.GetAsync("http://backend/WeatherForecast");
            var resultContent = $"result is {result.StatusCode} {await result.Content.ReadAsStringAsync()}";
            return Ok(resultContent);
        }

        // 通过DaprClient调用BackEnd
        [HttpGet("get2")]
        public async Task<ActionResult> Get2Async()
        {
            using var daprClient = new DaprClientBuilder().Build();
            var result = await daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "backend", "WeatherForecast");
            return Ok(result);
        }
    }
}
