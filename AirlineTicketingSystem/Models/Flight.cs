namespace AirlineTicketingSystem.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public int FlightCode { get; set; }
        public int MilPoint { get; set; }

    }
}
