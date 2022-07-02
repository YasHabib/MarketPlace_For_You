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
        Task<List<ListingVM>> GetAll();

        Task<List<ListingVM>> GetAllByCity(string city);
        Task<List<ListingVM>> GetAllByCategory(string category);
        Task<List<ListingVM>> Deals(decimal price);


        Task Delete(Guid id);
    }
}
