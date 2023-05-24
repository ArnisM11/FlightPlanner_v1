using FlightPlanner.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected IFlightPlannerDbContext _context;
        

        protected BaseApiController(IFlightPlannerDbContext context )
        {
            _context = context;
        }
    }
}
