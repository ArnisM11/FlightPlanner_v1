using FlightPlanner.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupController : BaseApiController
    {
        public CleanupController(IFlightPlannerDbContext context) : base(context)
        {
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
            return Ok();
        }
    }
}
