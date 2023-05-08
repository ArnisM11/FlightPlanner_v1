using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{

    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
        [HttpPut]
        [Route("flights")]

        public IActionResult AddFlight(Flight flight)
        {
            FlightStorage.AddFlight(flight);
            return Created("",flight);
        }


    }
}