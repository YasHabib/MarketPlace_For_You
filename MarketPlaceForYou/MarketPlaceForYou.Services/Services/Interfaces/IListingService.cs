using MarketPlaceForYou.Models.ViewModels.Listing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IListingService
    {
        Task<ListingVM> Create(ListingAddVM src, string userId);
        Task<ListingVM> Update(ListingUpdateVM src);
        Task<ListingVM> GetById(Guid id, string userid);
        Task<List<ListingVM>> GetAll(string userid);
        Task<List<ListingVM>> Search(string searchString, string userid);
        Task<List<ListingVM>> SearchWithFilters(string searchString, string userid, string? city=null, string? category=null);
        Task<List<ListingVM>> GetAllByCity(string city, string userid);
        Task<List<ListingVM>> GetAllByCategory(string category, string userid);
        Task<List<ListingVM>> Deals(string userid);
        Task<ListingVM> Purchase(ListingPurchaseVM src, string buyerId);
        Task Delete(Guid id);
    }
}
