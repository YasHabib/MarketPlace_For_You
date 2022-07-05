using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.Listing
{/// <summary>
/// View model for making a purchase
/// </summary>
    public class ListingPurchaseVM
    {
        /// <summary>
        /// Listing id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        ///// <summary>
        ///// Id of the user whom have purchased the item
        ///// </summary>
        //public string? BuyerID { get; set; }
    }
}
