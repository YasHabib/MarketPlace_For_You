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
        InAppNotificationVM WelcomeNotification(string firstname, DateTime sentDate);

        InAppNotificationVM Create1stOffer(DateTime sentDate);
    }
}
