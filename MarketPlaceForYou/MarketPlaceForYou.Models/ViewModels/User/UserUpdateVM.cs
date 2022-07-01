using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels
{/// <summary>
/// Updating user info
/// </summary>
    public class UserUpdateVM
    {
        /// <summary>
        /// User's id to be used to retriving the user
        /// </summary>
        [Key]
        public string? Id { get; set; }
        /// <summary>
        /// User's firstname
        /// </summary>
        [Required]
        public string? FirstName { get; set; }
        /// <summary>
        /// User's lastname
        /// </summary>
        [Required]
        public string? LastName { get; set; }
        /// <summary>
        /// User's address
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// user's phone
        /// </summary>
        [Required]
        public string? Phone { get; set; }
        /// <summary>
        /// user's city
        /// </summary>
        [Required]
        public string? City { get; set; }

    }
}
