using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.APAnalytics
{
    /// <summary>
    /// Displays sales per day within a month on AP dashboard
    /// </summary>
    public class SalesPerDayVM
    {
        /// <summary>
        /// Constructor for Sales per day over a month + % increase
        /// </summary>
        /// <param name="dates"></param>
        /// <param name="sales"></param>
        /// <param name="percentageIncrease"></param>
        public SalesPerDayVM(List<DateTime?> dates, List<decimal> sales, decimal percentageIncrease)
        {
            Dates = dates;
            Sales = sales;
            PercentageIncrease = percentageIncrease;
        }

        public List<DateTime?> Dates { get; set; } 
        public List<decimal> Sales { get; set; }  
        public decimal PercentageIncrease { get;set; }
    }
}
