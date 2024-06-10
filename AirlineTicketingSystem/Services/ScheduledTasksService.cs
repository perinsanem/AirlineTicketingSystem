using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;

public class ScheduledTasksService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ScheduledTasksService> _logger;
   

    public ScheduledTasksService(IServiceProvider serviceProvider, ILogger<ScheduledTasksService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            if (now.Hour == 0 && now.Minute == 0) // Run at midnight
            {
               
                await ProcessNewRegistrationsAsync();
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Check every minute
        }
    }





    private async Task ProcessNewRegistrationsAsync()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "new_registrations", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var registration = JsonConvert.DeserializeObject<Registration>(message);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    await emailService.SendEmailAsync(registration.Email, "Registration", $"Your registration: {registration.Message}");
                }
            };

            channel.BasicConsume(queue: "new_registrations", autoAck: true, consumer: consumer);
        }
    }
}

public class Registration
{
    public string Email { get; set; }
    public string Message { get; set; }
}