namespace AirlineTicketingSystem.Models
{
    public class FlightSearchModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Date { get; set; } // Departure Date
        public DateTime? ReturnDate { get; set; } // Optional Return Date
        public int Capacity { get; set; }
        public string TripType { get; set; } // "OneWay" or "RoundTrip"
    }
}
