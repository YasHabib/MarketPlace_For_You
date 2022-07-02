﻿using System;
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
        Condition = listInfo.Category;
        Price = listInfo.Price;
        City = listInfo.City;
        Created = listInfo.Created;
        UserId = listInfo.UserId;
    }
    /// <summary>
    /// Listing id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Listing name
    /// </summary>
    public string? ProdName { get; set; }
    /// <summary>
    /// Listing description
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Listing Category
    /// </summary>
    public string? Category { get; set; }
    /// <summary>
    /// Listing Condition (My condition is bad from all the commenting)
    /// </summary>
    public string? Condition { get; set; }
    /// <summary>
    /// Price of listing
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Address of the lister
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// City of the lister
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// Created date
    /// </summary>
    public DateTime Created { get; set; }
    /// <summary>
    /// User id of the person who created it
    /// </summary>
    public string? UserId { get; set; }

}
