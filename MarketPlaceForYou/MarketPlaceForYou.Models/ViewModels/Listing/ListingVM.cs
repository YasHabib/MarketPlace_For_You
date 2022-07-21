using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Models.ViewModels.Listing;
/// <summary>
/// Viewing a listing to user
/// </summary>
public class ListingVM
{
    /// <summary>
    /// Parameters to update for a listing
    /// </summary>
    public ListingVM(Entities.Listing listInfo) //fetching information from entity to display these to front end
    {
        Id = listInfo.Id;
        ProdName = listInfo.ProdName;
        Description = listInfo.Description;
        Category = listInfo.Category;
        Condition = listInfo.Condition;
        Price = listInfo.Price;
        Address = listInfo.Address;
        City = listInfo.City;
        Created = listInfo.Created;
        UserId = listInfo.UserId;
        //Purchased = listInfo.Purchased;
        //BuyerID = listInfo.BuyerID;
        Status = listInfo.Status;
    }

    /// <summary>
    /// Listing id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Listing name
    /// </summary>
    public string ProdName { get; set; } = string.Empty;

    /// <summary>
    /// Listing description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Listing Category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Listing Condition (My condition is bad from all the commenting)
    /// </summary>
    public string Condition { get; set; } = string.Empty;

    /// <summary>
    /// Price of listing
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Address of the lister
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// City of the lister
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Created date
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// User id of the person who created it
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Listing status
    /// </summary>
    public string? Status { get; set; } = string.Empty;
}
