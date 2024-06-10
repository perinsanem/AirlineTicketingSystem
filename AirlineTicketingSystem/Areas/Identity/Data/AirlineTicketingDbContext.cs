using AirlineTicketingSystem.Areas.Identity.Data;
using AirlineTicketingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketingSystem.Areas.Identity.Data;

public class AirlineTicketingDbContext : IdentityDbContext<AirlineTicketingSystemUser>
{
    public AirlineTicketingDbContext(DbContextOptions<AirlineTicketingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<MilesSmilesAccount> MilesSmilesAccounts { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<MilesSmilesAccount>()
           .HasOne(m => m.User)
           .WithOne(u => u.MilesSmilesAccount)
           .HasForeignKey<MilesSmilesAccount>(m => m.UserId);
    }
}
