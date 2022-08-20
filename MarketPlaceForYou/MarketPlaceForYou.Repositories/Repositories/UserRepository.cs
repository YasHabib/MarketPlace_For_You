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

    }
}
