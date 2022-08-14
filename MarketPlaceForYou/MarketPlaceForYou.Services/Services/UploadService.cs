using Amazon.S3;
using Amazon.S3.Transfer;
using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels.Upload;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class UploadService : IUploadService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public UploadService(IUnitOfWork uow, IConfiguration config)
        {
            _uow = uow;
            _config = config;
        }

        public async Task<List<UploadResultVM>> UploadImages(List<IFormFile> files)
        {
            var results = new List<UploadResultVM>();

            //Iterate over all files
            foreach(var file in files)
            {
                var newId = Guid.NewGuid(); //This will create a new Guid for each file uploaded, even if its the same file.
                var bucket = _config.GetSection("AWS").GetValue<string>("ImageBucket");
                var region = _config.GetSection("AWS").GetValue<string>("Region");

                //perform the upload
                using (var memoryStream = new MemoryStream())//This will use it and dispose it right after, hence why "using" is used here instead of up top. 
                {
                    await file.CopyToAsync(memoryStream);

                    //upload the file
                    var s3Client = new AmazonS3Client(Amazon.RegionEndpoint.GetBySystemName(region));
                    var fileTransfer = new TransferUtility(s3Client);
                    await fileTransfer.UploadAsync(memoryStream, bucket, newId.ToString());

                }
                //store the file info for reference by other entities
                _uow.Uploads.Create(new Upload
                {
                    Id = newId,
                    Url = $"https://{bucket}.s3.{region}.amazonaws.com/{newId}",
                });
                await _uow.SaveAsync();

                //build our response object
                results.Add(new UploadResultVM{
                    Id = newId
                });

            }
            return results;
        }
        //public async Task<ListingImageVM> AddImageToListing (AddImageToListingVM src)
        //{
        //    //Getting the Id of the images
        //    var images = await _uow.Uploads.GetById(src.Id);

        //    images.ListingId = src.ListingId;
        //    _uow.Uploads.Update(images);
        //    await _uow.SaveAsync();

        //    var model = new ListingImageVM(images);
        //    return model;
        //}
        //public async Task<List<ListingImageVM>> GetAllPerListing(Guid listingId)
        //{
        //    var results = await _uow.Uploads.GetAllPerListing(listingId);
        //    var model = results.Select(images => new ListingImageVM(images)).ToList();
        //    return model;
        //}
    }
}
