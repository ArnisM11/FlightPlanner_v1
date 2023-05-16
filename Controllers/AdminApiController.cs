using System;
using System.Linq;
using FlightPlanner.Controllers;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("admin-api/flights")]
    [ApiController]
    [Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightStorage _flightStorage;

        public AdminApiController(FlightStorage flightStorage) 
        {
            _flightStorage = flightStorage;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightStorage.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
        [HttpPut]

        public IActionResult AddFlight(Flight flightToAdd)
        {
            if (AdminApiValidator.HasInvalidValues(flightToAdd) || AdminApiValidator.IsSameAirport(flightToAdd) || AdminApiValidator.IsWrongDate(flightToAdd))
            {
                return BadRequest();
            }

            
            if (_flightStorage.FlightAlreadyExists(flightToAdd))
            {
                return Conflict();
            }

            var flight = _flightStorage.AddFlight(flightToAdd);
            
            return Created("", flight);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _flightStorage.GetFlight(id);

            if (flight == null)
            {
                return Ok();
            }

            _flightStorage.DeleteFlight(id);
            return Ok();
        }

    }
}