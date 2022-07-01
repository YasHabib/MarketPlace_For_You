using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.Listing;

public class ListingVM
{
    public ListingVM(Entities.Listing listInfo) //fetching information from entity to display these to front end
    {
        Id = listInfo.Id;
        ProdName = listInfo.ProdName;
        Description = listInfo.Description;
        Category = listInfo.Category;
        Condition = listInfo.Category;
        Price = listInfo.Price;
        City = listInfo.City;
        Created = listInfo.Created;
        UserId = listInfo.UserId;
    }
    public Guid Id { get; set; }
    public string? ProdName { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Condition { get; set; }
    public decimal Price { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public DateTime Created { get; set; }

    public string? UserId { get; set; }

}
