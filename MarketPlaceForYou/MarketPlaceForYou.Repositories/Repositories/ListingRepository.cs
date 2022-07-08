using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly MKPFYDbContext _context;

        public ListingRepository(MKPFYDbContext context)
        {
            _context = context;
        }

        //C
        public void Create(Listing entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Status = "Active";
            _context.Add(entity);
        }

        //R
        public async Task<Listing> GetById(Guid id)
        {
            var result = await _context.Listings.FirstAsync(i => i.Id == id);
            return result;
        }
        public async Task<List<Listing>> GetAll(string userId)
        {
            var result = await _context.Listings.Where(i=> i.UserId != userId && i.BuyerID == null).OrderByDescending(i => i.Created).ToListAsync();
            return result;
        }

        public async Task<List<Listing>> GetAllByCity(string city, string userid)
        {
            var result = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null && i.City == city).OrderByDescending(i => i.Created).ToListAsync();
            return result;
        }

        public async Task<List<Listing>> GetAllByCategory(string category, string userid)
        {
            var result = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null && i.Category == category).OrderByDescending(i => i.Created).ToListAsync();
            return result;
        }

        public async Task<List<Listing>> Search(string searchString, string userid)
        {
            var results = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null &&  (
                i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower()))).ToListAsync();
            return results;
        }
        //Test Condition API
        public async Task<List<Listing>> GetAllCond(string condition, string userid)
        {
            var result = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null && i.Condition == condition).OrderByDescending(i => i.Created).ToListAsync();
            return result;
        }

        //Start of searching with filters
        public async Task<List<Listing>> SearchWithFilters(string userid, string? searchString=null, string? city=null, string? category=null, string? condition =null, decimal minPrice =0, decimal maxPrice =0)
        {
            var results = await _context.Listings.ToListAsync();

            ////no fields has been set (not working)
            //if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            //{
            //    return results;
            //}
            //Search + all filters (Works)
             if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0) 
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
                                                                i.City == city &&
                                                                i.Category == category &&
                                                                i.Condition == condition &&
                                                                (minPrice <= i.Price && i.Price <= maxPrice)
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Search +  city, category and conditon (not working, condition)
            else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice == 0 && maxPrice == 0)
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
                                                                i.City == city &&
                                                                i.Category == category &&
                                                                i.Condition == condition
                                                        ).OrderByDescending(i => i.Created).ToListAsync();

            }
            //Search + filter by city, category (works)
            else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
                                                                i.City == city &&
                                                                i.Category == category
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Search + city (Works)
            else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
                                                                i.City == city
                                                       ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //search only (Works)
            else if (!string.IsNullOrEmpty(searchString))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower()))
                                            
                                                        ).ToListAsync();
            }
            //City + Category + Condition + Price (Works)
            else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&                                      
                                                                i.City == city &&
                                                                i.Category == category &&
                                                                i.Condition == condition &&
                                                                (minPrice <= i.Price && i.Price <= maxPrice)
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //City + Category + Condition (not working, condition)
            else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.City == city &&
                                                                i.Condition == condition &&
                                                                i.Category == category
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //City + Category (works)
            else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.City == city &&
                                                                i.Category == category
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //City (works)
            else if (!string.IsNullOrEmpty(city))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.City == city
                                                       ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Category + Condition + Price (Works)
            else if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.Category == category &&
                                                                i.Condition == condition &&
                                                                (minPrice <= i.Price && i.Price <= maxPrice)
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Category + Condition (not working, condition)
            else if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.Category == category &&
                                                                i.Condition == condition
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Category (working)
            else if (!string.IsNullOrEmpty(category))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.Category == category
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Condition + price (works)
            else if (!string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.Condition == condition &&
                                                                (minPrice <= i.Price && i.Price <= maxPrice)
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Condition (not working, not showing 1 Used items)
            else if (!string.IsNullOrEmpty(condition))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.Condition == condition
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Price (works)
            else if (!minPrice.Equals(null) && !maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                (minPrice <= i.Price && i.Price <= maxPrice)
                                                        ).OrderByDescending(i => i.Created).ToListAsync();
            }
            //else
            //{
            //    return results; // doesn't work?
            //}


            return results;
        }//End of searching with filters

        public async Task<List<Listing>> Deals(string userid)
        {
            var results = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null).OrderBy(i => i.Price).Take(16).OrderByDescending(i => i.Created)
                .ToListAsync();
            return results;
        }
        public async Task<List<Listing>> MyActiveListings(string userId)
        {
            var results = await _context.Listings.Where(i => i.Status == "Active" && i.UserId == userId).OrderByDescending(i => i.Created).ToListAsync();
            return results;
        }
        public async Task<List<Listing>> MySoldListings(string userId)
        {
            var results = await _context.Listings.Where(i => i.Status == "Sold" && i.UserId == userId).OrderByDescending(i => i.Created).ToListAsync();
            return results;
        }
        public async Task<List<Listing>> MyPurchases(string userId)
        {
            var results = await _context.Listings.Where(i => i.BuyerID == userId).OrderByDescending(i => i.Created).ToListAsync();
            return results;
        }
        public async Task<List<Listing>> PendingListings(string userId)
        {
            var results = await _context.Listings.Where(i => i.Status == "Pending" && i.UserId == userId).OrderByDescending(i => i.Created).ToListAsync();
            return results;
        }
        //U
        public void RequestPurchase(Listing entity)
        {
            entity.Status = "Pending";
            _context.Update(entity);
        }
        public void ConfirmPurchase(Listing entity)
        {
            entity.Purchased = DateTime.UtcNow;
            entity.Status = "Sold";
            _context.Update(entity);

        }
        public void CancelPurchase(Listing entity)
        {
            entity.Status = "Active";
            _context.Update(entity);
        }
        public void Update(Listing entity)
        {
            _context.Update(entity);
        }
        //D
        public void Delete(Listing entity)
        {
            _context.Remove(entity);
        }
    }
}
