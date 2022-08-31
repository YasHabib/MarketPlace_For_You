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

        public async Task<InAppNotificationVM> WelcomeNotification(string userId)
        {
            var user = await _uow.Users.GetById(userId);
            string firstName = user.FirstName;
            string welcome = "Hey " + firstName + ", welcome to MKTFY";
            DateTime sent = user.Created.Date;
            var model = new InAppNotificationVM(welcome, sent);
            return model;
        }
        
        public async Task<InAppNotificationVM> Create1stOffer(string userId)
        {
            string welcome = "Let's create your 1st offer!";
            var user = await _uow.Users.GetById(userId);
            DateTime sent = user.Created.Date;
            var model = new InAppNotificationVM(welcome, sent);
            return model;
        }
    }
}
