using MarketPlaceForYou.Models.ViewModels;
using MarketPlaceForYou.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{/// <summary>
/// User entity
/// </summary>
    public class User : BaseEntity<string>
    {/// <summary>
    /// Empty constructor
    /// </summary>
        //Default empty property 
        public User()
        {
        }
        /// <summary>
        /// Adding user
        /// </summary>
        /// <param name="src"></param>
        //For updating/Add user informations
        public User(UserAddVM src)
        {
            Id = src.Id;
            FirstName = src.FirstName;
            LastName = src.LastName;
            Address = src.Address;
            Phone = src.Phone;
            City = src.City;
            Email = src.Email;
        }
        /// <summary>
        /// Updating user
        /// </summary>
        /// <param name="src"></param>
        public User(UserUpdateVM src)
        {
            Id = src.Id;
            FirstName = src.FirstName;
            LastName = src.LastName;
            Address = src.Address;
            Phone = src.Phone;
            City = src.City;
        }
        /// <summary>
        /// User's first name
        /// </summary>
        [Required]
        [MaxLength(255)]    
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// User's last name
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// User's email
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// user's address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// User's phone
        /// </summary>
        [Required]
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// User's city
        /// </summary>
        [Required]
        public string City { get; set; } = string.Empty;

        ///// <summary>
        ///// # of the user's active listings
        ///// </summary>
        //public int ActiveListings { get; set; }
        ///// <summary>
        ///// # of the user's purchases
        ///// </summary>
        //public int Purchases { get; set; }
        ///// <summary>
        ///// $ value of total sold
        ///// </summary>
        //[Column(TypeName = "money")]
        //public decimal TotalSold { get; set; }
        ///// <summary>
        ///// $ value of total purchase
        ///// </summary>
        //[Column(TypeName = "money")]
        //public decimal TotalPurchase { get; set; }

        /// <summary>
        /// Soft deleting an entity,
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// If the user is blocked or not
        /// </summary>
        public bool IsBlocked { get; set; }
        //listing the user has created
        /// <summary>
        ///A collection of listing
        /// </summary>
        public ICollection<Listing> Listings { get; set; }


    }
}
