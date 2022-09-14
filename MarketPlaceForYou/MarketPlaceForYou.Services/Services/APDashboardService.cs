using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class APDashboardService : IAPDashboardService
    {
        private readonly IUnitOfWork _uow;

        public APDashboardService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //returns total active users registered in this system
        public async Task<int> TotalUsers()
        {
            var allUsers = await _uow.Users.GetAll(users => users.Where(users => users.IsBlocked == false));
            var count = allUsers.Count();
            return count;
        }

        // returns total numebr of listings that are currently active
        public async Task<int> TotalListings()
        {
            var allListings = await _uow.Listings.GetAll(items => items.Where(items => items.IsDeleted == false && items.Status == "Active"));
            var count = allListings.Count();
            return count;
        }

        //Getting all the users which has been created within the last 30 days from the current date.
        public async Task<int> NewUsers()
        {
            var days = DateTime.Today.AddDays(-30);
            var allUsers = await _uow.Users.GetAll(users => users.Where(users => users.IsBlocked == false));
            var newUsers = allUsers.Select(users => users.Created <= days);
            var count = newUsers.Count();
            return count;
        }

        public async Task<decimal> PerDayAvgSalesInYear()
        {
            var dtAnnual = DateTime.Today.AddDays(-365);

            //Grabbing all the listings created within a year from todays's date 
            var soldListings = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Sold" && items.Created <= dtAnnual));
            var totalAvgSold = soldListings.Average(items => items.Price);
            return totalAvgSold;
        }
    }
}
