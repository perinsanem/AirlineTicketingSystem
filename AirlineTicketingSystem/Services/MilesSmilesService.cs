using AirlineTicketingSystem.Areas.Identity.Data;
using AirlineTicketingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace AirlineTicketingSystem.Services
{
    public class MilesSmilesService
    {
        private readonly UserManager<AirlineTicketingSystemUser> _userManager;
        private readonly AirlineTicketingDbContext _context;
        public MilesSmilesService(UserManager<AirlineTicketingSystemUser> userManager,AirlineTicketingDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task RegisterMilesSmilesAccount(string userId, MilesSmilesRegistrationViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var account = new MilesSmilesAccount
                {
                    UserId = user.Id,
                    Name = model.Name,
                    Surname = model.Surname,
                    BirthDate = model.BirthDate,
                };

                _context.MilesSmilesAccounts.Add(account);
                await _context.SaveChangesAsync();

                
            }
            else
            {
                throw new ApplicationException("User not found.");
            }
        }

        public async Task AddMilesToAccount(int accountId, int milesToAdd)
        {
            var account = await _context.MilesSmilesAccounts.FindAsync(accountId);
            if (account != null)
            {
                account.Miles += milesToAdd;
                _context.MilesSmilesAccounts.Update(account);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Account not found.");
            }
        }

    }
}
