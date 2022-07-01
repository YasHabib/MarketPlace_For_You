using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.Listing
{
    public class ListingUpdateVM
    {
        [Key]
        public Guid Id { get; set; }
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
    }
}
