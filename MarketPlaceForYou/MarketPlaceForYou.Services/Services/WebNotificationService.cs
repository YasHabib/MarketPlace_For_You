using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Amazon.S3.Util.S3EventNotification;

namespace MarketPlaceForYou.Services.Services
{
    public class WebNotificationService : IWebNotificationService
    {
        private readonly IUnitOfWork _uow;

        public WebNotificationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> PendingListingCount(string userId)
        {
            var pending = await _uow.Listings.GetAll(items => items.Where(items => items.UserId == userId && items.Status == "Pending"));
            var count = pending.Count();
            return count;
        }

        public InAppNotificationVM WelcomeNotification(string firstname, DateTime sentDate)
        {
            string welcome = "Hey " + firstname + ", welcome to MKTFY";
            DateTime sent = sentDate.Date;
            var model = new InAppNotificationVM(welcome, sent);
            return model;
        }

        public InAppNotificationVM Create1stOffer(DateTime sentDate)
        {
            string welcome = "Let's create your 1st offer!";
            DateTime sent = sentDate.Date;
            var model = new InAppNotificationVM(welcome, sent);
            return model;
        }
    }
}
