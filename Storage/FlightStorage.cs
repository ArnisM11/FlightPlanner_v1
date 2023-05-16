using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private readonly FlightPlannerDbContext _context;

        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        private static readonly object lockObject = new object();
        
        public void Clear(){
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }
        public Flight GetFlight(int id)
        {
            lock (lockObject)
            {
                return _context.Flights.Include(flight => flight.From)
                    .Include(flight => flight.To)
                    .SingleOrDefault(flight => flight.Id == id);
            }
        }

        public Flight AddFlight(Flight flightToAdd)
        {
            _context.Flights.Add(flightToAdd);
            _context.SaveChanges();
            return flightToAdd;
        }

        public bool DeleteFlight(int id){
            lock (lockObject)
            {
                var flight = _context.Flights.SingleOrDefault(f => f.Id == id);
                if (flight == null)
                {
                    return false;
                }

                _context.Remove(flight);
                _context.SaveChanges();
                return true;
            }
        }
        public List<Airport> FindAirport(string phrase)
        {
            var formattedPhrase = phrase.ToLower().Trim();

            var airports = _context.Airports
                .Where(airport => airport.City.ToLower().Contains(formattedPhrase) ||
                                  airport.Country.ToLower().Contains(formattedPhrase) ||
                                  airport.AirportCode.ToLower().Contains(formattedPhrase)).ToList(); 
            
            return airports;
        }

        public PageResult SearchFlight(FlightSearch search)
        {
            var result = new PageResult();
            var totalItems = 0;
            var items = new List<Flight>();
            var flight = _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .FirstOrDefault(flight => 
                    flight.DepartureTime.Contains(search.DepartureDate) &&
                    flight.From.AirportCode == search.From &&
                    flight.To.AirportCode == search.To);
            if (flight != null)
            {
                items.Add(flight);
                totalItems++;
            }
            result.Items = items;
            result.TotalItems = totalItems;
            return result;
        }

        public bool FlightAlreadyExists(Flight request)
        {
            return _context.Flights.Any(f => f.From.City == request.From.City &&
                                             f.From.Country == request.From.Country &&
                                             f.From.AirportCode == request.From.AirportCode &&
                                             f.To.City == request.To.City &&
                                             f.To.Country == request.To.Country &&
                                             f.To.AirportCode == request.To.AirportCode &&
                                             f.Carrier == request.Carrier &&
                                             f.ArrivalTime == request.ArrivalTime &&
                                             f.DepartureTime == request.DepartureTime);
        }
    }
}
