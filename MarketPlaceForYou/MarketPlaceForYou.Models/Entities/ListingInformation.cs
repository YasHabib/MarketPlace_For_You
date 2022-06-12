using MarketPlaceForYou.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.Entities
{
    public class ListingInformation
    {
        //Creating a listing
        public ListingInformation(ListingAddVM addListing)
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
        [ForeignKey("UserId")]
        public virtual UserInformation Users { get; set; }
        //[Display(Name = UserInformation)]
        public virtual Guid UserId { get; set; }

        [Required]
        public string? ProdName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public string? Condition { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
    }
}
