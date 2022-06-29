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
<<<<<<< HEAD
        //Task<List<ListingVM>> GetAllByCity(string city);
=======

        Task<List<ListingVM>> GetAllByCity(string city);

>>>>>>> filterByCity
        Task Delete(Guid id);
    }
}
