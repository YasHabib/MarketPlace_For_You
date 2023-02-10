using MarketPlaceForYou.Models.ViewModels.APAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IAPDashboardService
    {
        Task<int> TotalUsers();
        Task<int> TotalListings();
        Task<int> NewUsers();
        Task<decimal> PerDayAvgSalesInYear();
        Task<SalesPerDayVM> SalesPerDayOverAMonth(DateTime start, DateTime end);
        //Task<SalesPerDayVM> SalesPerDay();

    }
}
