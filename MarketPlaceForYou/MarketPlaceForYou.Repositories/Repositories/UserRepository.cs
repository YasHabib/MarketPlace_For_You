using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Repositories.Repositories.Interfaces;
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

        public void Update(UserInformation entity)
        {
            _context.Update(entity);
        }
    }
}
