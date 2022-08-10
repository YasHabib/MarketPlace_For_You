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
    public class UploadRepository : IUploadRepository
    {
        private readonly MKPFYDbContext _context;

        public UploadRepository(MKPFYDbContext context)
        {
            _context = context;
        }
        public void Create(Upload entity)
        {
            _context.Add(entity);
        }
        public async Task<List<Upload>> GetAll(Func<IQueryable<Upload>, IQueryable<Upload>>? queryFunction)
        {
            List<Upload> results;
            if (queryFunction == null)
                results = await _context.Uploads.ToListAsync();
            else
                results = await queryFunction(_context.Uploads).ToListAsync();
            return results;
        }

        public async Task<List<Upload>> GetAllPerListing(Guid listingId)
        {
            var results = await _context.Uploads.Where(i => i.ListingId == listingId).ToListAsync();
            return results;
        }

        public async Task<Upload> GetById(Guid id)
        {
            var result = await _context.Uploads.FirstAsync(i => i.Id == id);
            return result;
        }
        public void Update(Upload entity)
        {
            _context.Update(entity);
        }
        public void Delete(Upload entity)
        {
            _context.Remove(entity);
        }
    }
}
