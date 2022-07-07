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
            var result = await _context.Listings.FirstAsync(i => i.Id == id && i.BuyerID == null);
            return result;
        }
        public async Task<List<Listing>> GetAll(string userid)
        {
            var result = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null).OrderByDescending(i => i.Created).ToListAsync();
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

        //Start of searching with filters
        public async Task<List<Listing>> SearchWithFilters(string userid, string? searchString=null, string? city=null, string? category=null, string? condition =null, decimal minPrice=0, decimal maxPrice = 0)
        {

            var results = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null).ToListAsync();
            //no fields has been set
            if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null).OrderByDescending(i => i.Created).ToListAsync();

            }
            //Search + all filters
            else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null)) 
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&  
                                            (
                                                i.City == city &&
                                                i.Category == category &&
                                                i.Condition == condition &&
                                                i.Price >= minPrice &&
                                                i.Price <= maxPrice &&
                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())                                                     
                                            ))).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Search +  city, category and conditon
            else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.City == city &&
                                                i.Category == category &&
                                                i.Condition == condition &&
                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())
                                            ))).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Search + filter by city, category
            else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.City == city &&
                                                i.Category == category &&
                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())
                                            ))).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Search + city
            else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.City == city &&
                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())
                                            ))).OrderByDescending(i => i.Created).ToListAsync();
            }
            //search only
            else if (!string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())
                                            )
                                            ).ToListAsync();
            }
            //City + Category + Condition + Price
            else if (string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.City == city &&
                                                i.Category == category &&
                                                i.Condition == condition &&
                                                i.Price >= minPrice &&
                                                i.Price <= maxPrice
                                            )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //City + Category + Condition
            else if (string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.City == city &&
                                                i.Category == category &&
                                                i.Condition == condition
                                             )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //City + Category
            else if (string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.City == city &&
                                                i.Category == category 
                                            )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //City
            else if (string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.City == city
                                            )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Category + Condition + Price
            else if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.Category == category &&
                                                i.Condition == condition &&
                                                i.Price >= minPrice &&
                                                i.Price <= maxPrice
                                            )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Category + Condition
            else if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.Category == category &&
                                                i.Condition == condition
                                            )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Category
            else if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.Category == category
                                            )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Condition + price
            else if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.Condition == condition &&
                                                i.Price >= minPrice &&
                                                i.Price <= maxPrice
                                             )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Condition
            else if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice.Equals(null) && maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.Condition == condition
                                             )).OrderByDescending(i => i.Created).ToListAsync();
            }
            //Price
            else if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null))
            {
                results = await _context.Listings.Where(i =>
                                            (i.UserId != userid && i.BuyerID == null) &&
                                            (
                                                i.Price >= minPrice &&
                                                i.Price <= maxPrice
                                            )).OrderByDescending(i => i.Created).ToListAsync();
            }
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
            var results = await _context.Listings.Where(i => i.Status == "Active").OrderByDescending(i => i.Created).ToListAsync();
            return results;
        }
        public async Task<List<Listing>> MySoldListings(string userId)
        {
            var results = await _context.Listings.Where(i => i.Status == "Sold").OrderByDescending(i => i.Created).ToListAsync();
            return results;
        }
        public async Task<List<Listing>> MyPurchases(string userId)
        {
            var results = await _context.Listings.Where(i => i.BuyerID == userId).OrderByDescending(i => i.Created).ToListAsync();
            return results;
        }
        public async Task<List<Listing>> PendingListings(string userId)
        {
            var results = await _context.Listings.Where(i => i.Status == "Pending").OrderByDescending(i => i.Created).ToListAsync();
            return results;
        }
        //UserId == userId && i.BuyerID == null
        //U
        public void RequestPurchase(Listing entity)
        {
            entity.Status = "Pending";
            _context.Update(entity);
        }
        public void ConfirmPurchase(Listing entity)
        {
             entity.Purchased = DateTime.UtcNow;
             _context.Update(entity);

        }
        public void CancelPurchase(Listing entity)
        {
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
