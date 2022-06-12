using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels
{
    public class ListingUpdateVM
    {
        public Guid ListingId { get; set; }
        public string? ProdName { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Condition { get; set; }
        public int Price { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
