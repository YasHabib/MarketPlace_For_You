using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IListingRepository : IBaseRepository<Listing, Guid>
    {
        Task<List<Listing>> SearchWithFilters(string userid, string? searchString = null, string? city = null, string? category = null, string? condition = null, decimal minPrice = 0, decimal maxPrice = 0,
                                               Func<IQueryable<Listing>, IQueryable<Listing>>? queryFunction = null);
        //Task<List<Listing>> Deals(string userid, Func<IQueryable<Listing>, IQueryable<Listing>>? queryFunction = null);
    }
}
