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
        private readonly FlightStorage _flightStorage;
        public ClientApiController(FlightStorage flightStorage)
        {
            _flightStorage = flightStorage;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var searchResult = _flightStorage.FindAirport(search);
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

            var result = _flightStorage.SearchFlight(flightSearch);
            return Ok(result);
        }

        [Route("flights/{id:int}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightStorage.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
