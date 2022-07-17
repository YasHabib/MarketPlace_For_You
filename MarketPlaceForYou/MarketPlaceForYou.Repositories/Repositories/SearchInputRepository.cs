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
    public class SearchInputRepository : ISearchInputRepository
    {
        private readonly MKPFYDbContext _context;

        public SearchInputRepository(MKPFYDbContext context)
        {
            _context = context;
        }
        public void SaveSearch(SearchInput entity)
        {
            entity.SearchedDate = DateTime.UtcNow;
            _context.Add(entity);
        }

        public async Task<List<SearchInput>> GetAll(string userId)
        {
            var result = await _context.SearchInputs.Where(i => i.UserId == userId).Take(3).OrderByDescending(i => i.SearchedDate).ToListAsync();
            return result;
        }
    }
}
