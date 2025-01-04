using System.Net.Mail;
using System.Net;

namespace KartingSystemSimulation.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration; // Inject configuration
            _logger = logger; // Inject logger
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("managekartingsyssim@gmail.com", "ldbp keic yimc uttq"), // App-specific password
                    EnableSsl = true
                };

                var message = new MailMessage
                {
                    From = new MailAddress("managekartingsyssim@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Enable HTML rendering
                };

                message.To.Add(toEmail);

                await client.SendMailAsync(message); // Send email asynchronously
                _logger.LogInformation($"Email successfully sent to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email to {toEmail}");
                throw new InvalidOperationException("Failed to send email. See inner exception for details.", ex);
            }
        }
    }
}
