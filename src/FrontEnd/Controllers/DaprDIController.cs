using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaprDIController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        public DaprDIController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [HttpGet()]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "backend", "WeatherForecast");
            return Ok(result);
        }
    }
}
