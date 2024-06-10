using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
public interface IEmailService 
{ 
    Task SendEmailAsync(string toEmail, string subject, string message); }
public class EmailService : IEmailService 
{ private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration) { _configuration = configuration; }
    public async Task SendEmailAsync(string toEmail, string subject, string message) { var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
        using (var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port)) { client.Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password);
            client.EnableSsl = smtpSettings.EnableSsl; var mailMessage = new MailMessage { From = new MailAddress(smtpSettings.UserName), Subject = subject, Body = message, IsBodyHtml = true, };
            mailMessage.To.Add(toEmail); await client.SendMailAsync(mailMessage); } } }
public class SmtpSettings { 
    public string Host { get; set; } 
    public int Port { get; set; } 
    public bool EnableSsl { get; set; } 
    public string UserName { get; set; } 
    public string Password { get; set; } 
}