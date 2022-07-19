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
    public class UserRepository : IUserRepository
    {
        private readonly MKPFYDbContext _context;

        public UserRepository(MKPFYDbContext context)
        {
            _context = context;
        }
        public void Create(User entity)
        {
            entity.ActiveListings = 0;
            entity.Purchases = 0;
            entity.TotalPurchase = 0;
            entity.TotalSold = 0;
            _context.Add(entity);
        }
        public async Task<User> GetById(string id)
        {
            var result = await _context.Users.FirstAsync(i => i.Id == id);
            return result;
        }
        public decimal UserPurchases(string userId, User entity) //Total $ value of items the user has purchased from other users.
        {
            decimal result = _context.Listings.Where(i => i.BuyerID == userId && i.Status == "Sold").Sum(i => i.Price);
            entity.TotalPurchase = result;
            return result;
        }
        public decimal UserSales(string userId)
        {
            decimal result = _context.Listings.Where(i => i.UserId == userId && i.Status == "Sold").Sum(i => i.Price);
            return result;
        }
        public async Task<List<User>> GetAll(string userId)
        {
            var results = await _context.Users.Where(i=> i.Id != userId).ToListAsync();
            return results;
        }
        public void Update(User entity)
        {
            _context.Update(entity);
        }
        public void Delete(User entity)
        {
            _context.Remove(entity);
        }


    }
}
