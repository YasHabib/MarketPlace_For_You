using MarketPlaceForYou.Models.ViewModels.Listing;
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

        public async Task WelcomeEmail(string email, string firstName, string lastName)
        {
            var apiKey = _configuration.GetValue<string>("SendGridAPIKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("yasin+mktfy@vogcalgaryappdeveloper.com", "Market For You");
            var subject = "Welcome to Market For You";
            var fullName = firstName + " " + lastName;
            var to = new EmailAddress(email, fullName);
            var plainTextContent = "Welcome to Market for you, start by creating a listing and check out what others are selling!";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        //public async Task PendingListing(ListingPurchaseVM src)
        //{
        //    var apiKey = _configuration.GetValue<string>("SendGridAPIKey");
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("yasin+mktfy@vogcalgaryappdeveloper.com", "Market For You");
        //    var subject = "You have a pending listing";
        //    var to = new EmailAddress(email, sellerfullName);
        //    var plainTextContent = "Hello " + sellerfullName + ", You have a pending listing from ";
        //    var htmlContent = "<strong></strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}
    }
}
