using MarketPlaceForYou.Models.ViewModels.Listing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{/// <summary>
/// Listing entity
/// </summary>
    public class Listing
    {
        //empty constructor
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Listing() { }

        //Creating a listing
        /// <summary>
        /// Creating a listing
        /// </summary>
        /// <param name="addListing"></param>
        /// <param name="userId"></param>
        public Listing(ListingAddVM addListing, string userId)
        {
            ProdName = addListing.ProdName;
            Description = addListing.Description;
            Category = addListing.Category;
            Condition = addListing.Condition;
            Price = addListing.Price;
            Address = addListing.Address;
            City = addListing.City;
            UserId = userId;
        }

        /// <summary>
        /// Listing ID (UUid)
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Foreign key (user id)
        /// </summary>
        //relationships
        [Required]
        //foreign key
        public string UserId { get; set; } //naming convention (EntityNameId)
        /// <summary>
        /// Settings relationship with user table
        /// </summary>
        public User? User { get; set; }


        /// <summary>
        /// Listing name
        /// </summary>
        [Required]
        public string ProdName { get; set; } = String.Empty;
        /// <summary>
        /// Description
        /// </summary>
        [Required]
        public string Description { get; set; } =String.Empty;
        /// <summary>
        /// Category
        /// </summary>
        [Required]
        public string Category { get; set; } = String.Empty;
        /// <summary>
        /// COndition
        /// </summary>
        [Required]
        public string Condition { get; set; } = String.Empty ;
        /// <summary>
        /// Price
        /// </summary>
        [Column(TypeName = "money")]
        [Required]
        public decimal Price { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        [Required]
        public string Address { get; set; } = String.Empty;
        /// <summary>
        /// City
        /// </summary>
        [Required]
        public string City { get; set; } = String.Empty;
        /// <summary>
        /// Created date
        /// </summary>
        [Required]
        public DateTime Created { get; set; }
        /// <summary>
        /// Status of a listing. Actual status (ie. Active, Pending, Sold) are written in DB during repository layer and are not hardcoded.
        /// </summary>
        public string Status { get; set; } = String.Empty;
        /// <summary>
        /// Purchased date
        /// </summary>
        public DateTime Purchased { get; set; }
        /// <summary>
        /// Buyer's id for purchasing
        /// </summary> 
        public string? BuyerID { get; set; }
    }
}
