using AirlineTicketingSystem.Areas.Identity.Data;
using AirlineTicketingSystem.Models;
using AirlineTicketingSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AirlineTicketingSystem.Controllers
{
    public class MilesSmilesController : Controller
    {
        private readonly UserManager<AirlineTicketingSystemUser> _userManager;
        private readonly MilesSmilesService _milesSmilesService;
        private readonly IEmailService _emailService;

        public MilesSmilesController(UserManager<AirlineTicketingSystemUser> userManager, MilesSmilesService milesSmilesService, IEmailService emailService)
        {
            _userManager = userManager;
            _milesSmilesService = milesSmilesService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MilesSmilesRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    await _milesSmilesService.RegisterMilesSmilesAccount(userId, model);
                    var message = $@"Welcome to the MilesSmiles program! We are excited to have you with us.";
                    var user = await _userManager.GetUserAsync(User);
                    var userEmail = user.Email;

                    await _emailService.SendEmailAsync(userEmail, "New Registration", message);
                    return RedirectToAction("Index", "Home");
                }
                catch (ApplicationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
           

            return View(model);
        }

        [HttpGet]
        public IActionResult AddMilesToAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMilesToAccount(AddMilesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _milesSmilesService.AddMilesToAccount(model.AccountId, model.Miles);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Failed to add miles: {ex.Message}");
                }
            }
            return View("AddMiles", model);
        }


    }
}
