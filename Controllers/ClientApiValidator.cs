using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    public static class ClientApiValidator
    {
        public static bool HasInvalidValues(FlightSearch flightSearch)
        {
            if (flightSearch.From == null ||
                flightSearch.To == null ||
                flightSearch.DepartureDate == null)
                return true;
            return false;
        }

        public static bool IsSameAirport(FlightSearch flightSearch)
        {
            return flightSearch.From == flightSearch.To;
        }
    }
}
