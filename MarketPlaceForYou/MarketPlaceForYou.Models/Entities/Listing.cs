using MarketPlaceForYou.Models.ViewModels.Listing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{
    public class Listing
    {
        //empty constructor
        public Listing() { }

        //Creating a listing
        public Listing(ListingAddVM addListing)
        {
            ProdName = addListing.ProdName;
            Description = addListing.Description;
            Category = addListing.Category;
            Condition = addListing.Category;
            Price = addListing.Price;
            Address = addListing.Address;
            City = addListing.City;
        }

        [Key]
        public Guid ListingId { get; set; }

        //foreign key
        public Guid UserId { get; set; } //naming convention (EntityNameId)

        [Required]
        public string? ProdName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public string? Condition { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
