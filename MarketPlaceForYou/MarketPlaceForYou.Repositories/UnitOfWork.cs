using MarketPlaceForYou.Repositories.Repositories;
using MarketPlaceForYou.Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MKPFYDbContext _context;


        //Note to self: The following codes can be added later during creating endpoint for each entitys (ie. Users,Listing)

        private IUserRepository _userRepository;

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public UnitOfWork(MKPFYDbContext context)
        {
            _context = context;
        }
        //End

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
