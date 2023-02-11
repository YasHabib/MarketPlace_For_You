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
        Task<ListingVM> Create(ListingAddVM src, string userId, Categories category, Cities city, Conditions condition);
        Task<ListingVM> Update(ListingUpdateVM src, Cities city, Categories category, Conditions condition);
        Task<ListingVM> GetById(Guid id);
        //Task<List<ListingVM>> GetAll(string userId);
        Task<List<ListingVM>> GetAll(string userId);

        Task<List<ListingVM>> Search(string searchString, string userId);
        Task<List<ListingVM>> SearchWithFilters(string userId, string? searchString = null, Cities? city = null, Categories? category = null, Conditions? condition = null, decimal minPrice = 0, decimal maxPrice = 0);
        Task<List<ListingVM>> GetAllByCity(Cities city, string userId);
        Task<List<ListingVM>> GetAllByCategory(Categories category, string userId);

        // This interface will implement a function which will retrieve all the listings based on the user's last 3 search results.
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
