using AirlineTicketingSystem.Models;
using AirlineTicketingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AirlineTicketingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;
        private readonly BookingService _bookingService;

        public BookingController(ILogger<BookingController> logger, BookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult SearchFlights()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchFlights(FlightSearchModel searchModel)
        {
            if (searchModel.TripType == "RoundTrip")
            {
                if (searchModel.ReturnDate == null)
                {
                    ModelState.AddModelError("ReturnDate", "Return date is required for round trip.");
                    return View(searchModel);
                }

                var departureFlights = await _bookingService.SearchFlights(searchModel.Origin, searchModel.Destination, searchModel.Date, searchModel.Capacity);
                var returnFlights = await _bookingService.SearchFlights(searchModel.Destination, searchModel.Origin, searchModel.ReturnDate.Value, searchModel.Capacity);

                var roundTripResults = new RoundTripResults
                {
                    DepartureFlights = departureFlights,
                    ReturnFlights = returnFlights
                };

                ViewBag.NumberOfPassengers = searchModel.Capacity;
                return View("RoundTripFlightResults", roundTripResults);
            }
            else
            {
                var flights = await _bookingService.SearchFlights(searchModel.Origin, searchModel.Destination, searchModel.Date, searchModel.Capacity);
                ViewBag.NumberOfPassengers = searchModel.Capacity;
                return View("FlightResults", flights);
            }

        }

        [HttpPost]
        public async Task<IActionResult> BookFlight(int flightId, int numberOfPassengers)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _bookingService.BookFlight(userId, flightId, numberOfPassengers);
                return RedirectToAction("Index", "Home");
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Failed to book flight.");
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> BookRoundTripFlights(int departureFlightId, int returnFlightId, int numberOfPassengers)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _bookingService.BookFlight(userId, departureFlightId, numberOfPassengers);
                await _bookingService.BookFlight(userId, returnFlightId, numberOfPassengers);
                return RedirectToAction("Index", "Home");
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Failed to book flight.");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
