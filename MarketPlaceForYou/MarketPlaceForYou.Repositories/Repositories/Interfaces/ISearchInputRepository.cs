using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface ISearchInputRepository : IBaseRepository<SearchInput, Guid>
    {
        //void SaveSearch(SearchInput entity);
        //Task<List<SearchInput>> Get3(string userId, Func<IQueryable<Listing>, IQueryable<Listing>>? queryFunction = null);
    }
}
