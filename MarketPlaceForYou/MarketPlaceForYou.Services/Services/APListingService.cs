using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class APListingService : IAPListingService
    {
        private readonly IUnitOfWork _uow;

        public APListingService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Retriving all listings where the status is Active
        public async Task<List<ListingVM>> ActiveListings()
        {
            var entities = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Active" && items.User.IsBlocked == false).Include(items => items.User).Include(items => items.Uploads));

            var models = entities.Select(listings => new ListingVM(listings)).ToList();
            return models;
        }

        public async Task<List<ListingVM>> SearchActives(string searchString)
        {

            var results = await _uow.Listings.GetAll(items => items.Where(items => (items.Description.ToLower().Contains(searchString.ToLower()) || items.ProdName.ToLower().Contains(searchString.ToLower())) && items.Status == "Active" && items.User.IsBlocked == false).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        //Retriving all listings where the status is Pending
        public async Task<List<ListingVM>> PendingListings()
        {
            var entities = await _uow.Listings.GetAll(items => items.Where(items => items.Status == "Pending" && items.User.IsBlocked == false).Include(items => items.User).Include(items => items.Uploads));

            var models = entities.Select(listings => new ListingVM(listings)).ToList();
            return models;
        }
        public async Task<List<ListingVM>> SearchPendings(string searchString)
        {

            var results = await _uow.Listings.GetAll(items => items.Where(items => (items.Description.ToLower().Contains(searchString.ToLower()) || items.ProdName.ToLower().Contains(searchString.ToLower())) && items.Status == "Pending" && items.User.IsBlocked == false).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        //Approve, Cancel and Delete listings. For approve and cancel, the listing needs to be in "Pending" status.

        public async Task<ListingVM> ApproveListing(Guid Id)
        {
            var entity = await _uow.Listings.GetById(Id, items => items.Include(items => items.User).Include(items => items.Uploads));
            string error;
            if (entity.Status == "Pending")
            {
                entity.Purchased = DateTime.UtcNow;
                entity.Status = "Sold";

                _uow.Listings.Update(entity);
                await _uow.SaveAsync();
            }
            else
                error = "Unable to approve this listing, this listing may have already been confirmed by the seller or has been cancelled";

            var model = new ListingVM(entity);
            return model;
        }

        public async Task<ListingVM> RejectListing(Guid Id)
        {
            var entity = await _uow.Listings.GetById(Id, item => item.Include(item => item.User).Include(item => item.Uploads));

            string error;
            if (entity.Status == "Pending")
            {
                entity.BuyerID = null;
                entity.Status = "Active";

                _uow.Listings.Update(entity);
                await _uow.SaveAsync();
            }
            else
                error = "Listing is not currently available";

            var model = new ListingVM(entity);
            return model;
        }

        public async Task SoftDelete(Guid Id)
        {
            var entity = await _uow.Listings.GetById(Id, item => item.Include(item => item.User).Include(item => item.Uploads));
            string error;

            //Admin can only soft delete listings that are currently active, if a listing is in the process of being sold or is already sold, they will not be deleted
            if (entity.Status == "Active")
            {
                entity.IsDeleted = true;
            }
            else
                error = "Unable to delete this listing, this listing may have already been purchased or currently being purchased";

            _uow.Listings.Update(entity);
            await _uow.SaveAsync();
        }
    }
}
