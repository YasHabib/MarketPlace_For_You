using MarketPlaceForYou.Models.Entities.Interfaces;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Models.ViewModels.Upload;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{/// <summary>
/// Listing entity
/// </summary>
    public class Listing : BaseEntity<Guid>,IDated
    {
        public enum Categories
        {
            Cars_And_Vehicle,
            Electronics,
            Real_Estate,
            Furniture
        }

        public enum Cities
        {
            Calgary,
            Brooks,
            Canmore
        }

        public enum Conditions
        {
            New,
            Used
        }

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
            Price = addListing.Price;
            Address = addListing.Address;
            UserId = userId;
            //Uploads is set under ListingService/Create, or alternatively
            //Uploads = addListing.UploadIds.Select(id => new Upload { Id = id }).ToList();
        }

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
        [InverseProperty("Listings")]
        public User User { get; set; }


        /// <summary>
        /// Listing name
        /// </summary>
        [Required]
        public string ProdName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        [Required]
        public Categories Category { get; set; }

        /// <summary>
        /// COndition
        /// </summary>
        [Required]
        public Conditions Condition { get; set; }

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
        public string Address { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [Required]
        public Cities City { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        [Required]
        public DateTime Created { get; set; }

        /// <summary>
        /// Status of a listing. Actual status (ie. Active, Pending, Sold) are written in DB during repository layer and are not hardcoded.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Soft deleting a user's listing
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Purchased date
        /// </summary>
        public DateTime Purchased { get; set; }

        /// <summary>
        /// Buyer's id for purchasing
        /// </summary> 
        [InverseProperty("Purchases")]
        public string? BuyerID { get; set; }

        /// <summary>
        /// Allowing listing to have max 5 of uploads
        /// </summary>
        [Required]
        [MaxLength(5)]
        public ICollection<Upload>? Uploads { get; set; }
    }
}
