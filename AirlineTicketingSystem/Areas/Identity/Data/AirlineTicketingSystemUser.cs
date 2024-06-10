using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineTicketingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace AirlineTicketingSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AirlineTicketingSystemUser class
public class AirlineTicketingSystemUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsAdmin { get; set; }
    public int UserMiles { get; set; }
    public MilesSmilesAccount? MilesSmilesAccount { get; set; }
}

