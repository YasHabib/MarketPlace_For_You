using MarketPlaceForYou.Models.ViewModels.Upload;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services.Interfaces
{
    public interface IUploadService
    {
        Task<List<UploadResultVM>> UploadImages(List<IFormFile> files);
        //Task <ListingImageVM> AddImageToListing(AddImageToListingVM src);
        Task <List<ListingImageVM>> GetAllPerListing(Guid listingId);
    }
}
