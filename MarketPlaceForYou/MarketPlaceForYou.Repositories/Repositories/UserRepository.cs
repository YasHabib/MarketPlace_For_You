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
    public class UserRepository : BaseRepository<User, string, MKPFYDbContext>, IUserRepository
    {
        private readonly MKPFYDbContext _context;
        public UserRepository(MKPFYDbContext context)
            : base(context)
        {
            _context = context;
        }

        //public async Task<User> TotalPurchase(string userId)
        //{
        //    int purchase = _context.Listings.Where(i => i.BuyerID == userId && i.Status == "Sold").Count();
        //    return purchase;
        //}
        //public void Create(User entity)
        //{
        //    entity.ActiveListings = 0;
        //    entity.Purchases = 0;
        //    entity.TotalPurchase = 0;
        //    entity.TotalSold = 0;
        //    _context.Add(entity);
        //}
        //public async Task<User> GetById(string id)
        //{
        //    var result = await _context.Users.FirstAsync(i => i.Id == id);
        //    return result;
        //}
        //public async Task<List<User>> GetAll(string userId)
        //{
        //    var results = await _context.Users.Where(i=> i.Id != userId).ToListAsync();
        //    return results;
        //}
        //public void Update(User entity)
        //{
        //    _context.Update(entity);
        //}
        //public void Delete(User entity)
        //{
        //    _context.Remove(entity);
        //}
        //public void SoftDelete(User entity)
        //{
        //    entity.IsDeleted = true;
        //}
        //public void BlockUser(User entity)
        //{
        //    entity.IsBlocked = true;
        //}
        //public void UnblockUser(User entity)
        //{
        //    entity.IsBlocked=false;
        //}


    }
}
