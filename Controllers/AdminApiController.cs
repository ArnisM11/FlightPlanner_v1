using System;
using System.Linq;
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
    public class AdminApiController : BaseApiController
    {
        public AdminApiController(FlightPlannerDbContext context) : base(context){ }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
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

            if (AdminApiValidator.IsFlightInList(flight, _context))
            {
               return Conflict();
            }

            _context.Flights.Add(flight);
            _context.SaveChanges();

            return Created("", flight);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _context.Flights.SingleOrDefault(f => f.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Remove(flight);
            _context.SaveChanges();

            return Ok();
        }

    }
}