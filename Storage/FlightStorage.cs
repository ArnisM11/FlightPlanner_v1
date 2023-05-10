using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FlightPlanner.Controllers;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Storage
{
    public static class FlightStorage
    {
        public static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;


        public static void Clear(){
            _flights.Clear();
            _id = 1;
        }
        public static Flight GetFlight(int id)
        {
            return _flights.SingleOrDefault(flight => flight.Id == id);
        }

        public static Flight AddFlight(Flight flight)
        {
            flight.Id = _id++;
            
            _flights.Add(flight);
            
            return flight;
        }

        public static bool DeleteFlight(int id){
            var flight = GetFlight(id);
            if (flight == null)
            {
                return false;
            }
            _flights.Remove(flight);
            return true;
        }
        public static List<Airport> FindAirport(string phrase)
        {
            var airports = new List<Airport>();
            var formattedPhrase = phrase.ToLower().Trim();

            foreach (var flight in _flights)
            {
                if (flight.From.City.ToLower().Contains(formattedPhrase) ||
                    flight.From.Country.ToLower().Contains(formattedPhrase) ||
                    flight.From.AirportCode.ToLower().Contains(formattedPhrase))
                {
                    airports.Add(flight.From);
                    return airports.ToList();
                }

                if (flight.To.City.ToLower().Contains(formattedPhrase) ||
                    flight.To.Country.ToLower().Contains(formattedPhrase) ||
                    flight.To.AirportCode.ToLower().Contains(formattedPhrase))
                {
                    airports.Add(flight.To);
                    return airports.ToList();
                }
            }

            return airports.ToList();
        }

        public static PageResult SearchFlight(FlightSearch search)
        {
            var result = new PageResult();
            var flights = _flights.Where(flight =>
                flight.DepartureTime.Contains(search.DepartureDate) &&
                flight.From.AirportCode.ToLower().Contains(search.From.ToLower().Trim()) &&
                flight.To.AirportCode.ToLower().Contains(search.To.ToLower().Trim())).ToList();
            result.Items = flights;
            result.TotalItems = flights.Count;
            return result;
        }
    }
}
