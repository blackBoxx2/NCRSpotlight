using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace NCRSPOTLIGHT.Utilities
{
    public class NCRSpotlightEmailer : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public NCRSpotlightEmailer(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient
            {
                Host = _smtpSettings.Host,
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential("resend", _smtpSettings.Password),
                EnableSsl = true
            };

            var message = new MailMessage(_smtpSettings.Username, email, subject, htmlMessage)
            {
                IsBodyHtml = true
            };

            return client.SendMailAsync(message);

        }
    }
}
