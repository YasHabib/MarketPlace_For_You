using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels.Listing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarketPlaceForYou.Models.Entities.Listing;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IListingService
    {
        Task<ListingVM> Create(ListingAddVM src, string userId);
        Task<ListingVM> Update(ListingUpdateVM src);
        Task<ListingVM> GetById(Guid id);
        //Task<List<ListingVM>> GetAll(string userId);
        Task<List<ListingVM>> GetAll();

        Task<List<ListingVM>> Search(string searchString);
        Task<List<ListingVM>> SearchWithFilters(string? searchString = null, string? city = null, string? category = null, string? condition = null, decimal minPrice = 0, decimal maxPrice = 0);
        Task<List<ListingVM>> GetAllByCity(string city);
        Task<List<ListingVM>> GetAllByCategory(string category);
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
