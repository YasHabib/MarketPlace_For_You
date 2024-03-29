﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlaceForYou.Models.Entities;
using static MarketPlaceForYou.Models.Entities.Listing;

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
        public string ProdName { get; set; } = string.Empty;
        /// <summary>
        /// Description of the listing
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty ;

        ///// <summary>
        ///// Category of the listing
        ///// </summary>
        //[Required]
        //public Categories Category { get; set; }

        ///// <summary>
        ///// Condition of the listing
        ///// </summary>
        //[Required]
        //public string Condition { get; set; }

        /// <summary>
        /// Price of the listing
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Address of the lister
        /// </summary>
        [Required]
        public string Address { get; set; } = string.Empty;

        ///// <summary>
        ///// City of the lister
        ///// </summary>
        //[Required]
        //public Cities City { get; set; }

        /// <summary>
        /// Allowing listing to have max 5 of uploads
        /// </summary>
        [Required]
        [MaxLength(5)]
        public ICollection<Guid>? UploadIds { get; set; }

    }
}
