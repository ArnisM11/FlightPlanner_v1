using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected FlightPlannerDbContext _context;
        protected FlightStorage _flightStorage;

        protected BaseApiController(FlightStorage flightStorage,FlightPlannerDbContext context )
        {
            _flightStorage = flightStorage;
            _context = context;
        }
    }
}
