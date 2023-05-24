using System;
using System.Linq;
using AutoMapper;
using FlightPlanner.Controllers;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
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
    public class AdminApiController : BaseApiController
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        public AdminApiController(IFlightPlannerDbContext context, IFlightService service, IMapper mapper) : base(context)
        {
            _flightService = service;
            _mapper = mapper;
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
            _flightService.Create(flight);
            return Created("",_mapper.Map<AddFlightRequest>(flight));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            
            return Ok();
        }

    }
}