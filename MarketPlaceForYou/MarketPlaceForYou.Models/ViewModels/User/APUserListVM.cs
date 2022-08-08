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
        /// User view model when admin is looking at user's list
        /// </summary>
        /// <param name="src"></param>
        public APUserListVM(Entities.User src)
        {
            Id = src.Id;
            FullName = src.FirstName + " " + src.LastName;
            Email = src.Email;
            City = src.City;
            Active = src.ActiveListings;
            Purchases = src.Purchases;
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
        public int Active { get; set; }
        /// <summary>
        /// # of purchased listings
        /// </summary>
        public int Purchases { get; set; }
    }
}
