using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarketPlaceForYou.Models.Entities.Listing;

namespace MarketPlaceForYou.Models.ViewModels.Listing
{
    /// <summary>
    /// Updating an existing listing
    /// </summary>
    public class ListingUpdateVM
    {
        /// <summary>
        /// Id of the listing which will be updated
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of listing
        /// </summary>
        [Required]
        public string ProdName { get; set; } = string.Empty;

        /// <summary>
        /// Description of listing
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;

        ///// <summary>
        ///// Category of listing
        ///// </summary>
        //[Required]
        //public Categories Category { get; set; }

        ///// <summary>
        ///// Condition of the listing
        ///// </summary>
        //[Required]
        //public Conditions Condition { get; set; }

        /// <summary>
        /// Price of the listing
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Address of the listing
        /// </summary>
        [Required]
        public string Address { get; set; } = string.Empty;

        ///// <summary>
        ///// City of the listing
        ///// </summary>
        //[Required]
        //public Cities City { get; set; }
    }
}
