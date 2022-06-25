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

        public async Task<ListingVM> Create(ListingAddVM src, string userId)
        {
            var newEntity = new Listing(src, userId);
            _uow.Listings.Create(newEntity);
            await _uow.SaveAsync();

            var model = new ListingVM(newEntity);
            return model;
        }

        public async Task<ListingVM> GetById(Guid id)
        {
            var result = await _uow.Listings.GetById(id);

            var model = new ListingVM(result);
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

            var model = new ListingVM(entity);
            return model;            
        }

        public async Task<List<ListingVM>> GetAll()
        {
            var results = await _uow.Listings.GetAll();
            var models = results.Select(listing => new ListingVM(listing)).ToList();
            return models;
        }
    }
}
