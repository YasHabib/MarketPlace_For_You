using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.FAQ;
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

        public async Task<InAppNotificationVM> WelcomeNotification(string notificationId)
        {
            var notification = await _uow.Notifications.GetById(notificationId);

            notification.SentDate = DateTime.UtcNow;
            _uow.Notifications.Update(notification);
            await _uow.SaveAsync();

            var model = new InAppNotificationVM(notification);
            return model;
        }

        public async Task<InAppNotificationVM> Create1stListing(string notificationId)
        {
            var notification = await _uow.Notifications.GetById(notificationId);

            notification.SentDate = DateTime.UtcNow;
            _uow.Notifications.Update(notification);
            await _uow.SaveAsync();

            var model = new InAppNotificationVM(notification);
            return model;
        }

        //Method to mark all notifications when the user will click the bell icon
        public async Task MarkAsRead()
        {
            var notifications = await _uow.Notifications.GetAll();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                _uow.Notifications.Update(notification);
            }
            await _uow.SaveAsync();
        }
    }
}
