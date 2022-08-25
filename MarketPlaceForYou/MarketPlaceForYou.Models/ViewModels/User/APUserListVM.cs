using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.User
{/// <summary>
/// These will be the user information viewed to the admin from user's list
/// </summary>
    public class APUserListVM
    {
        /// <summary>
        /// User id (auth)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User's full name
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// user's email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// user's city
        /// </summary>
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// # of active listings
        /// </summary>
        public int TotalActive { get; set; }
        /// <summary>
        /// # of purchased listing
        /// </summary>
        public decimal TotalPurchases { get; set; }
    }
}
