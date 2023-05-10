using System;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{

    [Route("admin-api/flights")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
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

        public IActionResult AddFlight(Flight flight)
        {
            if (AdminApiValidator.HasInvalidValues(flight) || AdminApiValidator.IsSameAirport(flight) || AdminApiValidator.IsWrongDate(flight))
            {
                return BadRequest();
            }

            if (AdminApiValidator.IsFlightInList(flight))
            {
               return Conflict();
            }

            return Created("", FlightStorage.AddFlight(flight));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }


    }
}