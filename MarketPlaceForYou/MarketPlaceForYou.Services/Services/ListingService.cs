using MarketPlaceForYou.Models.Entities;
using MarketPlaceForYou.Models.ViewModels.Listing;
using MarketPlaceForYou.Repositories;
using MarketPlaceForYou.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var newEntity = new Listing(src, userId);
            _uow.Listings.Create(newEntity);
            await _uow.SaveAsync();

            var model = new ListingVM(newEntity);
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
        public async Task Delete(Guid id)
        {
            var entity = await _uow.Listings.GetById(id);
            _uow.Listings.Delete(entity);
            await _uow.SaveAsync();
        }

        //retrieving listings
        public async Task<ListingVM> GetById(Guid id, string userId)
        {
            var result = await _uow.Listings.GetById(id);

            var model = new ListingVM(result);
            return model;
        }

        public async Task<List<ListingVM>> GetAll(string userid)
        {
            var results = await _uow.Listings.GetAll(userid);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        public async Task<List<ListingVM>> Deals(string userid)
        {
            var results = await _uow.Listings.Deals(userid);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        //Search and filter
        public async Task<List<ListingVM>> GetAllByCity(string city, string userid)
        {
            var results = await _uow.Listings.GetAllByCity(city, userid);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        public async Task<List<ListingVM>> GetAllByCategory(string category, string userid)
        {
            var results = await _uow.Listings.GetAllByCategory(category, userid);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        public async Task<List<ListingVM>> Search(string searchString, string userid)
        {
                var results = await _uow.Listings.Search(searchString, userid);
                var models = results.Select(listing => new ListingVM(listing)).ToList();
                return models;
        }

        public async Task<List<ListingVM>> SearchWithFilters(string userid, string? searchString = null, string? city = null, string? category = null, string? condition = null, decimal minPrice = 0, decimal maxPrice = 0)
        {
            var results = await _uow.Listings.SearchWithFilters(userid, searchString, city, category, condition, minPrice, maxPrice);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
        //User's listings
        public async Task<List<ListingVM>> MyActiveListings(string userid)
        {
            var results = await _uow.Listings.MyActiveListings(userid);
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> MySoldListings(string userid)
        {
            var results = await _uow.Listings.MySoldListings(userid);
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> MyPurchases(string userId)
        {
            var results = await _uow.Listings.MyPurchases(userId);
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }
        public async Task<List<ListingVM>> PendingListings(string userId)
        {
            var results = await _uow.Listings.PendingListings(userId);
            var model = results.Select(listings => new ListingVM(listings)).ToList();
            return model;
        }

        public async Task<ListingVM> RequestPurchase(ListingPurchaseVM src, string buyerId)
        {
            var entity = await _uow.Listings.GetById(src.Id);

            entity.BuyerID = buyerId;

            _uow.Listings.RequestPurchase(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }
        public async Task<ListingVM> ConfirmPurchase(ListingPurchaseVM src)
        {
            var entity = await _uow.Listings.GetById(src.Id);

            entity.Status = "Sold";

            _uow.Listings.ConfirmPurchase(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }
        public async Task<ListingVM> CancelPurchase(ListingPurchaseVM src)
        {
            var entity = await _uow.Listings.GetById(src.Id);

            entity.BuyerID = null;
            entity.Status = "Active";

            _uow.Listings.CancelPurchase(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }

    }
}
