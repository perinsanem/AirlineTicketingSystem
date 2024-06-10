using AirlineTicketingSystem.Areas.Identity.Data;
using AirlineTicketingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketingSystem.Services
{
    public class BookingService
    {
        private readonly UserManager<AirlineTicketingSystemUser> _userManager;
        private readonly AirlineTicketingDbContext _context;

        public BookingService(UserManager<AirlineTicketingSystemUser> userManager,AirlineTicketingDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<Flight>> SearchFlights(string origin, string destination, DateTime date, int capacity)
        {
            return await _context.Flights
                .Where(f => f.Origin == origin && f.Destination == destination && f.Date.Date == date.Date && f.Capacity >= capacity)
                .ToListAsync();
        }


        public async Task BookFlight(string userId, int flightId, int numberOfPassengers)
        {
            var user = await _userManager.Users.Include(u => u.MilesSmilesAccount).FirstOrDefaultAsync(u => u.Id == userId);
            var flight = await _context.Flights.FindAsync(flightId);

            if (user != null && flight != null && flight.Capacity >= numberOfPassengers)
            {
                var booking = new Booking
                {
                    FlightId = flightId,
                    UserId = userId,
                    NumberOfPassengers = numberOfPassengers,
                    BookingDate = DateTime.Now
                };

                // Adjust capacity
                flight.Capacity -= numberOfPassengers;
                _context.Update(flight);

                if (user.MilesSmilesAccount != null)
                {
                    // User is a MilesSmiles member
                    booking.MilesSmilesAccountId = user.MilesSmilesAccount.AccountId;
                }

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ApplicationException("User or flight not found, or not enough capacity.");
            }
        }
    }
}
