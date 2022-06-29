using MarketPlaceForYou.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Repositories.Repositories.Interfaces
{
    public interface IListingRepository
    {
        void Update(Listing entity);
        void Create(Listing entity);
        Task<Listing> GetById(Guid id);
        Task<Listing> Search(string searchString);
        Task<List<Listing>> GetAll();

        void Delete(Listing entity);
    }
}
