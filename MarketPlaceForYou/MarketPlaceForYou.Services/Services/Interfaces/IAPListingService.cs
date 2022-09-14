using MarketPlaceForYou.Models.ViewModels.Listing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IAPListingService
    {
        Task<List<ListingVM>> ActiveListings();
        Task<List<ListingVM>> PendingListings();
        Task<List<ListingVM>> SearchActives(string searchString);
        Task<List<ListingVM>> SearchPendings(string searchString);
        Task SoftDelete(Guid Id);
        Task<ListingVM> ApproveListing(Guid Id);
        Task<ListingVM> RejectListing(Guid Id);
    }
}
