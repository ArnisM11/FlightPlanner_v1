using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupController : ControllerBase
    {
        private readonly IDbService _dbService;
        public CleanupController(IDbService dbService) 
        {
            _dbService = dbService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _dbService.DeleteAll();
            return Ok();
        }
    }
}
