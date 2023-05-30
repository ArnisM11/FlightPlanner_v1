using AutoMapper;
using FlightPlanner.Controllers;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;
        
        public CustomerApiController(IFlightPlannerDbContext context, IFlightService service, IMapper mapper, IEnumerable<IValidation> validators, IAirportService airportService)
        {
            _flightService = service;
            _mapper = mapper;
            _airportService = airportService;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var airport = _airportService.SearchAirports(search);

            if (airport == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<AddAirportRequest>>(airport));
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight(FlightSearch request)
        {
            if (request.To == null || request.From == null || request.DepartureDate == null)
            {
                return BadRequest();
            }

            if (request.From == request.To)
            {
                return BadRequest();
            }

            return Ok(_airportService.SearchFlight(request));
        }

        [Route("flights/{id:int}")]
        [HttpGet]
        public IActionResult SearchFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            if (flight == null) return NotFound();
            return Ok(_mapper.Map<AddFlightRequest>(flight));
        }
    }
}
