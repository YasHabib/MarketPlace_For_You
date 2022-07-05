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
            _context.Add(entity);
        }

        //R
        public async Task<Listing> GetById(Guid id, string userId)
        {
            var result = await _context.Listings.FirstAsync(i => i.Id == id && i.UserId != userId && i.BuyerID == null);
            return result;
        }
        public async Task<List<Listing>> GetAll(string userid)
        {
            var result = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null).ToListAsync();
            return result;
        }

        public async Task<List<Listing>> GetAllByCity(string city, string userid)
        {
            var result = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null && i.City == city).ToListAsync();
            return result;
        }

        public async Task<List<Listing>> GetAllByCategory(string category, string userid)
        {
            var result = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null && i.Category == category).ToListAsync();
            return result;
        }

        public async Task<List<Listing>> Search(string searchString, string userid)
        {
            var results = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null &&  (
                i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower()))).ToListAsync();
            return results;
        }

        public async Task<List<Listing>> SearchWithFilters(string searchString, string city, string category, string userid)
        {
            var results = await _context.Listings.Where(i => 
                                                        i.UserId != userid && i.BuyerID == null && 
                                                        i.City == city &&
                                                        i.Category == category &&
                                                        (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())
                                                        )).ToListAsync();

            return results;
        }

        public async Task<List<Listing>> Deals(string userid)
        {
            var results = await _context.Listings.Where(i => i.UserId != userid && i.BuyerID == null).OrderBy(i => i.Price).Take(16)
                .ToListAsync();
            return results;
        }

        //U
        public void Purchase(Listing entity)
        {
            entity.Purchased = DateTime.UtcNow;
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
