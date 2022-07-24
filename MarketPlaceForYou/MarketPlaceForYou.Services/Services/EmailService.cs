using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class EmailService:IEmailService
    {
        private readonly IUnitOfWork _uow;

        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration, IUnitOfWork uow)
        {
            _configuration = configuration;
            _uow = uow;
        }   

        public async Task SendEmail(string toEmail)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@demo.com", "MarketForYou");
            var to = new EmailAddress(toEmail, "Example User");
            var subject = "Sending with SendGrid is Fun";
            var plainTextContent = "<p>Now buy something...NOW" + DateTime.Now + "</p><p>Did you buuy anything yet?</p>";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
