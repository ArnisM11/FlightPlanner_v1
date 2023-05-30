using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using FlightPlanner.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    public class AdminApiValidator 
    {

        private static readonly object lockObject = new object();

        public static bool HasInvalidValues(Flight flight)
        {
            if (string.IsNullOrWhiteSpace(flight.From?.Country) ||
                string.IsNullOrWhiteSpace(flight.From?.City) ||
                string.IsNullOrWhiteSpace(flight.From?.AirportCode) ||
                string.IsNullOrWhiteSpace(flight.To?.Country) ||
                string.IsNullOrWhiteSpace(flight.To?.City) ||
                string.IsNullOrWhiteSpace(flight.To?.AirportCode) ||
                string.IsNullOrWhiteSpace(flight.Carrier) ||
                string.IsNullOrWhiteSpace(flight.DepartureTime) ||
                string.IsNullOrWhiteSpace(flight.ArrivalTime))
            {
                return true;
            }

            return false;
        }

        public static bool IsSameAirport(Flight flight)
        {
            return flight.From.AirportCode.ToUpper().Trim() == flight.To.AirportCode.ToUpper().Trim();
        }

        public static bool IsWrongDate(Flight flight)
        {
            if (!DateTime.TryParse(flight.DepartureTime, out DateTime departureTime) ||
                !DateTime.TryParse(flight.ArrivalTime, out DateTime arrivalTime))
            {
                throw new ArgumentException("Invalid departure or arrival time! ");
            }

            if (departureTime >= arrivalTime)
            {
                return true;
            }

            return false;
        }
    }
}
