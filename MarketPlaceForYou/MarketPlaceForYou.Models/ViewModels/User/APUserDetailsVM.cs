using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.User
{/// <summary>
/// Detail view model of users from admin panel
/// </summary>
    public class APUserDetailsVM
    {/// <summary>
    /// User's detailed view
    /// </summary>
    /// <param name="src"></param>
        public APUserDetailsVM(Entities.User src)
        {
            Id = src.Id;
            FullName = src.FirstName + " " + src.LastName;
            City = src.City;
            //Total $ value of the items user has sold
            TotalSales = src.Listings.Where(i => i.UserId == Id && i.Status == "Sold").Sum(i => i.Price);
            //Total # of items user has bought
            NumberOfPurchases = src.Listings.Where(i => i.BuyerID == Id && i.Status == "Sold").Count();
        }
        /// <summary>
        /// User id (auth)
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// User's full name
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// user's city
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// $ value of active listings
        /// </summary>
        public decimal TotalSales { get; set; }

        /// <summary>
        /// # of purchased listings
        /// </summary>
        public int NumberOfPurchases { get; set; }
    }
}
