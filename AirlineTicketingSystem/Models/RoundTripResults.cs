namespace AirlineTicketingSystem.Models
{
    public class RoundTripResults
    {
        public IEnumerable<Flight> DepartureFlights { get; set; }
        public IEnumerable<Flight> ReturnFlights { get; set; }
    }
}
