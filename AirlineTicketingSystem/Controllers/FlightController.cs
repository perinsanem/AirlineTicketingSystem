using AirlineTicketingSystem.Models;
using AirlineTicketingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineTicketingSystem.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public IActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(Flight flight)
        {
            if (ModelState.IsValid)
            {
                await _flightService.AddFlight(flight);
                return RedirectToAction("Index", "Home");
            }
            return View(flight);
        }
    }
}
