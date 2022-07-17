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
        private IListingRepository _lisingRepository;
        private IFAQRepository _faqRepository;
        private IUploadRepository _uploadRepository;
        private ISearchInputRepository _searchInputRepository;

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public IListingRepository Listings
        {
            get
            {
                if (_lisingRepository == null)
                    _lisingRepository = new ListingRepository(_context);
                return _lisingRepository;
            }
        }
        public IFAQRepository FAQs
        {
            get
            {
                if (_faqRepository == null)
                    _faqRepository = new FAQRepository(_context);
                return _faqRepository;
            }
        }
        public IUploadRepository Uploads
        {
            get
            {
                if (_uploadRepository == null)
                    _uploadRepository = new UploadRepository(_context);
                return _uploadRepository;
            }
        }
        public ISearchInputRepository SearchInputs
        {
            get
            {
                if (_searchInputRepository == null)
                    _searchInputRepository = new SearchInputRepository(_context);
                return _searchInputRepository;
            }
        }
        //end 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UnitOfWork(MKPFYDbContext context)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _context = context;
        }
        

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
