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

        //------------------------------------------------------------CREATE------------------------------------------------
        public void Create(Listing entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Status = "Active";
            _context.Add(entity);
        }

        //------------------------------------------------------------READ------------------------------------------------
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

            var results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null)).ToListAsync();
            if (searchString != null)
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) && 
                                                                (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower()))).OrderByDescending(i => i.Created).ToListAsync();
            if (city != null)
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.City == city).OrderByDescending(i => i.Created).ToListAsync();
            if (category != null)
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) && 
                                                                i.Category == category).OrderByDescending(i => i.Created).ToListAsync();
            if (condition != null)
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                i.Condition == condition).OrderByDescending(i => i.Created).ToListAsync();
            if (minPrice != 0 && maxPrice != 0) 
                results = await _context.Listings.Where(i =>    (i.UserId != userid && i.BuyerID == null) &&
                                                                (minPrice <= i.Price && i.Price <= maxPrice)).OrderByDescending(i => i.Created).ToListAsync();



            ////#1 Search + all filters
            //if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.City == city &&
            //                                                    i.Category == category &&
            //                                                    i.Condition == condition &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#2 Search + city + category + conditon
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice == 0 && maxPrice == 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.City == city &&
            //                                                    i.Category == category &&
            //                                                    i.Condition == condition
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();

            //}
            ////#3 Search + City + Category + Price
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.City == city &&
            //                                                    i.Category == category &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#4 Search + Category + Condition + Price
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.Category == category &&
            //                                                    i.Condition == condition &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#5 Search + City + Condition + Price
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.City == city &&
            //                                                    i.Condition == condition &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#6 Search + city + category
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.City == city &&
            //                                                    i.Category == category
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#6 Search + City +  Price
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.City == city &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#7 Search + City + Condition
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(condition))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.City == city &&
            //                                                    i.Category == category
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#8 Search + Category + Condition
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.Category == category &&
            //                                                    i.Condition == condition
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#9 Search + Category + Price
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(category) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.Category == category &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#10 Search + Condition + Price
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.Condition == condition &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#11 Search + city 
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(city))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.City == city
            //                                           ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#12 Search + Category
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(category))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.Category == category
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#13 Search + Condition
            //else if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(condition))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    i.Condition == condition
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#14 Search + Price
            //else if (!string.IsNullOrEmpty(searchString) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())) &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#15 search only
            //else if (!string.IsNullOrEmpty(searchString))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower()))
            //                                            ).ToListAsync();
            //}
            ////#16 City + Category + Condition + Price
            //else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.City == city &&
            //                                                    i.Category == category &&
            //                                                    i.Condition == condition &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#16 City + Category + Condition
            //else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.City == city &&
            //                                                    i.Condition == condition &&
            //                                                    i.Category == category
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#17 City + Category + Price
            //else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.City == city &&
            //                                                    i.Category == category &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#18 City + Condition + Price
            //else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.City == city &&
            //                                                    i.Condition == condition &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#19 City + Category
            //else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(category) && string.IsNullOrEmpty(condition))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.City == city &&
            //                                                    i.Category == category
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#20 City + Price
            //else if (!string.IsNullOrEmpty(city) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.City == city &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#21 City + Condition
            //else if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(condition))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.City == city &&
            //                                                    i.Condition == condition
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#22 City
            //else if (!string.IsNullOrEmpty(city))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.City == city
            //                                           ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#23 Category + Condition + Price
            //else if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.Category == category &&
            //                                                    i.Condition == condition &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#24 Category + Condition
            //else if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(condition))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.Category == category &&
            //                                                    i.Condition == condition
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#25 Category + Price
            //else if (!string.IsNullOrEmpty(category) && minPrice != 0 && maxPrice != 0)
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.Category == category &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#26 Category
            //else if (!string.IsNullOrEmpty(category))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.Category == category
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#27 Condition + price
            //else if (!string.IsNullOrEmpty(condition) && !minPrice.Equals(null) && !maxPrice.Equals(null))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.Condition == condition &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#28 Condition
            //else if (!string.IsNullOrEmpty(condition))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    i.Condition == condition
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
            //}
            ////#29 Price
            //else if (!minPrice.Equals(null) && !maxPrice.Equals(null))
            //{
            //    results = await _context.Listings.Where(i => (i.UserId != userid && i.BuyerID == null) &&
            //                                                    (minPrice <= i.Price && i.Price <= maxPrice)
            //                                            ).OrderByDescending(i => i.Created).ToListAsync();
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
        //------------------------------------------------------------UPDATE------------------------------------------------
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
        //------------------------------------------------------------DELETE------------------------------------------------
        public void Delete(Listing entity)
        {
            _context.Remove(entity);
        }
    }
}
