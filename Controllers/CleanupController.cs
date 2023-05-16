using FlightPlanner.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupController : ControllerBase
    {
        private readonly FlightStorage _flightStorage;
        public CleanupController(FlightStorage flightStorage)
        {
            _flightStorage = flightStorage;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flightStorage.Clear();
            return Ok();
        }
    }
}
