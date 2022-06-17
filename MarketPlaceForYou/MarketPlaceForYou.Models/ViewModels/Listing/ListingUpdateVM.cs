using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.Listing
{
    public class ListingUpdateVM
    {
        public Guid Id { get; set; }
        public string? ProdName { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Condition { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
