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
        public SearchInputRepository(MKPFYDbContext context)
            : base(context)
        {
        }
    }
}
