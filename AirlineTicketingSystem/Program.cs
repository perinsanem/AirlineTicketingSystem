using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AirlineTicketingSystem.Areas.Identity.Data;
using AirlineTicketingSystem.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AirlineTicketingDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AirlineTicketingDbContextConnection' not found.");

builder.Services.AddDbContext<AirlineTicketingDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AirlineTicketingSystemUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AirlineTicketingDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<FlightService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<MilesSmilesService>();
builder.Services.AddHostedService<ScheduledTasksService>();
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();


app.Run();
