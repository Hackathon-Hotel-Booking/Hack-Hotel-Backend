using System.Net;
using System.Net.Mail;
using HotelAPI.Helpers;
using HotelAPI.Interfaces;

namespace HotelAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_emailSettings.FromEmail);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;

            using var smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(
                    _emailSettings.FromEmail,
                    _emailSettings.FromPassword
                ),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}