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
        Task<List<Listing>> GetAll();
        Task<List<Listing>> Search(string searchString);
        Task<List<Listing>> SearchWithFilters(string searchString, string city, string category);
        Task<List<Listing>> GetAllByCity(string city);
        Task<List<Listing>> GetAllByCategory(string category);
        Task<List<Listing>> Deals(string userid);
        void Delete(Listing entity);
    }
}
