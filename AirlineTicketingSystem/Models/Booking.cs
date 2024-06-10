namespace AirlineTicketingSystem.Models
{
        public class Booking
        {
            public int BookingId { get; set; }
            public int FlightId { get; set; }
            public string? UserId { get; set; }
            public int NumberOfPassengers { get; set; }
            public DateTime BookingDate { get; set; }
           public int? MilesSmilesAccountId { get; set; }
           public Flight? Flight { get; set; }
        }
    
}
