using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace CodePracticeTrackingApp.Utilities
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }
        public EmailSender(IConfiguration _config) => SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        //public EmailSender(IConfiguration _config) => SendGridSecret = _config["SendGridSecret"];
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // logic to send email
            //return Task.CompletedTask;

            var client = new SendGridClient(SendGridSecret);

            var from = new EmailAddress("tngo0508@gmail.com", "CodeTrack");
            var to = new EmailAddress(email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            return client.SendEmailAsync(message);
        }
    }
}
