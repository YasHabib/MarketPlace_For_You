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

        public void Create(Listing entity)
        {
            _context.Add(entity);
        }

        public void Update(Listing entity)
        {
            _context.Update(entity);
        }

        public async Task<Listing> GetById(Guid id)
        {
            var result = await _context.Listings.FirstAsync(i => i.Id == id);
            return result;
        }
        public async Task<List<Listing>> GetAllByCity(string city)
        {
            var result = await _context.Listings.Where(i => i.City == city).ToListAsync();
            return result;
        }

        public async Task<List<Listing>> GetAllByCategory(string category)
        {
            var result = await _context.Listings.Where(i => i.Category == category).ToListAsync();
            return result;
        }


        public async Task<List<Listing>> Search(string searchString)
        {
            var results = await _context.Listings.Where(i =>
                i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())).ToListAsync();
            return results;
        }

        public async Task<List<Listing>> SearchWithFilters(string searchString, string city, string category)
        {
            var results = await _context.Listings.Where(i => 
                                                        i.City == city &&
                                                        i.Category == category &&
                                                        (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())
                                                        )).ToListAsync();

            return results;
        }

        //public async Task<List<Listing>> Search(string searchString, string category)
        //{
        //    var result = await _context.Listings.Where(i => i.Category == category &&
        //        i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())).ToListAsync();
        //    return result;
        //}

        //public async Task<List<Listing>> Search(string searchString)
        //{

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        var result = await _context.Listings.Where(i => i.ProdName.Contains(searchString) || i.Description.Contains(searchString)).ToListAsync();
        //        return result;
        //    }
        //    else
        //    {
        //        var result = await _context.Listings.ToListAsync();
        //        return result;
        //    }

        //}


        public async Task<List<Listing>> GetAll()
        {
            var result = await _context.Listings.ToListAsync();
            return result;
        }

        public void Delete(Listing entity)
        {
            _context.Remove(entity);
        }



    }
}
