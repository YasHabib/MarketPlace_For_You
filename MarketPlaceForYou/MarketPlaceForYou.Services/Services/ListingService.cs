using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Models.ViewModels.SearchInput;
using MarketPlaceForYou.Models.ViewModels.Upload;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceForYou.Services.Services
{
    public class ListingService : IListingService
    {
        private readonly IUnitOfWork _uow;

        public ListingService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        //CRUD
        public async Task<ListingVM> Create(ListingAddVM src, string userId)
        {
            var images = await _uow.Uploads.GetAll(uploads => uploads.Where(upload => src.UploadIds.Contains(upload.Id)));
            var newEntityL = new Listing(src, userId);
            newEntityL.Uploads = images;
            newEntityL.Status = "Active";
            _uow.Listings.Create(newEntityL);
            await _uow.SaveAsync();

            var model = new ListingVM(newEntityL);

            return model;
        }
        public async Task<ListingVM> Update(ListingUpdateVM src)
        {
            //grab entity from database
            var entity = await _uow.Listings.GetById(src.Id);

            entity.ProdName = src.ProdName;
            entity.Description = src.Description;
            entity.Category = src.Category;
            entity.Condition = src.Condition;
            entity.Price = src.Price;
            entity.Address = src.Address;
            entity.City = src.City;

            _uow.Listings.Update(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }

        //retrieving listings
        public async Task<ListingVM> GetById(Guid id)
        {
            var result = await _uow.Listings.GetById(id, item => item.Include(items => items.User).Include(items => items.Uploads));

            var model = new ListingVM(result);
            return model;
        }
        public async Task<List<ListingVM>> GetAll(string userId)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userId).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        public async Task<List<ListingVM>> Deals(string userid)
        {

            ////Trial 1
            ////Gives 200ok but no result, debugged to see the issue, and searchInputs is null
            //var searchInputs = _uow.SearchInputs.Get3(userid).ToString().ToLower();

            //var results = await _uow.Listings.GetAll(items => items.Where(items => items.Description.ToLower().Contains(searchInputs)
            //            || items.ProdName.ToLower().Contains(searchInputs) && items.UserId != userid && items.Status == "Active")
            //            .Include(items => items.User).Include(items => items.Uploads).Take(16));


            ////Trial 2
            ////Grabbing the last 3 search results
            //var searchInputs = _uow.SearchInputs.Get3(userid).ToString().ToLower().ToList();
            //////Default view if there's no search inputs
            //var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userid && items.Status == "Active").OrderByDescending(items => items.Price).Take(16));
            ////List<Listing> results;
            ////looping through each of the search string to get the search results
            //if (searchInputs != null)
            //{
            //    foreach (var searchInput in searchInputs)
            //        results = await _uow.Listings.GetAll(items => items.Where(items => items.Description.ToLower().Contains(searchInput)
            //            || items.ProdName.ToLower().Contains(searchInput) && items.UserId != userid && items.Status == "Active")
            //            .Include(items => items.User).Include(items => items.Uploads).Take(16));
            //}

            //Trial 3
            //Grabbing the last 3 searches
            //var searchInputs = _uow.SearchInputs.Get3(userid);

            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId != userid && items.Status == "Active").OrderByDescending(items => items.Price).Take(16));


            //to view model for displaying to end user as a list
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        //Search and filter
        public async Task<List<ListingVM>> GetAllByCity(string city, string userid)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.City == city && items.UserId != userid).Include(items => items.User).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        public async Task<List<ListingVM>> GetAllByCategory(string category, string userid)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.Category == category && items.UserId != userid).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        public async Task<List<ListingVM>> Search(string searchString, string userid)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => (items.Description.ToLower().Contains(searchString.ToLower()) || items.ProdName.ToLower().Contains(searchString.ToLower())) && items.UserId != userid).Include(items => items.User).Include(items => items.Uploads));
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        //Search with filters
        public async Task<List<ListingVM>> SearchWithFilters(string userid, string? searchString = null, string? city = null, string? category = null, string? condition = null, decimal minPrice = 0, decimal maxPrice = 0)
        {
            var results = await _uow.Listings.SearchWithFilters(userid, searchString, city, category, condition, minPrice, maxPrice);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        //User's listings
        public async Task<List<ListingVM>> MyActiveListings(string userid)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId == userid && items.Status == "Active").Include(items => items.User).Include(items => items.Uploads));
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> MySoldListings(string userid)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.UserId == userid && items.Status == "Sold").Include(items => items.User).Include(items => items.Uploads));
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> MyPurchases(string userId)
        {
            var results = await _uow.Listings.GetAll(items => items.Where(items => items.BuyerID == userId).Include(items => items.User).Include(items => items.Uploads).Include(items => items.Uploads));
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> PendingListings(string userId)
        {
            var results = await _uow.Listings.GetAll(Items => Items.Where(items => items.UserId == userId && items.Status == "Pending").Include(items => items.User).Include(items => items.Uploads));
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }

        public async Task<ListingVM> RequestPurchase(ListingPurchaseVM src, string buyerId)
        {
            var entity = await _uow.Listings.GetById(src.Id, items => items.Include(items => items.User));
            entity.BuyerID = buyerId;
            entity.Status = "Pending";
            _uow.Listings.Update(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }
        //Question on this
        public async Task<ListingVM> ConfirmPurchase(ListingPurchaseVM src)
        {
            var entity = await _uow.Listings.GetById(src.Id, items => items.Include(items => items.User));
            entity.Purchased = DateTime.UtcNow;
            entity.Status = "Sold";

            _uow.Listings.Update(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }
        public async Task<ListingVM> CancelPurchase(ListingPurchaseVM src)
        {
            var entity = await _uow.Listings.GetById(src.Id, items => items.Include(items => items.User));

            entity.BuyerID = null;
            entity.Status = "Active";

            _uow.Listings.Update(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;          
        }
        public async Task Delete(Guid id)
        {
            var entity = await _uow.Listings.GetById(id, items => items.Include(items => items.User));
            _uow.Listings.Delete(entity);
            await _uow.SaveAsync();
        }
        //Admin Panel

    }
}
