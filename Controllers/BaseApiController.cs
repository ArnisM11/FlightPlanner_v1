using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected FlightPlannerDbContext _context;

        protected BaseApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }
    }
}
