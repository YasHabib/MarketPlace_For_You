using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IWebNotificationService
    {
        Task<int> PendingListingCount(string userId);
        //Task<int> ActiveListingCount();
        Task<InAppNotificationVM> WelcomeNotification(string firstname);

        Task<InAppNotificationVM> Create1stOffer(string userId);

        void AddNotifications(string userId);
    }
}
