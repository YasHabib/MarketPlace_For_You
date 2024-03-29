﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{/// <summary>
/// Search input entity
/// </summary>
    public class SearchInput : BaseEntity<Guid>
    {
       /// <summary>
       /// Empty constructor
       /// </summary>
        public SearchInput() { }
    /// <summary>
    /// Constructor to add a search string
    /// </summary>
    /// <param name="searchString"></param>
    /// <param name="userId"></param>
        public SearchInput(string searchString, string userId)
        {
            SearchString = searchString;
            UserId = userId;
        }

        /// <summary>
        /// Search String inputed in the search box
        /// </summary>
        public string SearchString { get; set; } = string.Empty;

        /// <summary>
        /// Date/Time the search string was used
        /// </summary>
        public DateTime SearchedDate { get; set; }

        /// <summary>
        /// Foreign key (user id)
        /// </summary>
        [Required]
        //foreign key
        public string UserId { get; set; }

    }
}
