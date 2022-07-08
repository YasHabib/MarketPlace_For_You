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
        Task<ListingVM> GetById(Guid id);
        Task<List<ListingVM>> GetAll(string userid);
        Task<List<ListingVM>> Search(string searchString, string userid);
        Task<List<ListingVM>> SearchWithFilters(string userid, string? searchString = null, string? city = null, string? category = null, string? condition = null, decimal minPrice = 0, decimal maxPrice = 0);
        Task<List<ListingVM>> GetAllByCity(string city, string userid);
        Task<List<ListingVM>> GetAllByCategory(string category, string userid);
        Task<List<ListingVM>> Deals(string userid);
        Task<List<ListingVM>> MyActiveListings(string userId);
        Task<List<ListingVM>> MySoldListings(string userId);
        Task<List<ListingVM>> MyPurchases(string userId);
        Task<List<ListingVM>> PendingListings(string userId);
        Task<ListingVM> RequestPurchase(ListingPurchaseVM src, string buyerId);
        Task<ListingVM> ConfirmPurchase(ListingPurchaseVM src);
        Task<ListingVM> CancelPurchase(ListingPurchaseVM src);
        Task Delete(Guid id);
    }
}
