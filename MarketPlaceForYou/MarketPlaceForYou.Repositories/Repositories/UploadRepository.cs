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
    public class UploadRepository : BaseRepository<Upload, Guid, MKPFYDbContext>, IUploadRepository
    {
        public UploadRepository(MKPFYDbContext context)
            : base(context)
        {
        }
        //public async Task<List<Upload>> GetAll(Func<IQueryable<Upload>, IQueryable<Upload>>? queryFunction)
        //{
        //    List<Upload> results;
        //    if (queryFunction == null)
        //        results = await _context.Uploads.ToListAsync();
        //    else
        //        results = await queryFunction(_context.Uploads).ToListAsync();
        //    return results;
        //}
    }
}
