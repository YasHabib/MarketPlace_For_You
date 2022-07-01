using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlaceForYou.Models.Entities;

namespace MarketPlaceForYou.Models.ViewModels.Listing
{
    /// <summary>
    /// Creating a listing
    /// </summary>
    public class ListingAddVM
    {
        /// <summary>
        /// Name of the listing
        /// </summary>
        [Required]
        public string? ProdName { get; set; }
        /// <summary>
        /// Description of the listing
        /// </summary>
        [Required]
        public string? Description { get; set; }
        /// <summary>
        /// Category of the listing
        /// </summary>
        [Required]
        public string? Category { get; set; }
        /// <summary>
        /// Condition of the listing
        /// </summary>
        [Required]
        public string? Condition { get; set; }
        /// <summary>
        /// Price of the listing
        /// </summary>
        [Required]
        public decimal Price { get; set; }
        /// <summary>
        /// Address of the lister
        /// </summary>
        [Required]
        public string? Address { get; set; }
        /// <summary>
        /// City of the lister
        /// </summary>
        [Required]
        public string? City { get; set; }
    }
}
