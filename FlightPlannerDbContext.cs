using System.Collections.Generic;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;
using FlightPlanner.Storage;

namespace FlightPlanner
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext(DbContextOptions options) : base(options)
        {
        }
        
        
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }

        
    }
}
