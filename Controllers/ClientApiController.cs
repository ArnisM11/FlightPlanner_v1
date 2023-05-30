using FlightPlanner.Controllers;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientApiController : BaseApiController
    {
        public ClientApiController(IFlightPlannerDbContext context):base(context) { }

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            //var searchResult = _flightStorage.FindAirport(search);
            return Ok();
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight()
        {
            
            return Ok();
        }

        [Route("flights/{id:int}")]
        [HttpGet]
        public IActionResult GetFlight()
        {
            return Ok();
        }
    }
}
