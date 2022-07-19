using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Create(User entity);
        Task<User> GetById(string id);
        Task<List<User>> GetAll(string userId);
        decimal UserSales(string userId);
        decimal UserPurchases(string userId, User entity);
        void Update(User entity);
        void Delete(User entity);
    }
}
