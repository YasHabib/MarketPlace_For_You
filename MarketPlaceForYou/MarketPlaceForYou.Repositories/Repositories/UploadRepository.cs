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
    }
}
