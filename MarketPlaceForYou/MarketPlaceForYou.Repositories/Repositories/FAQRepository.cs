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
    public class FAQRepository : IFAQRepository
    {

        private readonly MKPFYDbContext _context;
        public FAQRepository(MKPFYDbContext context)
        {
            _context = context;
        }
        public void Create(FAQ entity)
        {
            _context.Add(entity);
        }
        public async Task<FAQ> GetById(Guid id)
        {
            var result = await _context.FAQs.FirstAsync(i => i.Id == id);
            return result;
        }
        public async Task<List<FAQ>> GetAll()
        {
            var results = await _context.FAQs.OrderBy(i => i.Title).ToListAsync();
            return results;
        }
        public void Update(FAQ entity)
        {
            _context.Update(entity);
        }
        public void Delete(FAQ entity)
        {
            _context.Remove(entity);
        }

    }
}
