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
    public class SearchInputRepository : BaseRepository<SearchInput, Guid, MKPFYDbContext>, ISearchInputRepository
    {
        private readonly MKPFYDbContext _context;

        public SearchInputRepository(MKPFYDbContext context)
            : base(context)
        {
            _context = context;
        }
        public async Task<List<SearchInput>> Get3(string userId, Func<IQueryable<Listing>, IQueryable<Listing>>? queryFunction = null)
        {
            var result = await _context.SearchInputs.Where(i => i.UserId == userId).OrderByDescending(i => i.SearchedDate).Take(3).ToListAsync();
            return result;
        }
    }
}
