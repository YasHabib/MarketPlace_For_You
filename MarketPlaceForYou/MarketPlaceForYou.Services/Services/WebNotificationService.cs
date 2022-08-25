using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class WebNotificationService : IWebNotificationService
    {
        private readonly IUnitOfWork _uow;

        public WebNotificationService(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
        }

        public async Task<int> PendingListingCount(string userId)
        {
            var pending = await _uow.Listings.GetAll(items => items.Where(items => items.UserId == userId && items.Status == "Pending"));
            var count = pending.Count();
            return count;
        }
    }
}
