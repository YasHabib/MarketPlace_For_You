using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.User
{
    /// <summary>
    /// User gets added....
    /// </summary>
    public class UserAddVM
    {
        ///// <summary>
        ///// Auth id of the user
        ///// </summary>
        //[Required]
        //public string Id { get; set; } = string.Empty;

        /// <summary>
        /// User's first name
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// User's lastname
        /// </summary>
        [Required]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// user's email
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// user's address
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// User's phone number
        /// </summary>
        [Required]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// user's city
        /// </summary>
        [Required]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Created date
        /// </summary>
        [Required]
        public DateTime Created { get; set; }
    }
}
