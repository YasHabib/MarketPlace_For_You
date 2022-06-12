using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels
{
    public class ListingVM
    {
        public ListingVM(ListingInformation listInfo) //fetching information from entity to display these to front end
        {
            ListingId = listInfo.ListingId;
            ProdName = listInfo.ProdName;
            Description = listInfo.Description;
            Category = listInfo.Category;
            Condition = listInfo.Category;
            Price = listInfo.Price;
            City = listInfo.City;
        }

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
