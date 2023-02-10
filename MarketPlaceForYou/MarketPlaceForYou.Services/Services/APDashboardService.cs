using MarketPlaceForYou.Models.ViewModels.APAnalytics;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Features;
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

        //Getting all the users which has been created today.
        public async Task<int> NewUsers()
        {
            var newUsers = await _uow.Users.GetAll(users => users.Where(users => users.IsBlocked == false && users.Created == DateTime.Today));
            //var newUsers = allUsers.Select(users => users.Created == DateTime.Today);
            var count = newUsers.Count();
            return count;
        }

        //Getting average sales per day from year-to-date
        public async Task<decimal> PerDayAvgSalesInYear()
        {
            var dtAnnual = DateTime.Today.AddYears(-1);

            //Grabbing all the listings created within a year from todays's date 
            var soldListings = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Sold" && items.Purchased >= dtAnnual));
            var totalAvgSold = soldListings.Average(items => items.Price);
            return totalAvgSold;
        }

        //Getting total sales per day over a month, and % increased from last month
        public async Task<SalesPerDayVM> SalesPerDayOverAMonth(DateTime StartDate, DateTime EndDate)
        {
            //var dates = new List<DateTime>();
            //var priceOfEachListings = new List<decimal>();
            var sum = new List<decimal>();
            //var startDate = DateTime.Today;
            //var endDate = startDate.AddMonths(-1);
            //TimeSpan span = startDate - endDate;
            var dates = Enumerable
              .Range(0, int.MaxValue)
              .Select(index => new DateTime?(StartDate.Date.AddDays(index)))
              .TakeWhile(date => date <= EndDate.Date)
              .ToList();


            //This loop will grab the sum of sold listings price per day within a month
            //Initially this will take all the sold listings for each day of the month, and then group them by their purchased date (not time) incase there are multiple listings sold that day.
            foreach (DateTime day in dates)
            {
                var soldListingsWitinAMonth = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Sold" && items.Purchased == day));
                var groupedByPurchaseDate = soldListingsWitinAMonth.GroupBy(items => items.Purchased.Date);

                foreach (var group in groupedByPurchaseDate )
                {
                    //priceOfEachListings = (List<decimal>)groupedByPurchaseDate.Select(items => items.Price);
                    sum = (List<decimal>)group.Select(items => items.Price);
                    
                }
            }


            //Grabbing percentage increase within a week
            var prevWeek = new List<DateTime>();
            var currentWeek = new List<DateTime>();
            decimal total14Days = 0;
            decimal totalCurrentWeek = 0;
            decimal percentIncrease = 0;

            //Grabbing total $ valye of sales for the past 14 days
            for (var prevDate = DateTime.Today; prevDate <= prevDate.AddDays(-14); prevDate = prevDate.AddDays(-1))
            {
                prevWeek.Add(prevDate);
            }
            foreach (DateTime day in prevWeek)
            {
                var soldListingsWitinLast14Days = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Sold" && items.Purchased == day));
                total14Days = soldListingsWitinLast14Days.Select(items => items.Price).Sum();
            }

            //Grabbing total $ value of sales for the past 7 days
            for (var date = DateTime.Today; date <= date.AddDays(-7); date = date.AddDays(-1))
            {
                currentWeek.Add(date);
            }
            foreach (DateTime day in currentWeek)
            {
                var soldListingsWitinLast7Days = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Sold" && items.Purchased == day));
                totalCurrentWeek = soldListingsWitinLast7Days.Select(items => items.Price).Sum();
            }
            
            //Getting the total for last week, this will not be negative as the last 14 days includes the curernt week.
            var totalPrevWeek = total14Days - totalCurrentWeek;

            if (totalCurrentWeek > totalPrevWeek)
            {
                percentIncrease = (totalCurrentWeek - totalPrevWeek) / totalPrevWeek;
            }
            else if(totalPrevWeek == 0)
            {
                percentIncrease = 100;
            }
            else
                percentIncrease = 0;



            var model = new SalesPerDayVM(dates, sum, percentIncrease);
            return model;
        }

        //public async Task<SalesPerDayVM> SalesPerDay()
        //{
        //    var dates = new List<DateTime>();
        //    //var priceOfEachListings = new List<decimal>();
        //    var sum = new List<decimal>();
        //    //TimeSpan span = start - end;


        //    //for (var date = start.Date; date <= end.Date; date.AddDays(-1))
        //    //{
        //    //    dates.Add(date);
        //    //}
        //    for (var date = DateTime.Today.AddMonths(-1); date == DateTime.Today; date.AddDays(1))
        //    {
        //        dates.Add(date);
        //    }

        //    //This loop will grab the sum of sold listings price per day within a month
        //    //Initially this will take all the sold listings for each day of the month, and then group them by their purchased date (not time) incase there are multiple listings sold that day.
        //    foreach (DateTime day in dates)
        //    {
        //        var soldListingsWitinAMonth = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Sold" && items.Purchased == day));
        //        var groupedByPurchaseDate = soldListingsWitinAMonth.GroupBy(items => items.Purchased.Date);

        //        foreach (var group in groupedByPurchaseDate)
        //        {
        //            //priceOfEachListings = (List<decimal>)groupedByPurchaseDate.Select(items => items.Price);
        //            sum = (List<decimal>)group.Select(items => items.Price);

        //        }
        //    }


        //    //Grabbing percentage increase within a week
        //    var prevWeek = new List<DateTime>();
        //    var currentWeek = new List<DateTime>();
        //    decimal total14Days = 0;
        //    decimal totalCurrentWeek = 0;
        //    decimal percentIncrease = 0;

        //    //Grabbing total $ valye of sales for the past 14 days
        //    for (var prevDate = DateTime.Today; prevDate <= prevDate.AddDays(-14); prevDate = prevDate.AddDays(-1))
        //    {
        //        prevWeek.Add(prevDate);
        //    }
        //    foreach (DateTime day in prevWeek)
        //    {
        //        var soldListingsWitinLast14Days = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Sold" && items.Purchased == day));
        //        total14Days = soldListingsWitinLast14Days.Select(items => items.Price).Sum();
        //    }

        //    //Grabbing total $ value of sales for the past 7 days
        //    for (var date = DateTime.Today; date <= date.AddDays(-7); date = date.AddDays(-1))
        //    {
        //        currentWeek.Add(date);
        //    }
        //    foreach (DateTime day in currentWeek)
        //    {
        //        var soldListingsWitinLast7Days = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Sold" && items.Purchased == day));
        //        totalCurrentWeek = soldListingsWitinLast7Days.Select(items => items.Price).Sum();
        //    }

        //    //Getting the total for last week, this will not be negative as the last 14 days includes the curernt week.
        //    var totalPrevWeek = total14Days - totalCurrentWeek;

        //    if (totalCurrentWeek > totalPrevWeek)
        //    {
        //        percentIncrease = (totalCurrentWeek - totalPrevWeek) / totalPrevWeek;
        //    }
        //    else if (totalPrevWeek == 0)
        //    {
        //        percentIncrease = 100;
        //    }
        //    else
        //        percentIncrease = 0;



        //    var model = new SalesPerDayVM(dates, sum, percentIncrease);
        //    return model;
        //}
    }
}
