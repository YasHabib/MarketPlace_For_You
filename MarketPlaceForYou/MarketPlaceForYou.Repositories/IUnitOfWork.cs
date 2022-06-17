using MarketPlaceForYou.Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IListingRepository Listings { get; }

        Task SaveAsync();
    }
}
