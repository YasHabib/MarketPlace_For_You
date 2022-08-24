using MarketPlaceForYou.Models.ViewModels.User;
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
        //private readonly IUnitOfWork _uow;

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration, IUnitOfWork uow)
        {
            _configuration = configuration;
            //_uow = uow;
        }   

        public async Task WelcomeEmail(string email)
        {
            var apiKey = _configuration.GetValue<string>("SendGridAPIKey"); //gives back a 200 but no welcome email.
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("yasin+mktfy@vogcalgaryappdeveloper.com", "Market For You");
            var subject = "Welcome to Market For You";
            var to = new EmailAddress(email);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
