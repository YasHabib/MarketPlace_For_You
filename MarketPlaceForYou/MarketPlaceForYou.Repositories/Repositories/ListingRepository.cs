using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Repositories.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories
{
    public class ListingRepository : BaseRepository<Listing, Guid, MKPFYDbContext>, IListingRepository
    {
        private readonly MKPFYDbContext _context;
        public ListingRepository(MKPFYDbContext context)
            : base(context)
        {
            _context = context;
        }

        //Searching with filters
        public async Task<List<Listing>> SearchWithFilters(string userid, string? searchString = null, string? city = null, string? category = null, string? condition = null, decimal minPrice = 0, decimal maxPrice = 0,
                                                            Func<IQueryable<Listing>, IQueryable<Listing>>? queryFunction = null)
        {
            List<Listing> listings;
            if (queryFunction == null)
                listings = await _entityDbSet.ToListAsync();
            else
                listings = await queryFunction(_entityDbSet).ToListAsync();

            var query = _context.Listings.Where(i => i.UserId != userid && i.Status == "Active" && i.User.IsBlocked == false).Include(i => i.User).Include(i => i.Uploads).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(i => (i.ProdName.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())));
            if (city != null)
                query = query.Where(i => i.City == city);
            if (category != null)
                query = query.Where(i => i.Category == category);
            if (condition != null)
                query = query.Where(i => i.Condition == condition);
            if (minPrice >= 0 && maxPrice != 0)
                query = query.Where(i => (minPrice <= i.Price && i.Price <= maxPrice));
            return query.ToList();
        }
    }
}
