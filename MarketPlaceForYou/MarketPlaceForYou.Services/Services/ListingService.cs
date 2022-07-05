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
        public async Task<ListingVM> GetById(Guid id)
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

        public async Task<List<ListingVM>> SearchWithFilters(string searchString, string city, string category, string userid)
        {
            var results = await _uow.Listings.SearchWithFilters(searchString, city, category, userid);
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }

        public async Task<ListingVM> Purchase(ListingPurchaseVM src, string buyerId)
        {
            var entity = await _uow.Listings.GetById(src.Id);

            entity.BuyerID = buyerId;

            _uow.Listings.Purchase(entity);
            await _uow.SaveAsync();

            var model = new ListingVM(entity);
            return model;
        }

    }
}
