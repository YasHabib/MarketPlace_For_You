using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User, string>
    {
        //Task<User> TotalPurchase(string userId);
    }
}
