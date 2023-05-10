using FlightPlanner.Models;
using FlightPlanner.Storage;
using FlightPlanner.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientApiController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var searchResult = FlightStorage.FindAirport(search);
            return Ok(searchResult);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight(FlightSearch flightSearch)
        {
            if (ClientApiValidator.HasInvalidValues(flightSearch) ||
                ClientApiValidator.IsSameAirport(flightSearch))
            {
                return BadRequest();
            }

            var result = FlightStorage.SearchFlight(flightSearch);
            return Ok(result);
        }

        [Route("flights/{id:int}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
