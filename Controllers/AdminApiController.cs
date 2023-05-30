using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Controllers;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Data;
using FlightPlanner.Models;
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
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidation> _validators;
        private static readonly object _lock = new object();
        public AdminApiController(IFlightPlannerDbContext context, IFlightService service, IMapper mapper, IEnumerable<IValidation> validators)
        {
            _flightService = service;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AddFlightRequest>(flight));
        }
        [HttpPut]

        public IActionResult AddFlight(AddFlightRequest flightToAdd)
        {
            
            var flight = _mapper.Map<Flight>(flightToAdd);
            lock (_lock)
            {
                if (!_validators.All(v => v.IsValid(flight)))
                {
                    return BadRequest();
                }

                if (_flightService.FlightExists(flight))
                {
                    return Conflict();
                }

                _flightService.Create(flight);
            }

            return Created("",_mapper.Map<AddFlightRequest>(flight));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteFlight(int id)
        {

            var flight = _flightService.GetFullFlightById(id);
            if (flight != null)
            {
                _flightService.Delete<Flight>(flight);
            }

            return Ok();
        }

    }
}