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
        void Create(Listing entityL, User entityU);
        Task<Listing> GetById(Guid id);
        Task<List<Listing>> GetAll(string userId);
        Task<List<Listing>> Search(string searchString, string userid);
        Task<List<Listing>> SearchWithFilters(string userid, string? searchString=null,  string? city=null, string? category=null, string? condition=null, decimal minPrice=0, decimal maxPrice=0);
        Task<List<Listing>> GetAllByCity(string city, string userid);
        Task<List<Listing>> GetAllByCategory(string category, string userid);
        Task<List<Listing>> GetAllCond(string condition, string userid); //test
        Task<List<Listing>> Deals(string userid);
        Task<List<Listing>> MyActiveListings(string userId);
        Task<List<Listing>> MySoldListings(string userId);
        Task<List<Listing>> MyPurchases(string userId);
        Task<List<Listing>> PendingListings(string userId);
        void RequestPurchase(Listing entity);
        void ConfirmPurchase(Listing entityL, User entityU);   
        void CancelPurchase(Listing entity);
        void Delete(Listing entity);
    }
}
