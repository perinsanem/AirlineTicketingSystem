using AirlineTicketingSystem.Areas.Identity.Data;
using AirlineTicketingSystem.Models;

namespace AirlineTicketingSystem.Services
{
    public class FlightService
    {
        private readonly AirlineTicketingDbContext _context;
        public FlightService(AirlineTicketingDbContext context)
        {
            _context = context;
        }

        public async Task AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
        }
    }
}
